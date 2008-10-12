<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="EventSystem.aspx.cs" 
    Inherits="EventSystem" %>

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
        </script>

    </head>
    <body>
        <div id="results">
            Unknown
        </div>
        <form id="form1" runat="server">
            <ra:Label runat="server" ID="click" Text="click" OnClick="click_click" />
            <br />
            <ra:Label runat="server" ID="dblClick" Text="dblclick" OnDblClick="dblClick_dblClick" />
            <br />
            <ra:Label runat="server" ID="keyDown" Text="keyDown" OnKeyDown="keyDown_keyDown" />
        </form>
    </body>
</html>
