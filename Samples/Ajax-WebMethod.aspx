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
    onSuccess:function(val){
      alert(val);
    }
  }, [1,'ra-ajax rules']);
}
    </script>

    <h1>Ra-Ajax Samples - Calling WebMethod</h1>
    <p>
        Sometimes you need to mix in "good old JavaScript" but still keep as much as possible of the simplicity
        from the Ra-Ajax core. For such circumstances you will often want to call methods on the server yet
        still have the possibility of changing controls and such in those methods. In such circumstances the
        Ra-Ajax WebMethod is handy.
    </p>
    <button id="btn" onclick="callWebMethod();return false;">Invokes server-side method</button>
    <p>
        Try to click the above button to invoke a WebMethod...
    </p>
    
</asp:Content>

