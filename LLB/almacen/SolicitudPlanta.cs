using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.almacen
{
    public class SolicitudPlanta
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        Decimal _id_permiso = 0;
        Decimal _id_area = 0;
        Decimal _id_rol = 0;
        String _nivel = null;
        Boolean _autoriza = false;
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

        public Decimal ID_PERMISO
        {
            get { return _id_permiso; }
            set { _id_permiso = value; }
        }

        public Decimal ID_AREA
        {
            get { return _id_area; }
            set { _id_area = value; }
        }

        public Decimal ID_ROL
        {
            get { return _id_rol; }
            set { _id_rol = value; }
        }

        public String NIVEL
        {
            get { return _nivel; }
            set { _nivel = value; }
        }

        public Boolean AUTORIZA
        {
            get { return _autoriza; }
            set { _autoriza = value; }
        }
        #endregion propiedades

        #region constructores
        public SolicitudPlanta(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        public SolicitudPlanta()
        {
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerPermisosSobreSolicitudesPlanta()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_crt_permisos_solicitudes_obtenerActivos";

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

        public DataTable ObtenerPermisoDeUnUsuario(String USU_LOG)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_crt_permisos_solicitudes_obtenerPorUsuLog ";
            sql += "'" + USU_LOG + "'";

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

        public Boolean ActualizarRegistroDePermisoSobreSolicitudPlanta(Decimal ID_PERMISO,
            Decimal ID_AREA,
            Decimal ID_ROL,
            String NIVEL,
            Boolean AUTORIZA,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_crt_permisos_solicitudes_planta_actualizar ";

            #region validaciones
            if (ID_PERMISO != 0)
            {
                sql += ID_PERMISO + ", ";
                informacion += "ID_PERMISO = '" + ID_PERMISO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PERMISO no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_AREA != 0)
            {
                sql += ID_AREA + ", ";
                informacion += "ID_AREA = '" + ID_AREA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_AREA no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NIVEL) == false)
            {
                sql += "'" + NIVEL + "', ";
                informacion += "NIVEL = '" + NIVEL + "', ";
            }
            else
            {
                MensajeError = "El campo NIVEL no puede ser vacio.";
                ejecutar = false;
            }

            if (AUTORIZA == true)
            {
                sql += "'True', ";
                informacion += "AUTORIZA = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "AUTORIZA = 'False', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar desactivar el registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CRT_PERMISOS_SOLICITUDES_PLANTA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la desactivación del criterio.";
                            ejecutadoCorrectamente = false;
                        }
                    }
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

        public Decimal AdicionarPermisoSobreSolicitudPlanta(Decimal ID_AREA,
            Decimal ID_ROL,
            String NIVEL,
            Boolean AUTORIZA,
            Conexion conexion)
        {
            Decimal ID_PERMISO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_crt_permisos_solicitudes_planta_adicionar ";

            #region validaciones
            if (ID_AREA != 0)
            {
                sql += ID_AREA + ", ";
                informacion += "ID_AREA = '" + ID_AREA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_AREA no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NIVEL) == false)
            {
                sql += "'" + NIVEL + "', ";
                informacion += "NIVEL = '" + NIVEL + "', ";
            }
            else
            {
                MensajeError = "El campo NIVEL no puede ser vacio.";
                ejecutar = false;
            }

            if (AUTORIZA == true)
            {
                sql += "'True', ";
                informacion += "AUTORIZA = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "AUTORIZA = 'False', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_PERMISO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_PERMISO <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CRT_PERMISOS_SOLICITUDES_PLANTA, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro.";
                            ejecutadoCorrectamente = false;
                        }
                    }
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
                return ID_PERMISO;
            }
            else
            {
                return 0;
            }
        }

        public Boolean ActualizarPermisosSobreSolicitudesPlanta(List<SolicitudPlanta> listaPermisos)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean correcto = true;

            try
            {
                foreach (SolicitudPlanta permiso in listaPermisos)
                {
                    if (permiso.ID_PERMISO == 0)
                    {
                        if (AdicionarPermisoSobreSolicitudPlanta(permiso.ID_AREA, permiso.ID_ROL, permiso.NIVEL, permiso.AUTORIZA, conexion) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                    }
                    else
                    {
                        if (ActualizarRegistroDePermisoSobreSolicitudPlanta(permiso.ID_PERMISO, permiso.ID_AREA, permiso.ID_ROL, permiso.NIVEL, permiso.AUTORIZA, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
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
                correcto = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }


        public DataTable ObtenerInformacionParaGrillaSolicitudesPlanta(String NIVEL,
            Decimal ID_AREA,
            Decimal ID_ROL,
            Decimal ID_USUARIO,
            Boolean AUTORIZA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_solicitudes_planta_obtener_para_grilla ";

            if (String.IsNullOrEmpty(NIVEL) == false)
            {
                sql += "'" + NIVEL + "', ";
            }
            else
            {
                MensajeError = "El campo NIVEL no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_AREA != 0)
            {
                sql += ID_AREA + ", ";
            }
            else
            {
                MensajeError = "El campo ID_AREA no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO + ", ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO no puede ser vacio.";
                ejecutar = false;
            }

            if (AUTORIZA == true)
            {
                sql += "'True'";
            }
            else
            {
                sql += "'False'";
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

        public Decimal AdicionarSolicitudPlanta(DateTime FECHA_REGISTRO,
            Decimal ID_USUARIO_CREA,
            Decimal ID_PRODUCTO,
            Int32 CANTIDAD_REQUERIDA,
            String ESTADO,
            String OBSERVACIONES,
            DateTime FECHA_ENTREGA_SOLICITADA)
        {
            Decimal ID_SOLICITUD = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_solicitudes_planta_adicionar ";

            #region validaciones
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_REGISTRO) + "', ";
            informacion += "FECHA_RGISTRO = '" + FECHA_REGISTRO.ToShortDateString() + "', ";

            if (ID_USUARIO_CREA != 0)
            {
                sql += ID_USUARIO_CREA + ", ";
                informacion += "ID_USUARIO_CREA = '" + ID_USUARIO_CREA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO_CREA no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PRODUCTO no puede ser vacio.";
                ejecutar = false;
            }

            if (CANTIDAD_REQUERIDA != 0)
            {
                sql += CANTIDAD_REQUERIDA + ", ";
                informacion += "CANTIDAD_REQUERIDA = '" + CANTIDAD_REQUERIDA + "', ";
            }
            else
            {
                MensajeError = "El campo CANTIDAD_REQUERIDA no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "OBSERVACIONES = 'NULL', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ENTREGA_SOLICITADA) + "'";
            informacion += "FECHA_ENTREGA_SOLICITADA = '" + FECHA_ENTREGA_SOLICITADA.ToShortDateString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    ID_SOLICITUD = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_SOLICITUD <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_REG_SOLICITUDES_PLANTA, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
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
                return ID_SOLICITUD;
            }
            else
            {
                return 0;
            }
        }


        public Boolean CancelarSolicitudPlanta(Decimal ID_SOLICITUD,
            String ESTADO)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_reg_solicitudes_planta_cancelar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar cancelar el registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_REG_SOLICITUDES_PLANTA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la cancelación del registro.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
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

        public Boolean ActualizarCantidadObservacionesSolicitudPlanta(Decimal ID_SOLICITUD,
            Int32 CANTIDAD_REQUERIDA,
            String OBSERVACIONES)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_reg_solicitudes_planta_actualizar_cantidad_observaciones ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUD no puede ser vacio.";
                ejecutar = false;
            }

            if (CANTIDAD_REQUERIDA != 0)
            {
                sql += CANTIDAD_REQUERIDA + ", ";
                informacion += "CANTIDAD_REQUERIDA = '" + CANTIDAD_REQUERIDA + "', ";
            }
            else
            {
                MensajeError = "El campo CANTIDAD_REQUERIDA no puede ser vacio.";
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

                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar actualizar el registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_REG_SOLICITUDES_PLANTA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la actualización del registro.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
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

        public Boolean AprobarSolicitudPlanta(Decimal ID_SOLICITUD,
            Int32 CANTIDAD_APROBADA,
            String ESTADO,
            String MOTIVOAUTH,
            Decimal ID_USUARIO_AUTORIZA)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_reg_solicitudes_planta_aprobar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUD no puede ser vacio.";
                ejecutar = false;
            }

            if (CANTIDAD_APROBADA != 0)
            {
                sql += CANTIDAD_APROBADA + ", ";
                informacion += "CANTIDAD_APROBADA = '" + CANTIDAD_APROBADA + "', ";
            }
            else
            {
                MensajeError = "El campo CANTIDAD_APROBADA no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(MOTIVOAUTH) == false)
            {
                sql += "'" + MOTIVOAUTH + "', ";
                informacion += "MOTIVOAUTH = '" + MOTIVOAUTH + "', ";
            }
            else
            {
                MensajeError = "El campo MOTIVOAUTH no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_USUARIO_AUTORIZA != 0)
            {
                sql += ID_USUARIO_AUTORIZA + ", ";
                informacion += "ID_USUARIO_AUTORIZA = '" + ID_USUARIO_AUTORIZA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO_AUTORIZA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar actualizar el registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_REG_SOLICITUDES_PLANTA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la actualización del registro.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
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

        public Boolean NoAprobarSolicitudPlanta(Decimal ID_SOLICITUD,
            String ESTADO,
            String MOTIVO_AUTORIZACION,
            Decimal ID_USUARIO_NOAUTORIZA)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_reg_solicitudes_planta_noaprobar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(MOTIVO_AUTORIZACION) == false)
            {
                sql += "'" + MOTIVO_AUTORIZACION + "', ";
                informacion += "MOTIVO_AUTORIZACION = '" + MOTIVO_AUTORIZACION + "', ";
            }
            else
            {
                MensajeError = "El campo MOTIVO_AUTORIZACION no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_USUARIO_NOAUTORIZA != 0)
            {
                sql += ID_USUARIO_NOAUTORIZA + ", ";
                informacion += "ID_USUARIO_NOAUTORIZA = '" + ID_USUARIO_NOAUTORIZA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO_NOAUTORIZA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar actualizar el registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_REG_SOLICITUDES_PLANTA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la actualización del registro.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
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

        public Boolean PorEntregarSolicitudPlanta(Decimal ID_SOLICITUD,
            Int32 CANTIDAD_APROBADA,
            String ESTADO,
            String MOTIVO_ENTREGA,
            Decimal ID_USUARIO_ENTREGA)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_reg_solicitudes_planta_porentregar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUD no puede ser vacio.";
                ejecutar = false;
            }

            if (CANTIDAD_APROBADA != 0)
            {
                sql += CANTIDAD_APROBADA + ", ";
                informacion += "CANTIDAD_APROBADA = '" + CANTIDAD_APROBADA + "', ";
            }
            else
            {
                MensajeError = "El campo CANTIDAD_APROBADA no puede ser vacio.";
                ejecutar = false;
            }
            
            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(MOTIVO_ENTREGA) == false)
            {
                sql += "'" + MOTIVO_ENTREGA + "', ";
                informacion += "MOTIVO_ENTREGA = '" + MOTIVO_ENTREGA + "', ";
            }
            else
            {
                MensajeError = "El campo MOTIVO_ENTREGA no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_USUARIO_ENTREGA != 0)
            {
                sql += ID_USUARIO_ENTREGA + ", ";
                informacion += "ID_USUARIO_ENTREGA = '" + ID_USUARIO_ENTREGA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO_ENTREGA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar actualizar el registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_REG_SOLICITUDES_PLANTA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la actualización del registro.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
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

        public Boolean NoEntregarSolicitudPlanta(Decimal ID_SOLICITUD,
            String ESTADO,
            String MOTIVO_ENTREGA,
            Decimal ID_USUARIO_NOENTREGA)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_reg_solicitudes_planta_noporentregar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(MOTIVO_ENTREGA) == false)
            {
                sql += "'" + MOTIVO_ENTREGA + "', ";
                informacion += "MOTIVO_ENTREGA = '" + MOTIVO_ENTREGA + "', ";
            }
            else
            {
                MensajeError = "El campo MOTIVO_ENTREGA no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_USUARIO_NOENTREGA != 0)
            {
                sql += ID_USUARIO_NOENTREGA + ", ";
                informacion += "ID_USUARIO_NOENTREGA = '" + ID_USUARIO_NOENTREGA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO_NOENTREGA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar actualizar el registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_REG_SOLICITUDES_PLANTA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la actualización del registro.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
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


        public Boolean FinalizarSolicitudPlanta(Decimal ID_SOLICITUD,
            String ESTADO)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_reg_solicitudes_planta_finalizar ";

            #region validaciones
            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_SOLICITUD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar actualizar el registro.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_REG_SOLICITUDES_PLANTA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la actualización del registro.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
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
        #endregion metodos
    }
}
