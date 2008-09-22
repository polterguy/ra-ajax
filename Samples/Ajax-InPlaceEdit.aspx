<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-InPlaceEdit.aspx.cs" 
    Inherits="AjaxInPlaceEdit" 
    Title="Ra-Ajax InPlaceEdit Sample" %>

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

    <h1>Ra Ajax Samples - InPlaceEdit</h1>
    <p>
        The <em>Ajax InPlaceEdit</em> Control was originally conceived and made famous by 
        <a href="http://mir.aculo.us/">Thomas Fuchs</a> the original author behind
        <a href="http://script.aculo.us/">ScriptAculous</a>. The Ra-Ajax InPlaceEdit is created without using
        any custom JavaScript and is actually a very good starting point for your own Custom Ajax Controls.
    </p>

    <ext:InPlaceEdit 
        runat="server" 
        ID="inpl" 
        OnTextChanged="inpl_TextChanged" 
        Text="Click to edit" />

    <p>
        <ra:Label 
            runat="server" 
            ID="lbl" 
            Text="Watch me as focus gets moved away from the InPlaceEdit"
            CssClass="updateLbl" />
    </p>
    <p>
        Try to click the above InPlaceEdit and then when you're finished writing the new text just click
        TAB or with the mouse move the focus away from the InPlaceEdit above.
    </p>
    <p>
        The InPlaceEdit Control can be thought about as an editable label. Many times you have data which
        you first of all need to display as read-only. But you also wish that the user should easily be able
        to quickly edit that text data. Imagine for instance a text field inside a GridView or Data Repeater
        or something similar which also is supposed to be inline editable. For such circumstances the InPlaceEdit
        is perfect.
    </p>
    <h2>Why give away innovation for free?</h2>
    <p>
        I don't expect everyone to understand this, but I think of IT the same way many others think
        of <em>education, books and knowledge</em>. I think that in order to continue what Gutenberg
        started some 500 years ago, we must acknowledge that the access to IT and information
        is a <em>basic human right</em>! I believe that the only way we can achieve this, is by
        <em>commoditizing IT</em> which to some extent is similar to what happened with electricity
        more than 100 years ago. IT today is roughly where electricity was 100 years ago. Only the
        richest people on the planet could afford it and it was very expensive.
    </p>
    <p>
        Everyone in the western hemisphere have more or less access to IT today, but that is first of
        all only 30% of the world population. Secondly, and more importantly, very few have access
        to, or know how to search and find, the information they really need.
    </p>
    <p>
        YouTube, Google, Wikipedia, OLPC and others are doing a great job in regards to "helping out"
        but in order to make this happen we must all participate. And the only thing I am really good at is
        creating great Ajax Libraries!
    </p>
    <p>
        So I make great Ajax Libraries and I give them away to the entire world hoping that I will
        be able to make a living out of consuming those Ajax Libraries myself as a consultant in a 
        company maybe owned by you!
    </p>
    <p>
        If you want to hire me (or my friend Kariem) then take a look at 
        <a href="http://ra-ajax.org/Author.aspx">this page</a>. If you have a problem when using Ra-Ajax
        then maybe the solution is to hire one or both of us.
    </p>
    <h2>Open Innovation, Open Web and Open Source</h2>
    <p>
        These are our prime tools to make sure that IT becomes a <em>commodity</em>. And by giving away Ra-Ajax to 
        the world for free, I am hopefully making my contribution in regards to all three of the 
        points above. I love Open Source, Open Web and Open Innovation. And I think that currently
        Ajax from a conceptual point of view is that <em>one soldier at the very first line of soldiers</em>
        in the battle for <em>commoditization of information and information technology</em>.
    </p>
    <p>
        Now I might have scared some of you off to such an extent that you may never be able to hire
        me or Kariem. I just must say that although I have strong ideas about the Open Web, Open Source
        and Open Innovation, I am still able to work on an hourly basis for your cause unless it directly
        goes in the exact opposite direction of mine. I too realize that me and Kariem must have bread
        and butter to survive. I hope this clarified things for you so that you don't look
        at us as "fanatics". :)
    </p>
    <p>
        I also hope that you would be willing to also purchase our "commercial products" when we come out
        with them. The first will probably be a Wiki System which we're currently working on at Ra-Wiki.org.
    </p>
    <a href="Ajax-RichEdit.aspx">On to Ajax RichEdit</a>
</asp:Content>
