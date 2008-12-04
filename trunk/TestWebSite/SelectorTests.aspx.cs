using System;
using Ra.Widgets;
using RaSelector;

public partial class SelectorTests : System.Web.UI.Page
{
    protected void firstButton_Click(object sender, EventArgs e)
    {
        Selector.SelectFirst<Button>(easyTest).Text = Selector.SelectFirst<Button>(easyTest).ID;
    }

    protected void third_click(object sender, EventArgs e)
    {
        Selector.SelectFirst<Button>(recursiveTest).Text = Selector.SelectFirst<Button>(recursiveTest).ID;
    }
}
