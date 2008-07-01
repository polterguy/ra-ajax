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
            </div>
        </form>
    </body>
</html>
