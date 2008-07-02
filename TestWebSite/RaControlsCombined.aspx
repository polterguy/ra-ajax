<%@ Page 
    Language="C#" 
    AutoEventWireup="true"
    CodeFile="RaControlsCombined.aspx.cs" 
    Inherits="RaControlsCombined" %>

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



function verifyLabelIsRendered() {
  if( Ra.$('lbl').innerHTML == 'Some label' )
    Ra.$('results').setContent('success');
}




function verifyAccessKeyWorks() {
  if( Ra.$('accessKeyButton').accesskey == '1' )
    Ra.$('results').setContent('success');
}






function verifyPasswordValueChanged() {
  if( Ra.$('password2').value == 'success' )
    Ra.$('results').setContent('success');
}





function verifyControlsDoesnJSONWhenNotChanged() {
  var form = new Ra.Ajax({
    raCallback:true,
    onAfter: function(response){
      if( response.indexOf('testLblDoesntJson') == -1 &&
        response.indexOf('testTextBoxDoesntJson') == -1 &&
        response.indexOf('testButtonDoesntJson') == -1 ) {
        Ra.$('results').setContent('success');
      }
    }
  });
}






function verifyColsAndRowsOfTextAreaWasChanged() {
  var el = Ra.$('textArea');
  if( el.rows == 60 && el.cols == 10 )
    Ra.$('results').setContent('success');
}





        </script>
    </head>
    <body>
        <div id="results">
            Unknown
        </div>
        <form id="form1" runat="server">
            <div>
                <ra:Label runat="server" ID="lbl" Text="Some label" />
                <br />
                <ra:Label runat="server" ID="testChangeValue" Text="Some value" />
                <ra:Button runat="server" ID="textChangeLabelValue" OnClick="textChangeLabelValue_Click" Text="Test changing value of label" />
                <ra:Button runat="server" ID="accessKeyButton" OnClick="accessKeyButton_Click" Text="Test AccessKey property" AccessKey="1" />
                
                <br />
                <br />
                
                <ra:TextBox runat="server" ID="txtBox" Text="Value of text box" />
                <ra:Button runat="server" ID="testChangeTextBoxValue" Text="Change textbox value" OnClick="testChangeTextBoxValue_Click" />
                
                <br />
                <br />
                
                <ra:TextBox runat="server" ID="testCallBack" Text="default text" OnTextChanged="testCallBack_TextChanged" />
                
                <br />
                <br />
                
                <ra:TextBox runat="server" ID="txtComplexValue" Text="Some & complex __@£$%% value" />
                <ra:Button runat="server" ID="changeToComplexValue" Text="Change textbox to complex value" OnClick="changeToComplexValue_Click" />
                <ra:Button runat="server" ID="verifyComplexValue" Text="Verify complex value changed" OnClick="verifyComplexValue_Click" />
                
                <br />
                <br />

                <ra:TextArea runat="server" ID="textArea" Text="Text of textarea" />
                <ra:Button runat="server" ID="testTextArea" Text="Test text area" OnClick="testTextArea_Click" />
                <ra:Button runat="server" ID="testTextArea2" Text="Test text area2" OnClick="testTextArea2_Click" />
                <ra:Button runat="server" ID="testTextArea3" Text="Test text area3" OnClick="testTextArea3_Click" />
                <ra:Button runat="server" ID="changeColsRowsOfTextArea" Text="Change size of textarea" OnClick="changeColsRowsOfTextArea_Click" />
                
                <br />
                <br />
                
                <ra:TextBox runat="server" ID="password" TextMode="Password" Text="Password Text" />
                <ra:Button runat="server" ID="testPassword" Text="Verify password value" OnClick="testPassword_Click" />

                <ra:TextBox runat="server" ID="password2" TextMode="Password" Text="Password Text" />
                <ra:Button runat="server" ID="testPassword2" Text="Verify password CHANGES" OnClick="testPassword2_Click" />

                <br />
                <br />
                
                <ra:Label runat="server" ID="testLblDoesntJson" />
                <ra:TextBox runat="server" ID="testTextBoxDoesntJson" />
                <ra:Button runat="server" ID="testButtonDoesntJson" />
                <input type="button" value="Test unchanged values doesn't JSON" onclick="verifyControlsDoesnJSONWhenNotChanged();" />
            </div>
        </form>
    </body>
</html>
