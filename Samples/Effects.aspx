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
        There's no fun in Ajax if you don't have a powerful <em>Ajax Effect Collection</em> coupled with
        your Ajax Library. Ra-Ajax contains several different predefined Ajax Effects. In addition it is
        very easy to create your own Ajax Effects by looking at some of the existing ones.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn" 
        Text="Click me" 
        AccessKey="G"
        OnClick="btn_Click" />
    <br />
    <ra:Panel 
        runat="server" 
        ID="pnl" 
        style="position:absolute;background-color:#ddd;border:solid 1px Black;padding:15px;height:100px;width:100px;">
        Watch the Ajax Effect as you click the above button...
    </ra:Panel>
    <div class="spacer">&nbsp;</div>
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
        And as usual with Ra-Ajax, no JavaScript knowledge is required to use these constructs, take a look at 
        the source code for this page by clicking the "Show code" button at the top right corner.
        Also this is just a small subset of the Ajax Effects which exist in Ra-Ajax.
    </p>
    <p>
        And you can see here that we've created our Effects in the Click event handler of a button.
        You can of course create Ajax Effects on the server side from any Ra-Ajax Event Handler if 
        you wish.
    </p>
    <br />
    <h2>Moving focus around</h2>
    <p>
        Notice another little nifty thing which is that in this sample we're also moving the Focus 
        around to exactly the button we want. This too is done in C# on the server-side. This means
        that you can simply press space on your keyboard instead of having to use your mouse to click 
        the buttons. You can move focus around in Ra-Ajax pages exactly as you wish.
    </p>
    <br />
    <h2>Ajax with Keyboard Shortcuts</h2>
    <p>
        The buttons on this page have in order of appearance the keyboard shortcuts of "G", "H" and 
        "J". Meaning the top button have the keyboard shortcut "G". In FireFox you activate Keyboard
        Shortcuts by combining them with ALT + SHIFT. This means that if you use the keyboard combination
        of ALT+SHIFT+G you will click the top button, ALT+SHIFT+H the second button and so on. This
        can be set declaratively by using the <em>AccessKey</em> property of the Ra-Ajax WebControls.
    </p>
    <br />
    <h2>Chaining Ajax Effects</h2>
    <p>
        Sometimes you want to run more than one Effect on the same element, like for instance you have
        an invisible Panel and you want to show that Panel with both the EffectRollDown and the
        EffectFadeIn. If you just create them both and let them both run normally, this will 
        often create a jagging effect which doesn't look nice. If you instead <em>chain</em> the
        effects, it will most often appear much smoother. An example is given below. Click the
        "Show Code" button to see the code written to accomplish something like this.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn4" 
        Text="Click me" 
        OnClick="btn4_Click" />
    <ra:Panel 
        runat="server" 
        ID="pnl2" 
        style="display:none;position:absolute;"
        CssClass="panel">
        Watch the Ajax Effect as you click the above button...
    </ra:Panel>
    <div class="spacer">&nbsp;</div>
    <a href="Behaviors.aspx">On to "Ajax Behaviors"...</a>
</asp:Content>

