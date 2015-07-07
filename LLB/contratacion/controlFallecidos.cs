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
    public class controlFallecidos
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
        public controlFallecidos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region REG_CONTROL_FALLECIDOS
        public DataTable ObtenerTodosFallecidos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_control_fallecidos_obtener_todos_fallecidos";

            #region validaciones
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

        public DataTable ObtenerFallecidosPorNumIdentidad(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_control_fallecidos_obtener_por_num_doc_identidad ";

            #region validaciones

            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser 0\n";
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

        public DataTable ObtenerFallecidosPorNombres(String NOMBRES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_control_fallecidos_obtener_por_nombres ";

            #region validaciones

            if (String.IsNullOrEmpty(NOMBRES) == false)
            {
                sql += "'" + NOMBRES + "'";
                informacion += "NOMBRES = '" + NOMBRES + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser 0\n";
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

        public DataTable ObtenerFallecidosPorApellidos(String APELLIDOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_control_fallecidos_obtener_por_apellidos ";

            #region validaciones

            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "'";
                informacion += "APELLIDOS = '" + APELLIDOS + "'";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser 0\n";
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

        public DataTable ObtenerInfoTrabajadoresPorIdentidad(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_control_fallecidos_obtener_trabajadores_por_identificacion ";

            #region validaciones

            if (String.IsNullOrEmpty(NUM_DOC_IDENTIDAD) == false)
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser 0\n";
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

            DataTable tablaTemp = _dataTable;
            DataRow filaTemp;

            for (int i = (tablaTemp.Rows.Count - 1); i >= 0; i--)
            {
                filaTemp = tablaTemp.Rows[i];
                if ((filaTemp["Estado"].ToString().Trim()) == "Fallecido")
                {
                    _dataTable.Rows.RemoveAt(i);
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerInfoTrabajadoresPorNombres(String NOMBRES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_control_fallecidos_obtener_trabajadores_por_nombres ";

            #region validaciones

            if (String.IsNullOrEmpty(NOMBRES) == false)
            {
                sql += "'" + NOMBRES + "'";
                informacion += "NOMBRES = '" + NOMBRES + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRES no puede ser 0\n";
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

            DataTable tablaTemp = _dataTable;
            DataRow filaTemp;

            for (int i = (tablaTemp.Rows.Count - 1); i >= 0; i--)
            {
                filaTemp = tablaTemp.Rows[i];
                if ((filaTemp["Estado"].ToString().Trim()) == "Fallecido")
                {
                    _dataTable.Rows.RemoveAt(i);
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerInfoTrabajadoresPorApellidos(String APELLIDOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_control_fallecidos_obtener_trabajadores_por_apellidos ";

            #region validaciones

            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "'";
                informacion += "APELLIDOS = '" + APELLIDOS + "'";
            }
            else
            {
                MensajeError += "El campo APELLIDOS no puede ser 0\n";
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

            DataTable tablaTemp = _dataTable;
            DataRow filaTemp;

            for (int i = (tablaTemp.Rows.Count - 1); i >= 0; i--)
            {
                filaTemp = tablaTemp.Rows[i];
                if ((filaTemp["Estado"].ToString().Trim()) == "Fallecido")
                {
                    _dataTable.Rows.RemoveAt(i);
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerInfoFallecimientoPorIdContriolF(Decimal ID_CONTROL_F)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_control_fallecidos_obtener_por_id_control_f ";

            #region validaciones

            if (ID_CONTROL_F != 0)
            {
                sql += ID_CONTROL_F;
                informacion += "ID_CONTROL_F = '" + ID_CONTROL_F + "'";
            }
            else
            {
                MensajeError += "El campo ID_CONTROL_F no puede ser 0\n";
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

        public Decimal AdicionarControlfallecido(Decimal ID_SOLICITUD,
            Decimal REGISTRO_CONTRATO,
            Decimal ID_EMPRESA,
            DateTime FECHA_R,
            DateTime FECHA_FALLECIMIENTO,
            String MOTIVO_FALLECIMIENTO,
            String SEGURO_VIDA,
            Decimal VALOR_SEGURO,
            String OBSERVACIONES,
            Byte[] ARCHIVO_DEFUNCION,
            String ARCHIVO_DEFUNCION_EXTENSION,
            Int32 ARCHIVO_DEFUNCION_TAMANO,
            String ARCHIVO_DEFUNCION_TYPE,
            Byte[] ARCHIVO_AVISO1,
            String ARCHIVO_AVISO1_EXTENSION,
            Int32 ARCHIVO_AVISO1_TAMANO,
            String ARCHIVO_AVISO1_TYPE,
            Byte[] ARCHIVO_AVISO2,
            String ARCHIVO_AVISO2_EXTENSION,
            Int32 ARCHIVO_AVISO2_TAMANO,
            String ARCHIVO_AVISO2_TYPE)
        {
            Decimal ID_CONTROL_F = 0;
            Decimal ID_ARCHIVO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_control_fallecidos_adicionar ";

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

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO + ", ";
                informacion += "REGISTRO_CONTRATO = '" + REGISTRO_CONTRATO + "', ";
            }
            else
            {
                MensajeError = "El campo REGISTRO_CONTRATO no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
            informacion += "FECHA_R = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FALLECIMIENTO) + "', ";
            informacion += "FECHA_FALLECIMIENTO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FALLECIMIENTO) + "', ";

            if (String.IsNullOrEmpty(MOTIVO_FALLECIMIENTO) == false)
            {
                sql += "'" + MOTIVO_FALLECIMIENTO + "', ";
                informacion += "MOTIVO_FALLECIMIENTO = '" + MOTIVO_FALLECIMIENTO + "', ";
            }
            else
            {
                MensajeError = "El campo MOTIVO_FALLECIMIENTO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(SEGURO_VIDA) == false)
            {
                sql += "'" + SEGURO_VIDA + "', ";
                informacion += "SEGURO_VIDA = '" + SEGURO_VIDA + "', ";
            }
            else
            {
                MensajeError = "El campo SEGURO_VIDA no puede ser vacio.";
                ejecutar = false;
            }

            sql += VALOR_SEGURO + ", ";
            informacion += "VALOR_SEGURO = '" + VALOR_SEGURO + "', ";

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
                    ID_CONTROL_F = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_CONTROL_F <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar ingresar el fallecimiento.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        if (ARCHIVO_DEFUNCION != null)
                        {
                            ID_ARCHIVO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionarArchivoFallecimiento(ID_CONTROL_F, FECHA_R, "DEFUNCION", ARCHIVO_DEFUNCION, ARCHIVO_DEFUNCION_EXTENSION, ARCHIVO_DEFUNCION_TAMANO, ARCHIVO_DEFUNCION_TYPE, Usuario));

                            if (ID_ARCHIVO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Al intentar ingresar el archivo de fallecimineto.";
                                ejecutadoCorrectamente = false;
                            }
                        }

                        if (ejecutadoCorrectamente == true)
                        {
                            if (ARCHIVO_AVISO1 != null)
                            {
                                ID_ARCHIVO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionarArchivoFallecimiento(ID_CONTROL_F, FECHA_R, "AVISO1", ARCHIVO_AVISO1, ARCHIVO_AVISO1_EXTENSION, ARCHIVO_AVISO1_TAMANO, ARCHIVO_AVISO1_TYPE, Usuario));

                                if (ID_ARCHIVO <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "ERROR: Al intentar ingresar el archivo del aviso periodico 1.";
                                    ejecutadoCorrectamente = false;
                                }
                            }
                        }

                        if (ejecutadoCorrectamente == true)
                        {
                            if (ARCHIVO_AVISO2 != null)
                            {
                                ID_ARCHIVO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionarArchivoFallecimiento(ID_CONTROL_F, FECHA_R, "AVISO2", ARCHIVO_AVISO2, ARCHIVO_AVISO2_EXTENSION, ARCHIVO_AVISO2_TAMANO, ARCHIVO_AVISO2_TYPE, Usuario));

                                if (ID_ARCHIVO <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    MensajeError = "ERROR: Al intentar ingresar el archivo del aviso periodico 2.";
                                    ejecutadoCorrectamente = false;
                                }
                            }
                        }

                        if (ejecutadoCorrectamente == true)
                        {
                            auditoria _auditoria = new auditoria(Empresa);
                            if (!(_auditoria.Adicionar(Usuario, tabla.REG_CONTROL_FALLECIDOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de fallecidos.";
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
                return ID_CONTROL_F;
            }
            else
            {
                return 0;
            }
        }

        public Boolean ActualizarRegistroFallecimiento(Decimal ID_CONTROL_F,
            DateTime FECHA_FALLECIMIENTO,
            String MOTIVO_FALLECIMIENTO,
            String SEGURO_VIDA,
            Decimal VALOR_SEGURO,
            String OBSERVACIONES,
            Byte[] ARCHIVO_DEFUNCION,
            String ARCHIVO_DEFUNCION_EXTENSION,
            Int32 ARCHIVO_DEFUNCION_TAMANO,
            String ARCHIVO_DEFUNCION_TYPE,
            Byte[] ARCHIVO_AVISO1,
            String ARCHIVO_AVISO1_EXTENSION,
            Int32 ARCHIVO_AVISO1_TAMANO,
            String ARCHIVO_AVISO1_TYPE,
            Byte[] ARCHIVO_AVISO2,
            String ARCHIVO_AVISO2_EXTENSION,
            Int32 ARCHIVO_AVISO2_TAMANO,
            String ARCHIVO_AVISO2_TYPE)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_ARCHIVO = 0;

            tools _tools = new tools();

            sql = "usp_reg_control_fallecidos_actualizar ";

            #region validaciones
            if (ID_CONTROL_F != 0)
            {
                sql += ID_CONTROL_F + ", ";
                informacion += "ID_CONTROL_F = '" + ID_CONTROL_F + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CONTROL_F no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FALLECIMIENTO) + "', ";
            informacion += "FECHA_FALLECIMIENTO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FALLECIMIENTO) + "', ";

            if (String.IsNullOrEmpty(MOTIVO_FALLECIMIENTO) == false)
            {
                sql += "'" + MOTIVO_FALLECIMIENTO + "', ";
                informacion += "MOTIVO_FALLECIMIENTO = '" + MOTIVO_FALLECIMIENTO + "', ";
            }
            else
            {
                MensajeError = "El campo MOTIVO_FALLECIMIENTO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(SEGURO_VIDA) == false)
            {
                sql += "'" + SEGURO_VIDA + "', ";
                informacion += "SEGURO_VIDA = '" + SEGURO_VIDA + "', ";
            }
            else
            {
                MensajeError = "El campo SEGURO_VIDA no puede ser vacio.";
                ejecutar = false;
            }

            sql += VALOR_SEGURO + ", ";
            informacion += "VALOR_SEGURO = '" + VALOR_SEGURO + "', ";

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

            if (ejecutar == true)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    if (conexion.ExecuteNonQuery(sql) == 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "No se pudo realizar la actualización de datos del registro de fallecimiento.";
                        return false;
                    }
                    else
                    {
                        if (ARCHIVO_DEFUNCION != null)
                        {
                            ID_ARCHIVO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionarArchivoFallecimiento(ID_CONTROL_F, DateTime.Now, "DEFUNCION", ARCHIVO_DEFUNCION, ARCHIVO_DEFUNCION_EXTENSION, ARCHIVO_DEFUNCION_TAMANO, ARCHIVO_DEFUNCION_TYPE, Usuario));

                            if (ID_ARCHIVO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Al intentar ingresar el archivo de fallecimineto.";
                                return false;
                            }
                        }

                        if (ARCHIVO_AVISO1 != null)
                        {
                            ID_ARCHIVO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionarArchivoFallecimiento(ID_CONTROL_F, DateTime.Now, "AVISO1", ARCHIVO_AVISO1, ARCHIVO_AVISO1_EXTENSION, ARCHIVO_AVISO1_TAMANO, ARCHIVO_AVISO1_TYPE, Usuario));

                            if (ID_ARCHIVO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Al intentar ingresar el archivo del aviso periodico 1.";
                                return false;
                            }
                        }

                        if (ARCHIVO_AVISO2 != null)
                        {
                            ID_ARCHIVO = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionarArchivoFallecimiento(ID_CONTROL_F, DateTime.Now, "AVISO2", ARCHIVO_AVISO2, ARCHIVO_AVISO2_EXTENSION, ARCHIVO_AVISO2_TAMANO, ARCHIVO_AVISO2_TYPE, Usuario));

                            if (ID_ARCHIVO <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                MensajeError = "ERROR: Al intentar ingresar el archivo del aviso periodico 2.";
                                return false;
                            }
                        }

                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_CONTROL_FALLECIDOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de fallecidos.";
                            return false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = ex.Message;
                    return false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                return false;
            }
        }

        public Boolean cerrarFallecimiento(Decimal ID_CONTROL_F,
            String TIPO_ENTREGA,
            DateTime FECHA_ENTREGA_PRESTACIONES,
            String DETALLES_ENTREGA,
            String ESTADO,
            Byte[] ARCHIVO_CIERRE,
            String ARCHIVO_CIERRE_EXTENSION,
            int ARCHIVO_CIERRE_TAMANO,
            String ARCHIVO_CIERRE_TYPE)
        {
            Boolean verificador = true;

            Decimal ID_ARCHIVO_CIERRE = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarRegControlFallecimientosCerrar(ID_CONTROL_F, TIPO_ENTREGA, FECHA_ENTREGA_PRESTACIONES, DETALLES_ENTREGA, ESTADO, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    verificador = false;
                }
                else
                {
                    if (ARCHIVO_CIERRE != null)
                    {
                        ID_ARCHIVO_CIERRE = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionarArchivoFallecimiento(ID_CONTROL_F, DateTime.Now, "CIERRE", ARCHIVO_CIERRE, ARCHIVO_CIERRE_EXTENSION, ARCHIVO_CIERRE_TAMANO, ARCHIVO_CIERRE_TYPE, Usuario));

                        if (ID_ARCHIVO_CIERRE <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar el archivo de cierre de fallecimineto.";
                            verificador = false;
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
                conexion.DeshacerTransaccion();
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return verificador;
        }


        public Boolean ActualizarRegControlFallecimientosCerrar(Decimal ID_CONTROL_F,
            String TIPO_ENTREGA,
            DateTime FECHA_ENTREGA_PRESTACIONES,
            String DETALLES_ENTREGA,
            String ESTADO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_reg_control_fallecidos_cerrar_fallecimiento ";

            #region validaciones
            if (ID_CONTROL_F != 0)
            {
                sql += ID_CONTROL_F + ", ";
                informacion += "ID_CONTROL_F = '" + ID_CONTROL_F + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CONTROL_F no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_ENTREGA) == false)
            {
                sql += "'" + TIPO_ENTREGA + "', ";
                informacion += "TIPO_ENTREGA = '" + TIPO_ENTREGA + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_ENTREGA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ENTREGA_PRESTACIONES) + "', ";
            informacion += "FECHA_ENTREGA_PRESTACIONES = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ENTREGA_PRESTACIONES) + "', ";

            if (String.IsNullOrEmpty(DETALLES_ENTREGA) == false)
            {
                sql += "'" + DETALLES_ENTREGA + "', ";
                informacion += "DETALLES_ENTREGA = '" + DETALLES_ENTREGA + "', ";
            }
            else
            {
                MensajeError = "El campo DETALLES_ENTREGA no puede ser vacio.";
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

            if (ejecutar == true)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) == 0)
                    {
                        MensajeError = "No se pudo realizar el cierrre del fallecimiento.";
                        return false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_CONTROL_FALLECIDOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el cierre del fallecimiento.";
                            return false;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MensajeError = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion REG_CONTROL_FALLECIDOS

        #region REG_ARCHIVOS_CONTROL_FALLECIDOS
        public DataTable ObtenerInfoArchivoControlFallecidos(Decimal ID_CONTROL_F, String TIPO_DOCUMENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_archivos_control_fallecidos_obtener_archivo ";

            #region validaciones

            if (ID_CONTROL_F != 0)
            {
                sql += ID_CONTROL_F + ", ";
                informacion += "ID_CONTROL_F = '" + ID_CONTROL_F + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONTROL_F no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_DOCUMENTO) == false)
            {
                sql += "'" + TIPO_DOCUMENTO + "'";
                informacion += "TIPO_DOCUMENTO = '" + TIPO_DOCUMENTO + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_DOCUMENTO no puede ser 0\n";
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



        #endregion REG_ARCHIVOS_CONTROL_FALLECIDOS



        #endregion metodos
    }
}