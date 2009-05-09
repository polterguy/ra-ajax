<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.TabControl.ascx.cs" 
    Inherits="Docs_Controls_TabControl" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<ext:TabControl runat="server" ID="tab" OnActiveTabViewChanged="TabChaged">

    <ext:TabView runat="server" ID="tab1" Caption="First tab">
    
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
        Etiam eu metus nisi, et venenatis eros. Nunc imperdiet 
        nulla ut diam sagittis congue. Pellentesque vel dui est, 
        sit amet imperdiet sapien. Nullam a tellus sapien. Ut 
        dignissim, risus sit amet vestibulum gravida, mauris odio 
        malesuada nisl, eget fermentum ligula leo at nunc. Cras 
        a justo est, condimentum adipiscing metus. Quisque eu arcu 
        felis, sit amet auctor sapien. 

    </ext:TabView>

    <ext:TabView runat="server" ID="tab2" Caption="Second tab">

        <ext:Calendar 
            runat="server" 
            style="width:200px;"
            ID="cal" />

    </ext:TabView>

    <ext:TabView 
        runat="server" 
        ID="tab3" 
        Enabled="false"
        Caption="Disabled tab">

        Disabled until you chose the second tab.

    </ext:TabView>

</ext:TabControl>