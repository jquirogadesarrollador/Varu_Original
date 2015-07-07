using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.operaciones;

namespace Brainsbits.LLB.maestras
{
    public class contactos
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;

        private Dictionary<String, String> diccionarioContactos;
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

        #endregion propiedades

        #region constructores
        public contactos(String idEmpresa)
        {
            Empresa = idEmpresa;

            diccionarioContactos = new Dictionary<string, string>();
            diccionarioContactos.Add("CONT_NOM", "Nombre");
            diccionarioContactos.Add("CONT_CIUDAD", "Ciudad");
            diccionarioContactos.Add("CONT_CARGO", "Cargo");
            diccionarioContactos.Add("CONT_TEL", "Telefono");
            diccionarioContactos.Add("CONT_MAIL", "E-Mail");
        }
        #endregion

        #region metodos

        public Decimal AdicionarContactoTransaccion(Decimal ID_EMPRESA,
            Brainsbits.LLB.tabla.proceso PROCESO,
            String CONT_NOM,
            String CONT_CARGO,
            String CONT_MAIL,
            String CONT_TEL,
            String CONT_TEL1,
            String CONT_CELULAR,
            String CONT_CIUDAD,
            String USU_CRE,
            String CONT_ESTADO,
            Int32 DIAS,
            String FORMA_PAGO,
            Decimal BANCO,
            Decimal CUENTA,
            Conexion conexion)
        {
            Decimal registro = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_contactos_adicionar ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if ((Int32)PROCESO != 0)
            {
                sql += (Int32)PROCESO + ", ";
                informacion += "PROCESO = " + (Int32)PROCESO + ", ";
            }
            else
            {
                MensajeError = "El campo PROCESO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_NOM)))
            {
                sql += "'" + CONT_NOM + "', ";
                informacion += "CONT_NOM = '" + CONT_NOM.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DE CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_CARGO)))
            {
                sql += "'" + CONT_CARGO + "', ";
                informacion += "CONT_CARGO = '" + CONT_CARGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO DEL CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_MAIL)))
            {
                sql += "'" + CONT_MAIL + "', ";
                informacion += "CONT_MAIL = '" + CONT_MAIL.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CONT_MAIL = 'null', ";
            }

            if (!(String.IsNullOrEmpty(CONT_TEL)))
            {
                sql += "'" + CONT_TEL + "', ";
                informacion += "CONT_TEL = '" + CONT_TEL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO DEL CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_TEL1)))
            {
                sql += "'" + CONT_TEL1 + "', ";
                informacion += "CONT_TEL1 = '" + CONT_TEL1.ToString() + "', ";
            }
            else
            {
                sql += "Null,";
            }

            if (!(String.IsNullOrEmpty(CONT_CELULAR)))
            {
                sql += "'" + CONT_CELULAR + "', ";
                informacion += "CONT_CELULAR = '" + CONT_CELULAR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CELULAR DEL CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_CIUDAD)))
            {
                sql += "'" + CONT_CIUDAD + "', ";
                informacion += "CONT_CIUDAD = '" + CONT_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD DEL CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_CRE)))
            {
                sql += "'" + USU_CRE + "', ";
                informacion += "USU_CRE = '" + USU_CRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_ESTADO)))
            {
                sql += "'" + CONT_ESTADO + "', ";
                informacion += "CONT_ESTADO = '" + CONT_ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += DIAS.ToString() + ", '" + FORMA_PAGO + "', " + BANCO.ToString() + ", " + CUENTA.ToString() + "";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_CRE, tabla.VEN_CONTACTOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }

            return registro;
        }

        public Decimal Adicionar(Decimal ID_EMPRESA,
            Brainsbits.LLB.tabla.proceso PROCESO,
            String CONT_NOM,
            String CONT_CARGO,
            String CONT_MAIL,
            String CONT_TEL,
            String CONT_TEL1,
            String CONT_CELULAR,
            String CONT_CIUDAD,
            String USU_CRE,
            String CONT_ESTADO,
            Int32 DIAS = 0,
            String FORMA_PAGO = "",
            Decimal BANCO = 0,
            Decimal CUENTA = 0)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            Boolean continuarNormalmente = true;
            ManualServicio _manual = new ManualServicio(Empresa, USU_CRE);
            ManualServicio.ListaSecciones AREA_MANUAL;
            if (PROCESO == tabla.proceso.ContactoComercial)
            {
                AREA_MANUAL = ManualServicio.ListaSecciones.Comercial;
            }
            else
            {
                if (PROCESO == tabla.proceso.ContactoContratacion)
                {
                    AREA_MANUAL = ManualServicio.ListaSecciones.Contratacion;
                }
                else
                {
                    if (PROCESO == tabla.proceso.ContactoNominaFacturacion)
                    {
                        AREA_MANUAL = ManualServicio.ListaSecciones.Nomina;
                    }
                    else
                    {
                        if (PROCESO == tabla.proceso.ContactoSeleccion)
                        {
                            AREA_MANUAL = ManualServicio.ListaSecciones.Seleccion;
                        }
                        else
                        {
                            if (PROCESO == tabla.proceso.Financiera)
                            {
                                AREA_MANUAL = ManualServicio.ListaSecciones.Financiera;
                            }
                            else
                            {
                                AREA_MANUAL = ManualServicio.ListaSecciones.Desconocida;
                            }
                        }
                    }
                }
            }

            Decimal ID_CONTACTO = 0;
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion); 

                if (realizarVersionamientoManual == true)
                {
                    ID_VERSIONAMIENTO = _manual.RegistrarInsersionRegistroTabla(ID_VERSIONAMIENTO, AREA_MANUAL, "CONTÁCTO", "Contácto", CONT_NOM, ManualServicio.AccionesManual.Insertar, ID_EMPRESA, conexion);
                    if (ID_VERSIONAMIENTO == -1)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        continuarNormalmente = false;
                        ID_VERSIONAMIENTO = 0;
                        ID_CONTACTO = 0;
                    }
                }

                if (continuarNormalmente == true)
                {
                    ID_CONTACTO = AdicionarContactoTransaccion(ID_EMPRESA, PROCESO, CONT_NOM, CONT_CARGO, CONT_MAIL, CONT_TEL, CONT_TEL1, CONT_CELULAR, CONT_CIUDAD, USU_CRE, CONT_ESTADO, DIAS, FORMA_PAGO, BANCO, CUENTA, conexion);

                    if (ID_CONTACTO <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        ID_CONTACTO = 0;
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
                ID_CONTACTO = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_CONTACTO;
        }

        public Boolean ActualizarContactoConTransaccion(Decimal REGISTRO,
            Decimal ID_EMPRESA,
            Brainsbits.LLB.tabla.proceso PROCESO,
            String CONT_NOM,
            String CONT_CARGO,
            String CONT_MAIL,
            String CONT_TEL,
            String CONT_TEL1,
            String CONT_CELULAR,
            String CONT_CIUDAD,
            String USU_MOD,
            String CONT_ESTADO,
            Int32 DIAS,
            String FORMA_PAGO,
            Decimal BANCO,
            Decimal CUENTA,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_contactos_actualizar ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO.ToString() + ", ";
                informacion += "REGISTRO = " + REGISTRO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if ((Int32)PROCESO != 0)
            {
                sql += (Int32)PROCESO + ", ";
                informacion += "PROCESO = " + (Int32)PROCESO + ", ";
            }
            else
            {
                MensajeError = "El campo PROCESO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_NOM)))
            {
                sql += "'" + CONT_NOM + "', ";
                informacion += "CONT_NOM = '" + CONT_NOM.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DE CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_CARGO)))
            {
                sql += "'" + CONT_CARGO + "', ";
                informacion += "CONT_CARGO = '" + CONT_CARGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO DEL CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_MAIL)))
            {
                sql += "'" + CONT_MAIL + "', ";
                informacion += "CONT_MAIL = '" + CONT_MAIL.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CONT_MAIL = 'null', ";
            }

            if (!(String.IsNullOrEmpty(CONT_TEL)))
            {
                sql += "'" + CONT_TEL + "', ";
                informacion += "CONT_TEL = '" + CONT_TEL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO DEL CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_TEL1)))
            {
                sql += "'" + CONT_TEL1 + "', ";
                informacion += "CONT_TEL1 = '" + CONT_TEL1.ToString() + "', ";
            }
            else
            {
                sql += "Null,";
            }

            if (!(String.IsNullOrEmpty(CONT_CELULAR)))
            {
                sql += "'" + CONT_CELULAR + "', ";
                informacion += "CONT_CELULAR = '" + CONT_CELULAR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CELULAR DEL CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_CIUDAD)))
            {
                sql += "'" + CONT_CIUDAD + "', ";
                informacion += "CONT_CIUDAD = '" + CONT_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD DEL CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_MOD)))
            {
                sql += "'" + USU_MOD + "', ";
                informacion += "USU_MOD = '" + USU_MOD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONT_ESTADO)))
            {
                sql += "'" + CONT_ESTADO + "', ";
                informacion += "CONT_ESTADO = '" + CONT_ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += DIAS.ToString() + ", '" + FORMA_PAGO + "', " + BANCO.ToString() + ", " + CUENTA.ToString() + "";
            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_MOD, tabla.VEN_CONTACTOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean Actualizar(Decimal REGISTRO,
            Decimal ID_EMPRESA,
            Brainsbits.LLB.tabla.proceso PROCESO,
            String CONT_NOM,
            String CONT_CARGO,
            String CONT_MAIL,
            String CONT_TEL,
            String CONT_TEL1,
            String CONT_CELULAR,
            String CONT_CIUDAD,
            String USU_MOD,
            String CONT_ESTADO,
            Int32 DIAS = 0,
            String FORMA_PAGO = "",
            Decimal BANCO = 0,
            Decimal CUENTA = 0)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            Boolean continuarNormalmente = true;
            ManualServicio _manual = new ManualServicio(Empresa, USU_MOD);
            ManualServicio.ListaSecciones AREA_MANUAL;
            if (PROCESO == tabla.proceso.ContactoComercial)
            {
                AREA_MANUAL = ManualServicio.ListaSecciones.Comercial;
            }
            else
            {
                if (PROCESO == tabla.proceso.ContactoContratacion)
                {
                    AREA_MANUAL = ManualServicio.ListaSecciones.Contratacion;
                }
                else
                {
                    if (PROCESO == tabla.proceso.ContactoNominaFacturacion)
                    {
                        AREA_MANUAL = ManualServicio.ListaSecciones.Nomina;
                    }
                    else
                    {
                        if (PROCESO == tabla.proceso.ContactoSeleccion)
                        {
                            AREA_MANUAL = ManualServicio.ListaSecciones.Seleccion;
                        }
                        else
                        {
                            AREA_MANUAL = ManualServicio.ListaSecciones.Desconocida;
                        }
                    }
                }
            }

            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion);

                DataTable tablaDatosAnteriores = _manual.ObtenerInfoRegistroTabla(diccionarioContactos, "VEN_CONTACTOS", "REGISTRO", REGISTRO.ToString(), conexion);

                if (ActualizarContactoConTransaccion(REGISTRO, ID_EMPRESA, PROCESO, CONT_NOM, CONT_CARGO, CONT_MAIL, CONT_TEL, CONT_TEL1, CONT_CELULAR, CONT_CIUDAD, USU_MOD, CONT_ESTADO, DIAS, FORMA_PAGO, BANCO, CUENTA, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    DataTable tablaDatosNuevos = _manual.ObtenerInfoRegistroTabla(diccionarioContactos, "VEN_CONTACTOS", "REGISTRO", REGISTRO.ToString(), conexion);

                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.DeterminarYRegistrarModificacionesEnCampos(diccionarioContactos, tablaDatosAnteriores, tablaDatosNuevos, ID_VERSIONAMIENTO, ID_EMPRESA, AREA_MANUAL, ManualServicio.AccionesManual.Actualizar, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                        }
                    }
                }

                if ((correcto == true) && (continuarNormalmente == true))
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

        public DataTable ObtenerContactosPorIdEmpresa(Decimal ID_EMPRESA, Brainsbits.LLB.tabla.proceso PROCESO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_contactos_buscarPorIdEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ",";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }
            if ((Int32)PROCESO != 0)
            {
                sql += (Int32)PROCESO;
            }
            else
            {
                MensajeError = "El campo PROCESO no puede ser 0\n";
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

        public DataTable ObtenerContactosPorIdEmpresa(Decimal ID_EMPRESA, Brainsbits.LLB.tabla.proceso PROCESO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_contactos_buscarPorIdEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ",";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }
            if ((Int32)PROCESO != 0)
            {
                sql += (Int32)PROCESO;
            }
            else
            {
                MensajeError = "El campo PROCESO no puede ser 0\n";
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

        public DataTable ObtenerContactosPorRegistro(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_contactos_buscarPorRegistro ";

            if (REGISTRO != 0) sql += REGISTRO;
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0. \n";
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

        public DataTable ObtenerTodosContactosDeUnaEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_contactos_buscarTodosDeUnaEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString();
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0. \n";
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

        #endregion metodos
    }
}
