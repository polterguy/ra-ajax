﻿<%@ Page 
    Language="C#" 
    AutoEventWireup="true"
    CodeFile="RaDOMBasics.aspx.cs" 
    Inherits="RaDOMBasics" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Untitled Page</title>
        <script type="text/javascript">


// Function to "reset" the div used to track results
function init() {
  document.getElementById('results').innerHTML = "Unknown";
}


// Checks to see if JavaScript namespace Ra does exist
function checkForRa() {
  if( Ra ) {
    document.getElementById('results').innerHTML = "success";
  } else {
    document.getElementById('results').innerHTML = "failure";
  }
}


// Checks to see if Ra.$ function exists
function checkForRaDollar() {
  Ra.$('results').innerHTML = 'success';
}


// Checks to see if Ra.klass function exists
function checkCreateClass() {
  var XX = Ra.klass();
  if( XX )
    Ra.$('results').innerHTML = 'success';
  else
    Ra.$('results').innerHTML = 'failure';
}


// Checks to see if Ra.extend function exists
function checkExtend() {
  if( Ra.extend )
    Ra.$('results').innerHTML = 'success';
  else
    Ra.$('results').innerHTML = 'failure';
}


// Checks to see if basic use of Ra.extend works (prototype copying of properties)
function checkExtendFunctionalSimple() {
  var x = {};
  Ra.extend(x, {
    foo: true
  });
  if( x.foo )
    Ra.$('results').innerHTML = 'success';
  else
    Ra.$('results').innerHTML = 'failure';
}


// Checks to see if basic use of Ra.extend works (prototype copying of FUNCTION)
function checkExtendFunctionalSimpleMethod() {
  var x = {};
  Ra.extend(x, {
    foo: function(){}
  });
  if( x.foo )
    Ra.$('results').innerHTML = 'success';
  else
    Ra.$('results').innerHTML = 'failure';
}


// Checks to see if basic use of Ra.extend works (prototype copying of properties and then CALLING function)
function checkExtendFunctionalMethodInvoke() {
  var x = {};
  Ra.extend(x, {
    foo: function(){
      Ra.$('results').innerHTML = 'success';
    }
  });
  x.foo();
}


