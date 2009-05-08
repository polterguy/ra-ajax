<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Accordion.ascx.cs" 
    Inherits="Docs_Controls_Accordion" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ra:Label 
    runat="server" 
    Text="Changes..."
    ID="lbl" />

<ext:Accordion 
    runat="server" 
    OnActiveAccordionViewChanged="acc_ActiveAccordionViewChanged"
    ID="acc">

    <ext:AccordionView 
        runat="server" 
        ID="acc1" 
        Caption="First accordion">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
        Etiam eu metus nisi, et venenatis eros. Nunc imperdiet 
        nulla ut diam sagittis congue. Pellentesque vel dui est, 
        sit amet imperdiet sapien. Nullam a tellus sapien. Ut 
        dignissim, risus sit amet vestibulum gravida, mauris odio 
        malesuada nisl, eget fermentum ligula leo at nunc. Cras 
        a justo est, condimentum adipiscing metus. Quisque eu arcu 
        felis, sit amet auctor sapien. 
    </ext:AccordionView>

    <ext:AccordionView 
        runat="server" 
        ID="acc2" 
        Caption="Second accordion">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
        Etiam eu metus nisi, et venenatis eros. Nunc imperdiet 
        nulla ut diam sagittis congue. Pellentesque vel dui est, 
        sit amet imperdiet sapien. Nullam a tellus sapien. Ut 
        dignissim, risus sit amet vestibulum gravida, mauris odio 
        malesuada nisl, eget fermentum ligula leo at nunc. Cras a 
        justo est, condimentum adipiscing metus. Quisque eu arcu 
        felis, sit amet auctor sapien. Nunc ut magna nisl. Proin 
        auctor ullamcorper interdum. Suspendisse varius lorem ut 
        urna bibendum semper. Vestibulum faucibus luctus felis 
        vitae dapibus. Quisque et augue mauris, vel scelerisque 
        odio. Vivamus feugiat accumsan fermentum. Phasellus 
        vehicula lacinia tristique. Suspendisse a felis at nulla 
        convallis vehicula consequat eget ipsum. Praesent nec 
        quam ac felis mattis facilisis ac auctor massa. 
    </ext:AccordionView>

    <ext:AccordionView 
        runat="server" 
        ID="acc3" 
        Caption="Third accordion">
        <ext:Calendar 
            runat="server" 
            style="width:200px;"
            ID="cal" />
        <p>
            Here's an example of embedding controls within 
            an AccordionView.
        </p>
    </ext:AccordionView>

</ext:Accordion>