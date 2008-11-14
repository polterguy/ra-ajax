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
        Here are some testimonials from customers we've done projects for.
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

</asp:Content>

