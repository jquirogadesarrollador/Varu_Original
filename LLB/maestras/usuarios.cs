using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using Brainsbits.LDA;
namespace Brainsbits.LLB.maestras
{
    public class usuarios
    {
        #region variables
        public enum Empresas
        {
            Sertempo = 1,
            EYS = 3
        }
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
        public usuarios(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public String retornarIdUsuario(String UNIDAD_NEGOCIO, Decimal ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            String idEmpleado = "";

            sql = "usp_retornar_id_usuario ";

            #region validaciones
            sql += "'" + _usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(UNIDAD_NEGOCIO)))
            {
                sql += "'" + UNIDAD_NEGOCIO + "', ";
            }
            else
            {
                MensajeError = "El campo UNIDAD_NEGOCIO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + ID_EMPRESA.ToString() + "'";

            #endregion

            if (ejecutar)
            {

                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idEmpleado = conexion.ExecuteScalar(sql);
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

            return idEmpleado;

        }
        #endregion
    }
}
