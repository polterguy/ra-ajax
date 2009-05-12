<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.Image.ascx.cs" 
    Inherits="Docs_Controls_Image" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ra:Button 
    runat="server" 
    ID="btn" 
    Text="Change Image" 
    OnClick="btn_Click" />
<br />
<ra:Image 
    runat="server" 
    ImageUrl="media/Kariem.jpg" 
    AlternateText="Image"
    ID="img" />


