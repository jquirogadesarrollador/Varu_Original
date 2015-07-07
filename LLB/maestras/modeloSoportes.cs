using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class modeloSoportes
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
        public modeloSoportes(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerTodosMaestro()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_modelosoportes_obtener_todos_maestro ";

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

        public DataTable ObtenerTodosDetalle(Int32 ID_MODELO_SOPORTES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_modelosoportes_obtener_todos_detalle ";

            #region validaciones
            if (ID_MODELO_SOPORTES != 0)
            {
                sql += ID_MODELO_SOPORTES.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_MODELO_SOPORTES es requerido para la consulta.";
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

        public DataTable ObtenerPorIdMaestro(Int32 ID_MODELO_SOPORTES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_modelosoportes_obtener_registro_maestro ";

            #region validaciones
            if (ID_MODELO_SOPORTES != 0)
            {
                sql += ID_MODELO_SOPORTES.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_MODELO_SOPORTES es requerido para la consulta.";
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


        public DataTable ObtenerPorIdDetalle(Int32 ID_MODELO_SOPORTES_DETALLE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_modelosoportes_obtener_registro_detalle ";

            #region validaciones
            if (ID_MODELO_SOPORTES_DETALLE != 0)
            {
                sql += ID_MODELO_SOPORTES_DETALLE.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_MODELO_SOPORTES_DETALLE es requerido para la consulta.";
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

        public Decimal AdicionarMaestro(String GRUPO, String DESCRIPCION)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_modelosoportes_adicionar_maestro ";

            #region validaciones
            if (!(String.IsNullOrEmpty(GRUPO)))
            {
                sql += "'" + GRUPO + "', ";
                informacion += "GRUPO = '" + GRUPO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo GRUPO no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.PAR_MODELO_SOPORTES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Decimal AdicionarDetalle(Int32 ID_MODELO_SOPORTES, String REPORT_NAME, String REPORT_PATH, String REPORT_SERVER_URL)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_modelosoportes_adicionar_detalle ";

            #region validaciones
            if (ID_MODELO_SOPORTES != 0)
            {
                sql += ID_MODELO_SOPORTES.ToString() + ", ";
                informacion += "ID_MODELO_SOPORTES= '" + ID_MODELO_SOPORTES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_MODELO_SOPORTES no puede ser menor o igual a cero\n";
                ejecutar = false;
            }


            if (!(String.IsNullOrEmpty(REPORT_NAME)))
            {
                sql += "'" + REPORT_NAME + "', ";
                informacion += "REPORT_NAME = '" + REPORT_NAME.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REPORT_NAME no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REPORT_PATH)))
            {
                sql += "'" + REPORT_PATH + "', ";
                informacion += "REPORT_PATH = '" + REPORT_PATH.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REPORT_PATH no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REPORT_SERVER_URL)))
            {
                sql += "'" + REPORT_SERVER_URL + "', ";
                informacion += "REPORT_SERVER_URL = '" + REPORT_SERVER_URL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REPORT_SERVER_URL no puede ser nulo\n";
                ejecutar = false;
            }

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
                    _auditoria.Adicionar(Usuario, tabla.PAR_MODELO_SOPORTES_DETALLE, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean ActualizarMaestro(Int32 ID_MODELO_SOPORTES, String GRUPO, String DESCRIPCION)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_modelosoportes_actualizar_maestro ";

            #region validaciones

            if (ID_MODELO_SOPORTES != 0)
            {
                sql += ID_MODELO_SOPORTES.ToString() + ", ";
                informacion += "ID_MODELO_SOPORTES= '" + ID_MODELO_SOPORTES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_MODELO_SOPORTES no puede ser menor o igual a cero\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(GRUPO)))
            {
                sql += "'" + GRUPO + "', ";
                informacion += "GRUPO = '" + GRUPO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo GRUPO no puede ser nulo\n";
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

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_MODELO_SOPORTES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarDetalle(Int32 ID_MODELO_SOPORTES_DETALLE, String REPORT_NAME, String REPORT_PATH, String REPORT_SERVER_URL)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_modelosoportes_actualizar_detalle ";

            #region validaciones

            if (ID_MODELO_SOPORTES_DETALLE != 0)
            {
                sql += ID_MODELO_SOPORTES_DETALLE.ToString() + ", ";
                informacion += "ID_MODELO_SOPORTES_DETALLE= '" + ID_MODELO_SOPORTES_DETALLE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_MODELO_SOPORTES_DETALLE no puede ser menor o igual a cero\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REPORT_NAME)))
            {
                sql += "'" + REPORT_NAME + "', ";
                informacion += "REPORT_NAME = '" + REPORT_NAME.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REPORT_NAME no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REPORT_PATH)))
            {
                sql += "'" + REPORT_PATH + "', ";
                informacion += "REPORT_PATH = '" + REPORT_PATH.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REPORT_PATH no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REPORT_SERVER_URL)))
            {
                sql += "'" + REPORT_SERVER_URL + "', ";
                informacion += "REPORT_SERVER_URL = '" + REPORT_SERVER_URL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REPORT_SERVER_URL no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_MODELO_SOPORTES_DETALLE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerMaestros()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ObtenerMaestrosModeloSoportes ";

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

        public DataTable ObtenerModeloSoportesDrop()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_select_drop_modelosoportes ";

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

        #endregion metodos
    }
}
