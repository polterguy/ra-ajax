<%@ Page Language="C#" 
    AutoEventWireup="true" 
    CodeFile="RaControlsComplexSingle.aspx.cs" 
    Inherits="RaControlsComplexSingle" %>

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





function verifyCheckBoxInVisible() {
  var ctrl = Ra.Control.$('chkSetInVisible');
  if( !ctrl ) {
    var el = Ra.$('chkSetInVisible');
    if( el && el.style.display == 'none') {
      Ra.$('results').setContent('success');
    }
  }
}






function verifyCheckBoxVisible() {
  var ctrl = Ra.Control.$('chkSetVisible');
  if( ctrl ) {
    var el = Ra.$('chkSetVisible');
    if( el && el.style.display != 'none') {
      if( Ra.$('chkSetVisible_LBL').innerHTML == 'Set this to visible')
        Ra.$('results').setContent('success');
    }
  }
}






        </script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div id="results">
                Unknown
            </div>
            <br />
            <div>
                <ra:CheckBox runat="server" ID="chk" Text="Text of checkbox" OnCheckedChanged="chk_CheckedChanged" />
                <ra:CheckBox runat="server" ID="chkSetInVisible" Text="Set this to invisible" />
                <ra:Button runat="server" ID="setChkToInvisible" Text="Sets checkbox to invisible" OnClick="setChkToInvisible_Click" />
                <br />

                <ra:CheckBox Visible="false" runat="server" ID="chkSetVisible" Text="Set this to visible" OnCheckedChanged="chkSetVisible_CheckedChanged" />
                <ra:Button runat="server" ID="btnSetChkVisible" Text="Sets checkbox to visible" OnClick="btnSetChkVisible_Click" />
            </div>
        </form>
    </body>
</html>
