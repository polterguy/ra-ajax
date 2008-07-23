<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Author.aspx.cs" 
    Inherits="Author" 
    Title="About the author of Ra Ajax" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    Runat="Server">

    <h1>About me</h1>
    <p>
        I am a system developer who needs tools and have slowly grown tired of not being
        able to read, learn from and modify the tools I use. I am a Free Software advocate
        though I still think that proprietary software holds a place in the world.
    </p>
    <p>
        I am born in 1974 and my name is Thomas Hansen. I have three kids whom I hope will 
        get to inherit a better place than what I currently can give them. I think Open 
        Source, Open Standards and Freedom of Information are some of the prime tools for
        me to succeed in leaving this place in a better shape than what it currently is in.
    </p>
    <p>
        I have been obsessed with writing developer libraries large portions of my life and
        I have had a particular strong leaning towards GUI libraries. Ra Ajax is my third
        GUI library. My first GUI library was <a href="http://smartwinlib.org">SmartWin++</a>,
        my second GUI library was <a href="http://ajaxwidgets.com">Gaia Ajax Widgets</a> whom
        unfortunately was lost out of my hands some time back ago and Ra Ajax is my third
        and hopefully last GUI library. Though every time I build a new GUI library I get better
        at it, and the knowledge also learned from writing Gaia Ajax Widgets have been very 
        important for me in order to build Ra Ajax.
    </p>
    <p>
        I have an intense believe in the Open Web and Ajax as the foundation for the third
        generation of system development platforms, and I feel that it is imperative that the 
        tools for this platform belongs to the world and not to a specific group, organization, 
        company or person.
    </p>
    <p>
        I am proud to be an <strong>Open Web Advocate and Evangelist</strong>!
    </p>
    <h2>Hire me?</h2>
    <ra:Panel 
        runat="server" 
        ID="pnlEmail" 
        Visible="false" 
        style="padding:25px;background-color:Yellow;border:solid 1px Black;overflow:hidden;height:1px;">
        Sorry, just had to show off Ra Ajax ;)
        <br />
        My email address is; <strong>polterguy@gmail.com</strong>
    </ra:Panel>
    <p>
        I might be available for hire if you have an interesting project that spikes my
        interest. If you're interested, please 
        <ra:Button 
            runat="server" 
            Text="drop me a note" 
            ID="drop" 
            style="border:none;background-color:White;color:Blue;padding:0px;margin:0px;cursor:pointer;"
            OnClick="drop_click" />
        explaining what you'd like me to do. I am highly qualified in Ajax, ASP.NET, all 
        acronyms starting with OO and I have much experience with coaching which is the 
        role I am most happy with. You will get most value for your money if I can coach 
        and do architectural jobs in teams of domain experts and hired developers. I don't 
        do VB.NET, Silverlight, ActiveX or WinForms development and I do not work for 
        military organizations. I have been programming almost every day since I was 8 
        years old and I have been project leads mostly in the later years and among other 
        things I am the founder and "father" of Gaia Ajax Widgets and Gaiaware AS which 
        can be found at the <a href="http://ajaxwidgets.com">Gaia website</a>. 
        <br />
        BTW, I still own 25% of the shares in Gaiaware AS if you are interested in buying 
        them.
    </p>
    <p>
        If I can get to use Ra Ajax you'll get an instant discount. If I can start at the 
        beginning of the project instead of 3 months past due-date you will also get an 
        instant discount. If the project in its nature is important to the world and of 
        "helping character" you will get an instant discount. If the project is an Open 
        Source projects you will get an instant discount. If the project is to be deployed 
        on <a href="http://www.mono-project.com/">Mono</a> you will get an instant discount. 
        If I can use Ra Ajax to develop Open Source components to run on Mono which I myself 
        can keep the copyright for you will get a *MASSIVE* discount.
    </p>
    <p>
        I live in Porsgrunn/Norway but might be interested in temporary moving to attend very 
        interesting projects, especially if the location is warm and the project is interesting. 
        I can also work from Norway on remote locations if the project is of such a character. 
        I speak and write English and Norwegian fluently.
    </p>

</asp:Content>

