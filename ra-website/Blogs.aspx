<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Blogs.aspx.cs" 
    Inherits="RaWebsite.Blogs" 
    Title="Ra-Ajax Blogs" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Ra-Ajax Blogs</h1>
    <a href="http://ra-ajax.org/Kariem.blogger" class="blogLinks">
        <img src="media/Kariem.jpg" alt="Kariem Ali" style="float:left;padding:10px;" />
        <span style="display:block;font-size:24px;color:Black;">Kariem Ali</span>
        <span>
            Kariem's blog. Kariem lives in Alexandria in Egypt and is an employee in Ra-Software - the company
            that creates and maintains Ra-Ajax. Kariem is one of the core developers and architects behind Ra-Ajax.
            Kariem blogs about Ra-Ajax, development in general and other things he is interested in.
        </span>
    </a>
    <a href="http://ra-ajax.org/thomas.blogger" class="blogLinks">
        <img src="media/Thomas-Hansen.jpg" alt="Thomas Hansen" style="clear:right;float:right;padding:10px;" />
        <span style="display:block;font-size:24px;color:Black;">Thomas Hansen</span>
        <span>
            Thomas Hansen is the founder and owner of Ra-Software. Thomas is also one of the core
            developers and arcitects behind Ra-Ajax. Thomas blogs about politics, (Ra-)Ajax, development
            and all other things of immediate interest.
        </span>
    </a>
    <a href="http://ra-ajax.org/Rick.blogger" class="blogLinks">
        <img src="media/Rick-Gibson.jpg" alt="Rick Gibson" style="float:left;padding:10px;" />
        <span style="display:block;font-size:24px;color:Black;">Rick Gibson</span>
        <span>
            Rick lives in Tehama County about twenty miles from Chico, CA. Rick is a community member, user and
            supporter of Ra-Ajax. Rick blogs about Ra-Ajax and other things he is interested in.
        </span>
    </a>
</asp:Content>

