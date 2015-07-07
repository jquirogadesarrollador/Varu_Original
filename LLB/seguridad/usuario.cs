using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Security.Cryptography;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;



namespace Brainsbits.LLB.seguridad
{
    public class usuario
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;
        String _nombre_usuario = null;
        String _tipo_usuario = null;

        private String _apellidos_externo = null;
        private String _nombres_externo = null;
        private String _tip_doc_identidad_externo = null;
        private String _num_doc_identidad_externo = null;
        private String _direccion_externo = null;
        private String _telefono_externo = null;
        private String _celular_externo = null;

        public enum Rol
        {
            AnalistaNomina = 1,
            Psicologo = 2,
            GestorComercial = 3,
            EspecialistaServicios = 10
        }
        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public String NombreUsuario
        {
            get { return _nombre_usuario; }
            set { _nombre_usuario = value; }
        }

        public String TipoUsuario
        {
            get { return _tipo_usuario; }
            set { _tipo_usuario = value; }
        }

        public String ApellidosExterno
        {
            get { return _apellidos_externo; }
            set { _apellidos_externo = value; }
        }

        public String NombresExterno
        {
            get { return _nombres_externo; }
            set { _nombres_externo = value; }
        }

        public String TipDocIdentidadExterno
        {
            get { return _tip_doc_identidad_externo; }
            set { _tip_doc_identidad_externo = value; }
        }

        public String NumDocIdentidadExterno
        {
            get { return _num_doc_identidad_externo; }
            set { _num_doc_identidad_externo = value; }
        }

        public String DireccionExterno
        {
            get { return _direccion_externo; }
            set { _direccion_externo = value; }
        }

        public String TelefonoExterno
        {
            get { return _telefono_externo; }
            set { _telefono_externo = value; }
        }

        public String CelularExterno
        {
            get { return _celular_externo; }
            set { _celular_externo = value; }
        }
        #endregion propiedades

        #region constructores
        public usuario(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        public usuario()
        {

        }
        #endregion constructores

        #region metodos

        private Decimal AdicionarCrtUsuario(String USU_LOG,
            String USU_PSW,
            String Usu_Obs,
            String USU_CRE,
            Decimal ID_EMPLEADO,
            String USU_MAIL,
            String USU_TIPO,
            String nivelAcceso,
            String nivelAccesoRegionales,
            Decimal ID_ROL,
            String ID_CIUDAD_TRABAJADOR,
            String NIVEL_ACCESO_EMPRESAS,
            Conexion conexion)
        {
            Decimal ID_USUARIO = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_adicionar ";

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

            if (!(String.IsNullOrEmpty(USU_PSW)))
            {
                sql += "'" + Encriptar(USU_PSW) + "', ";
                informacion += "USU_PSW = '" + Encriptar(USU_PSW) + "', ";
            }
            else
            {
                MensajeError += "El campo USU_PSW no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usu_Obs)))
            {
                sql += "'" + Usu_Obs + "', ";
                informacion += "Usu_Obs = '" + Usu_Obs + "', ";
            }
            else
            {
                MensajeError += "El campo Usu_Obs no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + USU_CRE + "',";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_MAIL)))
            {
                sql += "'" + USU_MAIL + "', ";
                informacion += "USU_MAIL = '" + USU_MAIL + "', ";
            }
            else
            {
                MensajeError += "El campo USU_MAIL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_TIPO)))
            {
                sql += "'" + USU_TIPO + "', ";
                informacion += "USU_TIPO = '" + USU_TIPO + "', ";
            }
            else
            {
                MensajeError += "El campo USU_TIPO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(nivelAcceso)))
            {
                sql += "'" + nivelAcceso + "', ";
                informacion += "NIVEL_ACCESO = '" + nivelAcceso + "', ";
            }
            else
            {
                MensajeError += "El campo NIVEL_ACCESO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(nivelAccesoRegionales)))
            {
                sql += "'" + nivelAccesoRegionales + "', ";
                informacion += "NIVEL_ACCESO_REGIONALES = '" + nivelAccesoRegionales + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NIVEL_ACCESO_REGIONALES = 'NULL', ";
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ROL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD_TRABAJADOR) == false)
            {
                sql += "'" + ID_CIUDAD_TRABAJADOR + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD_TRABAJADOR + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NIVEL_ACCESO_EMPRESAS) == false)
            {
                sql += "'" + NIVEL_ACCESO_EMPRESAS + "'";
                informacion += "NIVEL_ACCESO_EMPRESAS = '" + NIVEL_ACCESO_EMPRESAS + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "NIVEL_ACCESO_EMPRESAS = 'NULL'";
            }

            if (ejecutar == true)
            {
                ID_USUARIO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                if (ID_USUARIO <= 0)
                {
                    MensajeError = "Ocurrio un error al intentar insertar el nuevo usuario, error INSERT.";
                    ID_USUARIO = 0;
                }
                else
                {
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(USU_CRE, tabla.CRT_USUARIOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                    {
                        MensajeError = "Ocurrio un error al intentar insertar la auditoria para el nuevo usuario; error INSERT.";
                        ID_USUARIO = 0;
                    }
                    #endregion auditoria
                }
            }
            else
            {
                ID_USUARIO = 0;
            }

            return ID_USUARIO;
        }

        public Decimal AdicionarCrtRegUsuariosNoPlanta(String APELLIDOS,
            String NOMBRES,
            String TIP_DOC_IDENTIDAD,
            String NUM_DOC_IDENTIDAD,
            String DIRECCION,
            String TELEFONO,
            String CELULAR,
            String USU_CRE,
            Conexion conexion)
        {
            Decimal ID_EMPLEADO = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_crt_reg_usuarios_no_planta_adicionar ";

            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "', ";
                informacion += "APELLIDOS = '" + APELLIDOS + "', ";
            }
            else
            {
                MensajeError = "El campo APELLIDOS no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRES)))
            {
                sql += "'" + NOMBRES + "', ";
                informacion += "NOMBRES = '" + NOMBRES + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIP_DOC_IDENTIDAD)))
            {
                sql += "'" + TIP_DOC_IDENTIDAD + "', ";
                informacion += "TIP_DOC_IDENTIDAD = '" + TIP_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo TIP_DOC_IDENTIDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "', ";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIRECCION)))
            {
                sql += "'" + DIRECCION + "', ";
                informacion += "DIRECCION = '" + DIRECCION + "', ";
            }
            else
            {
                MensajeError += "El campo DIRECCION no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TELEFONO)))
            {
                sql += "'" + TELEFONO + "', ";
                informacion += "TELEFONO = '" + TELEFONO + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CELULAR)))
            {
                sql += "'" + CELULAR + "', ";
                informacion += "CELULAR = '" + CELULAR + "', ";
            }
            else
            {
                MensajeError += "El campo CELULAR no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + USU_CRE + "'";

            if (ejecutar == true)
            {
                ID_EMPLEADO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                if (ID_EMPLEADO <= 0)
                {
                    MensajeError = "Ocurrio un error al intentar insertar los datos del usuario externo, error INSERT.";
                    ID_EMPLEADO = 0;
                }
                else
                {
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(USU_CRE, tabla.CRT_REG_USUARIOS_NO_PLANTA, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                    {
                        MensajeError = "Ocurrio un error al intentar insertar la auditoria para el nuevo usuario externo; error INSERT.";
                        ID_EMPLEADO = 0;
                    }
                    #endregion auditoria
                }
            }
            else
            {
                ID_EMPLEADO = 0;
            }

            return ID_EMPLEADO;
        }


        public Boolean InactivarRelacionesEmpresaCiudadesActualesDeUnUsuario(Decimal ID_USUARIO,
            String USU_MOD,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            sql = "usp_crt_usuarios_empresas_ciudades_inactivar_todas_de_id_usuario ";

            #region validaciones
            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_USUARIO no puede ser nulo.\n";
                ejecutar = false;
            }

            sql += "'" + USU_MOD + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                    numRegistrosAfectados = 0;
                }
            }

