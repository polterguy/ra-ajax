/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */


(function(){

// Creating class
Ra.ResHand = Ra.klass();


// Inheriting from Ra.Control
Ra.extend(Ra.ResHand.prototype, Ra.Control.prototype);


// Creating IMPLEMENTATION of class
Ra.extend(Ra.ResHand.prototype, {
  init: function(el, opt) {
    this.initControl(el, opt);
    Ra.extend(window, Ra.Element.prototype);
    window.observe('resize', this.onResized, this);
    this._size = {      width: window.innerWidth || document.body.clientWidth,       height: window.innerHeight || document.body.clientHeight    };    this.callback(this._size);
  },
  onResized: function() {    this._size = {      width: window.innerWidth || document.body.clientWidth,       height: window.innerHeight || document.body.clientHeight    };    var T = this;    setTimeout(function(){      T.checkToSeeIfCallback();    }, 500);  },
  checkToSeeIfCallback: function() {
    var sz = {      width: window.innerWidth || document.body.clientWidth,       height: window.innerHeight || document.body.clientHeight    };    if( sz.width == this._size.width && sz.height == this._size.height ) {      this.callback(this._size);    } else {      this._size = sz;      var T = this;      setTimeout(function(){        T.checkToSeeIfCallback();      }, 500);    }  },

  callback: function(sz) {
    new Ra.Ajax({
      args:'__RA_CONTROL=' + this.element.id + '&__EVENT_NAME=resized' + '&width=' + sz.width + '&height=' + sz.height,
      raCallback:true,
      onSuccess: this.onFinishedRequest,
      callingContext: this
    });
  }});})();
