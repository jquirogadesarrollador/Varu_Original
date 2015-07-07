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
    public class investigacionesAdministrativas
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
        public investigacionesAdministrativas(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region con_reg_investigaciones_administrativas
        public DataTable ObtenerinvestigacionesTodas(Int32 ACTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_investigaciones_administrativas_obtener_todas ";

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

        public DataTable ObtenerInvestigacionesPorIdInvestigacion(Decimal ID_INVESTIGACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_investigaciones_administrativas_obtener_por_id_investigacion ";

            #region validaciones
            if (ID_INVESTIGACION != 0)
            {
                sql += ID_INVESTIGACION;
            }
            else
            {
                MensajeError = "El campo ID_INVESTIGACION no puede ser nulo.";
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

        public Decimal AdicionarNuevainvestigacionAdminsitrativa(DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            String ENTIDAD_INVESTIGA,
            String ASUNTO,
            Decimal ID_ABOGADO,
            String OBSERVACIONES,
            Byte[] ARCHIVO_ASUNTO,
            Decimal ARCHIVO_ASUNTO_TAMANO,
            String ARCHIVO_ASUNTO_EXTENSION,
            String ARCHIVO_ASUNTO_TYPE,
            Byte[] ARCHIVO_RESOLUCION,
            Decimal ARCHIVO_RESOLUCION_TAMANO,
            String ARCHIVO_RESOLUCION_EXTENSION,
            String ARCHIVO_RESOLUCION_TYPE,
            DateTime FECHA_PLAZO_CONTESTA)
        {
            Decimal ID_INVESTIGACION = 0;
            Decimal ID_DOCUMENTO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_investigaciones_administrativas_adicionar ";

            #region validaciones

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";
            informacion += "FECHA_NOTIFICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";

            if (String.IsNullOrEmpty(ENTIDAD_INVESTIGA) == false)
            {
                sql += "'" + ENTIDAD_INVESTIGA + "', ";
                informacion += "ENTIDAD_INVESTIGA = '" + ENTIDAD_INVESTIGA + "', ";
            }
            else
            {
                MensajeError = "El campo ENTIDAD_INVESTIGA no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ASUNTO) == false)
            {
                sql += "'" + ASUNTO + "', ";
                informacion += "ASUNTO = '" + ASUNTO + "', ";
            }
            else
            {
                MensajeError = "El campo ASUNTO no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_ABOGADO > 0)
            {
                sql += ID_ABOGADO + ", ";
                informacion += "ID_ABOGADO = '" + ID_ABOGADO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ABOGADO no puede ser vacio.";
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

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "'";
            informacion += "FECHA_PLAZO_CONTESTA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    ID_INVESTIGACION = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_INVESTIGACION <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro de investigacion administrativa.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        if (ARCHIVO_ASUNTO != null)
                        {
                            ID_DOCUMENTO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaArchivoInvestigacionAdministrativa(ID_INVESTIGACION, FECHA_R, "ASUNTO", ARCHIVO_ASUNTO, ARCHIVO_ASUNTO_EXTENSION, ARCHIVO_ASUNTO_TAMANO, ARCHIVO_ASUNTO_TYPE, Usuario));

                            if (ID_DOCUMENTO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Al intentar ingresar el nuevo registro del archivo de asunto para la investigacion.";
                                ejecutadoCorrectamente = false;
                            }
                        }

                        if (ejecutadoCorrectamente == true)
                        {
                            if (ARCHIVO_RESOLUCION != null)
                            {
                                ID_DOCUMENTO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaArchivoInvestigacionAdministrativa(ID_INVESTIGACION, FECHA_R, "RESOLUCION", ARCHIVO_RESOLUCION, ARCHIVO_RESOLUCION_EXTENSION, ARCHIVO_RESOLUCION_TAMANO, ARCHIVO_RESOLUCION_TYPE, Usuario));

                                if (ID_DOCUMENTO <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "ERROR: Al intentar ingresar el nuevo registro del archivo de resolución para la investigacion.";
                                    ejecutadoCorrectamente = false;
                                }
                            }
                        }

                        if (ejecutadoCorrectamente == true)
                        {
                            auditoria _auditoria = new auditoria(Empresa);
                            if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_INVSTIGACIONES_ADMINISTRATIVAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de la investigacion administrativa.";
                                ejecutadoCorrectamente = false;
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
                return ID_INVESTIGACION;
            }
            else
            {
                return 0;
            }
        }


        public Boolean ActualizarInvestigacionAdminsitrativa(Decimal ID_INVESTIGACION,
            DateTime FECHA_NOTIFICACION,
            String ENTIDAD_INVESTIGA,
            String ASUNTO,
            Decimal ID_ABOGADO,
            String OBSERVACIONES,
            Byte[] ARCHIVO_ASUNTO,
            Decimal ARCHIVO_ASUNTO_TAMANO,
            String ARCHIVO_ASUNTO_EXTENSION,
            String ARCHIVO_ASUNTO_TYPE,
            Byte[] ARCHIVO_RESOLUCION,
            Decimal ARCHIVO_RESOLUCION_TAMANO,
            String ARCHIVO_RESOLUCION_EXTENSION,
            String ARCHIVO_RESOLUCION_TYPE,
            DateTime FECHA_PLAZO_CONTESTA)
        {
            Decimal ID_DOCUMENTO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_con_reg_investigaciones_administrativas_actualizar ";

            #region validaciones

            if (ID_INVESTIGACION != 0)
            {
                sql += ID_INVESTIGACION + ", ";
                informacion += "ID_INVESTIGACION = '" + ID_INVESTIGACION + "', ";
            }
            else
            {
                MensajeError = "El campo ENTIDAD_INVESTIGA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";
            informacion += "FECHA_NOTIFICACION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_NOTIFICACION) + "', ";

            if (String.IsNullOrEmpty(ENTIDAD_INVESTIGA) == false)
            {
                sql += "'" + ENTIDAD_INVESTIGA + "', ";
                informacion += "ENTIDAD_INVESTIGA = '" + ENTIDAD_INVESTIGA + "', ";
            }
            else
            {
                MensajeError = "El campo ENTIDAD_INVESTIGA no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ASUNTO) == false)
            {
                sql += "'" + ASUNTO + "', ";
                informacion += "ASUNTO = '" + ASUNTO + "', ";
            }
            else
            {
                MensajeError = "El campo ASUNTO no puede ser vacio.";
                ejecutar = false;
            }

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

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "'";
            informacion += "FECHA_PLAZO_CONTESTA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_PLAZO_CONTESTA) + "'";

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
                        MensajeError = "ERROR: Al intentar actualizar el registro de investigacion administrativa.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        DataTable tablaInfoArchivo;
                        if (ARCHIVO_ASUNTO != null)
                        {
                            tablaInfoArchivo = ObtenerArchivoInvestigacion(ID_INVESTIGACION, "ASUNTO");
                            if (tablaInfoArchivo.Rows.Count <= 0)
                            {
                                ID_DOCUMENTO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaArchivoInvestigacionAdministrativa(ID_INVESTIGACION, DateTime.Now, "ASUNTO", ARCHIVO_ASUNTO, ARCHIVO_ASUNTO_EXTENSION, ARCHIVO_ASUNTO_TAMANO, ARCHIVO_ASUNTO_TYPE, Usuario));

                                if (ID_DOCUMENTO <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "ERROR: Al intentar ingresar el nuevo registro del archivo de asunto para la investigacion.";
                                    ejecutadoCorrectamente = false;
                                }
                            }
                            else
                            {
                                DataRow filaInfoArchivo = tablaInfoArchivo.Rows[0];

                                if (conexion.ExecuteNonQueryParaActualizarArchivoInvestigacionAdministrativa(Convert.ToDecimal(filaInfoArchivo["ID_ARCHIVO_I"]), ARCHIVO_ASUNTO, ARCHIVO_ASUNTO_EXTENSION, ARCHIVO_ASUNTO_TAMANO, ARCHIVO_ASUNTO_TYPE, Usuario) <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "ERROR: Al intentar ingresar el nuevo registro del archivo de asunto para la investigacion.";
                                    ejecutadoCorrectamente = false;
                                }
                            }
                        }

                        if (ejecutadoCorrectamente == true)
                        {
                            if (ARCHIVO_RESOLUCION != null)
                            {
                                tablaInfoArchivo = ObtenerArchivoInvestigacion(ID_INVESTIGACION, "RESOLUCION");
                                if (tablaInfoArchivo.Rows.Count <= 0)
                                {
                                    ID_DOCUMENTO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaArchivoInvestigacionAdministrativa(ID_INVESTIGACION, DateTime.Now, "RESOLUCION", ARCHIVO_RESOLUCION, ARCHIVO_RESOLUCION_EXTENSION, ARCHIVO_RESOLUCION_TAMANO, ARCHIVO_RESOLUCION_TYPE, Usuario));

                                    if (ID_DOCUMENTO <= 0)
                                    {
                                        conexion.DeshacerTransaccion();
                                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro del archivo de resolución para la investigacion.";
                                        ejecutadoCorrectamente = false;
                                    }
                                }
                                else
                                {
                                    DataRow filaInfoArchivo = tablaInfoArchivo.Rows[0];

                                    if (conexion.ExecuteNonQueryParaActualizarArchivoInvestigacionAdministrativa(Convert.ToDecimal(filaInfoArchivo["ID_ARCHIVO_I"]), ARCHIVO_RESOLUCION, ARCHIVO_RESOLUCION_EXTENSION, ARCHIVO_RESOLUCION_TAMANO, ARCHIVO_RESOLUCION_TYPE, Usuario) <= 0)
                                    {
                                        conexion.DeshacerTransaccion();
                                        MensajeError = "ERROR: Al intentar actualizar el registro del archivo de resolución para la investigacion.";
                                        ejecutadoCorrectamente = false;
                                    }
                                }
                            }
                        }

                        if (ejecutadoCorrectamente == true)
                        {
                            auditoria _auditoria = new auditoria(Empresa);
                            if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_INVSTIGACIONES_ADMINISTRATIVAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de la investigacion administrativa.";
                                ejecutadoCorrectamente = false;
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

        public Boolean cerrarInvestigacionAdministrativa(Decimal ID_INVESTIGACION)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_investigaciones_administrativas_cerrar ";

            #region validaciones
            if (ID_INVESTIGACION != 0)
            {
                sql += ID_INVESTIGACION + ", ";
                informacion += "ID_INVESTIGACION = '" + ID_INVESTIGACION + "', ";
            }
            else
            {
                MensajeError += "El campo ID_INVESTIGACION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_INVSTIGACIONES_ADMINISTRATIVAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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
        #endregion con_reg_investigaciones_administrativas

        #region reg_archivos_investigaciones_administrativas
        public DataTable ObtenerArchivoInvestigacion(Decimal ID_INVESTIGACION, String TIPO_DOCUMENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_archivos_investigaciones_administrativas_obtener_archivo ";

            #region validaciones
            if (ID_INVESTIGACION != 0)
            {
                sql += ID_INVESTIGACION + ", ";
            }
            else
            {
                MensajeError = "El campo ID_INVESTIGACION no puede ser nulo.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_DOCUMENTO) == false)
            {
                sql += "'" + TIPO_DOCUMENTO + "'";
            }
            else
            {
                MensajeError = "El campo TIPO_DOCUMENTO no puede ser nulo.";
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
        #endregion reg_archivos_investigaciones_administrativas


        #region REG_ADJUNTOS_INVESTIGACIONES_ADMINISTRATIVAS


        public DataTable ObtenerAdjuntosInvestigacionPorIdInvestigacion(Decimal ID_INVESTIGACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_adjuntos_investigaciones_administrativas_obtener_por_id_investigacion ";

            #region validaciones
            if (ID_INVESTIGACION != 0)
            {
                sql += ID_INVESTIGACION;
                informacion += "ID_INVESTIGACION = '" + ID_INVESTIGACION + "'";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_INVESTIGACION = 'NULL'";
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




        public Decimal AdicionarAdjuntoInvestigacionAdminsitrativa(Decimal ID_INVESTIGACION,
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

            sql = "usp_reg_adjuntos_investigaciones_administrativas_adicionar ";

            #region validaciones

            if (ID_INVESTIGACION != 0)
            {
                sql += ID_INVESTIGACION + ", ";
                informacion += "ID_INVESTIGACION = '" + ID_INVESTIGACION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_INVESTIGACION no puede ser vacio.";
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
                    ID = conexion.ExecuteEscalarParaAdicionarAdjuntoInvestigacionesAdministrativas(ID_INVESTIGACION, FECHA_R, FECHA_ADJUNTO, TITULO, DESCRIPCION, ARCHIVO_ADJUNTO, ARCHIVO_ADJUNTO_EXTENSION, ARCHIVO_ADJUNTO_TAMANO, ARCHIVO_ADJUNTO_TYPE, Usuario);
                    #endregion adicionar adjunto

                    #region auditoria
                    if (Convert.ToDecimal(ID) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_ADJUNTOS_INVESTIGACIONES_ADMINISTRATIVAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
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


        public DataTable ObtenerInfoAdjuntoPorIdAdjuntoINvestigacion(Decimal ID_ADJUNTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_adjuntos_investigaciones_administrativas_obtenerPorIdAdjunto ";

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

        #endregion REG_ADJUNTOS_INVESTIGACIONES_ADMINISTRATIVAS
        #endregion metodos
    }
}