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
            <div><ra:Button ID="Button1" runat="server" OnClick="Btn_click" Text="sdsdsd">
</ra:Button>
                <ra:TextBox ID="TextBox1" runat="server">
                </ra:TextBox>
                <ra:window 
                    ID="wind" 
                    Caption="Window test"
                    Movable="true"
                    Dir="rtl"
                    style="position:absolute;top:5px;left:5px;width:300px;"
                    runat="server">
                    <div style="overflow:auto;">
                      متظاهرو تايلند يواصلون الاحتجاج	
قرب إقرار إصلاح المالية الأميركية	
سامبدوريا يعرقل استعادة روما للقمة	
كليفلاند يتقدم بنهائيات السلة الأميركية	
الجيش الأميركي يلغي تمرينا نوويا	
مدرعة بريطانية باهظة لم تر النور	
اتهام أوباما بالتعتيم على حادث تكساس	
المطاعم المصرية تقاطع اللحوم	
الصين تعزل حاكم إقليم شنغيانغ	
قتلى وجرحى في معارك بمقديشو	
زلزال بتايوان ولا تحذير من تسونامي	
روسيا تسلم قرغيزستان وزيرا مخلوعا	
إضراب بالجزائر يطالب برفع الأجور	
شافيز يحذر كولومبيا من فوز سانتوس	
متكي يدعو لمحادثات نووية جديدة	
موسوي: إيران في أزمة	
بن همام: مستقبل كرة القدم بآسيا	
حزب أوربان يفوز بانتخابات المجر	
رسام يدافع عن المصباح الكمثري	
استنكار لصمت لجنة حقوق الإنسان	
جين البدانة يؤدي لفقدان أنسجة الدماغ	
الرئيس النمساوي يفوز بولاية ثانية	
مذكرات بوش في نوفمبر	
عباس لأوباما: افرض الحل	
الأردن يفرج عن متهمين بالفساد	
ارتفاع مبيعات تويوتا 26%	
الريان وأم صلال لربع نهائي كأس الأمير	
مئات المسلمين بألمانيا قيد التحقيق	
بوسطن غلوب: استعدوا لإيران نووية	
غضب شعبي لمقتل مدنيين بأفغانستان	
	
	
التحذير من تدهور الموقف المالي لليابان
اعتقال مسلح بمطار غادره أوباما
مصر تتجه لتعديل ديني على المناهج
                    </div>
                   
                </ra:window>
                    <ra:window 
                    Visible="false"
                    ID="Window1" 
                    Caption="Window test"
                    Movable="true"
                    style="position:absolute;top:50px;left:50px;width:300px;"
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
