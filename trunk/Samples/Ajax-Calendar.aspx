<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Calendar.aspx.cs" 
    Inherits="Samples.AjaxCalendar" 
    Title="Ra-Ajax Calendar Sample" %>

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

    <h1>Ra-Ajax Samples - Calendar</h1>
    <p>
        This is our <em>Ajax Calendar</em> reference sample. The Ajax Calendar is an Extension control, which
        means that it can be found in the Extensions project. One of the important
        features of the Ra-Ajax Calendar is that it does <em>not</em> add to the JavaScript size. This is 
        because it is entirely composed of other existing Ajax Controls like LinkButtons, Labels, 
        DropDownLists and so on.
    </p>

    <ext:Calendar 
        runat="server" 
        ID="calTab" 
        CssClass="alphacube cal" 
        OnSelectedValueChanged="calTab_SelectedValueChanged"
        Value="2008.07.20 23:54" />

    <p style="clear:both;">
        <ra:Label 
            runat="server" 
            CssClass="updateLbl"
            ID="lbl" Text="Watch me as you change the date" />
    </p>
    <p>
        Try to change the Date in the Ajax Calendar above
    </p>
    <p>
        The Ajax Calendar is 100% localizable since everything is rendered on the server. You can localize it to 
        any language which is supported by .Net Framework or Mono. It also have properties for trapping both
        change of date and clicking a specific date or the Today button. Another nifty feature of our Calendar
        control is that it also have support for special rendering of days. This means that you can add up
        your own controls inside any specific cell in it.
    </p>
    <h2>Ra-Ajax and Mono/Linux/Apache</h2>
    <p>
        Did you know that even though Ra-Ajax is written on top of .Net, it is still possible (and easy) to deploy
        your Ra-Ajax Applications on Linux by using <a href="http://www.mono-project.com/">Mono</a>? Ra-Ajax
        is very dedicated to supporting Mono. In fact without Mono we probably would have implemented
        Ra-Ajax in RoR or J2EE or something. In fact I am changing back and forth between Linux and Windows
        when I develop and I roughly spend about 50% of my time developing Ra-Ajax functionality in Mono.
        Currently I am writing this on Ubuntu in MonoDevelop and running it using xsp2. In fact I haven't
        developed in anything but Mono now for more than 2 months! Ra-Ajax will always be very dedicated
        to supporting Linux through the Mono project since this gives you the freedom of running your 
        ASP.NET/Ra-Ajax website on Linux. This is important for us! <em>Ra-Ajax is 100% cross platform</em>.
    </p>
    <p>
        A very good example of a quite complex Ajax application which is written in .Net but runs on
        Mono, Apache and Linux is <a href="http://grurrah.com">Grurrah your environmental friend</a> 
        which although is not developed in Ra-Ajax but still developed in ASP.NET. Grurrah is written 
        in .Net, compiled using Mono's C# compiler, deployed on Apache and Linux running Mono and 100% 
        Open Source from the bottom and all the way to the top :)
    </p>
    <p>
        It even uses an Open Source database - <a href="http://mysql.com/">MySQL</a>!
    </p>
    <p>
        A lot of people are falsely claiming that Mono is a "child's toy version" of .Net and that they
        will always need to play catch-up with Microsoft. This is not true! Mono is a fully working 
        implementation of .Net. It even has full support for LINQ, Generics, ASP.NET and mostly everything
        you would expect to be in a full version of .Net.
    </p>
    <p>
        I happen to know large portions of the Mono team, including Miguel DeIcaza and I know that they
        have a lot of resources on making sure that they are constantly compatible with every aspect
        of Microsoft.NET. Sometimes they probably have more resources on Mono than what Microsoft 
        has on .Net, due to Microsoft's "big gun development plan", where they target everything into
        one aspect of their development.
    </p>
    <p>
        Ra-Ajax, including our Ajax Calendar, will always be focusing a lot of resources on being
        Mono Compatible so that you can have the choice of deploying your applications on both Linux
        and Windows Servers. Just like we believe in that in the client layer you should always be able
        to run your applications on everything from "Mom's toaster" to your "Cousin's mainframe" we also
        believe that you should be able to deploy your applications on Linux, Windows and 
        everything which can run either Mono or Windows Servers.
    </p>
    <p>
        In fact you could probably get your Ra-Ajax applications to run on J2EE by using 
        <a href="http://mainsoft.com/" rel="nofollow">Grasshopper</a> from MainSoft if you wanted. This means you
        could develop in Boo on Linux using MonoDevelop, then compile through GrassHopper on Windows
        and finally deploy on FreeBSD using Tomcat or JBoss. Though you'd have to be pretty insane for
        having such a weird development cycle ;)
    </p>
    <p>
        But you CAN! And this is what Ra-Ajax is all about! Freedom of choice!
    </p>
    <p>
        <em>"Build once, deploy anywhere and run all over the place"</em> is one of our slogans.
        The other is <em>"Building blocks for the next 5000 years"</em>. I hope you believe this
        and are willing to <a href="http://code.google.com/p/ra-ajax/">give Ra-Ajax a shot</a>.
        Ra-Ajax is free to use, we don't have any plans for starting charging you for anything else
        than consulting services (if you should need it), you can use it for free in your Closed Source
        applications (due to LGPL) and we will even help you for free in our 
        <a href="http://ra-ajax.org/Forums/Forums.aspx">forums</a> as much as we can for free.
    </p>
    <a href="Ajax-InPlaceEdit.aspx">On to Ajax InplaceEdit</a>
</asp:Content>
