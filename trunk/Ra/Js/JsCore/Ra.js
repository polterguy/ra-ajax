/*
 * Ra Ajax, Copyright 2008 - Thomas Hansen polterguy@gmail.com
 * Ra is Dual Licensed software, it is licensed under both an MITish license and a
 * pure GPL version 3 license. The licenses should exist on disc where you extracted Ra
 * and will be named license.txt
 * 
 */


/*
 * A general talk about function and conventions in Ra...
 * To inherit one class from another use the Ra.extend function, very similar to prototype, 
 * though for efficiency purposes there are NO $super in Ra.
 * Functions and properties not ment "fiddling with" (overriding, changing or anything like that)
 * starts by convention with an underscore (_)
 * You may some times be encouraged to USE methods that starts with underscores, though mostly not even that
 * but NEVER override them. They're to be considered "final" by convention.
 * 
 */


// Creating main namespace
// All of Ra is contained inside of this namespace
// Ra does neither modify any system objects, except when using the Ra.$ method
// Ra will extend the DOM object with the Ra specific methods
Ra = {}

Ra.Browser = {
  IE:             window.attachEvent && !window.opera,
  Opera:          !!window.opera,
  WebKit:         navigator.userAgent.indexOf('AppleWebKit') != -1,
  Gecko:          navigator.userAgent.indexOf('Gecko') != -1,
  MobileSafari:   !!navigator.userAgent.match(/Apple.*Mobile.*Safari/)
}

// $ method, used to retrieve elements on document
Ra.$ = function(id) {
  var el = document.getElementById(id);
  if( !el )
    return null;
  Ra.extend(el, Ra.Element.prototype);
  return el;
}


// To create a class which will automatically call "init" on objects 
// when created with the arguments applied
Ra.klass = function() {
  return function(){
    if( this.init )
      return this.init.apply(this, arguments);
    else
      throw 'Cannot have a Ra class without an init method...';
  };
}


// Takes one base object and one inherited object where all
// the properties and functions from the base object is copied into the 
// inherited object. The inherited object is returned
// for creating better syntax
Ra.extend = function(inherited, base) {
  for (var prop in base)
    inherited[prop] = base[prop];
  return inherited;
}


// =======================================
// Ra.Element class
// Used for DOM manipulation. 
// Wraps a DOM element.
// =======================================
Ra.Element = Ra.klass();


