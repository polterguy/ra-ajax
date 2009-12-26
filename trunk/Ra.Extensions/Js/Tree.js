/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */


(function(){

// Creating class
Ra.TreeNode = Ra.klass();


// Inheriting from Ra.Control
Ra.extend(Ra.TreeNode.prototype, Ra.Control.prototype);


Ra.TreeNode._list = [];


// Creating IMPLEMENTATION of class
Ra.extend(Ra.TreeNode.prototype, {
  init: function(el, opt) {
    this.initControl(el, opt);
    this.options = Ra.extend({
      hasChildren:false,
      childCtrl:null
    }, this.options || {});
    this.element.observe('mouseover', this.over, this);
    this.element.observe('mouseout', this.out, this);
  },

  over: function() {
    var found = false;
    var idx = Ra.TreeNode._list.length;
    while(idx--) {
      if( Ra.TreeNode._list[idx].id.indexOf(this.element.id) != -1 ) {
        found = true;
      } else {
        Ra.TreeNode._list[idx].removeClassName('tree-active');
      }
    }
    Ra.TreeNode._list.push(this.element);
    if( !found ) {
      this.element.addClassName('tree-active');
    }
    Ra.TreeNode._list.sort(function(a,b) {
      if( a.id < b.id ) {
        return -1;
      } else if( a.id > b.id ) {
        return 1;
      }
      return 0;
    });
  },

  out: function() {
    this.element.removeClassName('tree-active');
    var idx = Ra.TreeNode._list.length;
    while(idx--) {
      if( Ra.TreeNode._list[idx].id == this.element.id) {
        Ra.TreeNode._list.splice(idx, 1);
        break;
      }
    }
    if( Ra.TreeNode._list.length > 0 ) {
      Ra.TreeNode._list[Ra.TreeNode._list.length - 1].addClassName('tree-active');
    }
  },

  destroyThis: function() {

    this.element.stopObserving('mouseover', this.over, this);
    this.element.stopObserving('mouseout', this.out, this);

    // Forward call to allow overriding in inherited classes...
    this._destroyThisControl();
  },

  expand: function() {
    var T = this;
    if(!this.options.hasChildren) {

      // Need to fetch children from server...
      this.options.hasChildren = true;

      // We're basically just "faking" a click on the expander control here...
      this.callback('clientClick');

    } else {

      // Children already fetched from server...
      // Running effect to show/hide...
      if( this.isRunningEffect )
        return;
      this.isRunningEffect = true;
      if( Ra.$(this.options.childCtrl).style.display != 'none') {
        this.element.removeClassName('expanded');
        if( this.element.className.indexOf('collapsed') == -1 )
          this.element.addClassName('collapsed');
        Ra.E(this.options.childCtrl, {
          onStart: function() {
            this._fromHeight = this.element.getDimensions().height;
            this._overflow = this.element.getStyle('overflow');
            this.element.setStyle('overflow','hidden');
            this.element.setOpacity(1);
          },
          onFinished: function() {
            this.element.setStyles({display: 'none', height: '', overflow: this._overflow});
            T.isRunningEffect = false;
            this.element.setOpacity(0);
          },
          onRender: function(pos) {
            this.element.setStyle('height',((1.0-pos)*this._fromHeight) + 'px');
            this.element.setOpacity(1.0-pos);
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
            this.element.setStyles({height: '0px', display: 'block'});
            this._overflow = this.element.getStyle('overflow');
            this.element.setStyle('overflow','hidden');
            this.element.setOpacity(0);
          },
          onFinished: function() {
            this.element.setStyles({height: '', overflow: this._overflow});
            this.element.setOpacity(1);
            T.isRunningEffect = false;
          },
          onRender: function(pos) {
            this.element.setStyle('height', parseInt(this._toHeight*pos) + 'px');
            this.element.setOpacity(pos);
          },
          duration:200,
          transition:'Explosive'
        });
      }
    }
  }
});})();
