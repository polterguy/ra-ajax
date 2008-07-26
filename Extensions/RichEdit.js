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

    // We must have a preSerializer handler to make sure we serialize 
    // the value back to the server
    Ra.Form.preSerializers.push({handler: this.onPreSerializeForm, context: this});

    // We must handle focus and blur to STORE the old range of the editable element
    this.options.ctrl.observe('focus', this.onFocus, this);
    this.options.ctrl.observe('blur', this.onBlur, this);
  },

  onFocus: function() {
    this._restoreSelection(this._selection);
    this._selection = null;
  },

  onBlur: function() {
    this._selection = this._getSelection();
  },

  _getSelection: function() {
    if (window.getSelection) {
      var selection = window.getSelection();
      if (selection.rangeCount > 0) {
        var selectedRange = selection.getRangeAt(0);
        return selectedRange.cloneRange();
      } else {
        return null;
      }
    } else if (document.selection) {
      var selection = document.selection;
      if (selection.type.toLowerCase() == 'text') {
        return selection.createRange().getBookmark();
      } else {
        return null;
      }
    } else {
      return null;
    }
  },

  _restoreSelection: function(storedSelection) {
    if (storedSelection) {
      if (window.getSelection) {
        var selection = window.getSelection();
        selection.removeAllRanges();
        selection.addRange(storedSelection);
      } else if (document.selection && document.body.createTextRange) {
        var range = document.body.createTextRange();
        range.moveToBookmark(storedSelection);
        range.select();
      }
    }
  },

  onPreSerializeForm: function() {
    // This function will be called just before the form is serialized 
    // before pushing back a request to the server so here we set the 
    // value of our hidden value field(s)
    var retVal = this.options.label.getContent();

    // In application/xhtml+xml mode browser adds up "garbage attributes" to the innerHTML
    // which ends up poluting the end XHTML. This garbage should be REMOVED before transfering
    // the value of the control back to the server...
    var toRemove = [' xmlns="http://www.w3.org/1999/xhtml"', ' _moz_dirty=""', ' type="_moz"'];
    for( var idx = 0; idx < toRemove.length; idx++ ) {
      while(true) {
        if( retVal.indexOf(toRemove[idx]) == -1 )
          break;
        retVal = retVal.replace(toRemove[idx], '');
      }
    }

    // Setting the hidden field value to get it back to the server
    this.getValueElement().value = retVal;

    var selection = this._getSelection();
    if( selection )
      this.getSelectedElement().value = selection.toString();
  },

  getValue: function() {
    return this.options.ctrl.getContent();
  },

  Paste: function(value) {
    var sel = this._getSelection();
    if( sel ) {
      this._restoreSelection(sel);
      var newContent = sel.createContextualFragment(value);
      sel.deleteContents();
      sel.insertNode(newContent);
      this._selection = null;
    }
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
