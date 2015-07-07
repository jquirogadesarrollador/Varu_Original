using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;

using System.IO;

namespace Brainsbits.LLB.programasRseGlobal
{
    public class ActividadRseGlobal
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
        public ActividadRseGlobal()
        {

        }
        public ActividadRseGlobal(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerActividadesPorArea(Programa.Areas area)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_actividades_obtenerPorArea ";

            sql += "'" + area.ToString() + "'";

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

        public DataTable ObtenerActividadPorId(Decimal ID_ACTIVIDAD, Programa.Areas AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_actividades_obtenerPorId ";

            if (ID_ACTIVIDAD != 0)
            {
                sql += ID_ACTIVIDAD + ", ";
            }
            else
            {
                ejecutar = false;
            }

            sql += "'" + AREA.ToString() + "'";

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

        public DataTable ObtenerActividadesPorNombre(String NOMBRE, Programa.Areas AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_actividades_obtenerPorNombre ";

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
            }
            else
            {
                ejecutar = false;
            }

            sql += "'" + AREA.ToString() + "'";

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

        public DataTable ObtenerActividadesPorTipo(String TIPO, Programa.Areas AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_actividades_obtenerPorTipo ";

            if (String.IsNullOrEmpty(TIPO) == false)
            {
                sql += "'" + TIPO + "', ";
            }
            else
            {
                ejecutar = false;
            }

            sql += "'" + AREA.ToString() + "'";

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

        public DataTable ObtenerActividadesPorSector(String SECTOR, Programa.Areas AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_actividades_obtenerPorSector ";

            if (String.IsNullOrEmpty(SECTOR) == false)
            {
                sql += "'" + SECTOR + "', ";
            }
            else
            {
                ejecutar = false;
            }

            sql += "'" + AREA.ToString() + "'";

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

        public Decimal Adicionar(String NOMBRE,
            String DESCRIPCION,
            String TIPO,
            String SECTOR,
            Programa.Areas ID_AREA)
        {
            String sql = null;
            Decimal idRecuperado = 0;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_prog_actividades_adicionar ";

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }


            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO) == false)
            {
                sql += "'" + TIPO + "', ";
                informacion += "TIPO = '" + TIPO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(SECTOR) == false)
            {
                sql += "'" + SECTOR + "', ";
                informacion += "SECTOR = '" + SECTOR + "', ";
            }
            else
            {
                MensajeError += "El campo SECTOR no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + ID_AREA.ToString() + "', ";
            informacion += "AREA = '" + ID_AREA.ToString() + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecuperado = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_ACTIVIDADES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    idRecuperado = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return idRecuperado;
        }

        public Boolean Actualizar(Decimal ID_ACTIVIDAD,
            String NOMBRE,
            String DESCRIPCION,
            String TIPO,
            String SECTOR,
            Boolean ACTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_actividades_actualizar ";

            #region validaciones

            if (ID_ACTIVIDAD != 0)
            {
                sql += ID_ACTIVIDAD + ", ";
                informacion += "ID_ACTIVIDAD = '" + ID_ACTIVIDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ACTIVIDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError = "El campo DESCRIPCION no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO) == false)
            {
                sql += "'" + TIPO + "', ";
                informacion += "TIPO = '" + TIPO + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(SECTOR) == false)
            {
                sql += "'" + SECTOR + "', ";
                informacion += "SECTOR = '" + SECTOR + "', ";
            }
            else
            {
                MensajeError = "El campo SECTOR no puede ser vacio.";
                ejecutar = false;
            }

            if (ACTIVO == true)
            {
                sql += "'True', ";
            }
            else
            {
                sql += "'False', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_ACTIVIDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            return ejecutadoCorrectamente;
        }
        #endregion metodos

    }
}
