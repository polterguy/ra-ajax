<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Facts.aspx.cs" 
    Inherits="Facts" 
    Title="Facts about Ra Ajax" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Facts about Ra Ajax</h1>
    <p>
        Ra Ajax is an Ajax library for ASP.NET - both the 
        <a href="http://www.mono-project.com/Main_Page">Mono</a> version and the Microsoft 
        version. This means you can use Ra Ajax out of the box together with Visual Studio, 
        IIS, Windows Server and Linux/Apache/Mono etc.
    </p>
    <br />
    <h2>License</h2>
    <p>
        Ra Ajax is licensed under an MIT(ish) kind of license which basically says; <em>"Use
        as you wish as long as you don't work for an agressive military government and you 
        don't fork Ra"</em>. Read the <em>license.txt</em> which can be found in the main 
        folder on disc when downloading Ra. This among other things means that you can use 
        Ra for free in commercial/proprietary projects if you like. Without putting 
        restrictions on your derived works. Ra is also licensed under the GPL3 license if 
        you prefer this.
    </p>
    <br />
    <h2>JavaScript</h2>
    <p>
        Ra is made with the assumption that JavaScript is <em>evil</em> and not something
        Application Developers should do themselves. Ra tries to abstract away JavaScript
        as much as possible and you can probably create very complex applications without
        having to use JavaScript at all.
    </p>
    <br />
    <h2>Lightweight</h2>
    <p>
        Ra Ajax is very lightweight which means if you use it your applications will be 
        more responsive, demand less resources and run faster. Ra has less than 15KB of
        JavaScript in total which means that Ra can easily be used in front end websites
        without stalling the user experience at all. This web page scores more than 80
        in <a href="http://developer.yahoo.com/yslow/">YSlow</a> which is the highest
        I know about for an Ajax Framework with server-side bindings.
    </p>
    <br />
    <h2>Runs on everything</h2>
    <p>
        Ra Ajax should work on all browsers that are at least close to following 
        Open Standards. This includes devices such as iPhones/WindowsMobile, Opera,
        Safari, FireFox, Internet Explorer, Linux, Mac OS X, Konqueror, etc. As long
        as you have a browser with at least rudimentary JavaScript capabilities Ra 
        Ajax will work. In addition you can also use both Linux and Windows Servers
        as the back-end for your applications. The slogan could probably be; 
        <em>"Compile once, deploy once, run all over the place"</em>. 
    </p>
    <br />
    <h2>No learning curve</h2>
    <p>
        Ra Ajax comes with virtually no learning curve due to its closely coupling to
        the ASP.NET WebControl nature. If you have done conventional ASP.NET 
        development with WebControls then learning Ra Ajax will feel like a breeze
        to you.
    </p>
    <br />
    <h2>Is Ra Ajax related to ASP.NET AJAX</h2>
    <p>
        No. ASP.NET AJAX is a library created by and maintained by Microsoft. Ra Ajax
        shares no common code with ASP.NET AJAX and is created around a completely
        different model. While ASP.NET AJAX has a very rich client-side JavaScript
        API, Ra Ajax is created around the assumption that you should never have to 
        do JavaScript development yourself.
    </p>
    <br />
    <h2>Is Ra Ajax a commercial project?</h2>
    <p>
        No. Currently I don't think Ra Ajax is mature enough to be a commercially 
        backed project. This means you can use Ra Ajax for free in your Closed 
        Source projects. Though I cannot guarantee you that I will never charge 
        for support, help, custom work, upgrades and new versions as Ra Ajax 
        matures and demands more resources on my behalf. Though you will always be
        able to use Ra Ajax for free in Open Source projects. And Ra Ajax will always
        be Open Source.
    </p>

</asp:Content>

