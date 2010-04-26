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
        <link href="media/Gold/Gold.css" rel="stylesheet" type="text/css" />
        <title>Ra-Ajax Samples</title>
        
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <ra:Accordion 
                    runat="server" 
                    ActiveAccordionViewIndex ="1"
                    OnActiveAccordionViewChanged="acc_ActiveAccordionViewChanged"
                    ID="acc">
                    <ra:AccordionView 
                        runat="server" 
                        Caption="First accordion view..."
                        ID="acc1"
                        >
                        <div>
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                            sdfoij sdf
                            <br />
                        </div>
                    </ra:AccordionView>
                    <ra:AccordionView 
                        runat="server" 
                        Caption="Second accordion...!!!"
                        ID="acc2">
                        <div>
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                            qweouh werouh wer
                            <br />
                        </div>
                    </ra:AccordionView>
                </ra:Accordion>
            </div>
        </form>
    </body>
</html>
