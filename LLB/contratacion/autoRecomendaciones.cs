using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.contratacion
{
    public class autoRecomendaciones
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
        public autoRecomendaciones(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarConRegAutoRecomendaciones(int REGISTRO_EXAMENES_EMPLEADO, String OBSERVACIONES, DateTime FECHA_R)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_CON_REG_AUTO_RECOMENDACIONES_ADICIONAR ";

            #region validaciones

            if (REGISTRO_EXAMENES_EMPLEADO != 0)
            {
                sql += REGISTRO_EXAMENES_EMPLEADO + ", ";
                informacion += "REGISTRO_EXAMENES_EMPLEADO = '" + REGISTRO_EXAMENES_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_EXAMENES_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_AUTO_RECOMENDACIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }


        public Decimal AdicionarConRegAutoRecomendaciones(Decimal REGISTRO_EXAMENES_EMPLEADO, String OBSERVACIONES, DateTime FECHA_R, Conexion conexion)
        {
            String sql = null;
            Decimal identificador = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_CON_REG_AUTO_RECOMENDACIONES_ADICIONAR ";

            #region validaciones

            if (REGISTRO_EXAMENES_EMPLEADO != 0)
            {
                sql += REGISTRO_EXAMENES_EMPLEADO + ", ";
                informacion += "REGISTRO_EXAMENES_EMPLEADO = '" + REGISTRO_EXAMENES_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_EXAMENES_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE= '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_AUTO_RECOMENDACIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    identificador = 0;
                }
            }

            return identificador;
        }


        public Boolean ActualizarConRegAutoRecomendaciones(int REGISTRO, int REGISTRO_EXAMENES_EMPLEADO, String OBSERVACIONES, DateTime FECHA_R)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_CON_REG_AUTO_RECOMENDACIONES_ACTUALIZAR ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO= '" + REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }
            if (REGISTRO_EXAMENES_EMPLEADO != 0)
            {
                sql += REGISTRO_EXAMENES_EMPLEADO + ", ";
                informacion += "REGISTRO_EXAMENES_EMPLEADO= '" + REGISTRO_EXAMENES_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_EXAMENES_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES= '" + OBSERVACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(FECHA_R.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_R) + "', ";
                informacion += "FECHA_R= '" + FECHA_R.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA_R no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_AUTO_RECOMENDACIONES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerConRegAutoRecomendacionesPorRegistro(int REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_AUTO_RECOMENDACIONES_OBTENER_POR_REGISTRO ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_AUTO_RECOMENDACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        public DataTable ObtenerConRegAutoRecomendacionesPorRegistroExamenesEmpleado(int REGISTRO_EXAMENES_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_REG_AUTO_RECOMENDACIONES_OBTENER_POR_REGISTRO_EXAMENES_EMPLEADO ";

            if (REGISTRO_EXAMENES_EMPLEADO != 0)
            {
                sql += REGISTRO_EXAMENES_EMPLEADO;
                informacion += "REGISTRO_EXAMENES_EMPLEADO = '" + REGISTRO_EXAMENES_EMPLEADO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO_EXAMENES_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_AUTO_RECOMENDACIONES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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

        #endregion
    }
}
