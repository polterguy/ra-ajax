/*
 * Ra Ajax, Copyright 2008 - Thomas Hansen
 * All JS methods and objects are inside of the
 * Ra namespace.
 */


// Creating main namespace
Ra = {}


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


// Element class, used as helper to manipulate DOM elements
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

  // Set element to either visible or in-visible
  setVisible: function(value) {
    this.style.display = value ? '' : 'none';
    return this;
  },

  // Removes element out of DOM
  remove: function() {
    this.parentNode.removeChild(this);
    // We cannot (obviously) return the "this" object here...
  },

  // Taken and modified from prototype.js
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
    var originalVisibility = els.visibility;
    var originalPosition = els.position;
    var originalDisplay = els.display;
    els.visibility = 'hidden';
    els.position = 'absolute';
    els.display = 'block';
    var originalWidth = element.clientWidth;
    var originalHeight = element.clientHeight;
    els.display = originalDisplay;
    els.position = originalPosition;
    els.visibility = originalVisibility;
    return {
      width: originalWidth, 
      height: originalHeight
    };
  },

  // Returns the height of the element
  getHeight: function() {
    return this.getDimensions().height;
  },

  // Returns the width of the element
  getWidth: function() {
    return this.getDimensions().width;
  },

  setWidth: function(value) {
    this.style.width = value + 'px';
    return this;
  },

  setHeight: function(value) {
    this.style.height = value + 'px';
    return this;
  },

  addClassName: function(className) {
    this.className += (this.className ? ' ' : '') + className;
    return this;
  },

  removeClassName: function(className) {
    this.className = this.className.replace(
      new RegExp("(^|\\s+)" + className + "(\\s+|$)"), ' ').replace(/^\s+/, '').replace(/\s+$/, '');
    return this;
  }
});











