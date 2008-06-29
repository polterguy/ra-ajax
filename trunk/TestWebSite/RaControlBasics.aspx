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

    ctrl.handleJSON('{"RemoveStyle" : ["width", "fontWeight"]}');

    if( !ctrl.element.style.width && !ctrl.element.style.fontWeight ) {
      Ra.$('results').setContent('success');
    }
  }
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
                    runat="server" />

                <ra:Button 
                    ID="testCallback" 
                    runat="server" 
                    OnClicked="testCallback_Clicked" />

                <ra:Button 
                    ID="setInVisible" 
                    runat="server" />
                
                <input type="button" value="Test JSON Basics" id="testJSONBasicsBtn" onclick="checkJSONBasics();" />
            </div>
        </form>
    </body>
</html>
