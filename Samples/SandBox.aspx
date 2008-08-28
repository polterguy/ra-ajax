<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="SandBox.aspx.cs" 
    Inherits="SandBox" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
        <title>Untitled Page</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <ra:Button 
                ID="btnContinue1" 
                runat="server" 
                Text="&nbsp;Fortsätt&nbsp;"
                OnClick="btnContinue1_Click" />
            <ra:Panel 
                runat="server" 
                ID="panelResults" 
                style="display:none;border:solid 1px Black;background-color:Yellow;overflow:hidden;">
                <h3>Resultat</h3>
                <div class="div_table_2_cel_left"><span>Exempel *</span></div>
                <div class="div_table_2_cel_left"><span>Result</span></div>
                <span class="smalltext">
                    <strong>Information</strong>
                    <br />
                    Text text text text text
                </span>
                asdfuh asiduh asdiuh asdiuh asd
                asdfuh asiduh asdiuh asdiuh asd
                asdfuh asiduh asdiuh asdiuh asd
                asdfuh asiduh asdiuh asdiuh asd
                asdfuh asiduh asdiuh asdiuh asd
                asdfuh asiduh asdiuh asdiuh asd
                asdfuh asiduh asdiuh asdiuh asd
            </ra:Panel>
        </form>
    </body>
</html>
