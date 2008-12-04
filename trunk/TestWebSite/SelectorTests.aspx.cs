﻿using System;
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

    protected void fourth_click(object sender, EventArgs e)
    {
        Selector.SelectFirst<Button>(recursiveTest,
            delegate(System.Web.UI.Control idx)
            {
                if (idx is RaWebControl)
                    return (idx as RaWebControl).CssClass == "testCss";
                return false;
            }).Text = "new text";
    }
}
