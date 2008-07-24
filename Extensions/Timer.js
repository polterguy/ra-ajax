/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under an MIT(ish) kind of license which 
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
    var x = new Ra.Ajax({
      args:'__RA_CONTROL=' + this.element.id + '&__EVENT_NAME=tick',
      raCallback:true,
      onAfter: this.onFinishedTicking,
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
  }
});
