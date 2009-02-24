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

    protected void enumerableButton_Click(object sender, EventArgs e)
    {
        foreach (Button idx in Selector.Select<Button>(recursiveTest))
        {
            idx.Text = "enumerable passed";
        }
    }

    protected void enumerableButton2_Click(object sender, EventArgs e)
    {
        foreach (Button idx in Selector.Select<Button>(recursiveTest,
            delegate(System.Web.UI.Control idxBtn)
            {
                return (idxBtn is RaWebControl) && (idxBtn as RaWebControl).CssClass == "enumCssClass";
            }))
        {
            idx.Text = "enumerable CSS passed";
        }
    }

    protected void findControlBtn_Click(object sender, EventArgs e)
    {
        Selector.FindControl<Button>(Form, "fifthButton").Text = "findControl worked";
    }
}
