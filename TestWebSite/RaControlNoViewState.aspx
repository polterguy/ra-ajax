<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="RaControlNoViewState.aspx.cs" 
    EnableViewState="false"
    Inherits="RaControlNoViewState" %>

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




function checkStylesAfterServerChange() {
  var el = Ra.Control.$('verifyNoStylesChanged');
  if( el.element.style.width.toLowerCase() == '150px' ) {
    Ra.$('results').innerHTML = 'success';
  }
}









</script>


    </head>
    <body>
        <div id="results">
            Unknown
        </div>
        <form id="form1" runat="server">
            <div>
                <ra:Button runat="server" ID="testChangeStyle" Text="Changes style" OnClick="testChangeStyle_Click" />
                <ra:Button runat="server" ID="verifyNoStylesChanged" Text="Changes style" OnClick="verifyNoStylesChanged_Click" />
            </div>
        </form>
    </body>
</html>