Ra.extend(Ra.Element.prototype, {

  // Sets content of element (wrapper around innerHTML)
  setContent: function(html) {
    this.innerHTML = html;
    return this;
  },

  // Replaces element with given HTML
  replace: function(html) {

    // Storing id for later to be able to "re-extend" and return "this" back to caller
    var elId = this.id;

    // Creating node to wrap HTML content to replace this content with
    if( this.outerHTML ) {
      this.outerHTML = html;
    } else {
      var range = this.ownerDocument.createRange();
      range.selectNode(this);
      var newEl = range.createContextualFragment(html);

      // Doing replacing
      this.parentNode.replaceChild(newEl, this);
    }

    // Since this effectively REPLACES the element the return
    // value will actually be another physical object so we need
    // to re-retrieve the element, extend it and return the "new" 
    // object back to caller...
    // We're asserting that the Element's ID will be UNCHANGED though
    // we technically have no guarantee of this what-so-ever...
    return Ra.$(elId);
  },

  // Removes element out of DOM
  remove: function() {
    this.parentNode.removeChild(this);
    // We cannot (obviously) return the "this" object here...
  },

  // Inspired from prototype.js
  // Returns an object of { width, height } containing the width and height of the element
  getDimensions: function() {
    var display = this.style.display;
    if (display != 'none' && display != null) // Safari bug
      return {
        width: this.offsetWidth, 
        height: this.offsetHeight
      };

    // All *Width and *Height properties give 0 on elements with display none,
    // so enable the element temporarily
    var els = this.style;
    var orVis = els.visibility;
    var orPos = els.position;
    var orDis = els.display;
    els.visibility = 'hidden';
    els.position = 'absolute';
    els.display = 'block';
    var orWidth = element.clientWidth;
    var orHeight = element.clientHeight;
    els.display = orDis;
    els.position = orPos;
    els.visibility = orVis;
    return {
      width: orWidth, 
      height: orHeight
    };
  },

  // Appends a class name to the class of the element
  addClassName: function(className) {
    this.className += (this.className ? ' ' : '') + className;
    return this;
  },

  // Removes a class name from the element
  removeClassName: function(className) {
    this.className = this.className.replace(
      new RegExp("(^|\\s+)" + className + "(\\s+|$)"), ' ').replace(/^\s+/, '').replace(/\s+$/, '');
    return this;
  },

  // Sets opacity, expects a value between 0.0 and 1.0 where 0 == invisible and 1 == completely visible
  setOpacity: function(value) {
    if( Ra.Browser.IE ) {
      this.style.filter = 'alpha(opacity=' + (Math.round(value * 100)) + ')';
    } else {
      this.style.opacity = value == 1 ? '' : value < 0.0001 ? 0 : value;
    }
    return this;
  },

  // Returns opacity value of element 1 == completely visible and 0 == completely invisible
  getOpacity: function() {
    if( Ra.Browser.IE ) {
      var value = this.style.filter.match(/alpha\(opacity=(.*)\)/);
      if( value[1] )
        return parseFloat(value[1]) / 100;
      return 1.0;
    } else {
      if( this.style.opacity == '' )
        return 1.0;
      return this.style.opacity;
    }
  },

  // Observes an event with the given "func" parameter.
  // The callingContext will be the "this" pointer in the 
  // function call to the "func" when called.
  observe: function(evtName, func, callingContext, extraParams){

    // Creating wrapper to wrap around function event handler
    // Note that this logic only handles ONE event handler per event type / element
    if( !this._wrappers )
      this._wrappers = new Array();

    var wr = function() {
      if( extraParams )
        func.apply(callingContext, extraParams);
      else
        func.call(callingContext);
    };

    this._wrappers[evtName] = wr;

    // Adding up event handler
    if (this.addEventListener) {
      this.addEventListener(evtName, wr, false);
    } else {
      this.attachEvent('on' + evtName, wr);
    }
    return this;
  },

  stopObserving: function(evtName, func) {

    // Retrieving event handler wrapper
    var wr = this._wrappers[evtName];

    // Removing event handler from list
    if (this.removeEventListener) {
      this.removeEventListener(evtName, wr, false);
    } else {
      this.detachEvent('on' + evtName, wr);
    }
    return this;
  }
});





// ==============================================================================
// Ra.XHR class
// Used as wrapper around XMLHTTPRequest object
// ==============================================================================
Ra.XHR = Ra.klass();

// True if an ongoing request is in progress
// Ra.XHR does not allow more than one active request at the time...
Ra.XHR.activeRequest = false;

Ra.extend(Ra.XHR.prototype, {

  init: function(url, options) {
    this.initXHR(url, options);
  },

  initXHR: function(url, options) {
    if( Ra.XHR.activeRequest )
      throw 'Cannot have more than one active XHR request at the time...';
    Ra.XHR.activeRequest = true;
    this.url = url;
    this.options = Ra.extend({
      onSuccess:    function(){},
      onError:      function(){},
      body:         ''
    }, options || {});
    this.start();
  },

  start: function(){

    // Getting transport
    this.xhr = new XMLHttpRequest();
    if( !this.xhr )
      this.xhr = new ActiveXObject('Msxml2.XMLHTTP');
    if( !this.xhr )
      this.xhr = new ActiveXObject('Microsoft.XMLHTTP');

    // Opening transport and setting headers
    this.xhr.open('POST', this.url, true);
    this.xhr.setRequestHeader('Accept', 'text/javascript, text/html, application/xml, text/xml, */*');
    this.xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');

    // Setting body of request
    this.xhr.send(this.options.body);

    // Now we can start checking for readyState (waiting for request to be finished)
    var T = this;
    this.xhr.onreadystatechange = function() {
      if( T.xhr.readyState == 4 )
        T._finished();
    };
  },

  // Called when request is finished
  _finished: function(){
    if( this.xhr.status >= 200 && this.xhr.status < 300 )
      this.options.onSuccess(this.xhr.responseText);
    else
      this.options.onError(this.xhr.status, this.xhr.responseText);

    // Resetting active requests back to false to allow next request to run
    Ra.XHR.activeRequest = false;
  }
});







