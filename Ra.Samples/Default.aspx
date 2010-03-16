<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" %>

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

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Ra-Ajax Samples</title>
        <link href="media/Gold/Gold.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <ra:ExtButton 
                    runat="server" 
                    Text="Click me..."
                    OnClick="btn_Click"
                    ID="btn" />
                <ra:Window 
                    runat="server" 
                    Caption="Hello World 2.0 :)"
                    style="z-index:1000;position:absolute;top:100px;left:100px;width:500px;"
                    Visible="false"
                    ID="wnd">
                    <div style="padding:5px;height:300px;">
                        <ra:Timer 
                            runat="server" 
                            ID="timer" 
                            OnTick="timer_Tick" />
                        <ra:Label 
                            runat="server" 
                            ID="lbl" 
                            Text="Watch me..."
                            style="border:dashed 1px #999;padding:15px;font-weight:bold;margin:25px;display:block;" />
                    </div>
                    <ra:BehaviorObscurable 
                        runat="server" 
                        ID="obscurer" />
                </ra:Window>
            </div>
        </form>
    </body>
</html>