function checkExtendFunctionalMethodPrototype() {
  var X = Ra.klass();
  Ra.extend(X.prototype, {
    foo: 12,
    init: function(){
    }
  });
  var x = new X();
  if( x.foo == 12 ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
}


// Checks to see if arguments are passed into init function from CTOR calling
function checkExtendFunctionalMethodPrototypeWithInitArguments() {
  var X = Ra.klass();
  Ra.extend(X.prototype, {
    init: function(args){
      this.foo = args;
    }
  });
  var x = new X(12);
  if( x.foo == 12 ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
}


// Checks to see if MULTIPLE arguments are passed into init function from CTOR calling
function checkExtendFunctionalMethodPrototypeWithMultipleInitArguments() {
  var X = Ra.klass();
  Ra.extend(X.prototype, {
    init: function(arg1, arg2){
      this.foo = arg1;
      this.bar = arg2;
    }
  });
  var x = new X(5, 'test');
  if( x.foo == 5 && x.bar == 'test' ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
}


// Checks to see if this works within an extended object
function checkExtendMethodPrototypeWithThisArgument() {
  var X = Ra.klass();
  Ra.extend(X.prototype, {
    init: function(arg1) {
      this.foo = arg1;
    },

    bar: function() {
      if( this.foo == 7 ) {
        Ra.$('results').innerHTML = 'success';
      } else {
        Ra.$('results').innerHTML = 'failure';
      }
    }
  });
  var x = new X(7);
  x.bar();
}


// Checks to see if function are overridden with extend
function checkExtendInheritanceOverride() {
  var Class1 = Ra.klass();
  Ra.extend(Class1.prototype, {
    foo: function() {
      this.x1 = 5;
    },
    bar: function() {
      this.x2 = 7;
    }
  });
  
  var Class2 = Ra.klass();
  Ra.extend(Class2.prototype, Ra.extend(Class1.prototype, {
    bar: function() {
      this.x2 = 9;
    }
  }));
  
  var x = new Class2();
  x.foo();
  x.bar();
  if( x.x1 == 5 && x.x2 == 9 ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
}


// Checks to see if a DOM element fetched with Ra.$ contains the Ra specific
// extensions
function checkRaElementExtendWorks() {
  var el = Ra.$('results');
  if( el.replace ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
}


// Checks to see if Ra.Element.replace works
function checkRaElementReplaceWorks() {
  var el = Ra.$('results');
  el.replace('<div id="results">success</div>');
}


// Checks to see setVisible works
function checkRaElementReplaceWorks() {
  var el = Ra.$('results');
  el.setVisible(false);
  if( el.style.display == 'none' ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
  el.setVisible(true);
}


// Checks to see if remove works
function checkRemove() {
  var el = Ra.$('test');
  el.remove();
  var el2 = Ra.$('test');
  if( el2 ) {
    Ra.$('results').innerHTML = 'failure';
  } else {
    Ra.$('results').innerHTML = 'success';
  }
}


// Testing getWidth function
function checkWidth() {
  var el = Ra.$('test2');
  if( el.getWidth() == 100 ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
}


// Testing getWidth function
function checkHeight() {
  var el = Ra.$('test2');
  if( el.getHeight() == 150 ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
}


// Testing setHeight function
function checkSetHeight() {
  var el = Ra.$('test2');
  el.setHeight(80);
  if( el.getHeight() == 80 ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
}


// Testing setWidth function
function checkSetWidth() {
  var el = Ra.$('test2');
  el.setHeight(110);
  if( el.getHeight() == 110 ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
}


// Test adding class name
function checkAddClassName() {
  var el = Ra.$('test2');
  el.addClassName('test');
  if( el.className == 'test' ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
  el.className = '';
}


// Test REMOVE class name
function checkRemoveClassName() {
  var el = Ra.$('test2');
  el.addClassName('mambo');
  el.addClassName('jambo');
  el.removeClassName('mambo');
  if( el.className == 'jambo' ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
  el.className = '';
}


function checkOpacity() {
  var el = Ra.$('test2');
  el.setOpacity(0.8);
  if( el.getOpacity() == 0.8 ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
  el.setOpacity(1);
}


function testPosition() {
  var el = Ra.$('test2');
  el.style.position = 'absolute';
  el.setLeft(100).setTop(200);
  if( el.getLeft() == 100 && el.getTop() == 200 ) {
    Ra.$('results').innerHTML = 'success';
  } else {
    Ra.$('results').innerHTML = 'failure';
  }
  el.style.left = '';
  el.style.top = '';
  el.style.position = '';
}



// Tests setContent
function checkSetContent() {
  Ra.$('results').setContent('success');
}



function testFadeAndAppear() {
  new Ra.Effect('testAnimationDiv', {
    onRender: function(pos) {
      this.element.setOpacity(1.0 - pos);
    },
    onStart: function(){
      this.element.setContent('testing');
    },
    onFinished: function(){
      if( this.element.getOpacity() == 0 ) {
        new Ra.Effect('testAnimationDiv', {
          onRender: function(pos) {
            this.element.setOpacity(pos);
          },
          onFinished: function(){
            if( this.element.getOpacity() == 1 && this.element.innerHTML == 'testing')
              Ra.$('results').setContent('success');
          }
        });
      }
    }
  });
}


function onLoadMethod() {
  var el = Ra.$('evtTestBtn');
  el.observe('click', function(){
    if( this == 5 )
      Ra.$('results').setContent('success');
  }, 5);

  el = Ra.$('evtTestBtnPre');
  el.observe('click', function(){
    Ra.$('results').setContent('success');
  });

  el = Ra.$('evtTestBtn2');
  var func = function() {
    Ra.$('results').setContent('failure');
  };
  el.observe('click', func, null);

  el = Ra.$('evtTestBtn2');
  el.stopObserving('click', func);
}




        </script>
    </head>
    <body onload="onLoadMethod();">
        <form id="form1" runat="server">
            <div>
                <div id="results">
                    Unknown
                </div>
                <div id="test">
                    dummy
                </div>
                <div id="test2" style="width:100px;height:150px;">
                    &nbsp;
                </div>
                <div id="testAnimationDiv" style="width:100px;height:150px;background-color:Red;">
                    Howdy
                </div>
                <ra:Button ID="Button1" runat="server" />
                <input type="button" id="textButton" value="Test" onclick="testFadeAndAppear();" />
                <input type="button" id="evtTestBtn" value="Event Test Button" />
                <input type="button" id="evtTestBtn2" value="Event Test Button - removed observer" />
                <input type="button" id="evtTestBtnPre" value="Set to success" />
            </div>
        </form>
    </body>
</html>
