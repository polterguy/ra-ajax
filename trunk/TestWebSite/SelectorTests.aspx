<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="SelectorTests.aspx.cs" 
    Inherits="SelectorTests" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <ra:Panel runat="server" ID="easyTest">
            <ra:Button runat="server" ID="firstButton" Text="Click me" OnClick="firstButton_Click" />
            <ra:Button runat="server" ID="secondButton" />
        </ra:Panel>
        <ra:Panel runat="server" ID="recursiveTest">
            <ra:Panel runat="server" ID="recursive2">
                dummy
            </ra:Panel>
            <ra:Panel runat="server" ID="recursive3">
                <ra:Panel runat="server" ID="recursive4">
                    <ra:Button runat="server" ID="thirdButton" Text="click me" OnClick="third_click" />
                    <ra:Button runat="server" ID="fourthButton" CssClass="testCss" Text="click me" OnClick="fourth_click" />
                </ra:Panel>
                <ra:Panel runat="server" ID="recursive5">
                    <ra:Panel runat="server" ID="recursive6">
                        <ra:Button runat="server" ID="fifthButton" Text="click me" />
                    </ra:Panel>
                </ra:Panel>
            </ra:Panel>
        </ra:Panel>
        <ra:Button runat="server" ID="enumerableButton" Text="click me" OnClick="testButton_Click" />
    </form>
</body>
</html>
