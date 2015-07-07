using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.contratacion
{
    public class bancosPorEmpresa
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
        public bancosPorEmpresa(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerconBancoEmpresaPorCiudadEmpresa(String ID_CIUDAD, int ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_BANCO_EMPRESA_OBTENER_BANCOS_POR_CIUDAD ";

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.CON_BANCO_EMPRESA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerconBancoEmpresaPorEmpresa(Decimal ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_BANCO_EMPRESA_OBTENER_BANCOS_POR_EMPRESA ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
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

        public Boolean desactivarBancoEmpresa(Decimal REGISTRO_CON_BANCO_EMPRESA, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_banco_empresa_actualizar_estado ";

            if (REGISTRO_CON_BANCO_EMPRESA != 0)
            {
                sql += REGISTRO_CON_BANCO_EMPRESA + ", ";
                informacion += "REGISTRO = '" + REGISTRO_CON_BANCO_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'N', ";
            informacion += "ACTIVO = 'N', ";

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_BANCO_EMPRESA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        private Decimal adicionarBancoEmpresaCiudad(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal REGISTRO_BANCO, Conexion conexion)
        {
            String registro = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_banco_empresa_adicionar ";

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

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DOC_TRAB no puede ser nulo. \n";
                ejecutar = false;
            }

            if (REGISTRO_BANCO != 0)
            {
                sql += REGISTRO_BANCO + ", ";
                informacion += "REGISTRO_BANCO = '" + REGISTRO_BANCO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo REGISTRO_BANCO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    registro = conexion.ExecuteScalar(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_BANCO_EMPRESA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            return Convert.ToDecimal(registro);
        }

        public Decimal AsignarBancosACiudad(Decimal ID_EMPRESA, String ID_CIUDAD, List<bancos> listaBancos)
        {
            Boolean ejecutar = true;

            if (ID_EMPRESA == 0)
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD))
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();

                DataTable tablaBancosActuales = ObtenerconBancoEmpresaPorCiudadEmpresa(ID_CIUDAD, Convert.ToInt32(ID_EMPRESA));

                foreach (DataRow fila in tablaBancosActuales.Rows)
                {
                    ejecutar = true;
                    foreach (bancos filaDeLaLista in listaBancos)
                    {
                        if (filaDeLaLista.REGISTRO_CON_REG_BANCOS_EMPRESA == Convert.ToDecimal(fila["REGISTRO_CON_BANCO_EMPRESA"]))
                        {
                            ejecutar = false;
                            break;
                        }
                    }

                    if (ejecutar == true)
                    {
                        if (desactivarBancoEmpresa(Convert.ToDecimal(fila["REGISTRO_CON_BANCO_EMPRESA"]), conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            return 0;
                        }
                    }
                }

                foreach (bancos filaDeLaLista in listaBancos)
                {
                    if (filaDeLaLista.REGISTRO_CON_REG_BANCOS_EMPRESA == 0)
                    {
                        if (adicionarBancoEmpresaCiudad(ID_EMPRESA, ID_CIUDAD, filaDeLaLista.REGISTRO_BANCO, conexion) == 0)
                        {
                            conexion.DeshacerTransaccion();
                            return 0;
                        }
                    }
                }

                conexion.AceptarTransaccion();
                return 1;
            }
            else
            {
                return 0;
            }
        }
        #endregion
    }
}
