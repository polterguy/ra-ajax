<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-TabControl.aspx.cs" 
    Inherits="AjaxTabControl" 
    Title="Ra-Ajax TabControl Sample" %>

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

    <h1>Ajax TabControl Sample</h1>
    <p>
        This is our <em>Ajax TabControl Widget</em>. The Ra-Ajax TabControl is as most of the extension controls
        also entirely built on the server with no Custom JavaScript. It as quite similar to our 
        <a href="Ajax-Accordion.aspx">Ajax Accordion</a> though while the Accordion works mostly through DHTML
        and all Accordions are actually rendered in the HTML if you include one in your page the TabControl
        only renders the <em>ActiveTabControlView</em> and when the user switches ActiveView the server
        re-renders the TabControl making the HTML of the non-active TabViews in-visible.
    </p>
    <ext:TabControl runat="server" ID="tab" CssClass="tab-control">
        <ext:TabView Caption="Ajax TabControl view 1" runat="server" ID="tab1" CssClass="content">
        	Here you can see the content of the first TabControlView...
        	<br />
        	Our Ajax TabControl like all other Ajax Controls in Ra-Ajax can also contain any arbitrage HTML
        	and even other Ajax Controls as much as you like. This doesn't in any ways break the Ajax Event
        	logic of Ra-Ajax either but is 100% transparently implemented so that you as an application 
        	developer won't even notice the difference.
        </ext:TabView>
        <ext:TabView Caption="Tab view 2" runat="server" ID="tab2" CssClass="content">
        	Here's another TabControlView.
        	<br />
        	In fact the TabControl is purely built with the Ra-Ajax Panel, LinkButton and so on. The TabControl
        	is in fact a very nice starting point for your own Ajax Extension Controls.
        </ext:TabView>
        <ext:TabView Caption="Third" runat="server" ID="tab3" CssClass="content">
        	And here's yet another TabControlView
        </ext:TabView>
    </ext:TabControl>
    <br />
    <h2>Ra-Ajax is Open Source (and free of charge)</h2>
    <p>
    	Due to the LGPL license of Ra-Ajax you can use Ra-Ajax without having to pay us money or in any other
    	ways have restrictions on your own code. The LGPL effectively enables you to use Ra-Ajax in 
    	"Closed Source" applications. In fact the only restriction of the LGPL is if you fix a BUG or in any
    	other ways modifies Ra-Ajax itself and redistributes it. Then the LGPL kicks in and forces you
    	to also release those changes under the same LGPL license as Ra-Ajax itself is licensed under.
    </p>
    <p>
    	We choose to license Ra-Ajax under the LGPL because we believe that if we LGPL Ra-Ajax then a lot
    	of people will want to use it and help us find bugs, fix bugs and in any other ways help us create
    	a brilliant Ajax Library which we ourselves is dependant upon in our own work. We need great Ajax Libraries
    	both when we do consultancy work in addition to also when we develop our own Ajax Applications. And by
    	choosing the LGPL we get the best of both worlds. We ensure that many people will use it and help us 
    	make it better while at the same time we also ensures that Ra-Ajax <em>stays</em> Open Source and
    	isn't "stolen" and made into a proprietary Ajax Library by some huge corporation.
    </p>
    <br />
    <h2>How to help...</h2>
    <p>
    	First of all <em>spread the news</em>. If you have a blog then help us write tutorials, reviews and in
    	any other ways spread the word about Ra-Ajax. We are all dependant upon that as many as possible use 
    	Ra-Ajax if we want it to be a great Ajax Library. Things which would have been awesome would be
    	blogs about test results, maybe comparing Ra-Ajax to other Ajax Libraries? Code comparisons towards
    	ASP.NET AJAX and so on? Or scalability tests. Etc. Also we do appreciate it if you when you see some
    	blog or something where there's something you can comment on while posting a link to a Ra-Ajax 
    	blog/forums/samples as an "answer". We need help to spread the word! We belive strongly in Ajax and
    	the Open Web, but if the Open Web are going to prevail we need people to know about any tools that 
    	are great and can solve their problems when wanting to use it.
    </p>
    <p>
    	Also we need help in our <a href="http://ra-ajax.org/Forums/Forums.aspx">forums</a> answering support
    	and helping out newbies.
    </p>
    <p>
    	If you are a brilliant JavaScript and ASP.NET developer we could also need another set of hands, send
    	us an email telling us you'd like to help out. Also a bug report with a detailed description of how
    	to reproduce the given bug is very much worth for us. Or a patch to fix some thing you're having problems
    	with etc. Help is appreciated!
    </p>
    <p>
    	Another way you can help out is by creating extension controls on top of Ra-Ajax which are Open Source
    	and maybe LGPL themselves. This helps us by creating a better product since then people will see more
    	of their problems solved by choosing Ra-Ajax.
    </p>
    <a href="Ajax-Window.aspx">On to Ajax Window</a>
</asp:Content>
