<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Timer.aspx.cs" 
    Inherits="Timer" 
    Title="Untitled Page" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ajax Timers</h1>
    <p>
        Often you need to periodically poll the server to check for some condition or something. 
        For such circumstances the <em>Ra Ajax Timer</em> might come in handy. Like all other Ra
        Ajax Controls it too follows the complete Page Life cycle meaning you can do whatever you 
        wish in the Tick Event Handler.
    </p>
    <ext:Timer 
        runat="server" 
        OnTick="timer_Tick" 
        Milliseconds="1000"
        ID="timer" />
    <ra:Panel 
        runat="server" 
        ID="pnlTimer" 
        style="position:absolute;border:solid 1px Black;background-color:Yellow;width:400px;height:200px;padding:25px;float:left;">
        Follow this one as it grows randomly around due to the timer which creates an Ajax Effect on the server
        and updates the label below.
        <br />
        <ra:Label runat="server" ID="date" style="font-style:italic;color:#999;" Text="Date" />
    </ra:Panel>
    <div style="height:300px;">&nbsp;</div>
    <p>
        Of course this is completely erratic and not something you would do in a *real* application but
        I hope it illustrates the flexibility of Ra Ajax Timers. The Timer has properties for Milliseconds 
        between "Ticks" and it can be disabled and enabled. The reference application for an Ajax Timer would
        obviously be a Chat Client or maybe an Email Application that polls to check for new messages.
    </p>

</asp:Content>

