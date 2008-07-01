<%@ Page 
    Language="C#" 
    AutoEventWireup="true"
    CodeFile="RaControlBasics.aspx.cs" 
    Inherits="RaControlBasics" %>

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



// Creates some dummy JSON which it passes to the control to check if JSON
// parsing works for the Ra.Control class
function checkJSONBasics() {
  var ctrl = Ra.Control.$('Button1');

  ctrl.handleJSON('{"CssClass" : "testClass", "AddStyle" : [["width", "150px"], ["fontWeight", "bold"]]}');

  if( ctrl.element.className == 'testClass' && ctrl.element.style.width == '150px' && ctrl.element.style.fontWeight == 'bold') {
    Ra.$('results').setContent('success');
  }
}


function checkThatButtonWasDeleted() {
  var btn = Ra.Control.$('setInVisible');
  if( !btn )
    Ra.$('results').setContent('success');
}



function checkThatButtonWasCreated() {
  var btn = Ra.Control.$('setVisible');
  if( btn )
    Ra.$('results').innerHTML += 'ess';
}





function checkThatButtonIsInitiallyCreatedInVisible() {
  var btn = Ra.Control.$('setVisible');
  if( !btn )
    Ra.$('results').innerHTML = 'succ';
}



function checkCssClass() {
  var btn = Ra.Control.$('testCssClass');
  if( Ra.Control.$('testCssClass').element.className == 'someClass' && !Ra.Control.$('testCssClassNoClass').element.className)
    Ra.$('results').innerHTML = 'success';
}





function checkStylesAfterServerChange() {
  var el = Ra.Control.$('testAddStyles');
  if( el.element.style.width.toLowerCase() == '150px' && 
    el.element.style.fontWeight.toLowerCase() == 'bold' ) {
    Ra.$('results').innerHTML = 'success';
  }
}






function checkStylesWasSerialized() {
  var el = Ra.$('testStyleSerialized');
  if( el.style.width == '150px' && el.style.height == '100px' )
    Ra.$('results').innerHTML = 'success';
}










</script>


    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div id="results">
                    Unknown
                </div>

                <ra:Button 
                    ID="Button1" 
                    Text="Text JSON messaging to Control"
                    runat="server" />

                <ra:Button 
                    ID="setVisible" 
                    Visible="false"
                    Text="This was in-visible before"
                    runat="server" />

                <ra:Button 
                    ID="testCallback" 
                    runat="server" 
                    Text="Test Callback and set button to IN-visible"
                    OnClick="testCallback_Click" />

                <ra:Button 
                    ID="testCallbackSetButtonVisible" 
                    runat="server" 
                    Text="Test set button to Visible"
                    OnClick="testCallbackSetButtonVisible_Click" />

                <ra:Button 
                    ID="setInVisible" 
                    Text="This one will become in-visible"
                    runat="server" />

                <ra:Button 
                    ID="testCssClass" 
                    Text="Verify CssClass"
                    CssClass="someClass"
                    runat="server" />

                <ra:Button 
                    ID="testCssClassNoClass" 
                    Text="Verify NO CssClass"
                    runat="server" />

                <ra:Button 
                    ID="testAddStyles" 
                    Text="Test Add Styles"
                    OnClick="testAddStyles_Click"
                    runat="server" />

                <ra:Button 
                    ID="testSettingTextProperty" 
                    Text="Test Change Text value"
                    OnClick="testSettingTextProperty_Click"
                    runat="server" />

                <ra:Button 
                    ID="testChangeStyleValue" 
                    Text="Test Change Style Value"
                    OnClick="testChangeStyleValue_Click"
                    runat="server" />

                <ra:Button 
                    ID="testVerifyStyleValue" 
                    Text="Test Verify Changed Style Value"
                    OnClick="testVerifyStyleValue_Click"
                    runat="server" />

                <br />
                <br />

                <ra:Button 
                    ID="testStyleSerialized" 
                    Text="Verify Style properties serialized from .ASPX" 
                    style="width:150px;"
                    runat="server" />

                <br />
                <br />

                <input type="button" value="Test JSON Basics" id="testJSONBasicsBtn" onclick="checkJSONBasics();" />
                <input type="button" value="Dummy Test Button" id="Button2" onclick="checkStylesAfterServerChange();" />
            </div>
        </form>
    </body>
</html>































