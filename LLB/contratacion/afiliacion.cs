using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.seleccion;

namespace Brainsbits.LLB.contratacion
{
    public class afiliacion
    {
        #region variables
        public enum Entidades
        {
            Arl = 0,
            Eps,
            Afp,
            Ccf
        }

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
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
        public afiliacion(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #region CON_AFILIACION_F_PENSIONES
        public Decimal Adicionarconafiliacionfpensiones(int ID_SOLICITUD, int ID_F_PENSIONES, DateTime FECHA_R, String OBSERVACIONES, String PENSIONADO, int ID_REQUERIMIENTO, String TIPO_PENSIONADO, String NUMERO_RESOLUCION_TRAMITE)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_f_pensiones_adicionar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_F_PENSIONES != 0)
            {
                sql += ID_F_PENSIONES + ", ";
                informacion += "ID_F_PENSIONES= '" + ID_F_PENSIONES.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_F_PENSIONES= '0', ";
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(PENSIONADO)))
            {
                sql += "'" + PENSIONADO + "', ";
                informacion += "PENSIONADO= '" + PENSIONADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PENSIONADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ",";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "',";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(TIPO_PENSIONADO)))
            {
                sql += "'" + TIPO_PENSIONADO + "', ";
                informacion += "TIPO_PENSIONADO= '" + TIPO_PENSIONADO.ToString() + "', ";
            }
            else
            {
                sql += "'NULL', ";
                informacion += "TIPO_PENSIONADO= 'NULL', ";
            }
            if (!(String.IsNullOrEmpty(NUMERO_RESOLUCION_TRAMITE)))
            {
                sql += "'" + NUMERO_RESOLUCION_TRAMITE + "' ";
                informacion += "NUMERO_RESOLUCION_TRAMITE= '" + NUMERO_RESOLUCION_TRAMITE.ToString() + "' ";
            }
            else
            {
                sql += "'NULL' ";
                informacion += "NUMERO_RESOLUCION_TRAMITE = 'NULL' ";
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Decimal AdicionarconafiliacionfpensionesAuditoria(Decimal ID_SOLICITUD
            , Decimal ID_F_PENSIONES
            , DateTime FECHA_R
            , String OBSERVACIONES
            , String PENSIONADO
            , Decimal ID_REQUERIMIENTO
            , String TIPO_PENSIONADO
            , String NUMERO_RESOLUCION_TRAMITE
            , Decimal ID_EMPLEADO
            , Boolean ACTUALIZAR_ESTADO_PROCESO
            , DateTime FECHA_RADICACION
            , Decimal ID_CONTRATO
            , String ENTIDAD_ARCHIVO_RADICACION
            , Byte[] ARCHIVO_RADICACION
            , Int32 ARCHIVO_RADICACION_TAMANO
            , String ARCHIVO_RADICACION_EXTENSION
            , String ARCHIVO_RADICACION_TYPE)
        {
            Decimal ID_AFILIACION = 0;
            Decimal ID_AUDITORIA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean verificador = true;

            try
            {
                ID_AFILIACION = AdicionarAfiFondoPensiones(ID_SOLICITUD, ID_F_PENSIONES, FECHA_R, OBSERVACIONES, PENSIONADO, ID_REQUERIMIENTO, TIPO_PENSIONADO, NUMERO_RESOLUCION_TRAMITE, conexion, FECHA_RADICACION);

                if (ID_AFILIACION <= 0)
                {
                    conexion.DeshacerTransaccion();
                    ID_AFILIACION = 0;
                    verificador = false;
                }
                else
                {
                    if (ActualizarAfpDeNomEmpleadosPorIdEmpleado(ID_EMPLEADO, ID_AFILIACION, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        ID_AFILIACION = 0;
                        verificador = false;
                    }
                    else
                    {
                        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Empresa, Usuario);
                        ID_AUDITORIA = _auditoriaContratos.AdicionarAuditoriaContratos(ID_EMPLEADO, tabla.CON_AFILIACION_F_PENSIONES, ID_AFILIACION, DateTime.Now, conexion);

                        if (ID_AUDITORIA <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = _auditoriaContratos.MensajeError;
                            ID_AFILIACION = 0;
                            verificador = false;
                        }
                        else
                        {
                            if (ARCHIVO_RADICACION != null)
                            {
                                if (conexion.ExecuteEscalarParaAdicionarDocsAfiliacion(ID_CONTRATO, ENTIDAD_ARCHIVO_RADICACION, ARCHIVO_RADICACION, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_TYPE, Usuario) == null)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "Ocurrio un error al momento de adicionar el documento de radicación de la afiliación.";
                                    ID_AFILIACION = 0;
                                    verificador = false;
                                }
                            }


                            if (verificador == true)
                            {
                                if (ACTUALIZAR_ESTADO_PROCESO == true)
                                {
                                    radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                                    if (_radicacionHojasDeVida.ActualizarEstadoProcesoRegSolicitudesIngresoAuditoria(ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CON_AFILIACION_FONDO, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        MensajeError = _radicacionHojasDeVida.MensajeError;
                                        ID_AFILIACION = 0;
                                        verificador = false;
                                    }
                                }

                                if (verificador == true)
                                {
                                    conexion.AceptarTransaccion();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                ID_AFILIACION = 0;
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_AFILIACION;
        }


        public Decimal AdicionarAfiFondoPensiones(Decimal ID_SOLICITUD, Decimal ID_F_PENSIONES, DateTime FECHA_R, String OBSERVACIONES, String PENSIONADO, Decimal ID_REQUERIMIENTO, String TIPO_PENSIONADO, String NUMERO_RESOLUCION_TRAMITE, Conexion conexion, DateTime FECHA_RADICACION)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_f_pensiones_adicionar_auditoria ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_F_PENSIONES != 0)
            {
                sql += ID_F_PENSIONES + ", ";
                informacion += "ID_F_PENSIONES = '" + ID_F_PENSIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_F_PENSIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PENSIONADO)))
            {
                sql += "'" + PENSIONADO + "', ";
                informacion += "PENSIONADO = '" + PENSIONADO + "', ";
            }
            else
            {
                MensajeError += "El campo PENSIONADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PENSIONADO)))
            {
                sql += "'" + TIPO_PENSIONADO + "', ";
                informacion += "TIPO_PENSIONADO = '" + PENSIONADO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "TIPO_PENSIONADO = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(NUMERO_RESOLUCION_TRAMITE)))
            {
                sql += "'" + NUMERO_RESOLUCION_TRAMITE + "', ";
                informacion += "NUMERO_RESOLUCION_TRAMITE = '" + NUMERO_RESOLUCION_TRAMITE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NUMERO_RESOLUCION_TRAMITE = 'NULL', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RADICACION) + "'";
            informacion += "FECHA_RADICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RADICACION) + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Decimal AdicionarconafiliacionfpensionesAuditoria(int ID_SOLICITUD, int ID_F_PENSIONES, DateTime FECHA_R, String OBSERVACIONES, String PENSIONADO, int ID_REQUERIMIENTO)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_f_pensiones_adicionar_auditoria ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_F_PENSIONES != 0)
            {
                sql += ID_F_PENSIONES + ", ";
                informacion += "ID_F_PENSIONES= '" + ID_F_PENSIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_F_PENSIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(PENSIONADO)))
            {
                sql += "'" + PENSIONADO + "', ";
                informacion += "PENSIONADO= '" + PENSIONADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PENSIONADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }


            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Boolean Actualizarconafiliacionfpensiones(int REGISTRO, int ID_SOLICITUD, int ID_F_PENSIONES, DateTime FECHA_R, String OBSERVACIONES, String PENSIONADO, int ID_REQUERIMIENTO, String TIPO_PENSIONADO, String NUMERO_RESOLUCION_TRAMITE)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_f_pensiones_actualizar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_F_PENSIONES != 0)
            {
                sql += ID_F_PENSIONES + ", ";
                informacion += "ID_F_PENSIONES= '" + ID_F_PENSIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_F_PENSIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(PENSIONADO)))
            {
                sql += "'" + PENSIONADO + "', ";
                informacion += "PENSIONADO= '" + PENSIONADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PENSIONADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ",";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PENSIONADO)))
            {
                sql += "'" + TIPO_PENSIONADO + "', ";
                informacion += "TIPO_PENSIONADO = '" + TIPO_PENSIONADO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIPO_PENSIONADO = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(NUMERO_RESOLUCION_TRAMITE)))
            {
                sql += "'" + NUMERO_RESOLUCION_TRAMITE + "'";
                informacion += "NUMERO_RESOLUCION_TRAMITE = '" + NUMERO_RESOLUCION_TRAMITE.ToString() + "'";
            }
            else
            {
                sql += "null";
                informacion += "NUMERO_RESOLUCION_TRAMITE = 'NULL'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean Actualizarconafiliacionfpensiones(int REGISTRO, int ID_SOLICITUD, int ID_F_PENSIONES, DateTime FECHA_R, String OBSERVACIONES, String PENSIONADO, int ID_REQUERIMIENTO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_f_pensiones_actualizar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_F_PENSIONES != 0)
            {
                sql += ID_F_PENSIONES + ", ";
                informacion += "ID_F_PENSIONES= '" + ID_F_PENSIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_F_PENSIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(PENSIONADO)))
            {
                sql += "'" + PENSIONADO + "', ";
                informacion += "PENSIONADO= '" + PENSIONADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PENSIONADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerconafiliacionfpensionesPorIdSolicitud(int ID_SOLICITUD)
        {

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_f_pensiones_obtener_por_id_solicitud ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionfpensionesPorRegistro(int REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_f_pensiones_obtener_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionfpensionesPorIdFPensiones(int ID_F_PENSIONES)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_f_pensiones_obtener_id_f_penciones ";

            if (ID_F_PENSIONES != 0)
            {
                sql += ID_F_PENSIONES;
                informacion += "ID_F_PENSIONES = '" + ID_F_PENSIONES.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_F_PENSIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionfpensionesPorSolicitudRequerimiento(int ID_SOLICITUD, int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_f_pensiones_obtener_por_solicitud_requisicion ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + " ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        #endregion

        #region CON_AFILIACION_ARP

        public DataTable ObtenerconafiliacionfpensionesPorSolicitudRequerimiento_HV(int ID_SOLICITUD, int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_f_pensiones_obtener_por_solicitud_requisicion_HV ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + " ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_F_PENSIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        #endregion

        #region CON_AFILIACION_ARP

        public Decimal AdicionarconafiliacionArp(int ID_SOLICITUD, int ID_ARP, DateTime FECHA_R, String OBSERVACIONES, int ID_REQUERIMIENTO)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_arp_adicionar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_ARP != 0)
            {
                sql += ID_ARP + ", ";
                informacion += "ID_ARP= '" + ID_ARP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ARP no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_ARP, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Decimal AdicionarconafiliacionArpAuditoria(Decimal ID_SOLICITUD
            , Decimal ID_ARP
            , DateTime FECHA_R
            , String OBSERVACIONES
            , Decimal ID_REQUERIMIENTO
            , Decimal ID_EMPLEADO
            , Boolean ACTUALIZAR_ESTADO_PROCESO
            , DateTime FECHA_RADICACION
            , Decimal ID_CONTRATO
            , String ENTIDAD_ARCHIVO_RADICACION
            , Byte[] ARCHIVO_RADICACION
            , Int32 ARCHIVO_RADICACION_TAMANO
            , String ARCHIVO_RADICACION_EXTENSION
            , String ARCHIVO_RADICACION_TYPE)
        {
            Decimal ID_AFILIACION = 0;
            Decimal ID_AUDITORIA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean verificador = true;

            try
            {
                ID_AFILIACION = AdicionarAfiArp(ID_SOLICITUD, ID_ARP, FECHA_R, OBSERVACIONES, ID_REQUERIMIENTO, conexion, FECHA_RADICACION);

                if (ID_AFILIACION <= 0)
                {
                    conexion.DeshacerTransaccion();
                    ID_AFILIACION = 0;
                    verificador = false;
                }
                else
                {
                    if (ActualizarArpDeNomEmpleadosPorIdEmpleado(ID_EMPLEADO, ID_AFILIACION, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        ID_AFILIACION = 0;
                        verificador = false;
                    }
                    else
                    {
                        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Empresa, Usuario);
                        ID_AUDITORIA = _auditoriaContratos.AdicionarAuditoriaContratos(ID_EMPLEADO, tabla.CON_AFILIACION_ARP, ID_AFILIACION, DateTime.Now, conexion);

                        if (ID_AUDITORIA <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = _auditoriaContratos.MensajeError;
                            ID_AFILIACION = 0;
                            verificador = false;
                        }
                        else
                        {
                            if (ARCHIVO_RADICACION != null)
                            {
                                if (conexion.ExecuteEscalarParaAdicionarDocsAfiliacion(ID_CONTRATO, ENTIDAD_ARCHIVO_RADICACION, ARCHIVO_RADICACION, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_TYPE, Usuario) == null)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "Ocurrio un error al momento de adicionar el documento de radicación de la afiliación.";
                                    ID_AFILIACION = 0;
                                    verificador = false;
                                }
                            }

                            if (verificador == true)
                            {
                                if (ACTUALIZAR_ESTADO_PROCESO == true)
                                {
                                    radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                                    if (_radicacionHojasDeVida.ActualizarEstadoProcesoRegSolicitudesIngresoAuditoria(ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CON_AFILIACION_ARP, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        MensajeError = _radicacionHojasDeVida.MensajeError;
                                        ID_AFILIACION = 0;
                                        verificador = false;
                                    }
                                }

                                if (verificador == true)
                                {
                                    conexion.AceptarTransaccion();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                ID_AFILIACION = 0;
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_AFILIACION;
        }

        public Decimal AdicionarAfiArp(Decimal ID_SOLICITUD, Decimal ID_ARP, DateTime FECHA_R, String OBSERVACIONES, Decimal ID_REQUERIMIENTO, Conexion conexion, DateTime FECHA_RADICACION)
        {

            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_arp_adicionar_auditoria ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ARP != 0)
            {
                sql += ID_ARP + ", ";
                informacion += "ID_ARP= '" + ID_ARP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ARP no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RADICACION) + "'";
            informacion += "FECHA_RADICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RADICACION) + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_ARP, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }


        public Boolean ActualizarconafiliacionArp(int REGISTRO, int ID_SOLICITUD, int ID_ARP, DateTime FECHA_R, String OBSERVACIONES, int ID_REQUERIMIENTO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_arp_actualizar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_ARP != 0)
            {
                sql += ID_ARP + ", ";
                informacion += "ID_F_PENSIONES= '" + ID_ARP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_F_PENSIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_ARP, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerconafiliacionArpPorIdSolicitud(int ID_SOLICITUD)
        {

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_arp_obtener_por_id_solicitud ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_ARP, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionArpPorRegistro(int REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_arp_obtener_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_ARP, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionArpPorIdArp(int ID_ARP)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_arp_obtener_por_id_arp ";

            if (ID_ARP != 0)
            {
                sql += ID_ARP;
                informacion += "ID_ARP = '" + ID_ARP.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_ARP no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_ARP, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionArpPorSolicitudRequerimiento(int ID_SOLICITUD, int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "dbo.usp_con_afiliacion_arp_obtener_por_solicitud_requerimiento ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + " ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_ARP, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        #endregion
        public DataTable ObtenerconafiliacionArpPorSolicitudRequerimientoHV(int ID_SOLICITUD, int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_arp_obtener_por_solicitud_requerimiento_HV ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + " ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_ARP, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        #endregion
        #region CON_AFILIACION_CAJAS_C

        public Decimal AdicionarconafiliacionCajasC(int ID_SOLICITUD, int ID_CAJA_C, DateTime FECHA_R, String OBSERVACIONES, int ID_REQUERIMIENTO, string idciudad)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_cajas_c_adicionar_V2 ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_CAJA_C != 0)
            {
                sql += ID_CAJA_C + ", ";
                informacion += "ID_CAJA_C= '" + ID_CAJA_C.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CAJA_C= 0, ";
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + idciudad + "'";
            informacion += "ID_CIUDAD = '" + idciudad + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_CAJAS_C, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }


        public Decimal AdicionarconafiliacionCajasCAuditoria(Decimal ID_SOLICITUD
            , Decimal ID_CAJA_C
            , DateTime FECHA_R
            , String OBSERVACIONES
            , Decimal ID_REQUERIMIENTO
            , Decimal ID_EMPLEADO
            , Boolean ACTUALIZAR_ESTADO_PROCESO
            , DateTime FECHA_RADICACION
            , Decimal ID_CONTRATO
            , String ENTIDAD_ARCHIVO_RADICACION
            , Byte[] ARCHIVO_RADICACION
            , Int32 ARCHIVO_RADICACION_TAMANO
            , String ARCHIVO_RADICACION_EXTENSION
            , String ARCHIVO_RADICACION_TYPE
            , String idCiudad)
        {

            Decimal ID_AFILIACION = 0;
            Decimal ID_AUDITORIA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean verificador = true;

            try
            {
                ID_AFILIACION = AdicionarAfiCajaC(ID_SOLICITUD, ID_CAJA_C, FECHA_R, OBSERVACIONES, ID_REQUERIMIENTO, conexion, FECHA_RADICACION, idCiudad);

                if (ID_AFILIACION <= 0)
                {
                    conexion.DeshacerTransaccion();
                    ID_AFILIACION = 0;
                    verificador = false;
                }
                else
                {
                    if (ActualizarCajaCDeNomEmpleadosPorIdEmpleado(ID_EMPLEADO, ID_AFILIACION, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        ID_AFILIACION = 0;
                        verificador = false;
                    }
                    else
                    {
                        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Empresa, Usuario);

                        ID_AUDITORIA = _auditoriaContratos.AdicionarAuditoriaContratos(ID_EMPLEADO, tabla.CON_AFILIACION_CAJAS_C, ID_AFILIACION, DateTime.Now, conexion);

                        if (ID_AUDITORIA <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = _auditoriaContratos.MensajeError;
                            ID_AFILIACION = 0;
                            verificador = false;
                        }
                        else
                        {
                            if (ARCHIVO_RADICACION != null)
                            {
                                if (conexion.ExecuteEscalarParaAdicionarDocsAfiliacion(ID_CONTRATO, ENTIDAD_ARCHIVO_RADICACION, ARCHIVO_RADICACION, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_TYPE, Usuario) == null)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "Ocurrio un error al momento de adicionar el documento de radicación de la afiliación.";
                                    ID_AFILIACION = 0;
                                    verificador = false;
                                }
                            }

                            if (verificador == true)
                            {
                                if (ACTUALIZAR_ESTADO_PROCESO == true)
                                {
                                    radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                                    if (_radicacionHojasDeVida.ActualizarEstadoProcesoRegSolicitudesIngresoAuditoria(ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CON_AFILIACION_CCF, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        MensajeError = _radicacionHojasDeVida.MensajeError;
                                        ID_AFILIACION = 0;
                                        verificador = false;
                                    }
                                }

                                if (verificador == true)
                                {
                                    conexion.AceptarTransaccion();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                ID_AFILIACION = 0;
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_AFILIACION;
        }


        public Decimal AdicionarAfiCajaC(Decimal ID_SOLICITUD,
            Decimal ID_CAJA_C,
            DateTime FECHA_R,
            String OBSERVACIONES,
            Decimal ID_REQUERIMIENTO,
            Conexion conexion,
            DateTime FECHA_RADICACION,
            String idCiudad)
        {

            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_cajas_c_adicionar_auditoria_V2 ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CAJA_C != 0)
            {
                sql += ID_CAJA_C + ", ";
                informacion += "ID_CAJA_C = '" + ID_CAJA_C.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CAJA_C = '0'";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RADICACION) + "', ";
            informacion += "FECHA_RADICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RADICACION) + "', ";

            if (String.IsNullOrEmpty(idCiudad) == false)
            {
                sql += "'" + idCiudad + "'";
                informacion += "ID_CIUDAD = '" + idCiudad + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "ID_CIUDAD = 'NULL";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_CAJAS_C, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }


        public Boolean ActualizarconafiliacionCajasC(int REGISTRO,
            int ID_SOLICITUD,
            int ID_CAJA_C,
            DateTime FECHA_R,
            String OBSERVACIONES,
            int ID_REQUERIMIENTO,
            String idCiudad)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliacion_cajas_c_actualizar_V2 ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_CAJA_C != 0)
            {
                sql += ID_CAJA_C + ", ";
                informacion += "ID_CAJA_C= '" + ID_CAJA_C.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CAJA_C= '0',";
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (string.IsNullOrEmpty(idCiudad) == false)
            {
                sql += "'" + idCiudad + "'";
                informacion += "ID_CIUDAD = '" + idCiudad + "'";
            }
            else
            {
                sql += "null";
                informacion += "ID_CIUDAD = 'null";
            }


            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_CAJAS_C, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerconafiliacionCajasCPorIdSolicitud(int ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_cajas_c_obtener_por_id_solicitud ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_CAJAS_C, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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


        public DataTable ObtenerDepartamentosCajaC(Decimal registro, string tipoEntidad)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_ent_cajas_c_obtener_departamentos ";

            if (registro != 0)
            {
                sql += registro + ", ";
            }
            else
            {
                MensajeError += "El campo registro no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + tipoEntidad + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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


        public DataTable ObtenerCiudadesCajaC(Decimal registro, string tipoEntidad, String idDepartamento)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_ent_cajas_c_obtener_ciudades ";

            if (registro != 0)
            {
                sql += registro + ", ";
            }
            else
            {
                MensajeError += "El campo registro no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + idDepartamento + "', ";

            sql += "'" + tipoEntidad + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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


        public DataTable ObtenerconafiliacionCajasCPorRegistro(int REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_cajas_c_obtener_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_CAJAS_C, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionCajasCPorIdCajaC(int ID_CAJA_C)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_cajas_c_obtener_por_id_caja_c ";

            if (ID_CAJA_C != 0)
            {
                sql += ID_CAJA_C;
                informacion += "ID_CAJA_C = '" + ID_CAJA_C.ToString() + "'";
            }
            else
            {
                sql += "0";
                informacion += "ID_CAJA_C = '0'";
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_CAJAS_C, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionCajasCPorSolicitudRequerimiento(int ID_SOLICITUD, int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_cajas_c_obtener_por_solicitud_requerimiento ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + " ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_CAJAS_C, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionCajasCPorSolicitudRequerimientoHV(int ID_SOLICITUD, int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliacion_cajas_c_obtener_por_solicitud_requerimiento_HV ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + " ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_CAJAS_C, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        #region CON_AFILIACION_EPS

        public Decimal AdicionarconafiliacionEps(int ID_SOLICITUD, int ID_EPS, DateTime FECHA_R, String OBSERVACIONES, int ID_REQUERIMIENTO)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliaciones_eps_adicionar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EPS != 0)
            {
                sql += ID_EPS + ", ";
                informacion += "ID_EPS= '" + ID_EPS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EPS no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_EPS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Decimal AdicionarconafiliacionEpsAuditoria(Decimal ID_SOLICITUD
            , Decimal ID_EPS
            , DateTime FECHA_R
            , String OBSERVACIONES
            , Decimal ID_REQUERIMIENTO
            , Decimal ID_EMPLEADO
            , Boolean ACTUALIZAR_ESTADO_PROCESO
            , DateTime FECHA_RADICACION
            , Decimal ID_CONTRATO
            , String ENTIDAD_ARCHIVO_RADICACION
            , Byte[] ARCHIVO_RADICACION
            , Int32 ARCHIVO_RADICACION_TAMANO
            , String ARCHIVO_RADICACION_EXTENSION
            , String ARCHIVO_RADICACION_TYPE)
        {
            Decimal ID_AFILIACION = 0;
            Decimal ID_AUDITORIA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean verificador = true;

            try
            {
                ID_AFILIACION = AdicionarAfiEps(ID_SOLICITUD, ID_EPS, FECHA_R, OBSERVACIONES, ID_REQUERIMIENTO, conexion, FECHA_RADICACION);

                if (ID_AFILIACION <= 0)
                {
                    conexion.DeshacerTransaccion();
                    ID_AFILIACION = 0;
                    verificador = false;
                }
                else
                {
                    if (ActualizarEpsDeNomEmpleadosPorIdEmpleado(ID_EMPLEADO, ID_AFILIACION, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        ID_AFILIACION = 0;
                        verificador = false;
                    }
                    else
                    {
                        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Empresa, Usuario);
                        ID_AUDITORIA = _auditoriaContratos.AdicionarAuditoriaContratos(ID_EMPLEADO, tabla.CON_AFILIACION_EPS, ID_AFILIACION, DateTime.Now, conexion);

                        if (ID_AUDITORIA <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = _auditoriaContratos.MensajeError;
                            ID_AFILIACION = 0;
                            verificador = false;
                        }
                        else
                        {

                            if (ARCHIVO_RADICACION != null)
                            {
                                if (conexion.ExecuteEscalarParaAdicionarDocsAfiliacion(ID_CONTRATO, ENTIDAD_ARCHIVO_RADICACION, ARCHIVO_RADICACION, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_TYPE, Usuario) == null)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "Ocurrio un error al momento de adicionar el documento de radicación de la afiliación.";
                                    ID_AFILIACION = 0;
                                    verificador = false;
                                }
                            }

                            if (verificador == true)
                            {
                                if (ACTUALIZAR_ESTADO_PROCESO == true)
                                {
                                    radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                                    if (_radicacionHojasDeVida.ActualizarEstadoProcesoRegSolicitudesIngresoAuditoria(ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CON_AFILIACION_EPS, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        MensajeError = _radicacionHojasDeVida.MensajeError;
                                        ID_AFILIACION = 0;
                                        verificador = false;
                                    }
                                }

                                if (verificador == true)
                                {
                                    conexion.AceptarTransaccion();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                ID_AFILIACION = 0;
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_AFILIACION;
        }

        public Decimal AdicionarAfiEps(Decimal ID_SOLICITUD, Decimal ID_EPS, DateTime FECHA_R, String OBSERVACIONES, Decimal ID_REQUERIMIENTO, Conexion conexion, DateTime FECHA_RADICACION)
        {

            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliaciones_eps_adicionar_auditoria ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EPS != 0)
            {
                sql += ID_EPS + ", ";
                informacion += "ID_EPS = '" + ID_EPS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EPS no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RADICACION) + "'";
            informacion += "FECHA_RADICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_RADICACION) + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_EPS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Boolean ActualizarconafiliacionEps(int REGISTRO, int ID_SOLICITUD, int ID_EPS, DateTime FECHA_R, String OBSERVACIONES, int ID_REQUERIMIENTO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_afiliaciones_eps_actualizar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD= '" + ID_SOLICITUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EPS != 0)
            {
                sql += ID_EPS + ", ";
                informacion += "ID_EPS = '" + ID_EPS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EPS no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_EPS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerconafiliacionEpsPorIdSolicitud(int ID_SOLICITUD)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliaciones_eps_obtener_por_id_solicitud ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_EPS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionEpsPorRegistro(int REGISTRO)
        {

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliaciones_eps_obtener_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_EPS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionEpsPorIdEps(int ID_EPS)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliaciones_eps_obtener_por_id_eps ";

            if (ID_EPS != 0)
            {
                sql += ID_EPS;
                informacion += "ID_EPS = '" + ID_EPS.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EPS no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_EPS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerconafiliacionEpsPorSolicitudRequerimiento(int ID_SOLICITUD, int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliaciones_eps_obtener_por_solicitud_requerimiento ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + " ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_EPS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        #endregion

        public DataTable ObtenerconafiliacionEpsPorSolicitudRequerimientoHV(int ID_SOLICITUD, int ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_afiliaciones_eps_obtener_por_solicitud_requerimiento_HV  ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + " ";
                informacion += "ID_REQUERIMIENTO= '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_AFILIACION_EPS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        #endregion

        #region NOM_EMPLEADOS (AFILIACIONES)

        public Boolean ActualizarArpDeNomEmpleadosPorIdEmpleado(Decimal ID_EMPLEADO, Decimal ID_AFILIACION, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_empleados_actualizar_arp_por_idEmpleado ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_AFILIACION != 0)
            {
                sql += ID_AFILIACION + ", ";
                informacion += "ID_AFILIACION = '" + ID_AFILIACION + "', ";
            }
            else
            {
                MensajeError += "El campo ID_AFILIACION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarEpsDeNomEmpleadosPorIdEmpleado(Decimal ID_EMPLEADO, Decimal ID_EPS, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_empleados_actualizar_eps_por_idEmpleado ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EPS != 0)
            {
                sql += ID_EPS + ", ";
                informacion += "ID_EPS = '" + ID_EPS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EPS no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarCajaCDeNomEmpleadosPorIdEmpleado(Decimal ID_EMPLEADO, Decimal ID_CAJA_C, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_empleados_actualizar_caja_por_idEmpleado ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CAJA_C != 0)
            {
                sql += ID_CAJA_C + ", ";
                informacion += "ID_CAJA_C = '" + ID_CAJA_C.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CAJA_C = '0', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarAfpDeNomEmpleadosPorIdEmpleado(Decimal ID_EMPLEADO, Decimal ID_F_PENSIONES, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_empleados_actualizar_afp_por_idEmpleado ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_F_PENSIONES != 0)
            {
                sql += ID_F_PENSIONES + ", ";
                informacion += "ID_F_PENSIONES = '" + ID_F_PENSIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_F_PENSIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }
        #endregion

        #region REG_DOCS_RADICACION_AFILIACIONES

        public DataTable ObtenerDocsRadicacionPorCOntratoYEntidad(Decimal ID_CONTRATO
            , String ENTIDAD_AFILIACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_docs_radicacion_afiliaciones_obtener_por_id_contrato_y_entidad ";

            if (ID_CONTRATO != 0)
            {
                sql += ID_CONTRATO + ", ";
                informacion += "ID_CONTRATO = '" + ID_CONTRATO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ENTIDAD_AFILIACION) == false)
            {
                sql += "'" + ENTIDAD_AFILIACION + "'";
                informacion += "ENTIDAD_AFILIACION = '" + ENTIDAD_AFILIACION + "'";
            }
            else
            {
                MensajeError += "El campo ENTIDAD_AFILIACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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


        #endregion REG_DOCS_RADICACION_AFILIACIONES

    }
}
