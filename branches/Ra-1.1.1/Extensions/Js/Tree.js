/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */


(function(){

// Creating class
Ra.TreeNode = Ra.klass();


// Inheriting from Ra.Control
Ra.extend(Ra.TreeNode.prototype, Ra.Control.prototype);


// Creating IMPLEMENTATION of class
Ra.extend(Ra.TreeNode.prototype, {
  init: function(el, opt) {
    this.initControl(el, opt);
    this.options = Ra.extend({
      hasChildren:false,
      childCtrl:null
    }, this.options || {});
  },

  expand: function() {
    if(!this.options.hasChildren) {

      // Need to fetch children from server...
      this.options.hasChildren = true;

      // We're basically just "faking" a click on the expander control here...
      this.onEvent('clientClick');

    } else {

      // Children already fetched from server...
      // Running effect to show/hide...
      if( Ra.$(this.options.childCtrl).style.display != 'none') {
        this.element.removeClassName('expanded');
        if( this.element.className.indexOf('collapsed') == -1 )
          this.element.addClassName('collapsed');
        Ra.E(this.options.childCtrl, {
          onStart: function() {
            this._fromHeight = this.element.getDimensions().height;
            this._overflow = this.element.getStyle('overflow');
            this.element.setStyle('overflow','hidden');
          },
          onFinished: function() {
            this.element.setStyle('display','none');
            this.element.setStyle('height','');
            this.element.setStyle('overflow',this._overflow);
          },
          onRender: function(pos) {
            this.element.setStyle('height',((1.0-pos)*this._fromHeight) + 'px');
          },
          duration:200,
          transition:'Explosive'
        });
      } else {
        if( this.element.className.indexOf('expanded') == -1 )
          this.element.addClassName('expanded');
        this.element.removeClassName('collapsed');
        Ra.E(this.options.childCtrl, {
          onStart: function() {
            this._toHeight = this.element.getDimensions().height;
            this.element.setStyle('height','0px');
            this.element.setStyle('display','block');
            this._overflow = this.element.getStyle('overflow');
            this.element.setStyle('overflow','hidden');
          },
          onFinished: function() {
            this.element.setStyle('height', '');
            this.element.setStyle('overflow',this._overflow);
          },
          onRender: function(pos) {
            this.element.setStyle('height', parseInt(this._toHeight*pos) + 'px');
          },
          duration:200,
          transition:'Explosive'
        });
      }
    }
  }
});})();
