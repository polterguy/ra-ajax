<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Accordion.aspx.cs" 
    Inherits="Samples.AjaxAccordion" 
    Title="Ra-Ajax Accordion Sample" %>

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

    <h1>Ra-Ajax Samples - Accordion</h1>
    <p>
        This is our <em>Ajax Accordion</em> sample. An Ajax Accordion is basically like a stack
        of Panels where only the contents of one of these Panels is visible at any given time. When 
        Outlook 2000 came out, it was well known for its Accordion Menu to the left in the main view. This
        is something similar.
    </p>
    <ext:Accordion 
        runat="server" 
        ID="acc" 
        CssClass="alphacube-acc" 
        AnimationSpeed="200">

        <ext:AccordionView 
            Caption="First Accordion" 
            runat="server" 
            ID="acc1" 
            CssClass="content">
            Here is an example of the Ajax Accordion. Now the thing to pay particular attention to,
            in regards to the Ra-Ajax Accordion, is that it will actually let you define the
            size of individual AccordionViews as you wish. This means that the size of
            the Accordion you are currently looking at, which is quite large, will not affect the
            size of the next Accordions. You can see that yourself, if you activate one of the other
            Accordions, the total surface of the Accordion viewport will actually shrink.
        </ext:AccordionView>

        <ext:AccordionView 
            Caption="Second Accordion" 
            runat="server" 
            ID="acc2" 
            CssClass="content">
            Another thing to note about the Ra-Ajax Accordion, is that when you have two accordions
            which are of the same height, activating the second will not make the Accordion ViewPort
            area "jump" as in many other Ajax Accordion implementations.
        </ext:AccordionView>

        <ext:AccordionView 
            Caption="Third Accordion" 
            runat="server" 
            ID="acc3" 
            CssClass="content">
            <p>
                Did you see the lack of jumping?
            </p>
            <p>
                This is being done with some nifty JS/DOM techniques that together makes the rendering
                and reading of the contents of a Ra-Ajax Accordion hopefully a very pleasant experience 
                to the eye.
            </p>
        </ext:AccordionView>

        <ext:AccordionView 
            Caption="Fourth Accordion" 
            runat="server" 
            ID="AccordionView1" 
            CssClass="content">
            <p>
                Now the Ra-Ajax Accordion is also an <a href="Ajax-Panel.aspx">Ajax Panel</a> which
                means that you can embed other complex Ajax Controls like for instance an Ajax Calendar
                within it with no problems.
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
                    ID="lbl" 
                    Text="Watch me as you change the date" />
            </p>
        </ext:AccordionView>

    </ext:Accordion>
    <h2>Ajax and Performance</h2>
    <p>
        A good <a href="http://ra-ajax.org/managed-ajax-a-new-approach-to-ajax.blog" title="Managed Ajax">Managed Ajax library</a> 
        should not create a large bowl of JavaScript for you which will stall the user experience, since the user 
        will have to wait until the JavaScript has been downloaded before he can start reading the content and 
        interacting with your page.
    </p>
    <p>
        A very good tool for profiling your Ajax Library is 
        <a href="http://developer.yahoo.com/yslow/">YSlow</a> which is created and maintained by (among others)
        <a href="http://developer.yahoo.net/blog/archives/2007/08/yslow-podcast-screencast.html">Jeremy Zawodny</a>
        at <a href="http://www.yahoo.com">Yahoo!</a>. With YSlow you can profile and see how
        your webpages are doing. If you install YSlow you will see that this page scores roughly
        about <strong>74 or "C" in YSlow</strong>. A score of 74 is very good for a page with so much Ajax
        interaction like this page.
    </p>
    <p>
        The main reason why this page scores that high, even though it has an <em>Ajax Calendar, Ajax Label and
        an Ajax Accordion</em>, is because Ra-Ajax does not consist of more than a handful of JavaScript
        files. In fact this page only has *two* JavaScript files, which combined are <strong>less than 12KB in
        total of JavaScript</strong>.
    </p>
    <h2>Generalize, generalize and generalize</h2>
    <p>
        If you want to create an Ajax Library with <em>more than 18 Ajax Controls, like Ra-Ajax has,</em> then
        you must be able to <em>generalize as much as possible</em>. In Ra-Ajax we have done that by first of all
        not using a general purpose JavaScript framework as our foundation. Secondly, we have also created our 
        Control.js file in such a way that most of our Ajax Controls (including this Ajax Accordion) don't need their own
        "custom JavaScript" files.
    </p>
    <p>
        In fact most of our Ajax Controls are sharing the same JavaScript file since it is written so that all
        Ajax Controls (or at least most of them) should be able to share the same JavaScript file.
    </p>
    <p>
        Now if you start out with a General Purpose JavaScript framework you will start out with an "initial penalty"
        depending on which JavaScript Framework you choose of some specific KB which will come in addition to
        your custom JavaScript logic. I have created a table for you below which shows this initial penalty, 
        though you can probably add to that penalty quite a substantial bit, since you will also need your
        own custom JavaScript. This is true if you are creating your own Ajax Library on top of these JavaScript
        frameworks and also true if you consume them directly as standalone Frameworks.
    </p>
    <h2>Sizes of well known JavaScript/Ajax Frameworks</h2>
    <table style="width:500px;">
        <tr>
            <th>JS library</th>
            <th>Size</th>
        </tr>
        <tr>
            <td><a href="http://www.prototypejs.org/">Prototype.js</a></td>
            <td>121 KB (unpacked, not minified)</td>
        </tr>
        <tr>
            <td><a href="http://jquery.com/">jQuery</a></td>
            <td>97 KB (unpacked, not minified)</td>
        </tr>
        <tr>
            <td><a href="http://mootools.net/">MooTools</a></td>
            <td>62 KB (minified, all packets)</td>
        </tr>
        <tr>
            <td><a href="http://dojotoolkit.org/">DojoToolkit</a></td>
            <td>24 KB (minified and gzipped)</td>
        </tr>
    </table>
    <p>
        When you minify a JavaScript library it normally becomes half its original size, when you gzip it again,
        it usually becomes half the size of that. Ra-Ajax "core" JavaScript files are 32 KB
        before minification and before gzipping. After minification but before gzipping 
        <strong>Ra-Ajax consists of in total 11.6 KB JavaScript</strong>. In fact that is such a ridiculously
        low number that the whole point about gzipping JavaScript libraries makes no sense at all for 
        Ra-Ajax.
    </p>
    <p>
        So even though all of the above JavaScript Ajax Frameworks are brilliantly written by some of the best
        JavaScript developers in the world, and tuned down to <em>the last byte of JavaScript</em> they
        will still make you start out with an initial penalty when creating your Rich Internet Applications.
    </p>
    <p>
        To be fair, you can probably add up another 5 KB of JavaScript if you use the <em>Ajax RichEditor</em>
        and the <em>Ajax Timer</em> in the extension projects. But then we are talking about a complete
        Ajax Framework with 18 Ajax Controls which almost completely abstracts away the entire concept 
        of JavaScript from developers and makes it possible for them to exclusively focus on developing
        in a safe, compiled and well known language of their choice on the server.
    </p>
    <h2>Is Ra-Ajax a RIA Framework?</h2>
    <p>
        Since Ra-Ajax has such extremely small amounts of JavaScript it is probably not even fair to call
        it a RIA Framework. While most other Ajax Libraries and Frameworks claim to be RIA Frameworks,
        Ra-Ajax has such small amounts of JavaScript that you could probably just as easily create also 
        normal websites with it where load speed, responsiveness and all that matters to such an extent that
        you normally wouldn't even consider using an Ajax Framework at all. Ra-Ajax could easily be used in
        the front ends of your websites too. In fact every single page on this domain is including the entire
        JavaScript parts of the Ra-Ajax core within themselves.
    </p>
    <p>
        Suggestions here might include front ends of CMS systems, forum sites, your personal blogging system
        and so on. And due to the LGPL nature of Ra-Ajax you can do this just as you wish yourself without
        wondering about license costs and such. The LGPL license gives you the right to use Ra-Ajax in
        closed source/proprietary projects just as you wish.
    </p>
    <p>
        Ra-Ajax is of course also a RIA Framework. Though it is also something <strong>more</strong> than
        just another RIA Framework.
    </p>
    <h2>Ajax Controls in Ra-Ajax</h2>
    <p>
        To put the size in context I have created a list of all the Ajax Controls in Ra-Ajax below. Note 
        that this is the list of Ajax Controls in Ra-Ajax as of the 26th of August 2008 and will probably
        grow substantially over time ;)
    </p>
    <ul class="bulList">
        <li>Accordion</li>
        <li>Calendar</li>
        <li>InPlaceEdit</li>
        <li>RichEdit</li>
        <li>TabControl</li>
        <li>Timer</li>
        <li>Button</li>
        <li>CheckBox</li>
        <li>SelectList</li>
        <li>HiddenField</li>
        <li>Image</li>
        <li>ImageButton</li>
        <li>Label</li>
        <li>LinkButton</li>
        <li>Panel</li>
        <li>RadioButton</li>
        <li>TextArea</li>
        <li>TextBox</li>
        <li>Several DHTML Ajax Effects</li>
    </ul>
    <h2>Here Comes the Magic</h2>
    <p>
        The trained web developer will soon realize that the above list gives him all the "base controls" he
        needs to create more advanced Ajax Controls himself. And this is 99% of the whole point in
        Ra-Ajax. By utilizing the "generic" Ajax controls in the above list you can easily, through composition,
        create your own complex Ajax Controls. And in fact, the sample you are currently looking at, is a full
        demonstration of that fact.
    </p>
    <p>
        The Ajax Accordion control is itself composed of other Ajax Controls like the 
        <em>Ajax Panel, Ajax LinkButton and Ajax Effects</em>. This makes it possible to create
        a lot of Ajax Controls and still have no more than 11.6 KB of JavaScript. In fact, of the above
        Ajax Controls, the only ones which need their own "custom JavaScript" files are the Ajax Timer and
        the Ajax RichEdit. Even our Ajax InPlaceEdit is created utilizing the LinkButton and the Label and 
        nothing else.
    </p>
    <h2>We need you!</h2>
    <p>
        We are very interested in getting help to extend and make Ra-Ajax more useful. If you have created an
        Ajax Extension control on top of Ra-Ajax then I am very interested in hearing from you. Maybe even
        host it as a special download here at this site or create a sample for it to host here. If you do 
        create an Extension Control then please <a href="mailto:thomas@ra-ajax.org">send me an email</a> about
        it or post in our <a href="http://ra-ajax.org/Forums/Forums.aspx">forums</a> about it.
    </p>
    <p>
        A good place to start learning how to create your own Ajax Extension Controls, is the source for
        the Ajax Accordion, Ajax LinkButton, Ajax TabControl, Ajax Calendar or any of the other Ajax Controls
        which are purely built by using the existing "building blocks" to form more complex functionality.
    </p>
    <p>
        Oh yeah, and if you are a "JavaScript Guru" then feel free to create Ajax Controls which actually 
        needs their own JavaScript file. Though 99% of the time you need a new Ajax Control I believe that
        you will not need to resort to JavaScript ;)
    </p>
    <a href="Ajax-Calendar.aspx">On to Ajax Calendar</a>
</asp:Content>
