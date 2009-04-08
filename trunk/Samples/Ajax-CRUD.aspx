<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Ajax-CRUD.aspx.cs" 
    Inherits="Samples.CRUD" 
    Title="Ra-Ajax CRUD Sample" %>

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

    <h1>Ra-Ajax Samples - CRUD</h1>
    <p>
        CRUD means <em>Create, Read, Update and Delete</em>. Most web applications does this in some form
        or another. Ra-Ajax is especially great for creating CRUD applications due to its focus on 
        WebControls like Button, TextBox and so on. Here is an example of how you could create a CRUD 
        for to calculate Travel Expenses in your company.
    </p>
    <ext:Accordion 
        runat="server" 
        OnActiveAccordionViewChanged="accordion_ActiveAccordionViewChanged"
        style="margin-bottom:50px;"
        ID="accordion">

        <ext:AccordionView 
            runat="server" 
            ID="acc1" 
            style="position:relative;"
            Caption="Personal information">

            <p>Please give us your personal information</p>
            <table style="width:60%;float:left;font-size:0.7em;border-right:dashed 1px #999;">
                <tr>
                    <td>Firstname/Lastname</td>
                    <td>
                        <ra:TextBox 
                            runat="server" 
                            ID="firstname" 
                            OnTextChanged="TextBoxCheckForValue"
                            style="background-color:#fdd;width:70px;" />
                        <ra:TextBox 
                            runat="server" 
                            ID="lastname" 
                            OnTextChanged="TextBoxCheckForValue"
                            style="background-color:#fdd;width:70px;" />
                    </td>
                </tr>
                <tr>
                    <td>Address</td>
                    <td>
                        <ra:TextBox 
                            runat="server" 
                            ID="address" 
                            OnTextChanged="TextBoxCheckForValue"
                            style="background-color:#fdd;width:70px;" />
                    </td>
                </tr>
                <tr>
                    <td>Zip code</td>
                    <td>
                        <ra:TextBox 
                            runat="server" 
                            ID="zip" 
                            OnTextChanged="TextBoxCheckForValue"
                            style="background-color:#fdd;width:70px;" />
                    </td>
                </tr>
                <tr>
                    <td>Bank no</td>
                    <td>
                        <ra:TextBox 
                            runat="server" 
                            ID="bankno" 
                            OnTextChanged="TextBoxCheckForValue"
                            style="background-color:#fdd;width:70px;" />
                    </td>
                </tr>
            </table>
            <table style="width:35%;float:left;font-size:0.7em;">
                <tr>
                    <td>Position</td>
                    <td>
                        <ra:TextBox 
                            runat="server" 
                            ID="position" 
                            OnTextChanged="TextBoxCheckForValue"
                            style="background-color:#fdd;width:100px;" />
                    </td>
                </tr>
                <tr>
                    <td>Country</td>
                    <td>
                        <ra:SelectList runat="server" ID="country">
                            <ra:ListItem Text="Norway" />
                            <ra:ListItem Text="Egypt" />
                            <ra:ListItem Text="Somewhere else" />
                        </ra:SelectList>
                    </td>
                </tr>
            </table>
            <br style="clear:both;" />
            <ra:Button 
                runat="server" 
                ID="step2" 
                style="position:absolute;bottom:5px;right:5px;font-size:1.2em;"
                OnClick="step2_Click"
                Text="Proceed" />

        </ext:AccordionView>

        <ext:AccordionView 
            runat="server" 
            ID="acc2" 
            style="position:relative;"
            Caption="Purpose with journey">
            <p>
                Please give us details about the purpose of the journey...
            </p>
            <div style="float:left;width:45%;">
                <ra:TextArea 
                    runat="server" 
                    style="width:80%;background-color:#fdd;" 
                    OnTextChanged="TextAreaCheckForValue"
                    ID="purpose" />
            </div>
            <div style="float:left;width:45%;">
                <ra:RadioButton 
                    runat="server" 
                    ID="domestic" 
                    GroupName="whereTo" 
                    style="background-color:#fdd;" 
                    OnCheckedChanged="whereTo_CheckedChanged"
                    Text="Domestic" />
                <br />
                <ra:RadioButton 
                    runat="server" 
                    ID="abroad" 
                    GroupName="whereTo"
                    style="background-color:#fdd;" 
                    OnCheckedChanged="whereTo_CheckedChanged"
                    Text="Abroad" />
            </div>
            <br style="clear:both;" />
            <table> 
                <tr>
                    <th>Start</th>
                    <th>End</th>
                </tr>
                <tr>
                    <td>
                        <ext:DateTimePicker 
                            runat="server" 
                            ID="start" />
                    </td>
                    <td>
                        <ext:DateTimePicker 
                            runat="server" 
                            ID="end" />
                    </td>
                </tr>
            </table>
            <ra:Button 
                runat="server" 
                ID="step3" 
                style="position:absolute;bottom:5px;right:5px;font-size:1.2em;"
                OnClick="step3_Click"
                Text="Proceed" />
        </ext:AccordionView>

        <ext:AccordionView 
            runat="server" 
            ID="acc3" 
            style="position:relative;"
            Caption="Purpose with journey">
            <h2>Results</h2>
            <p>
                Congratuations
                <ra:Label runat="server" style="font-weight:bold;" id="fullName" />
                you started your journey 
                <ra:Label runat="server" style="font-weight:bold;" id="fromStart" />
                and you ended it on
                <ra:Label runat="server" style="font-weight:bold;" id="fromEnd" />.
            </p>
            <p>
                You live in <ra:Label runat="server" style="font-weight:bold;" id="fromAdr" />,
                you're a <ra:Label runat="server" style="font-weight:bold;" id="fromPos" /> living
                in
                <ra:Label runat="server" style="font-weight:bold;" id="fromCountry" />.
            </p>
            <p>
                You were traveling <ra:Label runat="server" style="font-weight:bold;" id="fromDomestic" />
                and you write; <ra:Label runat="server" style="font-weight:bold;" id="fromTxt" /> in
                the TextArea as the purpose of your journey...
            </p>
            <p>
                This is obviously NOT a fully fledged "travel expenses calculator" and could probably
                be extended quite heavily, but figuring out the math behind the traveling calculations 
                in addition to expanding upon it is an excercise left for you to figure out ;)
            </p>
            <p>
                Ohh yeah, and in case you wonder; <strong>this works on the iPhone</strong> in addition
                to every standard compliant browser on the planet...!!!
            </p>
        </ext:AccordionView>

    </ext:Accordion>
    <p>
        Creating CRUD applications in Ra-Ajax is very easy, the above sample just demonstrates how you
        could create a "travel expenses calculator" for your company or something. I haven't added any
        "math" behind it, but I think you'll get the point quite easily by dissecting the code which is
        very small and probably quite understandable...
    </p>
    <p>
        I created this sample in less then one hour since I read on 
        <a href="http://digi.no">digi.no</a> - a Norwegian IT magazine that the Norwegian government 
        had hired a consulting company (MakingWaves) to implement their travel expenses calculator.
        I think it was a very nice project since it included a pretty "pro Microsoft company" doing
        Open Source for the government (the code was BSD licensed) in addition to that it was the
        government creating not only an Open publicly available Portal where Norwegians could calculate 
		their own "travel expenses" as citizens but also even Open Source licensing it. However...
    </p>
    <p>
        Unfortunately they built the thing in Flash... Which I find a little bit depressing since there
        is absolutely no reasons what so ever why someone would choose Flash for a solution like this.
    </p>
    <p>
        I figured I could do this in an hour and at the same time demonstrate the CRUD capabilities of 
        Ra-Ajax, so I did ;)
    </p>
    <a href="Ajax-WebPart.aspx">On to Ajax WebPart</a>
</asp:Content>
