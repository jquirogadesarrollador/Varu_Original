using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.seguridad
{
    public class auditoria
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
        public auditoria(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        #endregion constructores

        #region metodos
        public bool Adicionar(String USU_LOG, String TABLA, String ACCION, String SENTENCIA, String INFORMACION, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_auditoria_adicionar ";

            if (!(String.IsNullOrEmpty(USU_LOG))) sql += "'" + USU_LOG + "', ";
            else
            {
                MensajError += "El campo USUARIO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TABLA))) sql += "'" + TABLA + "', ";
            else
            {
                MensajError += "El campo TABLA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SENTENCIA))) sql += "'" + SENTENCIA.ToString().Replace("'", "''") + "',";
            else
            {
                MensajError += "El campo SENTENCIA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ACCION))) sql += "'" + ACCION + "',";
            else
            {
                MensajError += "El campo ACCION no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(INFORMACION))) sql += "'" + INFORMACION.ToString().Replace("'", "''") + "'";
            else
            {
                MensajError += "El campo INFORMACION no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajError = e.Message;
                }
            }

            if (String.IsNullOrEmpty(MensajError)) return true;
            else return false;

        }

        public bool Adicionar(String USU_LOG, String TABLA, String ACCION, String SENTENCIA, String INFORMACION, String INFORMACION_SESION, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_auditoria_adicionar ";

            if (!(String.IsNullOrEmpty(USU_LOG))) sql += "'" + USU_LOG + "', ";
            else
            {
                MensajError += "El campo USUARIO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TABLA))) sql += "'" + TABLA + "', ";
            else
            {
                MensajError += "El campo TABLA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SENTENCIA))) sql += "'" + SENTENCIA.ToString().Replace("'", "''") + "',";
            else
            {
                MensajError += "El campo SENTENCIA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ACCION))) sql += "'" + ACCION + "',";
            else
            {
                MensajError += "El campo ACCION no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(INFORMACION))) sql += "'" + INFORMACION.ToString().Replace("'", "''") + "'";
            else
            {
                MensajError += "El campo INFORMACION no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(INFORMACION))) sql += "'" + INFORMACION_SESION.ToString().Replace("'", "''") + "'";
            else
            {
                MensajError += "El campo INFORMACION_SESION no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajError = e.Message;
                }
            }

            if (String.IsNullOrEmpty(MensajError)) return true;
            else return false;
        }

        public DataTable ObtenerReporteTrazabilidad(Decimal ID_EMPLEADO, DateTime FECHA_INICIAL, DateTime FECHA_FINAL, DateTime FECHA_HORA, String HORA_INICIAL, String HORA_FINAL, String TABLA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_auditoria_buscar ";

            if (ID_EMPLEADO == 0)
            {
                sql += "null, ";
            }
            else
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }

            if (FECHA_INICIAL == new DateTime())
            {
                sql += "null, ";
            }
            else
            {
                sql += "'" + FECHA_INICIAL.ToShortDateString() + "', ";
            }

            if (FECHA_FINAL == new DateTime())
            {
                sql += "null, ";
            }
            else
            {
                sql += "'" + FECHA_FINAL.ToShortDateString() + "', ";
            }

            if (FECHA_HORA == new DateTime())
            {
                sql += "null, ";
            }
            else
            {
                sql += "'" + FECHA_HORA.ToShortDateString() + "', ";
            }

            if (HORA_INICIAL == null)
            {
                sql += "null, ";
            }
            else
            {
                sql += "'" + HORA_INICIAL + "', ";
            }

            if (HORA_FINAL == null)
            {
                sql += "null, ";
            }
            else
            {
                sql += "'" + HORA_FINAL + "', ";
            }

            if (TABLA == null)
            {
                sql += "null";
            }
            else
            {
                sql += "'" + TABLA + "'";
            }

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
            return _dataTable;
        }
        #endregion metodos
    }
}
