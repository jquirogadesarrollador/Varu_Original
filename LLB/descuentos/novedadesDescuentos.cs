using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
namespace Brainsbits.LLB.descuentos
{
    public class novedadesDescuentos
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

        public novedadesDescuentos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerDescuentos(Int32 ID_EMPLEADO, String SALDO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_Obtener_Descuentos_Novedades ";

            #region validaciones
            sql += ID_EMPLEADO.ToString() + ", '" + SALDO + "'";
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

        public DataTable ObtenerPorIdEmpleado(Int32 ID_EMPLEADO, String ESTADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_descuentos_novedades_ObtenerPorEmpleado ";

            #region validaciones
            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
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

        public DataTable ObtenerPorId(Int32 ID_DESCUENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_descuentos_novedades_ObtenerPorId ";

            #region validaciones
            if (ID_DESCUENTO > 0)
            {
                sql += ID_DESCUENTO.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_DESCUENTO es requerido para la consulta.";
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

        public Decimal Adicionar(DateTime FECHA, Decimal ID_EMPLEADO, String NUMERO_IDENTIDAD, Decimal ID_CONCEPTO, Decimal VALOR, Int32 NUMERO_CUOTAS,
            Decimal VALOR_CUOTA, String DESCUENTO_PRIMAS, String LIQ_P_1, String LIQ_P_2, String LIQ_P_3, String LIQ_P_4, String TIPO)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_descuentos_novedades_adicionar ";

            #region validaciones
            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + FECHA.ToString() + "', ";

            if (ID_EMPLEADO != 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "', ";
                informacion += "ID_EMPLEADO= '" + ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + NUMERO_IDENTIDAD.ToString() + "', ";
            informacion += "NUMERO_IDENTIDAD= '" + NUMERO_IDENTIDAD.ToString() + ", ";

            if (ID_CONCEPTO != 0)
            {
                sql += "'" + ID_CONCEPTO.ToString() + "', ";
                informacion += "ID_CONCEPTO= '" + ID_CONCEPTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CONCEPTO es requerido para la consulta.";
                ejecutar = false;
            }

            if (VALOR != 0)
            {
                sql += "'" + VALOR.ToString() + "', ";
                informacion += "VALOR= '" + VALOR.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR es requerido para la consulta.";
                ejecutar = false;
            }
            if (NUMERO_CUOTAS != 0)
            {
                sql += "'" + NUMERO_CUOTAS.ToString() + "', ";
                informacion += "NUMERO_CUOTAS= '" + NUMERO_CUOTAS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo NUMERO_CUOTAS es requerido para la consulta.";
                ejecutar = false;
            }
            if (VALOR_CUOTA != 0)
            {
                sql += "'" + VALOR_CUOTA.ToString() + "', ";
                informacion += "VALOR_CUOTA= '" + VALOR_CUOTA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR_CUOTA es requerido para la consulta.";
                ejecutar = false;
            }
            if (DESCUENTO_PRIMAS == String.Empty) DESCUENTO_PRIMAS = "N";
            if (LIQ_P_1 == String.Empty) LIQ_P_1 = "N";
            if (LIQ_P_2 == String.Empty) LIQ_P_2 = "N";
            if (LIQ_P_3 == String.Empty) LIQ_P_3 = "N";
            if (LIQ_P_4 == String.Empty) LIQ_P_4 = "N";

            sql += "'" + DESCUENTO_PRIMAS.ToString() + "', '" + LIQ_P_1.ToString() + "', '" + LIQ_P_2.ToString() + "', '" + LIQ_P_3.ToString() + "', '" + LIQ_P_4.ToString() + "', ";
            informacion += "DESCUENTO_PRIMAS= '" + DESCUENTO_PRIMAS.ToString() + ", " + "LIQ_P_1= '" + LIQ_P_1.ToString() + ", " + "LIQ_P_2= '" + LIQ_P_2.ToString() + ", " +
                "LIQ_P_3= '" + LIQ_P_3.ToString() + ", " + "LIQ_P_4= '" + LIQ_P_4.ToString() + ", ";
            informacion += "LIQ_P_1= '" + LIQ_P_1.ToString() + ", ";

            sql += "'" + TIPO + "', ";
            informacion += "TIPO = '" + TIPO.ToString() + "', ";

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
                    _auditoria.Adicionar(Usuario, tabla.DESC_NOVEDADES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean Actualizar(Int32 ID_DESCUENTO, DateTime FECHA, Decimal ID_EMPLEADO, String NUMERO_IDENTIDAD, Decimal ID_CONCEPTO, Decimal VALOR, Int32 NUMERO_CUOTAS,
            Decimal VALOR_CUOTA, String DESCUENTO_PRIMAS, String LIQ_P_1, String LIQ_P_2, String LIQ_P_3, String LIQ_P_4, String TIPO, String OBSERVACIONES)
        {
            String sql = null;
            Int32 idUpdated = 0;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_descuentos_novedades_actualizar ";

            #region validaciones
            if (ID_DESCUENTO != 0)
            {
                sql += "'" + ID_DESCUENTO.ToString() + "', ";
                informacion += "ID_DESCUENTO= '" + ID_DESCUENTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_DESCUENTO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + FECHA.ToString() + "', ";

            if (ID_EMPLEADO != 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "', ";
                informacion += "ID_EMPLEADO= '" + ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + NUMERO_IDENTIDAD.ToString() + "', ";
            informacion += "NUMERO_IDENTIDAD= '" + NUMERO_IDENTIDAD.ToString() + ", ";

            if (ID_CONCEPTO != 0)
            {
                sql += "'" + ID_CONCEPTO.ToString() + "', ";
                informacion += "ID_CONCEPTO= '" + ID_CONCEPTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CONCEPTO es requerido para la consulta.";
                ejecutar = false;
            }

            if (VALOR != 0)
            {
                sql += "'" + VALOR.ToString() + "', ";
                informacion += "VALOR= '" + VALOR.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR es requerido para la consulta.";
                ejecutar = false;
            }
            if (NUMERO_CUOTAS != 0)
            {
                sql += "'" + NUMERO_CUOTAS.ToString() + "', ";
                informacion += "NUMERO_CUOTAS= '" + NUMERO_CUOTAS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo NUMERO_CUOTAS es requerido para la consulta.";
                ejecutar = false;
            }
            if (VALOR_CUOTA != 0)
            {
                sql += "'" + VALOR_CUOTA.ToString() + "', ";
                informacion += "VALOR_CUOTA= '" + VALOR_CUOTA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR_CUOTA es requerido para la consulta.";
                ejecutar = false;
            }

            if (DESCUENTO_PRIMAS == String.Empty) DESCUENTO_PRIMAS = "N";
            if (LIQ_P_1 == String.Empty) LIQ_P_1 = "N";
            if (LIQ_P_2 == String.Empty) LIQ_P_2 = "N";
            if (LIQ_P_3 == String.Empty) LIQ_P_3 = "N";
            if (LIQ_P_4 == String.Empty) LIQ_P_4 = "N";

            sql += "'" + DESCUENTO_PRIMAS.ToString() + "', '" + LIQ_P_1.ToString() + "', '" + LIQ_P_2.ToString() + "', '" + LIQ_P_3.ToString() + "', '" + LIQ_P_4.ToString() + "', ";
            informacion += "DESCUENTO_PRIMAS= '" + DESCUENTO_PRIMAS.ToString() + ", " + "LIQ_P_1= '" + LIQ_P_1.ToString() + ", " + "LIQ_P_2= '" + LIQ_P_2.ToString() + ", " +
                "LIQ_P_3= '" + LIQ_P_3.ToString() + ", " + "LIQ_P_4= '" + LIQ_P_4.ToString() + ", ";
            informacion += "LIQ_P_1= '" + LIQ_P_1.ToString() + ", ";

            sql += "'" + TIPO + "', ";
            informacion += "TIPO = '" + TIPO.ToString() + "', ";

            sql += "'" + OBSERVACIONES + "', ";
            informacion += "OBSERVACIONES = '" + OBSERVACIONES.ToString() + "', ";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idUpdated = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.DESC_NOVEDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            if (idUpdated > 0)
            {
                return true;
            }
            else
            {
                MensajeError = "Este credito ya tiene registros de abonos, no se puede modificar.";
                return false;
            }
        }

        public Boolean Abonar(Int32 ID_DESCUENTO, DateTime FECHA, Decimal VALOR, Int32 NUMERO_CUOTAS, Decimal VALOR_CUOTA, String COMPROBANTE, String OBSERVACIONES)
        {
            String sql = null;
            Int32 idUpdated = 0;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_descuentos_novedades_abonar ";

            #region validaciones
            if (ID_DESCUENTO != 0)
            {
                sql += "'" + ID_DESCUENTO.ToString() + "', ";
                informacion += "ID_DESCUENTO= '" + ID_DESCUENTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_DESCUENTO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + FECHA.ToString() + "', ";

            if (VALOR != 0)
            {
                sql += "'" + VALOR.ToString() + "', ";
                informacion += "VALOR= '" + VALOR.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR es requerido para la consulta.";
                ejecutar = false;
            }

            if (NUMERO_CUOTAS != 0)
            {
                sql += "'" + NUMERO_CUOTAS.ToString() + "', ";
                informacion += "NUMERO_CUOTAS= '" + NUMERO_CUOTAS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo NUMERO_CUOTAS es requerido para la consulta.";
                ejecutar = false;
            }
            if (VALOR_CUOTA != 0)
            {
                sql += "'" + VALOR_CUOTA.ToString() + "', ";
                informacion += "VALOR_CUOTA= '" + VALOR_CUOTA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR_CUOTA es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + COMPROBANTE + "', ";
            sql += "'" + OBSERVACIONES + "', ";
            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idUpdated = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.DESC_NOVEDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            if (idUpdated > 0) return true;
            else return false;
        }

        public Boolean Desactivar(Int32 ID_DESC_TERCERO, Int32 DESACTIVAR)
        {
            String sql = null;
            Int32 Count_Updated = 0;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_descuentos_novedades_desactivar ";

            #region validaciones
            if (ID_DESC_TERCERO != 0)
            {
                sql += "'" + ID_DESC_TERCERO.ToString() + "', ";
                informacion += "ID_DESC_TERCERO= '" + ID_DESC_TERCERO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_DESC_TERCERO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + DESACTIVAR + "', ";
            informacion += "DESACTIVAR = '" + DESACTIVAR + "', ";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    Count_Updated = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.DESC_NOVEDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            if (Count_Updated > 0) return true;
            else return false;
        }

        public Boolean Pagar(Int32 ID_DESC_TERCERO, Int32 CUOTAS, Decimal VALOR)
        {
            String sql = null;
            Int32 Count_Updated = 0;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_descuentos_novedades_pagar ";

            #region validaciones
            if (ID_DESC_TERCERO != 0)
            {
                sql += "'" + ID_DESC_TERCERO.ToString() + "', ";
                informacion += "ID_DESC_TERCERO= '" + ID_DESC_TERCERO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_DESC_TERCERO es requerido para la consulta.";
                ejecutar = false;
            }

            if (CUOTAS != 0)
            {
                sql += "'" + CUOTAS.ToString() + "', ";
                informacion += "CUOTAS= '" + CUOTAS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo CUOTAS es requerido para la consulta.";
                ejecutar = false;
            }

            if (VALOR != 0)
            {
                sql += "'" + VALOR.ToString() + "', ";
                informacion += "VALOR= '" + VALOR.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo VALOR es requerido para la consulta.";
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
                    Count_Updated = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.DESC_NOVEDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            if (Count_Updated > 0) return true;
            else return false;
        }

        public Boolean Anular(Int32 ID_DESC_TERCERO, String OBSERVACIONES)
        {
            String sql = null;
            Int32 Count_Updated = 0;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_descuentos_novedades_anular ";

            #region validaciones
            if (ID_DESC_TERCERO != 0)
            {
                sql += "'" + ID_DESC_TERCERO.ToString() + "', ";
                informacion += "ID_DESC_TERCERO= '" + ID_DESC_TERCERO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_DESC_TERCERO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + OBSERVACIONES + "', ";
            informacion += "OBSERVACIONES = '" + OBSERVACIONES.ToString() + "'";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    Count_Updated = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.DESC_NOVEDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            if (Count_Updated > 0) return true;
            else return false;
        }


        public Boolean Eliminar(Int32 ID_DESC_TERCERO, String OBSERVACIONES)
        {
            String sql = null;
            Int32 Count_Updated = 0;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_descuentos_novedades_eliminar ";

            #region validaciones
            if (ID_DESC_TERCERO != 0)
            {
                sql += "'" + ID_DESC_TERCERO.ToString() + "', ";
                informacion += "ID_DESC_TERCERO= '" + ID_DESC_TERCERO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_DESC_TERCERO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + OBSERVACIONES + "', ";
            informacion += "OBSERVACIONES = '" + OBSERVACIONES.ToString() + "'";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    Count_Updated = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.DESC_NOVEDADES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            if (Count_Updated > 0)
            {
                return true;
            }
            else
            {
                if (Count_Updated < 0) MensajeError = "Este registro ya tiene movimientos no se puede eliminar.";
                return false;
            }
        }

        public DataTable ObtenerTiposCreditos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_Obtener_Tipos_Creditos ";

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


        public DataTable ObtenerDropTercerosCreditos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_Obtener_Terceros_Creditos ";

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

        public DataTable ObtenerDropConceptosCreditos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_Obtener_Conceptos_Creditos ";

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

        public DataTable ObtenerDropEmpleadosCreditos(Int32 ID_TERCERO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_Obtener_Empleados_Creditos ";

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

        public DataTable ObtenerPeriodos(Decimal ID_EMPLEADO, Int32 ID_DESCUENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_Obtener_Periodos_Descuentos ";

            #region validaciones
            if (ID_EMPLEADO > 0)
            {
                sql += "'" + ID_EMPLEADO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + ID_DESCUENTO.ToString() + "' ";
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

        public DataTable ObtenerIdTerceroDescuentos(String NUMERO_IDENTIFICACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_Obtener_Id_Tercero_Descuentos ";

            #region validaciones

            if (!(String.IsNullOrEmpty(NUMERO_IDENTIFICACION)))
                sql += "'" + NUMERO_IDENTIFICACION.ToString() + "'";
            else
            {
                MensajeError = "El campo NUMERO_IDENTIFICACION es requerido para la consulta.";
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

        public DataTable ObtenerPorNumeroIdentificacion(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_Obtener_Empleados_Creditos_Por_Numero_Identificacion ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
                ejecutar = false;
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

        public DataTable ObtenerContratoPorNumeroIdentificacion(String NUM_DOC_IDENTIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_Obtener_Empleado_Creditos_Por_Cedula ";

            if (!(String.IsNullOrEmpty(NUM_DOC_IDENTIDAD)))
            {
                sql += "'" + NUM_DOC_IDENTIDAD + "'";
                informacion += "NUM_DOC_IDENTIDAD = '" + NUM_DOC_IDENTIDAD + "'";
            }
            else
            {
                MensajeError += "El campo NUM_DOC_IDENTIDAD no puede ser nulo. \n";
                ejecutar = false;
            }


            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CONTRATOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPorNombre(String NOMBRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_Obtener_Empleados_Creditos_Por_Nombre ";

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo. \n";
                ejecutar = false;
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

        public DataTable ObtenerPorApellido(String APELLIDO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_Obtener_Empleados_Creditos_Por_Apellido ";

            if (!(String.IsNullOrEmpty(APELLIDO)))
            {
                sql += "'" + APELLIDO + "'";
            }
            else
            {
                MensajeError += "El campo APELLIDO no puede ser nulo. \n";
                ejecutar = false;
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