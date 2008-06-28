
// ==============================================================================
// Ra.Control class
// Wraps a server-side Ra Ajax Control
// ==============================================================================
Ra.Control = Ra.klass();


// Static array which contains all the client-side registered controls
Ra.Control._controls = new Array();


Ra.extend(Ra.Control.prototype, {
  init: function(element, options) {

    // Wrapping DOM element
    this.element = Ra.$(element);

    // Setting default options
    this.options = Ra.extend({
      // Defaults here...
      serverEvents: {}
    }, options || {});

    // Registering control
    Ra.Control._controls.add(this);

    // Creating event handlers for the client-side events needed to be dispatched 
    // back to server
    this.initEvents();
  },

  initEvents: function() {
    var evts = this.options.serverEvents;
    for( var idx = 0; idx < evts.length; idx++ ) {
      this.element.observe(evts[idx][0], this.onEvent, this, [evts[idx]]);
    }
  },

  onEvent: function(evt) {
    
  },

  destroy: function() {

    // Since some controls may be children of the "this" widget we must
    // collect all those widgets too and call destroy on those too
    var childrenAndSelf = new Array();

    // First we must find all the objects which are CHILD objects
    // to the current one (being destroyed)
    // Then we must destroy all those objects (including self)
    for( var idx = 0; idx < Ra.Control._controls.length; idx++ ) {
      if( this.element.id.indexOf(Ra.Control._controls[idx].element.id) == 0 ) {
        childrenAnSelf.add(Ra.Control._controls[idx]);
      }
    }

    // Sorting all elements in REVERSE order
    // This is to make sure we destroy the objects in depth first order
    childrenAndSelf.reverse(function(a, b) {
      if( a.element.id < b.element.id )
        return -1;
      if( a.element.id > b.element.id )
        return 1;
      return 0;
    });

    // Now looping through and destroying all objects
    for( var idx = 0; idx < childrenAndSelf.length; idx++ ) {
      childrenAndSelf[idx]._destroyImpl();
    }
  },

  _destroyImpl: function() {
    // Forward call to allow overriding in inherited classes...
    this.destroyControlImpl();
  },

  _destroyControlImpl: function() {

    // Unlistening all event observers to avoid leaking memory
    for( var idx = 0; idx < evts.length; idx++ ) {
      this.element.stopObserving(evts[idx][0], this.onEvent);
    }

    // Looping through regsitered controls to REMOVE this instance
    var idxToRemove = 0;
    for( var idx = 0; idx < Ra.Control._controls.length; idx++ ) {
      if( Ra.Control._controls[idx].element.id == this.element.id )
        break;
    }
    Ra.Control._controls.splice(idxToRemove, 1);
  }
});


























