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
  }
});











