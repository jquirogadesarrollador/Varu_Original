using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class seccion
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
        public seccion(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerTodasLasSecciones()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_seccion_buscar_todas";
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

        public DataTable ObtenerSeccionPorIdSeccion(String ID_SECCION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_seccion_obtener_por_id_seccion ";

            #region VALIDACIONES
            if (String.IsNullOrEmpty(ID_SECCION) == false)
            {
                sql += "'" + ID_SECCION + "'";
            }
            else
            {
                MensajError = "ERROR: El campo ID_DE LA SECCIÓN no puede ser vacio.";
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

        public Boolean Adicionar(String ID_SECCION,
            String NOMBRE,
            String USU_CRE)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Boolean verificador = true;

            tools _tools = new tools();

            sql = "usp_ven_seccion_adicionar ";
            informacion = sql;

            if (String.IsNullOrEmpty(ID_SECCION) == false)
            {
                sql += "'" + ID_SECCION + "', ";
                informacion += "ID_SECCION = '" + ID_SECCION + "', ";
            }
            else
            {
                MensajError = "ERROR: El campo ID DE SECCION no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + ID_SECCION + "'";
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
                    ID_SECCION = conexion.ExecuteScalar(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(USU_CRE, tabla.VEN_SECCION, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                    {
                        MensajError = "ERROR: Al intentar ingresar la auditoria de la sección.";
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

        public DataTable ObtenerDivisionesClasesActidadesEmpresaPorIdSeccion(String ID_SECCION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_divisiones_clases_actividades_empresas_asociadas_a_seccion ";

            #region VALIDACIONES
            if (String.IsNullOrEmpty(ID_SECCION) == false)
            {
                sql += "'" + ID_SECCION + "'";
            }
            else
            {
                MensajError = "ERROR: El campo ID_DE LA SECCIÓN no puede ser vacio.";
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

        public Boolean ActualizarSeccion(String ID_SECCION, String NOMBRE, String USU_MOD)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_seccion_actualizar ";


            if (String.IsNullOrEmpty(ID_SECCION) == false)
            {
                sql += "'" + ID_SECCION + "', ";
                informacion += "ID_SECCION = '" + ID_SECCION + "', ";
            }
            else
            {
                MensajError = "El campo ID_DE LA SECCION no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + ID_SECCION + "'";
            }
            else
            {
                MensajError = "El campo NOMBRE DE LA SECCIÓN no puede ser 0\n";
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
                    _auditoria.Adicionar(USU_MOD, tabla.VEN_SECCION, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean EliminarSeccion(String ID_SECCION, String USU_MOD)
        {
            Int32 cantidadRegistrosBorrados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_seccion_eliminar ";


            if (String.IsNullOrEmpty(ID_SECCION) == false)
            {
                sql += "'" + ID_SECCION + "'";
                informacion += "ID_SECCION = '" + ID_SECCION + "'";
            }
            else
            {
                MensajError = "El campo ID_DE LA SECCION no puede ser 0\n";
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
                    _auditoria.Adicionar(USU_MOD, tabla.VEN_SECCION, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
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
        #endregion metodos
    }
}