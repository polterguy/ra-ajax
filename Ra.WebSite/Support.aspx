<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Support.aspx.cs" 
    Inherits="RaWebsite.Support" 
    Title="Get support with Ra-Ajax" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>Support</h1>
    <p>
        Our primary support mechanism for Ra-Ajax is <a href="http://stacked.ra-ajax.org">our forums</a>.
        Make sure you check to see if there already exists an answer for your question before posting it.
        Mostly you will get an answer to your question within 24 hours, but we cannot supply any guarantees
        for non-paying customers.
    </p>
    <div class="button">
        <a href="Docs.aspx">
            <img class="buttonImage" src="media/book_48.png" alt="Docs" />
            <span class="buttonText">Documentation</span>
            <span class="buttonText" style="margin-top:0;font-size:10px;">Docs &amp; Tutorials</span>
        </a>
    </div>
    <div class="button">
        <a href="http://stacked.ra-ajax.org">
            <img class="buttonImage" src="media/questionmark_48.png" alt="Forums" />
            <span class="buttonText">Forums</span>
            <span class="buttonText" style="margin-top:0;font-size:10px;">Quesions &amp; Answers</span>
        </a>
    </div>
    <div class="button">
        <a href="Blogs.aspx">
            <img class="buttonImage" src="media/kate.png" alt="Software" />
            <span class="buttonText">Read our Blogs</span>
            <span class="buttonText" style="margin-top:0;font-size:10px;">We frequently blog about Ra-Ajax</span>
        </a>
    </div>
    <div class="button">
        <a href="SoftwareFactory.aspx">
            <img class="buttonImage" src="media/blockdevice.png" alt="Software" />
            <span class="buttonText">Need Software?</span>
            <span class="buttonText" style="margin-top:0;font-size:10px;">We are a Software Factory</span>
        </a>
    </div>
    <h2>Documentation</h2>
    <p>
        You can find the <a href="Docs.aspx">documentation for Ra-Ajax here</a>. The documentation
        is highly organized and includes several tutorials for you to get started.
    </p>
    <h2>Professional help or want to use Ra-Ajax in closed source projects?</h2>
    <p>
        Ra-Software does not publish Ra-Ajax under any other license then the GPL, we also do
        not sell proprietary licenses of Ra-Ajax. However, Ra-Software is a consulting company 
        and when we do consulting we will use Ra-Ajax ourselves and <strong>you will be granted a 
        commercial/proprietary license of Ra-Ajax</strong> which we and you will use in your project 
        which does not restrict your project to the GPL license.
    </p>
    <p>
        This means that if you want to build proprietary software using Ra-Ajax you're gonna
        have to purchase consulting services from us. We do however provide consulting services
        all over the world (with the help of Skype, PayPal, email and such) and we also have 
        physical appearance in Northern California, Oslo Norway and Alexandria Egypt.
    </p>
    <p>
        You choose how many hours or how much of our services you would like to purchase
        but smaller jobs will obviously be more expensive then larger jobs. Our prices ranges from
        $250 per hour to $100 per hour depending on what you would want us to do, the length of the
        contract and so on. Normally we say yes to most jobs, though we might turn some jobs down
        due to ideological reasons. For instance we don't create weapon systems, gambling software and
        software which is illegal or contains obvious moral questions.
    </p>
    <p>
        Ra-Software is an <em>Ethical Company</em>...
    </p>
    <p>
        Read about <a href="Author.aspx">our qualifications here</a> or <a href="Testimonials.aspx">what our customers say about us</a>.
    </p>
    <p>
        And read more about our <a href="SoftwareFactory.aspx">consulting services or Software Factory options here</a>...
    </p>
</asp:Content>

