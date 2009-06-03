<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Ra.Extensions.Widgets.TabControl.ascx.cs" 
    Inherits="Docs_Controls_TabControl" %>

<ra:TabControl runat="server" ID="tab" OnActiveTabViewChanged="TabChaged">

    <ra:TabView runat="server" ID="tab1" Caption="First tab">
    
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
        Etiam eu metus nisi, et venenatis eros. Nunc imperdiet 
        nulla ut diam sagittis congue. Pellentesque vel dui est, 
        sit amet imperdiet sapien. Nullam a tellus sapien. Ut 
        dignissim, risus sit amet vestibulum gravida, mauris odio 
        malesuada nisl, eget fermentum ligula leo at nunc. Cras 
        a justo est, condimentum adipiscing metus. Quisque eu arcu 
        felis, sit amet auctor sapien. 

    </ra:TabView>

    <ra:TabView runat="server" ID="tab2" Caption="Second tab">

        <ra:Calendar 
            runat="server" 
            style="width:200px;"
            ID="cal" />

    </ra:TabView>

    <ra:TabView 
        runat="server" 
        ID="tab3" 
        Enabled="false"
        Caption="Disabled tab">

        Disabled until you chose the second tab.

    </ra:TabView>

</ra:TabControl>