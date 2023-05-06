using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace Sigesoft.Server.WebClientAdmin.UI.window
{
    public partial class window_maximized_fixed : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnShowWindowMax.OnClientClick = Window1.GetShowReference() + Window1.GetMaximizeReference();

                btnShowWindow900.OnClientClick = Window1.GetShowReference(900, 450);

                btnShowWindow.OnClientClick = Window1.GetShowReference();

                btnCloseWindow.OnClientClick = Window1.GetHideReference();

                btnShowWindowLargeHeight.OnClientClick = Window1.GetShowReference(900, 10000);


            }
        }

    }
}
