
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
  }
});
