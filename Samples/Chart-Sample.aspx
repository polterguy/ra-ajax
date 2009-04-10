<%@ Page 
    Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    CodeFile="Chart-Sample.aspx.cs" 
    Inherits="Samples.Chart" 
    Title="Ra-Ajax Chart Sample" %>

<%@ Register 
    Assembly="Ra" 
    Namespace="Ra.Widgets" 
    TagPrefix="ra" %>

<%@ Register 
    Assembly="Extensions" 
    Namespace="Ra.Extensions" 
    TagPrefix="ext" %>

<%@ Register 
    Assembly="System.Web.DataVisualization" 
    Namespace="System.Web.UI.DataVisualization.Charting" 
    TagPrefix="chart" %>

<asp:Content 
    ID="Content1" 
    ContentPlaceHolderID="cnt1" 
    runat="server">

    <h1>Ra-Ajax Chart Sample</h1>
    <p>
        How to create Ajax Charts with Ra-Ajax
    </p>


    <ra:Panel 
        runat="server" 
        ID="chartWrp" 
        style="float:left;width:430px;">
        <chart:Chart 
            id="Chart1" 
            runat="server" 
            ImageStorageMode="UseImageLocation"
            Height="296px" 
            Width="412px" 
            ImageLocation="~/ChartImages/ChartPic_#SEQ(300,3)" 
            Palette="BrightPastel" 
            imagetype="Png" 
            BorderDashStyle="Solid" 
            BackSecondaryColor="White" 
            BackGradientStyle="TopBottom" 
            BorderWidth="2" 
            backcolor="#D3DFF0" 
            BorderColor="26, 59, 105">
            <legends>
                <chart:Legend 
                    IsTextAutoFit="False" 
                    Name="Default" 
                    BackColor="Transparent" 
                    Font="Trebuchet MS, 8.25pt, style=Bold" />
            </legends>
            <borderskin skinstyle="Emboss" />
            <chartareas>
                <chart:ChartArea 
                    Name="ChartArea1" 
                    BorderColor="64, 64, 64, 64" 
                    BorderDashStyle="Solid" 
                    BackSecondaryColor="White" 
                    BackColor="64, 165, 191, 228" 
                    ShadowColor="Transparent" 
                    BackGradientStyle="TopBottom">
                    <area3dstyle 
                        Rotation="10" 
                        perspective="10" 
                        Inclination="15" 
                        IsRightAngleAxes="False" 
                        wallwidth="0" 
                        IsClustered="False" />
                    <axisy linecolor="64, 64, 64, 64">
                        <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
                        <majorgrid linecolor="64, 64, 64, 64" />
                    </axisy>
                    <axisx linecolor="64, 64, 64, 64">
                        <labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
                        <majorgrid linecolor="64, 64, 64, 64" />
                    </axisx>
                </chart:ChartArea>
            </chartareas>
        </chart:Chart>
    </ra:Panel>
    <div style="float:left;width:250px;">
        <ra:TextBox 
            runat="server" 
            ID="lastVal" 
            OnEnterPressed="ChangeChart"
            Text="77" />
    </div>
    <div style="clear:both;">&nbsp;</div>


    <ra:Panel 
        runat="server" 
        ID="infoPanel" 
        style="margin-top:15px;padding:15px;border:dashed 1px #aaa;opacity:0.3;cursor:pointer;">
        <p>
            Features shown in this sample;
        </p>
        <ul class="list">
            <li>Using ASP.NET Charting with Ra-Ajax</li>
            <li>Key handling in TextBoxes</li>
            <li>Effects - Fading and Highlight etc</li>
            <li>Enter pressed in TextBox</li>
        </ul>
        <ra:BehaviorUnveiler 
            runat="server" 
            ID="unveilInfo" />
    </ra:Panel>
</asp:Content>
