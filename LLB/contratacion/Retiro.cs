using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;


namespace Brainsbits.LLB.contratacion
{
    public class Retiro
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
        public Retiro(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable Consultar(string dato)
        {
            Conexion conexion = new Conexion(Empresa);
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_contratacion_retiros_consultar '" + dato + "'";

            try
            {
                dataTable = conexion.ExecuteReader(sql).Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar información del trabajador en retiros. " + e.Message);

            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ObtenerPorIdEmpleado(decimal idEmpleado)
        {
            Conexion conexion = new Conexion(Empresa);
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_contratacion_retiros_obtenerPorIdEmpleado '" + idEmpleado + "'";

            try
            {
                dataTable = conexion.ExecuteReader(sql).Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar información del retiro. " + e.Message);

            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public bool Actualizar(decimal idEmpleado, DateTime fechaRetiro, string notas, string estado, string carpeta)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            bool actualizado = true;

            sql = "usp_contratacion_retiros_actualizar_v2 " + idEmpleado;

            if (!string.IsNullOrEmpty(notas)) sql += ", '" + notas + "'";
            else sql += ", Null";

            if (!string.IsNullOrEmpty(estado)) sql += ", '" + estado + "'";
            else sql += ", Null";

            if (!string.IsNullOrEmpty(carpeta)) sql += ", '" + carpeta + "'";
            else sql += ", Null";

            if (new DateTime() != fechaRetiro) sql += ", '" + fechaRetiro.ToShortDateString() + "'";
            else sql += ",null";

            sql += ", '" + Usuario + "'";

            try
            {
                conexion.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                actualizado = false;
                throw new Exception("Error al consultar información del retiro. " + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return actualizado;
        }


        public DataTable ReversarRetiroTemporal(decimal idEmpleado)
        {
            Conexion conexion = new Conexion(Empresa);
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_contratacion_reversar_retiro_temporal '" + idEmpleado + "', '" + Usuario + "'";

            try
            {
                dataTable = conexion.ExecuteReader(sql).Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }
        #endregion metodos
    }
}
