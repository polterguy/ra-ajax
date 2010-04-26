<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="CheckBox_Default" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Behaviors" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Ra.Extensions" 
    Namespace="Ra.Extensions.Widgets" 
    TagPrefix="ra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ra:CheckBox 
                runat="server" 
                ID="chk" 
                Text="Click me" />
        </div>
    </form>
</body>
</html>
