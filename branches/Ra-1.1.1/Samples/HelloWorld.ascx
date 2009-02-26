<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="HelloWorld.ascx.cs" 
    Inherits="Samples.UserControls.HelloWorld" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<ra:Button 
    runat="server" 
    ID="submit" 
    Text="Say Hello World" 
    OnClick="submit_Click" />
<ra:Label runat="server" ID="lblResult" />
