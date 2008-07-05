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



function verifyPanel2DoesntExist() {
  if( !Ra.Control.$('pnlVisible') )
    Ra.$('results').setContent('success');
}



function verifyPanel2DoesExist() {
  if( Ra.Control.$('pnlVisible') )
    Ra.$('results').setContent('success');
}



function verifyPanel3DoesntExist() {
  if( !Ra.Control.$('pnlToggle') )
    Ra.$('results').setContent('success');
}



function verifyPanel3DoesExist() {
  if( Ra.Control.$('pnlToggle') )
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

                <br />

                <ra:Panel runat="server" ID="pnlVisible">
                    Panel is visible, make INvisible
                </ra:Panel>
                <ra:Button runat="server" ID="btnMakeINVisible" Text="Make panel in-visible" OnClick="btnMakeINVisible_Click" />

                <br />

                <ra:Panel runat="server" ID="pnlToggle">
                    Panel is here
                </ra:Panel>
                <ra:Button runat="server" ID="btnToggle" Text="Toggle panel" OnClick="btnToggle_Click" />

                <br />

                <ra:Panel runat="server" ID="pnlControls">
                    <ra:Button runat="server" ID="btnTest" Text="Changes text" OnClick="btnTest_Click" />
                </ra:Panel>

                <br />

                <ra:Panel runat="server" ID="pnlControlsINVisible" Visible="false">
                    <ra:Button runat="server" ID="btnTestINVisible" Text="Should be registered" OnClick="btnTestINVisible_Click" />
                </ra:Panel>
                <ra:Button runat="server" ID="setPnlVisible" Text="Sets panel with controls to visible" OnClick="setPnlVisible_Click" />

                <br />

                <ra:Panel runat="server" ID="pnlRec1" Visible="false">
                    <ra:Panel runat="server" ID="pnlRec2" Visible="false">
                        <ra:Button runat="server" ID="btnRec1" Text="Changes text" OnClick="btnRec1_Click" />
                    </ra:Panel>
                </ra:Panel>
                <ra:Button runat="server" ID="btnShowPnlRec" Text="Shows recursive panels" OnClick="btnShowPnlRec_Click" />

            </div>
        </form>
    </body>
</html>
