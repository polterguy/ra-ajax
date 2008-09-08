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
  init: function(element, options) {

    // Forward call to enable inheritance
    this.initBehavior(element, options);
  },

  initBehavior: function(element, options) {
    this.element = Ra.$(element);
    this.options = options;
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

  // CTOR
  init: function(element, options) {

    // Forward call to enable inheritance
    this.initBDrag(element, options);
  },

  initBDrag: function(element, options) {
    this.initBehavior(element, options);
    this.element.observe('mousedown', this.onMouseDown, this);
    Ra.extend(document.body, Ra.Element.prototype);
    document.body.observe('mouseup', this.onMouseUp, this);
    document.body.observe('mousemove', this.onMouseMove, this);
  },

  onMouseDown: function(event) {
    this._hasCaption = true;
    this._pos = this.pointer(event);
    this._oldX = parseInt(this.element.style.left, 10);
    this._oldY = parseInt(this.element.style.top, 10);
  },

  onMouseUp: function() {
    this._hasCaption = false;
    delete this._pos;
  },

  onMouseMove: function(event) {
    if( this._hasCaption ) {
      var pos = this.pointer(event);
      var xDelta = pos.x - this._pos.x;
      var yDelta = pos.y - this._pos.y;
      var newX = this._oldX + xDelta;
      var newY = this._oldY + yDelta;
      this.element.style.left = newX + 'px';
      this.element.style.top = newY + 'px';
    }
  },

  pointer: function(event) {
    return {
      x: event.pageX || (event.clientX +
        (document.documentElement.scrollLeft || document.body.scrollLeft)),
      y: event.pageY || (event.clientY +
        (document.documentElement.scrollTop || document.body.scrollTop))
    };
  }
});













