using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class CotizacionocDtoList
    {
        public string i_CotizacionIdOc { get; set; }

        public string i_EmpresaId { get; set; }
        public string i_Locationd { get; set; }

        public string i_PersonId { get; set; }

        public string v_Description { get; set; }

        public string v_Comentary { get; set; }

        public int i_NumberOfWorker { get; set; }

        public float? r_TotalCost { get; set; }

        public DateTime d_DeliveryDate { get; set; }

        public int i_LineaCreditoId { get; set; }

        public string LineaCredito { get; set; }

        public int i_EMOType { get; set; }

        public int i_InsertUserId { get; set; }

        public DateTime d_InsertDate { get; set; }

        public int i_UpdateUserId { get; set; }

        public string d_UpdateDate { get; set; }

        public string v_ComentaryUpdate { get; set; }

        public int i_MostrarPrecio { get; set; }

        public string v_RazonSocial { get; set; }

        public string v_RepresentanteLegal { get; set; }

        public string v_DireccionEmpresa { get; set; }

        public string v_Sumilla { get; set; }

        public string v_Asunto { get; set; }

        public int i_IsDeleted { get; set; }

        public string Usuario { get; set; }
        public string celularUsuarioGraba { get; set; }
        public string EmailUsuarioGraba { get; set; }
        public byte[] FirmaUsuario { get; set; }

        public string v_Interconsultas { get; set; }
        public string v_Anotaciones { get; set; }

        public List<CotizacionocdetaillList> CotizacionocdetaillList { get; set; }

    }
}
