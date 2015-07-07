using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.nomina
{
    public class parametroLiquidaHora
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
        #endregion variables

        #region propiedades
        public String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        #endregion propiedades

        #region constructores
        public parametroLiquidaHora(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerTodos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_liq_horas_obtener_todos ";

            #region validaciones

            #endregion

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
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            return _dataTable;
        }

        public DataTable ObtenerPorId(Int32 ID_LIQUIDA_HORAS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_liq_horas_obtener_registro ";

            #region validaciones
            if (ID_LIQUIDA_HORAS != 0)
            {
                sql += ID_LIQUIDA_HORAS.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_LIQUIDA_HORAS es requerido para la consulta.";
                ejecutar = false;
            }
            #endregion

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
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            return _dataTable;
        }

        public Decimal Adicionar(String DESCRIPCION, Int32 UNIDADES_MES, Decimal PORCENTAJE, Int32 UNIDADES_MAX_REPORTE)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_liq_horas_adicionar ";

            #region Validaciones
            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + UNIDADES_MES.ToString() + "', ";
            informacion += "UNIDADES_MES= '" + UNIDADES_MES.ToString() + "', ";

            if (PORCENTAJE > 0)
            {
                sql += "'" + PORCENTAJE.ToString() + "', ";
                informacion += "PORCENTAJE= '" + PORCENTAJE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PORCENTAJE debe ser mayor a 0\n";
                ejecutar = false;
            }

            sql += "'" + UNIDADES_MAX_REPORTE.ToString() + "', ";
            informacion += "UNIDADES_MAX_REPORTE= '" + UNIDADES_MAX_REPORTE.ToString() + "', ";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecuperado = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_LIQUIDACION_HORAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecuperado))) return Convert.ToDecimal(idRecuperado);
            else return 0;
        }

        public Boolean Actualizar(Int32 ID_LIQUIDA_HORAS, String DESCRIPCION, Int32 UNIDADES_MES, Decimal PORCENTAJE, Int32 UNIDADES_MAX_REPORTE)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_liq_horas_actualizar ";

            #region Validaciones
            if (ID_LIQUIDA_HORAS != 0)
            {
                sql += ID_LIQUIDA_HORAS.ToString() + ", ";
                informacion += "ID_LIQUIDA_HORAS= '" + ID_LIQUIDA_HORAS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_LIQUIDA_HORAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + UNIDADES_MES.ToString() + "', ";
            informacion += "UNIDADES_MES= '" + UNIDADES_MES.ToString() + "', ";

            if (PORCENTAJE > 0)
            {
                sql += "'" + PORCENTAJE.ToString() + "', ";
                informacion += "PORCENTAJE= '" + PORCENTAJE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PORCENTAJE debe ser mayor a 0\n";
                ejecutar = false;
            }

            sql += "'" + UNIDADES_MAX_REPORTE.ToString() + "', ";
            informacion += "UNIDADES_MAX_REPORTE= '" + UNIDADES_MAX_REPORTE.ToString() + "', ";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_LIQUIDACION_HORAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        #endregion metodos
    }
}
