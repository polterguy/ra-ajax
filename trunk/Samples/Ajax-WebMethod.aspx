<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-WebMethod.aspx.cs" 
    Inherits="Samples.AjaxWebMethod" 
    Title="Ra-Ajax WebMethod Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">
    
    <script type="text/javascript">
        function callWebMethod() {
        
          Ra.Control.callServerMethod('foo', {
            onSuccess:function(retVal) {
              Ra.$('resultSpan').innerHTML = retVal;
            },
            
            onError: function(status, fullTrace) {
              Ra.$('resultSpan').innerHTML = status + '<br />' +  fullTrace;
            }
          }, [Ra.$('nameTextBox').value, parseInt(Ra.$('ageTextBox').value, 10)]);
          
        }
    </script>

    <h1>Ra-Ajax Samples - Calling WebMethod</h1>
    <p>
        Sometimes you need to mix in "good old JavaScript" but still keep as much as possible of the simplicity
        from the Ra-Ajax core. For such circumstances you will often want to call methods on the server yet
        still have the possibility of changing controls and such in those methods. In such circumstances the
        Ra-Ajax WebMethod is handy.
    </p>
    <p>Your Name: <input type="text" id="nameTextBox" /></p>
    <p>Your Age: <input type="text" id="ageTextBox" /></p>
    <button id="btn" onclick="callWebMethod();return false;">Invoke server-side method</button>
    <ra:Panel runat="server" ID="pnl" style="width:200px;height:70px;border:solid 1px black;background-color:LightGray;">
        <span id="resultSpan"></span>
    </ra:Panel>
    
</asp:Content>

