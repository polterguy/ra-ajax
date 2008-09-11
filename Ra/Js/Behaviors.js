/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */


// ==============================================================================
//
// All the default Ajax Behaviors which exists in Ra-Ajax core project
// First the Base class for all behaviors.
// Note that the Behaviors have "delayed construction" which actually 
// is fired AFTER the Ra.Control is finished initializing which is why
// you don't see a forward call from the Ra.Beha.init to the
// specific overridden initBehavior which does the actual "initializsation"
// of the Behavior.
//
// ==============================================================================
Ra.Beha = Ra.klass();


// Retrieve function
Ra.Beha.$ = function(id) {
  var idxCtrl = Ra.Control._controls.length;
  while( idxCtrl-- ) {
    var idxBeha = Ra.Control._controls[idxCtrl].options.beha.length;
    while( idxBeha-- ) {
      if( Ra.Control._controls[idxCtrl].options.beha[idxBeha].id == id )
        return Ra.Control._controls[idxCtrl].options.beha[idxBeha];
    }
  }
}


Ra.Beha.prototype = {

  // CTOR
  init: function(id, options) {
    this.options = options;
    this.id = id;
  },

  handleJSON: function(json) {

    // Looping through all "top-level" objects and calling the functions for those keys
    for( var idxKey in json ) {
      this[idxKey](json[idxKey]);
    }
  }
}






// ==============================================================================
//
// This is the BehaviorDraggable
// By using this you can get a control which can be dragged and dropped
// on the screen which again will trigger a server-side event
// which you can trap in your own code.
// It has properties like "snap" which makes it draggable within a "grid"
// and bounds which tells it a rectangular boundary which is the max/min
// boundaries of the draggable area.
//
// ==============================================================================
Ra.BDrag = Ra.klass();


// Inheriting from Ra.Control
Ra.extend(Ra.BDrag.prototype, Ra.Beha.prototype);


// We need to extend the document.body
Ra.BDrag._doc = Ra.extend(document.body, Ra.Element.prototype);


// Creating IMPLEMENTATION of class
Ra.extend(Ra.BDrag.prototype, {

  // Delayed CTOR, actually called by the Ra.Control class
  // for all Behaviors within the Control
  initBehavior: function(parent) {

    this._hasCaption = false;
    this.parent = parent;
    
    Ra.extend(document.body, Ra.Element.prototype);
    
    this.parent.element.observe('mousedown', this.onMouseDown, this);
    Ra.BDrag._doc.observe('mouseup', this.onMouseUp, this);
    Ra.BDrag._doc.observe('mousemove', this.onMouseMove, this);

    this.options = Ra.extend({
      bounds: {left:-2000, top:-2000, width: 4000, height: 4000},
      snap:{x:1,y:1}
    }, this.options || {});

  },

  // Setter for the Bounds Rectangle which determines the max/min
  // position the control can be dragged around within.
  Bounds: function(rc) {
    this.options.bounds = rc;
  },

  // Setter for the Snap Point which determines how the 
  // control is supposed to "snap" when dragged
  Snap: function(pt) {
    this.options.snap = pt;
  },

  // Called when mouse is being pushed DOWN on top of the Control
  onMouseDown: function(event) {
    this._hasCaption = true;
    this._pos = this.pointer(event);
    this._oldX = parseInt(this.parent.element.style.left, 10);
    this._oldY = parseInt(this.parent.element.style.top, 10);
  },

  // Called when mouse is released. Note that this
  // is currently being trapped for the DOM element of the control
  // but should be trapped for the document.body element.
  onMouseUp: function(event) {
    if( !this._hasCaption )
      return;
    this._hasCaption = false;
    delete this._pos;

    // Calling server to update new position of element and potentially raise event
    var newX = parseInt(this.parent.element.style.left, 10);
    var newY = parseInt(this.parent.element.style.top, 10);

    // Calling server with new position and (maybe) raising the Dropped event
    var mouse = this.pointer(event);
    var affectedDroppers = Ra.BDrop.getAffected(mouse.x, mouse.y);
    var dropParams = '';
    if( affectedDroppers.length > 0 ) {
      var idx = affectedDroppers.length;
      dropParams = '&drops=';
      while( idx-- ) {
        if( idx > 0 )
          dropParams += ',';
        dropParams += affectedDroppers[idx].id;
        affectedDroppers[idx].unTouch();
      }
    }
    new Ra.Ajax({
      args:'__RA_CONTROL=' + this.id + '&__EVENT_NAME=dropped' + '&x=' + newX + '&y=' + newY + dropParams,
      raCallback:true,
      onSuccess: this.onFinishedRequest,
      onError: this.onFailedRequest,
      callingContext: this
    });
  },

  // Called when mouse is moved. This too have the same "bug" as the
  // function above.
  onMouseMove: function(event) {
    if( this._hasCaption ) {
      var pos = this.pointer(event);
      var xDelta = pos.x - this._pos.x;
      var yDelta = pos.y - this._pos.y;
      xDelta -= xDelta % this.options.snap.x;
      yDelta -= yDelta % this.options.snap.y;
      var bn = this.options.bounds;
      this.parent.element.style.left = Math.min(Math.max(this._oldX + xDelta, bn.left), bn.width + bn.left) + 'px';
      this.parent.element.style.top = Math.min(Math.max(this._oldY + yDelta, bn.top), bn.height + bn.top) + 'px';

      // Signaling affected droppers
      var affectedDroppers = Ra.BDrop.getAffected(pos.x, pos.y);
      var idx = affectedDroppers.length;
      while( idx-- ) {
        affectedDroppers[idx].touched();
      }
      // Unsignalizing UN-affected ones
      affectedDroppers = Ra.BDrop.getUnAffected(pos.x, pos.y);
      idx = affectedDroppers.length;
      while( idx-- ) {
        affectedDroppers[idx].unTouch();
      }
    }
  },

  onFinishedRequest: function(response) {
    eval(response);
  },

  onFailedRequest: function(status, fullTrace) {
    Ra.Control.errorHandler(status, fullTrace);
  },

  // Returns the x,y position of the mouse from the given event
  pointer: function(event) {
    return {
      x: event.pageX || (event.clientX +
        (document.documentElement.scrollLeft || document.body.scrollLeft)),
      y: event.pageY || (event.clientY +
        (document.documentElement.scrollTop || document.body.scrollTop))
    };
  },

  // Called when Control is being destroyed
  destroy: function() {
    this.parent.element.stopObserving('mousedown', this.onMouseDown, this);
    Ra.BDrag._doc.stopObserving('mouseup', this.onMouseUp, this);
    Ra.BDrag._doc.stopObserving('mousemove', this.onMouseMove, this);
  }
});






