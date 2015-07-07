using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Security.Cryptography;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.operaciones;

namespace Brainsbits.LLB.seguridad
{
    public class seguridad
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_EMPRESA_USUARIO = 0;
        private Decimal _id_USuario = 0;
        private String _USU_LOG = null;
        private Decimal _ID_ROL = 0;
        private Decimal _ID_EMPRESA = 0;

        private Decimal _ID_UNIDAD_NEGOCIO = 0;
        private String _UNIDAD_NEGOCIO = null;

        private Dictionary<String, String> diccionarioCamposUnidadNegocio;
        #endregion

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

        public Decimal id_Usuario
        {
            get { return _id_USuario; }
            set { _id_USuario = value; }
        }
        public String USU_LOG
        {
            get { return _USU_LOG; }
            set { _USU_LOG = value; }
        }
        public Decimal ID_ROL
        {
            get { return _ID_ROL; }
            set { _ID_ROL = value; }
        }
        public Decimal ID_EMPRESA
        {
            get { return _ID_EMPRESA; }
            set { _ID_EMPRESA = value; }
        }
        public Decimal ID_EMPRESA_USUARIO
        {
            get { return _ID_EMPRESA_USUARIO; }
            set { _ID_EMPRESA_USUARIO = value; }
        }
        public Decimal ID_UNIDAD_NEGOCIO
        {
            get { return _ID_UNIDAD_NEGOCIO; }
            set { _ID_UNIDAD_NEGOCIO = value; }
        }
        public String UNIDAD_NEGOCIO
        {
            get { return _UNIDAD_NEGOCIO; }
            set { _UNIDAD_NEGOCIO = value; }
        }
        #endregion

