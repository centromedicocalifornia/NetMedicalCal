﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class UsuarioGrabo
    {
        public string Nombre { get; set; }
        public byte[] Firma  { get; set; }
        public byte[] Huella { get; set; }
        public string CMP { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Nombres { get; set; }
        public string Dni { get; set; }
    }
}
