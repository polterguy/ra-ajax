<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ExtButton_Default" %>

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
    <head id="Head1" runat="server">
        <link href="../media/Gold/Gold.css" rel="stylesheet" type="text/css" />
        <title>Ra-Ajax Samples</title>
<style type="text/css">
.icon .ra-button-content
{
    background:transparent url(../media/Gold/sprites.png) no-repeat 0 -1245px;
    padding-left:20px;
    margin-left:-5px;
}
.icon[dir=rtl] .ra-button-content
{
    background:transparent url(../media/Gold/sprites.png) no-repeat right -1245px;
    padding-right:20px;
    margin-right:-5px;
}
</style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <ra:ExtButton 
                    ID="extbtn1" 
                    Text="ايكست بتتون" 
                    OnClick="extbtn1_Click"
                    runat="server" 
                    Dir="rtl"
                    CssClass="ra-button icon" />
                <ra:ExtButton 
                    ID="extbtn2" 
                    Text="Ra ExtButton" 
                    OnClick="extbtn2_Click"
                    CssClass="ra-button icon"
                    runat="server" />
            </div>
        </form>
    </body>
</html>
