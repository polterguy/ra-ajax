<%@ Page 
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
    init: function(){},
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


// Tests setContent
function checkSetContent() {
  Ra.$('results').setContent('success');
}



function testFadeAndAppear() {
  Ra.E('testAnimationDiv', {
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




function testXHRBasics() {
  new Ra.XHR('RaDOMBasics.aspx', {
    onSuccess: function(response) {
      Ra.$('results').setContent('success');
    }
  });
}




function testXHRParameters() {
  new Ra.XHR('RaDOMBasics.aspx', {
    body: 'testingXHR=true',
    onSuccess: function(response) {
      if( response == 'works' )
        Ra.$('results').setContent('success');
    }
  });
}




function testCallback() {
  var form = new Ra.Form(null, {
    args:'testingForm=testing',
    onFinished: function(response){
      if( this == 5 ) {
        if( response == 'this worked' )
          Ra.$('results').setContent('success');
      }
    },
    callingContext: 5
  });
  form.callback();
}



function testCallback2() {
  var form = new Ra.Form(null, {
    args:'testingForm=testing',
    onFinished: function(response){
      // In case of NULL calling context, we will get in here with options as "this" pointer
      if( this.args == 'testingForm=testing' ) {
        if( response == 'this worked' )
          Ra.$('results').setContent('success');
      }
    }
  });
  form.callback();
}




function testCallbackError() {
  var form = new Ra.Form(null, {
    args:'testingForm=testingError',
    onFinished: function(response){
      Ra.$('results').setContent('failure');
    },
    onError: function(status, response){
      if( status == 500 && response.indexOf('TESTING ERROR HANDLER IN FORM / XHR') != -1)
        Ra.$('results').setContent('success');
    }
  });
  form.callback();
}





function testCallbackFormValues() {
  var form = new Ra.Form(null, {
    args:'testingForm=testingParams1',
    onFinished: function(response) {
      // In case of NULL calling context, we will get in here with options as "this" pointer
      if( response == 'this worked' ) {
        Ra.$('results').setContent('success');
      }
    }
  });
  form.callback();
}




function testCallbackFormValuesWeird() {
  var form = new Ra.Form(null, {
    args:'testingForm=testingParams2',
    onFinished: function(response) {
      // In case of NULL calling context, we will get in here with options as "this" pointer
      if( response == 'this worked' ) {
        Ra.$('results').setContent('success');
      }
    }
  });
  form.callback();
}




function testCallbackFormValuesMultiple() {
  var form = new Ra.Form(null, {
    args:'testingForm=testingParamsMultiple',
    onFinished: function(response) {
      // In case of NULL calling context, we will get in here with options as "this" pointer
      if( response == 'this worked' ) {
        Ra.$('results').setContent('success');
      }
    }
  });
  form.callback();
}



function testMultipleRequests() {
  Ra.$('results').innerHTML = '';
  new Ra.Ajax({
    args: 'id=0',
    onSuccess: function(r) {
      Ra.$('results').innerHTML += r;
    }
  });
  new Ra.Ajax({
    args: 'id=1',
    onSuccess: function(r) {
      Ra.$('results').innerHTML += r;
    }
  });
  new Ra.Ajax({
    args: 'id=2',
    onSuccess: function(r) {
      Ra.$('results').innerHTML += r;
    }
  });
  new Ra.Ajax({
    args: 'id=3',
    onSuccess: function(r) {
      Ra.$('results').innerHTML += r;
    }
  });
  new Ra.Ajax({
    args: 'id=4',
    onSuccess: function(r) {
      Ra.$('results').innerHTML += r;
    }
  });
  new Ra.Ajax({
    args: 'id=5',
    onSuccess: function(r) {
      Ra.$('results').innerHTML += r;
    }
  });
  new Ra.Ajax({
    args: 'id=6',
    onSuccess: function(r) {
      Ra.$('results').innerHTML += r;
    }
  });
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
                <input type="text" name="testingInput" value="testing input for form" />
                <input type="text" name="testingInput2" value="testing input for form with $€//@#£&&&__{[]} funny" />

                <input type="checkbox" name="testCheckBox" checked="checked" />
                <input type="checkbox" name="testCheckBox2" />
                <input type="radio" name="testRadio" checked="checked" />
                <input type="radio" name="testRadio2" />
                <input type="button" name="testButton" value="Testing button" />
                <input type="hidden" name="testHidden" value="testing value &&& $$ ££__//" />
                <input type="password" name="testPwd" value="testing password" />
                <input type="text" disabled="disabled" name="testShouldnt" value="Shouldnt pass" />
                <select name="testSelect">
                    <option value="sel1" />
                    <option value="sel2" selected="selected" />
                </select>

                <!-- To inject Ra core scripts onto form -->
                <ra:Button ID="Button1" runat="server" />


                <input type="button" id="textButton" value="Test" onclick="checkRaElementReplaceWorks();" />
                <input type="button" id="evtTestBtn" value="Event Test Button" />
                <input type="button" id="evtTestBtn2" value="Event Test Button - removed observer" />
                <input type="button" id="evtTestBtnPre" value="Set to success" />
                <input type="button" id="testXHR" value="Test XHR" onclick="testXHRBasics();" />
                <input type="button" id="testXHRParams" value="Test XHR params" onclick="testXHRParameters();" />
                <input type="button" id="testFormCallback" value="Test Form Callback" onclick="testCallback();" />
                <input type="button" id="testFormCallback2" value="Test Form Callback" onclick="testCallback2();" />
                <input type="button" id="testFormCallbackError" value="Test Form Callback - WITH ERROR" onclick="testCallbackError();" />
                <input type="button" id="testFormCallbackWithTextInputField" value="Test Form Callback - WITH FORM VALUES" onclick="testCallbackFormValues();" />
                <input type="button" id="testFormCallbackWithWeirdTextInputField" value="Test Form Callback - WITH WEIRD FORM VALUES" onclick="testCallbackFormValuesWeird();" />
                <input type="button" id="testFormCallbackMultiple" value="Test Form Callback - Multiple Values" onclick="testCallbackFormValuesMultiple();" />
                <input type="button" id="testMultipleRequestsBtn" value="Test Ra.Ajax - Multiple requests" onclick="testMultipleRequests();" />
            </div>
        </form>
    </body>
</html>
