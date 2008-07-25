/*
 * Ra Ajax - An Ajax Library for Mono ++
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under an MIT(ish) kind of license which 
 * can be found in the license.txt file on disc.
 * 
 */


// Creating class
Ra.RichEdit = Ra.klass();


// Inheriting from Ra.Control
Ra.extend(Ra.RichEdit.prototype, Ra.Control.prototype);


// Creating IMPLEMENTATION of class
Ra.extend(Ra.RichEdit.prototype, {
  init: function(el, opt) {
    this.initControl(el, opt);
    this.initRichEdit();
  },

  getValueElement: function() {
    return Ra.$(this.element.id + '__VALUE');
  },

  getSelectedElement: function() {
    return Ra.$(this.element.id + '__SELECTED');
  },

  initRichEdit: function() {
    this.options.label.contentEditable = true;
    this.getValueElement().value = this.element.getContent();
  }
});