            return true;
        }

        public Boolean AsignarRelacionEmpresaCiudadAUnUsuario(Decimal ID_USUARIO,
            Decimal ID_EMPRESA,
            String ID_CIUDAD,
            String USU_CRE,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            sql = "usp_crt_usuarios_empresas_ciudades_actualizar_o_adicionar ";

            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_USUARIO no puede ser nulo.\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo.\n";
                ejecutar = false;

            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo.\n";
                ejecutar = false;
            }

            sql += "'" + USU_CRE + "'";

            #region validaciones

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                    numRegistrosAfectados = 0;
                }
            }

            if (numRegistrosAfectados == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Decimal AdicionarUsuarioSistema(String USU_LOG,
            String USU_PSW,
            String Usu_Obs,
            String USU_CRE,
            Decimal ID_EMPLEADO,
            String USU_MAIL,
            String USU_TIPO,
            Boolean usuExternoCreado,
            usuario usuarioExterno,
            String nivelAcceso,
            String nivelAccesoRegionales,
            List<UsuarioEmpresaCiudad> listaUsuarioEmpresasCiudades,
            Decimal ID_ROL,
            String ID_CIUDAD_TRABAJADOR,
            String NIVEL_ACCESO_EMPRESAS)
        {
            Decimal ID_USUARIO = 0;

            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (USU_TIPO == "PLANTA")
                {
                    ID_USUARIO = AdicionarCrtUsuario(USU_LOG, USU_PSW, Usu_Obs, USU_CRE, ID_EMPLEADO, USU_MAIL, USU_TIPO, nivelAcceso, nivelAccesoRegionales, ID_ROL, ID_CIUDAD_TRABAJADOR, NIVEL_ACCESO_EMPRESAS, conexion);

                    if (ID_USUARIO <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        ID_USUARIO = 0;
                        correcto = false;
                    }
                }
                else
                {
                    if (usuExternoCreado == true)
                    {
                        ID_USUARIO = AdicionarCrtUsuario(USU_LOG, USU_PSW, Usu_Obs, USU_CRE, ID_EMPLEADO, USU_MAIL, USU_TIPO, nivelAcceso, nivelAccesoRegionales, ID_ROL, ID_CIUDAD_TRABAJADOR, NIVEL_ACCESO_EMPRESAS, conexion);

                        if (ID_USUARIO <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            ID_USUARIO = 0;
                            correcto = false;
                        }
                    }
                    else
                    {
                        ID_EMPLEADO = AdicionarCrtRegUsuariosNoPlanta(usuarioExterno.ApellidosExterno, usuarioExterno.NombresExterno, usuarioExterno.TipDocIdentidadExterno, usuarioExterno.NumDocIdentidadExterno, usuarioExterno.DireccionExterno, usuarioExterno.TelefonoExterno, usuarioExterno.CelularExterno, USU_CRE, conexion);

                        if (ID_EMPLEADO > 0)
                        {
                            ID_USUARIO = AdicionarCrtUsuario(USU_LOG, USU_PSW, Usu_Obs, USU_CRE, ID_EMPLEADO, USU_MAIL, USU_TIPO, nivelAcceso, nivelAccesoRegionales, ID_ROL, ID_CIUDAD_TRABAJADOR, NIVEL_ACCESO_EMPRESAS, conexion);

                            if (ID_USUARIO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                ID_USUARIO = 0;
                                correcto = false;
                            }
                        }
                        else
                        {
                            conexion.DeshacerTransaccion();
                            ID_USUARIO = 0;
                            correcto = false;
                        }
                    }
                }

                if (correcto == true)
                {
                    if (InactivarRelacionesEmpresaCiudadesActualesDeUnUsuario(ID_USUARIO, USU_CRE, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        ID_USUARIO = 0;
                        correcto = false;
                    }
                    else
                    {
                        foreach (UsuarioEmpresaCiudad uec in listaUsuarioEmpresasCiudades)
                        {
                            if (AsignarRelacionEmpresaCiudadAUnUsuario(ID_USUARIO, uec.ID_EMPRESA, uec.ID_CIUDAD, USU_CRE, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                ID_USUARIO = 0;
                                correcto = false;
                                break;
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch
            {
                conexion.DeshacerTransaccion();
                ID_USUARIO = 0;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_USUARIO;
        }

        private Boolean ActualizarCrtUsuario(Decimal Id_Usuario,
            String USU_LOG,
            String Usu_Obs,
            String USU_MOD,
            Decimal ID_EMPLEADO,
            String USU_MAIL,
            String USU_TIPO,
            String ESTADO,
            String USU_NIVEL_ACCESO,
            String USU_NIVEL_ACCESO_REGIONALES,
            Decimal ID_ROL,
            String ID_CIUDAD_TRABAJADOR,
            String USU_NIVEL_ACCESO_EMPRESAS,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            tools _tools = new tools();

            Boolean verificador = true;

            sql = "usp_crt_usuarios_actualizar ";

            #region validaciones
            if (Id_Usuario != 0)
            {
                sql += Id_Usuario + ", ";
                informacion += "Id_Usuario = " + Id_Usuario.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID USUARIO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_LOG)))
            {
                sql += "'" + USU_LOG + "', ";
                informacion += "USU_LOG = '" + USU_LOG + "', ";
            }
            else
            {
                MensajeError += "El campo USU_LOG no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usu_Obs)))
            {
                sql += "'" + Usu_Obs + "', ";
                informacion += "Usu_Obs = '" + Usu_Obs + "', ";
            }
            else
            {
                MensajeError += "El campo Usu_Obs no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_MOD)))
            {
                sql += "'" + USU_MOD + "', ";
                informacion += "USU_MOD = '" + USU_MOD + "', ";
            }
            else
            {
                MensajeError += "El campo USU_MOD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = " + ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPLEADO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_MAIL)))
            {
                sql += "'" + USU_MAIL + "', ";
                informacion += "USU_MAIL = '" + USU_MAIL + "', ";
            }
            else
            {
                MensajeError += "El campo USU_MAIL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_TIPO)))
            {
                sql += "'" + USU_TIPO + "', ";
                informacion += "USU_TIPO = '" + USU_TIPO + "', ";
            }
            else
            {
                MensajeError += "El campo USU_TIPO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ROL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_NIVEL_ACCESO)))
            {
                sql += "'" + USU_NIVEL_ACCESO + "', ";
                informacion += "USU_NIVEL_ACCESO = '" + USU_NIVEL_ACCESO + "', ";
            }
            else
            {
                MensajeError += "El campo USU_NIVEL_ACCESO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_NIVEL_ACCESO_REGIONALES)))
            {
                sql += "'" + USU_NIVEL_ACCESO_REGIONALES + "', ";
                informacion += "USU_NIVEL_ACCESO_REGIONALES = '" + USU_NIVEL_ACCESO_REGIONALES + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "USU_NIVEL_ACCESO_REGIONALES = 'NULL', ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD_TRABAJADOR) == false)
            {
                sql += "'" + ID_CIUDAD_TRABAJADOR + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD_TRABAJADOR + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(USU_NIVEL_ACCESO_EMPRESAS) == false)
            {
                sql += "'" + USU_NIVEL_ACCESO_EMPRESAS + "'";
                informacion += "USU_NIVEL_ACCESO_EMPRESAS = '" + USU_NIVEL_ACCESO_EMPRESAS + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "USU_NIVEL_ACCESO_EMPRESAS = 'NULL'";
            }

            #endregion validaciones

            if (ejecutar == true)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        verificador = false;
                        MensajeError = "No se pudo realizar la actualización del usuario del sistema, error en UPDATE.";
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(USU_MOD, tabla.CRT_USUARIOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false)
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






        public Boolean ActualizarRolUsuario(String USU_LOG,
            Decimal ID_ROL,
            String USU_MOD,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            tools _tools = new tools();

            Boolean verificador = true;

            sql = "usp_crt_usuarios_actualizar_rol ";

            #region validaciones
            if (!(String.IsNullOrEmpty(USU_LOG)))
            {
                sql += "'" + USU_LOG + "', ";
                informacion += "USU_LOG = '" + USU_LOG + "', ";
            }
            else
            {
                MensajeError += "El campo USU_LOG no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID ID_ROL no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + USU_MOD + "'";
            #endregion validaciones

            if (ejecutar == true)
            {
                try
                {
                    if (Convert.ToInt32(conexion.ExecuteScalar(sql)) <= 0)
                    {
                        verificador = false;
                        MensajeError = "No se pudo realizar la actualización del rol, error en UPDATE.";
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(USU_MOD, tabla.CRT_USUARIOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false)
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


        public Boolean ActualizarUsuario(Decimal Id_Usuario,
            String USU_LOG,
            String Usu_Obs,
            String USU_MOD,
            Decimal ID_EMPLEADO,
            String USU_MAIL,
            String USU_TIPO,
            String USU_PSW,
            Boolean usuExternoCreado,
            usuario usuarioExterno,
            String ESTADO,
            String USU_NIVEL_ACCESO,
            String USU_NIVEL_ACCESO_REGIONALES,
            Decimal ID_ROL,
            List<UsuarioEmpresaCiudad> listaUsuarioEmpresasCiudades,
            String ID_CIUDAD_TRABAJADOR,
            String USU_NIVEL_ACCESO_EMPRESAS)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean correcto = true;
            try
            {
                if (USU_TIPO == "PLANTA")
                {
                    if (ActualizarCrtUsuario(Id_Usuario, USU_LOG, Usu_Obs, USU_MOD, ID_EMPLEADO, USU_MAIL, USU_TIPO, ESTADO, USU_NIVEL_ACCESO, USU_NIVEL_ACCESO_REGIONALES, ID_ROL, ID_CIUDAD_TRABAJADOR, USU_NIVEL_ACCESO_EMPRESAS, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                    }
                }
                else
                {
                    if (usuExternoCreado == true)
                    {
                        if (ActualizarCrtUsuario(Id_Usuario, USU_LOG, Usu_Obs, USU_MOD, ID_EMPLEADO, USU_MAIL, USU_TIPO, ESTADO, USU_NIVEL_ACCESO, USU_NIVEL_ACCESO_REGIONALES, ID_ROL, ID_CIUDAD_TRABAJADOR, USU_NIVEL_ACCESO_EMPRESAS, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                        }
                    }
                    else
                    {
                        ID_EMPLEADO = AdicionarCrtRegUsuariosNoPlanta(usuarioExterno.ApellidosExterno, usuarioExterno.NombresExterno, usuarioExterno.TipDocIdentidadExterno, usuarioExterno.NumDocIdentidadExterno, usuarioExterno.DireccionExterno, usuarioExterno.TelefonoExterno, usuarioExterno.CelularExterno, USU_MOD, conexion);

                        if (ID_EMPLEADO > 0)
                        {
                            if (ActualizarCrtUsuario(Id_Usuario, USU_LOG, Usu_Obs, USU_MOD, ID_EMPLEADO, USU_MAIL, USU_TIPO, ESTADO, USU_NIVEL_ACCESO, USU_NIVEL_ACCESO_REGIONALES, ID_ROL, ID_CIUDAD_TRABAJADOR, USU_NIVEL_ACCESO_EMPRESAS, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                            }
                        }
                        else
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                        }
                    }
                }

                if (correcto == true)
                {
                    if (String.IsNullOrEmpty(USU_PSW) == false)
                    {
                        if (ActualizarClaveUsuarioDesdeAdministracion(USU_LOG, USU_PSW, USU_MOD, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                        }
                    }
                }

                if (correcto == true)
                {
                    if (InactivarRelacionesEmpresaCiudadesActualesDeUnUsuario(Id_Usuario, USU_MOD, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                    }
                    else
                    {
                        foreach (UsuarioEmpresaCiudad uec in listaUsuarioEmpresasCiudades)
                        {
                            if (AsignarRelacionEmpresaCiudadAUnUsuario(Id_Usuario, uec.ID_EMPRESA, uec.ID_CIUDAD, USU_MOD, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                break;
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
                correcto = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }


        public DataTable ObtenerEmpleadosPorIdRol(Rol idRol)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_obtenerEmpleadosPorIdRol ";

            if (!(String.IsNullOrEmpty(idRol.ToString()))) sql += Convert.ToInt32(idRol).ToString();
            else
            {
                MensajeError += "El campo idRol no puede vacio. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerListaRoles()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_rol_obtener_todos ";

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


        public DataTable ObtenerListaRolesSoloActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_rol_obtener_todos_los_activos";

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

        public DataTable ObtenerListaRolesSoloActivos(String usuLog)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_rol_obtener_todos_los_activos_segun_permios_usu_log ";
            sql += "'" + usuLog.Trim() + "'";

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


        public DataTable ObtenerRolPorNombre(String NOMBRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_rol_obtener_por_nombre ";

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "'";
            }
            else
            {
                ejecutar = false;
            }

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

        public DataTable ObtenerListausuariosSistema()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_view_informacion_usuarios_sistema_obtener_todos";

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


        public DataTable ObtenerListausuariosSistemaSegunPermisosUsuLog(String usuLog)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_view_informacion_usuarios_sistema_obtener_todos_segun_permisos_usu_log ";
            sql += "'" + usuLog.Trim() + "'";

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


        public DataTable ObtenerListaUsuariosSistemaSinRol()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_view_informacion_usuarios_sistema_obtener_sin_rol";

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

        public DataTable ObtenerListaUsuariosSistemaActivosPorRol(Decimal ID_ROL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_view_informacion_usuarios_sistema_obtener_activos_por_rol ";

            if (ID_ROL != 0)
            {
                sql += ID_ROL;
            }
            else
            {
                MensajeError += "El campo ID_ROL no puede vacio. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerListaUsuariosSistemaActivosPorNombreRol(String NOMBRE_ROL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_view_informacion_usuarios_sistema_obtener_activos_por_nombreRol ";

            if (String.IsNullOrEmpty(NOMBRE_ROL) == false)
            {
                sql += "'" + NOMBRE_ROL + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRE_ROL no puede vacio. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerEmailyNombresEmpleadosPorIdRol(Decimal idRol)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_obtener_nombres_y_correo_PorIdRol ";

            if (idRol != 0)
            {
                sql += idRol;
            }
            else
            {
                MensajeError += "El campo idRol no puede vacio. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerEmpleadoPorIdEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerEmpleadoPorIdEmpleado ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO.ToString();
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede vacio. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerTodosLosEmpleadosActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_crt_usuarios_obtenerTodosEmpleadosActivos ";

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
            return _dataTable;
        }

        public DataTable ObtenerEmpleadosRestriccionEmpresas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_crt_usuarios_obtenerEmpleadosRestriccionEmpresas ";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataTable = _dataSet.Tables[0];

            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }
            return _dataTable;
        }


        public Boolean IniciarSesion(String usuario, String clave, String LOGON_USER, String REMOTE_USER, String LOCAL_ADDR, String REMOTE_ADDR, String REMOTE_HOST, String HTTP_USER_AGENT)
        {
            MensajeError = null;

            Boolean correcto = true;
            Boolean resultado = true;

            String sql = String.Empty;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                DataTable tablaInfoUsuario = ObtenerUsuarioPorUsuLog(usuario, conexion);

                if (tablaInfoUsuario.Rows.Count <= 0)
                {
                    resultado = false;
                    MensajeError = "1:El Usuario no se encuentra registrado en el sistema. Consulte al administrador.";
                }
                else
                {
                    DataRow filaInfoUsuario = tablaInfoUsuario.Rows[0];

                    if (filaInfoUsuario["USU_PSW"].ToString() != Encriptar(clave))
                    {
                        sql = "usp_crt_usuarios_actualizarContador ";
                        sql += "'" + usuario + "'";
                        if (conexion.ExecuteNonQuery(sql) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            resultado = false;
                            MensajeError = "3:No se pudo actualizar el contador de Bloqueo. Consulte al administrador del Sistema.";
                        }
                        else
                        {
                            resultado = false;
                            MensajeError = "2:El Nombre de Usuario ó Password son incorrectos. (Al tercer intento de ingreso fallido el usuario será BLOQUEADO).";
                        }
                    }
                    else
                    {
                        if ((filaInfoUsuario["USU_CAMBIO_PASSWORD"] == DBNull.Value) || (filaInfoUsuario["USU_CAMBIO_PASSWORD"].ToString().ToUpper() == "TRUE"))
                        {
                            resultado = false;
                            MensajeError = "8:El Usuario necesita cambio de password.";
                        }
                        else
                        {
                            if (filaInfoUsuario["USU_FCH_RENOVACION_PSW"] == DBNull.Value)
                            {
                                resultado = false;
                                MensajeError = "8:El Usuario necesita cambio de password.";
                            }
                            else
                            {
                                if (Convert.ToDateTime(filaInfoUsuario["USU_FCH_RENOVACION_PSW"]) <= Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                                {
                                    resultado = false;
                                    MensajeError = "8:El Usuario necesita cambio de password.";
                                }
                            }
                        }

                        if (correcto == true && resultado == true)
                        {
                            if (filaInfoUsuario["ESTADO_USUARIO"].ToString().Trim().ToUpper() == "ACTIVO")
                            {
                                sql = "usp_crt_usuarios_borrarContador ";
                                sql += "'" + usuario + "' ";
                                if (conexion.ExecuteNonQuery(sql) <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    resultado = false;
                                    MensajeError = "4:Error al reiniciar el contador de bloqueo, Consulte al administrador del sistema.";
                                }

                                if (resultado == true)
                                {
                                    if (filaInfoUsuario["ID_ROL"] == DBNull.Value)
                                    {
                                        resultado = false;
                                        MensajeError = "5:El Usuario no tiene asignado un ROL. Consulte al administrador del sistema.";
                                    }


                                    if (filaInfoUsuario["ID_EMPLEADO"] == DBNull.Value)
                                    {
                                        resultado = false;

                                        if (String.IsNullOrEmpty(MensajeError) == false)
                                        {
                                            MensajeError += "<br>El Usuario no tiene asignado un EMPLEADO. Consulte al administrador del sistema.";
                                        }
                                        else
                                        {
                                            MensajeError = "6:El Usuario no tiene asignado un EMPLEADO. Consulte al administrador del sistema.";
                                        }
                                    }
                                    else
                                    {
                                        if (filaInfoUsuario["USU_TIPO"].ToString() == "PLANTA")
                                        {
                                            NombreUsuario = filaInfoUsuario["NOMBRES"].ToString().Trim() + " " + filaInfoUsuario["APELLIDOS"].ToString().Trim();

                                            if (filaInfoUsuario["ACTIVO_EMPLEADO"].ToString() != "S")
                                            {
                                                resultado = false;

                                                if (String.IsNullOrEmpty(MensajeError) == false)
                                                {
                                                    MensajeError += "<br>El Usuario no tiene asignado un EMPLEADO ACTIVO. Consulte al administrador del sistema.";
                                                }
                                                else
                                                {
                                                    MensajeError = "7:El Usuario no tiene asignado un EMPLEADO ACTIVO. Consulte al administrador del sistema.";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            NombreUsuario = filaInfoUsuario["NOMBRES_EXTERNO"].ToString().Trim() + " " + filaInfoUsuario["APELLIDOS_EXTERNO"].ToString().Trim();
                                        }

                                        TipoUsuario = filaInfoUsuario["USU_TIPO"].ToString().Trim();
                                    }
                                }
                            }
                            else
                            {
                                if (filaInfoUsuario["ESTADO_USUARIO"].ToString() == "BLOQUEADO")
                                {
                                    resultado = false;
                                    MensajeError = "9:Usuario BLOQUEADO. Consulte al administrador del sistema.";
                                }
                                else
                                {
                                    if (filaInfoUsuario["ESTADO_USUARIO"].ToString() == "INACTIVO")
                                    {
                                        resultado = false;
                                        MensajeError = "10:Usuario INACTIVO. Consulte al administrador del sistema.";
                                    }
                                    else
                                    {
                                        resultado = false;
                                        MensajeError = "11:Usuario sin ESTADO. Consulte al administrador del sistema.";
                                    }
                                }
                            }
                        }
                    }
                }

                if ((correcto == true) && (resultado == true))
                {
                    sql = "inicio de sesion: Usuario: '" + usuario + "', Clave: '" + Encriptar(clave) + "'";
                    String informacion = "LOGON_USER: " + LOGON_USER + ", RTEMOTE_USER: " + REMOTE_USER + ", LOCAL_ADDR: " + LOCAL_ADDR + ", REMOTE_ADDR: " + REMOTE_ADDR + ", REMOTE_HOST: " + REMOTE_HOST + ", HTTP_USER_AGENT: " + HTTP_USER_AGENT;
                    #region auditoria_cliente
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(usuario, tabla.AUDITORIA, tabla.ACCION_INICIO_SESION, sql, informacion, conexion);
                    #endregion auditoria_cliente
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                correcto = false;
                MensajeError = "-1:" + ex.Message;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return resultado;
        }

        private String ObtenerEstado(String usuario)
        {
            String sql = null;
            String estado = null;

            sql = "usp_crt_usuarios_obtenerEstado '" + usuario + "'";

            Conexion conexion = new Conexion(Empresa);
            try
            {
                estado = conexion.ExecuteScalar(sql);
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }
            return estado;
        }

        public DataTable ObtenerInicioSesionPorUsuLog(String USU_LOG)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_obtener_datos_inicio_sesion ";

            if (!(String.IsNullOrEmpty(USU_LOG)))
                sql += "'" + USU_LOG + "'";
            else
            {
                MensajeError += "El campo USU_LOG no puede ser vacio. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerUsuarioPorUsuLog(String USU_LOG)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_buscarPorUsuLog ";

            if (!(String.IsNullOrEmpty(USU_LOG)))
                sql += "'" + USU_LOG + "'";
            else
            {
                MensajeError += "El campo USU_LOG no puede ser vacio. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerUsuarioPorUsuLog_BuscarCoincidencias(String USU_LOG_BUSCADO, String USU_LOG_BUSCA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_buscarPorUsuLog_buscar ";

            if (!(String.IsNullOrEmpty(USU_LOG_BUSCADO)))
                sql += "'" + USU_LOG_BUSCADO + "', ";
            else
            {
                MensajeError += "El campo USU_LOG_BUSCADO no puede ser vacio.\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_LOG_BUSCA)))
                sql += "'" + USU_LOG_BUSCA + "'";
            else
            {
                MensajeError += "El campo USU_LOG_BUSCA no puede ser vacio.\n";
                ejecutar = false;
            }

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

        public DataTable ObtenerUsuarioPorUsuLog(String USU_LOG, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_buscarPorUsuLog ";

            if (!(String.IsNullOrEmpty(USU_LOG)))
                sql += "'" + USU_LOG + "'";
            else
            {
                MensajeError += "El campo USU_LOG no puede ser vacio. \n";
                ejecutar = false;
            }

            if (ejecutar)
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

        public DataTable ObtenerUsuarioNoPLantaPorNumDocIdentidad(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_reg_usuarios_no_planta_obtener_por_documento_identidad ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser vacio. \n";
                ejecutar = false;
            }

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


        public DataTable ObtenerUsuarioPorNombreEmpleado(String NOMBRE_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_obtenerUsuarioPorNombreEmpleado ";

            if (!(String.IsNullOrEmpty(NOMBRE_EMPLEADO)))
                sql += "'" + NOMBRE_EMPLEADO + "'";
            else
            {
                MensajeError += "El campo NOMBRE_EMPLEADO no puede ser vacio. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerUsuarioPorNombreRol(String NOMBRE_ROL, String USU_LOG_BUSCA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_obtenerUsuarioPorNombreRol ";

            if (!(String.IsNullOrEmpty(NOMBRE_ROL)))
                sql += "'" + NOMBRE_ROL + "', ";
            else
            {
                MensajeError += "El campo NOMBRE_ROL no puede ser vacio. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_LOG_BUSCA)))
                sql += "'" + USU_LOG_BUSCA + "'";
            else
            {
                MensajeError += "El campo USU_LOG_BUSCA no puede ser vacio. \n";
                ejecutar = false;
            }

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

        public Boolean ObtenerClaveUsuarioPorUsuLogCedula(String USU_LOG, String DOC_IDENTIDAD)
        {

            String clave = null;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            MensajeError = null;

            Boolean correcto = true;

            String NOMBRE_EMPLEADO = null;
            String NUM_DOC_IDENTIDAD_EMPLEADO = null;
            String mensaje = null;

            try
            {
                DataTable tablaInfoUsuario = ObtenerUsuarioPorUsuLog(USU_LOG, conexion);

                if (tablaInfoUsuario.Rows.Count <= 0)
                {
                    if (MensajeError == null)
                    {
                        MensajeError = "El Nombre de usuario digitado no se encuentra registrado en la base de datos.";
                    }

                    correcto = false;

                    conexion.DeshacerTransaccion();
                }
                else
                {
                    DataRow filaInfoUsuario = tablaInfoUsuario.Rows[0];

                    if (filaInfoUsuario["ESTADO_USUARIO"].ToString() == "INACTIVO")
                    {
                        MensajeError = "El Usuario digitado esta en estado INCATIVO, Consulte al Administrador del Sistema.";
                        correcto = false;
                        conexion.DeshacerTransaccion();
                    }
                    else
                    {
                        if ((filaInfoUsuario["ID_ROL"] == DBNull.Value) || (filaInfoUsuario["ACTIVO_ROL"] == DBNull.Value) || (filaInfoUsuario["ACTIVO_ROL"].ToString().Trim() == "False"))
                        {
                            MensajeError = "El Usuario digitado no tiene asignado un ROL, o el ROL esta inactivo, Consulte al Administrador del Sistema.";
                            correcto = false;
                            conexion.DeshacerTransaccion();
                        }
                        else
                        {
                            if (filaInfoUsuario["USU_TIPO"].ToString().Trim() == "PLANTA")
                            {
                                if ((filaInfoUsuario["ACTIVO_EMPLEADO"].ToString() == "N") || (filaInfoUsuario["ID_EMPLEADO"] == DBNull.Value))
                                {
                                    MensajeError = "El Usuario digitado no tiene asignado un EMPLEADO ó el EMPLEADO está INACTIVO, Consulte al Administrador del Sistema.";
                                    correcto = false;
                                    conexion.DeshacerTransaccion();
                                }
                                else
                                {
                                    NOMBRE_EMPLEADO = filaInfoUsuario["NOMBRES"].ToString().Trim() + " " + filaInfoUsuario["APELLIDOS"].ToString().Trim();
                                    NUM_DOC_IDENTIDAD_EMPLEADO = filaInfoUsuario["NUM_DOC_IDENTIDAD"].ToString().Trim();
                                }
                            }
                            else
                            {
                                if (filaInfoUsuario["ID_EMPLEADO"] == DBNull.Value)
                                {
                                    MensajeError = "El Usuario digitado no tiene asignado un EMPLEADO, Consulte al Administrador del Sistema.";
                                    correcto = false;
                                    conexion.DeshacerTransaccion();
                                }
                                else
                                {
                                    NOMBRE_EMPLEADO = filaInfoUsuario["NOMBRES_EXTERNO"].ToString().Trim() + " " + filaInfoUsuario["APELLIDOS_EXTERNO"].ToString().Trim();
                                    NUM_DOC_IDENTIDAD_EMPLEADO = filaInfoUsuario["NUM_DOC_IDENTIDAD_EXTERNO"].ToString().Trim();
                                }
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        if (NUM_DOC_IDENTIDAD_EMPLEADO != DOC_IDENTIDAD)
                        {
                            MensajeError = "El número de identificación digitado no corresponde al que esta almacenado en la bd. Consulte al administrador.";
                            correcto = false;
                            conexion.DeshacerTransaccion();
                        }
                    }

                    if (correcto == true)
                    {
                        tools _tools = new tools();

                        clave = CrearPassword(8);
                        mensaje = "Se ha realizado una solicitud de cambio de contraseña para el usuario <b>[USU_LOG]</b>.<br />";
                        mensaje += "<br />";
                        mensaje += "USUARIO: <b>[USU_LOG]</b><br />";
                        mensaje += "CONTRASEÑA NUEVA:<b>[CLAVE]</b><BR />";
                        mensaje += "<BR />";
                        mensaje += "Al ingresar nuevamente al Sistema debe digitar su nueva contraseña. <B>IMPORTANTE:</B> Se pedirá cambio obligatorio de esta contraseña.<br />";
                        mensaje += "<br />";
                        mensaje += "<br />";
                        mensaje += "Por favor no responder a este correo, es un correo de administración del Sistema.";
                        mensaje += "<br />";
                        mensaje += "<br />";
                        mensaje += "<br />";
                        mensaje += "<B>SISER WEB</B>";

                        mensaje = mensaje.Replace("[USU_LOG]", filaInfoUsuario["USU_LOG"].ToString().Trim());
                        mensaje = mensaje.Replace("[CLAVE]", clave);

                        if (_tools.enviarCorreoConCuerpoHtml(filaInfoUsuario["USU_MAIL"].ToString(), "Nueva Contraseña SISER WEB", mensaje) == true)
                        {
                            if (ActualizarClaveUsuario(USU_LOG, filaInfoUsuario["USU_PSW"].ToString(), Encriptar(clave), conexion) == false)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                            }
                        }
                        else
                        {
                            MensajeError = "No se puedo enviar el correo, la contraseña no pudo ser recuperada. Consulte al administrador del sistema.";
                            correcto = false;
                            conexion.DeshacerTransaccion();
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
                MensajeError = ex.Message;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public Boolean ActualizarClaveUsuarioDesdeInicioSesion(DataRow filaInfoUsuario, String NUM_DOC_IDENTIDAD, String USU_PSW_ANT, String USU_PSW_NEW)
        {
            String sql = String.Empty;
            String informacion;

            Boolean resultado = true;
            Boolean correcto = true;

            String USU_LOG = filaInfoUsuario["USU_LOG"].ToString().Trim();

            auditoria _auditoria = new auditoria(Empresa);

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if ((filaInfoUsuario["ESTADO_USUARIO"].ToString().ToUpper() == "INACTIVO") || (filaInfoUsuario["ID_ROL"] == DBNull.Value) || (filaInfoUsuario["ID_EMPLEADO"] == DBNull.Value))
                {
                    MensajeError = "Usuario con una o más restricciones. el cambio de password no se puede llevar a cabo. Consulte al administrador del sistema.";
                    resultado = false;
                }
                else
                {
                    if (filaInfoUsuario["USU_TIPO"].ToString().ToUpper() == "PLANTA")
                    {
                        if (filaInfoUsuario["NUM_DOC_IDENTIDAD"].ToString().Trim() != NUM_DOC_IDENTIDAD)
                        {
                            MensajeError = "La cedula no es válida, verifique dicha información.";
                            resultado = false;
                        }
                        else
                        {
                            if (Encriptar(USU_PSW_ANT).ToUpper() != filaInfoUsuario["USU_PSW"].ToString().Trim().ToUpper())
                            {
                                sql = "usp_crt_usuarios_actualizarContador ";
                                sql += "'" + USU_LOG + "'";
                                informacion = "usp_crt_usuarios_actualizarContador USU_LOG = '" + USU_LOG + "'";
                                if (conexion.ExecuteNonQuery(sql) <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    resultado = false;
                                    MensajeError = "No se pudo realizar la actualización de contador al determinar que el password no es correcto.";
                                }
                                else
                                {
                                    #region auditoria
                                    _auditoria.Adicionar(USU_LOG, tabla.CRT_USUARIOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                                    #endregion auditoria

                                    MensajeError = "El password anterior no coincide con el almacenado en la base de datos, recuerde que al tercer intento incorrecto el usuario será bloqueado.";
                                    resultado = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (filaInfoUsuario["NUM_DOC_IDENTIDAD_EXTERNO"].ToString().Trim() != NUM_DOC_IDENTIDAD)
                        {
                            MensajeError = "La cedula o password anterior no son válidos, verifique dicha información.";
                            resultado = false;
                        }
                        else
                        {
                            if (Encriptar(USU_PSW_ANT).ToUpper() != filaInfoUsuario["USU_PSW"].ToString().Trim().ToUpper())
                            {
                                sql = "usp_crt_usuarios_actualizarContador ";
                                sql += "'" + USU_LOG + "'";
                                informacion = "usp_crt_usuarios_actualizarContador USU_LOG = '" + USU_LOG + "'";

                                if (conexion.ExecuteNonQuery(sql) <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    resultado = false;
                                    MensajeError = "No se pudo realizar la actualización de contador al determinar que el password no es correcto.";
                                }
                                else
                                {
                                    #region auditoria
                                    _auditoria.Adicionar(USU_LOG, tabla.CRT_USUARIOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                                    #endregion auditoria

                                    MensajeError = "El password anterior no coincide con el almacenado en la base de datos, recuerde que al tercer intento incorrecto el usuario será bloqueado.";
                                    resultado = false;
                                }
                            }
                        }
                    }
                }

                if ((resultado == true) && (correcto == true))
                {
                    sql = "usp_crt_usuarios_actualizarClave_2 ";
                    sql += "'" + USU_LOG + "', ";
                    sql += "'" + Encriptar(USU_PSW_NEW) + "'";
                    informacion = "usp_crt_usuarios_actualizarClave_2 USU_LOG = '" + USU_LOG + "', USU_PSW_NEW = '" + Encriptar(USU_PSW_NEW) + "'";

                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        correcto = false;
                        resultado = false;
                        MensajeError = "No se pudo realizar la actualización del password, error en update.";
                        conexion.DeshacerTransaccion();
                    }
                    else
                    {
                        #region auditoria
                        _auditoria.Adicionar(USU_LOG, tabla.CRT_USUARIOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                        #endregion auditoria
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
                resultado = false;
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return resultado;
        }



        public Boolean ActualizarClaveUsuario(String USU_LOG, String USU_PSW, String USU_PSW_NEW)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            String pswEncriptada = null;
            String pswNewEncriptada = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_actualizarClave ";

            #region validaciones

            if (USU_PSW.Equals(USU_PSW_NEW))
            {
                MensajeError += "La nueva contraseña es igual a la anterior. Por Favor modifiquela.";
                ejecutar = false;
            }
            else
            {

                if (!(String.IsNullOrEmpty(USU_LOG)))
                {
                    sql += "'" + USU_LOG + "', ";
                    informacion += "USU_LOG = '" + USU_LOG.ToString() + "', ";
                }
                else
                {
                    MensajeError = "El campo USU_LOG no puede ser 0\n";
                    ejecutar = false;
                }

                if (!(String.IsNullOrEmpty(USU_PSW)))
                {
                    pswEncriptada = Encriptar(USU_PSW);
                    sql += "'" + pswEncriptada + "', ";

                    informacion += "USU_PSW = '" + pswEncriptada + "', ";
                }
                else
                {
                    MensajeError += "El campo USU_PSW no puede ser nulo\n";
                    ejecutar = false;
                }

                if (!(String.IsNullOrEmpty(USU_PSW_NEW)))
                {
                    pswNewEncriptada = Encriptar(USU_PSW_NEW);
                    sql += "'" + pswNewEncriptada + "'";
                    informacion += "USU_PSW_NEW = '" + pswNewEncriptada + "'";
                }
                else
                {
                    MensajeError += "El campo USU_PSW_NEW no puede ser nulo\n";
                    ejecutar = false;
                }

            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria2 = new auditoria(Empresa);
                    _auditoria2.Adicionar(USU_LOG, tabla.CRT_USUARIOS, tabla.ACCION_INICIO_SESION, sql, informacion, conexion);
                    #endregion auditoria

                    if (cantidadRegistrosActualizados == 0)
                    {
                        sql = "usp_crt_usuarios_actualizarContador";
                        sql += "'" + USU_LOG + "'";
                        int j = conexion.ExecuteNonQuery(sql);

                        #region auditoria
                        auditoria _auditoria3 = new auditoria(Empresa);
                        _auditoria3.Adicionar(USU_LOG, tabla.CRT_USUARIOS, tabla.ACCION_INICIO_SESION.ToString(), sql, informacion, conexion);
                        #endregion auditoria
                        MensajeError = "El usuario o clave no son correctos, verifique por favor. Al tercer intento sera bloqueado el usuario";

                    }
                    else
                    {
                        sql = "usp_crt_usuarios_borrarContador";
                        sql += "'" + USU_LOG + "' ";
                        int i = conexion.ExecuteNonQuery(sql);

                        #region auditoria
                        auditoria _auditoria4 = new auditoria(Empresa);
                        _auditoria4.Adicionar(USU_LOG, tabla.CRT_USUARIOS, tabla.ACCION_INICIO_SESION.ToString(), sql, informacion, conexion);
                        #endregion auditoria

                    }
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


        public Boolean ActualizarClaveUsuario(String USU_LOG, String USU_PSW, String USU_PSW_NEW, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            String pswEncriptada = null;
            String pswNewEncriptada = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_actualizarClave ";

            #region validaciones

            if (USU_PSW.Equals(USU_PSW_NEW))
            {
                MensajeError += "La nueva contraseña es igual a la anterior. Por Favor modifiquela.";
                ejecutar = false;
            }
            else
            {
                if (!(String.IsNullOrEmpty(USU_LOG)))
                {
                    sql += "'" + USU_LOG + "', ";
                    informacion += "USU_LOG = '" + USU_LOG.ToString() + "', ";
                }
                else
                {
                    MensajeError = "El campo USU_LOG no puede ser 0\n";
                    ejecutar = false;
                }

                if (!(String.IsNullOrEmpty(USU_PSW)))
                {
                    sql += "'" + USU_PSW + "', ";

                    informacion += "USU_PSW = '" + USU_PSW + "', ";
                }
                else
                {
                    MensajeError += "El campo USU_PSW no puede ser nulo\n";
                    ejecutar = false;
                }

                if (!(String.IsNullOrEmpty(USU_PSW_NEW)))
                {
                    sql += "'" + USU_PSW_NEW + "'";
                    informacion += "USU_PSW_NEW = '" + USU_PSW_NEW + "'";
                }
                else
                {
                    MensajeError += "El campo USU_PSW_NEW no puede ser nulo\n";
                    ejecutar = false;
                }

            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria2 = new auditoria(Empresa);
                    _auditoria2.Adicionar(USU_LOG, tabla.CRT_USUARIOS, tabla.ACCION_INICIO_SESION, sql, informacion, conexion);
                    #endregion auditoria

                    if (cantidadRegistrosActualizados == 0)
                    {
                        sql = "usp_crt_usuarios_actualizarContador";
                        sql += "'" + USU_LOG + "'";
                        int j = conexion.ExecuteNonQuery(sql);

                        #region auditoria
                        auditoria _auditoria3 = new auditoria(Empresa);
                        _auditoria3.Adicionar(USU_LOG, tabla.CRT_USUARIOS, tabla.ACCION_INICIO_SESION.ToString(), sql, informacion, conexion);
                        #endregion auditoria
                        MensajeError = "El usuario o clave no son correctos, verifique por favor. Al tercer intento sera bloqueado el usuario";

                    }
                    else
                    {
                        sql = "usp_crt_usuarios_borrarContador";
                        sql += "'" + USU_LOG + "' ";
                        int i = conexion.ExecuteNonQuery(sql);

                        #region auditoria
                        auditoria _auditoria4 = new auditoria(Empresa);
                        _auditoria4.Adicionar(USU_LOG, tabla.CRT_USUARIOS, tabla.ACCION_INICIO_SESION.ToString(), sql, informacion, conexion);
                        #endregion auditoria

                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarClaveUsuarioDesdeAdministracion(String USU_LOG, String USU_PSW_NEW, String USU_MOD, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            String pswEncriptada = null;
            String pswNewEncriptada = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_actualizarClave_1 ";

            #region validaciones

            if (!(String.IsNullOrEmpty(USU_LOG)))
            {
                sql += "'" + USU_LOG + "', ";
                informacion += "USU_LOG = '" + USU_LOG + "', ";
            }
            else
            {
                MensajeError = "El campo USU_LOG no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_PSW_NEW)))
            {
                pswNewEncriptada = Encriptar(USU_PSW_NEW);
                sql += "'" + pswNewEncriptada + "'";
                informacion += "USU_PSW = '" + pswEncriptada + "'";
            }
            else
            {
                MensajeError += "El campo USU_PSW no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria2 = new auditoria(Empresa);
                    _auditoria2.Adicionar(USU_MOD, tabla.CRT_USUARIOS, tabla.ACCION_INICIO_SESION.ToString(), sql, informacion, conexion);
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

        public void encriptarPasswordsBD()
        {
            String sql = null;
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();

            sql = "select USU_PSW, USU_LOG from CRT_USUARIOS";

            _dataSet = conexion.ExecuteReader(sql);
            _dataView = _dataSet.Tables[0].DefaultView;
            _dataTable = _dataView.Table;

            String clave = null;
            String usuario = null;
            String claveEncriptada = null;
            foreach (DataRow x in _dataTable.Rows)
            {
                usuario = x["USU_LOG"].ToString().Trim();
                clave = x["USU_PSW"].ToString().Trim();
                claveEncriptada = Encriptar(clave);
                ActualizarClaveUsuario(usuario, clave, claveEncriptada);
            }
        }

        public static string CrearPassword(int longitud)
        {
            string _caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Byte[] valoresAleatorios = new Byte[longitud];
            char[] caracteres = new char[longitud];
            int allowedCharCount = _caracteres.Length;

            for (int i = 0; i < longitud; i++)
            {
                Random randomObj = new Random();
                randomObj.NextBytes(valoresAleatorios);
                caracteres[i] = _caracteres[(int)valoresAleatorios[i] % allowedCharCount];
            }

            return new string(caracteres);
        }

        public String Encriptar(String value)
        {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes(value);

            data = provider.ComputeHash(data);

            string md5 = string.Empty;

            for (int i = 0; i < data.Length; i++)
                md5 += data[i].ToString("x2").ToLower();

            return md5;
        }

        public String ObtenerUsuarioMailPorUsuLog(String USU_LOG)
        {
            Conexion conexion = new Conexion(Empresa);
            String mail = null;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_buscarPorUsuLog ";

            if (!(String.IsNullOrEmpty(USU_LOG)))
                sql += "'" + USU_LOG + "'";
            else
            {
                MensajeError += "El campo USU_LOG no puede ser vacio. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    mail = conexion.ExecuteScalar(sql);
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
            return mail;
        }

        public Decimal AdicionarNomEmpleados(int ID_CONTRATO, int ID_SOLICITUD, int ID_EMPRESA, int ID_CENTRO_C, int ID_SUB_C,
            DateTime FCH_INGRESO, Decimal SALARIO, String PENSIONADO, String ACTIVO, String LIQUIDADO, Decimal RIESGO, int ID_ARP, int ID_CAJA_C,
            int ID_EPS, int ID_F_PENSIONES, String TIP_PAGO, int ID_ENTIDAD, String NUM_CUENTA, String SAL_INT, int ID_PERFIL)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_empleados_adicionar ";

            #region validaciones

            if (ID_CONTRATO != 0)
            {
                sql += ID_CONTRATO + ", ";
                informacion += "ID_CONTRATO = '" + ID_CONTRATO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CONTRATO = '0', ";
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
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C= '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C= '" + ID_SUB_C.ToString() + "', ";
            }
            if (!(String.IsNullOrEmpty(FCH_INGRESO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INGRESO) + "', ";
                informacion += "FCH_INGRESO= '" + FCH_INGRESO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FCH_INGRESO no puede ser nulo\n";
                ejecutar = false;
            }
            if (SALARIO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(SALARIO) + ", ";
                informacion += "SALARIO= '" + SALARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SALARIO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(PENSIONADO.ToString())))
            {
                sql += "'" + PENSIONADO + "', ";
                informacion += "PENSIONADO= '" + PENSIONADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PENSIONADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ACTIVO.ToString())))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO = '" + ACTIVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(LIQUIDADO.ToString())))
            {
                sql += "'" + LIQUIDADO + "', ";
                informacion += "LIQUIDADO = '" + LIQUIDADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQUIDADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (RIESGO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(RIESGO) + ", ";
                informacion += "RIESGO= '" + RIESGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RIESGO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_ARP != 0)
            {
                sql += ID_ARP + ", ";
                informacion += "ID_ARP = '" + ID_ARP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ARP no puede ser nulo\n";
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
            if (!(String.IsNullOrEmpty(TIP_PAGO)))
            {
                sql += "'" + TIP_PAGO + "', ";
                informacion += "TIP_PAGO = '" + TIP_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIP_PAGO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD + ", ";
                informacion += "ID_ENTIDAD = '" + ID_ENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(NUM_CUENTA)))
            {
                sql += "'" + NUM_CUENTA + "', ";
                informacion += "NUM_CUENTA = '" + NUM_CUENTA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_CUENTA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(SAL_INT)))
            {
                sql += "'" + SAL_INT + "', ";
                informacion += "SAL_INT = '" + SAL_INT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SAL_INT no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + NombreUsuario + "', ";
            informacion += "USU_CRE= 'null', ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + " ";
                informacion += "ID_PERFIL = '" + ID_PERFIL.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
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
                    _auditoria.Adicionar(NombreUsuario, tabla.NOM_EMPLEADOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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


        public Decimal AdicionarNomEmpleados(int ID_CONTRATO, int ID_SOLICITUD, int ID_EMPRESA, int ID_CENTRO_C, int ID_SUB_C,
            DateTime FCH_INGRESO, Decimal SALARIO, String PENSIONADO, String ACTIVO, String LIQUIDADO, Decimal RIESGO, int ID_ARP, int ID_CAJA_C,
            int ID_EPS, int ID_F_PENSIONES, String TIP_PAGO, int ID_ENTIDAD, String NUM_CUENTA, String SAL_INT, String ID_CIUDAD, int ID_PERFIL, String TIPO_CUENTA, string descripcion_salario, string formaPago, Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_empleados_adicionar ";

            #region validaciones

            if (ID_CONTRATO != 0)
            {
                sql += ID_CONTRATO + ", ";
                informacion += "ID_CONTRATO = '" + ID_CONTRATO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CONTRATO = '0', ";
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

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C= '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C= '" + ID_SUB_C.ToString() + "', ";
            }
            if (!(String.IsNullOrEmpty(FCH_INGRESO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INGRESO) + "', ";
                informacion += "FCH_INGRESO= '" + FCH_INGRESO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FCH_INGRESO no puede ser nulo\n";
                ejecutar = false;
            }
            if (SALARIO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(SALARIO) + ", ";
                informacion += "SALARIO= '" + SALARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SALARIO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(PENSIONADO.ToString())))
            {
                sql += "'" + PENSIONADO + "', ";
                informacion += "PENSIONADO= '" + PENSIONADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PENSIONADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(ACTIVO.ToString())))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO = '" + ACTIVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(LIQUIDADO.ToString())))
            {
                sql += "'" + LIQUIDADO + "', ";
                informacion += "LIQUIDADO = '" + LIQUIDADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQUIDADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (RIESGO != 0)
            {
                sql += RIESGO.ToString() + ", ";
                informacion += "RIESGO = '" + RIESGO + "', ";
            }
            else
            {
                MensajeError += "El campo RIESGO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ARP != 0)
            {
                sql += ID_ARP + ", ";
                informacion += "ID_ARP = '" + ID_ARP.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_ARP = '0', ";
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

            if (ID_F_PENSIONES != 0)
            {
                sql += ID_F_PENSIONES + ", ";
                informacion += "ID_F_PENSIONES = '" + ID_F_PENSIONES.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_F_PENSIONES = 0, ";
            }

            if (!(String.IsNullOrEmpty(TIP_PAGO)))
            {
                sql += "'" + TIP_PAGO + "', ";
                informacion += "TIP_PAGO = '" + TIP_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIP_PAGO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD + ", ";
                informacion += "ID_ENTIDAD = '" + ID_ENTIDAD.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_ENTIDAD = 0, ";
            }

            if (!(String.IsNullOrEmpty(NUM_CUENTA)))
            {
                sql += "'" + NUM_CUENTA + "', ";
                informacion += "NUM_CUENTA = '" + NUM_CUENTA.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NUM_CUENTA = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(SAL_INT)))
            {
                sql += "'" + SAL_INT + "', ";
                informacion += "SAL_INT = '" + SAL_INT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SAL_INT no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + NombreUsuario + "', ";
            informacion += "USU_CRE= 'null', ";

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "ID_CIUDAD = 'null', ";
            }
            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL = '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_CUENTA.ToString())))
            {
                sql += "'" + TIPO_CUENTA + "', ";
                informacion += "TIPO_CUENTA= '" + TIPO_CUENTA.ToString() + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "TIPO_CUENTA= 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(descripcion_salario.ToString())))
            {
                sql += "'" + descripcion_salario + "', ";
                informacion += "DESCRIPCION_SALARIO= '" + descripcion_salario.ToString() + ", ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "descripcion_salario = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(TIPO_CUENTA.ToString())))
            {
                sql += "'" + formaPago + "'";
                informacion += "FORMA_PAGO = '" + formaPago.ToString() + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "FORMA_PAGO = 'NULL'";
            }

            #endregion validaciones

            if (ejecutar)
            {

                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(NombreUsuario, tabla.NOM_EMPLEADOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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


        public Boolean ActualizarNomEmpleados(int ID_EMPLEADO, int ID_CONTRATO, int ID_SOLICITUD, int ID_EMPRESA, int ID_CENTRO_C, int ID_SUB_C,
            DateTime FCH_INGRESO, Decimal SALARIO, String PENSIONADO, String ACTIVO, String LIQUIDADO, Decimal RIESGO, int ID_ARP, int ID_CAJA_C,
            int ID_EPS, int ID_F_PENSIONES, String TIP_PAGO, int ID_ENTIDAD, String NUM_CUENTA, String SAL_INT, int ID_PERFIL)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_empleados_actualizar ";

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
            if (ID_CONTRATO != 0)
            {
                sql += ID_CONTRATO + ", ";
                informacion += "ID_CONTRATO = '" + ID_CONTRATO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CONTRATO = '0', ";
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
            
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            
            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C= '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C= '" + ID_SUB_C.ToString() + "', ";
            }
            
            if (!(String.IsNullOrEmpty(FCH_INGRESO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INGRESO) + "', ";
                informacion += "FCH_INGRESO= '" + FCH_INGRESO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FCH_INGRESO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (SALARIO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(SALARIO) + ", ";
                informacion += "SALARIO= '" + SALARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SALARIO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (!(String.IsNullOrEmpty(PENSIONADO.ToString())))
            {
                sql += "'" + PENSIONADO + "', ";
                informacion += "PENSIONADO= '" + PENSIONADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PENSIONADO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (!(String.IsNullOrEmpty(ACTIVO.ToString())))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO = '" + ACTIVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (!(String.IsNullOrEmpty(LIQUIDADO.ToString())))
            {
                sql += "'" + LIQUIDADO + "', ";
                informacion += "LIQUIDADO = '" + LIQUIDADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQUIDADO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (RIESGO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(RIESGO) + ", ";
                informacion += "RIESGO= '" + RIESGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RIESGO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (ID_ARP != 0)
            {
                sql += ID_ARP + ", ";
                informacion += "ID_ARP = '" + ID_ARP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ARP no puede ser nulo\n";
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
                informacion += "ID_CAJA_C = '0',";
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
            
            if (!(String.IsNullOrEmpty(TIP_PAGO)))
            {
                sql += "'" + TIP_PAGO + "', ";
                informacion += "TIP_PAGO = '" + TIP_PAGO.ToString() + "', ";
            }
            else
            {
                sql += "' ', ";
                informacion += "TIP_PAGO = ' ', ";
            }
            
            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD + ", ";
                informacion += "ID_ENTIDAD = '" + ID_ENTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (!(String.IsNullOrEmpty(NUM_CUENTA)))
            {
                sql += "'" + NUM_CUENTA + "', ";
                informacion += "NUM_CUENTA = '" + NUM_CUENTA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUM_CUENTA no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (!(String.IsNullOrEmpty(SAL_INT)))
            {
                sql += "'" + SAL_INT + "', ";
                informacion += "SAL_INT = '" + SAL_INT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SAL_INT no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + NombreUsuario + "', ";
            informacion += "USU_MOD= 'null', ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + " ";
                informacion += "ID_PERFIL = '" + ID_PERFIL.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
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
                    _auditoria.Adicionar(NombreUsuario, tabla.NOM_CONCEPTOS_EMPLEADO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public Boolean ActualizarNomEmpleados(int ID_EMPLEADO, int ID_CONTRATO, int ID_SOLICITUD, int ID_EMPRESA, int ID_CENTRO_C, int ID_SUB_C,
            DateTime FCH_INGRESO, Decimal SALARIO, String PENSIONADO, String ACTIVO, String LIQUIDADO, Decimal RIESGO, int ID_ARP, int ID_CAJA_C,
            int ID_EPS, int ID_F_PENSIONES, String TIP_PAGO, int ID_ENTIDAD, String NUM_CUENTA, String SAL_INT, String ID_CIUDAD, int ID_PERFIL, string formaPago, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_nom_empleados_actualizar ";

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
            if (ID_CONTRATO != 0)
            {
                sql += ID_CONTRATO + ", ";
                informacion += "ID_CONTRATO = '" + ID_CONTRATO.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CONTRATO = '0', ";
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
            
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "', ";
            }
            
            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C= '" + ID_SUB_C.ToString() + "', ";
            }
            else
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C= '" + ID_SUB_C.ToString() + "', ";
            }
            
            if (!(String.IsNullOrEmpty(FCH_INGRESO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INGRESO) + "', ";
                informacion += "FCH_INGRESO= '" + FCH_INGRESO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FCH_INGRESO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (SALARIO != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(SALARIO) + ", ";
                informacion += "SALARIO= '" + SALARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SALARIO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (!(String.IsNullOrEmpty(PENSIONADO.ToString())))
            {
                sql += "'" + PENSIONADO + "', ";
                informacion += "PENSIONADO= '" + PENSIONADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PENSIONADO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (!(String.IsNullOrEmpty(ACTIVO.ToString())))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO = '" + ACTIVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (!(String.IsNullOrEmpty(LIQUIDADO.ToString())))
            {
                sql += "'" + LIQUIDADO + "', ";
                informacion += "LIQUIDADO = '" + LIQUIDADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo LIQUIDADO no puede ser nulo\n";
                ejecutar = false;
            }
            
            if (RIESGO != 0)
            {
                sql += RIESGO + ", ";
                informacion += "RIESGO=  '" + RIESGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RIESGO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ARP != 0)
            {
                sql += ID_ARP + ", ";
                informacion += "ID_ARP = '" + ID_ARP.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_ARP = '0', ";
            }

            if (ID_CAJA_C != 0)
            {
                sql += ID_CAJA_C + ", ";
                informacion += "ID_CAJA_C = '" + ID_CAJA_C.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CAJA_C = 0, ";
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
            
            if (ID_F_PENSIONES != 0)
            {
                sql += ID_F_PENSIONES + ", ";
                informacion += "ID_F_PENSIONES = '" + ID_F_PENSIONES.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_F_PENSIONES = 0, ";
            }
            
            if (!(String.IsNullOrEmpty(TIP_PAGO)))
            {
                sql += "'" + TIP_PAGO + "', ";
                informacion += "TIP_PAGO = '" + TIP_PAGO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "TIP_PAGO = ' ', ";
            }

            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD + ", ";
                informacion += "ID_ENTIDAD = '" + ID_ENTIDAD.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_ENTIDAD = 0, ";
            }

            if (!(String.IsNullOrEmpty(NUM_CUENTA)))
            {
                sql += "'" + NUM_CUENTA + "', ";
                informacion += "NUM_CUENTA = '" + NUM_CUENTA.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "NUM_CUENTA = 'null', ";
            }
            
            if (!(String.IsNullOrEmpty(SAL_INT)))
            {
                sql += "'" + SAL_INT + "', ";
                informacion += "SAL_INT = '" + SAL_INT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SAL_INT no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + NombreUsuario + "', ";
            informacion += "USU_MOD = '" + NombreUsuario + "', ";

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                sql += "Null,";
                informacion += "ID_CIUDAD = 'null', ";
            }

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL = '" + ID_PERFIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(formaPago)))
            {
                sql += "'" + formaPago + "'";
                informacion += "FORMA_PAGO = '" + formaPago.ToString() + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "FORMA_PAGO = 'null'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(NombreUsuario, tabla.NOM_CONCEPTOS_EMPLEADO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public DataTable ObtenerCiudadRegionalPorUsuLog(String USU_LOG)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuario_obtener_ciudad_regional_por_usu_log  ";

            if (!(String.IsNullOrEmpty(USU_LOG)))
                sql += "'" + USU_LOG + "'";
            else
            {
                MensajeError += "El campo USU_LOG no puede ser vacio. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerCrtUsuarioPorIdUsuario(Decimal ID_USUARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_ObtenerPorIdUsuario ";

            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_USUARIO no puede ser 0.";
            }

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

        public DataTable ObtenerEmpresasAsociadasAUsaurio(Decimal ID_USUARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_empresas_ciudades_ObtenerEmpresasAsociadasAUsuario ";

            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_USUARIO no puede ser 0.";
            }

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

        public DataTable ObtenerIdEmpresaAsociadaAUsuarioPublico(String usuLog)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_empresas_ciudades_obtener_empresa_usuario_publico ";

            if (String.IsNullOrEmpty(usuLog) == false)
            {
                sql += "'" + usuLog + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_USUARIO no puede ser 0.";
            }

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
