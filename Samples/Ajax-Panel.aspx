<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Panel.aspx.cs" 
    Inherits="Samples.AjaxPanel" 
    Title="Ra-Ajax Panel Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra-Ajax Samples - Panel</h1>
    <p>
        Our <em>Ajax Panel</em> is, more or less, a superset of the <em>System.Web.UI.WebControls.Panel</em> though
        it does not inherit from it. One thing which is mostly unique about the Ajax Panel is that, contrary to all 
        other Ajax Controls we have looked at so far (assuming you have been reading the samples sequentially as 
        suggested), the Ajax Panel is the first control so far that can have Child Controls. Or at least where it 
        makes sense to have Child Controls.
    </p>
    <ra:Panel 
        runat="server" 
        ID="pnl" 
        style="background-color:Yellow;"
        CssClass="panel">

        <p>
            This is an <em>Ajax Panel</em> and notice how you can easily have
            <ra:LinkButton 
                runat="server" 
                ID="lnk" 
                Text="Ajax Controls" 
                OnClick="lnk_Click" />
            inside of the Panel itself.
        </p>
        <p>
            Try to edit content inside of this TextBox; 
        </p>
        <p>
            <ra:TextBox 
                runat="server" 
                ID="txt" 
                Text="Edit me"
                OnKeyUp="txt_KeyUp" />
        </p>
    </ra:Panel>
    <h2>Complexity Within Complexity</h2>
    <p>
        Notice how the background color changes when you click the LinkButton and how the color of the
        text within the Ajax Panel changes as we type into the TextBox.
    </p>
    <p>
        This would be impossible in an Ajax framework that is constructed around the concept of Partial Rendering.
        This is because; as we click the LinkButton or type into the TextBox contained inside of the Ajax Panel,
        the Ajax Panel itself would be re-rendered. This would first of all remove focus, but more importantly,
        even remove things we had typed into the TextBox after the Ajax Request was initiated but before the Ajax 
        Request had returned. We discussed this in greater detail in the 
        <a href="Ajax-DropDownList.aspx">Ajax DropDownList Sample</a>. So I won't go into more details about
        Partial Rendering in particular.
    </p>
    <p>
        But what I will say is that if you build a server-side bound Ajax framework the right way, it is
        extremely easy to completely abstract away JavaScript from users of your framework while at 
        the same time enable very rich expressiveness and <em>richness in richness</em> or 
        <em>complexity inside complexity</em> as the above example shows.
    </p>
    <p>
        Now the above sample is pretty simple and we could have brought it way further by creating an
        Ajax AutoCompleter which used our Ajax Panel or something like that without even having to
        tamper with JavaScript ourselves at all. And the only reason why this is possible; is because our
        Ajax framework is not created around the concept of Partial Rendering.
    </p>
    <h2>Think of a Control</h2>
    <p>
        The Ajax Panel above can in fact contain any controls you wish, to some extent even 3rd. party
        controls which don't even know what Ra-Ajax is. In fact a very common pattern for creating 
        <em>DataGrids</em> using Ra-Ajax is by wrapping the Ra-Ajax Panel around a GridView or a
        Repeater and then when your Repeater/GridView needs to be re-rendered you just call the 
        <em>ReRender</em> method on the Panel as described in the 
        <a href="Dynamic.aspx">Dynamic Ajax Controls</a> example.
    </p>
    <a href="Ajax-RadioButton.aspx">On to Ajax RadioButton</a>
</asp:Content>
