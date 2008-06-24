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

  // Modified from prototype.js
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

  // Returns the height of the element
  getHeight: function() {
    return this.getDimensions().height;
  },

  // Returns the width of the element
  getWidth: function() {
    return this.getDimensions().width;
  },

  // Sets the width, expects an INTEGER value, appends 'px' meaning this is a PIXEL operation
  setWidth: function(value) {
    this.style.width = value + 'px';
    return this;
  },

  // Sets the height, expects an INTEGER value, appends 'px' meaning this is a PIXEL operation
  setHeight: function(value) {
    this.style.height = value + 'px';
    return this;
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
  setOpacity: function(value){
    this.style.opacity = value == 1 ? '' : value < 0.0001 ? 0 : value;
    return this;
  },

  // Returns opacity value of element 1 == completely visible and 0 == completely invisible
  getOpacity: function() {
    return this.style.opacity;
  },

  // Returns the integer value of the left styled position
  getLeft: function(){
    return parseInt(this.style.left, 10) || 0;
  },

  // Returns the integer value of the top styled position
  getTop: function(){
    return parseInt(this.style.top, 10) || 0;
  },

  // Sets the left position value of the element. Note the 'px' is appended meaning this is a PIXEL operation
  setLeft: function(value) {
    this.style.left = value + 'px';
    return this;
  },

  // Sets the top position value of the element. Note the 'px' is appended meaning this is a PIXEL operation
  setTop: function(value) {
    this.style.top = value + 'px';
    return this;
  }
});











