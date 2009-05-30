<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.BehaviorUpdater.ascx.cs" 
    Inherits="Docs_Controls_BehaviorUpdater" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ext:ExtButton 
    runat="server" 
    ID="btn" 
    OnClick="btn_Click" 
    Text="Click me">
    <ra:BehaviorUpdater 
        runat="server" 
        Delay="500" 
        Color="#999999"
        ID="updater" />
</ext:ExtButton>

<ra:Panel 
    runat="server" 
    Visible="false"
    ID="pnl">
    <div style="padding:15px;margin:15px;border:dashed 2px #999;">
        If you look at the code you will see that there's
        a sleep on the server-side for two seconds. This 
        sleep makes sure that the updater will kick in 
        since it has a delay of 0.5 seconds. Use the
        BehaviorUpdater for controls which you expect will
        execute their Ajax Requests "slowly"...
    </div>
</ra:Panel>