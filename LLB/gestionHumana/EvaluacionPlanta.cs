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
    public class EvaluacionPlanta
    {
        #region variables

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        public enum TiposEvaluacion
        {
            PRUEBA = 0,
            DESEMPENO,
            ACTITUDINAL
        }

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
        public EvaluacionPlanta()
        {

        }
        public EvaluacionPlanta(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerInformacionParaSemaforoPrincipal(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_gh_evaluaciones_obtenerSemaforoEvaluacionesPeriodoPrueba ";

            sql += ID_EMPRESA.ToString();

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

        public DataTable ObtenerEmpleadosPlantaPorIdentificacion(Decimal ID_EMPRESA, String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerEmpleadoPlantaPorIdentificacion ";

            sql += ID_EMPRESA.ToString() + ", '" + NUM_DOC_IDENTIDAD + "'";

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


        public DataTable ObtenerEmpleadosPlantaPorNombres(Decimal ID_EMPRESA, String NOMBRES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerEmpleadoPlantaPorNombres ";

            sql += ID_EMPRESA.ToString() + ", '" + NOMBRES + "'";

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


        public DataTable ObtenerEmpleadosPlantaPorApellidos(Decimal ID_EMPRESA, String APELLIDOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerEmpleadoPlantaPorApellidos ";

            sql += ID_EMPRESA.ToString() + ", '" + APELLIDOS + "'";

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


        public DataTable ObtenerHistorialEvaluacionesEmpleadoPorTipoEvaluacion(Decimal ID_EMPLEADO, TiposEvaluacion tipoEvaluacion)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_gh_evaluaciones_obtenerHistorialEmpleadoPorTipoEvaluacion ";

            sql += ID_EMPLEADO.ToString() + ", '" + tipoEvaluacion.ToString() + "'";

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


        public Decimal AdicionarEvaluacion(String TIPO_EVALUACION,
            Decimal ID_EMPLEADO_EVALUADO,
            Decimal ID_EMPLEADO_EVALUADOR,
            DateTime FECHA_EVALUACION,
            Decimal PUNTAJE,
            String CALIFICACION,
            String FORTALEZAS,
            String OPORTUNIDADES_MEJORAS,
            String CURSOS_CAPACITACIONES)
        {
            tools _tools = new tools();

            String sql = null;
            Decimal idRecuperado = 0;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_gh_evaluaciones_adicionar ";


            if (String.IsNullOrEmpty(TIPO_EVALUACION) == false)
            {
                sql += "'" + TIPO_EVALUACION + "', ";
                informacion += "TIPO_EVALUACION = '" + TIPO_EVALUACION + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_EVALUACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPLEADO_EVALUADO != 0)
            {
                sql += ID_EMPLEADO_EVALUADO + ", ";
                informacion += "ID_EMPLEADO_EVALUADO = '" + ID_EMPLEADO_EVALUADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO_EVALUADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPLEADO_EVALUADOR != 0)
            {
                sql += ID_EMPLEADO_EVALUADOR + ", ";
                informacion += "ID_EMPLEADO_EVALUADOR = '" + ID_EMPLEADO_EVALUADOR + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO_EVALUADOR no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_EVALUACION) + "', ";
            informacion += "FECHA_EVALUACION = '" + FECHA_EVALUACION.ToShortDateString() + "', ";

            if (PUNTAJE != 0)
            {
                sql += PUNTAJE.ToString().Replace(',', '.') + ", ";
                informacion += "PUNTAJE = '" + PUNTAJE.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "PUNTAJE = '0', ";
            }

            if (String.IsNullOrEmpty(CALIFICACION) == false)
            {
                sql += "'" + CALIFICACION + "', ";
                informacion += "CALIFICACION = '" + CALIFICACION + "', ";
            }
            else
            {
                MensajeError += "El campo CALIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(FORTALEZAS) == false)
            {
                sql += "'" + FORTALEZAS + "', ";
                informacion += "FORTALEZAS = '" + FORTALEZAS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FORTALEZAS = 'null', ";
            }

            if (String.IsNullOrEmpty(OPORTUNIDADES_MEJORAS) == false)
            {
                sql += "'" + OPORTUNIDADES_MEJORAS + "', ";
                informacion += "OPORTUNIDADES_MEJORAS = '" + OPORTUNIDADES_MEJORAS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OPORTUNIDADES_MEJORAS = 'null', ";
            }

            if (String.IsNullOrEmpty(CURSOS_CAPACITACIONES) == false)
            {
                sql += "'" + CURSOS_CAPACITACIONES + "', ";
                informacion += "CURSOS_CAPACITACIONES = '" + CURSOS_CAPACITACIONES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CURSOS_CAPACITACIONES = 'null', ";
            }

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
                    _auditoria.Adicionar(Usuario, tabla.GH_EVALUACIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public DataTable ObtenerEvaluacionEmpleado(Decimal ID_EVALUACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_gh_evaluaciones_obtenerPorIdEvaluacion ";

            sql += ID_EVALUACION;

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

        public Boolean Actualizar(Decimal ID_EVALUACION,
            Decimal ID_EMPLEADO_EVALUADOR,
            DateTime FECHA_EVALUACION,
            Decimal PUNTAJE,
            String CALIFICACION,
            String FORTALEZAS,
            String OPORTUNIDAD_MEJORAS,
            String CURSOS_CAPACITACIONES)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_gh_evaluaciones_actualizar ";

            #region validaciones

            if (ID_EVALUACION != 0)
            {
                sql += ID_EVALUACION + ", ";
                informacion += "ID_EVALUACION = '" + ID_EVALUACION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EVALUACION no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_EMPLEADO_EVALUADOR != 0)
            {
                sql += ID_EMPLEADO_EVALUADOR + ", ";
                informacion += "ID_EMPLEADO_EVALUADOR = '" + ID_EMPLEADO_EVALUADOR + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO_EVALUADOR no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_EVALUACION) + "', ";
            informacion += "FECHA_EVALUACION = '" + FECHA_EVALUACION.ToShortDateString() + "', ";

            if (ID_EMPLEADO_EVALUADOR != 0)
            {
                sql += PUNTAJE.ToString().Replace(',', '.') + ", ";
                informacion += "PUNTAJE = '" + PUNTAJE.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "PUNTAJE = '0', ";
            }

            if (String.IsNullOrEmpty(CALIFICACION) == false)
            {
                sql += "'" + CALIFICACION + "', ";
                informacion += "CALIFICACION = '" + CALIFICACION + "', ";
            }
            else
            {
                MensajeError = "El campo CALIFICACION no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(FORTALEZAS) == false)
            {
                sql += "'" + FORTALEZAS + "', ";
                informacion += "FORTALEZAS = '" + FORTALEZAS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FORTALEZAS = 'null', ";
            }

            if (String.IsNullOrEmpty(OPORTUNIDAD_MEJORAS) == false)
            {
                sql += "'" + OPORTUNIDAD_MEJORAS + "', ";
                informacion += "OPORTUNIDAD_MEJORAS = '" + OPORTUNIDAD_MEJORAS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OPORTUNIDAD_MEJORAS = 'null', ";
            }

            if (String.IsNullOrEmpty(CURSOS_CAPACITACIONES) == false)
            {
                sql += "'" + CURSOS_CAPACITACIONES + "', ";
                informacion += "CURSOS_CAPACITACIONES = '" + CURSOS_CAPACITACIONES + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CURSOS_CAPACITACIONES = 'null', ";
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
                    _auditoria.Adicionar(Usuario, tabla.GH_EVALUACIONES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public String ObtenerCalificacionSegunPuntajeYTipoEvaluacion(Decimal puntaje, String tipoEvaluacion)
        {
            if (tipoEvaluacion == TiposEvaluacion.ACTITUDINAL.ToString())
            {
                if (puntaje >= 46 && puntaje <= 54)
                {
                    return "BUENO";
                }
                else
                {
                    if (puntaje >= 31 && puntaje <= 45)
                    {
                        return "ACEPTABLE";
                    }
                    else
                    {
                        return "DEFICIENTE";
                    }
                }
            }
            else
            {
                if (tipoEvaluacion == TiposEvaluacion.DESEMPENO.ToString())
                {
                    if (puntaje >= 4.5M && puntaje >= 5.0M)
                    {
                        return "EXCEPCIONAL";
                    }
                    else
                    {
                        if (puntaje >= 4.0M && puntaje >= 4.4M)
                        {
                            return "MUY BUENO";
                        }
                        else
                        {
                            if (puntaje >= 3.5M && puntaje >= 3.9M)
                            {
                                return "COMPETENTE";
                            }
                            else
                            {
                                if (puntaje >= 3.0M && puntaje >= 3.4M)
                                {
                                    return "ACEPTABLE";
                                }
                                else
                                {
                                    return "DEFICIENTE";
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (tipoEvaluacion == TiposEvaluacion.PRUEBA.ToString())
                    {
                        if (puntaje >= 47 && puntaje <= 63)
                        {
                            return "BUENO";
                        }
                        else
                        {
                            if (puntaje >= 35 && puntaje <= 46)
                            {
                                return "ACEPTABLE";
                            }
                            else
                            {
                                return "DEFICIENTE";
                            }
                        }
                    }
                    else
                    {
                        return "Tipo de evaluación indeterminada";
                    }
                }
            }
        }


        public DataTable ObtenerInformacionBasicaEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtenerInformacionBasica ";

            sql += ID_EMPLEADO;

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
