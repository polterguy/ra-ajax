﻿<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Testimonials.aspx.cs" 
    Inherits="Testimonials" 
    Title="Testimonials" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Testimonials from customers</h1>
    <p>
        Here are some testimonials from customers we have done projects for.
    </p>
    <hr style="clear:both;margin-top:25px;margin-bottom:25px;" />
    <h2>Adrian Biffen - Canada</h2>
    <p>
        <img style="float:left;padding:15px;" src="media/ab.jpg" alt="Adrian Biffen" />
        <em>
            "Ra-Software delivered our online database component far above expectations: on time, 
            well under industry pricing and well above feature requirements. I recommend anyone who 
            needs Ajax work done to use Ra-Software - it sets a new standard for Ajax applications."
        </em>
        <pre>
Adrian Biffen
Senior Partner
<a href="http://CreateSimpleDatabase.com/">Masterise(TM) Mobile Data Systems</a>
        </pre>
    </p>
    <hr style="clear:both;margin-top:25px;margin-bottom:25px;" />
    <h2>Morten Andresen - Norway</h2>
    <p>
        <em>"When we hired Ra-Software to help us out with developing the next
        version of Imatis Portal we expected a lot from them. Even with
        our already extremely high hopes they were still able to deliver far above even
        those expectations!
    </p>
    <p>
        Ra-Software delivered
        stunningly high quality in addition to that the people from Ra-Software
        also served as a source of inspiration to the rest of the developers
        on the team. We have already placed an order
        for training for all our in-house developers which will be held by Ra-Software
        later this year. I would recommend Ra-Software to help you out with
        your software project any day of the week!"</em>
    </p>
    <p>
        <pre>
Morten Andresen
CEO @ Imatis AS
<a href="http://imatis.no/">Imatis AS</a>
        </pre>
    </p>
    <p>
        Below is a demonstration of what we created for Imatis using Ra-Ajax.
    </p>
    <object width="500" height="380">
        <param name="allowfullscreen" value="true" />
        <param name="allowscriptaccess" value="always" />
        <param name="movie" value="http://vimeo.com/moogaloop.swf?clip_id=5372246&amp;server=vimeo.com&amp;show_title=1&amp;show_byline=1&amp;show_portrait=0&amp;color=&amp;fullscreen=1" />
        <embed src="http://vimeo.com/moogaloop.swf?clip_id=5372246&amp;server=vimeo.com&amp;show_title=1&amp;show_byline=1&amp;show_portrait=0&amp;color=&amp;fullscreen=1" type="application/x-shockwave-flash" allowfullscreen="true" allowscriptaccess="always" width="500" height="380"></embed>
    </object>
    <hr style="clear:both;margin-top:25px;margin-bottom:25px;" />
    <h2>
        Dave Lazarow - South Africa
    </h2>
    <p>
        Dave was so happy about Ra-Software that he created a video himself showing off his application and
        how it uses Ra-Ajax. Note that this is a purely "one page application" which means that
        after you open the URL you never navigate away from the page at all. Even logging in and 
        out is done on the same page. Also he has integrated ASP.NET Charts with Ra-Ajax. So it
        is a pretty interesting use-case for Ra-Ajax.
    </p>
    <object width="500" height="380">
        <param name="allowfullscreen" value="true" />
        <param name="allowscriptaccess" value="always" />
        <param name="movie" value="http://vimeo.com/moogaloop.swf?clip_id=4241922&amp;server=vimeo.com&amp;show_title=1&amp;show_byline=1&amp;show_portrait=0&amp;color=&amp;fullscreen=1" />
        <embed src="http://vimeo.com/moogaloop.swf?clip_id=4241922&amp;server=vimeo.com&amp;show_title=1&amp;show_byline=1&amp;show_portrait=0&amp;color=&amp;fullscreen=1" type="application/x-shockwave-flash" allowfullscreen="true" allowscriptaccess="always" width="500" height="380">
        </embed>
    </object>
    <hr style="clear:both;margin-top:25px;margin-bottom:25px;" />
    <h2>Want to be up here?</h2>
    <p>
        If you want to be up here then please let us now by <a href="mailto:thomas@ra-ajax.org">sending us an email</a>
        with some statement you'd like to tell the world about us and also a link plus an image you want us to use.
    </p>
    <h2>Software Factory</h2>
    <p>
        Ra-Software AS is a <em>Software Factory</em> which means we can do your Ajax Development.
        If you have an Ajax Application you need to build we can do it for you. We can also offer training and
        professional help with Ra-Ajax. If you're interested in such an arrangement then please check out
        our <a href="SoftwareFactory.aspx">consulting offers</a> or check out 
        <a href="Author.aspx">our qualifications here</a>.
    </p>
</asp:Content>
