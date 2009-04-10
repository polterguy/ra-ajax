<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Chat-Sample.aspx.cs" 
    Inherits="Samples.Chat" 
    Title="Ra-Ajax Chat Sample" %>

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
    runat="server">

    <h1>Ra-Ajax Chat Sample</h1>
    <p>
        How to create an Ajax chat application in less then 100 lines of code
    </p>
    <ext:Window 
        runat="server" 
        Closable="false"
        Caption="Chat with other visitors"
        style="width:650px;"
        ID="chatWindow">
        <ext:Timer 
            runat="server" 
            ID="timer" 
            OnTick="timer_Tick"
            Duration="4000" />
        <div style="padding:15px;height:250px;width:100%;">
            <ra:Label 
                runat="server" 
                id="chatCnt"
                CssClass="allChats"
                Tag="div" />
        </div>
        <div style="padding:15px;width:600px;">
            <ra:TextBox 
                runat="server" 
                style="width:100%;" 
                OnEnterPressed="SubmitChat" 
                MaxLength="100"
                Text="Type here..." 
                ID="txt" />
        </div>
    </ext:Window>
</asp:Content>
