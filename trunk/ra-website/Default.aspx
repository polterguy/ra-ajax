<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="RaWebsite._Default" 
    Title="Ra Ajax - Home" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ra Ajax Home</h1>
    <p>
        Ra Ajax is an <strong>Ajax library for ASP.NET and Mono</strong> but its general nature on the client-side
        will make it very useful also for other server-side bindings like PHP, J2EE RubyOnRails etc all though other 
        bindings than towards ASP.NET is not included. Ra Ajax is licensed under the <a href="http://www.gnu.org/licenses/lgpl.html">LGPL3 license</a>. 
        Ra Ajax is built around the assumption that JavaScript is hard and not something Application Developers should 
        spend time on doing. Every single sample here in the samples section is written entirely without <em>one line 
        of Custom JavaScript</em>. This makes you;
    </p>
    <ul>
        <li>More productive</li>
        <li>More wealthy</li>
        <li>More happy</li>
    </ul>
    <p>
        Try out the Ra Ajax "Hello World" application below.
    </p>
    <br />
    <div style="float:left;height:200px;">
        <table>
            <tr>
                <td>
                    First name:
                </td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="txtFirstName" />
                </td>
            </tr>
            <tr>
                <td>
                    Surname:
                </td>
                <td>
                    <ra:TextBox 
                        runat="server" 
                        ID="txtSurname" />
                </td>
            </tr>
            <tr>
                <td 
                    colspan="2" 
                    style="text-align:right;">
                    <ra:Button 
                        runat="server" 
                        ID="btnSubmit" 
                        Text="Save" 
                        OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </div>

    <ra:Panel 
        runat="server" 
        ID="pnlResults" 
        Visible="false" 
        style="border:solid 1px Black;background-color:Yellow;width:400px;padding:25px;float:left;display:none;">
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
        Thanx to Ra Ajax' close resemblance to the ASP.NET WebControls method of development
        you will be up running minutes after downloading Ra Ajax. Meaning if you know how to 
        use the ASP.NET Buttons, Labels and CheckBoxes - you already also know how to use 
        the Ra Ajax Buttons, CheckBoxes and Labels.
    </p>
    <h2>Lighter than air - faster than lightning</h2>
    <p>
        If you use e.g. <a href="http://getfirebug.com/">FireBug</a> and check out the size
        of this webpage you might be surprised. This entire webpage, which is a 
        complete Ajax WebPage with rich server-side Ajax Controls and completely written in 
        C# on the server without custom JavaScript is <strong>less than 50KB in total</strong>. 
        This includes the images, CSS, content and JavaScript. The entire JavaScript in Ra 
        Ajax is minified less than 15KB in size. Not to mention its YSlow score of more than 
        80. Ra Ajax can easily be incorporated into your front end websites without your 
        users even noticing any overhead at all for the initial download or the Ajax Callbacks.
    </p>
    <h2>This website...</h2>
    <p>
        ...is entirely created using Ra Ajax and actually the samples website for Ra Ajax. It uses 
        <em>ActiveRecord</em> for database abstraction and <em>Url Rewriting</em>
        based on <em>Fabrice</em>'s work. Everything you 
        see here, including the forums and blogs is Open Source and part of the Ra Ajax download
        and can be consumed in your own projects as you wish. But please rewrite the TEXT of the 
        static pages if you do so since otherwise Google will penaltize websites if it finds what 
        it considers "duplicate content".
    </p>
    <p>
        If you see a page you're curious about how is being implemented then you can easily view
        the source of that page by clicking the "Show code" button which can be found at the 
        top/right corner of all the webpages at this website.
    </p>
    <p>
        Every single page at this website is extremely optimized for Search Engine Visibility (SEO),
        load speed, execution speed and so on. Every page at this website scores almost 90 in 
        <a href="http://developer.yahoo.com/yslow/">YSlow</a> which BTW is a marvelous tool for
        web-developers to optimize their websites.
    </p>
    <p>
        This website should serve as a very good starting point for your own website projects if
        you need forum support, basic TODO lists (bugtracker) and blogging coupled with static pages or 
        "functional pages".
    </p>
</asp:Content>

