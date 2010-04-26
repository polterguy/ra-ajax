<%@ Page 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Default.aspx.cs" 
    Inherits="Window_Default" %>

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
    <head id="Head1" runat="server">
        <link href="../media/Gold/Gold.css" rel="stylesheet" type="text/css" />
        <title>Ra-Ajax Samples</title>
        
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <ra:window 
                    ID="wind" 
                    Caption="Window test"
                    Movable="true"
                    style="position:absolute;top:5px;left:5px;width:300px;"
                    runat="server">
                    <div style="height:200px;">
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                        sdfouh sdfouh dsfiuh dsf
                        <br />
                    </div>
                   
                </ra:window>
            </div>
        </form>
    </body>
</html>
