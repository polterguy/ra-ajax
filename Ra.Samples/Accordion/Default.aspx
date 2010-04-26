<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="_Default" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Behaviors" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Ra.Extensions" 
    Namespace="Ra.Extensions.Widgets" 
    TagPrefix="ra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link href="../media/Gold/Gold.css" rel="stylesheet" type="text/css" />
        <title>Ra-Ajax Samples</title>
        
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <ra:Accordion 
                    runat="server" 
                    style="display:table;margin-left:auto;margin-right:auto;width:400px;margin-top:50px;"
                    ActiveAccordionViewIndex ="0"
                    OnActiveAccordionViewChanged="acc_ActiveAccordionViewChanged"
                    ID="acc">
                    <ra:AccordionView 
                        runat="server" 
                        Caption="First accordion view..."
                        ID="acc1">
                        <div>
                            This is our first Accordion View, and the one which is by default visible...
                        </div>
                    </ra:AccordionView>
                    <ra:AccordionView 
                        runat="server" 
                        Caption="Second accordion...!!!"
                        ID="acc2">
                        <div>
                            Here is our second AccordionView, this one contains a lot mor text as you can see and hence
                            it will roll over the text on multiple lines, and hence this AccordionView will be slightly 
                            taller than the first one. This is automatically being taken care of since the AccordionView
                            perfectly supports "liquid layouts" as you obviously can see...
                        </div>
                    </ra:AccordionView>
                    <ra:AccordionView 
                        runat="server" 
                        Caption="Second accordion...!!!"
                        ID="acc3">
                        <div style="height:150px;">
                            This AccordionView has a "fixed height" which still works perfectly...
                        </div>
                    </ra:AccordionView>
                </ra:Accordion>
            </div>
        </form>
    </body>
</html>
