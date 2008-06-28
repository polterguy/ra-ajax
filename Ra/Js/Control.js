
// ==============================================================================
// Ra.Control class
// Wraps a server-side Ra Ajax Control
// Every Control in Ra have its client-side object
// attached to it to help facilitate with event listening,
// server dispatching and so on.
// This is the "bas class" for those Client-Side objects.
// Also many server-side controls will use this class directly
// since there will be little use for creating specific wrappers
// for all controls since this class will be sufficiant enough
// for wrapping the server-side control.
// ==============================================================================
Ra.Control = Ra.klass();


// Static array which contains all the client-side registered controls
Ra.Control._controls = new Array();


Ra.extend(Ra.Control.prototype, {

  // CTOR
  init: function(element, options) {

    // Wrapping DOM element
    this.element = Ra.$(element);

    // Setting default options
    this.options = Ra.extend({
      // Defaults here...
      serverEvents: []
    }, options || {});

    // Registering control
    Ra.Control._controls.add(this);

    // Creating event handlers for the client-side events needed to be dispatched 
    // back to server
    this.initEvents();
  },

  // Initializes all events on control
  initEvents: function() {
    var evts = this.options.serverEvents;
    for( var idx = 0; idx < evts.length; idx++ ) {
      this.element.observe(evts[idx], this.onEvent, this, [evts[idx]]);
    }
  },

  // Called when an event is raised, tha parameter passed is the this.options.serverEvent instance 
  // which we will use to know how to call our server
  onEvent: function(evt) {
    new Ra.Ajax({
      args:'__RA_CONTROL=' + this.element.id + '&__EVENT_NAME=' + evt,
      onSuccess: this.onFinishedRequest,
      callingContext: this
    });
  },

  onFinishedRequest: function(response){
    alert(response);
  },

  // Called when control is destroyed
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

    // Replacing the control's HTML with a "wrapper span" so we can re-create it later
    // in its exactly correct position...
    // Note that since all other "child controls" are children DOM elements of the "this DOM element"
    // there is no need to do this for the child controls since their HTML will disappear
    // anyway.
    this.element.replace('<span id="' + this.element.id + '" style="display:none;" />');
  },

  // Destruction implementation
  // If you override this (which you often will end up doing) in your own derived classes
  // then you should make sure you call the _destroyControlImpl method to make
  // sure you don't leak memory and gets the "basic" functionality from destroy...
  _destroyImpl: function() {

    // Forward call to allow overriding in inherited classes...
    this.destroyControlImpl();
  },

  // Implementation of destroy
  // Basically unlisetens all events and removes object out 
  // of registered controls collection
  _destroyControlImpl: function() {

    // Unlistening all event observers to avoid leaking memory
    var evts = this.options.serverEvents;
    for( var idx = 0; idx < evts.length; idx++ ) {
      this.element.stopObserving(evts[idx][0], this.onEvent);
    }

    // Looping through registered controls to remove the "this instance"
    var idxToRemove = 0;
    for( var idx = 0; idx < Ra.Control._controls.length; idx++ ) {
      if( Ra.Control._controls[idx].element.id == this.element.id )
        // We have found our instance, idxToRemove now should contain the index of the control
        break;
    }

    // Removes control out from registered controls collection
    Ra.Control._controls.splice(idxToRemove, 1);
  }
});


























