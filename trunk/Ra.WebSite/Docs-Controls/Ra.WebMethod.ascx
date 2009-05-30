<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.WebMethod.ascx.cs" 
    Inherits="Docs_Controls_WebMethod" %>

<script type="text/javascript">

function foo() {
  Ra.Control.callServerMethod('DocsUserControl.foo', {
    onSuccess: function(retVal) {
      alert(retVal);
    },
    onError: function(status, fullTrace) {
      alert(fullTrace);
    }
  }, [Ra.$('txt').value]);
}

</script>

<input 
    type="text" 
    id="txt" 
    value="Your name please" />

<input 
    type="button" 
    value="Go server side..." 
    onclick="foo();" />