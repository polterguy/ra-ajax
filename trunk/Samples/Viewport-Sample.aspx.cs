/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using Ra.Extensions;

namespace Samples
{
    public partial class Viewport : System.Web.UI.Page
    {
        private bool _triedToLogin;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Remove this line, just here for "testing purposes" to make it easy to "log out"
            if (!IsPostBack)
            {
                Session["user"] = null;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            // Creating our dynamically loaded nodes...
            if (Session["user"] != null && ((string)Session["user"]) == "admin")
            {
                for (int idx = 0; idx < 10; idx++)
                {
                    TreeNode n = new TreeNode();
                    n.ID = "first" + idx;
                    n.Text = "Node " + idx;
                    dynamicNodes.Controls.Add(n);
                }

                // Since these nodes are NOT rendered BEFORE we log in our user
                // we need to WAI until the user has been logged in, then create our nodes
                // and then RE-render the dynamic TreeNodes
                // Though we do NOT want to re-render the dynamic nodes the next time, but we
                // still need to re-create it on the server in order to trap events and such...
                if (ViewState["hasLoaded"] == null)
                    dynamicNodes.ReRender();
                ViewState["hasLoaded"] = true;
            }
            base.OnInit(e);
        }

        // We want to wait as long as possible with this logic to make sure we're NOT
        // running this logic e.e. when user is actually logging in etc...
        protected override void OnPreRender(EventArgs e)
        {
            if (Session["user"] == null && !_triedToLogin && !loginWnd.Visible)
            {
                // Uncomment the line below to have "true" login logic...
                // This line is commented just to make everything show for "bling reasons"...
                //everything.Visible = false;
                loginWnd.Visible = true;
                new EffectFadeIn(loginWnd, 1000)
                    .ChainThese(new EffectFocusAndSelect(username))
                    .Render();
                username.Text = "username";
                password.Text = "password";
            }
            base.OnPreRender(e);
        }

        protected void menu_MenuItemSelected(object sender, EventArgs e)
        {
        }

        protected void tree_SelectedNodeChanged(object sender, EventArgs e)
        {
            lblStatus.Text = tree.SelectedNodes[0].ID;
        }

        protected void resizer_Resized(object sender, ResizeHandler.ResizedEventArgs e)
        {
            lbl.Text = string.Format("Width: {0}, Height: {1}", e.Width, e.Height);
            new EffectHighlight(lbl, 500).Render();
            int width = Math.Max(e.Width - 264, 400);
            int height = Math.Max(e.Height - 101, 200);
            int heightLeft = Math.Max(e.Height - 390, 50);
            wndRight.Style["width"] = width.ToString() + "px";
            pnlRight.Style["height"] = height.ToString() + "px";
            pnlLeft.Style["height"] = heightLeft.ToString() + "px";
        }

        protected void loginBtn_Click(object sender, EventArgs e)
        {
            _triedToLogin = true;
            if (username.Text == "admin" && password.Text == "admin")
            {
                loginWnd.Visible = false;
                everything.Visible = true;
                Session["user"] = "admin";
            }
            else
            {
                loginInfo.Text = "Username; <strong>'admin'</strong>, Password; <strong>'admin'</strong>";
                new EffectHighlight(loginInfo, 500).Render();
                username.Select();
                username.Focus();
            }
        }
    }
}