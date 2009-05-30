<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.ImageButton.ascx.cs" 
    Inherits="Docs_Controls_ImageButton" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<p>
    Click button to change image...
</p>
<ra:ImageButton 
    runat="server" 
    ImageUrl="media/Kariem.jpg" 
    AlternateText="Image"
    OnClick="btn_Click"
    ID="img" />


