using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.maestras
{
    public class regional
    {
        #region variables
        public enum Empresas
        {
            Sertempo = 1,
            EYS = 3
        }
        String _empresa = null;
        String _mensaje_error = null;
        #endregion variables

        #region propiedades
        public String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }
        #endregion propiedades

        #region constructores
        public regional(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerTodasLasRegionales()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_regional_buscar_todas ";
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
            }
            catch (Exception e)
            {
                MensajError = e.Message;
            }

            return _dataTable;
        }


        public DataTable ObtenerTodasLasRegionalesConRestriccion(Decimal ID_EMPRESA, String USU_LOG)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_regional_buscar_todas_con_restriccion " + ID_EMPRESA.ToString() + ", '" + USU_LOG + "'";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
            }
            catch (Exception e)
            {
                MensajError = e.Message;
            }

            return _dataTable;
        }




        public DataTable ObtenerRegionalPorId(String idRegional)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_regional_obtener_por_id ";
            if (!(String.IsNullOrEmpty(idRegional)))
            {
                sql += "'" + idRegional + "'";
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajError = e.Message;
                }
            }
            else
            {
                MensajError = "ADVERTENCIA: El campo ID_regional no puede ser vacio. \n";
            }
            return _dataTable;
        }

        #endregion metodos
    }
}