using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.maestras
{
    public class parametro
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;
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
        public parametro(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerParametrosPorTabla(String tabla)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_parametros_buscarPorTabla ";

            if (!(String.IsNullOrEmpty(tabla))) sql += tabla;
            else
            {
                MensajeError = "El campo tabla no puede ser null";
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

        public DataTable ObtenerParametrosPorTablaRestriccionUsuario(String tabla, String usuLog)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_parametros_buscarPorTablaConRestriccionUsuario ";

            if (!(String.IsNullOrEmpty(tabla)))
            {
                sql += "'" + tabla + "', ";
            }
            else
            {
                MensajeError = "El campo tabla no puede ser null";
                ejecutar = false;
            }

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

        public DataTable ObtenerNivEstudiosParametros()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_parametros_obtenerNivEstudios";

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

        public DataTable ObtenerDescripcionParametroPorCodigo(String tabla, String codigo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_parametros_buscarDescripcionPorCodigo ";

            if (!(String.IsNullOrEmpty(tabla)))
            {
                sql += "'" + tabla + "', ";
            }
            else
            {
                MensajeError = "El campo tabla no puede ser null";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(codigo)))
            {
                sql += "'" + codigo + "'";
            }
            else
            {
                MensajeError = "El campo codigo no puede ser null";
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

        public DataTable ObtenerParametrosVariablePorCodigo(String codigo)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_parametros_buscarPorCodigo_variable ";

            if (!(String.IsNullOrEmpty(codigo))) sql += "'" + codigo + "'";
            else
            {
                MensajeError = "El campo codigo no puede ser null";
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