        #region constructores
        public seguridad()
        {
            diccionarioCamposUnidadNegocio = new Dictionary<string, string>();

            diccionarioCamposUnidadNegocio.Add("UNIDAD_NEGOCIO", "Unidad de Negocio");
        }
        public seguridad(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region metodosRol

        public DataTable ObtenerRolPorId(Decimal ID_ROL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_rol_obtener_por_id_rol ";

            if (ID_ROL != 0)
            {
                sql += ID_ROL;
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser vacio.";
                ejecutar = false;
            }

            if (ejecutar == true)
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

        private Decimal AdicinarRegistroCRT_ROL(String NOMBRE,
            Boolean GESTIONA_PERMISOS,
            Boolean GESTIONA_USUARIOS,
            Boolean RESTRICCION_EMPRESAS,
            Boolean ACCESO_PLANTA,
            Boolean RECIBE_CORREO_NEW_REQ,
            Boolean SOLO_REQ_USUARIO,
            Conexion conexion)
        {
            Decimal ID_ROL = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_crt_rol_adicionar ";

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE no puede ser 0\n";
                ejecutar = false;
            }

            if (GESTIONA_PERMISOS == true)
            {
                sql += "'true', ";
                informacion += "GESTIONA_PERMISOS = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "GESTIONA_PERMISOS = 'false', ";
            }


            if (GESTIONA_USUARIOS == true)
            {
                sql += "'true', ";
                informacion += "GESTIONA_USUARIOS = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "GESTIONA_USUARIOS = 'false', ";
            }

            if (RESTRICCION_EMPRESAS == true)
            {
                sql += "'true', ";
                informacion += "RESTRICCION_EMPRESAS = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "RESTRICCION_EMPRESAS = 'false', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario + "', ";

            if (ACCESO_PLANTA == true)
            {
                sql += "'true', ";
                informacion += "ACCESO_PLANTA = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "ACCESO_PLANTA = 'false', ";
            }

            if (RECIBE_CORREO_NEW_REQ == true)
            {
                sql += "'true', ";
                informacion += "RECIBE_CORREO_NEW_REQ = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "RECIBE_CORREO_NEW_REQ = 'false', ";
            }

            if (SOLO_REQ_USUARIO == true)
            {
                sql += "'true'";
                informacion += "SOLO_REQ_USUARIO = 'true'";
            }
            else
            {
                sql += "'false'";
                informacion += "SOLO_REQ_USUARIO = 'false'";
            }

            if (ejecutar == true)
            {
                ID_ROL = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                if (ID_ROL <= 0)
                {
                    MensajeError = "Ocurrio un error al intentar insertar el nuevo rol, error INSERT.";
                    ID_ROL = 0;
                }
                else
                {
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(Usuario, tabla.CRT_ROL, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                    {
                        MensajeError = "Ocurrio un error al intentar insertar la auditoria para el nuevo rol; error INSERT.";
                        ID_ROL = 0;
                    }
                    #endregion auditoria
                }
            }
            else
            {
                ID_ROL = 0;
            }

            return ID_ROL;
        }


        public Decimal AdicionarRol(String NOMBRE,
            Boolean GESTIONA_PRMISOS,
            Boolean GESTIONA_USUARIOS,
            Boolean RESTRICCION_EMPRESAS,
            List<Decimal> listaBotonesConAcceso,
            Boolean ACCESO_PLANTA,
            Boolean RECIBE_CORREO_NEW_REQ,
            Boolean SOLO_REQ_USUARIO)
        {
            Decimal ID_ROL = 0;

            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_ROL = AdicinarRegistroCRT_ROL(NOMBRE, GESTIONA_PRMISOS, GESTIONA_USUARIOS, RESTRICCION_EMPRESAS, ACCESO_PLANTA, RECIBE_CORREO_NEW_REQ, SOLO_REQ_USUARIO, conexion);

                if (ID_ROL == 0)
                {
                    conexion.DeshacerTransaccion();
                    verificador = false;
                }
                else
                {
                    Decimal ID_PERMISO_BOTON = 0;
                    foreach (Decimal boton in listaBotonesConAcceso)
                    {
                        ID_PERMISO_BOTON = 0;

                        ID_PERMISO_BOTON = AdicinarRegistroCRT_PERMISOS(ID_ROL, boton, conexion);

                        if (ID_PERMISO_BOTON <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            verificador = false;
                            break;
                        }
                    }

                    if (verificador == true)
                    {
                        conexion.AceptarTransaccion();
                    }
                }
            }
            catch
            {
                conexion.DeshacerTransaccion();
                ID_ROL = 0;
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_ROL;
        }

        private Boolean ActualizarRegistroCRT_ROL(Decimal ID_ROL,
            String NOMBRE,
            Boolean GESTIONA_PERMISOS,
            Boolean GESTIONA_USUARIOS,
            Boolean RESTRICCION_EMPRESAS,
            String ACTIVO,
            Boolean ACCESO_PLANTA,
            Boolean RECIBE_CORREO_NEW_REQ,
            Boolean SOLO_REG_USUARIO,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            tools _tools = new tools();

            Boolean verificador = true;

            sql = "usp_crt_rol_actualizar ";

            #region validaciones
            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = " + ID_ROL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo. \n";
                ejecutar = false;
            }

            if (GESTIONA_PERMISOS == true)
            {
                sql += "'true', ";
                informacion += "GESTIONA_PERMISOS = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "GESTIONA_PERMISOS = 'false', ";
            }

            if (GESTIONA_USUARIOS == true)
            {
                sql += "'true', ";
                informacion += "GESTIONA_USUARIOS = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "GESTIONA_USUARIOS = 'false', ";
            }

            if (RESTRICCION_EMPRESAS == true)
            {
                sql += "'true', ";
                informacion += "RESTRICCION_EMPRESAS = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "RESTRICCION_EMPRESAS = 'false', ";
            }

            if (!(String.IsNullOrEmpty(ACTIVO)))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO = '" + ACTIVO + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario + "', ";

            if (ACCESO_PLANTA == true)
            {
                sql += "'true', ";
                informacion += "ACCESO_PLANTA = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "ACCESO_PLANTA = 'false', ";
            }

            if (RECIBE_CORREO_NEW_REQ == true)
            {
                sql += "'true', ";
                informacion += "RECIBE_CORREO_NEW_REQ = 'true', ";
            }
            else
            {
                sql += "'false', ";
                informacion += "RECIBE_CORREO_NEW_REQ = 'false', ";
            }

            if (SOLO_REG_USUARIO == true)
            {
                sql += "'true'";
                informacion += "SOLO_REG_USUARIO = 'true'";
            }
            else
            {
                sql += "'false'";
                informacion += "SOLO_REG_USUARIO = 'false'";
            }

            #endregion validaciones

            if (ejecutar == true)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        verificador = false;
                        MensajeError = "No se pudo realizar la actualización del rol, error en UPDATE.";
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(Usuario, tabla.CRT_ROL, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false)
                        {
                            verificador = false;
                            MensajeError = _auditoria.MensajError;
                        }
                        #endregion auditoria
                    }
                }
                catch (Exception ex)
                {
                    verificador = false;
                    MensajeError = ex.Message;
                }
            }
            else
            {
                verificador = false;
            }

