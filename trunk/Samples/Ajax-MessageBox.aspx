<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-MessageBox.aspx.cs" 
    Inherits="Samples.AjaxMessageBox" 
    Title="Ra-Ajax MessageBox Sample" %>

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

    <h1>Ra-Ajax Samples - MessageBox</h1>
    <p>
        This is our <em>Ajax MessageBox sample</em>. Click the buttons below to test it out.
    </p>
    <p>
        <ra:Button 
            runat="server" 
            ID="showMsgBox1" 
            Text="Show message box (OK)" 
            OnClick="showMsgBox1_Click" />
        <br />
        <ra:Button 
            runat="server" 
            ID="showMsgBox2" 
            Text="Show message box (OK/Cancel)" 
            OnClick="showMsgBox2_Click" />
        <br />
        <ra:Button 
            runat="server" 
            ID="showMsgBox3" 
            Text="Show message box (Yes, No, Cancel)" 
            OnClick="showMsgBox3_Click" />
        <br />
        <ra:Button 
            runat="server" 
            ID="showMsgBox4" 
            Text="Show message box (get input)" 
            OnClick="showMsgBox4_Click" />
        <br />
        <ra:Button 
            runat="server" 
            ID="showMsgBox5" 
            Text="Show message box (get multiple lines input)" 
            OnClick="showMsgBox5_Click" />
    </p>
    <p>
        <ra:Label 
            runat="server" 
            ID="lbl" />
    </p>
    <p>
        The Ra-Ajax MessageBox is constructed in markup with ONE line of code, then you can set event 
        handlers for "closed" which will trigger when the user closes the MessageBox and pass on an 
        enumeration telling you which button was used to close it (OK, Cancel, Yes, No etc) and also
        (if there was any textual based input fields defined in it) what Text the user wrote inside
        of it.
    </p>
    <p>
        It's actually just a wrapper around a Modal Window, but is a nifty shorthand for creating the
        Window yourself without having to set all the properties and such yourself.
    </p>
    <ext:MessageBox 
        runat="server" 
        ID="MessageBox1" 
        Caption="Some sample MessageBox" 
        Body="This is the body of your MessageBox. This one has only an OK button..."
        style="width:250px;position:absolute;left:250px;top:250px;z-index:5000;" 
        MessageBoxType="OK"
        OnClosed="MessageBox_Closed" />
    <ext:MessageBox 
        runat="server" 
        ID="MessageBox2" 
        Caption="Some sample MessageBox" 
        Body="This is the body of your MessageBox... As you can see you can also add a Cancel button to get verifications from your users..."
        style="width:250px;position:absolute;left:250px;top:250px;z-index:5000;" 
        MessageBoxType="OK_Cancel"
        OnClosed="MessageBox_Closed" />
    <ext:MessageBox 
        runat="server" 
        ID="MessageBox3" 
        Caption="Yes/No/Cancel MessageBox" 
        Body="Here's a Yes/No/Cancel message box"
        style="width:250px;position:absolute;left:250px;top:250px;z-index:5000;" 
        MessageBoxType="Yes_No_Cancel"
        OnClosed="MessageBox_Closed" />
    <ext:MessageBox 
        runat="server" 
        ID="MessageBox4" 
        Caption="Give me some input" 
        Body="I want to know the meaning of life..."
        style="width:250px;position:absolute;left:250px;top:250px;z-index:5000;" 
        MessageBoxType="Get_Text"
        OnClosed="MessageBox_Closed" />
    <ext:MessageBox 
        runat="server" 
        ID="MessageBox5" 
        Caption="Give me some input" 
        Body="I want to know the meaning of life..."
        style="width:250px;position:absolute;left:250px;top:250px;z-index:5000;" 
        MessageBoxType="Get_Text_Multiple_Lines"
        OnClosed="MessageBox_Closed" />
</asp:Content>

