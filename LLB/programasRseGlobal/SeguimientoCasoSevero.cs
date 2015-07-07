using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;

using System.IO;

namespace Brainsbits.LLB.programasRseGlobal
{
    public class SeguimientoCasoSevero
    {
        #region variables

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        public enum EstadosCaso
        {
            ABIERTO = 0,
            CERRADO,
            SEGUIMIENTO
        }

        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }
        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }
        #endregion propiedades

        #region constructores
        public SeguimientoCasoSevero()
        {

        }
        public SeguimientoCasoSevero(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerSeguimientoCasoSeveroPorIdSolicitud(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_seguimiento_casos_severos_obtenerPorIdSolicitud ";

            sql += ID_SOLICITUD;

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public Decimal AdicionarRegistroProgSeguimientoCasosSeveros(Decimal ID_EMPLEADO,
            String COND_FACT,
            String ORIGEN,
            String DESCRIPCION_ACCIDENTE,
            String PRONOSTICO,
            String OBS_PRONOSTICO,
            Boolean CALIFICADO,
            String OBS_CALIFICADO,
            Boolean PCL,
            String OBS_PCL,
            Boolean APELO_PCL,
            String OBS_APELO_PCL,
            Boolean LABORANDO,
            String OBS_LABORANDO,
            Boolean REUBICADO,
            Boolean REUBICADO_ORD_JUD,
            String OBS_REUBICADO,
            Boolean REABILITADO,
            String OBS_REABILITADO,
            Boolean INCAPACITADO,
            String OBS_INCAPACITADO,
            Boolean RECOMENDACIONES,
            DateTime FCH_RECOM_DESDE,
            DateTime FCH_RECOM_HASTA,
            String OBS_RECOMENDACIONES,
            String ACTITUD,
            EstadosCaso ESTADO_CASO,
            Decimal ID_SOLICITUD,
            Conexion conexion)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_MAESTRA_CASO_SEVERO = 0;

            sql = "usp_prog_seguimiento_casos_severos_adicionar ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(COND_FACT) == false)
            {
                sql += "'" + COND_FACT + "', ";
                informacion += "COND_FACT = '" + COND_FACT + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "COND_FACT = 'null', ";
            }

            if (String.IsNullOrEmpty(ORIGEN) == false)
            {
                sql += "'" + ORIGEN + "', ";
                informacion += "ORIGEN = '" + ORIGEN + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ORIGEN = 'null', ";
            }

            if (String.IsNullOrEmpty(DESCRIPCION_ACCIDENTE) == false)
            {
                sql += "'" + DESCRIPCION_ACCIDENTE + "', ";
                informacion += "DESCRIPCION_ACCIDENTE = '" + DESCRIPCION_ACCIDENTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DESCRIPCION_ACCIDENTE = 'null', ";
            }

            if (String.IsNullOrEmpty(PRONOSTICO) == false)
            {
                sql += "'" + PRONOSTICO + "', ";
                informacion += "PRONOSTICO = '" + PRONOSTICO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "PRONOSTICO = 'null', ";
            }

            if (String.IsNullOrEmpty(OBS_PRONOSTICO) == false)
            {
                sql += "'" + OBS_PRONOSTICO + "', ";
                informacion += "OBS_PRONOSTICO = '" + OBS_PRONOSTICO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_PRONOSTICO = 'null', ";
            }

            if (CALIFICADO == true)
            {
                sql += "'True', ";
                informacion += "CALIFICADO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "CALIFICADO = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_CALIFICADO) == false)
            {
                sql += "'" + OBS_CALIFICADO + "', ";
                informacion += "OBS_CALIFICADO = '" + OBS_CALIFICADO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_CALIFICADO = 'null', ";
            }

            if (PCL == true)
            {
                sql += "'True', ";
                informacion += "PCL = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "PCL = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_PCL) == false)
            {
                sql += "'" + OBS_PCL + "', ";
                informacion += "OBS_PCL = '" + OBS_PCL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_PCL = 'null', ";
            }

            if (APELO_PCL == true)
            {
                sql += "'True', ";
                informacion += "APELO_PCL = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APELO_PCL = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_APELO_PCL) == false)
            {
                sql += "'" + OBS_APELO_PCL + "', ";
                informacion += "OBS_APELO_PCL = '" + OBS_APELO_PCL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_APELO_PCL = 'null', ";
            }

            if (LABORANDO == true)
            {
                sql += "'True', ";
                informacion += "LABORANDO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "LABORANDO = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_LABORANDO) == false)
            {
                sql += "'" + OBS_LABORANDO + "', ";
                informacion += "OBS_LABORANDO = '" + OBS_LABORANDO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_LABORANDO = 'null', ";
            }

            if (REUBICADO == true)
            {
                sql += "'True', ";
                informacion += "REUBICADO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "REUBICADO = 'False', ";
            }

            if (REUBICADO_ORD_JUD == true)
            {
                sql += "'True', ";
                informacion += "REUBICADO_ORD_JUD = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "REUBICADO_ORD_JUD = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_REUBICADO) == false)
            {
                sql += "'" + OBS_REUBICADO + "', ";
                informacion += "OBS_REUBICADO = '" + OBS_REUBICADO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_REUBICADO = 'null', ";
            }

            if (REABILITADO == true)
            {
                sql += "'True', ";
                informacion += "REABILITADO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "REABILITADO = 'False', ";
            }


            if (String.IsNullOrEmpty(OBS_REABILITADO) == false)
            {
                sql += "'" + OBS_REABILITADO + "', ";
                informacion += "OBS_REABILITADO = '" + OBS_REABILITADO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_REABILITADO = 'null', ";
            }

            if (INCAPACITADO == true)
            {
                sql += "'True', ";
                informacion += "INCAPACITADO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "INCAPACITADO = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_INCAPACITADO) == false)
            {
                sql += "'" + OBS_INCAPACITADO + "', ";
                informacion += "OBS_INCAPACITADO = '" + OBS_INCAPACITADO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_INCAPACITADO = 'null', ";
            }

            if (RECOMENDACIONES == true)
            {
                sql += "'True', ";
                informacion += "RECOMENDACIONES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "RECOMENDACIONES = 'False', ";
            }

            if (FCH_RECOM_DESDE != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_RECOM_DESDE) + "', ";
                informacion += "FCH_RECOM_DESDE = '" + FCH_RECOM_DESDE.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FCH_RECOM_DESDE = 'null', ";
            }

            if (FCH_RECOM_HASTA != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_RECOM_HASTA) + "', ";
                informacion += "FCH_RECOM_HASTA = '" + FCH_RECOM_HASTA.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FCH_RECOM_HASTA = 'null', ";
            }

            if (String.IsNullOrEmpty(OBS_RECOMENDACIONES) == false)
            {
                sql += "'" + OBS_RECOMENDACIONES + "', ";
                informacion += "OBS_RECOMENDACIONES = '" + OBS_RECOMENDACIONES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_RECOMENDACIONES = 'null', ";
            }

            if (String.IsNullOrEmpty(ACTITUD) == false)
            {
                sql += "'" + ACTITUD + "', ";
                informacion += "ACTITUD = '" + ACTITUD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ACTITUD = 'null', ";
            }

            sql += "'" + ESTADO_CASO.ToString() + "', ";
            informacion += "ESTADO_CASO = '" + ESTADO_CASO.ToString() + "', ";

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    ID_MAESTRA_CASO_SEVERO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_SEGUIMIENTO_CASOS_SEVEROS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_MAESTRA_CASO_SEVERO = 0;
                }
            }

            return ID_MAESTRA_CASO_SEVERO;
        }

        public Decimal AdicionarRegistroProgRecomendacionesCasoSevero(Decimal ID_MAESTRA_CASO_SEVERO,
            String RECOMENDACIONES,
            DateTime FCH_RECOM_DESDE,
            DateTime FCH_RECOM_HASTA,
            String TIPO_ENTIDAD_EMITE,
            Decimal ID_ENTIDAD_EMITE,
            Conexion conexion)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_RECOMENDACION = 0;

            sql = "usp_prog_recomendaciones_casos_severos_adicionar ";

            if (ID_MAESTRA_CASO_SEVERO != 0)
            {
                sql += ID_MAESTRA_CASO_SEVERO + ", ";
                informacion += "ID_MAESTRA_CASO_SEVERO = '" + ID_MAESTRA_CASO_SEVERO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_MAESTRA_CASO_SEVERO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RECOMENDACIONES) == false)
            {
                sql += "'" + RECOMENDACIONES + "', ";
                informacion += "RECOMENDACIONES = '" + RECOMENDACIONES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "RECOMENDACIONES = 'null', ";
            }

            if (FCH_RECOM_DESDE != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_RECOM_DESDE) + "', ";
                informacion += "FCH_RECOM_DESDE = '" + FCH_RECOM_DESDE.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FCH_RECOM_DESDE = 'null', ";
            }

            if (FCH_RECOM_HASTA != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_RECOM_HASTA) + "', ";
                informacion += "FCH_RECOM_HASTA = '" + FCH_RECOM_HASTA.ToShortDateString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FCH_RECOM_HASTA = 'null', ";
            }

            if (String.IsNullOrEmpty(TIPO_ENTIDAD_EMITE) == false)
            {
                sql += "'" + TIPO_ENTIDAD_EMITE + "', ";
                informacion += "TIPO_ENTIDAD_EMITE = '" + TIPO_ENTIDAD_EMITE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_ENTIDAD_EMITE = 'null', ";
            }

            if (ID_ENTIDAD_EMITE != 0)
            {
                sql += ID_ENTIDAD_EMITE + ", ";
                informacion += "ID_ENTIDAD_EMITE = '" + ID_ENTIDAD_EMITE + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ENTIDAD_EMITE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_RECOMENDACION = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_RECOMENDACIONES_CASOS_SEVEROS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_RECOMENDACION = 0;
                }
            }

            return ID_RECOMENDACION;
        }

        public Decimal AdicionarRegistroProgDiagnosticosAdicionalesCasoSevero(Decimal ID_MAESTRA_CASO_SEVERO,
            Decimal REGISTRO_DIAGNOSTICO,
            String DSC_DIAG,
            String CLASE_DIAGNOSTICO,
            Conexion conexion)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_DIAGNOSTICO = 0;

            sql = "usp_prog_diagnosticos_adicionales_casos_severos_adicionar ";

            if (ID_MAESTRA_CASO_SEVERO != 0)
            {
                sql += ID_MAESTRA_CASO_SEVERO + ", ";
                informacion += "ID_MAESTRA_CASO_SEVERO = '" + ID_MAESTRA_CASO_SEVERO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_MAESTRA_CASO_SEVERO no puede ser nulo\n";
                ejecutar = false;
            }

            if (REGISTRO_DIAGNOSTICO != 0)
            {
                sql += REGISTRO_DIAGNOSTICO + ", ";
                informacion += "REGISTRO_DIAGNOSTICO = '" + REGISTRO_DIAGNOSTICO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_DIAGNOSTICO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DSC_DIAG) == false)
            {
                sql += "'" + DSC_DIAG + "', ";
                informacion += "DSC_DIAG = '" + DSC_DIAG + "', ";
            }
            else
            {
                MensajeError += "El campo DSC_DIAG no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CLASE_DIAGNOSTICO) == false)
            {
                sql += "'" + CLASE_DIAGNOSTICO + "', ";
                informacion += "CLASE_DIAGNOSTICO = '" + CLASE_DIAGNOSTICO + "', ";
            }
            else
            {
                MensajeError += "El campo CLASE_DIAGNOSTICO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_DIAGNOSTICO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_DIAGNOSTICOS_ADICIONALES_CASOS_SEVEROS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_DIAGNOSTICO = 0;
                }
            }

            return ID_DIAGNOSTICO;
        }


        public Decimal AdicionarSeguimientoCasoSevero(Decimal ID_EMPLEADO,
            String COND_FACT,
            String ORIGEN,
            String DESCRIPCION_ACCIDENTE,
            String PRONOSTICO,
            String OBS_PRONOSTICO,
            Boolean CALIFICADO,
            String OBS_CALIFICADO,
            Boolean PCL,
            String OBS_PCL,
            Boolean APELO_PCL,
            String OBS_APELO_PCL,
            Boolean LABORANDO,
            String OBS_LABORANDO,
            Boolean REUBICADO,
            Boolean REUBICADO_ORD_JUD,
            String OBS_REUBICADO,
            Boolean REABILITADO,
            String OBS_REABILITADO,
            Boolean INCAPACITADO,
            String OBS_INCAPACITADO,
            String ACTITUD,
            EstadosCaso ESTADO_CASO,
            Decimal ID_SOLICITUD,
            List<DetalleSeguimientoCasoSevero> listaDetallesSeguimiento,
            String NOMBRE_CASO_SEVERO,
            Programa.Areas ID_AREA,
            List<RecomendacionCasoSevero> listaRecomendaciones,
            List<DiagnosticoCasoSevero> listaDiagnosticosAdicionales)
        {
            Boolean correcto = true;

            Decimal ID_MAESTRA_CASO_SEVERO = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_MAESTRA_CASO_SEVERO = AdicionarRegistroProgSeguimientoCasosSeveros(ID_EMPLEADO, COND_FACT, ORIGEN, DESCRIPCION_ACCIDENTE, PRONOSTICO, OBS_PRONOSTICO, CALIFICADO, OBS_CALIFICADO, PCL, OBS_PCL, APELO_PCL, OBS_APELO_PCL, LABORANDO, OBS_LABORANDO, REUBICADO, REUBICADO_ORD_JUD, OBS_REUBICADO, REABILITADO, OBS_REABILITADO, INCAPACITADO, OBS_INCAPACITADO, false, new DateTime(), new DateTime(), null, ACTITUD, ESTADO_CASO, ID_SOLICITUD, conexion);

                if (ID_MAESTRA_CASO_SEVERO <= 0)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                    ID_MAESTRA_CASO_SEVERO = 0;
                }
                else
                {
                    Programa _prog = new Programa(Empresa, Usuario);
                    foreach (DetalleSeguimientoCasoSevero detalleCaso in listaDetallesSeguimiento)
                    {
                        if (detalleCaso.GENERA_COMPROMISO == true)
                        {
                            detalleCaso.ID_COMPROMISO_GENERADO = _prog.AdicionarRegistroCompromisoEnActividad(ID_MAESTRA_CASO_SEVERO, NOMBRE_CASO_SEVERO, Programa.TiposGeneraCompromiso.CASOSEVERO.ToString(), detalleCaso.SEGUIMIENTO, detalleCaso.ENCARGADO_COMPROMISO, detalleCaso.FECHA_COMPROMISO, detalleCaso.OBSERVACIONES, MaestraCompromiso.EstadosCompromiso.ABIERTO.ToString(), ID_AREA.ToString(), conexion);

                            if (detalleCaso.ID_COMPROMISO_GENERADO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_MAESTRA_CASO_SEVERO = 0;
                                MensajeError = _prog.MensajeError;
                                break;
                            }
                        }
                        else
                        {
                            detalleCaso.ID_COMPROMISO_GENERADO = 0;
                        }

                        if (correcto == true)
                        {
                            Decimal ID_DETALLE_SEGUIMIENTO = AdicionarRegistroProgDetSegCasoSevero(ID_MAESTRA_CASO_SEVERO, detalleCaso.FECHA_SEGUIMIENTO, detalleCaso.SEGUIMIENTO, detalleCaso.GENERA_COMPROMISO, detalleCaso.ID_COMPROMISO_GENERADO, conexion);

                            if (ID_DETALLE_SEGUIMIENTO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_MAESTRA_CASO_SEVERO = 0;
                                break;
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (RecomendacionCasoSevero recomendacion in listaRecomendaciones)
                        {
                            if (recomendacion.ID_RECOMENDACION == 0)
                            {
                                decimal ID_RECOMENDACICON = AdicionarRegistroProgRecomendacionesCasoSevero(ID_MAESTRA_CASO_SEVERO, recomendacion.RECOMENDACION, recomendacion.FCH_RECOM_DESDE, recomendacion.FCH_RECOM_HASTA, recomendacion.TIPO_ENTIDAD_EMITE, recomendacion.ID_ENTIDAD_EMITE, conexion);

                                if (ID_RECOMENDACICON <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    ID_MAESTRA_CASO_SEVERO = 0;
                                    break;
                                }
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (DiagnosticoCasoSevero diagnostico in listaDiagnosticosAdicionales)
                        {
                            if (diagnostico.ID_DIAGNOSTICO == 0)
                            {
                                decimal ID_DIAGNOSTICO = AdicionarRegistroProgDiagnosticosAdicionalesCasoSevero(ID_MAESTRA_CASO_SEVERO, diagnostico.REGISTRO_DIAGNOSTICO, diagnostico.DSC_DIAG, diagnostico.CLASE_DIAGNOSTICO, conexion);

                                if (ID_DIAGNOSTICO <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    ID_MAESTRA_CASO_SEVERO = 0;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                correcto = false;
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                ID_MAESTRA_CASO_SEVERO = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_MAESTRA_CASO_SEVERO;
        }


        public DataTable ObtenerDetalleSeguimientoCasoSeveroPorIdMaestraCasoSevero(Decimal ID_MAESTRA_CASO_SEVERO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_det_seg_casos_severos_obtener_por_id_maestra_caso_severo ";

            sql += ID_MAESTRA_CASO_SEVERO;

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public Decimal AdicionarRegistroProgDetSegCasoSevero(Decimal ID_MAESTRA_CASO_SEVERO,
            DateTime FECHA_SEGUIMIENTO,
            String SEGUIMIENTO,
            Boolean GENERA_COMPROMISO,
            Decimal ID_COMPROMISO_GENERADO,
            Conexion conexion)
        {
            tools _tools = new tools();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_DETALLE_SEGUIMIENTO = 0;

            sql = "usp_prog_det_seg_casos_severos_adicionar ";

            if (ID_MAESTRA_CASO_SEVERO != 0)
            {
                sql += ID_MAESTRA_CASO_SEVERO + ", ";
                informacion += "ID_MAESTRA_CASO_SEVERO = '" + ID_MAESTRA_CASO_SEVERO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_MAESTRA_CASO_SEVERO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_SEGUIMIENTO) + "', ";
            informacion += "FECHA_SEGUIMIENTO = '" + FECHA_SEGUIMIENTO.ToShortDateString() + "', ";

            if (String.IsNullOrEmpty(SEGUIMIENTO) == false)
            {
                sql += "'" + SEGUIMIENTO + "', ";
                informacion += "SEGUIMIENTO = '" + SEGUIMIENTO + "', ";
            }
            else
            {
                MensajeError += "El campo SEGUIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (GENERA_COMPROMISO == true)
            {
                sql += "'True', ";
                informacion += "GENERA_COMPROMISO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "GENERA_COMPROMISO = 'False', ";
            }

            if (ID_COMPROMISO_GENERADO != 0)
            {
                sql += ID_COMPROMISO_GENERADO + ", ";
                informacion += "ID_COMPROMISO_GENERADO = '" + ID_COMPROMISO_GENERADO + "', ";
            }
            else
            {
                sql += ID_COMPROMISO_GENERADO + ", ";
                informacion += "ID_COMPROMISO_GENERADO = '" + ID_COMPROMISO_GENERADO + "', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_DETALLE_SEGUIMIENTO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_DET_SEG_CASOS_SEVEROSS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_DETALLE_SEGUIMIENTO = 0;
                }
            }

            return ID_DETALLE_SEGUIMIENTO;
        }


        private Boolean ActualizarRegistroProgSeguimientoCasosSeveros(Decimal ID_MAESTRA_CASO_SEVERO,
            String COND_FACT,
            String ORIGEN,
            String DESCRIPCION_ACCIDENTE,
            String PRONOSTICO,
            String OBS_PRONOSTICO,
            Boolean CALIFICADO,
            String OBS_CALIFICADO,
            Boolean PCL,
            String OBS_PCL,
            Boolean APELO_PCL,
            String OBS_APELO_PCL,
            Boolean LABORANDO,
            String OBS_LABORANDO,
            Boolean REUBICADO,
            Boolean REUBICADO_ORD_JUD,
            String OBS_REUBICADO,
            Boolean REABILITADO,
            String OBS_REABILITADO,
            Boolean INCAPACITADO,
            String OBS_INCAPACITADO,
            Boolean RECOMENDACIONES,
            DateTime FCH_RECOM_DESDE,
            DateTime FCH_RECOM_HASTA,
            String OBS_RECOMENDACIONES,
            String ACTITUD,
            String ESTADO_CASO,
            Decimal ID_EMPLEADO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_seguimiento_casos_severos_actualizar ";

            #region validaciones
            if (ID_MAESTRA_CASO_SEVERO != 0)
            {
                sql += ID_MAESTRA_CASO_SEVERO + ", ";
                informacion += "ID_MAESTRA_CASO_SEVERO = '" + ID_MAESTRA_CASO_SEVERO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_MAESTRA_CASO_SEVERO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(COND_FACT) == false)
            {
                sql += "'" + COND_FACT + "', ";
                informacion += "COND_FACT = '" + COND_FACT + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "COND_FACT = 'null', ";
            }

            if (String.IsNullOrEmpty(ORIGEN) == false)
            {
                sql += "'" + ORIGEN + "', ";
                informacion += "ORIGEN = '" + ORIGEN + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ORIGEN = 'null', ";
            }

            if (String.IsNullOrEmpty(ORIGEN) == false)
            {
                sql += "'" + DESCRIPCION_ACCIDENTE + "', ";
                informacion += "DESCRIPCION_ACCIDENTE = '" + DESCRIPCION_ACCIDENTE + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DESCRIPCION_ACCIDENTE = 'null', ";
            }

            if (String.IsNullOrEmpty(ORIGEN) == false)
            {
                sql += "'" + PRONOSTICO + "', ";
                informacion += "PRONOSTICO = '" + PRONOSTICO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "PRONOSTICO = 'null', ";
            }

            if (String.IsNullOrEmpty(OBS_PRONOSTICO) == false)
            {
                sql += "'" + OBS_PRONOSTICO + "', ";
                informacion += "OBS_PRONOSTICO = '" + OBS_PRONOSTICO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_PRONOSTICO = 'null', ";
            }

            if (CALIFICADO == true)
            {
                sql += "'True', ";
                informacion += "CALIFICADO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "CALIFICADO = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_CALIFICADO) == false)
            {
                sql += "'" + OBS_CALIFICADO + "', ";
                informacion += "OBS_CALIFICADO = '" + OBS_CALIFICADO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_CALIFICADO = 'null', ";
            }

            if (PCL == true)
            {
                sql += "'True', ";
                informacion += "PCL = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "PCL = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_PCL) == false)
            {
                sql += "'" + OBS_PCL + "', ";
                informacion += "OBS_PCL = '" + OBS_PCL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_PCL = 'null', ";
            }

            if (APELO_PCL == true)
            {
                sql += "'True', ";
                informacion += "APELO_PCL = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "APELO_PCL = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_APELO_PCL) == false)
            {
                sql += "'" + OBS_APELO_PCL + "', ";
                informacion += "OBS_APELO_PCL = '" + OBS_APELO_PCL + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_APELO_PCL = 'null', ";
            }

            if (LABORANDO == true)
            {
                sql += "'True', ";
                informacion += "LABORANDO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "LABORANDO = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_LABORANDO) == false)
            {
                sql += "'" + OBS_LABORANDO + "', ";
                informacion += "OBS_LABORANDO = '" + OBS_LABORANDO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_LABORANDO = 'null', ";
            }

            if (REUBICADO == true)
            {
                sql += "'True', ";
                informacion += "REUBICADO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "REUBICADO = 'False', ";
            }

            if (REUBICADO_ORD_JUD == true)
            {
                sql += "'True', ";
                informacion += "REUBICADO_ORD_JUD = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "REUBICADO_ORD_JUD = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_REUBICADO) == false)
            {
                sql += "'" + OBS_REUBICADO + "', ";
                informacion += "OBS_REUBICADO = '" + OBS_REUBICADO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_REUBICADO = 'null', ";
            }

            if (REABILITADO == true)
            {
                sql += "'True', ";
                informacion += "REABILITADO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "REABILITADO = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_REABILITADO) == false)
            {
                sql += "'" + OBS_REABILITADO + "', ";
                informacion += "OBS_REABILITADO = '" + OBS_REABILITADO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_REABILITADO = 'null', ";
            }

            if (INCAPACITADO == true)
            {
                sql += "'True', ";
                informacion += "INCAPACITADO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "INCAPACITADO = 'False', ";
            }

            if (String.IsNullOrEmpty(OBS_INCAPACITADO) == false)
            {
                sql += "'" + OBS_INCAPACITADO + "', ";
                informacion += "OBS_INCAPACITADO = '" + OBS_INCAPACITADO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_INCAPACITADO = 'null', ";
            }

            if (RECOMENDACIONES == true)
            {
                sql += "'True', ";
                informacion += "RECOMENDACIONES = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "RECOMENDACIONES = 'False', ";
            }

            if (FCH_RECOM_DESDE == new DateTime())
            {
                sql += "null, ";
                informacion += "FCH_RECOM_DESDE = 'null', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_RECOM_DESDE) + "', ";
                informacion += "FCH_RECOM_DESDE = '" + FCH_RECOM_DESDE.ToShortDateString() + "', ";
            }

            if (FCH_RECOM_HASTA == new DateTime())
            {
                sql += "null, ";
                informacion += "FCH_RECOM_HASTA = 'null', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_RECOM_HASTA) + "', ";
                informacion += "FCH_RECOM_HASTA = '" + FCH_RECOM_HASTA.ToShortDateString() + "', ";
            }

            if (String.IsNullOrEmpty(OBS_RECOMENDACIONES) == false)
            {
                sql += "'" + OBS_RECOMENDACIONES + "', ";
                informacion += "OBS_RECOMENDACIONES = '" + OBS_RECOMENDACIONES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_RECOMENDACIONES = 'null', ";
            }

            if (String.IsNullOrEmpty(ACTITUD) == false)
            {
                sql += "'" + ACTITUD + "', ";
                informacion += "ACTITUD = '" + ACTITUD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ACTITUD = 'null', ";
            }

            if (String.IsNullOrEmpty(ESTADO_CASO) == false)
            {
                sql += "'" + ESTADO_CASO + "', ";
                informacion += "ESTADO_CASO = '" + ESTADO_CASO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ESTADO_CASO = 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO;
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "'";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO no puede ser vacio.";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_SEGUIMIENTO_CASOS_SEVEROS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean ActualizarSeguimientoCasoSevero(Decimal ID_MAESTRA_CASO_SEVERO,
            String COND_FACT,
            String ORIGEN,
            String DESCRIPCION_ACCIDENTE,
            String PRONOSTICO,
            String OBS_PRONOSTICO,
            Boolean CALIFICADO,
            String OBS_CALIFICADO,
            Boolean PCL,
            String OBS_PCL,
            Boolean APELO_PCL,
            String OBS_APELO_PCL,
            Boolean LABORANDO,
            String OBS_LABORANDO,
            Boolean REUBICADO,
            Boolean REUBICADO_ORD_JUD,
            String OBS_REUBICADO,
            Boolean REABILITADO,
            String OBS_REABILITADO,
            Boolean INCAPACITADO,
            String OBS_INCAPACITADO,
            String ACTITUD,
            String ESTADO_CASO,
            List<DetalleSeguimientoCasoSevero> listaDetallesSeguimiento,
            String NOMBRE_CASO_SEVERO,
            Programa.Areas ID_AREA,
            Decimal ID_EMPLEADO,
            List<RecomendacionCasoSevero> listaRecomendaciones,
            List<DiagnosticoCasoSevero> listaDiagnosticosAdicionales)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarRegistroProgSeguimientoCasosSeveros(ID_MAESTRA_CASO_SEVERO, COND_FACT, ORIGEN, DESCRIPCION_ACCIDENTE, PRONOSTICO, OBS_PRONOSTICO, CALIFICADO, OBS_CALIFICADO, PCL, OBS_PCL, APELO_PCL, OBS_APELO_PCL, LABORANDO, OBS_LABORANDO, REUBICADO, REUBICADO_ORD_JUD, OBS_REUBICADO, REABILITADO, OBS_REABILITADO, INCAPACITADO, OBS_INCAPACITADO, false, new DateTime(), new DateTime(), null, ACTITUD, ESTADO_CASO, ID_EMPLEADO, conexion) == false)
                {
                    correcto = false;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    Programa _prog = new Programa(Empresa, Usuario);
                    foreach (DetalleSeguimientoCasoSevero detalleCaso in listaDetallesSeguimiento)
                    {
                        if (detalleCaso.GENERA_COMPROMISO == true)
                        {
                            detalleCaso.ID_COMPROMISO_GENERADO = _prog.AdicionarRegistroCompromisoEnActividad(ID_MAESTRA_CASO_SEVERO, NOMBRE_CASO_SEVERO, Programa.TiposGeneraCompromiso.CASOSEVERO.ToString(), detalleCaso.SEGUIMIENTO, detalleCaso.ENCARGADO_COMPROMISO, detalleCaso.FECHA_COMPROMISO, detalleCaso.OBSERVACIONES, MaestraCompromiso.EstadosCompromiso.ABIERTO.ToString(), ID_AREA.ToString(), conexion);

                            if (detalleCaso.ID_COMPROMISO_GENERADO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                MensajeError = _prog.MensajeError;
                                break;
                            }
                        }
                        else
                        {
                            detalleCaso.ID_COMPROMISO_GENERADO = 0;
                        }

                        if (correcto == true)
                        {
                            Decimal ID_DETALLE_SEGUIMIENTO = AdicionarRegistroProgDetSegCasoSevero(ID_MAESTRA_CASO_SEVERO, detalleCaso.FECHA_SEGUIMIENTO, detalleCaso.SEGUIMIENTO, detalleCaso.GENERA_COMPROMISO, detalleCaso.ID_COMPROMISO_GENERADO, conexion);

                            if (ID_DETALLE_SEGUIMIENTO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_MAESTRA_CASO_SEVERO = 0;
                                break;
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (RecomendacionCasoSevero recomendacion in listaRecomendaciones)
                        {
                            if (recomendacion.ID_RECOMENDACION == 0)
                            {
                                decimal ID_RECOMENDACICON = AdicionarRegistroProgRecomendacionesCasoSevero(ID_MAESTRA_CASO_SEVERO, recomendacion.RECOMENDACION, recomendacion.FCH_RECOM_DESDE, recomendacion.FCH_RECOM_HASTA, recomendacion.TIPO_ENTIDAD_EMITE, recomendacion.ID_ENTIDAD_EMITE, conexion);

                                if (ID_RECOMENDACICON <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    ID_MAESTRA_CASO_SEVERO = 0;
                                    break;
                                }
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (DiagnosticoCasoSevero diagnostico in listaDiagnosticosAdicionales)
                        {
                            if (diagnostico.ID_DIAGNOSTICO == 0)
                            {
                                decimal ID_DIAGNOSTICO = AdicionarRegistroProgDiagnosticosAdicionalesCasoSevero(ID_MAESTRA_CASO_SEVERO, diagnostico.REGISTRO_DIAGNOSTICO, diagnostico.DSC_DIAG, diagnostico.CLASE_DIAGNOSTICO, conexion);

                                if (ID_DIAGNOSTICO <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }


        public DataTable ObtenerDatosInformeSeguimientoCasoSevero(Decimal ID_MAESTRA_CASO_SEVERO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "RPT_SALUD_OCUPACIONAL_PDF_SEGUIMIENTO_MAESTRO_CASOS_SEVEROS ";

            sql += ID_MAESTRA_CASO_SEVERO;

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerRecomndacionesCasoSeveroPorIdMaestraCasoSevero(Decimal ID_MAESTRA_CASO_SEVERO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_recomendaciones_casos_severos_obtenerPorIdMaestraCasoSevero ";

            sql += ID_MAESTRA_CASO_SEVERO;

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerDiagnosticosAdicionalesCasoSeveroPorIdMaestraCasoSevero(Decimal ID_MAESTRA_CASO_SEVERO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_diagnosticos_adicionales_casos_severos_obtenerPorIdMaestraCasoSevero ";

            sql += ID_MAESTRA_CASO_SEVERO;

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }
        #endregion metodos

    }
}
