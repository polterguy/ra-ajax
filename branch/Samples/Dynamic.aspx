<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Dynamic.aspx.cs" 
    Inherits="Samples.Dynamic_Sample" 
    Title="Dynamically Loading ASP.NET WebControls" %>

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

    <h1>Dynamically Loading ASP.NET WebControls</h1>
    <p>
        Sometimes you need to dynamically load ASP.NET WebControls (which includes Ra-Ajax Controls) 
        and for such cases the Ra-Ajax <em>Dynamic</em> Control is just what you need. 
        Here's a small example of usage. Fill in the parameters and watch them being used 
        as the basis for some Ajax Charting.
    </p>
    <p>
        <ra:Dynamic 
            runat="server" 
            OnReload="dynamic_Reload"
            ID="dynamic" />
        <ra:Button 
            runat="server" 
            ID="reset" 
            Text="Reset" 
            AccessKey="R"
            Tooltip="R is shortcut key"
            Visible="false" 
            OnClick="reset_Click" />
    </p>
    <h2>HTTP is stateless</h2>
    <p>
        To WinForms, GTK and Qt developers beginning ASP.NET the fact that we need to "re-create" 
        the controls on every single postback/callback might seem unintuitive.
        But remember that HTTP is a <strong>stateless protocol</strong>. This means that when the server
        is finished serving your HTTP request it is gone forever. The server cannot hold on to
        the fact that you rendered "this" or "that" control into this page. And in fact when you
        come back you are coming back to a "new" page. And this "new" page doesn't know which controls you
        dynamically loaded previously. This is a big part of the beauty of HTTP and HTML since that's 
        what makes HTTP scale so insanely good.
    </p>
    <p>
        Normally for static controls the .aspx file
        will handle this for you through its "magic". But for Dynamically Created Controls
        it has no mechanism to know "which controls to reload". This means you must take
        care of which controls to reload yourself.
    </p>
    <p>
        This is not unique for Ra-Ajax but rather a general problem for all applications
        built on top of ASP.NET and in fact the HTTP protocol in general. But for some obscure 
        reason it seems to show up a lot more often in Ajax Frameworks than in "normal
        conventional" ASP.NET applications. I guess with power, comes the lust to do more...
    </p>
    <p>
        If you are stuck with problems regarding Dynamically Created Ra-Ajax Controls then
        you probably need to check up the <em>Ra-Ajax Dynamic Control</em> which is the one
        being showcased on this page. It very effectively abstracts away all these problems 
        if used correctly.
    </p>
    <a href="Updater.aspx">On to Ajax Updater Sample</a>
</asp:Content>

