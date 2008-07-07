<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" 
    Title="Ra Ajax Samples - Home" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ra Ajax Samples Home</h1>
    <p>
        Ra Ajax is an <strong>Ajax library for ASP.NET and Mono</strong> but its general nature on the client-side
        will make it very useful also for other server-side bindings like PHP, J2EE RubyOnRails etc. Ra Ajax
        is licensed under an MIT(ish) license which basically says <em>"use as you wish, but don't fork the code and
        don't use it if you work for an agressive military power"</em>. Ra Ajax is built around the assumption that 
        JavaScript is hard and not something Application Developers should spend time on doing. Every single sample 
        here in the samples section is written entirely without <em>one line of Custom JavaScript</em>. This makes you;
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
    <h2>Lighter than air</h2>
    <p>
        If you use e.g. <a href="http://getfirebug.com/">FireBug</a> and check out the size
        of this webpage you might be surprised. This entire webpage, which is a 
        complete Ajax WebPage with rich server-side Ajax Controls and completely written in 
        C# on the server without custom JavaScript is <strong>less than 50KB in total</strong>. 
        This includes the images, CSS, content and JavaScript. The entire JavaScript in Ra 
        Ajax is minified less than 15KB in size. Not to mention its YSlow score of more than 
        80. Ra Ajax can easily be incorporated into your front end websites without your 
        users even noticing any overhead at all for the initial download.
    </p>
</asp:Content>

