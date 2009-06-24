/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 * Authors: 
 * Thomas Hansen (thomas@ra-ajax.org)
 * Kariem Ali (kariem@ra-ajax.org)
 * 
 */


(function(){

// Creating class
Ra.GlobalUpdater = Ra.klass();


// Inheriting from Ra.Control
Ra.extend(Ra.GlobalUpdater.prototype, Ra.Control.prototype);

Ra.GlobalUpdater._current = null;
Ra.GlobalUpdater._widget = null;


// Creating IMPLEMENTATION of class
Ra.extend(Ra.GlobalUpdater.prototype, {

  init: function(el, opt) {
    this.initControl(el, opt);
    this.options = Ra.extend({
      delay:1000,
      maxOpacity:1
    }, this.options || {});
    if( Ra.GlobalUpdater._current ) {
      throw "Can't have more than one active GlobalUpdater at the same time on the same page!";
    }
    Ra.GlobalUpdater._current = this;
    this._oldCallback = Ra.Form.prototype.callback;
    Ra.Form.prototype.callback = this.callback;
  },

  callback: function() {
    var T = Ra.GlobalUpdater._current;
    Ra.GlobalUpdater._widget = this;
    T._previousTimeout = setTimeout(function() {
      T.startAnimation();
    }, T.options.delay);
    T._active = true;
    T._oldCallback.apply(this, [T.onSuccess, T.onError]);
  },

  startAnimation: function() {
    this._previousTimeout = null;
    if(this._active) {
      var T = this;
      this._effect = Ra.E(T.element, {
        onStart: function() {
          this.element.setOpacity(0);
          this.element.setStyle('display','');
        },
        onFinished: function() {
          this.element.setOpacity(T.options.maxOpacity);
          T._effect = null;
        },
        onRender: function(pos) {
          this.element.setOpacity(pos * T.options.maxOpacity);
        }, 
        duration:1000, 
        transition:'Explosive'
      });
    }
  },

  onSuccess: function(response) {
    var T = Ra.GlobalUpdater._current;
    T._active = false;
    var widget = Ra.GlobalUpdater._widget;
    if( T._previousTimeout !== null) {
      clearTimeout(T._previousTimeout);
      T._previousTimeout = null;
    }
    T.element.setStyle('display', 'none');
    T.element.setOpacity(0);
    if( !widget.options.callingContext ) {
      widget.options.onFinished(response);
    } else {
      widget.options.onFinished.call(widget.options.callingContext, response);
    }
  },

  onError: function(status, response) {
    var T = Ra.GlobalUpdater._current;
    T._active = false;
    var widget = Ra.GlobalUpdater._widget;
    if( T._previousTimeout !== null) {
      clearTimeout(T._previousTimeout);
      T._previousTimeout = null;
    }
    if( T._effect ) {
      T._effect.stopped = true;
    }
    T.element.setStyle('display', 'none');
    T.element.setOpacity(0);
    if( !widget.options.callingContext ) {
      widget.options.onError(status, response);
    } else {
      widget.options.onError.call(T.options.callingContext, status, response);
    }
  },

  Delay: function(value) {
    this.options.delay = value;
  },

  MaxOpacity: function(value) {
    this.options.maxOpacity = value;
  },

  destroyThis: function() {

    // Restoring old callback
    Ra.Form.prototype.callback = this._oldCallback;
    Ra.GlobalUpdater._current = null;

    // Forward call to allow overriding in inherited classes...
    this._destroyThisControl();
  }
});})();
