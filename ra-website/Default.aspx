<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="RaWebsite._Default" 
    Title="Ra-Ajax - Home" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Breathtaking Ajax for ASP.NET - Free of Charge and Open Source</h1>
    <p>
        Ra-Ajax is an <em>Open Source and Free of Charge</em> 
        <a href="managed-ajax-a-new-approach-to-ajax.blog" title="Managed Ajax">Managed Ajax library</a> for 
        ASP.NET and <a href="http://www.mono-project.com/Main_Page">Mono</a>. Ra-Ajax is licensed under the 
        <a href="http://www.gnu.org/licenses/lgpl.html">LGPL3 license</a> and therefore effectively 
        <strong>Free of Charge</strong> - even though we charge for using some parts of Ra-Ajax commercially. 
        Ra-Ajax is built around the assumption that Partial Rendering sucks and JavaScript is hard. Every 
        single sample here in the samples section is written entirely without 
        <em>one line of Custom JavaScript</em>. This makes you;
    </p>
    <ul>
        <li>More productive</li>
        <li>More wealthy</li>
        <li>More happy</li>
    </ul>
    <p>
        Try out some of our proudest examples in Ra-Ajax below...
    </p>
    <div class="thumbs">
        <a href="http://ra-ajax.org/samples/Viewport-Calendar-Starter-Kit.aspx" class="links">
            <span class="image1">&nbsp;</span>
            <span class="header">Ajax Calendar Starter-Kit</span>
            <span class="text">Demonstrates how to create an Ajax Calendar Application in addition to being an "Ajax Starter-Kit" for your own projects. <br />Written in C#...</span>
        </a>
    </div>
    <div class="thumbs">
        <a href="http://ra-ajax.org/samples/Viewport-Sample.aspx" class="links">
            <span class="image2">&nbsp;</span>
            <span class="header">Ajax Viewport Starter-Kit</span>
            <span class="text">Demonstrates how to create an Ajax Viewport Application with a TreeView and an Ajax TabControl in addition to being an "Ajax Starter-Kit" for your own projects.</span>
        </a>
    </div>
    <div class="thumbs">
        <a href="http://ra-ajax.org/samples/Viewport-GridView-Sample.aspx" class="links">
            <span class="image3">&nbsp;</span>
            <span class="header">Ajax GridView/DataGrid Starter-Kit</span>
            <span class="text">Demonstrates how to create an Ajax GridView/DataGrid Application in addition to being an "Ajax Starter-Kit" for your own projects. <br />Written in VB.NET...</span>
        </a>
    </div>
    <ra:Panel 
        runat="server" 
        ID="pnlResults" 
        Visible="false" 
        style="border:solid 1px Black;background-color:Yellow;width:400px;padding:25px;float:left;display:none;margin-bottom:10px;">
        <ra:Label 
            runat="server" 
            ID="lblResults" 
            style="font-weight:bold;" />
        <p>
            Notice how there was no "custom JavaScript" written to show this Panel. Everything was done on the
            server in pure C# and resembles the ASP.NET WebControls way of writing Web Applications.
        </p>
    </ra:Panel>
    <h2 style="clear:both;">Up running in minutes</h2>
    <p>
        Thanx to Ra-Ajax' close resemblance to the ASP.NET WebControls method of development
        you will be up running minutes after downloading Ra-Ajax. Meaning if you know how to 
        use the ASP.NET Buttons, Labels and CheckBoxes - you already also know how to use 
        the Ra-Ajax Buttons, CheckBoxes and Labels. <strong>This makes you productive in seconds</strong>.
    </p>
    <h2>Lighter than air - faster than lightning</h2>
    <p>
        If you use e.g. <a href="http://getfirebug.com/">FireBug</a> and check out the size
        of the <a href="http://ra-ajax.org/samples/">samples in Ra-Ajax</a> you will probably 
        be very surprised. Ra-Ajax has close to ZIP JavaScript compared to other Ajax Frameworks. 
        Also Ra-Ajax is lightning fast and virtually has no overhead at all on the server. 
        <strong>This makes your apps way more responsive</strong>.
    </p>
    <h2>Forget JavaScript - Ditch Partial Rendering</h2>
    <p>
        Ra-Ajax completely abstracts away the very concept of JavaScript. In addition Ra-Ajax uses a
        stateful client pattern which makes it possible for Ra-Ajax to not rely at all on Partial Rendering.
        <strong>This makes you extremely productive</strong>.
    </p>
    <h2>Leave your troubles behind</h2>
    <p>
        Compared to other Ajax Frameworks, Ra-Ajax will feel like a miracle. In fact, forget everything 
        you think you know about Ajax. Ra-Ajax changes the whole ball game. With Ra-Ajax you can deliver
        web applications before your competitors have even finished up deciding which JavaScript frameworks 
        to us. With Ra-Ajax your apps will run on everything, including your cousin's toaster - if it has
        a browser. With Ra-Ajax you can actually claim your life back - imagine an Ajax Framework with a 
        <em>"see your family again guarantee"</em>. In fact when your children grows up Ra-Ajax will be
        the reason why you were able to be there for them since it makes you deliver in a fraction of the 
        time any other Ajax Framework will let you deliver in.
    </p>
    <h2>Satisfaction guaranteed!</h2>
    <p>
        In fact we're so sure of that Ra-Ajax will completely change your life that if you 
        <a href="http://ra-ajax.org/Starter-Kits.aspx">purchase one of our commercial Starter-Kits</a> 
        and you any time before 1 year have passed are for any reasons not satisfied we will 
        give you all your money back - AND let you keep using the Starter-Kit for free under the 
        commercial license terms. No questions asked!
    </p>
</asp:Content>

