<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-ImageButton.aspx.cs" 
    Inherits="AjaxImageButton" 
    Title="Ajax ImageButton Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - ImageButton</h1>
    <p>
        An <em>Ajax ImageButton</em> is a Button which instead of showing Text as the main content,
        displays an Image. The Ajax ImageButton is a wrapper around <em>&lt;input type="image"</em>
        and, as all other Ajax Controls in Ra-Ajax, follows the Open Standards as given by
        the <a href="http://w3.org">W3C</a>. Try to click the ImageButton below.
    </p>
    <ra:ImageButton 
        runat="server" 
        ID="imgBtn" 
        AlternateText="Winnie the Pooh on AJAX" 
        OnClick="imgBtn_Click"
        ImageUrl="media/flower2.jpg" />
    <br />
    <ra:Label 
        runat="server" 
        ID="lblResults" 
        Text="Watch me change" 
        style="color:#33f;" />
    <br />
    <br />
    <h2>Ra-Ajax and Open Standards</h2>
    <p>
        Ra-Ajax follows the Open Standards given by W3C on everything. This might sound foolish
        for some people since it according to some rumours <em>is so much easier to create killer features
        by bypassing the Open Standards and create some "custom plugin"</em> or something like that.
    </p>
    <p>
        The first time I started creating Web Applications I thought it was very difficult and when
        I learned how ActiveX worked I was thrilled. With ActiveX, I could do "whatever I wished" and
        I was not "locked into" thinking in HTML, CSS and JavaScript.
    </p>
    <p>
        In 1998 I created a website for me and my family. This website still exists (though I am too ashamed
        to show it) and it still runs in fact and works. This was even though I had to make a lot of really
        dirty hacks to be able to have it running since this was at the peak of the Browser Wars. It still
        works and most browsers will to some extent display it roughly correct. This website was created
        using JavaScript, HTML and CSS.
    </p>
    <p>
        None of the ActiveX "websites" I created after that works today. ActiveX is today on the "scrap yard
        of IT" and nobody takes it seriously today and it has transformed into a "hall of shame technology word".
    </p>
    <br />
    <h2>The ActiveX 2.0 de-evolution</h2>
    <p>
        Today there are lots of companies that want you to believe that they have the "new and improved HTML,
        CSS and JavaScript framework". Though for some of us that have been around for quite a while,
        those arguments give us an echo feeling. The exact same arguments were used by Microsoft
        in the late 90s about ActiveX.
    </p>
    <p>
        The slogan of Ra-Ajax is: <em>Building blocks for the next 5000 years</em>. And although
        that might be exaggerating, I am in no doubt what so ever, that web applications built
        on top of Open Web Standards instead of "ActiveX 2.0" will definitely be here considerably
        longer than the "ActiveX 2.0" technologies.
    </p>
    <p>
        So when it comes to using Open Standards, there are basically two questions that you need to ask yourself.
        Do I want to build applications that are still here 50 years from now or am I happy with
        building stuff that must be rebuilt or thrown away 5 years from now. And the second question
        is: Do I want my applications to run on everything or is <em>"only xxx yyy with zzz
        OK for me"</em>.
    </p>
    <p>
        If you build on Open Standards, then your applications will probably still function 100 years
        from now and they will run on everything from your Mom's toaster to your cousin's mainframe
        system. Not to mention that nobody will own you in regards to the very foundation of your
        existence. Open Web Matters! And Open Web is equivalent to using Open Standards! Open Web
        is about you being in control of what you do and saying no to be in the "pocket" of some
        big proprietary ActiveX 2.0 RIA Framework vendor.
    </p>
    <a href="Ajax-Label.aspx">On to Ajax Label</a>
</asp:Content>

