/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */


// ==============================================================================
// Ra.Control class
// Wraps a server-side Ra Ajax Control
// Every Control in Ra have its client-side object
// attached to it to help facilitate with event listening,
// server dispatching and so on.
// This is the "bas class" for those Client-Side objects.
// Also many server-side controls will use this class directly
// since there will be little use for creating specific wrappers
// for all controls since this class will be sufficiant enough
// for wrapping the server-side control.
// ==============================================================================


(function(){

Ra.Control = Ra.klass();


// "Optimized" constructor for creating a new Ra.Control without having to use the "new" keyword and
// the whole name of the Control class...
// Saves a significant amount of "dynamic" data for Ra...
Ra.C = function(el, options) {
  return new Ra.Control(el, options);
};


// Static array which contains all the client-side registered controls
Ra.Control._controls = [];


Ra.Control.errorHandler = function(status, fullTrace) {
  if( status != 0 ) // Probably "unload" process
    alert(status + '\r\n' + fullTrace);
}


// Static method to retrieve a specific Ra control
// Pass in an ID and get the Ra.Control instance of the Control with the given ID
Ra.Control.$ = function(id) {
  for( var idx = 0; idx < Ra.Control._controls.length; idx++ ) {
    if( Ra.Control._controls[idx].element.id == id ) {
      return Ra.Control._controls[idx];
    }
  }
  return null;
};


Ra.Control.prototype = {

  // CTOR
  init: function(element, options) {

    // Forward call to enable inheritance
    this.initControl(element, options);
  },

  initControl: function(element, options) {

    // Wrapping DOM element
    this.element = Ra.$(element);

    // Setting default options
    this.options = Ra.extend({
      // Defaults here...
      evts: [],

      // If true, focus will be set to control initially
      focus: false,

      // If true, the contents of the control will be initially selected
      select: false,

      // If set defines the element of the actual control
      ctrl: null,

      // Behvaiors
      beha: {},

      // If set defines the element of an associated label which contains the text value and so on...
      label: null
    }, options || {});

    // Checking to see if a "real" control was passed
    if( this.options.ctrl ) {
      this.options.ctrl = Ra.$(this.options.ctrl);
    }

    // Checking to see if an extra Label was passed
    if( this.options.label ) {
      this.options.label = Ra.$(this.options.label);
    }

    // Setting focus to control (of we should)
    if( this.options.focus ) {
      this.Focus();
    }

    // Selecting contents of control (if we should)
    if( this.options.select ) {
      this.element.select();
    }

    // Registering control
    Ra.Control._controls.push(this);

    // Creating event handlers for the client-side events needed to be dispatched 
    // back to server
    this.initEvents();

    // Initializing behaviors
    var idx = this.options.beha.length;
    while( idx-- ) {
      this.options.beha[idx].initBehavior(this);
    }

  },

  // This is the method being called from the server-side when
  // we have messages sent to this control.
  // To handle specific data transfers for your controls
  // create a method with the exact same name as the "key" value
  // of the JSON value in your overridden Control class.
  // Normally these methods should be easy to spot in an extended control
  // since they should (by convention) start with a CAPITAL letter to
  // mimick the looks of a property...
  handleJSON: function(json) {

    // Looping through all "top-level" objects and calling the functions for those keys
    for( var idxKey in json ) {
      this[idxKey](json[idxKey]);
    }
  },



  // JSON parser methods, called by server through the handleJSON function
  // These functions are easy to spot since they all starts with a CAPITAL 
  // letter (by convention) and they all take ONE parameter.
  

  // Expects only a string
  CssClass: function(value) {
    this.element.className = value;
  },

  // Expects and array of arrays where each array-item is a key/value object
  // and the key (first sub-item in array) is the name of the style property 
  // and the value (second sub-item array) its value
  // Note you can also use this one to REMOVE styles by having an empty string 
  // as the "value" part.
  AddStyle: function(values) {
    for( var idx = 0; idx < values.length; idx++ ) {
      if( values[idx][0] == 'opacity' )
        this.element.setOpacity(values[idx][1]);
      else
        this.element.style[values[idx][0]] = values[idx][1];
    }
  },

  // Expects only a Text string, does a replace on the innerHTML with the updated text string
  // Useful for labels, textareas and so on...
  Text: function(value) {
    (this.options.label || this.element).setContent(value);
  },

  // Expects a single character - Sets the access key (ALT + value) for giving focus to control
  AccessKey: function(value) {
    (this.options.ctrl || this.element).accesskey = value;
  },

  // Expects a text value, sets the "value" of the control to the given value
  // Useful for TextBoxes (input type="text") and so on...
  Value: function(value) {
    this.element.value = value;
  },

  // Sets focus to control
  Focus: function() {
    this.element.focus();
  },

  // Selects a range from e.g. a TextBox
  Select: function() {
    this.element.select();
  },

  // Expects a type - defines type of control (text, password etc...)
  Type: function(value) {
    (this.options.ctrl || this.element).type = value;
  },

  // Expects any value, will set that property of the element
  // Useful for sending "generic" attributes over to the element
  Generic: function(values) {
    for( var idx = 0; idx < values.length; idx++ ) {
      (this.options.ctrl || this.element)[values[idx][0]] = values[idx][1];
    }
  },



  // Initializes all events on control
  initEvents: function() {
    var evts = this.options.evts;
    var idx = evts.length;
    while( idx-- ) {
      // This one will prioritize the third event parameter, then the ctrl option and finally
      // the this.element if the two previous was undefined or not given
      (evts[idx].length > 2 ? Ra.$(evts[idx][2]) : (this.options.ctrl || this.element)).observe(
        evts[idx][0], 
        this.onEvent, 
        this, 
        [evts[idx][0], evts[idx][1]] );
    }
  },

  getValue: function() {
    return (this.options.ctrl || this.element).value;
  },

  checkValueForKeyUp: function() {
    // This logic will actually HALT the Ajax Request until the user have NOT 
    // typed anything into the Control for more than 0.5 seconds...
    if( this.getValue() == this._oldValue ) {
      var x = new Ra.Ajax({
        args:'__RA_CONTROL=' + this.element.id + '&__EVENT_NAME=keyup',
        raCallback:true,
        onSuccess: this.onFinishedRequest,
        callingContext: this
      });
    } else {
      this._oldValue = this.getValue();
      var T = this;
      setTimeout(function() {
        T.checkValueForKeyUp();
      }, 500);
    }
  },

  // Called when an event is raised, the parameter passed is the this.options.serverEvent instance 
  // which we will use to know how to call our server
  onEvent: function(evt, shouldStop, domEvt) {
    if( evt == 'keyup' ) {
      // This one needs SPECIAL handling to not drain resources
      if( !this._oldValue ) {
        this._oldValue = this.getValue();
        var T = this;
        setTimeout(function() {
          T.checkValueForKeyUp();
        }, 500);
      }
    } else {
      this.callback(evt);
    }
    if( shouldStop ) {
      // Event is supposed to be stopped
      domEvt.stopped = true;
      domEvt.cancelBubble = true;
      return false;
    }
  },

  callback: function(evt) {
    new Ra.Ajax({
      args:'__RA_CONTROL=' + this.element.id + '&__EVENT_NAME=' + evt,
      raCallback:true,
      onSuccess: this.onFinishedRequest,
      onError: this.onFailedRequest,
      callingContext: this
    });
  },

  onFinishedRequest: function(response) {
    if( this._oldValue )
      delete this._oldValue;
    eval(response);
  },

  onFailedRequest: function(status, fullTrace) {
    if( this._oldValue )
      delete this._oldValue;
    Ra.Control.errorHandler(status, fullTrace);
  },



  reRender: function(html) {

    // Since we're re-rendering the control we have to destroy 
    // the child controls in addition to unlistening all the event handlers
    // for the "this control" before re initializing the event handlers again 
    // after replacing the HTML.
    // The "new child controls" will initialize themselves...
    this._destroyChildControls();
    this._unlistenEventHandlers();

    this.element = this.element.replace(html);

    // Then we must RE init the events
    this.initEvents();
  },

  // Called when control is destroyed
  // This one will un-register the control in the control collection, clean up
  // any resources consumed by the control and replace the DOM element with a
  // wrapper span...
  // This one will also destroy all CHILD controls of the control...
  // If you override this method you probably will want to call the base
  // implementation called "destroyControl"!
  destroy: function() {

    // Forward calling to enable inheritance...
    this._destroyChildControls();

    // Destroying this
    this.destroyThis();

    // Replacing the control's HTML with a "wrapper span" so we can re-create it later
    // in its exactly correct position...
    // Note that since all other "child controls" are children DOM elements of the "this DOM element"
    // there is no need to do this for the child controls since their HTML will disappear
    // anyway.
    this.element.replace('<span id="' + this.element.id + '" style="display:none;" />');
  },

  // This function will search for child controls and make sure those too are detroyed...
  _destroyChildControls: function() {

    // Since some controls may be children of the "this" widget we must
    // collect all those widgets too and call destroy on those too
    var children = [];

    // First we must find all the objects which are CHILD objects
    // to the current one (being destroyed)
    // Then we must destroy all those objects (excluding self, self is handled other places...)
    var idx = Ra.Control._controls.length;
    while( idx-- ) {

      // Checking to see that this is NOT the "this" control
      if( Ra.Control._controls[idx].element.id.length > this.element.id.length ) {
        if( Ra.Control._controls[idx].element.id.indexOf(this.element.id) === 0 ) {
          children.push(Ra.Control._controls[idx]);
        }
      }
    }

    // Now looping through and destroying all objects
    var idx = children.length;
    while( idx-- ) {
      children[idx].destroyThis();
    }
  },



  // Destruction implementation
  // If you override this (which you often will end up doing) in your own derived classes
  // then you should make sure you call the _destroyThisControl method to make
  // sure you don't leak memory and gets the "basic" functionality from destroy...
  destroyThis: function() {

    // Forward call to allow overriding in inherited classes...
    this._destroyThisControl();
  },



  // Implementation of destroy
  // Basically unlisetens all events and removes object out 
  // of registered controls collection
  _destroyThisControl: function() {

    var idx = this.options.beha.length;
    while( idx-- ) {
      this.options.beha[idx].destroy();
    }

    // Unregistering the event handlers for this control
    this._unlistenEventHandlers();

    // Looping through registered controls to remove the "this instance"
    idx = Ra.Control._controls.length;
    while( idx-- ) {
      if( Ra.Control._controls[idx].element.id == this.element.id ) {
        // We have found our instance, idxToRemove now should contain the index of the control
        break;
      }
    }

    // Removes control out from registered controls collection
    Ra.Control._controls.splice(idx, 1);
  },



  _unlistenEventHandlers: function() {
    // Unlistening all event observers to avoid leaking memory
    var evts = this.options.evts;
    var idx = evts.length;
    while( idx-- ) {
      (this.options.ctrl || this.element).stopObserving(evts[idx][0], this.onEvent, this);
    }
  }
}
})();

























