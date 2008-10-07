/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the LGPL version 3 which 
 * can be found in the license.txt file on disc.
 * 
 */

using System;
using Ra.Widgets;
using System.Collections.Generic;

namespace Samples
{
    public partial class AjaxComet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (comet.IsQueueFull)
            {
                lbl.Text = "Cannot connect to Comet Queue since max limit is reached";
            }
            else
            {
                lbl.Text = "Number of currently connected users; " + comet.NumberOfConnections;
            }
            if (!IsPostBack)
            {
                foreach (string idx in Chats)
                {
                    System.Web.UI.WebControls.Literal lit = new System.Web.UI.WebControls.Literal();
                    lit.Text = "<p>" + idx + "</p>";
                    chat.Controls.Add(lit);
                }
            }
        }

        protected void comet_Tick(object sender, Ra.Extensions.Comet.CometEventArgs e)
        {
            // Effect on area...
            new EffectHighlight(chat, 600).Render();

            // Locking to make sure no one tampers with our Chats while iterating them...
            lock (typeof(AjaxComet))
            {
                // Removing all "old" controls
                chat.Controls.Clear();

                foreach (string idx in Chats)
                {
                    System.Web.UI.WebControls.Literal lit = new System.Web.UI.WebControls.Literal();
                    lit.Text = "<p>" + idx + "</p>";
                    chat.Controls.Add(lit);
                }

                // Signalizing that chat output should re-render...
                chat.ReRender();
            }
            lbl.Text += ", Event sent; " + e.Id;
        }

        private List<string> Chats
        {
            get
            {
                List<string> retVal = Application["CometChats"] as List<string>;
                if (retVal == null)
                {
                    retVal = new List<string>();
                    Application["CometChats"] = retVal;
                }
                return retVal;
            }
            set
            {
                Application["CometChats"] = value;
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            lock (typeof(AjaxComet))
            {
                if (Chats.Count >= 5)
                {
                    Chats = new List<string>(Chats.GetRange(1, 4));
                }
                Chats.Add(newChat.Text);
            }

            // Signaling to all Comet Listeners that a new Message has arrived...
            comet.SendMessage(Guid.NewGuid().ToString());

            newChat.Select();
            newChat.Focus();
        }
    }
}
