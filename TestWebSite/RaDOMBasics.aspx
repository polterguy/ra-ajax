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


function init() {
  document.getElementById('results').innerHTML = "Unknown";
}


function checkForRa() {
  if( Ra ) {
    document.getElementById('results').innerHTML = "success";
  } else {
    document.getElementById('results').innerHTML = "failure";
  }
}


function checkForRaDollar() {
  Ra.$('results').innerHTML = 'success';
}


function checkCreateClass() {
  var XX = Ra.klass();
  if( XX )
    Ra.$('results').innerHTML = 'success';
  else
    Ra.$('results').innerHTML = 'failure';
}


function checkExtend() {
  if( Ra.extend )
    Ra.$('results').innerHTML = 'success';
  else
    Ra.$('results').innerHTML = 'failure';
}


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


function checkExtendMethodPrototypeWithThisArgument() {
  var X = Ra.klass();
  Ra.extend(X.prototype, {
    init: function(arg1){
      this.foo = arg1;
    },
    bar: function(){
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


        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div id="results">
                    Unknown
                </div>
                <ra:Button ID="Button1" runat="server" />
            </div>
        </form>
    </body>
</html>
