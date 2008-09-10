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
// First the Base class for all behaviors
//
// ==============================================================================
Ra.Beha = Ra.klass();


Ra.Beha.prototype = {

  // CTOR
  init: function(id, options) {
    this.options = options;
    this.id = id;
  }

}






// ==============================================================================
//
// This is the BehaviorDraggable
//
// ==============================================================================
Ra.BDrag = Ra.klass();


// Inheriting from Ra.Control
Ra.extend(Ra.BDrag.prototype, Ra.Beha.prototype);


// Creating IMPLEMENTATION of class
Ra.extend(Ra.BDrag.prototype, {

  initBehavior: function(parent) {

    this._hasCaption = false;
    this.parent = parent;
    this.parent.element.observe('mousedown', this.onMouseDown, this);
    this.parent.element.observe('mouseup', this.onMouseUp, this);
    this.parent.element.observe('mousemove', this.onMouseMove, this);

    this.options = Ra.extend({
      bounds: {left:-2000, top:-2000, width: 4000, height: 4000}
    }, this.options || {});

  },

  Bounds: function(x, y, width, height) {
    this.options.bounds = {x:x, y:y, width:width, height:width};
  },

  onMouseDown: function(event) {
    this._hasCaption = true;
    this._pos = this.pointer(event);
    this._oldX = parseInt(this.parent.element.style.left, 10);
    this._oldY = parseInt(this.parent.element.style.top, 10);
  },

  onMouseUp: function() {
    if( !this._hasCaption )
      return;
    this._hasCaption = false;
    delete this._pos;

    // Calling server to update new position of element and potentially raise event
    var newX = parseInt(this.parent.element.style.left, 10);
    var newY = parseInt(this.parent.element.style.top, 10);

    new Ra.Ajax({
      args:'__RA_CONTROL=' + this.id + '&__EVENT_NAME=dropped' + '&x=' + newX + '&y=' + newY,
      raCallback:true,
      onSuccess: this.onFinishedRequest,
      onError: this.onFailedRequest,
      callingContext: this
    });
  },

  onMouseMove: function(event) {
    if( this._hasCaption ) {
      var pos = this.pointer(event);
      var xDelta = pos.x - this._pos.x;
      var yDelta = pos.y - this._pos.y;
      var newX = this._oldX + xDelta;
      var newY = this._oldY + yDelta;
      var bn = this.options.bounds;
      newX = Math.min(Math.max(newX, bn.left), bn.width + bn.left);
      newY = Math.min(Math.max(newY, bn.top), bn.height + bn.top);
      this.parent.element.style.left = newX + 'px';
      this.parent.element.style.top = newY + 'px';
    }
  },

  pointer: function(event) {
    return {
      x: event.pageX || (event.clientX +
        (document.documentElement.scrollLeft || document.body.scrollLeft)),
      y: event.pageY || (event.clientY +
        (document.documentElement.scrollTop || document.body.scrollTop))
    };
  },

  onFinishedRequest: function(response) {
    eval(response);
  },

  onFailedRequest: function(status, fullTrace) {
    Ra.Control.errorHandler(status, fullTrace);
  },

  destroy: function() {
    this.parent.element.stopObserving('mousedown', this.onMouseDown, this);
    this.parent.element.stopObserving('mouseup', this.onMouseUp, this);
    this.parent.element.stopObserving('mousemove', this.onMouseMove, this);
  }
});

