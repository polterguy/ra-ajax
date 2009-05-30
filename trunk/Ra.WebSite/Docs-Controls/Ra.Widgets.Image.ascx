<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Widgets.Image.ascx.cs" 
    Inherits="Docs_Controls_Image" %>

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


