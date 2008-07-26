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
    Ra.Form.preSerializers.push({handler: this.onPreSerializeForm, context: this});
  },

  onPreSerializeForm: function() {
    // This function will be called just before the form is serialized 
    // before pushing back a request to the server so here we set the 
    // value of our hidden value field(s)
    var retVal = this.options.label.getContent();
    while(true) {
      if( retVal.indexOf(' xmlns="http://www.w3.org/1999/xhtml"') == -1 )
        break;
      retVal = retVal.replace(' xmlns="http://www.w3.org/1999/xhtml"', '');
    }
    while(true) {
      if( retVal.indexOf(' _moz_dirty=""') == -1 )
        break;
      retVal = retVal.replace(' _moz_dirty=""', '');
    }
    this.getValueElement().value = retVal;
  },

  getValue: function() {
    return this.options.ctrl.getContent();
  },

  destroyThis: function() {

    // Removing the Form Pre-Serialization handler
    var idxOfRemove = 0;
    for( var idx = 0; idx < Ra.Form.preSerializers.length; idx++) {
      if( Ra.Form.preSerializers[idx].handler == this.onPreSerializeForm )
        break;
      idxOfRemove += 1;
    }
    Ra.Form.preSerializers.splice(idxOfRemove, 1);

    // Calling base
    this._destroyThisControl();
  }
});
