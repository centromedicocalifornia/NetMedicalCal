using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE.Custom
{
    public class MatrizHudbay
    {
        public string ServiceId { get; set; }
        public string PersonId { get; set; }
        public string ProtocolId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_EmployerLocationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_WorkingLocationId { get; set; }

        public int N { get; set; }
        public string Dni { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public int? Sexo { get; set; }

        public string Procedencia { get; set; }
        public string DireccionActual { get; set; }
        public string Empresa { get; set; }
        public string Ocupacion { get; set; }
        public string TipoExamen { get; set; } 
        public DateTime? FechaEvaluacion { get; set; }
        public string ResultadoAptitudMedica { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        public string Observacionlevantar01mes { get; set; }
        public string Observacionlevantar03mes { get; set; }
        public string Observacionlevantar06mes { get; set; }
        public string ResultadoAptitudEspecifica { get; set; }
        public string Restriccion01 { get; set; }
        public string Restriccion02 { get; set; }
        public string Restriccion03 { get; set; }

        public string Hemograma { get; set; }
        public string Hemoglobina { get; set; }
        public string GrupoFactorSanguineo { get; set; }
        public int? _Grupo { get; set; }
        public int? _Factor { get; set; }
        public string Glucosa { get; set; }
        public string HbGlicosilada { get; set; }
        public string RprVDRL { get; set; }

        public string OrinaCompleto { get; set; }
        public string OrinaToxicologico { get; set; }
        public string PruebaHcgb { get; set; }

        public string ColesterolTotal { get; set; }
        public string HDL { get; set; }
        public string LDL { get; set; }
        public string Trigliceridos { get; set; }
   
        public string Coproparasito_logico_seriado { get; set; }     //----x
        public string Coprocultivo { get; set; }                //----x
        public string CultivoHisopadoFaringeo { get; set; }       //----x

        public string Caries { get; set; }

        public string OdVa500 { get; set; }
        public string OdVa1000 { get; set; }
        public string OdVa2000 { get; set; }
        public string OdVa3000 { get; set; }
        public string OdVa4000 { get; set; }
        public string OdVa6000 { get; set; }
        public string OdVa8000 { get; set; }
        public string OdSTS { get; set; }
        public string OdDxClinico { get; set; }
        public string OdDxOcupacional { get; set; }

        public string OiVa500 { get; set; }
        public string OiVa1000 { get; set; }
        public string OiVa2000 { get; set; }
        public string OiVa3000 { get; set; }
        public string OiVa4000 { get; set; }
        public string OiVa6000 { get; set; }
        public string OiVa8000 { get; set; }
        public string OiSTS { get; set; }
        public string OiDxClinico { get; set; }
        public string OiDxOcupacional { get; set; }
        
        public string RadioToraxOit { get; set; }
        public string RadioToraxHallazgos { get; set; }
        public string RadioToraxDx { get; set; }

        public string EspirometriaCvf { get; set; }
        public string EspirometriaFev1 { get; set; }
        public string CmbioFEV1 { get; set; }
        public string EspirometriaDx { get; set; }

        public string CardiologiaPaS { get; set; }
        public string CardiologiaPaD { get; set; }
        public string CardiologiaEkg { get; set; }
        public string CardiologiaPruebaEsfuerzo { get; set; }
        public string HipertensionArterial_SINO { get; set; }
        public string Diabetes_SINO { get; set; }
        public string Fumador_SINO { get; set; }
        public string ScoreFramingham { get; set; }

        public string OftalmoVCODSC { get; set; }
        public string OftalmoVCOISC { get; set; }
        public string OftalmoVCODCC { get; set; }
        public string OftalmoVCOICC { get; set; }

        public string OftalmoVLODSC { get; set; }
        public string OftalmoVLOISC { get; set; }
        public string OftalmoVLODCC { get; set; }
        public string OftalmoVLOICC { get; set; }

        public string AgudezaBinocularSC { get; set; }
        public string AgudezaBinocularCC { get; set; }
        public string VisionColoresTestIshihara { get; set; }
        public string VisionEstereoscopica { get; set; }
        public string CampoVisualOD { get; set; }
        public string CampoVisualOI { get; set; }   
        
        public string Peso { get; set; }
        public string Talla { get; set; }
        public string Imc { get; set; }
        public string DxTriaje { get; set; }
        public string Fasciograma { get; set; }

        public string AntecedentesPersonales { get; set; }
        public string AntecedentesOcupacionales { get; set; }
        public string AntecedentesAlergias { get; set; }

        public string InmunizacionesTetano { get; set; }
        public string InmunizacionesFiebreTifoidea { get; set; }
        public string InmunizacionesHepatitisA { get; set; }
        public string InmunizacionesHepatitisB { get; set; }
        public string InmunizacionesInfluenza { get; set; }

        public string CuadrosintomaticoRespitarorio { get; set; }
        public string Baciloscopia { get; set; }
        public string PPD { get; set; }           //----x

        public string MiniTestPsiquiatrico { get; set; }
        public string EvaPsicologiDx { get; set; }
                
        public string EvaPsicosensometricoCondicion { get; set; }
        public string FichaSAHS { get; set; }
        public string EvaPsicosensometricoConclusion { get; set; }

        public string AlturaAudit { get; set; }
        public string AlturaNeurologia { get; set; }
        public string AlturaAlturaEstructuralAptitud { get; set; }
        public string EvalNEurologicaporMedioNeurologico { get; set; }
        public string AlturaEEG { get; set; }
        public string AlturaPlantigrafia { get; set; }  //----x
        public string ManipuladordeAlimentos { get; set; }

        public string LugarRealizoEMO { get; set; }
        public string MedicoRealizaEMO { get; set; }
        
        public string Puntaje { get; set; }

        public string v_OrganizationId { get; set; }
        public string Facturacion { get; set; }
    }

    public class MatrizHudbay_Part1
    {
        public string ServiceId { get; set; }
        public string PersonId { get; set; }
        public string ProtocolId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_EmployerLocationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_WorkingLocationId { get; set; }

        public int N { get; set; }

        public string Vacio { get; set; } //celda de plantilla vácía
        public string Dni { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string Genero { get; set; }
        public int? Sexo { get; set; }

        public string Procedencia { get; set; }
        public string DireccionActual { get; set; }
        public string Empresa { get; set; }
        public string Ocupacion { get; set; }
        public string TipoExamen { get; set; }
        public DateTime? FechaEvaluacion { get; set; }
        public string ResultadoAptitudMedica { get; set; }
        public DateTime? FechaVencimiento { get; set; }

        public string Observacionlevantar01mes { get; set; }
        public string Observacionlevantar03mes { get; set; }
        public string Observacionlevantar06mes { get; set; }
        public string ResultadoAptitudEspecifica1 { get; set; }
        public string ResultadoAptitudEspecifica2 { get; set; }
        public string ResultadoAptitudEspecifica3 { get; set; }
        public string ResultadoAptitudEspecifica4 { get; set; }
        public string ResultadoAptitudEspecifica5 { get; set; }
        public string ResultadoAptitudEspecifica6 { get; set; }
        public string ResultadoAptitudEspecifica7 { get; set; }

        public string ResultadoAptitudEspecifica { get; set; }
        public string Restriccion01 { get; set; }
        public string Restriccion02 { get; set; }
        public string Restriccion03 { get; set; }
        public string Restriccion04 { get; set; }
        public string Restriccion05 { get; set; }
        public string Restriccion06 { get; set; }

        public string Leucocitos { get; set; }
        public string Hematies { get; set; }
        public string Hemoglobina { get; set; }
        public string Hematocrito { get; set; }
        public string NeutrofilosSegmentados { get; set; }
        public string NeutrofilosSegmentados_cel_ml { get; set; } //x
        public string Plaquetas { get; set; }
        public string VcmFL { get; set; } //volumen corpuscular medio
        public string Hcm { get; set; } //hemoglobina corpuscular medio
        public string CCMH { get; set; } //concentracion de hemoglobina corpuscular media
        public string Rdw { get; set; }//x
        public string Vpm { get; set; }//x
        public string Bastones1 { get; set; }
        public string Linfocitos1 { get; set; }
        public string Monocitos1 { get; set; }
        public string Eosinofilos1 { get; set; }
        public string Basofilos1 { get; set; }
        public string MetaMielocitos1 { get; set; }//x
        public string Mielocitos1 { get; set; }
        public string ProMielocitos1 { get; set; }//x
        public string Blastos1 { get; set; }//x
        public string Bastones2 { get; set; }//x
        public string Linfocitos2 { get; set; }//x
        public string Monocitos2 { get; set; }//x
        public string Eosinofilos2 { get; set; }//x
        public string Basofilos2 { get; set; }//x
        public string Mielocitos2 { get; set; }//x
        public string MetaMielocitos2 { get; set; }//x
        public string ProMielocitos2 { get; set; }//x
        public string Blastos2 { get; set; }//x
        public string ComprbacionRecuento { get; set; }//x

        public string GrupoFactorSanguineo { get; set; }
        public int? _Grupo { get; set; }
        public int? _Factor { get; set; }
        public string Glucosa { get; set; }
        public string HbGlicosilada { get; set; }
        public string RprVDRL { get; set; }

        public string Color { get; set; }
        public string Aspecto { get; set; }
        public string Densidad { get; set; }
        public string Ph { get; set; }
        public string GlucosaOr { get; set; }
        public string Bilirrubina { get; set; }
        public string Cuerpos_Cetonicos { get; set; }
        public string Proteinas { get; set; }
        public string Urobilinogeno { get; set; } //x
        public string Nitritos { get; set; }
        public string HemoglobinaOr { get; set; } //x
        public string LeucocitosOr { get; set; }
        public string HematiesOr { get; set; }
        public string CelulasEpiteliales { get; set; }
        public string Levadura { get; set; }
        public string Cristales { get; set; } //x
        public string CristalesAcidoUrico { get; set; }//x
        public string CristalesFos_F_Amorfos { get; set; }
        public string CristalesFos_T_Amorfos { get; set; }
        public string CristalesOx_Calcio { get; set; }
        public string CristalesFos_F_Triples { get; set; }
        public string Cilindros { get; set; } //x
        public string CilindrosHialinos { get; set; }
        public string CilindrosGranulosos { get; set; }
        public string FilamentoMucoides { get; set; }
        public string Germenes { get; set; }


        public string ToxCoca { get; set; }
        public string ToxMarihuana { get; set; }
        public string PruebaHcgb { get; set; }

        public string ColesterolTotal { get; set; }
        public string HDL { get; set; }
        public string LDL { get; set; }
        public string Trigliceridos { get; set; }

        public string Caries { get; set; }

        public string OdVa500 { get; set; }
        public string OdVa1000 { get; set; }
        public string OdVa2000 { get; set; }
        public string OdVa3000 { get; set; }
        public string OdVa4000 { get; set; }
        public string OdVa6000 { get; set; }
        public string OdVa8000 { get; set; }
        public string OdSTS { get; set; }
        public string OdDxClinico { get; set; }
        public string OdDxOcupacional { get; set; }

        public string OiVa500 { get; set; }
        public string OiVa1000 { get; set; }
        public string OiVa2000 { get; set; }
        public string OiVa3000 { get; set; }
        public string OiVa4000 { get; set; }
        public string OiVa6000 { get; set; }
        public string OiVa8000 { get; set; }
        public string OiSTS { get; set; }
        public string OiDxClinico { get; set; }
        public string OiDxOcupacional { get; set; }

        public string RadioToraxOit { get; set; }
        public string RadioToraxHallazgos { get; set; }
        public string RadioToraxDx { get; set; }

        public string EspirometriaCvf { get; set; }
        public string EspirometriaFev1 { get; set; }
        public string CmbioFEV1 { get; set; }
        public string EspirometriaDx { get; set; }

        public string CardiologiaPaS { get; set; }
        public string CardiologiaPaD { get; set; }
        public string CardiologiaEkg { get; set; }
        public string CardiologiaPruebaEsfuerzo { get; set; }
        public string HipertensionArterial_SINO { get; set; }
        public string Diabetes_SINO { get; set; }
        public string Fumador_SINO { get; set; }
        public string ScoreFramingham { get; set; }

        public string OftalmoVCODSC { get; set; }
        public string OftalmoVCOISC { get; set; }
        public string OftalmoVCODCC { get; set; }
        public string OftalmoVCOICC { get; set; }

        public string OftalmoVLODSC { get; set; }
        public string OftalmoVLOISC { get; set; }
        public string OftalmoVLODCC { get; set; }
        public string OftalmoVLOICC { get; set; }

        public string AgudezaBinocularSC { get; set; }
        public string AgudezaBinocularCC { get; set; }
        public string VisionColoresTestIshihara { get; set; }
        public string VisionEstereoscopica { get; set; }
        public string CampoVisualOD { get; set; }
        public string CampoVisualOI { get; set; }

        public string Peso { get; set; }
        public string Talla { get; set; }
        public string Imc { get; set; }
        public string DxTriaje { get; set; }
        public string Fototipo { get; set; }
        public string FototipoHallazgo { get; set; }

        public string AntecedentesPersonales { get; set; }
        public string AntecedentesOcupacionales { get; set; }
        public string AntecedentesAlergiasSI { get; set; }
        public string AntecedentesAlergiasNO { get; set; }


        public string InmunizacionesTetano { get; set; }
        public string InmunizacionesFiebreTifoidea { get; set; }
        public string InmunizacionesHepatitisA { get; set; }
        public string InmunizacionesHepatitisB { get; set; }
        public string InmunizacionesInfluenza { get; set; }

        public string CuadrosintomaticoRespitarorio { get; set; }
        public string Baciloscopia { get; set; }

        public string MiniTestPsiquiatrico { get; set; }
        public string PsicologiaOtros { get; set; }
        public string EvaPsicologiDx { get; set; }

        public string EvaPsicosensometricoCondicion { get; set; }
        public string FichaSAHS { get; set; }
        public string Barsit { get; set; } //x
        public string Direcciones { get; set; } //x
        public string Semaforo { get; set; } //x
        public string PercepcionRiesgo { get; set; } //x
        public string Barrat { get; set; } //x
        public string EvaPsicosensometricoConclusion { get; set; }

        public string AlturaAudit { get; set; }
        public string AlturaNeurologia { get; set; }
        public string AlturaAlturaEstructuralAptitud { get; set; }
        public string EvalNEurologicaporMedioNeurologico { get; set; }
        public string AlturaEEG { get; set; }
        public string ManipuladordeAlimentos { get; set; }

        public string LugarRealizoEMO { get; set; }
        public string MedicoRealizaEMO { get; set; }

        public string MinaMedico { get; set; }
        public string MinaFecha { get; set; }
        public string MinaMedicoLev { get; set; }
        public string MinaFechaLev { get; set; }

        public string Cobre { get; set; }
        public string Molibdeno { get; set; }
        public string Plomo { get; set; }
        public string Cadmio { get; set; }

        public string Puntaje { get; set; }

        //filtro
        public string v_OrganizationId { get; set; }
        public string Facturacion { get; set; }
    }

}
