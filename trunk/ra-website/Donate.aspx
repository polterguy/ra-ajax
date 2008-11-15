<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Donate.aspx.cs" 
    Inherits="Donate" 
    Title="Donate to Ra-Ajax" %>

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
    Runat="Server">

    <h1>Donate to Ra-Ajax</h1>
    <p>
        Ra-Ajax is our gift to the world. In addition we need a great Ajax library like Ra-Ajax ourselves when doing
        Ajax development ourselves. But even though we're giving away Ra-Ajax for free it is not free to maintain and
        develop. And we would appreciate all the help you could give us to help make Ra-Ajax better. Click the below
        PayPal button to go to PayPal and help us out making the best Ajax library in the world for ASP.NET.
    </p>
    <p>
        (leads you to PayPal)
        <br />
        <a href="https://www.paypal.com/_donations/?cmd=_s-xclick&amp;hosted_button_id=838418">
            <img src="media/donate-paypal.gif" alt="Donate to Ra-Ajax" />
        </a>
    </p>
    <p>
        Some reasons why you would want to donate to Ra-Ajax and Ra-Software AS - the company behind Ra-Ajax are;
    </p>
    <ul>
        <li>You're using it yourself and you think it's a great tool and want to show your appreciation</li>
        <li>You would like to see IT become a commodity and think Ajax and Open Web tools are the enablers of this to happen</li>
        <li>You dislike big companies having a monopoly on your development cycle and think that LGPL is a great tool to remove Lock-In, EEE and FUD</li>
        <li>You would like to help us out in the <a href="http://ra-ajax.org/gaiaware-and-b-rd-stranheim-declaring-war-on-ra-ajax.blog">ongoing law suite against Gaiaware</a></li>
        <li>We have given you superb free support in our forums and thereby helped you save a lot of time on your own projects</li>
        <li>We created another release for you where we fixed a bug that stopped you from moving forward in your own project</li>
        <li>You live in a rich country or work in a rich company and would like to see people with less means also have great Ajax tools</li>
    </ul>
    <p>
        There are probably a gazillion reasons to donate, and not many to not donate. And fact is that if everyone who
        used Ra-Ajax would donate one dollar each which is very little compared to the value Ra-Ajax gives you we would
        probably not have to do anything else then creating great Ajax tools for you which you and the rest of the world
        can use in their own projects - for FREE!
    </p>
    <p>
        Whatever your reasons are, we are happy for every dollar that comes our way. And every dollar given to Ra-Ajax will
        be spent wisely in the constant ongoing maintenance and further development of Ra-Ajax. Every dollar we receive in
        donations are another dollar we don't have to earn on doing off topic consultancy work to make our living. This
        effectively means that if we could cover our living expenses completely on donations we would be working 100%
        on Ra-Ajax all the time instead of having to spend 50% of our time billing hours to make a living for ourselves.
    </p>

</asp:Content>

