<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    ValidateRequest="false"
    AutoEventWireup="true" 
    CodeFile="Ajax-RichEdit.aspx.cs" 
    Inherits="Samples.AjaxRichEdit" 
    Title="Ra-Ajax RichEdit Sample" %>

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

    <h1>Ajax RichEdit Sample</h1>
    <p>
        This is our <em>Ajax RichEditor</em> sample. The Ra-Ajax RichEdit is actually just a wrapper around
        an Ajax Panel (HTML "div" element) which is set to <em>contentEditable = true</em>.
    </p>
    <ext:RichEdit 
        runat="server" 
        ID="richeditor" 
        CssClass="panel"
        Text="<p>Try to edit this text. This is an <em>Ajax RichEditor</em> try editing the text within here...</p>" />
    <ra:Button 
        runat="server" 
        ID="boldBtn" 
        Text="Make selection bold" 
        OnClick="boldBtn_Click" />
    <h2>This is a really crappy Ajax RichEditor...</h2>
    <p>
        ...and I agree *100%* ;)
    </p>
    <p>
        The thing about the <em>Ra-Ajax RichEdit control</em> is that it is 100% generic. This means that
        you can create <em>any WYSIWYG RichEditor</em> you want yourself out of it! In fact by utilizing
        this Ajax RichEditor you can create support for anything you wish yourself. And if you want to reuse it
        then you can create that functionality as a UserControl or even better a WebControl.
    </p>
    <p>
        Instead of pushing my view about how a WYSIWYG RichEditor for the web should look like we have chosen to
        give you the basic tools needed to create a WYSIWYG RichEditor just the way you want it to be.
    </p>
    <p>
        In our Wiki system we are actually using this RichEditor and we 
        have support for adding Images and all the rich formatting you can dream of. So instead of giving
        you a "fully fledged and way too fat" JavaScript file which tries to solve everything for you
        (hint; check the JS size of most WYSIWYG Editors available today) we have given you the
        equivalent of the Windows API RichEdit control which you (and others) can use for creating the
        Ajax RichEdit for your needs.
    </p>
    <p>
        This does not mean that we won't create a full WYSIWYG RichEdit later down the road which also will
        be an out of the box automagically fully working thing. But it means that we see the need for
        others to be able to create just the WYSIWYG RichEditor they themselves need instead of we
        creating something we guess you might need.
    </p>
    <h2>How does this work?</h2>
    <p>
        If you click the "Show Code" for this page you will see that the only logic we're actually doing
        here is roughly something like this;
    </p>
    <p>
        <em>richeditor.Selection = string.Format("&lt;strong&gt;{0}&lt;/strong&gt;", richeditor.Selection);</em>
    </p>
    <p>
        Now when you understand how and why the above line of code actually work you might get an "Aha experience".
        By creating your own interface to the RichEditor you can actually create any functionality you want
        including having panels for creating hyperlink which are shown through the most creative Ajax Effects
        you choose. And even have template driven editing with 15 pre-defined templates and no possibility
        of changing formatting for your users. Etc...
    </p>
    <p>
        For those of you who wants to implement one of these beasts on your own there is one very important
        tips in regards to doing so. When the RichEdit looses Focus you must store the current Range object
        since otherwise you won't be able to know which parts the user has selected when the user clicks
        buttons and such to change the formatting of the "currently selected text".
    </p>
    <p>
        Also I still despite of studying other RichEditors out there (DojoToolkit one among others) haven't had 
        my aha moment in regards to how to modify the cursor behavior which means that currently the RichEditor 
        can only modify text which is already selected! This is a missing feature of this Ajax Editor and all 
        suggestions is recieved with gratitude. Here's my <a href="mailto:thomas@ra-ajax.org">email</a> for those 
        with brilliant ideas about how to do just that. Among other things this means that you cannot just "insert"
        things, you must replace selected text only!
    </p>
    <h2>Security issues</h2>
    <p>
        Though to use this Ajax RichEditor you must turn off the RequestValidation. This can be done on
        the Page directive by setting the <em>ValidateRequest</em> to false. The reason why is because the
        semantics behind the RichEditor is that it will store the HTML parts of the RichEdit surface
        as a value in an input of type hidden. (HiddenField)
    </p>
    <p>
        This in turns will create Ajax Requests which contains HTML. Normally this is a security risk for
        application developers since this makes it possible for users to create content which they submit
        to e.g. your forums and such which can force other users into downloading JavaScript, instantiating
        ActiveX objects and so on. So be careful when using the Ajax RichEditor for the above reasons.
        Normally you would create a "white list" of tags which are legal in your business code layer.
        Though this is up to you (or some community member) to figure out for now :)
    </p>
    <h2>Other things to think about</h2>
    <p>
        Also the <em>contentEditable</em> attribute on a div element seems to make FireFox (3.x) go
        a little bit berserk when scrolling the page, especially with Page Up and Page Down. This is
        (I don't think so at least) not a problem with Ra-Ajax but more of a problem with FireFox.
    </p>
    <p>
        Also the Ajax RichEditor control contains its own JavaScript file which will add up to the 
        bandwidth usage of your application. This is a very small JavaScript file though, and my guess 
        is that it would be less than 5 KB when minified with e.g. JSMin or something similar. Meaning
        the total size of JavaScript would grow from 11.6 KB to about 16 KB.
    </p>
    <p>
        Though the good parts about that the RichEditor contains its own JavaScript file is that it
        is a very good starting point for you when developing your own Ajax Extension Controls on top
        of Ra-Ajax :)
    </p>
    <a href="Ajax-Timer.aspx">On to Ajax Timers</a>
</asp:Content>
