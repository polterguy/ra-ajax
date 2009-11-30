<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Widgets.RichEdit.ascx.cs" 
    Inherits="Docs_Controls_RichEdit" %>

<ra:Label 
    runat="server" 
    ID="lbl" 
    style="display:block;padding:5px;" />
<ra:RichEdit
    ID="editor" 
    CssClass="rich-edit"
    Keys="s,g"
    OnCtrlKey="editor_CtrlKey"
    runat="server" />