// ==============================================================================
//
// This is the BehaviorDroppable
// A Droppable is an element which can handle draggables dropped on
// top of it.
// Inspired from ScriptAculous and Prototype.js
//
// ==============================================================================
Ra.BDrop = Ra.klass();


// Registered droppers on page
Ra.BDrop._droppers = [];


Ra.BDrop.getAffected = function(x, y) {
  var retVal = [];
  var idx = Ra.BDrop._droppers.length;
  while( idx-- ) {
    if( Ra.BDrop._droppers[idx].parent.element.within(x,y) ) {
      retVal.push(Ra.BDrop._droppers[idx]);
    }
  }
  return retVal;
}


Ra.BDrop.getUnAffected = function(x, y) {
  var retVal = [];
  var idx = Ra.BDrop._droppers.length;
  while( idx-- ) {
    if( !Ra.BDrop._droppers[idx].parent.element.within(x,y) ) {
      retVal.push(Ra.BDrop._droppers[idx]);
    }
  }
  return retVal;
}


// Inheriting from Ra.Control
Ra.extend(Ra.BDrop.prototype, Ra.Beha.prototype);


// Creating IMPLEMENTATION of class
Ra.extend(Ra.BDrop.prototype, {

  // Delayed CTOR, actually called by the Ra.Control class
  // for all Behaviors within the Control
  initBehavior: function(parent) {
    this.parent = parent;
    Ra.BDrop._droppers.push(this);

    this.options = Ra.extend({
      touched: null
    }, this.options || {});
  },

  TouchedCssClass: function(value) {
    this.options.touched = value;
  },

  touched: function() {
    if( this.isTouched )
      return;
    this.isTouched = true;
    this._oldClassName = this.parent.element.className;
    this.parent.element.className = this.options.touched;
  },

  unTouch: function() {
    if( !this.isTouched )
      return;
    this.isTouched = false;
    this.parent.element.className = this._oldClassName;
  },

  destroy: function() {
    idx = Ra.BDrop._droppers.length;
    while( idx-- ) {
      if( Ra.BDrop._droppers[idx].id == this.id ) {
        // We have found our instance, idxToRemove now should contain the index of the control
        break;
      }
    }

    // Removes control out from registered controls collection
    Ra.Control._controls.splice(idx, 1);
  }
});






