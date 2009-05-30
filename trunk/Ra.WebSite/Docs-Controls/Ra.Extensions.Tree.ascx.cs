/*
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the the MIT license which 
 * means you can use it (almost) exactly as you wish in your 
 * own projects and copy and paste as much as you want.
 */

using System;
using Ra.Extensions;

public partial class Docs_Controls_Tree : System.Web.UI.UserControl
{
    protected void window_GetChildNodes(object sender, EventArgs e)
    {
        TreeNodes level = sender as TreeNodes;
        for (int idx = 0; idx < 5; idx++)
        {
            TreeNode i = new TreeNode();
            i.ID = level.ID + idx;
            i.Text = "Window " + idx;
            level.Controls.Add(i);

            TreeNodes l = new TreeNodes();
            l.ID = level.ID + "LL" + idx;
            l.GetChildNodes += window_GetChildNodes;
            i.Controls.Add(l);
        }
    }

    protected void tree_ItemClicked(object sender, EventArgs e)
    {
        lbl.Text = tree.SelectedNodes[0].Text;
    }
}
