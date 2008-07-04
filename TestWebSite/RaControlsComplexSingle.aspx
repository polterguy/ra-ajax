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






function verifyToggleCheckBoxInVisible() {
  var ctrl = Ra.Control.$('chkToggle');
  if( !ctrl ) {
    var el = Ra.$('chkToggle');
    if( el && el.style.display == 'none') {
      Ra.$('results').setContent('success');
    }
  }
}






function verifyToggleCheckBoxVisible() {
  var ctrl = Ra.Control.$('chkToggle');
  if( ctrl ) {
    var el = Ra.$('chkToggle');
    if( el && el.style.display != 'none') {
      if( Ra.$('chkToggle_LBL').innerHTML == 'Toggles with button')
        Ra.$('results').setContent('success');
    }
  }
}





function verifyStylesChanged() {
  var el = Ra.$('chkChangeStyle');
  if( el.style.width == '400px' && el.style.height == '200px' )
    Ra.$('results').setContent('success');
}





function verifyTogglingOfStylesON() {
  var el = Ra.$('chkToggleStyle');
  if( el.style.width == '200px')
    Ra.$('results').setContent('success');
}






function verifyTogglingOfStylesOFF() {
  var el = Ra.$('chkToggleStyle');
  if( el.style.width == '100px')
    Ra.$('results').setContent('success');
}







function verifyAccessKeyForCheckBox() {
  if( Ra.$('chkAccKey_CTRL').accesskey == '1')
    Ra.$('results').setContent('success');
}





function verifyCheckBoxDisabled() {
  if( Ra.$('disabledCheckBox_CTRL').disabled == 'disabled')
    Ra.$('results').setContent('success');
}





function verifyCheckBoxEnabled() {
  if( Ra.$('disabledCheckBox_CTRL').disabled != 'disabled')
    Ra.$('results').setContent('success');
}




function clickRadioButton(which) {
  Ra.$('rdo' + which + '_CTRL').click();
  Ra.Control.$('rdo' + which).onEvent('change');
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

                <br />

                <ra:CheckBox runat="server" ID="chkSetInVisible" Text="Set this to invisible" />
                <ra:Button runat="server" ID="setChkToInvisible" Text="Sets checkbox to invisible" OnClick="setChkToInvisible_Click" />

                <br />

                <ra:CheckBox Visible="false" runat="server" ID="chkSetVisible" Text="Set this to visible" OnCheckedChanged="chkSetVisible_CheckedChanged" />
                <ra:Button runat="server" ID="btnSetChkVisible" Text="Sets checkbox to visible" OnClick="btnSetChkVisible_Click" />

                <br />
                
                <ra:CheckBox runat="server" ID="chkToggle" Text="Toggles with button" />
                <ra:Button runat="server" ID="btnToggleChk" Text="Toggles checkbox" OnClick="btnToggleChk_Click" />

                <br />
                
                <ra:CheckBox runat="server" Text="Changes style" ID="chkChangeStyle" />
                <ra:Button runat="server" ID="btnChangeChkStyle" Text="Changes style of checkbox" OnClick="btnChangeChkStyle_Click" />

                <br />
                
                <ra:CheckBox runat="server" Text="Toggle style" ID="chkToggleStyle" />
                <ra:Button runat="server" ID="btnToggleStyle" Text="Toggles style of checkbox" OnClick="btnToggleStyle_Click" />

                <br />
                
                <ra:CheckBox runat="server" Text="Access key 1" ID="chkAccKey" AccessKey="1" OnCheckedChanged="chkAccKey_CheckedChanged" />

                <br />
                
                <ra:CheckBox runat="server" Text="Disabled checkbox" ID="disabledCheckBox" Enabled="false" OnCheckedChanged="disabledCheckBox_CheckedChanged" />
                <ra:Button runat="server" ID="btnEnabledCheckBox" Text="Enables checkbox" OnClick="btnEnabledCheckBox_Click" />

                <br />
                
                <ra:RadioButton runat="server" Text="Radio 1" ID="rdo1" OnCheckedChanged="rdo_CheckedChanged" GroupName="RdoGroup1" />
                <ra:RadioButton runat="server" Text="Radio 2" ID="rdo2" OnCheckedChanged="rdo_CheckedChanged" GroupName="RdoGroup1" />

                <br />
                
                <ra:Image runat="server" ID="img" ImageUrl="testImage1.png" AlternateText="Original" />
                <ra:Button runat="server" ID="btnChangeImg" Text="Changes values of image" OnClick="btnChangeImg_Click" />

            </div>
        </form>
    </body>
</html>
