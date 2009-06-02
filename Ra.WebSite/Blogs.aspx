<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Blogs.aspx.cs" 
    Inherits="RaWebsite.Blogs" 
    Title="Ra-Ajax Blogs" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Bloggers at Ra-Ajax</h1>
    <a href="http://kariem.net" class="blogLinks" target="_blank">
        <img src="media/Kariem.jpg" alt="Kariem Ali" style="float:left;padding:10px;" />
        <span style="display:block;font-size:24px;color:Black;">Kariem Ali</span>
        <span>
            Kariem lives in Alexandria, Egypt and is a Senior Software Architect in Ra-Software - the company
            that creates and maintains Ra-Ajax. Kariem is one of the core developers and architects behind Ra-Ajax.
            He blogs about Ra-Ajax, development in general and other things he is interested in.
        </span>
    </a>
    <a href="http://ra-ajax.org/thomas.blogger" class="blogLinks">
        <img src="media/Thomas-Hansen.jpg" alt="Thomas Hansen" style="clear:right;float:right;padding:10px;" />
        <span style="display:block;font-size:24px;color:Black;">Thomas Hansen</span>
        <span>
            Thomas Hansen is the founder and owner of Ra-Software. Thomas is also one of the core
            developers and architects behind Ra-Ajax. Thomas blogs about politics, religion, (Ra-)Ajax, 
            development and all other things of immediate interest. Thomas is the copyright holder 
            of Ra-Ajax.
        </span>
    </a>
    <a href="http://ra-ajax.org/Rick.blogger" class="blogLinks">
        <img src="media/Rick-Gibson.jpg" alt="Rick Gibson" style="float:left;padding:10px;" />
        <span style="display:block;font-size:24px;color:Black;">Rick Gibson</span>
        <span>
            Rick lives in Tehama County about twenty miles from Chico, CA. Rick's dog is named
            John Lennon which should give you an idea of Rick's interests. He blogs about sharing, 
            Ra-Ajax and other things he is interested in.
        </span>
    </a>
    <p style="text-align:center;">
        <a href="Login.aspx">Login</a>
    </p>
</asp:Content>

