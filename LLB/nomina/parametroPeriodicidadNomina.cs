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
    public class parametroPeriodicidadNomina
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
        #endregion varialbes

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
        public parametroPeriodicidadNomina(String idEmpresa, String usuario)
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

            sql = "usp_param_periodospago_obtener_todos ";

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

        public DataTable ObtenerPorId(Int32 ID_PERIODO_PAGO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_periodospago_obtener_registro ";

            #region validaciones
            if (ID_PERIODO_PAGO != 0)
            {
                sql += ID_PERIODO_PAGO.ToString();
            }
            else
            {
                MensajeError += "El campo ID_PERIODO_PAGO no puede ser nulo\n";
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

        public Int32 ObtenerPeriodosMes(Int32 ID_PERIODO_PAGO)
        {
            String sql = null;
            String idRecuperado = null;
            Boolean ejecutar = true;

            sql = "usp_param_periodospago_obtener_periodosMes ";

            #region validaciones
            if (ID_PERIODO_PAGO != 0)
            {
                sql += ID_PERIODO_PAGO.ToString();
            }
            else
            {
                MensajeError += "El campo ID_PERIODO_PAGO no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar == true)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecuperado = conexion.ExecuteScalar(sql);
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

            if (!(String.IsNullOrEmpty(idRecuperado))) return Convert.ToInt32(idRecuperado);
            else return 0;
        }

        public Decimal Adicionar(String DESCRIPCION, Int32 DIAS, Int32 HORAS, Int32 PERIODOS_MES, Boolean ACTIVO)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_periodospago_adicionar ";

            #region validaciones
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
            #endregion validaciones

            sql += "'" + DIAS.ToString() + "', ";
            informacion += "DIAS= '" + DIAS.ToString() + ", ";

            sql += "'" + HORAS.ToString() + "', ";
            informacion += "HORAS= '" + HORAS.ToString() + "', ";

            sql += "'" + PERIODOS_MES.ToString() + "', ";
            informacion += "PERIODOS_MES= '" + PERIODOS_MES.ToString() + "', ";


            if (ACTIVO) sql += "1,";
            else sql += "0,";

            informacion += "ACTIVO = '" + ACTIVO + "' ";
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecuperado = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_PERIODOS_PAGO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean Actualizar(Int32 ID_PERIODO_PAGO, String DESCRIPCION, Int32 DIAS, Int32 HORAS, Int32 PERIODOS_MES, Boolean ACTIVO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_periodospagos_actualizar ";

            #region validaciones
            if (ID_PERIODO_PAGO != 0)
            {
                sql += ID_PERIODO_PAGO.ToString() + ", ";
                informacion += "ID_PERIODO_PAGO= '" + ID_PERIODO_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERIODO_PAGO no puede ser nulo\n";
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

            sql += "'" + DIAS.ToString() + "', ";
            informacion += "DIAS= '" + DIAS.ToString() + ", ";

            sql += "'" + HORAS.ToString() + "', ";
            informacion += "HORAS= '" + HORAS.ToString() + "', ";

            sql += "'" + PERIODOS_MES.ToString() + "', ";
            informacion += "PERIODOS_MES= '" + PERIODOS_MES.ToString() + "', ";


            if (ACTIVO) sql += "1,";
            else sql += "0,";

            informacion += "ACTIVO = '" + ACTIVO + "' ";

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_PERIODOS_PAGO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerPeriodosPagoDrop()
        {
            {
                Conexion conexion = new Conexion(Empresa);
                DataSet _dataSet = new DataSet();
                DataView _dataView = new DataView();
                DataTable _dataTable = new DataTable();
                String sql = null;

                sql = "usp_select_drop_periodospago ";

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
                return _dataTable;

            }
        }

        #endregion metodos
    }
}
