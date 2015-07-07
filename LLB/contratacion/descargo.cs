using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.contratacion
{
    public class descargo
    {
        #region variables
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
        public descargo(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public Decimal Adicionar(Decimal ID_EMPLEADO, DateTime FECHA_R, String OBS_REG, DateTime FECHA_SOLICITUD, DateTime FECHA_ACTA, DateTime FECHA_CIERRE, String MOTIVO, Byte[] ARCHIVO_ACTA, Int32 ARCHIVO_TAMANO, String ARCHIVO_EXTENSION, String ARCHIVO_TYPE)
        {
            #region validaciones
            if (ID_EMPLEADO == 0)
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser 0\n";
                return 0;
            }

            if (String.IsNullOrEmpty(MOTIVO))
            {
                MensajeError += "El campo MOTIVO no puede ser nulo\n";
                return 0;
            }
            #endregion validaciones

            if (ARCHIVO_ACTA == null)
            {
                return ingresarActaDescargosSinArchivo(ID_EMPLEADO, FECHA_R, OBS_REG, FECHA_SOLICITUD, FECHA_ACTA, FECHA_CIERRE, MOTIVO);
            }
            else
            {
                return ingresarActaDescargosConArchivo(ID_EMPLEADO, FECHA_R, OBS_REG, FECHA_SOLICITUD, FECHA_ACTA, FECHA_CIERRE, MOTIVO, ARCHIVO_ACTA, ARCHIVO_TAMANO, ARCHIVO_EXTENSION, ARCHIVO_TYPE);
            }
        }

        private Decimal ingresarActaDescargosSinArchivo(Decimal ID_EMPLEADO, DateTime FECHA_R, String OBS_REG, DateTime FECHA_SOLICITUD, DateTime FECHA_ACTA, DateTime FECHA_CIERRE, String MOTIVO)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_acta_desc_adicionar ";

            #region validaciones
            sql += ID_EMPLEADO + ", ";
            informacion += "ID_EMPLEADO = " + ID_EMPLEADO + ", ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + FECHA_R.ToString() + "', ";

            if (String.IsNullOrEmpty(OBS_REG) == false)
            {
                sql += "'" + OBS_REG + "', ";
                informacion += "OBS_REG = '" + OBS_REG + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBS_REG = 'NULL', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_SOLICITUD) + "', ";
            informacion += "FECHA_SOLICITUD = '" + FECHA_R.ToString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACTA) + "', ";
            informacion += "FECHA_ACTA = '" + FECHA_R.ToString() + "', ";

            if (FECHA_CIERRE == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FECHA_CIERRE = 'NULL', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";
                informacion += "FECHA_CIERRE = '" + FECHA_R.ToString() + "', ";
            }

            sql += "'" + MOTIVO + "', ";
            informacion += "MOTIVO = '" + MOTIVO + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar descargo
                    ID = conexion.ExecuteScalar(sql);
                    #endregion adicionar descargo

                    #region auditoria
                    if (Convert.ToDecimal(ID) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_ACTA_DESC, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            ejecutadoCorrectamente = false;
                            MensajeError = "ERROR: Al intentar registrar la auditoría.";
                            conexion.DeshacerTransaccion();
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                    else
                    {
                        ejecutadoCorrectamente = false;
                        conexion.DeshacerTransaccion();
                    }
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (ejecutadoCorrectamente)
            {
                return Convert.ToDecimal(ID);
            }
            else
            {
                return 0;
            }
        }

        private Decimal ingresarActaDescargosConArchivo(Decimal ID_EMPLEADO, DateTime FECHA_R, String OBS_REG, DateTime FECHA_SOLICITUD, DateTime FECHA_ACTA, DateTime FECHA_CIERRE, String MOTIVO, Byte[] ARCHIVO_ACTA, Int32 ARCHIVO_TAMANO, String ARCHIVO_EXTENSION, String ARCHIVO_TYPE)
        {
            String ID = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_acta_desc_adicionar_con_archivo ";

            #region validaciones
            sql += ID_EMPLEADO + ", ";
            informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";

            sql += "'" + FECHA_R.ToString() + "', ";
            informacion += "FECHA_R = '" + FECHA_R.ToString() + "', ";

            if (String.IsNullOrEmpty(OBS_REG) == false)
            {
                sql += "'" + OBS_REG + "', ";
                informacion += "OBS_REG = '" + OBS_REG + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBS_REG = 'NULL', ";
            }

            sql += "'" + FECHA_SOLICITUD.ToString() + "', ";
            informacion += "FECHA_SOLICITUD = '" + FECHA_SOLICITUD.ToString() + "', ";

            sql += "'" + FECHA_ACTA.ToString() + "', ";
            informacion += "FECHA_ACTA = '" + FECHA_ACTA.ToString() + "', ";

            if (FECHA_CIERRE == new DateTime())
            {
                sql += "'NULL', ";
                informacion += "FECHA_CIERRE = 'NULL', ";
            }
            else
            {
                sql += "'" + FECHA_CIERRE.ToString() + "', ";
                informacion += "FECHA_CIERRE = '" + FECHA_CIERRE.ToString() + "', ";
            }

            sql += "'" + MOTIVO + "', ";
            informacion += "MOTIVO = '" + MOTIVO + "', ";

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar descargo
                    ID = conexion.ExecuteEscalarParaAdicionarDescargoConArchivo(ID_EMPLEADO, FECHA_R, OBS_REG, FECHA_SOLICITUD, FECHA_ACTA, FECHA_CIERRE, MOTIVO, ARCHIVO_ACTA, ARCHIVO_TAMANO, ARCHIVO_EXTENSION, ARCHIVO_TYPE, Usuario);
                    #endregion adicionar descargo

                    #region auditoria
                    if (Convert.ToDecimal(ID) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_ACTA_DESC, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: intenatar ingresar en la bd el descargo.";
                        ejecutadoCorrectamente = false;
                    }
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (ejecutadoCorrectamente)
            {
                return Convert.ToDecimal(ID);
            }
            else
            {
                return 0;
            }
        }

        public Boolean Actualizar(Decimal REGISTRO, Decimal ID_EMPLEADO, DateTime FECHA_R, String OBS_REG, DateTime FECHA_SOLICITUD, DateTime FECHA_ACTA, DateTime FECHA_CIERRE, String MOTIVO, Byte[] ARCHIVO_ACTA, Int32 ARCHIVO_TAMANO, String ARCHIVO_EXTENSION, String ARCHIVO_TYPE)
        {
            #region validaciones
            if (REGISTRO == 0)
            {
                MensajeError += "El campo REGISTRO no puede ser 0\n";
                return false;
            }

            if (ID_EMPLEADO == 0)
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser 0\n";
                return false;
            }

            if (String.IsNullOrEmpty(MOTIVO) == true)
            {
                MensajeError = "El campo MOTIVO no puede ser 0\n";
                return false;
            }
            #endregion validaciones

            if (ARCHIVO_ACTA == null)
            {
                return actualizarDescargoSinArchivo(REGISTRO, ID_EMPLEADO, FECHA_R, OBS_REG, FECHA_SOLICITUD, FECHA_ACTA, FECHA_CIERRE, MOTIVO);
            }
            else
            {
                if (actualizarDescargoConArchivo(REGISTRO, ID_EMPLEADO, FECHA_R, OBS_REG, FECHA_SOLICITUD, FECHA_ACTA, FECHA_CIERRE, MOTIVO, ARCHIVO_ACTA, ARCHIVO_TAMANO, ARCHIVO_EXTENSION, ARCHIVO_TYPE) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private Boolean actualizarDescargoSinArchivo(Decimal REGISTRO, Decimal ID_EMPLEADO, DateTime FECHA_R, String OBS_REG, DateTime FECHA_SOLICITUD, DateTime FECHA_ACTA, DateTime FECHA_CIERRE, String MOTIVO)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_acta_desc_actualizar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            if (String.IsNullOrEmpty(OBS_REG) == false)
            {
                sql += "'" + OBS_REG + "', ";
                informacion += "OBS_REG = '" + OBS_REG + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBS_REG = 'NULL', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_SOLICITUD) + "', ";
            informacion += "FECHA_SOLICITUD = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_SOLICITUD) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACTA) + "', ";
            informacion += "FECHA_ACTA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACTA) + "', ";

            if (FECHA_CIERRE == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FECHA_CIERRE = 'NULL', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";
                informacion += "FECHA_CIERRE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";
            }

            if (String.IsNullOrEmpty(MOTIVO) == false)
            {
                sql += "'" + MOTIVO + "', ";
                informacion += "MOTIVO = '" + MOTIVO + "', ";
            }
            else
            {
                MensajeError += "El campo MOTIVO no puede ser 0\n";
                ejecutar = false;

            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    if (conexion.ExecuteNonQuery(sql) == 0)
                    {
                        ejecutadoCorrectamente = false;
                        conexion.DeshacerTransaccion();
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_ACTA_DESC, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            ejecutadoCorrectamente = false;
                            MensajeError = "ERROR: Al intentar realizar la auditoría.";
                            conexion.DeshacerTransaccion();
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                        #endregion auditoria
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
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

        private Decimal actualizarDescargoConArchivo(Decimal REGISTRO, Decimal ID_EMPLEADO, DateTime FECHA_R, String OBS_REG, DateTime FECHA_SOLICITUD, DateTime FECHA_ACTA, DateTime FECHA_CIERRE, String MOTIVO, Byte[] ARCHIVO_ACTA, Int32 ARCHIVO_TAMANO, String ARCHIVO_EXTENSION, String ARCHIVO_TYPE)
        {
            Int32 actualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_acta_desc_actualizar_con_archivo ";

            #region validaciones
            sql += REGISTRO + ", ";
            informacion += "REGISTRO = '" + REGISTRO + "', ";

            sql += ID_EMPLEADO + ", ";
            informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            if (String.IsNullOrEmpty(OBS_REG) == false)
            {
                sql += "'" + OBS_REG + "', ";
                informacion += "OBS_REG = '" + OBS_REG + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBS_REG = 'NULL', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_SOLICITUD) + "', ";
            informacion += "FECHA_SOLICITUD = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_SOLICITUD) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACTA) + "', ";
            informacion += "FECHA_ACTA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACTA) + "', ";

            if (FECHA_CIERRE == new DateTime())
            {
                sql += "'NULL', ";
                informacion += "FECHA_CIERRE = 'NULL', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";
                informacion += "FECHA_CIERRE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_CIERRE) + "', ";
            }

            sql += "'" + MOTIVO + "', ";
            informacion += "MOTIVO = '" + MOTIVO + "', ";

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region actualizar descargo
                    actualizados = conexion.ExecuteNonQueryParaActualizarDescargoConArchivo(REGISTRO, ID_EMPLEADO, FECHA_R, OBS_REG, FECHA_SOLICITUD, FECHA_ACTA, FECHA_CIERRE, MOTIVO, ARCHIVO_ACTA, ARCHIVO_TAMANO, ARCHIVO_EXTENSION, ARCHIVO_TYPE, Usuario);
                    #endregion adicionar descargo

                    #region auditoria
                    if (actualizados > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_ACTA_DESC, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: intenatar actualizar en la bd el descargo.";
                        ejecutadoCorrectamente = false;
                    }
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (ejecutadoCorrectamente)
            {
                return actualizados;
            }
            else
            {
                return 0;
            }
        }

        public DataTable ObtenerPorRegistro(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_acta_desc_obtenerPorRegistro ";

            #region validaciones

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = " + REGISTRO;
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }
            #endregion validaciones

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

        public DataTable ObtenerPorIdEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_acta_desc_ObtenerPorIdEmpleado ";

            #region validaciones

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO;
                informacion += "ID_EMPLEADO = " + ID_EMPLEADO;
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser 0\n";
                ejecutar = false;
            }
            #endregion validaciones

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

        public DataTable ObtenerProcesoDisciplinarioPorIdDescargo(Decimal REGISTRO_DESCARGO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_proceso_disciplinario_obtener_por_registro_descargo ";

            #region validaciones

            if (REGISTRO_DESCARGO != 0)
            {
                sql += REGISTRO_DESCARGO;
                informacion += "REGISTRO_DESCARGO = " + REGISTRO_DESCARGO;
            }
            else
            {
                MensajeError += "El campo REGISTRO_DESCARGO no puede ser 0\n";
                ejecutar = false;
            }
            #endregion validaciones

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

        public Decimal AdicionarProcesoDisciplinario(Decimal REGISTRO_DESCARGO, DateTime FECHA_R, DateTime FECHA_PROCESO, String TIPO_PROCESO, String MOTIVO, String DESCRIPCION, Decimal DIAS_SANCION, Byte[] ARCHIVO_REGISTRO,
            Int32 ARCHIVO_REGISTRO_TAMANO,
            String ARCHIVO_REGISTRO_EXTENSION,
            String ARCHIVO_REGISTRO_TYPE)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_proceso_disciplinario_adicionar ";

            #region validaciones
            if (REGISTRO_DESCARGO != 0)
            {
                sql += REGISTRO_DESCARGO + ", ";
                informacion += "REGISTRO_DESCARGO = " + REGISTRO_DESCARGO + ", ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_DESCARGO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PROCESO) + "', ";
            informacion += "FECHA_PROCESO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PROCESO) + "', ";

            if (!(String.IsNullOrEmpty(TIPO_PROCESO)))
            {
                sql += "'" + TIPO_PROCESO + "', ";
                informacion += "TIPO_PROCESO = '" + TIPO_PROCESO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_PROCESO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOTIVO)))
            {
                sql += "'" + MOTIVO + "', ";
                informacion += "MOTIVO = '" + MOTIVO + "', ";
            }
            else
            {
                sql += "'null, ";
                informacion += "MOTIVO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DESCRIPCION = 'null', ";
            }

            if (DIAS_SANCION != 0)
            {
                sql += DIAS_SANCION + ", ";
                informacion += "DIAS_SANCION = " + DIAS_SANCION + ", ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DIAS_SANCION = 'NULL', ";
            }

            sql += "'[DATOS_BINARIOS]', ";
            informacion += "ARCHIVO_REGISTRO = '[DATOS_BINARIOS]', ";

            sql += "'" + ARCHIVO_REGISTRO_EXTENSION + "', ";
            informacion += "ARCHIVO_REGISTRO_EXTENSION = '" + ARCHIVO_REGISTRO_EXTENSION + "', ";

            sql += ARCHIVO_REGISTRO_TAMANO + ", ";
            informacion += "ARCHIVO_REGISTRO_TAMANO = '" + ARCHIVO_REGISTRO_TAMANO + "', ";

            sql += "'" + ARCHIVO_REGISTRO_TYPE + "', ";
            informacion += "ARCHIVO_REGISTRO_TYPE = '" + ARCHIVO_REGISTRO_TYPE + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    ID = conexion.ExecuteEscalarParaAdicionarProcesoDisciplianario(REGISTRO_DESCARGO, FECHA_R, FECHA_PROCESO, TIPO_PROCESO, MOTIVO, DESCRIPCION, DIAS_SANCION, ARCHIVO_REGISTRO, ARCHIVO_REGISTRO_EXTENSION, ARCHIVO_REGISTRO_TAMANO, ARCHIVO_REGISTRO_TYPE, Usuario);

                    if ((String.IsNullOrEmpty(ID) == true) || (ID == "0"))
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: al intentar adicionar el proceso.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (TIPO_PROCESO == "SA")
                        {
                            if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_PROCESOS_DISCIPLINARIOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                            {
                                conexion.DeshacerTransaccion();
                                ejecutadoCorrectamente = false;
                            }
                            else
                            {
                                conexion.AceptarTransaccion();
                            }
                        }
                        else
                        {
                            if (terminarProcesoDescargo(REGISTRO_DESCARGO, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                ejecutadoCorrectamente = false;
                            }
                            else
                            {
                                if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_PROCESOS_DISCIPLINARIOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                                {
                                    conexion.DeshacerTransaccion();
                                    ejecutadoCorrectamente = false;
                                }
                                else
                                {
                                    conexion.AceptarTransaccion();
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (ejecutadoCorrectamente) return Convert.ToDecimal(ID);
            else return 0;
        }

        public int AdicionarArchivoNotificacionProcesoDisciplinario(Decimal ID_PROCESO, Decimal REGISTRO_DESCARGO, Byte[] ARCHIVO_NOTIFICACION,
            Int32 ARCHIVO_NOTIFICACION_TAMANO,
            String ARCHIVO_NOTIFICACION_EXTENSION,
            String ARCHIVO_NOTIFICACION_TYPE)
        {
            String sql = null;
            Int32 registrosActualizados = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_proceso_disciplinario_actualizar_notificacion ";

            #region validaciones
            if (ID_PROCESO != 0)
            {
                sql += ID_PROCESO + ", ";
                informacion += "ID_PROCESO = " + ID_PROCESO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_PROCESO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'[DATOS_BINARIOS]', ";
            informacion += "ARCHIVO_NOTIFICACION = '[DATOS_BINARIOS]', ";

            sql += "'" + ARCHIVO_NOTIFICACION_EXTENSION + "', ";
            informacion += "ARCHIVO_NOTIFICACION_EXTENSION = '" + ARCHIVO_NOTIFICACION_EXTENSION + "', ";

            sql += ARCHIVO_NOTIFICACION_TAMANO + ", ";
            informacion += "ARCHIVO_NOTIFICACION_TAMANO = '" + ARCHIVO_NOTIFICACION_TAMANO + "', ";

            sql += "'" + ARCHIVO_NOTIFICACION_TYPE + "', ";
            informacion += "ARCHIVO_NOTIFICACION_TYPE = '" + ARCHIVO_NOTIFICACION_TYPE + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    registrosActualizados = conexion.ExecuteNonQueryParaAdicionarArchivoNotificacionProcesoDisciplianario(ID_PROCESO, ARCHIVO_NOTIFICACION, ARCHIVO_NOTIFICACION_EXTENSION, ARCHIVO_NOTIFICACION_TAMANO, ARCHIVO_NOTIFICACION_TYPE, Usuario);

                    if (registrosActualizados <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: al intentar actualizar el proceso.";
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);

                        if (terminarProcesoDescargo(REGISTRO_DESCARGO, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                        }
                        else
                        {
                            if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_PROCESOS_DISCIPLINARIOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                            {
                                conexion.DeshacerTransaccion();
                            }
                            else
                            {
                                conexion.AceptarTransaccion();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return registrosActualizados;
        }

        public Boolean terminarProcesoDescargo(Decimal REGISTRO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_acta_desc_terminar_proceso ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_DEMANDAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public DataTable ObtenerProcesoDisciplinarioPorIdProceso(Decimal ID_PROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_proceso_disciplinario_obtener_por_id_proceso ";

            #region validaciones

            if (ID_PROCESO != 0)
            {
                sql += ID_PROCESO;
                informacion += "ID_PROCESO = " + ID_PROCESO;
            }
            else
            {
                MensajeError += "El campo ID_PROCESO no puede ser 0\n";
                ejecutar = false;
            }
            #endregion validaciones

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