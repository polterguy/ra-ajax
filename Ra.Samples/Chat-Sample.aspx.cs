/*
 * Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
 * Copyright 2008 - 2009 - Thomas Hansen thomas@ra-ajax.org
 * This code is licensed under the GPL version 3 which 
 * can be found in the license.txt file on disc.
 *
 */

using System;
using Ra.Widgets;
using Ra.Extensions;
using Ra.Effects;

namespace Samples
{
    public partial class Chat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBindChats();
                new EffectFadeIn(chatWindow, 500)
                    .ChainThese(new EffectFocusAndSelect(txt))
                    .Render();
            }
        }

        private void DataBindChats()
        {
            lock (this.GetType())
            {
                string content = "";
                string oldChat = chatCnt.Text;
                foreach (Message idx in Message.Messages)
                {
                    string tmp = string.Format(@"<div class=""chat"">{0}</div>", idx.Content);
                    content += tmp;
                }
                chatCnt.Text = content;
                if (oldChat != chatCnt.Text)
                {
                    // New chat since last time...
                    chatCnt.Style["display"] = "none";
                    new EffectFadeIn(chatCnt, 200)
                        .JoinThese(new EffectHighlight())
                        .Render();
                }
            }
        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            DataBindChats();
        }

        protected void SubmitChat(object sender, EventArgs e)
        {
            lock (this.GetType())
            {
                if (Message.Messages.Count >= 5)
                {
                    Message.Messages.RemoveAt(0);
                }
                Message.Messages.Add(new Message(txt.Text));
            }
            DataBindChats();
            txt.Text = "Type here...";
            txt.Select();
            txt.Focus();
        }
    }
}
