using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class InfoServiceComponent
    {
        public string componentName { get; set; }
        public string serviceId_ { get; set; }
    }

    public class listComponents
    {
        public string v_ServiceComponentId { get; set; }
        public List<listComponents> ListservComp { get; set; }
    }

    public class UsuariosAnt
    {
        public int userId1 { get; set; }
        public int userId2 { get; set; }
    }

}
