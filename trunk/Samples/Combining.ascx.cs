using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace UC
{
    public partial class Combining : System.Web.UI.UserControl
    {
        public event EventHandler Saved;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                name.Focus();
            err.Text = "";
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            int x;
            if (!Int32.TryParse(age.Text, out x))
            {
                err.Text = "Age is not an integer value...";
                age.Focus();
                age.Select();
            }
            else
            {
                if (Saved != null)
                    Saved(this, new EventArgs());
            }
        }

        public string Name
        {
            get { return name.Text; }
        }

        public int Age
        {
            get { return Int32.Parse(age.Text); }
        }
    }
}