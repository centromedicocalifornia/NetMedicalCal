using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    public class MatrizLaZanja
    {
        public int Número { get; set; }
        public string ServiceId { get; set; }
        public string PersonId { get; set; }
        public string ProtocolId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_EmployerLocationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_WorkingLocationId { get; set; }
        
        public string ApellidosNombres { get; set; }
        public string Procedencia { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public string Empresa { get; set; }
        public string Dni { get; set; }
        public string PuestoTrabajo { get; set; }
        public int Edad { get; set; }
        public string TipoExamen { get; set; }
        public DateTime? FechaDigitacion { get; set; }
        public DateTime? FechaExamenOcupacional { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Peso { get; set; }
        public string Talla { get; set; }
        public string Imc { get; set; }
        public string DxAntropometria { get; set; }
        public string Fc { get; set; }

        public string Fr { get; set; }
        public string SatO2 { get; set; }
        public string Pas { get; set; }
        public string Pad { get; set; }
        public string DxPa { get; set; }
        public string GsFactor { get; set; }
        public string Hto { get; set; }
        public string Hb { get; set; }
        public string DxHb { get; set; }
        public string Orina { get; set; }

        public string Rpr { get; set; }
        public string Cocaina { get; set; }
        public string Marihuana { get; set; }
        public string Calidad { get; set; }
        public string Oit { get; set; }
        public string DxRx { get; set; }
        public string Audiometria { get; set; }
        public string CalidadEspirometria { get; set; }
        public string DxExpirometria { get; set; }
        public string VcScOd { get; set; }

        public string VcScOi { get; set; }
        public string VcCcOd { get; set; }
        public string VcCcOi { get; set; }
        public string VlScOd { get; set; }
        public string VlScOi { get; set; }
        public string VlCcOi { get; set; }
        public string VlCcOd { get; set; }
        public string DxAgudeza { get; set; }
        public string VisionCromatica { get; set; }
        public string Ekg { get; set; }

        public string OtrosDx { get; set; }
        public string Interconsultas { get; set; }
        public string Condicion { get; set; }
        public string Restriccion { get; set; }
        public string Restriccion2 { get; set; }
        public string Restriccion3 { get; set; }
        public string Restriccion4 { get; set; }
        public string Restriccion5 { get; set; }
        public string Restriccion6 { get; set; }
        public string Observacion { get; set; }

        public string Observacion2 { get; set; }
        public string Observacion3 { get; set; }
        public string FechaLevantamientoObs { get; set; }
        public string ValidacionSi { get; set; }
        public string ValidacionNo { get; set; }
        public string LugarExamen { get; set; }
        public string MEdicoRevisaExamen { get; set; }
        public string FechaAptitud { get; set; }
        public string LicenciadaRegistra { get; set; }

        public string Condicion_ { get; set; }
        public string TipoEmpresa { get; set; }

        public int? sexo_ { get; set; }
        public int? _Grupo { get; set; }
        public int? _Factor { get; set; }
        public string Glucosa { get; set; }
        public string DxGlucemia { get; set; }

        public int? websino { get; set; }
        public string VisualizaOnline { get; set; }

        public string Hir { get; set; }
        public string Neum { get; set; }
        public string TME { get; set; }
        public string Alturas { get; set; }
        public string EquipoMovil { get; set; }
        public string Manipulador { get; set; }
        public string Brigadista { get; set; }
        public string Observaciones { get; set; }

        public string v_OrganizationId { get; set; }
        public string Facturacion { get; set; }

        public string EtadoTrabajadorEmpresa { get; set; }
        public string email { get; set; }
        public string puestoDeTrabajo { get; set; }
        public string area { get; set; }
        public string zona { get; set; }
        public string compañia { get; set; }
        public string unidad { get; set; }
        public string fechaIngreso { get; set; }
        public string vigenciaEmoActual { get; set; }
        public string vigenciaEmoBase { get; set; }
        public string fechaProgramacionEmo { get; set; }
        public string dxAgudezaVisualLejos { get; set; }
    }

    public class MatrizManucci
    {
        public int Número { get; set; }
        public string ServiceId { get; set; }
        public string PersonId { get; set; }
        public string ProtocolId { get; set; }
        public string v_CustomerOrganizationId { get; set; }
        public string v_CustomerLocationId { get; set; }
        public string v_EmployerOrganizationId { get; set; }
        public string v_EmployerLocationId { get; set; }
        public string v_WorkingOrganizationId { get; set; }
        public string v_WorkingLocationId { get; set; }


        public DateTime? FechaDigitacion { get; set; }
        public DateTime? FechaExamenOcupacional { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaVigenciaExamen { get; set; }

        public string N_Hcl { get; set; }
        public string Dni { get; set; }
        public string Apellidos_Nombres { get; set; }
        public int Edad { get; set; }
        public string Empresa { get; set; }
        public string Tipo_Examen { get; set; }
        public DateTime? Fecha_Atencion { get; set; }
        public string Sexo { get; set; }
        public string Estado_Civil { get; set; }
        public string Ocupacion { get; set; }
        public string Ant_Personales { get; set; }
        public string Ant_Familiares { get; set; }
        public string Frec_Alcohol { get; set; }
        public string Cigarrps { get; set; }
        public string Drogas { get; set; }
        public string Medicamentos { get; set; }
        public string Antecedentes_Ocupácionales { get; set; }
        public string AccidentesTrabajo { get; set; }
        public string TrabajoActual { get; set; }
        public string Riesgos_Ocupacionales { get; set; }
        public string ProtecciónPersonal { get; set; }
        public string Anamnesis { get; set; }
        public string PA { get; set; }
        public string Dx_PA { get; set; }
        public string FC { get; set; }
        public string FR { get; set; }
        public string Peso { get; set; }
        public string Talla { get; set; }
        public string IMC { get; set; }
        public string P_Abdominal { get; set; }
        public string Dx_Nutricional { get; set; }
        public string Examen_Fisico { get; set; }
        public string Sistema_Osteomuscular { get; set; }
        public string Hemograma { get; set; }
        public string Leucocitos { get; set; }
        public string Hemoglobina { get; set; }
        public string Hematocrito { get; set; }
        public string Hematies { get; set; }
        public string Segmentados { get; set; }
        public string Eosinofilos { get; set; }
        public string Linfocitos { get; set; }
        public string Rcto_Plaquetas { get; set; }
        public string Grupo_Factor { get; set; }
        public string Glucosa { get; set; }
        public string Colesterol { get; set; }
        public string Trigliceridos { get; set; }
        public string Vrdl { get; set; }
        public string Psa { get; set; }
        public string Cocaina { get; set; }
        public string Colesterol_Hdl { get; set; }
        public string Colesterol_Ldl { get; set; }
        public string Colesterol_Vldl { get; set; }
        public string Orina { get; set; }
        public string Electrocardiograma { get; set; }
        public string Agudeza_Visual { get; set; }
        public string SL_Cerca_OD { get; set; }
        public string SL_Cerca_OI { get; set; }
        public string SL_Lejos_OD { get; set; }
        public string SL_Lejos_OI { get; set; }
        public string CL_Cerca_OD { get; set; }
        public string CL_Cerca_OI { get; set; }
        public string CL_Lejos_OD { get; set; }
        public string CL_Lejos_OI { get; set; }
        public string Ishihara { get; set; }
        public string Test_Estereopsis { get; set; }
        public string Enfermedades_Oculares { get; set; }
        public string Conclusiones_Audiometricas { get; set; }
        public string Otoscopia_OD { get; set; }
        public string Otoscopia_OI { get; set; }
        public string OD_250 { get; set; }
        public string OD_500 { get; set; }
        public string OD_1000 { get; set; }
        public string OD_2000 { get; set; }
        public string OD_3000 { get; set; }
        public string OD_4000 { get; set; }
        public string OD_6000 { get; set; }
        public string OD_8000 { get; set; }
        public string OI_250 { get; set; }
        public string OI_500 { get; set; }
        public string OI_1000 { get; set; }
        public string OI_2000 { get; set; }
        public string OI_3000 { get; set; }
        public string OI_4000 { get; set; }
        public string OI_6000 { get; set; }
        public string OI_8000 { get; set; }
        public string Espirometria { get; set; }
        public string Rx_Torax { get; set; }
        public string Psicologia { get; set; }
        public string Dx_Cie10 { get; set; }
        public string Recomendaciones { get; set; }
        public string Aptitud_Trabajar_Altura { get; set; }
        public string Res_Dx_Enfermedades_Ocup { get; set; }
        public string Res_Dx_Enfermedades_Comunes { get; set; }
        public string Aptitud { get; set; }
        public string Restricciones { get; set; }
        public string Observaciones { get; set; }
        public string Fecha_Vigencia { get; set; }


        public int? sexo_ { get; set; }
        public int? _Grupo { get; set; }
        public int? _Factor { get; set; }

        public int? eSTADOc_ { get; set; }

        public string Facturacion { get; set; }
        public string v_OrganizationId { get; set; }
        public string compañia { get; set; }


    }

}
