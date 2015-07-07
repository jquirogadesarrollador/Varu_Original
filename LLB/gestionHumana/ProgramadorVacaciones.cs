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

namespace Brainsbits.LLB.GestionHumana
{
    public class ProgramadorVacaciones
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
        public ProgramadorVacaciones()
        {

        }
        public ProgramadorVacaciones(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerProgramacionVacacionesPlantaAnio(Decimal ID_EMPRESA_EMPLEADOR, Int32 anio, String ID_REGIONAL, Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerProgramacionVacacionesAnio ";

            sql += ID_EMPRESA_EMPLEADOR.ToString() + ", " + anio + ", '" + ID_REGIONAL + "', " + ID_SUB_C.ToString();

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

        public DataTable ObtenerHistorialVacacionesEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerhistorialVacaciones ";

            sql += ID_EMPLEADO.ToString();

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


        public Decimal AdicionarRegistroVacaciones(Decimal ID_EMPLEADO,
            Decimal ID_EMPRESA_EMPLEADOR,
            Int32 ANIO,
            DateTime FECHA_INICIAL,
            DateTime FECHA_FINAL,
            Int32 DIAS_DISFRUTADOS,
            Int32 DIAS_PENDIENTES,
            String OBSERVACIONES)
        {
            Decimal ID_VACACIONES = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_gh_programacion_vacaciones_adicionar ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO no puede ser 0 o vacio.\n";
                ejecutar = false;
            }

            if (ID_EMPRESA_EMPLEADOR != 0)
            {
                sql += ID_EMPRESA_EMPLEADOR + ", ";
                informacion += "ID_EMPRESA_EMPLEADOR = '" + ID_EMPRESA_EMPLEADOR.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA_EMPLEADOR no puede ser 0 o vacio.\n";
                ejecutar = false;
            }

            if (ANIO != 0)
            {
                sql += ANIO + ", ";
                informacion += "ANIO = '" + ANIO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ANIO no puede ser 0 o vacio.\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";
            informacion += "FECHA_INICIAL = '" + FECHA_INICIAL.ToShortDateString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FINAL) + "', ";
            informacion += "FECHA_FINAL = '" + FECHA_FINAL.ToShortDateString() + "', ";

            if (DIAS_DISFRUTADOS != 0)
            {
                sql += DIAS_DISFRUTADOS + ", ";
                informacion += "DIAS_DISFRUTADOS = '" + DIAS_DISFRUTADOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo DIAS_DISFRUTADOS no puede ser 0 o vacio.\n";
                ejecutar = false;
            }

            if (DIAS_PENDIENTES != 0)
            {
                sql += DIAS_PENDIENTES + ", ";
                informacion += "DIAS_PENDIENTES = '" + DIAS_PENDIENTES.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "DIAS_PENDIENTES = '0', ";
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo OBSERVACIONES no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    ID_VACACIONES = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.GH_PROGRAMACION_VACACIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_VACACIONES = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return ID_VACACIONES;
        }

        public Boolean ActualizarRegistroVacaciones(Decimal ID_VACACIONES,
            DateTime FECHA_INICIAL,
            DateTime FECHA_FINAL,
            Int32 DIAS_DISFRUTADOS,
            Int32 DIAS_PENDIENTES,
            String OBSERVACIONES)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_gh_programacion_vacaciones_actualizar ";

            #region validaciones

            if (ID_VACACIONES != 0)
            {
                sql += ID_VACACIONES + ", ";
                informacion += "ID_VACACIONES = '" + ID_VACACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo ID_VACACIONES no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIAL) + "', ";
            informacion += "FECHA_INICIAL = '" + FECHA_INICIAL.ToShortDateString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FINAL) + "', ";
            informacion += "FECHA_FINAL = '" + FECHA_FINAL.ToShortDateString() + "', ";

            if (DIAS_DISFRUTADOS != 0)
            {
                sql += DIAS_DISFRUTADOS + ", ";
                informacion += "DIAS_DISFRUTADOS = '" + DIAS_DISFRUTADOS + "', ";
            }
            else
            {
                MensajeError = "El campo DIAS_DISFRUTADOS no puede ser vacio.";
                ejecutar = false;
            }

            if (DIAS_PENDIENTES != 0)
            {
                sql += DIAS_PENDIENTES + ", ";
                informacion += "DIAS_PENDIENTES = '" + DIAS_PENDIENTES + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "DIAS_PENDIENTES = '0', ";
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo OBSERVACIONES no puede ser vacio.";
                ejecutar = false;
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
                    _auditoria.Adicionar(Usuario, tabla.GH_PROGRAMACION_VACACIONES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable ObtenerSubCentrosRelacionadosAEmpresaRegional(Decimal ID_EMPRESA_EMPLEADOR, String ID_REGIONAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerSubCentrosPorRegionalyEmpresa ";

            sql += ID_EMPRESA_EMPLEADOR.ToString() + ", '" + ID_REGIONAL + "'";

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
