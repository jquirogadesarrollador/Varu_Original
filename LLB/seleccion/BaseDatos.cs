using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.operaciones;

namespace Brainsbits.LLB.seleccion
{
    public class BaseDatos
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
        public BaseDatos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        public BaseDatos()
        {

        }
        #endregion constructores

        #region metodos


        public DataTable ObtenerReporteBaseDatos(String ID_OCUPACION,
            String NIV_EDUCACION,
            String PROFESION,
            String AREA_INTERES_LABORAL,
            String EXPERIENCIA,
            String EDAD_DESDE,
            String EDAD_HASTA,
            String ID_CIUDAD,
            String NOMBRES,
            String APELLIDOS,
            String BARRIO,
            String ASPIRACION_DESDE,
            String ASPIRACION_HASTA,
            String FECHA_ACTUALIZACION_DESDE,
            String FECHA_ACTUALIZACION_HASTA,
            String PALABRA_CLAVE,
            String TIPO_REPORTE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_base_de_datos_obtener_por_filtros ";

            if (String.IsNullOrEmpty(ID_OCUPACION) == false)
            {
                sql += ID_OCUPACION + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(NIV_EDUCACION) == false)
            {
                sql += NIV_EDUCACION + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(PROFESION) == false)
            {
                sql += "'" + PROFESION + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(AREA_INTERES_LABORAL) == false)
            {
                sql += AREA_INTERES_LABORAL + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(EXPERIENCIA) == false)
            {
                sql += "'" + EXPERIENCIA + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(EDAD_DESDE) == false)
            {
                sql += EDAD_DESDE + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(EDAD_HASTA) == false)
            {
                sql += EDAD_HASTA + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(NOMBRES) == false)
            {
                sql += "'" + NOMBRES + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(APELLIDOS) == false)
            {
                sql += "'" + APELLIDOS + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(BARRIO) == false)
            {
                sql += "'" + BARRIO + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(ASPIRACION_DESDE) == false)
            {
                sql += ASPIRACION_DESDE + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(ASPIRACION_HASTA) == false)
            {
                sql += ASPIRACION_HASTA + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(FECHA_ACTUALIZACION_DESDE) == false)
            {
                sql += "'" + FECHA_ACTUALIZACION_DESDE + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(FECHA_ACTUALIZACION_HASTA) == false)
            {
                sql += "'" + FECHA_ACTUALIZACION_HASTA + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(PALABRA_CLAVE) == false)
            {
                sql += "'" + PALABRA_CLAVE + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(TIPO_REPORTE) == false)
            {
                sql += "'" + TIPO_REPORTE + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El Campo TIPO DE REPORTE no puede ser nulo.";
            }

            if (ejecutar)
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