// ==============================================================================
// Ra.Form class
// Used as wrapper around form elements to create callbacks
// and so on.
// ==============================================================================
Ra.Form = Ra.klass();

Ra.extend(Ra.Form.prototype, {

  init: function(form, options) {
    // If no form is given we automagically wrap the FIRST form on page
    this.form = form || document.getElementsByTagName('form')[0];

    this.options = Ra.extend({
      args:           '',
      url:            this.form.action,
      onFinished:     function(){},
      onError:        function(){},
      callingContext: null
    }, options || {});
  },

  callback: function() {
    var T = this;
    var xhr = new Ra.XHR(this.options.url, {
      body: this.serializeForm() + '&' + this.options.args,
      onSuccess: function(response) {
        if( !T.options.callingContext )
          T.options.onFinished(response)
        else
          T.options.onFinished.call(T.options.callingContext, response);
      },
      onError: function(status, response) {
        if( !T.options.callingContext )
          T.options.onError(status, response)
        else
          T.options.onError.call(T.options.callingContext, status, response);
      }
    });
  },

  // Serializes a form
  // Will return a string which is a vald HTTP POST body for the given form
  // If no form is given, it will search for the _FIRST_ form on the page
  serializeForm: function() {

    // Return value
    var retVal = '';

    // Getting ALL elements inside of form element
    var els = this.form.getElementsByTagName('*');

    // Looping through all elements inside of form and checking to see if they're "form elements"
    for( var idx = 0; idx < els.length; idx++ ) {
      var el = els[idx];

      // According to the HTTP/HTML specs we shouldn't serialize invisible form elements
      // Note that Ra controls does NOE have a "name" but used the "id" attribute for
      // serializing form values back to the server.
      // Though to support serialization of NONE Ra Controls (and pure HTML elements)
      // we check for the "name" attribute first and if it doesn't exists we use the
      // "id" attribute for the POST parameter.
      if( el.style.display != 'none' && !el.disabled ) {
        switch(el.tagName.toLowerCase()) {
          case 'input':
            switch( el.type ) {
              case 'checkbox':
              case 'radio':
                if( el.checked ) {
                  if( retVal.length > 0 )
                    retVal += '&';
                  retVal += (el.name || el.id) + '=' + encodeURIComponent(el.value);
                }
                break;
              case 'hidden':
              case 'password':
              case 'text':
                if( retVal.length > 0 )
                  retVal += '&';
                retVal += (el.name || el.id) + '=' + encodeURIComponent(el.value);
                break;
            }
            break;
          case 'select':
          case 'textarea':
            if( retVal.length > 0 )
              retVal += '&';
            retVal += (el.name || el.id) + '=' + encodeURIComponent(el.value);
            break;
        }
      }
    }
    return retVal;
  }
});










// ==============================================================================
// Ra.Ajax
// Ra.Ajax handles Ajax requests in a que for you
// to make sure there will never be more than one active
// request at any given time.
// This is to avoid having the server and the client getting out
// of sync.
// Also this class automatically serializes the given form (defaults to the
// first form if none give) and handles everything transparently for you.
// ==============================================================================
Ra.Ajax = Ra.klass();


// Static list of queued Ajax requests
Ra.Ajax._activeRequests = new Array();

// Starting message queue pump dispatching all active requests sequentially
Ra.Ajax._startPumping = function() {
  if( !Ra.XHR.activeRequest ) {
    Ra.Ajax._activeRequests[0].start();
  } else {
    setTimeout(function() {
      Ra.Ajax._startPumping();
    }, 50);
  }
}

