using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using Sigesoft.Node.WinClient.BE;
using Sigesoft.Node.WinClient.DAL;
using Sigesoft.Common;
using System.Data.SqlClient;

namespace Sigesoft.Node.WinClient.BLL
{
    public class AseguradoraBL
    {
        public List<LiquidacionAseguradora> GetLiquidacionAseguradoraPagedAndFiltered(ref OperationResult pobjOperationResult,string pstrFilterExpression,DateTime? pdatBeginDate, DateTime? pdatEndDate)
        {
            try
            {
                SigesoftEntitiesModel dbContext = new SigesoftEntitiesModel();
                var query = from A in dbContext.service
                            join CA in dbContext.calendar on A.v_ServiceId equals  CA.v_ServiceId
                            join B in dbContext.person on A.v_PersonId equals B.v_PersonId
                            join C in dbContext.protocol on A.v_ProtocolId equals C.v_ProtocolId
                            join E in dbContext.plan on C.v_ProtocolId equals E.v_ProtocoloId
                            join D in dbContext.organization on E.v_OrganizationSeguroId equals D.v_OrganizationId
                            where A.i_IsDeleted == 0 && 
                                  A.d_ServiceDate >= pdatBeginDate.Value && 
                                  A.d_ServiceDate <= pdatEndDate.Value && 
                                  CA.i_CalendarStatusId != 4
                    select new LiquidacionAseguradora
                    {
                        ServicioId = A.v_ServiceId,
                        FechaServicio = A.d_ServiceDate.Value,
                        Paciente = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName,
                        PacientDocument = B.v_FirstName + " " + B.v_FirstLastName + " " + B.v_SecondLastName + " " + B.v_DocNumber,
                        EmpresaId = E.v_OrganizationSeguroId,
                        Aseguradora = D.v_Name,
                        Protocolo = C.v_ProtocolId,
                        Factor_ = C.r_PriceFactor,
                        PPS_ = C.r_MedicineDiscount
                    };

                    if (!string.IsNullOrEmpty(pstrFilterExpression))
                    {
                        query = query.Where(pstrFilterExpression);
                    }
                    
                 List<LiquidacionAseguradora> data = query.OrderBy(o => o.FechaServicio).ToList();
                 

                 data = data.GroupBy(p => p.ServicioId).Select(s => s.First()).ToList();

                var ListaLiquidacion = new List<LiquidacionAseguradora>();
                LiquidacionAseguradora oLiquidacionAseguradora;
                
                LiquiAseguradoraDetalle oLiquiAseguradoraDetalle;
                decimal? TotalAseguradora = 0;
                foreach (var servicio in data)
                {
                    oLiquidacionAseguradora = new LiquidacionAseguradora();

                    oLiquidacionAseguradora.ServicioId = servicio.ServicioId;
                    oLiquidacionAseguradora.FechaServicio = servicio.FechaServicio;
                    oLiquidacionAseguradora.Paciente = servicio.Paciente;
                    oLiquidacionAseguradora.Aseguradora = servicio.Aseguradora;
                    oLiquidacionAseguradora.Protocolo = servicio.Protocolo;
                    oLiquidacionAseguradora.Factor = decimal.Round((decimal)servicio.Factor_, 2);
                    oLiquidacionAseguradora.PPS = servicio.PPS_ + "%";


                    var serviceComponents = obtenerServiceComponentsByServiceId(servicio.ServicioId);
                    var detalle = new List<LiquiAseguradoraDetalle>();
                    foreach (var componente in serviceComponents)
                    {

                        oLiquiAseguradoraDetalle = new LiquiAseguradoraDetalle();
                        oLiquiAseguradoraDetalle.Descripcion = componente.v_ComponentName;
                        
                        oLiquiAseguradoraDetalle.Tipo = componente.i_EsDeducible == 1 ? "DEDUCIBLE" : "COASEGURO";
                        string simbolo = "";
                        if (oLiquiAseguradoraDetalle.Tipo == "DEDUCIBLE")
                        {
                            simbolo = " S/.";
                        }
                        else
                        {
                            simbolo = " %";
                        }

                        oLiquiAseguradoraDetalle.Valor = componente.d_Importe.ToString() + simbolo;
                        oLiquiAseguradoraDetalle.SaldoPaciente = componente.d_SaldoPaciente.Value;
                        oLiquiAseguradoraDetalle.SaldoAseguradora = componente.d_SaldoAseguradora.Value;
                        TotalAseguradora += oLiquiAseguradoraDetalle.SaldoAseguradora;
                        oLiquiAseguradoraDetalle.SubTotal = componente.d_SaldoPaciente.Value + componente.d_SaldoAseguradora.Value;
                        oLiquiAseguradoraDetalle.Cantidad = 1M;
                        oLiquiAseguradoraDetalle.PrecioUnitario = decimal.Parse(componente.r_Price.ToString());
                        detalle.Add(oLiquiAseguradoraDetalle);
                    }
                    #region
                    //var tickets = obtenerTicketsByServiceId(servicio.ServicioId);
                    //foreach (var ticket in tickets)
                    //{
                    //    oLiquiAseguradoraDetalle = new LiquiAseguradoraDetalle();
                    //    oLiquiAseguradoraDetalle.Descripcion = ticket.v_NombreProducto;
                       
                    //    oLiquiAseguradoraDetalle.Tipo = ticket.i_EsDeducible == 1 ? "DEDUCIBLE" : "COASEGURO";
                    //    string simbolo = "";
                    //    if (oLiquiAseguradoraDetalle.Tipo == "DEDUCIBLE")
                    //    {
                    //        simbolo = " S/.";
                    //    }
                    //    else
                    //    {
                    //        simbolo = " %";
                    //    }    
                    //    oLiquiAseguradoraDetalle.Valor = ticket.d_Importe.ToString() + simbolo;
                    //    oLiquiAseguradoraDetalle.SaldoPaciente = ticket.d_SaldoPaciente.Value;
                    //    oLiquiAseguradoraDetalle.SaldoAseguradora = ticket.d_SaldoAseguradora.Value;
                    //    TotalAseguradora += oLiquiAseguradoraDetalle.SaldoAseguradora.Value;
                    //    oLiquiAseguradoraDetalle.SubTotal = ticket.d_SaldoPaciente.Value + ticket.d_SaldoAseguradora.Value;
                    //    oLiquiAseguradoraDetalle.Cantidad = ticket.d_Cantidad;
                    //    oLiquiAseguradoraDetalle.PrecioUnitario = ticket.d_PrecioVenta;
                    //    detalle.Add(oLiquiAseguradoraDetalle);
                    //}

                    //var recetas = obtenerRecetasByServiceId(servicio.ServicioId);
                    //foreach (var receta in recetas)
                    //{
                    //    oLiquiAseguradoraDetalle = new LiquiAseguradoraDetalle();
                    //    oLiquiAseguradoraDetalle.Descripcion = dbContext.obtenerproducto(receta.v_IdProductoDetalle).ToList()[0].v_Descripcion;// receta.v_IdProductoDetalle;
                       
                    //    oLiquiAseguradoraDetalle.Tipo = receta.i_EsDeducible == 1 ? "DEDUCIBLE" : "COASEGURO";
                    //    string simbolo = "";
                    //    if (oLiquiAseguradoraDetalle.Tipo == "DEDUCIBLE")
                    //    {
                    //        simbolo = " S/.";
                    //    }
                    //    else
                    //    {
                    //        simbolo = " %";
                    //    }    
                    //    oLiquiAseguradoraDetalle.Valor = receta.d_Importe.ToString() + simbolo;
                    //    oLiquiAseguradoraDetalle.Cantidad = receta.i_Cantidad.Value;
                    //    oLiquiAseguradoraDetalle.SaldoPaciente = receta.d_SaldoPaciente;
                    //    oLiquiAseguradoraDetalle.SaldoAseguradora = receta.d_SaldoAseguradora;
                    //    TotalAseguradora += oLiquiAseguradoraDetalle.SaldoAseguradora;
                    //    oLiquiAseguradoraDetalle.SubTotal = (oLiquiAseguradoraDetalle.SaldoPaciente.Value + oLiquiAseguradoraDetalle.SaldoAseguradora.Value);
                    //    #region Conexion SIGESOFT
                    //    ConexionSigesoft conectasam = new ConexionSigesoft();
                    //    conectasam.opensigesoft();
                    //    #endregion
                    //    var cadena1 = "select PR.r_MedicineDiscount, OO.v_Name, PR.v_CustomerOrganizationId from Organization OO inner join protocol PR On PR.v_AseguradoraOrganizationId = OO.v_OrganizationId where PR.v_ProtocolId ='" + oLiquidacionAseguradora.Protocolo + "'";
                    //    SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
                    //    SqlDataReader lector = comando.ExecuteReader();
                    //    string factor = "";
                    //    while (lector.Read())
                    //    {
                    //        factor = lector.GetValue(0).ToString();
                    //    }
                    //    lector.Close();
                    //    conectasam.closesigesoft();
                    //    #region Conexion SAMBHS
                    //    ConexionSambhs conectaConexionSambhs = new ConexionSambhs();
                    //    conectaConexionSambhs.openSambhs();
                    //    #endregion

                    //    var cadenasam = "select PP.d_PrecioMayorista from producto PP inner join productodetalle PD on PD.v_IdProducto = PP.v_IdProducto where PD.v_IdProductoDetalle ='" + receta.v_IdProductoDetalle + "'";
                    //    comando = new SqlCommand(cadenasam, connection: conectaConexionSambhs.conectarSambhs);
                    //    lector = comando.ExecuteReader();
                    //    string preciounitario = "";
                    //    while (lector.Read())
                    //    {
                    //        preciounitario = lector.GetValue(0).ToString();
                    //    }
                    //    lector.Close();
                    //    conectaConexionSambhs.closeSambhs();

                    //    oLiquiAseguradoraDetalle.PrecioUnitario = decimal.Parse(preciounitario) - (decimal.Parse(preciounitario) * decimal.Parse(factor) / 100);
                    //    detalle.Add(oLiquiAseguradoraDetalle);
                    //}

                    #endregion
                    oLiquidacionAseguradora.TotalAseguradora = TotalAseguradora;
                    oLiquidacionAseguradora.Detalle = detalle;
                    
                    ListaLiquidacion.Add(oLiquidacionAseguradora);
                }
             
                    pobjOperationResult.Success = 1;
            return ListaLiquidacion;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public List<LiquidacionesSeguro> Seguro_Tickets(ref OperationResult pobjOperationResult,  DateTime? pdatBeginDate, DateTime? pdatEndDate,
            string nombre, string empresa, string liquidacion, string comprobante)
        {
            ConexionSigesoft conectasam = new ConexionSigesoft();
            conectasam.opensigesoft();
              
            //valida filtros 
            string condicionNombre = "";
            if (nombre != null && nombre != "")
            {
                condicionNombre = "AND LTRIM(per.v_FirstName) + ' ' + per.v_FirstLastName + ' ' + per.v_SecondLastName like '%"+nombre+"%' ";
            }

            string condicionempresa = "";
            if (empresa != null && empresa != "")
            {
                condicionempresa = "AND org.v_Name like '%" + empresa + "%' ";
            }

            string condicionLiquidacion = "";
            if (liquidacion != null && liquidacion != "")
            {
                condicionLiquidacion = "AND s.v_NroLiquidacion like '%" + liquidacion + "' ";
            }

            string condicionComprobante = "";
            if (comprobante != null && comprobante != "")
            {
                condicionComprobante = "AND v1.v_SerieDocumento + '-' + v1.v_CorrelativoDocumento LIKE '% " + comprobante + " %'";
            }

           
            //
            var cadena1 =
                @" select DISTINCT s.v_ServiceId as 'ServicioId',  
                 LTRIM(per.v_FirstName) + ' ' + per.v_FirstLastName + ' ' + per.v_SecondLastName AS 'Paciente',  
                 p.v_Name as 'Protocolo'  , 
                 s.d_InsertDate as 'Fecha'  , 
                 perr.v_FirstName + ' ' + perr.v_FirstLastName AS 'Trabajador' ,  
                CASE WHEN s.v_ComprobantePago IS NULL THEN 'SIN COBRAR' ELSE s.v_ComprobantePago END AS 'SRV',   
                CASE WHEN v.v_SerieDocumento IS NULL THEN 'SIN COBRAR' ELSE v.v_SerieDocumento + '-' + v.v_CorrelativoDocumento END AS 'DOC',   
                CASE WHEN v.d_Total IS NULL THEN CAST(0.00 AS DECIMAL(14,2)) ELSE CAST(v.d_Total AS DECIMAL(14,2)) END AS 'Total'  , 
                CASE WHEN cp.d_Saldo IS NULL THEN CAST(0.00 AS DECIMAL(14,2)) ELSE CAST(cp.d_Saldo AS DECIMAL(14,2)) END AS 'Saldo'   ,
                CASE WHEN cp.t_InsertaFecha IS NULL THEN '2000-01-01 00:00:00' ELSE  cp.t_InsertaFecha END AS 'F_Comprobante' ,  
                CASE WHEN cd.i_IdFormaPago IS NULL THEN 'NP' WHEN cd.i_IdFormaPago = 9 THEN 'DEPOSITO' ELSE d.v_Value1 END AS 'Condicion', 
				 
                 CASE WHEN cd.i_IdTipoDocumentoRef IS NULL THEN 'NP'   
		                WHEN CAST(cd.i_IdFormaPago AS VARCHAR(14))  = '9' AND CAST(cd.i_IdTipoDocumentoRef AS NVARCHAR(14)) = '-1' THEN 'DEPOSITO'  
		                WHEN CAST(cd.i_IdFormaPago AS VARCHAR(14))  = '1' AND CAST(cd.i_IdTipoDocumentoRef AS NVARCHAR(14)) = '-1' THEN 'EFECTIVO SOLES' 
		                WHEN CAST(cd.i_IdFormaPago AS VARCHAR(14))  = '2' AND CAST(cd.i_IdTipoDocumentoRef AS NVARCHAR(14)) = '421' THEN 'VISA' 
		                 ELSE '- - -' END AS 'Tipo',  
						 
						 --(SELECT TOP 1 pp.v_FirstName + ' ' + pp.v_FirstLastName + ' ' + pp.v_SecondLastName   
					  --              FROM servicecomponent as sc   
					  --              JOIN systemuser as scm on sc.i_MedicoTratanteId = scm.i_SystemUserId  
					  --              JOIN person as pp on scm.v_PersonId = pp.v_PersonId  
					  --              WHERE sc.v_ServiceId = s.v_ServiceId) AS 'Medico'  ,

                CASE WHEN p.v_Procedencia ='H' THEN 'HOSPITALARIO'   
	                WHEN p.v_Procedencia = 'E' THEN 'EMERGENCIA'  
	                WHEN p.v_Procedencia = 'A' THEN 'AMBULATORIO '  
	                ELSE '- - -' END AS 'Procedencia', 
                CASE WHEN lin.v_Nombre IS NULL THEN '- - -' ELSE lin.v_Nombre END AS 'Plan',  
                p.r_PriceFactor as 'Factor' ,
                CONVERT (varchar,cast(p.r_MedicineDiscount as int)) + '%'  as 'Descuento_PPS' , 
                CASE WHEN pl.d_Importe IS NULL THEN 'NP' ELSE 'S/.   ' + CONVERT (varchar,cast(pl.d_Importe as money)) END as 'Deducible',   
                CASE WHEN pl.d_ImporteCo IS NULL THEN 'NP' ELSE CONVERT (varchar,cast(pl.d_ImporteCo as int)) + '%' END AS 'Coaseguro'  , 
                
				CASE WHEN liq.v_NroLiquidacion IS NULL THEN 'SIN LIQUIDAR' ELSE liq.v_NroLiquidacion END AS 'Liquidacion' ,  
                CASE WHEN v1.v_SerieDocumento IS NULL THEN 'SIN COBRAR' ELSE v1.v_SerieDocumento + '-' + v1.v_CorrelativoDocumento END AS 'DOC_L', 
                CASE WHEN v1.d_Total IS NULL THEN CAST(0.00 AS DECIMAL(14,2)) ELSE CAST(v1.d_Total AS DECIMAL(14,2)) END AS 'Total_L' ,   
                CASE WHEN cp1.d_Saldo IS NULL THEN CAST(0.00 AS DECIMAL(14,2)) ELSE CAST(cp1.d_Saldo AS DECIMAL(14,2)) END AS 'Saldo_L',   
                CASE WHEN cp1.t_InsertaFecha IS NULL THEN '2000-01-01 00:00:00' ELSE  cp1.t_InsertaFecha END AS 'F_Comprobante_L'  , 
                CASE WHEN liq.d_FechaVencimiento IS NULL THEN '2000-01-01 00:00:00' ELSE  liq.d_FechaVencimiento END AS 'F_Comprobante_L',   
                CASE WHEN cd1.i_IdFormaPago IS NULL THEN 'NP' WHEN cd1.i_IdFormaPago = 9 THEN 'DEPOSITO' ELSE d1.v_Value1 END AS 'Condicion_L',   
                CASE WHEN cd1.i_IdTipoDocumentoRef IS NULL THEN 'NP'   
		                WHEN CAST(cd1.i_IdFormaPago AS VARCHAR(14))  = '9' AND CAST(cd1.i_IdTipoDocumentoRef AS NVARCHAR(14)) = '-1' THEN 'DEPOSITO'  
		                WHEN CAST(cd1.i_IdFormaPago AS VARCHAR(14))  = '1' AND CAST(cd1.i_IdTipoDocumentoRef AS NVARCHAR(14)) = '-1' THEN 'EFECTIVO SOLES'  
		                WHEN CAST(cd1.i_IdFormaPago AS VARCHAR(14))  = '2' AND CAST(cd1.i_IdTipoDocumentoRef AS NVARCHAR(14)) = '421' THEN 'VISA'  
		                ELSE '- - -' END AS 'Tipo_L', 
                org.v_Name as 'Aseguradora'
				,
				ISNULL(sumed.v_UserName, '-') as 'USUARIO_MED'
				,
				ISNULL(ppmed.v_FirstName + ' ' + ppmed.v_FirstLastName + ' ' + ppmed.v_SecondLastName,'-') as 'NOMBRE_MED'
				,
				ISNULL(dht.v_Value1, '-')  AS 'ESPECIALIDAD_MED'

                from service as s  
                JOIN protocol as p on s.v_ProtocolId = p.v_ProtocolId  
                JOIN calendar as c on s.v_ServiceId = c.v_ServiceId  
                JOIN person as per on s.v_PersonId = per.v_PersonId  
                JOIN systemuser as sy on c.i_InsertUserId = sy.i_SystemUserId  
                JOIN person as perr on sy.v_PersonId = perr.v_PersonId  
                LEFT JOIN [20505310072].dbo.venta as v on  s.v_ComprobantePago LIKE '%'+v.v_SerieDocumento + '-' + v.v_CorrelativoDocumento+'%'   
                --LEFT JOIN [20505310072].dbo.venta as v on  s.v_ComprobantePago = v.v_SerieDocumento + '-' + v.v_CorrelativoDocumento
                LEFT JOIN [20505310072].dbo.cobranzapendiente as cp on v.v_IdVenta = cp.v_IdVenta  
                LEFT JOIN [20505310072].dbo.cobranzadetalle as cd on v.v_IdVenta = cd.v_IdVenta  
                LEFT JOIN [20505310072].dbo.datahierarchy d on d.i_GroupId = 41 AND d.i_ItemId = v.i_IdCondicionPago  
                LEFT JOIN [SigesoftDesarrollo_2].dbo.[plan] as pl on s.i_PlanId = pl.i_PlanId  
                LEFT JOIN [20505310072].dbo.linea as lin on pl.v_IdUnidadProductiva = lin.v_IdLinea  
                LEFT JOIN liquidacion as liq on s.v_NroLiquidacion = liq.v_NroLiquidacion  
                LEFT JOIN [20505310072].dbo.venta as v1 on  liq.v_NroFactura = v1.v_SerieDocumento + '-' + v1.v_CorrelativoDocumento  
                LEFT JOIN [20505310072].dbo.cobranzapendiente as cp1 on v1.v_IdVenta = cp1.v_IdVenta 
                LEFT JOIN [20505310072].dbo.cobranzadetalle as cd1 on v1.v_IdVenta = cd1.v_IdVenta  
                LEFT JOIN [20505310072].dbo.datahierarchy d1 on d1.i_GroupId = 41 AND d1.i_ItemId = v1.i_IdCondicionPago  
                LEFT JOIN organization as org on pl.v_OrganizationSeguroId = org.v_OrganizationId  
				LEFT JOIN servicecomponent as sc  on s.v_ServiceId = sc.v_ServiceId and v_ComponentId = 'N009-ME000000405'
				left join systemuser as sumed on sc.i_MedicoTratanteId = sumed.i_SystemUserId
				left join person as ppmed on sumed.v_PersonId = ppmed.v_PersonId
				left join professional prof on ppmed.v_PersonId =  prof.v_PersonId
				left join datahierarchy dht on prof.i_Profesion = dht.i_ItemId and dht.i_GroupId = 126 " +
                " where '" + pdatBeginDate + "' <= s.d_ServiceDate AND s.d_ServiceDate <= '"+pdatEndDate+"' " +
	            " AND p.i_MasterServiceTypeId = 11 AND c.i_CalendarStatusId <> 4 " +
                " AND p.v_ProtocolId <> 'N009-PR000000636' " + condicionNombre + condicionempresa + condicionLiquidacion + condicionComprobante + " ORDER BY s.d_InsertDate";
            SqlCommand comando = new SqlCommand(cadena1, connection: conectasam.conectarsigesoft);
            comando.CommandTimeout = 300;

            SqlDataReader lector1 = comando.ExecuteReader();

            //if (!string.IsNullOrEmpty(pstrFilterExpression))
            //{
            //    cadena1 = cadena1.Where(pstrFilterExpression);
            //}
            LiquidacionesSeguro _oSeguros = new LiquidacionesSeguro();
            List<LiquidacionesSeguro> _ListaSeguros = new List<LiquidacionesSeguro>();
            while (lector1.Read())
            {
                _oSeguros = new LiquidacionesSeguro();
                _oSeguros.ServicioId = lector1.GetValue(0).ToString();
                _oSeguros.Paciente = lector1.GetValue(1).ToString();
                _oSeguros.Protocolo = lector1.GetValue(2).ToString();
                _oSeguros.Fecha = DateTime.Parse(lector1.GetValue(3).ToString().Split(' ')[0]);
                _oSeguros.Trabajador = lector1.GetValue(4).ToString();
                _oSeguros.SRV = lector1.GetValue(5).ToString();
                _oSeguros.DOC = lector1.GetValue(6).ToString();
                _oSeguros.Total = decimal.Parse(lector1.GetValue(7).ToString());
                _oSeguros.Saldo = decimal.Parse(lector1.GetValue(8).ToString());
                _oSeguros.F_Comprobante = lector1.GetValue(9).ToString().Split(' ')[0];
                _oSeguros.Condicion = lector1.GetValue(10).ToString();
                _oSeguros.Tipo = lector1.GetValue(11).ToString();
                _oSeguros.Procedencia = lector1.GetValue(12).ToString();
                _oSeguros.Plan = lector1.GetValue(13).ToString();
                _oSeguros.Factor = decimal.Parse(lector1.GetValue(14).ToString());
                _oSeguros.Descuento_PPS = lector1.GetValue(15).ToString();
                _oSeguros.Deducible = lector1.GetValue(16).ToString();
                _oSeguros.Coaseguro = lector1.GetValue(17).ToString();
                _oSeguros.Liquidacion = lector1.GetValue(18).ToString();
                _oSeguros.DOC_L = lector1.GetValue(19).ToString();
                _oSeguros.Total_L = decimal.Parse(lector1.GetValue(20).ToString());
                _oSeguros.Saldo_L = decimal.Parse(lector1.GetValue(21).ToString());
                _oSeguros.F_Comprobante_L = lector1.GetValue(22).ToString().Split(' ')[0];
                _oSeguros.F_Venc_Comprobante_L = lector1.GetValue(23).ToString().Split(' ')[0];
                _oSeguros.Condicion_L = lector1.GetValue(24).ToString();
                _oSeguros.Tipo_L = lector1.GetValue(25).ToString();
                _oSeguros.Aseguradora = lector1.GetValue(26).ToString();
                _oSeguros.USUARIO_MED = lector1.GetValue(27).ToString();
                _oSeguros.Medico = lector1.GetValue(28).ToString();
                _oSeguros.ESPECIALIDAD_MED = lector1.GetValue(29).ToString();

                _ListaSeguros.Add(_oSeguros);
            }
            lector1.Close();
            conectasam.closesigesoft();

            //var lista = _Lista.ToList().GroupBy(g => g.Servicio).Select(s => s.First()).ToList();

            //LiquidacionesSeguro _oHospitalizacion1 = new LiquidacionesSeguro();
            //List<LiquidacionesSeguro> _Lista1 = new List<LiquidacionesSeguro>();
            //foreach (var item in lista)
            //{
            //    _oHospitalizacion1 = new LiquidacionesSeguro();
            //    //_oHospitalizacion1.Tipo = item.Tipo;
                
            //    _Lista1.Add(_oHospitalizacion1);
            //}
            pobjOperationResult.Success = 1;
            return _ListaSeguros;

        }
        private List<recetaList> obtenerRecetasByServiceId(string serviceId)
        {
            try
            {
                 var dbContext = new SigesoftEntitiesModel();
                 var query = (from A in dbContext.receta
                              join B in dbContext.diagnosticrepository on A.v_DiagnosticRepositoryId equals B.v_DiagnosticRepositoryId
                              join C in dbContext.service on A.v_ServiceId equals C.v_ServiceId
                              join I in dbContext.protocol on C.v_ProtocolId equals I.v_ProtocolId
                              join G in dbContext.plan on new { a = I.v_ProtocolId, b = A.v_IdUnidadProductiva }
                                  equals new { a = G.v_ProtocoloId, b = G.v_IdUnidadProductiva } into G_join
                              from G in G_join.DefaultIfEmpty()
                              where A.v_ServiceId == serviceId && C.i_IsDeleted == 0 && (A.d_SaldoPaciente.Value > 0 || A.d_SaldoAseguradora.Value > 0)
                              select new recetaList
                              {
                                  v_IdProductoDetalle = A.v_IdProductoDetalle,
                                  d_SaldoPaciente = A.d_SaldoPaciente,
                                  d_SaldoAseguradora = A.d_SaldoAseguradora,
                                  v_IdUnidadProductiva = A.v_IdUnidadProductiva,
                                  i_EsDeducible = G.i_EsDeducible.Value,
                                  i_EsCoaseguro = G.i_EsCoaseguro.Value,
                                  d_Importe = G.d_Importe,
                                  i_Cantidad = A.d_Cantidad.Value
                              }
                             ).ToList();

                 return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private List<TicketDetalleList> obtenerTicketsByServiceId(string serviceId)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.ticket
                             join B in dbContext.ticketdetalle on A.v_TicketId equals  B.v_TicketId
                             join C in dbContext.service on serviceId equals C.v_ServiceId
                             join I in dbContext.protocol on C.v_ProtocolId equals I.v_ProtocolId
                             join G in dbContext.plan on new { a = I.v_ProtocolId, b = B.v_IdUnidadProductiva }
                                    equals new { a = G.v_ProtocoloId, b = G.v_IdUnidadProductiva } into G_join
                             from G in G_join.DefaultIfEmpty()
                             where A.v_ServiceId == serviceId && A.i_IsDeleted == 0
                             select new TicketDetalleList
                             {
                                 v_NombreProducto = B.v_Descripcion,
                                 d_SaldoPaciente = B.d_SaldoPaciente.Value,
                                 d_SaldoAseguradora = B.d_SaldoAseguradora.Value,
                                 i_EsDeducible = G.i_EsDeducible.Value,
                                 i_EsCoaseguro = G.i_EsCoaseguro.Value,
                                 d_Importe = G.d_Importe,
                                 d_Cantidad = B.d_Cantidad.Value,
                                 d_PrecioVenta = (decimal)B.d_PrecioVenta
                             }).ToList();

                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private List<ServiceComponentList> obtenerServiceComponentsByServiceId(string serviceId)
        {
            try
            {
                var dbContext = new SigesoftEntitiesModel();

                var query = (from A in dbContext.servicecomponent
                             join C in dbContext.systemuser on A.i_MedicoTratanteId equals C.i_SystemUserId
                             join D in dbContext.person on C.v_PersonId equals D.v_PersonId
                             join B in dbContext.component on A.v_ComponentId equals B.v_ComponentId
                             join F in dbContext.systemparameter on new { a = B.i_CategoryId.Value, b = 116 }
                                     equals new { a = F.i_ParameterId, b = F.i_GroupId } into F_join
                             from F in F_join.DefaultIfEmpty()
                             join H in dbContext.service on A.v_ServiceId equals H.v_ServiceId
                             join I in dbContext.protocol on H.v_ProtocolId equals I.v_ProtocolId
                             join G in dbContext.plan on new { a = I.v_ProtocolId, b = A.v_IdUnidadProductiva }
                                     equals new { a = G.v_ProtocoloId, b = G.v_IdUnidadProductiva } 
                             where A.v_ServiceId == serviceId &&
                                   A.i_IsDeleted == 0 &&
                                   A.i_IsRequiredId == 1 &&
                                   (A.d_SaldoPaciente.Value > 0 || A.d_SaldoAseguradora.Value > 0)

                             select new ServiceComponentList
                             {
                                 v_ServiceComponentId = A.v_ServiceComponentId,
                                 v_ComponentId = A.v_ComponentId,
                                 r_Price = A.r_Price.Value,
                                 v_ComponentName = B.v_Name,
                                 v_CategoryName = F.v_Value1,
                                 MedicoTratante = D.v_FirstName + " " + D.v_FirstLastName + " " + D.v_SecondLastName,
                                 d_InsertDate = A.d_InsertDate,
                                 d_SaldoPaciente = A.d_SaldoPaciente.Value,
                                 d_SaldoAseguradora = A.d_SaldoAseguradora.Value,
                                 i_EsDeducible = G.i_EsDeducible.Value,
                                 i_EsCoaseguro = G.i_EsCoaseguro.Value,
                                 d_Importe = G.d_Importe
                             }).ToList();

                return query;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
