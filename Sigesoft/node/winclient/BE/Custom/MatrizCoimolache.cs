using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class MatrizCoimolache
    {
        public string ServiceId { get; set; }
        public string PersonId { get; set; }
        public string ProtocolId { get; set; }

        public string v_CustomerOrganizationId { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }

        public string v_CustomerLocationId { get; set; }
        public string v_WorkingLocationId { get; set; }
        public string v_EmployerLocationId { get; set; }

        public int N { get; set; }
        public string Unidad { get; set; }
        public string Compañia { get; set; }
        public string Contrata { get; set; }
        public string Dni { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombres { get; set; }
        public int? Genero { get; set; }
        public string Sexo { get; set; }
        public string TipoExamen  { get; set; }
        public DateTime? FechaExamenMedico { get; set; }
        public DateTime? FechaVigenciaExamen { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string GradoInstruccion { get; set; }
        public string PuestoTrabajo { get; set; }
        public string AreaTrabajo { get; set; }
        public string Peso { get; set; }
        public string Talla { get; set; }
        public string Imc { get; set; }
        public string ClasificacionOmsImc { get; set; }
        public string Cintura { get; set; }
        public string Tabaqiuismo { get; set; }
        public string ActividadFisica { get; set; }
        public string AntecedentesPatologicos { get; set; }
        public string Hospitalizaciones { get; set; }
        public string AntecedentesQuirurgicos { get; set; }
        public string HistorialAccidentesPrevios { get; set; }
        public string Medicamentos { get; set; }
        public string Hemoglobina { get; set; }
        public string VCM { get; set; }
        public string HCM { get; set; }
        public string ProteinasOrina { get; set; }
        public string GrupoFactorSanguineo { get; set; }
        public int? _Grupo { get; set; }
        public int? _Factor { get; set; }
        public string Glicemia { get; set; }
        public string OdVa125 { get; set; }
        public string OdVa250 { get; set; }
        public string OdVa500  { get; set; }
        public string OdVa1000 { get; set; }
        public string OdVa2000 { get; set; }
        public string OdVa4000 { get; set; }
        public string OdVa8000 { get; set; }
        public string STS1 { get; set; }
        public string OiVa125 { get; set; }
        public string OiVa250 { get; set; }
        public string OiVa500 { get; set; }
        public string OiVa1000 { get; set; }
        public string OiVa2000 { get; set; }
        public string OiVa4000 { get; set; }
        public string OiVa8000 { get; set; }
        public string STS2 { get; set; }
        public string DxOiDerecho { get; set; }
        public string DxOiIzquierdo { get; set; }
        public string Profunsion { get; set; }
        public string CalidadRx { get; set; }
        public string Pleura { get; set; }
        public string Simbolos { get; set; }
        public string RadioToraxDx { get; set; }
        public string OftalmoVLODSC { get; set; }
        public string OftalmoVLOISC { get; set; }
        public string OftalmoVLODCC { get; set; }
        public string OftalmoVLOICC { get; set; }
        public string VisionColores { get; set; }
        public string OftalmologiaDx { get; set; }
        public string PresionArterial { get; set; }
        public string CardiologiaDx { get; set; }
        public string InterpretacionPresionArterial { get; set; }
        public string HallazgoOsteomuscular { get; set; }
        public string EspirometriaDx { get; set; }
        public string PsicologiaDx { get; set; }
        public string Cocaina { get; set; }
        public string Marihuana { get; set; }
        public string PlomoSangre { get; set; }
        public string Aptitud { get; set; }
        public string AptitudConducir { get; set; }
        public string Restricciones { get; set; }
        public string Clinica { get; set; }

        public string OtrosDX { get; set; }
        public string v_OrganizationId { get; set; }
        public string Facturacion { get; set; }
    }
}
