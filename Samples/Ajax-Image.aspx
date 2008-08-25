<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-Image.aspx.cs" 
    Inherits="AjaxImage" 
    Title="Ajax Image Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ajax Image Sample</h1>
    <p>
        This is our <em>Ajax Image</em> reference sample. The Ajax Image is a "passive" control which means
        it doesn't have any events it dispatches. This means that you can set the properties for it in
        an Ajax Callback, but you cannot have an Ajax Image to CREATE an Ajax Callback.
    </p>
    <ra:Button 
        runat="server" 
        ID="btn" 
        Text="Change image" 
        OnClick="btn_Click" />
    <br />
    <ra:Image 
        runat="server" 
        ID="img"
        AlternateText="Winnie the Pooh an AJAX" 
        ImageUrl="media/Pooh_Shepard_1926.png" />
    <br />
    <p>
        When you click the above Button the Ajax Image changes it ImageUrl...
        <br />
        PS!
        <br />
        Images taken from <em>WIKIPEDIA</em> which means they're supposed to be some kind of GDPL thingie... ;)
    </p>
    <br />
    <h2>Ajax JavaScript and Security</h2>
    <p>
        There is nothing which doesn't make it possible for you to create secure applications in any Ajax Frameworks.
        Though if you rely heavily on JavaScript it is easier to "broaden the Attack Surface". Take this simple
        Ajax Image sample. Now imagine that in the <em>OnClick Event Handler</em> for the above Button you would
        authorize the currently logged in user before allowing him to change the image URL for security reasons.
        In Ra-Ajax this is extremely easy and not a single user one the planet would be able to get to change
        the above Ajax Image unless he would pass your "validate user" logic which runs completely isolated
        on your server. And unless you create a "Show Code" button (which this example have ;) nobody would
        probably even understand the semantics of how you authorize your users.
    </p>
    <p>
        In a JavaScript Ajax solution this is also *possible* to do, though it is very tempting to take "shortcuts"
        and just embed the image URL in some JavaScript file or something, or call a WebServer which resides on
        some other server or application pool (meaning cookies and/or session is not transfered unless you *explicitly*
        tells it to do so) etc...
    </p>
    <br />
    <h2>Does JavaScript Ajax Solutions broaden the "Attack Surface"...</h2>
    <p>
        ...not by itself, but often they TEND to do so in practice since you're developing in a Language 
        that is sent to the client, which means the client can look at your code and potentially also exploit
        your code. No IF you do everything 100% correct then this is not a problem, though if you're developing
        in JavaScript yourself it is very easy to "leak" Business Logic into the client area which again means
        that others can take advantage of that leakage.
    </p>
    <br />
    <h2>Ra-Ajax does NOT expose Business Logic</h2>
    <p>
        In Ra-Ajax (and other similar server-side frameworks) this is NOT the case. In Ra-Ajax you will
        have all your logic on the server which means you can authenticate and authorize on the same logic 
        as the rest of your application and you don't <em>expose busines logic</em>.
    </p>
    <p>
        Ra-Ajax heavily uses and is dependant upon JavaScript. But for Ra-Ajax JavaScript is a "rendering
        surface", or the "View" if you want. Nothing more. In fact if you hav invisible controls, even 
        containing content of themselves the client will NOT see that content at all. Below I have
        included a Ra-Ajax Image with a "secret" URL. Notice that if you click View Source in your
        browser you aren't even able to determine which TYPE of control this is. Not to mention it's
        settings or content for that matter. This content could have been the patient data for your
        medical Software System. Or it could have been National Security data. And so on...
    </p>
    <ra:Image 
        runat="server" 
        ID="Image1" 
        Visible="false"
        AlternateText="Winnie the Pooh an AJAX" 
        ImageUrl="media/Pooh_Shepard_1926.png" />
    <p>
        Click the "View Source" of your browser and verify that you cannot see anything apart from an
        <em>empty span</em> while if you click "Show Code" at the top/right corner of your browser you
        will see that the empty span is in fact an Ajax Image and you can see it's URL and so on. Though
        this is because I *WANT* you to be able to see that fact, NOT because you delberately tried to
        hack my systems and due to leaking JavaScript into the client you were able to do just that.
    </p>
    <p>
        JavaScript Ajax solutions CAN be secure. Ra-Ajax solutions ARE secure (unless you deliberately 
        make them in-secure ;)
    </p>
</asp:Content>

