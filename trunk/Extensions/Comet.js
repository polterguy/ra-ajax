/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */


// Creating class
Ra.Comet = Ra.klass();


// Inheriting from Ra.Control
Ra.extend(Ra.Comet.prototype, Ra.Control.prototype);


// Creating IMPLEMENTATION of class
Ra.extend(Ra.Comet.prototype, {
  init: function(el, opt) {
    this.initControl(el, opt);
    this.options = Ra.extend({
      url:document.getElementsByTagName('form')[0].action,
      previousMsg:''
    }, opt || {});

    if( this.options.enabled)
      this.start();
  },

  start: function() {

    // Only running Comet if Comet is ENABLED...
    if( !this.options.enabled )
      return;

    var T = this;

    // Note that the Comet Control is intentionally NOT using the Ra.Ajax since
    // we don't want to pass in any of the Form Values or anything like that
    // In fact when the Comet Request returns is where the "real" magic happens
    // and we raises the Tick Event on the server through the "normal" Ra.Ajax logic
    // which uses the Ajax Queue implementation, serializes form elements and so on...
    // This is by design!
    // This also means that when the Comet Request returns (and event) it will
    // not directly manipulate the client-side DOM or anything like that.
    // First when the Tick is raised on the server through the normal Ra.Ajax
    // logic the Tick is raised and the client-code run...
    this._xhr = new Ra.XHR(this.options.url, {
      body: this.element.id + '=comet' + '&prevMsg=' + encodeURIComponent(this.options.previousMsg),
      queue:false,
      onTimeout: function() {
        // Silently restarting XHR...
        T.start();
      },
      onSuccess: function(response) {
        if( response !== null && response != '' ) {
          T.options.previousMsg = response;
          T.tick(response);
        } else {
          // No new messages, timeout from server...
          T.start();
        }
      },
      onError: function(status, response) {
        T.error(status, response);
      }
    });
  },

  tick: function(response){
    if( this.options.enabled ) {
      // Raising the "Tick" Event on the server...
      this.callback(response);
    }
  },

  callback: function(evt) {
    var x = new Ra.Ajax({
      args:'__RA_CONTROL=' + this.element.id + '&__EVENT_NAME=tick' + '&__EVENT_ARGS=' + encodeURIComponent(response),
      raCallback:true,
      onSuccess: this.onFinishedTicking,
      callingContext: this
    });
  },

  error: function(status, response){
    Ra.Control.errorHandler(status, response);

    // Intentionally STOPPING Comet Queue...!!!!
  },

  Enabled: function(value) {
    this.options.enabled = value;
    this.start();
  },

  onFinishedTicking: function(response){
    this.onFinishedRequest(response);
    this.start();
  },

  destroyThis: function() {

    // Making sure we STOP ticking
    this.options.enabled = false;

    // Forward call to allow overriding in inherited classes...
    this._destroyThisControl();
  }
});
