using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class CrtCalendario
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
        public CrtCalendario(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DateTime ObtenerFechaRequiereCliente(DateTime fechaInicial, Int32 numdias, Int32 proceso)
        {
            tools _tools = new tools();

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            DateTime fechaRequiereCliente = DateTime.Now;

            sql = "usp_crt_calendario_obtenerFechaRequiereCliente ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaInicial) + "', ";
            sql += numdias + ", ";
            sql += proceso;

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

            if (_dataTable.Rows.Count > 0)
            {
                DataRow _dataRow = _dataTable.Rows[0];
                fechaRequiereCliente = Convert.ToDateTime(_dataRow["FECHA_REQUIERE"]);
            }

            return fechaRequiereCliente;
        }

        public DateTime ObtenerFechaDiasHabiles(DateTime fechaInicial, Int32 numdias, Int32 proceso)
        {
            tools _tools = new tools();

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            DateTime fechaRequiereCliente = DateTime.Now;

            sql = "usp_crt_calendario_obtenerFechaDesdeDiasParametrizados ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaInicial) + "', ";
            sql += numdias + ", ";
            sql += proceso;

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

            if (_dataTable.Rows.Count > 0)
            {
                DataRow _dataRow = _dataTable.Rows[0];
                fechaRequiereCliente = Convert.ToDateTime(_dataRow["FECHA_REQUIERE"]);
            }

            return fechaRequiereCliente;
        }

        #endregion metodos
    }
}