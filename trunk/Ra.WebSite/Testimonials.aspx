<%@ Page 
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
        Here are some testimonials from customers we've done projects for and users of Ra-Ajax.
    </p>
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
    <p style="clear:both;">
        If you want to be up here then please let us now by <a href="mailto:thomas@ra-ajax.org">sending us an email</a>
        with some statement you'd like to tell the world about us and also a link plus an image you want us to use.
    </p>
    <h2>Software Factory</h2>
    <p>
        Ra-Software is a Norwegian based <em>Software Factory</em> which means we can do your Ajax Development.
        If you have an Ajax Application you need to build we can do it for you. We can also offer training and
        professional help with Ra-Ajax. If you're interested in such an arrangement then please send 
        <a href="mailto:thomas@ra-ajax.org">Thomas an email</a> and tell us about your problem.
    </p>
    <p>
        <a href="Author.aspx">Read about our qualifications here</a>.
    </p>
</asp:Content>