            return verificador;
        }

        private Boolean DesactivarPermisosBotones(Decimal ID_PERMISO_BOTON,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            tools _tools = new tools();

            Boolean verificador = true;

            sql = "usp_crt_permisos_botones_desactivar ";

            #region validaciones
            if (ID_PERMISO_BOTON != 0)
            {
                sql += ID_PERMISO_BOTON + ", ";
                informacion += "ID_PERMISO_BOTON = '" + ID_PERMISO_BOTON + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PERMISO_BOTON no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar == true)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        verificador = false;
                        MensajeError = "No se pudo realizar la actualización del rol, error en UPDATE.";
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(Usuario, tabla.CRT_PERMISOS_BOTONES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false)
                        {
                            verificador = false;
                            MensajeError = _auditoria.MensajError;
                        }
                        #endregion auditoria
                    }
                }
                catch (Exception ex)
                {
                    verificador = false;
                    MensajeError = ex.Message;
                }
            }
            else
            {
                verificador = false;
            }

            return verificador;
        }


        public Boolean ActualizarRol(Decimal ID_ROL,
            String NOMBRE,
            Boolean GESTINA_PERMISOS,
            Boolean GESTIONA_USUARIOS,
            Boolean RESTRICCION_EMPRESAS,
            String ACTIVO,
            List<Decimal> listaPermisos,
            Boolean ACCESO_PLANTA,
            Boolean RECIBE_CORREO_NEW_REQ,
            Boolean SOLO_REQ_USUARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean verificador = true;
            try
            {
                if (ActualizarRegistroCRT_ROL(ID_ROL, NOMBRE, GESTINA_PERMISOS, GESTIONA_USUARIOS, RESTRICCION_EMPRESAS, ACTIVO, ACCESO_PLANTA, RECIBE_CORREO_NEW_REQ, SOLO_REQ_USUARIO, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    verificador = false;
                }
                else
                {
                    DataTable tablaPermisosActuales = ObtenerPermisosBotonesRolPorId(ID_ROL, conexion);
                    Boolean permisoEncontrado = false;
                    Decimal ID_PERMISO_BOTON = 0;

                    foreach (DataRow fila in tablaPermisosActuales.Rows)
                    {
                        permisoEncontrado = false;

                        ID_PERMISO_BOTON = Convert.ToDecimal(fila["ID_PERMISO_BOTON"]);

                        foreach (Decimal permiso in listaPermisos)
                        {
                            if (permiso == Convert.ToDecimal(fila["ID_BOTON_MODULO"]))
                            {
                                permisoEncontrado = true;
                                break;
                            }
                        }

                        if (permisoEncontrado == false)
                        {
                            if (DesactivarPermisosBotones(ID_PERMISO_BOTON, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                verificador = false;
                                break;
                            }
                        }
                    }

                    if (verificador == true)
                    {
                        foreach (Decimal botonConPermiso in listaPermisos)
                        {
                            permisoEncontrado = false;

                            foreach (DataRow fila in tablaPermisosActuales.Rows)
                            {
                                if (botonConPermiso == Convert.ToDecimal(fila["ID_BOTON_MODULO"]))
                                {
                                    permisoEncontrado = true;
                                    break;
                                }
                            }

                            if (permisoEncontrado == false)
                            {
                                if (AdicinarRegistroCRT_PERMISOS(ID_ROL, botonConPermiso, conexion) <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    verificador = false;
                                    break;
                                }
                            }
                        }

                        if (verificador == true)
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                verificador = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return verificador;
        }



        #endregion metodosRol

        #region metodosAreas

        public DataTable ObtenerAreaTodas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_areas_obtener_todas";

            if (ejecutar == true)
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

        public DataTable ObtenerAreaTodasConContadorPermisos(String USU_LOG)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_areas_obtener_con_info_contador_permisos ";

            if (String.IsNullOrEmpty(USU_LOG) == false)
            {
                sql += "'" + USU_LOG + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo USU_LOG nom puede ser vacio.";
            }

            if (ejecutar == true)
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

        private DataTable ObtenerDatosAreaPorNombreArea(String NOMBRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_areas_obtener_por_nombre ";

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo NOMBRE no puede ser vacio.";
            }

            if (ejecutar == true)
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

        #endregion metodosAreas

        #region metodosModulos

        public DataTable ObtenerDatosModulosPorArea(Decimal ID_AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_modulos_obtener_por_area ";

            if (ID_AREA != 0)
            {
                sql += ID_AREA.ToString();
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_AREA no puede ser vacio.";
            }

            if (ejecutar == true)
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

        #endregion metodosModulos

        #region metodosBotonesModulos

        public DataTable ObtenerBotonesPorModulo(Decimal ID_MODULO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_botones_modulos_obtener_por_modulo ";

            #region validaciones
            if (ID_MODULO != 0)
            {
                sql += ID_MODULO;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_MODULO no puede ser 0";
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

        public DataTable ObtenerPermisosBotones(String NOMBRE_AREA, String FORMULARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_botones_modulos_obtener_por_modulo_con_info_permisos_rol ";

            #region validaciones
            if (String.IsNullOrEmpty(NOMBRE_AREA) == false)
            {
                sql += "'" + NOMBRE_AREA + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo NOMBRE_AREA no puede ser 0";
            }

            if (String.IsNullOrEmpty(FORMULARIO) == false)
            {
                sql += "'" + FORMULARIO.Replace("/UI","") + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo FORMULARIO no puede ser 0";
            }

            sql += "'" + Usuario + "'";

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

        public DataTable ObtenerPermisosBotonesConAyudaDeNombreModulo(String NOMBRE_AREA, String FORMULARIO, String NOMBRE_MODULO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_botones_modulos_obtener_por_modulo_y_nombre_modulo_con_info_permisos_rol ";

            #region validaciones
            if (String.IsNullOrEmpty(NOMBRE_AREA) == false)
            {
                sql += "'" + NOMBRE_AREA + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo NOMBRE_AREA no puede ser 0";
            }

            if (String.IsNullOrEmpty(FORMULARIO) == false)
            {
                sql += "'" + FORMULARIO + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo FORMULARIO no puede ser 0";
            }

            sql += "'" + Usuario + "', ";

            if (String.IsNullOrEmpty(NOMBRE_MODULO) == false)
            {
                sql += "'" + NOMBRE_MODULO + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo NOMBRE_MODULO no puede ser 0";
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


        public DataTable ObtenerPermisosBotonesConSoloNombreDeModulo(String NOMBRE_AREA, String NOMBRE_MODULO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_botones_modulos_obtener_con_solo_nombre_modulo_con_info_permisos_rol ";

            #region validaciones
            if (String.IsNullOrEmpty(NOMBRE_AREA) == false)
            {
                sql += "'" + NOMBRE_AREA + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo NOMBRE_AREA no puede ser 0";
            }

            sql += "'" + Usuario + "', ";

            if (String.IsNullOrEmpty(NOMBRE_MODULO) == false)
            {
                sql += "'" + NOMBRE_MODULO + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo NOMBRE_MODULO no puede ser 0";
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





        #endregion metodosBotonesModulos

        #region metodosPermisosBotones

        public DataTable ObtenerPermisosBotonesRolPorId(Decimal ID_ROL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_permisos_botones_obtener_por_id_rol ";

            if (ID_ROL != 0)
            {
                sql += ID_ROL;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_ROL no puede ser vacio.";
            }

            if (ejecutar == true)
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

        public DataTable ObtenerPermisosBotonesRolPorId(Decimal ID_ROL, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_permisos_botones_obtener_por_id_rol ";

            if (ID_ROL != 0)
            {
                sql += ID_ROL;
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser vacio.";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return _dataTable;
        }

        private Decimal AdicinarRegistroCRT_PERMISOS(Decimal ID_ROL,
            Decimal ID_BOTON_MODULO,
            Conexion conexion)
        {
            Decimal ID_PERMISO_BOTON = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_crt_permisos_botones_adicionar ";

            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_BOTON_MODULO != 0)
            {
                sql += ID_BOTON_MODULO + ", ";
                informacion += "ID_BOTON_MODULO = '" + ID_BOTON_MODULO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_BOTON_MODULO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            if (ejecutar == true)
            {
                ID_PERMISO_BOTON = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                if (ID_PERMISO_BOTON <= 0)
                {
                    MensajeError = "Ocurrio un error al intentar insertar un permiso a botoon, error INSERT.";
                    ID_PERMISO_BOTON = 0;
                }
                else
                {
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(Usuario, tabla.CRT_PERMISOS_BOTONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                    {
                        MensajeError = "Ocurrio un error al intentar insertar la auditoria para el permiso a un boton; error INSERT.";
                        ID_PERMISO_BOTON = 0;
                    }
                    #endregion auditoria
                }
            }
            else
            {
                ID_PERMISO_BOTON = 0;
            }

            return ID_PERMISO_BOTON;
        }


        #endregion metodosPermisosBotones

        #region metodosRestriccionEmpresas

        public DataTable ObtenerRestriccionEmpresasPorUsuLogIdRol(String USU_LOG, Decimal ID_ROL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_empresas_por_usuario_obtener_por_usu_log_id_rol ";

            if (String.IsNullOrEmpty(USU_LOG) == false)
            {
                sql += "'" + USU_LOG + "', ";
            }
            else
            {
                MensajeError = "El campo USU_LOG no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL;
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser vacio.";
            }

            if (ejecutar == true)
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

        public DataTable ObtenerRestriccionEmpresasPorUsuLogIdRol(String USU_LOG, Decimal ID_ROL, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_empresas_por_usuario_obtener_por_usu_log_id_rol ";

            if (String.IsNullOrEmpty(USU_LOG) == false)
            {
                sql += "'" + USU_LOG + "', ";
            }
            else
            {
                MensajeError = "El campo USU_LOG no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL;
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser vacio.";
            }

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return _dataTable;
        }

        private Boolean DesactivarRestriccionEmpresa(String USU_LOG,
            Decimal ID_ROL,
            Decimal ID_EMPRESA,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            tools _tools = new tools();

            Boolean verificador = true;

            sql = "usp_crt_empresas_por_usuario_desactivar ";

            #region validaciones
            if (String.IsNullOrEmpty(USU_LOG) == false)
            {
                sql += "'" + USU_LOG + "', ";
                informacion += "USU_LOG = '" + USU_LOG + "', ";
            }
            else
            {
                MensajeError = "El campo USU_LOG no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar == true)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        verificador = false;
                        MensajeError = "No se pudo realizar la desactivación de la restricción por empresa, error en UPDATE.";
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(Usuario, tabla.CRT_EMPRESAS_POR_USUARIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false)
                        {
                            verificador = false;
                            MensajeError = _auditoria.MensajError;
                        }
                        #endregion auditoria
                    }
                }
                catch (Exception ex)
                {
                    verificador = false;
                    MensajeError = ex.Message;
                }
            }
            else
            {
                verificador = false;
            }

            return verificador;
        }

        private Decimal AdicinarRegistroCRT_EMPRESAS_POR_USUARIO(Decimal ID_USUARIO,
            String USU_LOG,
            Decimal ID_ROL,
            Decimal ID_EMPRESA,
            Conexion conexion)
        {
            Decimal ID_EMPRESA_USUARIO_GENERADO = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_crt_empresas_por_usuario_adicionar ";

            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO + ", ";
                informacion += "ID_USUARIO = '" + ID_USUARIO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(USU_LOG) == false)
            {
                sql += "'" + USU_LOG + "', ";
                informacion += "USU_LOG = '" + USU_LOG + "', ";
            }
            else
            {
                MensajeError = "El campo USU_LOG no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            if (ejecutar == true)
            {
                ID_EMPRESA_USUARIO_GENERADO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                if (ID_EMPRESA_USUARIO_GENERADO <= 0)
                {
                    MensajeError = "Ocurrio un error al intentar insertar una restriccón por empresa para el usuario, error INSERT.";
                    ID_EMPRESA_USUARIO_GENERADO = 0;
                }
                else
                {
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(Usuario, tabla.CRT_EMPRESAS_POR_USUARIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                    {
                        MensajeError = "Ocurrio un error al intentar insertar la auditoria para la restricción de empresa para el usuario; error INSERT.";
                        ID_EMPRESA_USUARIO_GENERADO = 0;
                    }
                    #endregion auditoria
                }
            }
            else
            {
                ID_EMPRESA_USUARIO_GENERADO = 0;
            }

            return ID_EMPRESA_USUARIO_GENERADO;
        }


        public void Adicionar(string idUsuario, string idEmpresa, string unidadNegocio)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            String sql = null;
            int idRol;
            string usuLog;
            string id;

            sql = "select * from CRT_USUARIOS where Id_Usuario = " + idUsuario;

            try
            {
                dataSet = conexion.ExecuteReader(sql);
                dataTable = dataSet.Tables[0];

                if (dataTable.Rows.Count > 0)
                {
                    DataRow dataRow = dataTable.Rows[0];

                    if (!string.IsNullOrEmpty(dataRow["ID_ROL"].ToString()))
                    {
                        idRol = Convert.ToInt32(dataRow["ID_ROL"]);
                        usuLog = dataRow["USU_LOG"].ToString().Trim();

                        sql = "usp_crt_empresas_por_usuario_adicionar ";
                        sql += idUsuario + ", ";
                        sql += "'" + usuLog + "', "; 
                        sql += idRol + ", ";
                        sql += idEmpresa + ", ";
                        sql += "'" + Usuario + "'";

                        id = conexion.ExecuteScalar(sql);

                        sql = "usp_crt_unidad_negocio_adicionar ";
                        sql += id + ", ";
                        sql += "'" + unidadNegocio + "', ";
                        sql += "'" + Usuario + "'";

                        conexion.ExecuteNonQuery(sql);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        public Boolean ActualizarRestriccionesEmpresas(Decimal Id_Usuario,
            String USU_LOG,
            Decimal ID_ROL,
            Boolean RESTRICCION_EMPRESAS,
            List<seguridad> listaRestricciones)
        {
            Decimal ID_EMPRESA_USUARIO_GENERADO = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean verificador = true;
            try
            {
                usuario _usuario = new usuario(Empresa);

                if (_usuario.ActualizarRolUsuario(USU_LOG, ID_ROL, Usuario, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = _usuario.MensajeError;
                    verificador = false;
                }
                else
                {
                    if (RESTRICCION_EMPRESAS == true)
                    {
                        DataTable tablaRestriccionesActuales = ObtenerRestriccionEmpresasPorUsuLogIdRol(USU_LOG, ID_ROL, conexion);
                        Boolean existeEmpresa = false;

                        if (tablaRestriccionesActuales.Rows.Count > 0)
                        {
                            foreach (DataRow fila in tablaRestriccionesActuales.Rows)
                            {
                                existeEmpresa = false;

                                foreach (seguridad empresa in listaRestricciones)
                                {
                                    if (empresa.ID_EMPRESA == Convert.ToDecimal(fila["ID_EMPRESA"]))
                                    {
                                        existeEmpresa = true;
                                        break;
                                    }
                                }

                                if (existeEmpresa == false)
                                {
                                    if (DesactivarRestriccionEmpresa(USU_LOG, ID_ROL, Convert.ToDecimal(fila["ID_EMPRESA"]), conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        break;
                                    }
                                }
                            }
                        }

                        if (verificador == true)
                        {
                            foreach (seguridad empresa in listaRestricciones)
                            {
                                if (empresa.ID_EMPRESA_USUARIO == 0)
                                {
                                    ID_EMPRESA_USUARIO_GENERADO = AdicinarRegistroCRT_EMPRESAS_POR_USUARIO(empresa.id_Usuario, empresa.USU_LOG, empresa.ID_ROL, empresa.ID_EMPRESA, conexion);

                                    if (ID_EMPRESA_USUARIO_GENERADO <= 0)
                                    {
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (verificador == true)
                    {
                        conexion.AceptarTransaccion();
                    }
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                verificador = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return verificador;
        }

        #endregion metodosRestriccionEmpresas

        #region metodosUnidadNegocio

        public DataTable ObtenerCrtUnidadNegocio(Decimal ID_EMPRESA_USUARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_unidad_negocio_obtener_activas_por_empresa_usuario ";

            if (ID_EMPRESA_USUARIO != 0)
            {
                sql += ID_EMPRESA_USUARIO;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA_USUARIO no puede ser vacio.";
                ejecutar = false;
            }

            if (ejecutar == true)
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

        public DataTable ObtenerCrtUnidadNegocio(Decimal ID_EMPRESA_USUARIO,
            Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_unidad_negocio_obtener_activas_por_empresa_usuario ";

            if (ID_EMPRESA_USUARIO != 0)
            {
                sql += ID_EMPRESA_USUARIO;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA_USUARIO no puede ser vacio.";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerUsuariosPorEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_empresas_por_usuario_obtener_activos_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
            }

            if (ejecutar == true)
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

            _dataTable.Columns.Add("UNIDAD_NEGOCIO");

            DataRow filaEmpresaUsuario;
            DataTable tablaUnidad;
            String unidad_negocio;

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                filaEmpresaUsuario = _dataTable.Rows[i];

                unidad_negocio = "";

                tablaUnidad = ObtenerCrtUnidadNegocio(Convert.ToDecimal(filaEmpresaUsuario["ID_EMPRESA_USUARIO"]));

                if (tablaUnidad.Rows.Count > 0)
                {
                    foreach (DataRow fila in tablaUnidad.Rows)
                    {
                        if (unidad_negocio == "")
                        {
                            unidad_negocio = fila["UNIDAD_NEGOCIO"].ToString();
                        }
                        else
                        {
                            unidad_negocio += ", " + fila["UNIDAD_NEGOCIO"].ToString();
                        }
                    }
                }

                if (unidad_negocio == "")
                {
                    filaEmpresaUsuario["UNIDAD_NEGOCIO"] = "Sin Asignación";
                }
                else
                {
                    filaEmpresaUsuario["UNIDAD_NEGOCIO"] = unidad_negocio;
                }
            }

            return _dataTable;
        }


        public DataTable ObtenerUnidadesNegocioDeUnaEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_empresas_por_usuario_obtener_activos_por_id_empresa_con_informacion_unidad_negocio ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
            }

            if (ejecutar == true)
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



            DataTable tablaUnidadNegocio = new DataTable();

            tablaUnidadNegocio.Columns.Add("ID_EMPRESA_USUARIO", typeof(Decimal));
            tablaUnidadNegocio.Columns.Add("ID_USUARIO", typeof(Decimal));
            tablaUnidadNegocio.Columns.Add("USU_LOG", typeof(String));
            tablaUnidadNegocio.Columns.Add("ID_ROL", typeof(Decimal));
            tablaUnidadNegocio.Columns.Add("ID_EMPRESA", typeof(Decimal));
            tablaUnidadNegocio.Columns.Add("ACTIVO");
            tablaUnidadNegocio.Columns.Add("USU_CRE", typeof(String));
            tablaUnidadNegocio.Columns.Add("FCH_CRE", typeof(DateTime));
            tablaUnidadNegocio.Columns.Add("USU_MOD", typeof(String));
            tablaUnidadNegocio.Columns.Add("FCH_MOD", typeof(DateTime));

            tablaUnidadNegocio.Columns.Add("NOMBRES_EMPLEADO", typeof(String));
            tablaUnidadNegocio.Columns.Add("DOCUMENTO_EMPLEADO", typeof(String));
            tablaUnidadNegocio.Columns.Add("USU_TIPO", typeof(String));
            tablaUnidadNegocio.Columns.Add("ID_EMPLEADO", typeof(Decimal));
            tablaUnidadNegocio.Columns.Add("NOMBRE_ROL", typeof(String));
            tablaUnidadNegocio.Columns.Add("USU_MAIL", typeof(String));

            tablaUnidadNegocio.Columns.Add("UNIDAD_NEGOCIO", typeof(String));

            if (_dataTable.Rows.Count <= 0)
            {
                return tablaUnidadNegocio;
            }
            else
            {
                Decimal ID_EMPRESA_USUARIO_ACTUAL = Convert.ToDecimal(_dataTable.Rows[0]["ID_EMPRESA_USUARIO"]);
                String UNIDAD_NEGOCIO_ACTUAL = _dataTable.Rows[0]["UNIDAD_NEGOCIO"].ToString().Trim();

                foreach (DataRow filaOriginal in _dataTable.Rows)
                {
                    Decimal ID_EMPRESA_USUARIO_ORIGINAL = Convert.ToDecimal(filaOriginal["ID_EMPRESA_USUARIO"]);

                    if (ID_EMPRESA_USUARIO_ACTUAL != ID_EMPRESA_USUARIO_ORIGINAL)
                    {
                        DataRow filaNueva = tablaUnidadNegocio.NewRow();

                        filaNueva["ID_EMPRESA_USUARIO"] = ID_EMPRESA_USUARIO_ACTUAL;
                        filaNueva["ID_USUARIO"] = filaOriginal["ID_USUARIO"];
                        filaNueva["USU_LOG"] = filaOriginal["USU_LOG"];
                        filaNueva["ID_ROL"] = filaOriginal["ID_ROL"];
                        filaNueva["ID_EMPRESA"] = filaOriginal["ID_EMPRESA"];
                        filaNueva["ACTIVO"] = filaOriginal["ACTIVO"];
                        filaNueva["USU_CRE"] = filaOriginal["USU_CRE"];
                        filaNueva["FCH_CRE"] = filaOriginal["FCH_CRE"];
                        filaNueva["USU_MOD"] = filaOriginal["USU_MOD"];
                        filaNueva["FCH_MOD"] = filaOriginal["FCH_MOD"];

                        filaNueva["NOMBRES_EMPLEADO"] = filaOriginal["NOMBRES_EMPLEADO"];
                        filaNueva["DOCUMENTO_EMPLEADO"] = filaOriginal["DOCUMENTO_EMPLEADO"];
                        filaNueva["USU_TIPO"] = filaOriginal["USU_TIPO"];
                        filaNueva["ID_EMPLEADO"] = filaOriginal["ID_EMPLEADO"];
                        filaNueva["NOMBRE_ROL"] = filaOriginal["NOMBRE_ROL"];
                        filaNueva["USU_MAIL"] = filaOriginal["USU_MAIL"];

                        filaNueva["UNIDAD_NEGOCIO"] = UNIDAD_NEGOCIO_ACTUAL;

                        tablaUnidadNegocio.Rows.Add(filaNueva);

                        ID_EMPRESA_USUARIO_ACTUAL = ID_EMPRESA_USUARIO_ORIGINAL;
                        UNIDAD_NEGOCIO_ACTUAL = filaOriginal["UNIDAD_NEGOCIO"].ToString();
                    }
                    else
                    {
                        UNIDAD_NEGOCIO_ACTUAL += ", " + filaOriginal["UNIDAD_NEGOCIO"].ToString();
                    }
                }

                return tablaUnidadNegocio;
            }
        }


        public DataTable ObtenerUsuariosPorEmpresa(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_empresas_por_usuario_obtener_activos_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            _dataTable.Columns.Add("UNIDAD_NEGOCIO");

            DataRow filaEmpresaUsuario;
            DataTable tablaUnidad;
            String unidad_negocio;

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                filaEmpresaUsuario = _dataTable.Rows[i];

                unidad_negocio = "";

                tablaUnidad = ObtenerCrtUnidadNegocio(Convert.ToDecimal(filaEmpresaUsuario["ID_EMPRESA_USUARIO"]));

                if (tablaUnidad.Rows.Count > 0)
                {
                    foreach (DataRow fila in tablaUnidad.Rows)
                    {
                        if (unidad_negocio == "")
                        {
                            unidad_negocio = fila["UNIDAD_NEGOCIO"].ToString();
                        }
                        else
                        {
                            unidad_negocio += ", " + fila["UNIDAD_NEGOCIO"].ToString();
                        }
                    }
                }

                if (unidad_negocio == "")
                {
                    filaEmpresaUsuario["UNIDAD_NEGOCIO"] = "Sin Asignación";
                }
                else
                {
                    filaEmpresaUsuario["UNIDAD_NEGOCIO"] = unidad_negocio;
                }
            }

            return _dataTable;
        }



        public Boolean DesactivarUnidadNegocio(Decimal ID_UNIDAD_NEGOCIO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_crt_unidad_negocio_desactivar ";

            if (ID_UNIDAD_NEGOCIO != 0)
            {
                sql += ID_UNIDAD_NEGOCIO + ", ";
                informacion += "ID_UNIDAD_NEGOCIO = " + ID_UNIDAD_NEGOCIO + ", ";
            }
            else
            {
                MensajeError = "El campo ID DE UNIDAD DE NEGOCIO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CRT_UNIDAD_NEGOCIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Decimal AdicionarUnidadNegocio(Decimal ID_EMPRESA_USUARIO
            , String UNIDAD_NEGOCIO
            , Conexion conexion)
        {
            Decimal ID_UNIDAD_NEGOCIO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_crt_unidad_negocio_adicionar ";

            if (ID_EMPRESA_USUARIO != 0)
            {
                sql += ID_EMPRESA_USUARIO + ", ";
                informacion += "ID_EMPRESA_USUARIO = " + ID_EMPRESA_USUARIO + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA_USUARIO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(UNIDAD_NEGOCIO)))
            {
                sql += "'" + UNIDAD_NEGOCIO + "', ";
                informacion += "UNIDAD_NEGOCIO = '" + UNIDAD_NEGOCIO + "', ";
            }
            else
            {
                MensajeError = "El campo UNIDAD_NEGOCIO no puede ser vacio\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    ID_UNIDAD_NEGOCIO = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    if (ID_UNIDAD_NEGOCIO <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar la información de la unidad de negocio.";
                        ID_UNIDAD_NEGOCIO = 0;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(Usuario, tabla.CRT_UNIDAD_NEGOCIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoria de la unidad de negocio.";
                            ID_UNIDAD_NEGOCIO = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_UNIDAD_NEGOCIO = 0;
                }
            }

            return ID_UNIDAD_NEGOCIO;
        }

        public Boolean ActualizarUnidadNegocio(Decimal ID_EMPRESA,
            Decimal ID_EMPRESA_USUARIO,
            List<seguridad> listaUnidadNegocio)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            Boolean continuarNormalmente = true;
            ManualServicio _manual = new ManualServicio(Empresa, Usuario);

            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion); 

                DataTable tablaUnidadNegocioActuales = ObtenerCrtUnidadNegocio(ID_EMPRESA_USUARIO, conexion);

                Boolean verificadorUnidad = false;
                foreach (DataRow infoUnidadActual in tablaUnidadNegocioActuales.Rows)
                {
                    verificadorUnidad = false;
                    foreach (seguridad infoUnidadLista in listaUnidadNegocio)
                    {
                        if (Convert.ToDecimal(infoUnidadActual["ID_UNIDAD_NEGOCIO"]) == infoUnidadLista.ID_UNIDAD_NEGOCIO)
                        {
                            verificadorUnidad = true;
                            break;
                        }
                    }

                    if (verificadorUnidad == false)
                    {
                        if (realizarVersionamientoManual == true)
                        {
                            ID_VERSIONAMIENTO = _manual.RegistrarDesactivacionRegistroTabla(ID_EMPRESA, ManualServicio.ListaSecciones.Comercial, ManualServicio.AccionesManual.Eliminar, "UNIDAD_NEGOCIO", "unidad de Negocio", infoUnidadActual["UNIDAD_NEGOCIO"].ToString().Trim(), ID_VERSIONAMIENTO, conexion);
                            if (ID_VERSIONAMIENTO == -1)
                            {
                                conexion.DeshacerTransaccion();
                                verificador = false;
                                continuarNormalmente = false;
                                ID_VERSIONAMIENTO = 0;
                                break;
                            }
                        }

                        if (continuarNormalmente == true)
                        {
                            if (DesactivarUnidadNegocio(Convert.ToDecimal(infoUnidadActual["ID_UNIDAD_NEGOCIO"]), conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                verificador = false;
                                break;
                            }
                        }
                    }
                }

                if ((verificador == true) && (continuarNormalmente == true))
                {
                    foreach (seguridad infoUnidad in listaUnidadNegocio)
                    {
                        if (infoUnidad.ID_UNIDAD_NEGOCIO == 0)
                        {
                            if (realizarVersionamientoManual == true)
                            {
                                ID_VERSIONAMIENTO = _manual.RegistrarInsersionRegistroTabla(ID_VERSIONAMIENTO, ManualServicio.ListaSecciones.Comercial, "UNIDAD_NEGOCIO", "Unidad de Negocio", infoUnidad.UNIDAD_NEGOCIO, ManualServicio.AccionesManual.Insertar, ID_EMPRESA, conexion);
                                if (ID_VERSIONAMIENTO == -1)
                                {
                                    conexion.DeshacerTransaccion();
                                    verificador = false;
                                    continuarNormalmente = false;
                                    ID_VERSIONAMIENTO = 0;
                                    break;
                                }
                            }

                            if (continuarNormalmente == true)
                            {
                                if (AdicionarUnidadNegocio(infoUnidad.ID_EMPRESA_USUARIO, infoUnidad.UNIDAD_NEGOCIO, conexion) <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    verificador = false;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (verificador == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                verificador = false;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return verificador;
        }
        #endregion metodosUnidadNegocio

        public DataTable UsuarioConRestriccionTopoReq(String usuLog)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_saber_restriccion_tipo_req ";

            sql += "'" + usuLog + "'";

            if (ejecutar == true)
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
