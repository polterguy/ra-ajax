<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-WebMethod.aspx.cs" 
    Inherits="Samples.AjaxWebMethod" 
    Title="Ra-Ajax Calling WebMethods" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">
    
    <script type="text/javascript">

// This is "user code" where we're calling into the server a method called "foo"
function callWebMethod() {

  // "Validating"...
  if( !parseInt(Ra.$('ageTextBox').value, 10) ) {
    Ra.$('resultSpan').innerHTML = 'NUMBERS please...!';
    return;
  }

  // Here we're calling a method on the server, the first parameter
  // is the method name. The two next options are the success and failure
  // callback functions. And the third array is the "list of arguments"
  // being passed to that method...
  Ra.Control.callServerMethod('foo', {
    onSuccess: function(retVal) {
      Ra.$('resultSpan').innerHTML = retVal;
    },
    onError: function(status, fullTrace) {
      alert(fullTrace);
    }
  }, [Ra.$('nameTextBox').value, parseInt(Ra.$('ageTextBox').value, 10)]);
  
}
    </script>

    <h1>Ra-Ajax - WebMethod sample</h1>
    <p>
        Sometimes you need to mix in "conventional JavaScript" but still keep as much as possible of the simplicity
        from the Ra-Ajax core. For such circumstances you will often want to call methods on the server yet
        still have the possibility of changing controls and such in those methods. In such circumstances the
        Ra-Ajax WebMethod is handy.
    </p>
    <p><span style="width:100px;display:block;float:left;">Your Name:</span><input type="text" id="nameTextBox" /></p>
    <p><span style="width:100px;display:block;float:left;">Your Age:</span><input type="text" id="ageTextBox" /></p>
    <button id="btn" onclick="callWebMethod();return false;">Invoke server-side method</button>
    <ra:Panel   
        runat="server" 
        ID="pnl" 
        style="width:200px;height:70px;border:solid 1px black;background-color:#eee;padding:5px;">
        <span id="resultSpan"></span>
    </ra:Panel>
    <p>
        To do the above all you need to do is create a method in your codebehind file for your .ASPX page, mark it
        with the attribute <em>WebMethod</em> and call it with the JavaScript counterpart parameters. Everything else
        comes for free. And in these WebMethods you still have access to the entire Ra-Ajax "life cycle" which means 
        you can create any Ajax Controls and manipulate any value of any existing Ajax Control on that page. And
        everything just works...
    </p>
    <p>
        Make sure you view the HTML source for this page to understand how this is accomplished...
    </p>
    
</asp:Content>

