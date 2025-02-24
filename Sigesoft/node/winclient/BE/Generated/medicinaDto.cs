//-------------------------------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EntitiesToDTOs.v3.1 (entitiestodtos.codeplex.com).
//     Timestamp: 2024/12/06 - 17:12:51
//
//     Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//-------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sigesoft.Node.WinClient.BE
{
    [DataContract()]
    public partial class medicinaDto
    {
        [DataMember()]
        public String v_MedicinaId { get; set; }

        [DataMember()]
        public String v_ServicioId { get; set; }

        [DataMember()]
        public String v_Cabeza { get; set; }

        [DataMember()]
        public String v_Cuello { get; set; }

        [DataMember()]
        public String v_Nariz { get; set; }

        [DataMember()]
        public String v_BocaAmigdalas { get; set; }

        [DataMember()]
        public String v_MiembrosSuperiores { get; set; }

        [DataMember()]
        public String v_MiembrosInferiores { get; set; }

        [DataMember()]
        public String v_ReflejosOsteos { get; set; }

        [DataMember()]
        public String v_Columna { get; set; }

        [DataMember()]
        public String v_EnMarcha { get; set; }

        [DataMember()]
        public String v_Abdomen { get; set; }

        [DataMember()]
        public String v_DesGeneral { get; set; }

        [DataMember()]
        public String v_Anillos { get; set; }

        [DataMember()]
        public String v_Hernias { get; set; }

        [DataMember()]
        public String v_Varices { get; set; }

        [DataMember()]
        public String v_Genitales { get; set; }

        [DataMember()]
        public String v_Ganglios { get; set; }

        [DataMember()]
        public String v_LenguajeAtencionMemoria { get; set; }

        [DataMember()]
        public String v_ConclusionesMedicina { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaInsercion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UsuarioInsertaId { get; set; }

        [DataMember()]
        public Nullable<DateTime> d_FechaActualizacion { get; set; }

        [DataMember()]
        public Nullable<Int32> i_UsuarioActualizaId { get; set; }

        [DataMember()]
        public Nullable<Boolean> i_EsEliminado { get; set; }

        [DataMember()]
        public servicioDto servicio { get; set; }

        public medicinaDto()
        {
        }

        public medicinaDto(String v_MedicinaId, String v_ServicioId, String v_Cabeza, String v_Cuello, String v_Nariz, String v_BocaAmigdalas, String v_MiembrosSuperiores, String v_MiembrosInferiores, String v_ReflejosOsteos, String v_Columna, String v_EnMarcha, String v_Abdomen, String v_DesGeneral, String v_Anillos, String v_Hernias, String v_Varices, String v_Genitales, String v_Ganglios, String v_LenguajeAtencionMemoria, String v_ConclusionesMedicina, Nullable<DateTime> d_FechaInsercion, Nullable<Int32> i_UsuarioInsertaId, Nullable<DateTime> d_FechaActualizacion, Nullable<Int32> i_UsuarioActualizaId, Nullable<Boolean> i_EsEliminado, servicioDto servicio)
        {
			this.v_MedicinaId = v_MedicinaId;
			this.v_ServicioId = v_ServicioId;
			this.v_Cabeza = v_Cabeza;
			this.v_Cuello = v_Cuello;
			this.v_Nariz = v_Nariz;
			this.v_BocaAmigdalas = v_BocaAmigdalas;
			this.v_MiembrosSuperiores = v_MiembrosSuperiores;
			this.v_MiembrosInferiores = v_MiembrosInferiores;
			this.v_ReflejosOsteos = v_ReflejosOsteos;
			this.v_Columna = v_Columna;
			this.v_EnMarcha = v_EnMarcha;
			this.v_Abdomen = v_Abdomen;
			this.v_DesGeneral = v_DesGeneral;
			this.v_Anillos = v_Anillos;
			this.v_Hernias = v_Hernias;
			this.v_Varices = v_Varices;
			this.v_Genitales = v_Genitales;
			this.v_Ganglios = v_Ganglios;
			this.v_LenguajeAtencionMemoria = v_LenguajeAtencionMemoria;
			this.v_ConclusionesMedicina = v_ConclusionesMedicina;
			this.d_FechaInsercion = d_FechaInsercion;
			this.i_UsuarioInsertaId = i_UsuarioInsertaId;
			this.d_FechaActualizacion = d_FechaActualizacion;
			this.i_UsuarioActualizaId = i_UsuarioActualizaId;
			this.i_EsEliminado = i_EsEliminado;
			this.servicio = servicio;
        }
    }
}
