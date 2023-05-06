using Sigesoft.Server.WebClientAdmin.UI;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace Sigesoft.Server.WebClientAdmin.UI.block
{
    public partial class form : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnSubmitForm1_Click(object sender, EventArgs e)
        {
            ShowNotify("Form 1 verified and submitted successfully!");
        }

        protected void btnSubmitForm2_Click(object sender, EventArgs e)
        {
            ShowNotify("Form 2 verified and submitted successfully!");
        }

        protected void btnSubmitForm3_Click(object sender, EventArgs e)
        {
            ShowNotify("Form 3 verified and submitted successfully!");
        }

    }
}