Ra.extend(Ra.Ajax.prototype, {
  init: function(options) {
    this.options = Ra.extend({

      // Extra arguments passed to the server
      args:'',

      // Used to track on the server whether or not this is a Ra Ajax Callback
      raCallback: false,

      // Form to submit
      form: null, // Defaults to first form on page

      // Called BEFORE request starts (remember this is a queue and it 
      // might take some time after creating this instance before the request 
      // actually is initiated)
      onBefore: function(){},

      // Called AFTER the request is finished with the given response
      onAfter: function(){},

      // Calling context (this pointer) for onBefore and onAfter
      callingContext: null
    }, options || {});

    // Adding up the this request into the list of queued Ajax requests
    Ra.Ajax._activeRequests.push(this);
    if( !Ra.XHR.activeRequest ) {
      this.start();
    } else {
      Ra.Ajax._startPumping();
    }
  },

  start: function() {

    // Raising "onBefore" event
    if( this.options.callingContext )
      this.options.onBefore.call(this.options.callingContext);
    else
      this.options.onBefore();

    // Starting actual request
    var form = new Ra.Form(this.options.form, {
      args: this.options.args,
      callingContext: this,
      onFinished: function(response) {
        this.sliceRequest();
        if( this.options.callingContext )
          this.options.onAfter.call(this.options.callingContext, response);
        else
          this.options.onAfter(response);
      },
      onError: function() {
        this.sliceRequest();
      }
    });
    if( this.options.raCallback ) {
      if( form.options.args != null && form.options.args.length > 0 ) {
        form.options.args += '&';
      }
      form.options.args += '__RA_CALLBACK=true';
    }
    form.callback();
  },

  // Removes request out of queue
  sliceRequest: function() {
    Ra.Ajax._activeRequests = Ra.Ajax._activeRequests.slice(1);
  }
});









// ==============================================================================
// Ra.Effect class
// Base class for DHTML Effects
// This class is used e.g. like this to create a fading effect
//  new Ra.Effect('testAnimationDiv', {
//    onRender: function(pos) {
//      this.element.setOpacity(1.0 - pos);
//    }
//  });
// Possible options are;
//  * duration == number of seconds the effect spends running
//  * onStart == method called before effect starts looping
//  * onFinished == method called when effect is finished running
// The onRender will be called with the Effect as the "this" parameter
// and the "pos" parameter passed will be a value between 0.0 and 1.0 
// indicating how much of the effect "duration" have passed.
// The element passed as the first parameter to the constructor
// is accessible through the "this.element" member and is a Ra extended
// DOM element meaning it has all the extension methods from the 
// Ra.Element class available for use.
// You can pass null as the element parameter in which case (of course) 
// no DOM element will be associated with the Effect.
// Also the onStart and the onFinish will pass the this parameter as
// the Effect instance meaning you can store in e.g. onStart variables in
// the this pointer needed for later use in e.g. onRender calls.
// ==============================================================================
Ra.Effect = Ra.klass();


Ra.extend(Ra.Effect.prototype, {

  // CTOR
  init: function(element, options) {
    this.initEffect(element, options);
  },

  // CTOR implementation to support inheritance 
  // without having to repeat all of this content
  initEffect: function(element, options) {
    this.options = Ra.extend({
      duration: 1.0,
      onStart: function(){},
      onFinished: function(){},
      onRender: null
    }, options || {});
    if( element )
      this.element = Ra.$(element);
    this.options.onStart.call(this);
    this.startTime = new Date().getTime();
    this.finishOn = this.startTime + (this.options.duration * 1000);
    this.loop();
  },

  // Called once every 10 millisecond. Heartbeat of animation
  loop: function() {
    var curTime = new Date().getTime();
    if( curTime >= this.finishOn ) {
      this.render(1.0);
      this.options.onFinished.call(this);
    } else {
      // One tick
      var delta = (curTime - this.startTime) / (this.options.duration * 1000);
      this.render(delta);
      var T = this;
      setTimeout(function(){
        T.loop();
      }, 10);
    }
  },

  // Called by loop every 10 milliesond with "position" of animation
  // Position will be a number betweeb 0.0 and 1.0 where 0.0 == beginning and 1.0 == end
  // and anything between the position of the animation meaning if duration == 3 seconds
  // then after 2 seconds the position will equal 0.6666666.
  render: function(pos) {
    this.options.onRender.call(this, pos);
  }
});




