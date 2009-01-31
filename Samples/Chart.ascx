<%@ Control 
    Language="C#" 
    AutoEventWireup="true" 
    CodeFile="Chart.ascx.cs" 
    Inherits="Chart" %>

<%@ Register 
    Assembly="System.Web.DataVisualization" 
    Namespace="System.Web.UI.DataVisualization.Charting" 
    TagPrefix="chart" %>

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
