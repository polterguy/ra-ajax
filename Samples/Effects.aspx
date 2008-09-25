<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Effects.aspx.cs" 
    Inherits="Effects" 
    Title="Ra-Ajax Effects" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra Ajax Samples - Ajax Effects</h1>
    <p>
        There is no fun in Ajax unless you have a powerful Ajax Effect Collection coupled with
        your Ajax Library. Ra-Ajax contains several different predefined Ajax Effects. It is also
        very easy to create your own Ajax Effects by looking at the source for any of the existing ones.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn" 
        Text="Click me" 
        AccessKey="G"
        OnClick="btn_Click" />
    <ra:Panel 
        runat="server" 
        ID="pnl" 
        CssClass="pnlAnimation">
        Watch the Ajax Effect as you click the above button...
    </ra:Panel>
    <div class="spacer">&nbsp;</div>
    <p>
        The effect you just observed is actually three <em>paralleled</em> effects which means that all the effects
        will run on the same Control with the same duration and conserving resources on the client and give the impression
        of being "one" effect even though technically they're unrelated and only distinct "paralleled" effects.
    </p>
    <p>
        Now try to click the button below here to see another Ajax Effect.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn2" 
        OnClick="btn2_Click" 
        Enabled="false"
        AccessKey="H"
        Text="Click me too" />
    <p>
        And finally the button below here
    </p>
    <ra:Button 
        runat="server" 
        ID="btn3" 
        OnClick="btn3_Click"
        Enabled="false" 
        AccessKey="J"
        Text="Click me too" />
    <p>
        And as usual with Ra-Ajax, no JavaScript knowledge is required to use any of these constructs. Take a look at 
        the source code for this page by clicking the "Show code" button and you will notice that everything is 100% C#
        and is entirely written in server-side code.
    </p>
    <h2>Moving focus around</h2>
    <p>
        In this sample we're also manipulating the Focus entirely from the same server-side C# code. 
        Yet again JavaScript is a bonus and not needed for doing this. This means
        that you can simply press space on your keyboard instead of having to use your mouse to click 
        the buttons. You can move focus around in Ra-Ajax pages from control to control exactly as 
        you wish completely transparently without having to rely on "quirks" at all.
    </p>
    <h2>Ajax with Keyboard Shortcuts</h2>
    <p>
        The buttons on this page have in order of appearance the keyboard shortcuts of "G", "H" and 
        "J". Meaning the top button have the keyboard shortcut "G". In FireFox you activate Keyboard
        Shortcuts by combining them with ALT + SHIFT. This means that if you use the keyboard combination
        of ALT+SHIFT+G you will click the top button, ALT+SHIFT+H the second button and so on. This
        can be set declaratively by using the <em>AccessKey</em> property of the Ra-Ajax WebControls.
        This also makes your Ra-Ajax solutions far more accessible for people with disabilities. Ra-Ajax
        is built from the ground up with accessibility in mind.
    </p>
    <h2>Chaining Ajax Effects</h2>
    <p>
        Sometimes you want to <em>chain</em> effects. This can easily be accomplished by adding to the Chained
        list of effects on your parent effect. The chained effects will be rendered immediately after the parent
        effect is finished rendering and thereby creating a sequence of effects which can be Paralleled, Chained
        and basically be as long and deep as you wish. Below we have probably gone a little bit "over the edge"
        with chaining effects to demonstrate this in real life for you. Have a look at the code to see how we're
        doing this.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn4" 
        Text="Click me" 
        OnClick="btn4_Click" />
    <ra:Panel 
        runat="server" 
        ID="pnl2"
        style="width:100px;height:100px;overflow:hidden;background-color:#eee;"
        CssClass="panel pnlInv pnlAbs">
        Watch the Ajax Effect as you click the above button...
    </ra:Panel>
    <div class="spacer">&nbsp;</div>
    <a href="Behaviors.aspx">On to "Ajax Behaviors"...</a>
</asp:Content>

