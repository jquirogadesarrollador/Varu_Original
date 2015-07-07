using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.nomina
{
    public class MigracionTemp
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
        public MigracionTemp(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos


        public DataTable ObtenerTotalesV3PeriodoCodEmpresa(String codEmpresa, Int32 periodo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_temp_migracion_totales_periodo_empresa '" + codEmpresa + "', " + periodo.ToString();

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

        public DataTable MigrarPeriodoMemoV3aWeb(String codEmpresa, Int32 periodo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_temp_migracion_final_lps '" + codEmpresa + "', " + periodo.ToString();

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

        public DataTable ListarCentrosDeCosto()
        {
            Conexion conexion = new Conexion(Empresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select distinct NOM_CC from VEN_CC_EMPRESAS";
            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarCentrosDeCostoPorEmpresa(string @empresa)
        {
            Conexion conexion = new Conexion(Empresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select distinct NOM_CC from VEN_CC_EMPRESAS WHERE ID_EMPRESA = (SELECT distinct ID_EMPRESA FROM VEN_EMPRESAS WHERE COD_EMPRESA = '" + @empresa + "')";
            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }
        public DataTable MigrarMemorando_Nomina(String @NOM_CC, String @PERIODO, String @EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            if (Convert.ToInt32(@PERIODO) > 0)
            {
                sql = "[dbo].[NOM_MIGRACION_NOMINA_WEB_V3] '" + @NOM_CC + "','" + @PERIODO + "', '" + @EMPRESA + "'";
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
            else
            {
                sql = "NOM_MIGRACION_MEMORANDOS_WEB_V3'" + @NOM_CC + "','" + @PERIODO + "', '" + @EMPRESA + "'";
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
        }
        #endregion metodos
    }
}
