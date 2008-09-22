<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Window.aspx.cs" 
    Inherits="AjaxWindow" 
    Title="Ra-Ajax Window Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax Window Sample</h1>
    <p>
    	This is our <em>Ajax Window</em> example. The Ajax Window tries to as much as possible mimick a Desktop Window in that
    	it has properties which makes it behave at least close to the behavior of a Desktop Window. The Ajax Window is also
    	like most of our Ajax Extension Controls created without any custom JavaScript. This means it also serves as a perfect
    	reference for how to create your own Ajax Controls utilizing the building blocks from Ra-Ajax.
    </p>
    <p>
        <ra:Button 
    	    runat="server" 
    	    id="showWindow" 
    	    Text="Show Window" 
    	    OnClick="showWindow_Click" />
    </p>
    <div style="position:relative;">
        <ext:WindowLight 
    	    runat="server"
    	    Caption="Ajax Window"
    	    CssClass="window smallWnd"
    	    id="window">
            <div style="padding:5px;">
                <p>
	                Try to move me around by dragging my header field.
	            </p>
	            <p>
                    <img 
                        alt="Flower" 
                        src="media/flower1.jpg" />
                </p>
            </div>
        </ext:WindowLight>
    </div>
    <div class="spacerLarge">&nbsp;</div>
    <p>
    	Try to move the Ajax Window by dragging and dropping its header.
    </p>
    <h2>About creating Ajax Extension Controls</h2>
    <p>
    	The Ajax Window is a very good example of how to create an Ajax Extension Control yourself utilizing Ra-Ajax. In fact the
    	entire code for the Ajax Window is 100% written in server-side C#. The Ajax Window itself is inherited from Ra-Ajax Panel
    	and in the <em>CreateChildControls</em> method we just create a couple of extras for the Window which is being added to
    	the Controls collection of the Window Control itself. Then from our override of the <em>LoadViewState</em> method we
    	make sure we call the <em>EnsureChildControls</em> which will call our CreateChildControls method.
    </p>
    <p>
    	In fact the entire code for the whole Ajax Window is so small it's easy to reproduce it here...
    </p>
    <pre>
using System;
using System.ComponentModel;
using WEBCTRLS = System.Web.UI.WebControls;
using Ra.Widgets;

namespace Ra.Extensions
{
    [ASP.ToolboxData("&lt;{0}:Window runat=\"server\"&gt;&lt;/{0}:Window&gt;")]
    public class Window : Panel
    {
        private WEBCTRLS.Panel _pnlHead;
        private Label _lblHead;
        private BehaviorDraggable _dragger;

        [DefaultValue("")]
        public string Caption
        {
            get { return ViewState["Text"] == null ? "" : (string)ViewState["Text"]; }
            set { ViewState["Text"] = value; }
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            CreateWindowControls();
        }

        private void CreateWindowControls()
        {
            // Creating header control(s)
            _pnlHead = new WEBCTRLS.Panel();
            _pnlHead.ID = "head";
            _lblHead = new Label();
            _lblHead.ID = "headCaption";
            _pnlHead.Controls.Add(_lblHead);
            this.Controls.AddAt(0, _pnlHead);
			
            // Creating dragger
            _dragger = new BehaviorDraggable();
            this.Controls.Add(_dragger);
        }

        protected override void OnPreRender (EventArgs e)
        {
            _pnlHead.CssClass = CssClass + "-head";
            _lblHead.Text = Caption;
            _dragger.Handle = _pnlHead.ClientID;
            base.OnPreRender (e);
        }
    }
}
    </pre>
    <p>
    	And that's it. With those codelines utilizing Ra-Ajax you actually have an Ajax Window.
    </p>
    <p>
    	In fact the most "difficult" thing to understand is where you call EnsureChildControls. I choose
    	mostly to do this in the LoadViewState since that's the "earliest" time I have access to the ViewState.
    	And often I store stuff in the ViewState which I again need when I create the Window. Also you should
    	upon EVERY Callback (and postback) re-create the Window using the exact same procedure to make sure
    	your (Ajax) Controls are added to the Controls collection in the correct order and so on. If you're 
    	stuck with creating an Ajax Extension control this is our favorite topic to help you out with though
    	at <a href="http://ra-ajax.org/Forums/Forums.aspx">our forums</a> so don't be afraid to ask for help!
    </p>
    <p>
    	Another thing which is important to understand is that mostly you want to "defer" setting some properties until
    	the OnPreRender override of your Control. You can see I am doing this with e.g. the Text of the Caption Label
    	and the CssClass of the _pnlHead. This is because sometimes your users of your Ajax Extension Controls will want to set some
    	of those properties in the code-behind file in maybe OnLoad or some similar method. And since some of your properties
    	tends to be dependant upon others. Like for instance the CssClass of the _pnlHead field in the above sample. You
    	must wait until you can be pretty sure about that the consumer of your Ajax Extension Control is "finished" doing
    	his parts...
    </p>
    <p>
    	Also theoretically we could save some ViewState bytes in the above sample by directly using the _lblHeader.Text
    	as the value of the Caption property. Though this I have bad experience with since it tends to become very complex
    	since then you must make sure things are loaded and so on before accessing the property etc to such an extent that
    	it becomes too hard to understand and maintain. But feel free to experiment with this if you like...
    </p>
    <p>
    	Also we are VERY interested in getting to know about Extension Controls created utilizing Ra-Ajax. So let us know
    	if you do something great! Especially if you want to share it with others ;)
    </p>
    <a href="Ajax-DataGrid.aspx">On to Ajax DataGrid</a>
</asp:Content>




