/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */


// Creating class
Ra.Timer = Ra.klass();


// Inheriting from Ra.Control
Ra.extend(Ra.Timer.prototype, Ra.Control.prototype);


// Creating IMPLEMENTATION of class
Ra.extend(Ra.Timer.prototype, {
  init: function(el, opt) {
    this.initControl(el, opt);
    this.options = Ra.extend({
      duration:1000
    }, this.options || {});
    if( this.options.enabled)
      this.start();
  },

  start: function() {
    var T = this;
    setTimeout(function(){
      T.tick();
    }, this.options.duration);
  },

  tick: function(){
    if( this.options.enabled ) {
      this.callback();
    }
  },

  callback: function() {
    var x = new Ra.Ajax({
      args:'__RA_CONTROL=' + this.element.id + '&__EVENT_NAME=tick',
      raCallback:true,
      onSuccess: this.onFinishedTicking,
      callingContext: this
    });
  },

  Enabled: function(value) {
    this.options.enabled = value;
  },

  onFinishedTicking: function(response){
    this.onFinishedRequest(response);
    if( this.options.enabled )
      this.start();
  },

  destroyThis: function() {

    // Making sure we STOP ticking
    this.options.enabled = false;

    // Forward call to allow overriding in inherited classes...
    this._destroyThisControl();
  }
});
