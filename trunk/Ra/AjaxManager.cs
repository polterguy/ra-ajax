using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web;

[assembly: WebResource("Ra.Js.JsCore.Ra.js", "text/javascript")]
[assembly: WebResource("Ra.Js.Control.js", "text/javascript")]

namespace Ra
{
    public sealed class AjaxManager
    {
        private static AjaxManager _instance;

        public static AjaxManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AjaxManager();
                return _instance;
            }
        }

        private Page CurrentPage
        {
            get { return ((Page)HttpContext.Current.CurrentHandler); }
        }

        public void IncludeMainRaScript()
        {
            CurrentPage.ClientScript.RegisterClientScriptResource(typeof(AjaxManager), "Ra.Js.JsCore.Ra.js");
        }

        public void IncludeMainControlScripts()
        {
            CurrentPage.ClientScript.RegisterClientScriptResource(typeof(AjaxManager), "Ra.Js.Control.js");
        }
    }
}
