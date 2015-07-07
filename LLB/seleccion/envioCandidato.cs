using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.operaciones;

namespace Brainsbits.LLB.seleccion
{
    public class envioCandidato
    {
        #region varialbes

        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;

        private Dictionary<String, String> diccionarioCamposVenPEnvioCandidatos;

        #endregion varialbes

        #region propiedades
        public String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        #endregion propiedades

        #region constructores
        public envioCandidato(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;

            diccionarioCamposVenPEnvioCandidatos = new Dictionary<string, string>();

            diccionarioCamposVenPEnvioCandidatos.Add("DIR_ENVIO", "Dirección de Envío");
            diccionarioCamposVenPEnvioCandidatos.Add("CIU_ENVIO", "Ciudad de Envío");
            diccionarioCamposVenPEnvioCandidatos.Add("TEL_ENVIO", "Telefono de Envío");
            diccionarioCamposVenPEnvioCandidatos.Add("COND_ENVIO", "Condiciones de Envío");
            diccionarioCamposVenPEnvioCandidatos.Add("REGISTRO_CONTACTO", "Contácto de Envío");

        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarCondicionConTransaccion(Decimal ID_EMPRESA,
            String DIR_ENVIO,
            String CIU_ENVIO,
            String TEL_ENVIO,
            Decimal REGISTRO_CONTACTO,
            String COND_ENVIO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Decimal registro = 0;

            sql = "usp_ven_p_envio_candidatos_adicionar ";

            #region validaciones

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + " ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIR_ENVIO)))
            {
                sql += "'" + DIR_ENVIO + "', ";
                informacion += "DIR_ENVIO = '" + DIR_ENVIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DIR_ENVIO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIU_ENVIO)))
            {
                sql += "'" + CIU_ENVIO + "', ";
                informacion += "CIU_ENVIO = '" + CIU_ENVIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIU_ENVIO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_ENVIO)))
            {
                sql += "'" + TEL_ENVIO + "', ";
                informacion += "TEL_ENVIO = '" + TEL_ENVIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TEL_ENVIO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (REGISTRO_CONTACTO != 0)
            {
                sql += REGISTRO_CONTACTO + ", ";
                informacion += "REGISTRO_CONTACTO = '" + REGISTRO_CONTACTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_CONTACTO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COND_ENVIO)))
            {
                sql += "'" + COND_ENVIO + "', ";
                informacion += "COND_ENVIO = '" + COND_ENVIO.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo COND_ENVIO no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_P_ENVIO_CANDIDATOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }
            else
            {
                registro = 0;
            }

            return registro;
        }

        public Decimal Adicionar(Decimal ID_EMPRESA, String DIR_ENVIO, String CIU_ENVIO, String TEL_ENVIO, Decimal REGISTRO_CONTACTO, String COND_ENVIO)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            Boolean continuarNormalmente = true;
            ManualServicio _manual = new ManualServicio(Empresa, Usuario);

            Decimal REGISTRO = 0;
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion); 

                if (realizarVersionamientoManual == true)
                {
                    ID_VERSIONAMIENTO = _manual.RegistrarInsersionRegistroTabla(ID_VERSIONAMIENTO, ManualServicio.ListaSecciones.Seleccion, "CONDICION_ENVIO", "Condición de Envío", COND_ENVIO, ManualServicio.AccionesManual.Insertar, ID_EMPRESA, conexion);
                    if (ID_VERSIONAMIENTO == -1)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        continuarNormalmente = false;
                        ID_VERSIONAMIENTO = 0;
                        REGISTRO = 0;
                    }
                }

                if (continuarNormalmente == true)
                {
                    REGISTRO = AdicionarCondicionConTransaccion(ID_EMPRESA, DIR_ENVIO, CIU_ENVIO, TEL_ENVIO, REGISTRO_CONTACTO, COND_ENVIO, conexion);

                    if (REGISTRO <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        REGISTRO = 0;
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
                REGISTRO = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return REGISTRO;
        }

        public Boolean ActualizarConTransaccion(Decimal REGISTRO,
            String DIR_ENVIO,
            String CIU_ENVIO,
            String TEL_ENVIO,
            String COND_ENVIO,
            Decimal REGISTRO_CONTACTO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_envio_candidatos_actualizar ";

            #region VALIDACIONES
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = " + REGISTRO.ToString() + ",";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(DIR_ENVIO)))
            {
                sql += "'" + DIR_ENVIO + "', ";
                informacion += "DIR_ENVIO = '" + DIR_ENVIO + "', ";
            }
            else
            {
                MensajeError += "La DIR_ENVIO no puede ser nula\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(CIU_ENVIO)))
            {
                sql += "'" + CIU_ENVIO + "', ";
                informacion += "CIU_ENVIO = '" + CIU_ENVIO + "', ";
            }
            else
            {
                MensajeError += "La CIU_ENVIO no puede ser nula\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(TEL_ENVIO)))
            {
                sql += "'" + TEL_ENVIO + "', ";
                informacion += "TEL_ENVIO = '" + TEL_ENVIO + "', ";
            }
            else
            {
                MensajeError += "La TEL_ENVIO no puede ser nula\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(COND_ENVIO)))
            {
                sql += "'" + COND_ENVIO + "', ";
                informacion += "COND_ENVIO = '" + COND_ENVIO + "', ";
            }
            else
            {
                MensajeError += "La COND_ENVIO no puede ser nula\n";
                ejecutar = false;
            }
            if (REGISTRO_CONTACTO != 0)
            {
                sql += REGISTRO_CONTACTO + ", ";
                informacion += "REGISTRO_CONTACTO = " + REGISTRO_CONTACTO.ToString() + ",";
            }
            else
            {
                MensajeError = "El campo REGISTRO_CONTACTO no puede ser 0\n";
                ejecutar = false;
            }
            #endregion

            sql += "'" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario.ToString(), tabla.VEN_P_ENVIO_CANDIDATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }
            else
            {
                cantidadRegistrosActualizados = 0;
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean Actualizar(Decimal REGISTRO,
            String DIR_ENVIO,
            String CIU_ENVIO,
            String TEL_ENVIO,
            String COND_ENVIO,
            Decimal REGISTRO_CONTACTO,
            Decimal ID_EMPRESA)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            ManualServicio _manual = new ManualServicio(Empresa, Usuario);

            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion); 
                DataTable tablaDatosAnteriores = _manual.ObtenerInfoRegistroTabla(diccionarioCamposVenPEnvioCandidatos, "VEN_P_ENVIO_CANDIDATOS", "REGISTRO", REGISTRO.ToString(), conexion);

                if (ActualizarConTransaccion(REGISTRO, DIR_ENVIO, CIU_ENVIO, TEL_ENVIO, COND_ENVIO, REGISTRO_CONTACTO, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    DataTable tablaDatosNuevos = _manual.ObtenerInfoRegistroTabla(diccionarioCamposVenPEnvioCandidatos, "VEN_P_ENVIO_CANDIDATOS", "REGISTRO", REGISTRO.ToString(), conexion);

                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.DeterminarYRegistrarModificacionesEnCampos(diccionarioCamposVenPEnvioCandidatos, tablaDatosAnteriores, tablaDatosNuevos, ID_VERSIONAMIENTO, ID_EMPRESA, ManualServicio.ListaSecciones.Seleccion, ManualServicio.AccionesManual.Actualizar, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            ID_VERSIONAMIENTO = 0;
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

        public DataTable ObtenerTodosLosContactosParaEnvioDeCandidatosPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_envio_candidatos_obtenerTodosLosContactosParaEnvioDeCandidatosPorIdEmpresa ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA;
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
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

        public DataTable ObtenerTodosLosContactosParaEnvioDeCandidatosPorIdEmpresa(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_envio_candidatos_obtenerTodosLosContactosParaEnvioDeCandidatosPorIdEmpresa ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA;
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
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


        public DataTable ObtenerEnvioDeCandidatoPorRegistro(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_envio_candidatos_buscarPorRegistro ";

            if (REGISTRO != 0) sql += REGISTRO;
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
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

        public DataTable ObtenerEnvioContactoVigentePorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_envio_candidatos_obtenerVigentePorIdEmpresa ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA;
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
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


        public DataTable ObtenerInformacionEnvioPorRegistro(Decimal registro)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_envio_candidatos_obtenerInformacionCompletaDeCondicion ";

            if (registro != 0)
            {
                sql += registro.ToString();
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0 ó nulo.\n";
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