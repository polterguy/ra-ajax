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
        Ra Ajax is licensed under the <a href="http://www.gnu.org/licenses/lgpl.html">LGPL version 3</a> 
        Open Source license. This basically means that you can use it for free as you wish, you can 
        fork the library and you don't even have to tell me (or anyone else) that you're using it. Though
        if you create changes in the "core library" you are obliged to also release those changes in code
        form under the LGPL licence terms to those you distribute that modified library to. Basically
        Ra-Ajax is <em>free as in free beer and as in freedom</em>.
    </p>
    <br />
    <h2>JavaScript</h2>
    <p>
        Ra is made with the assumption that JavaScript is <em>hard</em> and not something
        Application Developers should do themselves. Ra tries to abstract away JavaScript
        as much as possible and you can probably create very complex Ajax Applications 
        with Ra Ajax without having to use JavaScript at all.
    </p>
    <br />
    <h2>Lightweight</h2>
    <p>
        Ra Ajax is very lightweight which means if you use it your applications will be 
        more responsive, demand less resources and run faster. Ra has less than 15KB of
        JavaScript in total which means that Ra can easily be used in front end websites
        without stalling the user experience. This web page scores more than 80 in 
        <a href="http://developer.yahoo.com/yslow/">YSlow</a>.
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
        <em>"Compile once, deploy anywhere, run all over the place"</em>. 
    </p>
    <br />
    <h2>No learning curve</h2>
    <p>
        Ra Ajax comes with virtually no learning curve due to its closely coupling to
        the ASP.NET WebControl nature. If you have done conventional ASP.NET 
        development with WebControls then learning Ra Ajax will feel like a breeze.
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
        NO! Ra-Ajax is not a commercial product and everything in the library is done 
        on a completely voluntary basis. We wanted to create a business model around 
        Ra-Ajax but since <a href="http://gaiaware.net">Gaiaware AS</a> (a company Thomas 
        Hansen founded a couple of years ago) threatened us
        with lawsuits if we were to create a <em>competing product</em> we felt it was 
        better to remove all financial incentives behind the library itself and just have
        it as a <em>hobby project</em> to remove all doubt. Read the whole 
        <a href="goodbye-gaia-ajax-widgets-hello-ra-ajax.blog">Gaia versus Ra Ajax story here</a>. 
        Though there are <a href="Forums/Forums.aspx">forums</a> where you can ask for support if you're 
        stuck and hopefully lots of people in our hopefully growing community in addition 
        to that the developers at Ra Software will always be able to answer your requests. We
        also have a <a href="Todo.aspx">bugtracker</a> which we monitor if you find bugs and such. In 
        addition all the developers at Ra Software will always use Ra-Ajax as their main tool
        when developing other applications which will be our "bread and butter" applications which
        will drive the development of Ra-Ajax forward for you. So Ra-Ajax is very unlikely going
        to be abandoned anytime soon even though it is not a commercial product.
    </p>
    <br />
    <h2>Why create yet another Ajax Library?</h2>
    <p>
        First of all because we need Open Source/Open Web tools for our own needs in our other 
        "bread and butter" applications. In addition there is nothing more fun than writing
        an Ajax Library :)
        <br />
        We in Ra Software have intense believe in the Open Web and Ajax and while there exists
        great portable Open Source tools for almost every possible thing here in this world, Ajax
        for ASP.NET is virtually a "dark spot". Currently there exists only - 
        <a href="http://ajaxwidgets.com">Gaia Ajax Widgets</a> which has some greatness within, 
        though we in Ra Software do not believe that Gaia will stay 
        <a href="http://www.mono-project.com/Main_Page">Mono</a> compatible. For us Mono compatibility
        is crucial. Also owning our own foundation is crucial for us and since 
        <a href="goodbye-gaia-ajax-widgets-hello-ra-ajax.blog">Gaia Ajax Widgets was lost</a> for 
        us in Ra Software we had little choice but starting our own Ajax for ASP.NET initiative.
    </p>

</asp:Content>

