<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="RaPanel.aspx.cs" 
    Inherits="RaPanel" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Untitled Page</title>

        <script type="text/javascript">


function init() {
  Ra.$('results').setContent('unknown');
}



function verifyPanelDoesntExist() {
  if( !Ra.Control.$('pnlInvisible') )
    Ra.$('results').setContent('success');
}



function verifyPanelDoesExist() {
  if( Ra.Control.$('pnlInvisible') )
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
                <ra:Panel runat="server" ID="pnl1">
                    testing rendering of panel
                </ra:Panel>
                <ra:Panel runat="server" ID="pnlInvisible" Visible="false">
                    Panel was visible, howdie's are cool
                </ra:Panel>
                <ra:Button runat="server" ID="btnMakeVisible" Text="Make panel visible" OnClick="btnMakeVisible_Click" />
            </div>
        </form>
    </body>
</html>
