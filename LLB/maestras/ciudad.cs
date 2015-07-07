using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.maestras
{
    public class ciudad
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
        public ciudad(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerCiudadesPorIdDepartamento(String idDepartamento = null)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ciudad_buscar_por_id_departamento ";
            if (!(String.IsNullOrEmpty(idDepartamento))) sql += "'" + idDepartamento + "'";
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

        public DataTable ObtenerIdRegionalIdDepartamentoConIdCiudad(String idCiudad = null)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ciudad_buscar_id_departamento_id_regional ";
            if (!(String.IsNullOrEmpty(idCiudad)))
            {
                sql += "'" + idCiudad + "'";

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
                MensajError = "ADVERTENCIA: El campo ID_CIUDAD no puede ser vacio. \n";
            }

            return _dataTable;
        }

        public DataTable ObtenerIdDepartamentoConIdCiudad(String idCiudad = null)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ciudad_buscar_id_departamento ";
            if (!(String.IsNullOrEmpty(idCiudad)))
            {
                sql += "'" + idCiudad + "'";

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
                MensajError = "ADVERTENCIA: El campo ID_CIUDAD no puede ser vacio. \n";
            }

            return _dataTable;
        }

        public DataTable ObtenerCiudadPorIdCiudad(String idCiudad)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ciudad_buscar_por_id_ciudad ";
            if (!(String.IsNullOrEmpty(idCiudad)))
            {
                sql += "'" + idCiudad + "'";
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
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                MensajError = "El ID_CIUDAD no puede ser vacio. \n";
            }

            return _dataTable;
        }

        public DataTable ObtenerCiudadPorIdCiudad(String idCiudad, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ciudad_buscar_por_id_ciudad ";
            if (!(String.IsNullOrEmpty(idCiudad)))
            {
                sql += "'" + idCiudad + "'";
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
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
                MensajError = "El ID_CIUDAD no puede ser vacio. \n";
            }

            return _dataTable;
        }

        public DataTable ObtenerNombreCiudadPorIdCiudad(String idCiudad)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ciudad_obtener_nombre_por_id_ciudad ";
            if (!(String.IsNullOrEmpty(idCiudad)))
            {
                sql += "'" + idCiudad + "'";
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
                MensajError = "El ID_CIUDAD no puede ser vacio. \n";
            }

            return _dataTable;
        }

        public DataTable ObtenerTodasLasCiudades()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ciudad_buscar_todas ";
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

        public DataTable ObtenerTodasLasCiudadesAsociadasARegional()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ciudad_buscar_todas_con_asociacion_a_regionales";
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


        public DataTable ObtenerCiudadesPorIdDepartamentoEIdRegional(String idRegional = null, String idDepartamento = null)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ciudad_buscar_por_id_departamento_e_id_regional ";
            if (!(String.IsNullOrEmpty(idRegional)))
            {
                sql += "'" + idRegional + "', ";
            }
            else
            {
                ejecutar = false;
                MensajError += "El campo ID_REGIONAL no puede ser vacio. \n";
            }

            if (!(String.IsNullOrEmpty(idDepartamento)))
            {
                sql += "'" + idDepartamento + "'";
            }
            else
            {
                ejecutar = false;
                MensajError += "El campo ID_DEPARTAMENTO no puede ser vacio. \n";
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
                    MensajError = e.Message;
                }
            }
            return _dataTable;
        }

        public DataTable ObtenerCiudadesPorIdDepartamentoEIdRegional_ConRestricciones(String idRegional, String idDepartamento, Decimal ID_EMPRESA, String USU_LOG)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ciudad_buscar_por_id_departamento_e_id_regional_con_restriccion '" + idRegional + "', '" + idDepartamento + "', " + ID_EMPRESA + ", '" + USU_LOG + "'";

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
                    MensajError = e.Message;
                }
            }
            return _dataTable;
        }

        public DataTable ObtenerNombreCiudadPorIdRegional(String ID_REGIONAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ciudad_obtenerPorIdRegional ";
            if (!(String.IsNullOrEmpty(ID_REGIONAL)))
            {
                sql += "'" + ID_REGIONAL + "'";
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
                MensajError = "El ID_REGIONAL no puede ser vacio. \n";
            }

            return _dataTable;
        }

        public DataTable ObtenerCiudadesPorIdDepartamentoSinimportarRegional_ConRestricciones(String idDepartamento, Decimal ID_EMPRESA, String USU_LOG)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ciudad_buscar_por_id_departamento_con_restriccion__sin_importar_regional '" + idDepartamento + "', " + ID_EMPRESA + ", '" + USU_LOG + "'";

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
                    MensajError = e.Message;
                }
            }
            return _dataTable;
        }



        #endregion metodos
    }
}