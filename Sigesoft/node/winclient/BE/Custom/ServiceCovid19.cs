using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class ServiceCovid19
    {
        public string SERVICIO { get; set; }
        public string ID_PACIENTE { get; set; }
        public string PACIENTE { get; set; }
        public string DNI { get; set; }
        public DateTime? NACIMIENTO { get; set; }
        public int? EDAD { get; set; }
        public string OCUPACION { get; set; }
        public string SEXO { get; set; }
        public string TELEFONO { get; set; } 
        public string DIRECCION { get; set; }
        public string CONTACTO_EMERGENCIA { get; set; }
        public string TELEFONO_EMERGENCIA { get; set; }
        public string EXAMEN { get; set; }
        public decimal PRECIO { get; set; }
        public DateTime? FECHA { get; set; }
        public string COMPROBANTE { get; set; }
        public string PROTOCOLOID { get; set; }
        public string PROTOCOLO { get; set; }
        public string RESULTADO { get; set; }
        public string PRIMER_RES { get; set; }
        public string SEGUNDO_RES { get; set; }
        public string ATENCION { get; set; }
        public string USUARIO { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string PROVINCIA { get; set; }
        public string DISTRITO { get; set; }
        public string UBIGEO { get; set; }
        public string MINAID { get; set; }
        public string MINANAME { get; set; }
        public string CONTRATAID { get; set; }
        public string CONTRATANAME { get; set; }
        public string TERCEROID { get; set; }
        public string TERCERONAME { get; set; }
        public int i_StatusLiquidation { get; set; }
        public int i_MasterServiceId { get; set; }
        public string AP_PATERNO { get; set; }
        public string AP_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string Direccion_Toma { get; set; }
        public string Lugar { get; set; }
        public string GradoInstruc { get; set; }
        public string EstadoCivil { get; set; }
        public string FechaNacimiento { get; set; }
        public string TIPOEXAMEN { get; set; }
        public string Sintomas { get; set; }
        public string VACUNAS { get; set; }        
    }
    public class SintomasCovid
    {
        public string Sintoma { get; set; }
        public string Valor { get; set; }
    }
}
