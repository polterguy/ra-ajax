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

    <h1>*Really* easy Ajax for ASP.NET</h1>
    <p>
        Ra-Ajax is an <em>Open Source and Free of Charge</em> Ajax library for ASP.NET. With Ra-Ajax you will
        experience;
    </p>
    <ul>
        <li>Way faster Time2Market</li>
        <li>More beautiful applications</li>
        <li>Way more flexibility</li>
    </ul>
    <p>
        Ra-Ajax puts the <em>"code less, create more"</em> slogan to new heights! And that without
        compromising flexibility what-so-ever! Ra-Ajax is just <em>"more gain for less pain"</em>, period!
    </p>
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
            <span class="text">Demonstrates how to create an Ajax Viewport Application with a TreeView and an Ajax TabControl in addition to being an "Ajax Starter-Kit" for your own projects. <br />Written in C#...</span>
        </a>
    </div>
    <div class="thumbs">
        <a href="http://ra-ajax.org/samples/Viewport-GridView-Sample.aspx" class="links">
            <span class="image3">&nbsp;</span>
            <span class="header">Ajax GridView/DataGrid Starter-Kit</span>
            <span class="text">Demonstrates how to create an Ajax GridView/DataGrid Application in addition to being an "Ajax Starter-Kit" for your own projects. <br />Written in VB.NET...</span>
        </a>
    </div>
    <p>
        ...most of these samples are less then 300 lines of code, and all of them are contained in 
        our <a href="http://code.google.com/p/ra-ajax/">download</a>.
    </p>
    <p>
        The <em>Hello World</em> application...
    </p>
    <p>
        <ra:Button 
            runat="server" 
            ID="btn" 
            OnClick="btn_Click"
            Text="Click me..." />
    </p>
    <p>
        All the code written to do the above is here;
    </p>
    <pre>
<strong>.ASPX</strong>;
<span style="color:Blue;">&lt;</span><span style="color:#b00;">ra</span><span style="color:Blue;">:</span><span style="color:#b00;">Button</span>
    <span style="color:Red;">runat</span><span style="color:Blue;">="server" </span>
    <span style="color:Red;">ID</span><span style="color:Blue;">="Button1" </span>
    <span style="color:Red;">OnClick</span><span style="color:Blue;">="btn_Click"</span>
    <span style="color:Red;">Text</span><span style="color:Blue;">="Click me..." /&gt;</span>
    </pre>
    <pre>
<strong>C#</strong>;
<span style="color:Blue;">protected void</span> btn_Click(<span style="color:Blue;">object</span> sender, <span style="color:#06b;">EventArgs</span> e)
{
    btn.Text = <span style="color:#b00;">"Hello world"</span>;
    <span style="color:Blue;">new</span> <span style="color:#06b;">EffectSize</span>(btn, 500, 10, 25)
      .ChainThese(<span style="color:Blue;">new</span> <span style="color:#06b;">EffectFadeOut</span>(btn, 500)
        .ChainThese(<span style="color:Blue;">new</span> <span style="color:#06b;">EffectFadeIn</span>(btn, 500)
          .ChainThese(<span style="color:Blue;">new</span> <span style="color:#06b;">EffectSize</span>(btn, 200, 80, 500)
            .ChainThese(<span style="color:Blue;">new</span> <span style="color:#06b;">EffectHighlight</span>(btn, 500))))).Render();
}
    </pre>
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
        stateful client pattern which makes it possible for you to completely forget about Partial Rendering.
        <strong>This makes you extremely productive</strong>.
    </p>
    <h2>Leave your troubles behind</h2>
    <p>
        Compared to other Ajax Frameworks, Ra-Ajax will feel like a miracle. In fact, forget everything 
        you think you knew about Ajax. Ra-Ajax changes the whole ball game. With Ra-Ajax you can deliver
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
    <p>
        So why not <a href="http://code.google.com/p/ra-ajax/">download and test Ra-Ajax for yourself</a>.
        You already know what's needed to use it, it's free of charge and all you really risk is
        wasting 850KB of bandwidth while downloading Ra-Ajax. You will <strong>*not*</strong> regret it!
        And the Ra-Ajax download comes with *TONS* of samples for you to look at for inspiration.
    </p>
</asp:Content>

