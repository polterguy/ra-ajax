<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Facts.aspx.cs" 
    Inherits="RaWebsite.Facts" 
    Title="Facts about Ra-Ajax" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Facts about Ra-Ajax</h1>
    <p>
        Ra-Ajax is a <a href="managed-ajax-a-new-approach-to-ajax.blog" title="Managed Ajax">Managed Ajax library</a>
        for ASP.NET - both the <a href="http://www.mono-project.com/Main_Page">Mono</a> version and the Microsoft 
        version. This means you can use Ra-Ajax out of the box together with Visual Studio, 
        IIS, Windows Server and Linux/Apache/Mono etc.
    </p>
    <h2>License</h2>
    <p>
        Ra-Ajax is licensed under the <a href="http://www.gnu.org/licenses/lgpl.html">LGPL version 3</a> 
        Open Source license. This basically means that you can use it for free as you wish and you can 
        even fork the library if you wish. You
        can use it in Closed Source projects or in Open Source projects. You can also create Closed Source
        Extension Widgets for it. Though if you create changes in the "core library" you are obliged to 
        also release those changes in code form under the LGPL licence terms to those you distribute 
        that modified library to. Basically Ra-Ajax is <em>"Free as in Free Beer and Free as in Freedom"</em>
        - to use a Richard Stallman and Free Software Foundation expression.
    </p>
    <h2>JavaScript</h2>
    <p>
        Ra is made with the assumption that JavaScript is <em>hard</em> and not something
        most Application Developers should do themselves. Ra-Ajax tries to abstract away JavaScript
        as much as possible and you can probably create highly interactive Ajax Applications 
        with Ra-Ajax without having to resort to JavaScript at all.
    </p>
    <h2>Lightweight</h2>
    <p>
        Ra-Ajax is extremely lightweight which means if you use it your applications will be 
        more responsive, demand less resources and run faster. Ra-Ajax has less than 15KB of
        JavaScript in total which means that Ra-Ajax can easily be used in front end websites
        without stalling the user experience. This web page scores more than 80 in 
        <a href="http://developer.yahoo.com/yslow/">YSlow</a>. A typical Ra-Ajax webpage
        will use <strong>less then 8K of JavaScript</strong> and have very small amounts of
        CSS and CSS Background Images. According to YSlow our 
        <a href="http://ra-ajax.org/samples/Viewport-Calendar-Starter-Kit.aspx">Ajax Calendar Starter-Kit</a>
        is less then 25KB in size in total, as of the 7t of January 2009 - which probably would be several
        orders of magnitudes smaller then a similar solution built in most other Ajax Frameworks - 
        regardless of platform/language.
    </p>
    <h2>Runs on everything</h2>
    <p>
        Ra-Ajax should work on all browsers that are at least close to following 
        Open Standards. This includes devices such as iPhones/WindowsMobile, Opera,
        Safari, FireFox, Internet Explorer, Linux, Mac OS X, Konqueror, etc. As long
        as you have a browser with at least rudimentary JavaScript capabilities Ra 
        Ajax will work. In addition you can also use both Linux and Windows Servers
        as the back-end for your applications. The slogan could probably be; 
        <em>"Compile once, deploy anywhere, run all over the place"</em>. 
    </p>
    <h2>No learning curve</h2>
    <p>
        Ra-Ajax comes with virtually no learning curve due to its closely coupling to
        the ASP.NET WebControl nature. If you have done conventional ASP.NET 
        development with WebControls then learning Ra-Ajax will feel like a breeze.
    </p>
    <h2>Is Ra-Ajax related to ASP.NET AJAX?</h2>
    <p>
        <strong>No!</strong> ASP.NET AJAX is a library created by and maintained by Microsoft. Ra-Ajax
        shares no common code with ASP.NET AJAX and is created around a completely
        different model. While ASP.NET AJAX has a very fat client-side JavaScript
        API, and in addition builds a lot on "Partial Rendering" - Ra-Ajax is created 
        around the assumption that you should not have to do JavaScript development 
        yourself and that Partial Rendering is a <em>*dirty hack*</em> - best avoided.
    </p>
    <p>
        Ra-Ajax does not have ScriptManager(Proxy), it does not rely on Partial Rendering
        and there are no "update triggers" or "update panels" in Ra-Ajax. Ra-Ajax just 
        contains substitutes for the common WebControls like Button, CheckBox and RadioButton
        etc - in addition to some richer controls like Calendar, TreeView, Window and TabControl etc.
    </p>
    <p>
        Also Ra-Ajax will use orders of magnitudes less bandwidth then ASP.NET AJAX. Both
        in the initial rendering and in every Ajax Request after the initial rendering.
    </p>
    <p>
        In addition the core architecture behind Ra-Ajax makes it much easier to create stunningly
        advanced functionality with far less complexity and code then ASP.NET AJAX enables. Our
        <a href="http://ra-ajax.org/samples/Ajax-Forum-Starter-Kit.aspx">Ajax Forum sample</a>
        are completely made without one line of JavaScript and less then 300 lines of code - 
        purely C# code too. To create something similar in ASP.NET AJAX is probably close to 
        impossible due to the complexity of the code it will require.
    </p>
    <p>
        Ra-Ajax was built as a <em>"Free of Charge, Open Source and better alternative then ASP.NET AJAX to
        the Ajax for ASP.NET problem"</em>. Also Mono support is a high priority in Ra-Ajax.
    </p>
    <p>
        Compared to ASP.NET AJAX you will notice an extreme increase in productivity, far less complex code
        and far easier to maintain code. No to mention orders of magnitude less bandwidth usage compared
        to ASP.NET AJAX.
    </p>
    <h2>Is Ra-Ajax a commercial project?</h2>
    <p>
        Ra-Ajax is <strong>Free of Charge to use</strong> and you have all the freedoms you normally
        have in an Open Source project with Ra-Ajax. Including even forking the project if you wish.
        Ra-Ajax was built as an <em>Open Web RIA alternative to Adobe Flex and Silverlight</em> and
        we think it is very important that also the .Net developers have an Open Web alternative
        and are not condemned into using Silverlight or some other ActiveX based technology to build
        their Rich Internet Applications on. And this was more important for us then to have a 
        business model and earn money on Ra-Ajax.
    </p>
    <p>
        However, we too need food for ourselves and our kids. So we do charge for
        using our <a href="Starter-Kits.aspx">Starter-Kits</a> which will be a solid foundation for you
        to start your applications from. And we also appreciate all <a href="Donate.aspx">donations</a>
        given to us.
    </p>
    <h2>Why create yet another Ajax Library?</h2>
    <p>
        First of all because we need Open Source/Open Web tools for our own needs in our other 
        "bread and butter" applications. We also need great Ajax Libraries when 
        <a href="Author.aspx">working as consultants</a>. In addition to create Ajax Libraries are
        what we are good at and also what we enjoy the most.
    </p>
    <p>
        We in Ra Software have an intense believe in the Open Web and Ajax and while there exists
        great portable Open Source tools for almost every possible thing here in this world, Ajax
        for ASP.NET is virtually a "dark spot". Currently there exists far too few (good) 
        Open-Web/Ajax alternatives for ASP.NET that are Free as in Freedom and Free as in Free Beer.
    </p>
    <h2>Where is the documentation and support for Ra-Ajax</h2>
    <p>
        You can find <a href="http://ra-ajax.org/docs/namespaces.html">the documentation to Ra-Ajax here</a>.
        And if you need support you can ask your questions for free in our 
        <a href="http://stacked.ra-ajax.org/">Questions/Answers Stacked installation</a> - which even
        won't require you to confirm your email address since you can use OpenID to register there.
    </p>
    <p>
        If you need professional support or training, you should check out our 
        <a href="Author.aspx">"hire us" webpage</a>. We also have independant developers in our community 
        all over the world if you need someone "physically close" to your own offices.
    </p>

</asp:Content>

