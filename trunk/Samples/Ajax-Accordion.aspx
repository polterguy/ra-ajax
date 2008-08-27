<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Accordion.aspx.cs" 
    Inherits="AjaxAccordion" 
    Title="Ajax Accordion Sample" %>

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

    <h1>Ajax Accordion Sample</h1>
    <p>
        This is our <em>Ajax Accordion</em> sample. An Ajax Accordion can be thought about like a stack
        of Panels where only the contents of one of those may be visible at any given time. When 
        Outlook 2000 came it it was well known for its "Accordion Control" to the left in the main
        view.
    </p>
    <ext:Accordion 
        runat="server" 
        ID="acc" 
        CssClass="accordion-control" 
        AnimationSpeed="0.2"
        style="width:75%;">

        <ext:AccordionView 
            Caption="Ajax Accordion 1" 
            runat="server" 
            ID="acc1" 
            CssClass="content">
            Here is an example of an Ajax Accordion. Now the thing to pay particular notice of
            in regards to the Ra-Ajax Accordion is that it will actually let you define the
            size of individual AccordionViews as you wish yourself. This means that the size of
            the Accordion you are currently reading in which is quite large will not affect the
            size of the next accordions which you can see that when you activate one of the other
            Accordions then the total surface of the Accordion viewport will actually shrink.
        </ext:AccordionView>

        <ext:AccordionView 
            Caption="Ajax Accordion 2" 
            runat="server" 
            ID="acc2" 
            CssClass="content">
            Another thing to notice about the Ra-Ajax Accordion is that when you have two accordions
            which are of the same height, then activating the second will not make the Accordion ViewPort
            area "jump" as in many other Ajax Accordion implementations.
        </ext:AccordionView>

        <ext:AccordionView 
            Caption="Third Ajax Accordion" 
            runat="server" 
            ID="acc3" 
            CssClass="content">
            Did you see the (lack of) jumping...?
            <br />
            This is being done with some nifty JS/DOM techniques that together makes the rendering
            and reading of the contents of a Ra-Ajax Accordion hopefully a very pleasent experience 
            on the eye.
        </ext:AccordionView>

        <ext:AccordionView 
            Caption="Third Ajax Accordion" 
            runat="server" 
            ID="AccordionView1" 
            CssClass="content">
            Now the Ra-Ajax Accordion is also an <a href="Ajax-Panel.aspx">Ajax Panel</a> which
            means that you can embed other complex Ajax Controls like for instance an Ajax Calendar
            within it with no problems.
            <br />
            <ext:Calendar 
                runat="server" 
                ID="calTab" 
                CssClass="calendar" 
                OnSelectedValueChanged="calTab_SelectedValueChanged"
                Value="2008.07.20 23:54" />
            <br />
            <ra:Label 
                runat="server" 
                style="color:#999;font-style:italic;"
                ID="lbl" Text="Watch me as you change the date" />
        </ext:AccordionView>

    </ext:Accordion>
    <br />
    <h2>Ajax and Performance</h2>
    <p>
        A good server-side binded Ajax Library should not create a large bowl of JavaScript for you
        which will stall the user experience since the user will have to wait until the JavaScript have 
        been downloaded before he can start reading the content and interacting with your page.
    </p>
    <p>
        A very good tool for profiling your Ajax Library is 
        <a href="http://developer.yahoo.com/yslow/">YSlow</a> which is created and maintained by (among others)
        <a href="http://developer.yahoo.net/blog/archives/2007/08/yslow-podcast-screencast.html">Jeremy Zawodny</a>
        at <a href="http://www.yahoo.com">Yahoo</a>. With YSlow you can profile and check for yourself how
        your webpages are doing. If you install YSlow for instance you will see that this page scores roughly
        about <strong>84 or "B" in YSlow</strong>. A score of 84 is very good for a page with so much Ajax
        interaction as this page have.
    </p>
    <p>
        A lot of the reason why this page scores that high even though it has an <em>Ajax Calendar, Ajax Label and
        an Ajax Accordion</em> in it is because of that Ra-Ajax does not consist of more than a handful of JavaScript
        files. In fact this page only have *two* JavaScript files which combined are <strong>less than 12KB in
        total of JavaScript</strong>.
    </p>
    <br />
    <h2>Generalize, generalize and generalize</h2>
    <p>
        If you want to create an Ajax Library with <em>more than 18 Ajax Controls within as Ra-Ajax consists of</em> then
        you must be able to <em>generalize as much as possible</em>. In Ra-Ajax we have done this by first of all
        not using a general purpose JavaScript framework as our foundation. Secondly we have also created our 
        Control.js file in such a way that most of our Ajax Controls (including this Ajax Accordion) don't need their own
        "custom JavaScript" file.
    </p>
    <p>
        In fact most of our Ajax Controls are sharing the same JavaScript file since it is written so that all
        Ajax Controls (or at least most of them) should be able to share the same JavaScript file.
    </p>
    <p>
        Now if you start out with a General Purpose JavaScript framework you will start out with an "initial penalty"
        depending upon which JavaScript Framework you choose of some specific KB which will come in addition to
        your custom JavaScript logic. I have created a table for you below which shows this initial penalty, 
        though you can probably add to that penalty with quite a substantial bit since you will also need your
        own custom JavaScript. This is true if you are creating your own Ajax Library on top of these JavaScript
        frameworks and also true if you consume them directly as standalone Frameworks.
    </p>
    <br />
    <h2>Size of well known JavaScript Ajax Frameworks</h2>
    <table>
        <tr>
            <th>JavaScript library</th>
            <th>Size of library</th>
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
    <br />
    <p>
        When you minify a JavaScript library it normally becomes half the size, when you gzip it again
        it normally becomes half the size of that again. Ra-Ajax "core" JavaScript files are 32 KB
        before minification and before gzipping. After minification but before gzipping 
        <strong>Ra-Ajax consists of in total 11.6 KB JavaScript</strong>. In fact that is such a ridicilously
        low number that the whole point about gzipping JavaScript libraries makes no sense at all for 
        Ra-Ajax.
    </p>
    <p>
        So even though all of the above JavaScript Ajax Frameworks are brilliantly written by some of the best
        JavaScript developers in the world, and tuned down to <em>the last byte of JavaScript</em> they
        will still make you start out with an initial penalty when creating your Rich Internet Applications.
    </p>
    <p>
        To be fair you can probably add up another 5 KB of JavaScript if you use the <em>Ajax RichEditor</em>
        and the <em>Ajax Timer</em> in the extension projects. But then we are talking about a complete
        Ajax Framework with 18 Ajax Controls within which almost completely abstracts away the entire concept 
        of JavaScript for developers and makes it possible for them to exclusively focus on developing
        in a safe, compiled and well known language of their choice on the server.
    </p>
    <br />
    <h2>Is Ra-Ajax a RIA Framework?</h2>
    <p>
        Since Ra-Ajax has such extremely small amounts of JavaScript it is probably not even fair to call
        it a RIA Framework. While most other Ajax Libraries and Frameworks claims to be RIA Frameworks
        Ra-Ajax has such small amount of JavaScript that you could probably just as easily create also 
        normal websites with it where load speed, responsiveness and all that matters to such an extent that
        you normally wouldn't even consider using an Ajax Framework at all.
    </p>
    <p>
        Suggestions here might include front ends of CMS systems, forum sites, your personal blogging system
        and so on. And due to the LGPL nature of Ra-Ajax you can do this just as you wish yourself without
        wondering about license costs and such. The LGPL license gives you the right to use Ra-Ajax in
        closed source/proprietary projects just as you wish :)
    </p>
    <br />
    <h2>Ajax Controls in Ra-Ajax</h2>
    <p>
        To put the size in context I have created a list of all the Ajax Controls in Ra-Ajax below. Note 
        that this is the list of Ajax Controls in Ra-Ajax as of the 26th of August 2008 and will probably
        grow substantially over time ;)
    </p>
    <ul>
        <li>Accordion</li>
        <li>Calendar</li>
        <li>InPlaceEdit</li>
        <li>RichEdit</li>
        <li>TabControl</li>
        <li>Timer</li>
        <li>Button</li>
        <li>CheckBox</li>
        <li>DropDownList</li>
        <li>HiddenField</li>
        <li>Image</li>
        <li>ImageButton</li>
        <li>Label</li>
        <li>LinkButton</li>
        <li>Panel</li>
        <li>RadioButton</li>
        <li>TextArea</li>
        <li>TextBox</li>
        <li>++ a whole bunch of DHTML Ajax Effects...</li>
    </ul>
    <br />
    <h2>Here comes the magic ;)</h2>
    <p>
        The trained (web) developer will soon realize that the above list gives him all the "base controls" he
        needs himself to create more advanced Ajax Controls himself. And this is 99% of the whole point in
        Ra-Ajax. By utilizing the "generic" Ajax controls in the above list you can easily through composition
        create your own far more complex Ajax Controls. And in fact the sample you are currently looking at
        demonstrates that fact to the full.
    </p>
    <p>
        The Ajax Accordion control is itself composed of other already existing Ajax Controls like the 
        <em>Ajax Panel, Ajax LinkButton and Ajax Effects</em>. This makes it possible to create actually
        billions of Ajax Controls and still have no more than 11.6 KB of JavaScript. In fact of the above
        Ajax Controls the only ones which needs their own "custom JavaScript" file are the Ajax Timer and
        the Ajax RichEdit. Even our Ajax InPlaceEdit is created utilizing the LinkButton and the Label and 
        nothing else...
    </p>
    <br />
    <h2>We need you!</h2>
    <p>
        We are very interested in getting help to extend and make Ra-Ajax more useful. If you have created an
        Ajax Extension control on top of Ra-Ajax then I am very interested in hearing from you. Maybe even
        host it as a special download here at this site or create a sample for it to host here. If you do 
        create an Extension Control then please <a href="mailto:thomas@ra-ajax.org">send me an email</a> about
        it or post in our <a href="http://ra-ajax.org/Forums/Forums.aspx">forums</a> about it.
    </p>
    <p>
        Good places to start for creating your own Ajax Extension Controls are to look at the source for
        the Ajax Accordion, Ajax LinkButton, Ajax TabControl, Ajax Calendar or any of the other Ajax Controls
        which are purely built by using the existing "building blocks" to form more complex functionality.
    </p>
    <p>
        Ohh yeah, and if you are a "JavaScript Guru" then feel free to create Ajax Controls which actually 
        needs their own JavaScript file. Though 99% of the time you need a new Ajax Control I believe that
        you will not need to resort to JavaScript ;)
    </p>
    <a href="Ajax-Calendar.aspx">On to Ajax Calendar</a>
</asp:Content>
