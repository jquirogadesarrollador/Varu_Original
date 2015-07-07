using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.comercial
{
    public class centroCosto
    {
        #region varialbes

        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
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
        public centroCosto(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable AdicionarCC(Decimal ID_EMPRESA, String NOM_CC, String TIPO_NOM, String CC_EXC_IVA, String ID_CIUDAD, String ID_BANCO)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Conexion conexion = new Conexion(Empresa);

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();

            sql = "usp_ven_cc_empresas_adicionar_V2 ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_CC)))
            {
                sql += "'" + NOM_CC + "', ";
                informacion += "NOM_CC = '" + NOM_CC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_CC no puede ser nulo\n";
                ejecutar = false;
            }

            if (!String.IsNullOrEmpty(TIPO_NOM))
            {
                sql += "'" + TIPO_NOM + "', ";
                informacion += "TIPO_NOM = '" + TIPO_NOM.ToString() + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "TIPO_NOM = Null,";
            }

            if (!(String.IsNullOrEmpty(CC_EXC_IVA)))
            {
                sql += "'" + CC_EXC_IVA + "', ";
                informacion += "CC_EXC_IVA = '" + CC_EXC_IVA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CC_EXC_IVA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!String.IsNullOrEmpty(ID_BANCO))
            {
                sql += "'" + ID_BANCO + "', ";
                informacion += "ID_BANCO = '" + ID_BANCO.ToString() + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "ID_BANCO = Null,";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_CC_EMPRESAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public DataTable ActualizarCC(Decimal ID_CENTRO_C,
            Decimal ID_EMPRESA,
            String NOM_CC,
            String TIPO_NOM,
            String CC_EXC_IVA,
            String ID_CIUDAD,
            String ID_BANCO,
            String ESTADO)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Conexion conexion = new Conexion(Empresa);

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();

            sql = "usp_ven_cc_empresas_actualizar_V2 ";

            #region validaciones
            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C.ToString() + ", ";
                informacion += "ID_CENTRO_C= '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CENTRO_C no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_CC)))
            {
                sql += "'" + NOM_CC + "', ";
                informacion += "NOM_CC = '" + NOM_CC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_CC no puede ser nulo\n";
                ejecutar = false;
            }

            if (!String.IsNullOrEmpty(TIPO_NOM))
            {
                sql += "'" + TIPO_NOM + "', ";
                informacion += "TIPO_NOM = '" + TIPO_NOM.ToString() + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "TIPO_NOM = Null,";
            }

            if (!(String.IsNullOrEmpty(CC_EXC_IVA)))
            {
                sql += "'" + CC_EXC_IVA + "', ";
                informacion += "CC_EXC_IVA = '" + CC_EXC_IVA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CC_EXC_IVA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!String.IsNullOrEmpty(ID_BANCO))
            {
                sql += "'" + ID_BANCO + "', ";
                informacion += "ID_BANCO = '" + ID_BANCO.ToString() + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "ID_BANCO = Null,";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "'";
                informacion += "ESTADO = '" + ESTADO + "'";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
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

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_CC_EMPRESAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerCentrosDeCostoPorIdEmpresaIdCiudad(Decimal ID_EMPRESA, String ID_CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_cc_empresas_obtenerPorIdEmpresaIdCiudad ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA + ", ";
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD))) sql += "'" + ID_CIUDAD.ToString() + "' ";
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo. \n";
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

        public DataTable ObtenerCentrosDeCostoPorIdCentroCosto(Decimal ID_CENTRO_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_cc_empresas_obtenerPorIdCentroC ";

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C;
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
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

        public DataTable ObtenerCentrosDeCostoPorIdCentroCosto(Decimal ID_CENTRO_C, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_cc_empresas_obtenerPorIdCentroC ";

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C;
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
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

        public DataTable ObtenerCentrosDeCostoPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_cc_empresas_obtenerTodosPorIdEmpresa ";

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

        public DataTable ObtenerCentrosDeCostoPorIdEmpresaIdCiudadNombreCC(Decimal ID_EMPRESA, String ID_CIUDAD, String NOM_CC)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_cc_empresas_obtenerPorIdEmpresaIdCiudadNombreCC ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_CC)))
            {
                sql += "'" + NOM_CC + "'";
            }
            else
            {
                MensajeError += "El campo NOM_CC no puede ser nulo. \n";
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