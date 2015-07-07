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
    public class requerimientosMinisterio
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
        public requerimientosMinisterio(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region con_reg_requerimientos_ministerio
        public DataTable ObtenerRequerimientosMinTodos(Int32 ACTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_requerimientos_ministerio_obtener_todos ";

            #region validaciones
            sql += ACTIVO.ToString();
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

        public Decimal AdicionarNuevoRequerimientoMinisterio(DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO,
            Decimal ID_ABOGADO,
            String ID_CIUDAD,
            String INSPECCION,
            String RECLAMACIONES,
            String OBSERVACIONES)
        {
            Decimal ID_REQUERIMIENTO_M = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_requerimientos_ministerio_adicionar ";

            #region validaciones

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";
            informacion += "FECHA_NOTIFICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO) + "', ";
            informacion += "FECHA_PLAZO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO) + "', ";

            if (ID_ABOGADO != 0)
            {
                sql += ID_ABOGADO + ", ";
                informacion += "ID_ABOGADO = '" + ID_ABOGADO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ABOGADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(INSPECCION) == false)
            {
                sql += "'" + INSPECCION + "', ";
                informacion += "INSPECCION = '" + INSPECCION + "', ";
            }
            else
            {
                MensajeError = "El campo INSPECCION no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RECLAMACIONES) == false)
            {
                sql += "'" + RECLAMACIONES + "', ";
                informacion += "RECLAMACIONES = '" + RECLAMACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo RECLAMACIONES no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo OBSERVACIONES no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    ID_REQUERIMIENTO_M = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_REQUERIMIENTO_M <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para el requerimiento al ministerio.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_REQUERIMIENTOS_MINISTERIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de la requerimiento al ministerio.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
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
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return ID_REQUERIMIENTO_M;
            }
            else
            {
                return 0;
            }
        }

        public DataTable ObtenerRequeriminetosPorIdReq(Decimal ID_REQUERIMINETO_M)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_requerimientos_ministerio_obtener_por_id_requerimiento ";

            #region validaciones
            if (ID_REQUERIMINETO_M != 0)
            {
                sql += ID_REQUERIMINETO_M;
            }
            else
            {
                MensajeError = "El campo ID_REQUERIMINETO_M no puede ser nulo.";
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

        public Boolean ActualizarRequerimientoMinisterio(Decimal ID_REQUERIMIENTO_M,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO,
            Decimal ID_ABOGADO,
            String ID_CIUDAD,
            String INSPECCION,
            String RECLAMACIONES,
            String OBSERVACIONES)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_requerimientos_ministerio_actualizar ";

            #region validaciones

            if (ID_REQUERIMIENTO_M != 0)
            {
                sql += ID_REQUERIMIENTO_M + ", ";
                informacion += "ID_REQUERIMIENTO_M = '" + ID_REQUERIMIENTO_M + "', ";
            }
            else
            {
                MensajeError = "El campo ID_REQUERIMIENTO_M no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";
            informacion += "FECHA_NOTIFICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO) + "', ";
            informacion += "FECHA_PLAZO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO) + "', ";

            if (ID_ABOGADO != 0)
            {
                sql += ID_ABOGADO + ", ";
                informacion += "ID_ABOGADO = '" + ID_ABOGADO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ABOGADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(INSPECCION) == false)
            {
                sql += "'" + INSPECCION + "', ";
                informacion += "INSPECCION = '" + INSPECCION + "', ";
            }
            else
            {
                MensajeError = "El campo INSPECCION no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RECLAMACIONES) == false)
            {
                sql += "'" + RECLAMACIONES + "', ";
                informacion += "RECLAMACIONES = '" + RECLAMACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo RECLAMACIONES no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo OBSERVACIONES no puede ser vacio.";
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
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar actualizar el registro de requerimiento al ministerio.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_INVSTIGACIONES_ADMINISTRATIVAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de la requerimiento al ministerio.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
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

        public Boolean terminarProcesoRequerimientoMinisterio(Decimal ID_REQUERIMIENTO_M,
            Byte[] ARCHIVO_ACTA,
            Int32 ARCHIVO_ACTA_TAMANO,
            String ARCHIVO_ACTA_EXTENSION,
            String ARCHIVO_ACTA_TYPE)
        {
            Decimal ID_ARCHIVO = 0;
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_requerimientos_ministerio_terminar_proceso ";

            #region validaciones
            if (ID_REQUERIMIENTO_M != 0)
            {
                sql += ID_REQUERIMIENTO_M + ", ";
                informacion += "ID_REQUERIMIENTO_M = '" + ID_REQUERIMIENTO_M + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO_M no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    if (cantidadRegistrosActualizados <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: No se pudo cerrar el requerimineto debido a problemas con el UPDATE.";
                    }
                    else
                    {
                        ID_ARCHIVO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaArchivoRequermientoMinisterio(ID_REQUERIMIENTO_M, DateTime.Now, "ACTA", ARCHIVO_ACTA, ARCHIVO_ACTA_EXTENSION, ARCHIVO_ACTA_TAMANO, ARCHIVO_ACTA_TYPE, Usuario));

                        if (ID_ARCHIVO <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: No se pudo cerrar el requerimineto debido a problemas con el UPDATE.";
                            cantidadRegistrosActualizados = 0;
                        }
                        else
                        {
                            auditoria _auditoria = new auditoria(Empresa);
                            if (_auditoria.Adicionar(Usuario, tabla.CON_REG_REQUERIMIENTOS_MINISTERIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: No se pudo cerrar el requerimineto debido a problemas con el UPDATE.";
                                cantidadRegistrosActualizados = 0;
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
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }
        #endregion con_reg_requerimientos_ministerio

        #region reg_archivos_requerimientos_ministerio
        #endregion reg_archivos_requerimientos_ministerio

        #region REG_ADJUNTOS_REQUERIMIENTOS_MINISTERIO

        public DataTable ObtenerAdjuntosRequerimientoPorIdRequerimiento(Decimal ID_REQUERIMIENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_adjuntos_requerimientos_ministerio_obtener_por_id_requerimiento ";

            #region validaciones
            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO + "'";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_REQUERIMIENTO = 'NULL'";
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

        public Decimal AdicionarAdjuntoRequerimientoMinisterio(Decimal ID_REQUERIMIENTO,
            DateTime FECHA_R,
            DateTime FECHA_ADJUNTO,
            String TITULO,
            String DESCRIPCION,
            Byte[] ARCHIVO_ADJUNTO,
            String ARCHIVO_ADJUNTO_EXTENSION,
            Decimal ARCHIVO_ADJUNTO_TAMANO,
            String ARCHIVO_ADJUNTO_TYPE)
        {
            String ID = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_adjuntos_requerimientos_ministerio_adicionar ";

            #region validaciones

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_REQUERIMIENTO no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ADJUNTO) + "', ";
            informacion += "FECHA_ADJUNTO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ADJUNTO) + "', ";

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                MensajeError = "El campo TITULO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError = "El campo DESCRIPCION no puede ser vacio.";
                ejecutar = false;
            }

            if (ARCHIVO_ADJUNTO != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO_ADJUNTO = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_ADJUNTO_EXTENSION + "', ";
                informacion += "ARCHIVO_ADJUNTO_EXTENSION = '" + ARCHIVO_ADJUNTO_EXTENSION + "', ";

                sql += ARCHIVO_ADJUNTO_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_ADJUNTO_TAMANO = '" + ARCHIVO_ADJUNTO_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_ADJUNTO_TYPE + "', ";
                informacion += "ARCHIVO_ADJUNTO_TYPE = '" + ARCHIVO_ADJUNTO_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO_ADJUNTO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_ADJUNTO_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_ADJUNTO_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_ADJUNTO_TYPE = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar adjunto
                    ID = conexion.ExecuteEscalarParaAdicionarAdjuntoRequerimientoMinisterio(ID_REQUERIMIENTO, FECHA_R, FECHA_ADJUNTO, TITULO, DESCRIPCION, ARCHIVO_ADJUNTO, ARCHIVO_ADJUNTO_EXTENSION, ARCHIVO_ADJUNTO_TAMANO, ARCHIVO_ADJUNTO_TYPE, Usuario);
                    #endregion adicionar adjunto

                    #region auditoria
                    if (Convert.ToDecimal(ID) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_ADJUNTOS_REQUERIMIENTOS_MINISTERIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
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
                        MensajeError = "ERROR: intenatar ingresar en la bd el adjunto.";
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

        public DataTable ObtenerInfoAdjuntoPorIdAdjuntoRequerimiento(Decimal ID_ADJUNTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_adjuntos_requerimientos_ministerio_obtenerPorIdAdjunto ";

            #region validaciones

            if (ID_ADJUNTO != 0)
            {
                sql += ID_ADJUNTO + "";
                informacion += "ID_ADJUNTO = '" + ID_ADJUNTO + "'";
            }
            else
            {
                MensajeError = "El campo ID_ADJUNTO no puede ser nullo.";
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
        #endregion REG_ADJUNTOS_REQUERIMIENTOS_MINISTERIO
        #endregion metodos
    }
}