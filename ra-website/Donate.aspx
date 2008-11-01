<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Donate.aspx.cs" 
    Inherits="Donate" 
    Title="Donate" %>

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
    Runat="Server">

    <h1>Donate to Ra-Ajax</h1>
    <p>
        All though Ra-Software AS is a company, we have no means to earn money on maintaining Ra-Ajax. You can help us
        to work more on Ra-Ajax by donating whatever amount you feel you can spare. Click the PayPal
        button below to go to PayPal to donate to Ra-Software and thereby the project Ra-Ajax.
    </p>
    <p>
        <a href="https://www.paypal.com/_donations/?cmd=_s-xclick&hosted_button_id=838418">
            <img src="media/donate-paypal.gif" alt="Donate to Ra-Ajax" />
        </a>
        <br />
        (leads you to PayPal)
    </p>
    <p>
        The more people donating to Ra-Software and Ra-Ajax, the more we can extend and work on maintaining Ra-Ajax and
        give you free support in our forums.
    </p>

</asp:Content>

