using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class actividad
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
        public actividad(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerActividadesPorIdClase(String idClase = null)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_actividad_buscar_por_id_clase ";
            if (!(String.IsNullOrEmpty(idClase))) sql += "'" + idClase + "'";
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

        public DataTable ObtenerActividPorIdActividad(String idActividad = null)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_actividad_buscar_por_id_actividad ";
            if (!(String.IsNullOrEmpty(idActividad)))
            {
                sql += "'" + idActividad + "'";

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
                MensajError = "ADVERTENCIA: El cmpo ID_ACTIVIDAD no puede ser vacio. \n";
            }


            return _dataTable;
        }

        public DataTable ObtenerEmpresaPorIdActividad(String ID_ACTIVIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empresas_asociadas_a_actividad ";

            #region VALIDACIONES
            if (String.IsNullOrEmpty(ID_ACTIVIDAD) == false)
            {
                sql += "'" + ID_ACTIVIDAD + "'";
            }
            else
            {
                MensajError = "ERROR: El campo ID_DE LA ACTIVIDAD no puede ser vacio.";
                ejecutar = false;
            }
            #endregion VALIDACIONES

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


        public Boolean ActualizarActividad(String ID_ACTIVIDAD, String NOMBRE, String USU_MOD)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_actividad_actualizar ";


            if (String.IsNullOrEmpty(ID_ACTIVIDAD) == false)
            {
                sql += "'" + ID_ACTIVIDAD + "', ";
                informacion += "ID_ACTIVIDAD = '" + ID_ACTIVIDAD + "', ";
            }
            else
            {
                MensajError = "El campo ID_DE LA ACTIVIDAD no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + NOMBRE + "'";
            }
            else
            {
                MensajError = "El campo NOMBRE DE LA CLASE no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_MOD, tabla.VEN_ACTIVIDAD, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean EliminarActividad(String ID_ACTIVIDAD, String USU_MOD)
        {
            Int32 cantidadRegistrosBorrados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_actividad_eliminar ";


            if (String.IsNullOrEmpty(ID_ACTIVIDAD) == false)
            {
                sql += "'" + ID_ACTIVIDAD + "'";
                informacion += "ID_ACTIVIDAD = '" + ID_ACTIVIDAD + "'";
            }
            else
            {
                MensajError = "El campo ID_DE LA ACTIVIDAD no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosBorrados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_MOD, tabla.VEN_ACTIVIDAD, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajError = e.Message;
                    cantidadRegistrosBorrados = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (cantidadRegistrosBorrados > 0) return true;
            else return false;
        }

        public Boolean Adicionar(String ID_ACTIVIDAD,
            String ID_CLASE,
            String NOMBRE,
            String USU_CRE)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Boolean verificador = true;

            tools _tools = new tools();

            sql = "usp_ven_actividad_adicionar ";
            informacion = sql;

            if (String.IsNullOrEmpty(ID_ACTIVIDAD) == false)
            {
                sql += "'" + ID_ACTIVIDAD + "', ";
                informacion += "ID_ACTIVIDAD = '" + ID_ACTIVIDAD + "', ";
            }
            else
            {
                MensajError = "ERROR: El campo ID DE ACTIVIDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CLASE) == false)
            {
                sql += "'" + ID_CLASE + "', ";
                informacion += "ID_CLASE = '" + ID_CLASE + "', ";
            }
            else
            {
                MensajError = "ERROR: El campo ID DE CLASE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + NOMBRE + "'";
            }
            else
            {
                MensajError = "ERROR: El campo NOMBRE no puede ser vacio.";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    ID_CLASE = conexion.ExecuteScalar(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(USU_CRE, tabla.VEN_ACTIVIDAD, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                    {
                        MensajError = "ERROR: Al intentar ingresar la auditoria de la división.";
                        verificador = false;
                    }
                }
                catch (Exception e)
                {
                    MensajError = e.Message;
                    verificador = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                verificador = false;
            }

            return verificador;
        }
        #endregion metodos
    }
}