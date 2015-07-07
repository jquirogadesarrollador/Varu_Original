using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.almacen
{
    public class negociacionProveedores
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
        public negociacionProveedores(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region alm_negociacion_proveedores

        public DataTable ObtenerNegociacionProveedor(Decimal ID_PROVEEDOR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_NEGOCIACION_PROVEEDOR_OBTENER_POR_ID_PROVEEDOR ";

            #region validaciones
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PROVEEDOR no puede ser 0";
            }
            #endregion validaciones

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

        public Decimal AdicionarNuevaNegociacion(Decimal ID_PROVEEDOR,
            Int32 GARANTIA,
            Int32 PERIODOENTREGA,
            Int32 PERIODOPAGO,
            String FORMA_PAGO,
            Decimal ID_ENTIDAD_BANCARIA,
            String CUENTA_BANCARIA,
            String TIPO_CUENTA,
            String CONDICIONES_ENTREGA,
            Conexion conexion)
        {
            Decimal ID_NEGOCIACION = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_NEGOCIACION_PROVEEDOR_ADICIONAR ";

            #region validaciones
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + ", ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PROVEEDOR no puede ser vacio.";
                ejecutar = false;
            }

            sql += GARANTIA + ", ";
            informacion += "GARANTIA = '" + GARANTIA + "', ";

            sql += PERIODOENTREGA + ", ";
            informacion += "PERIODOENTREGA = '" + PERIODOENTREGA + "', ";

            sql += PERIODOPAGO + ", ";
            informacion += "PERIODOPAGO = '" + PERIODOPAGO + "', ";

            if (String.IsNullOrEmpty(FORMA_PAGO) == false)
            {
                sql += "'" + FORMA_PAGO + "', ";
                informacion += "FORMA_PAGO = '" + FORMA_PAGO + "', ";
            }
            else
            {
                MensajeError = "El campo FORMA_PAGO no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_ENTIDAD_BANCARIA != 0)
            {
                sql += ID_ENTIDAD_BANCARIA + ", ";
                informacion += "ID_ENTIDAD_BANCARIA = '" + ID_ENTIDAD_BANCARIA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_ENTIDAD_BANCARIA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CUENTA_BANCARIA) == false)
            {
                sql += "'" + CUENTA_BANCARIA + "', ";
                informacion += "CUENTA_BANCARIA = '" + CUENTA_BANCARIA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CUENTA_BANCARIA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(TIPO_CUENTA) == false)
            {
                sql += "'" + TIPO_CUENTA + "', ";
                informacion += "TIPO_CUENTA = '" + TIPO_CUENTA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "TIPO_CUENTA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CONDICIONES_ENTREGA) == false)
            {
                sql += "'" + CONDICIONES_ENTREGA + "', ";
                informacion += "CONDICIONES_ENTREGA = '" + CONDICIONES_ENTREGA + "', ";
            }
            else
            {
                MensajeError = "El campo CONDICIONES_ENTREGA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    DataTable tablaEmpleado = ValidarNumCuenta(CUENTA_BANCARIA, conexion);
                    if (tablaEmpleado.Rows.Count <= 0)
                    {
                        ID_NEGOCIACION = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                        if (ID_NEGOCIACION <= 0)
                        {
                            MensajeError = "ERROR: Al intentar ingresar el nuevo registro para la negociación.";
                            ejecutadoCorrectamente = false;
                            ID_NEGOCIACION = 0;
                        }
                        else
                        {
                            auditoria _auditoria = new auditoria(Empresa);
                            if (!(_auditoria.Adicionar(Usuario, tabla.ALM_NEGOCIACION_PROVEEDOR, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                            {
                                MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de la negociación.";
                                ejecutadoCorrectamente = false;
                                ID_NEGOCIACION = 0;
                            }
                        }
                    }
                    else
                    {
                        MensajeError = "ERROR: La cuenta bancaría se encuentra asociada a un trabajador de la empresa.";
                        ejecutadoCorrectamente = false;
                        ID_NEGOCIACION = 0;
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                    ID_NEGOCIACION = 0;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
                ID_NEGOCIACION = 0;
            }

            if (ejecutadoCorrectamente)
            {
                return ID_NEGOCIACION;
            }
            else
            {
                return 0;
            }
        }

        public Boolean ActualizarNegociacionProveedor(Decimal ID_NEGOCIACION,
            Decimal ID_PROVEEDOR,
            Int32 GARANTIA,
            Int32 PERIODOENTREGA,
            Int32 PERIODOPAGO,
            String FORMA_PAGO,
            Decimal ID_ENTIDAD_BANCARIA,
            String CUENTA_BANCARIA,
            String TIPO_CUENTA,
            String CONDICIONES_ENTREGA,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_NEGOCIACION_PROVEEDOR_ACTUALIZAR ";

            #region validaciones

            if (ID_NEGOCIACION != 0)
            {
                sql += ID_NEGOCIACION + ", ";
                informacion += "ID_NEGOCIACION = '" + ID_NEGOCIACION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_NEGOCIACION no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + ", ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PROVEEDOR no puede ser vacio.";
                ejecutar = false;
            }

            sql += GARANTIA + ", ";
            informacion += "GARANTIA = '" + GARANTIA + "', ";

            sql += PERIODOENTREGA + ", ";
            informacion += "PERIODOENTREGA = '" + PERIODOENTREGA + "', ";

            sql += PERIODOPAGO + ", ";
            informacion += "PERIODOPAGO = '" + PERIODOPAGO + "', ";

            if (String.IsNullOrEmpty(FORMA_PAGO) == false)
            {
                sql += "'" + FORMA_PAGO + "', ";
                informacion += "FORMA_PAGO = '" + FORMA_PAGO + "', ";
            }
            else
            {
                MensajeError = "El campo FORMA_PAGO no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_ENTIDAD_BANCARIA != 0)
            {
                sql += ID_ENTIDAD_BANCARIA + ", ";
                informacion += "ID_ENTIDAD_BANCARIA = '" + ID_ENTIDAD_BANCARIA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_ENTIDAD_BANCARIA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CUENTA_BANCARIA) == false)
            {
                sql += "'" + CUENTA_BANCARIA + "', ";
                informacion += "CUENTA_BANCARIA = '" + CUENTA_BANCARIA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CUENTA_BANCARIA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(TIPO_CUENTA) == false)
            {
                sql += "'" + TIPO_CUENTA + "', ";
                informacion += "TIPO_CUENTA = '" + TIPO_CUENTA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "TIPO_CUENTA = 'NULL', ";
            }

            if (String.IsNullOrEmpty(CONDICIONES_ENTREGA) == false)
            {
                sql += "'" + CONDICIONES_ENTREGA + "', ";
                informacion += "CONDICIONES_ENTREGA = '" + CONDICIONES_ENTREGA + "', ";
            }
            else
            {
                MensajeError = "El campo CONDICIONES_ENTREGA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    DataTable tablaEmpleado = ValidarNumCuenta(CUENTA_BANCARIA, conexion);
                    if (tablaEmpleado.Rows.Count <= 0)
                    {
                        if (conexion.ExecuteNonQuery(sql) <= 0)
                        {
                            MensajeError = "ERROR: Al intentar actualizar el registro de la negociación.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            auditoria _auditoria = new auditoria(Empresa);
                            if (!(_auditoria.Adicionar(Usuario, tabla.ALM_NEGOCIACION_PROVEEDOR, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                            {
                                MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de la negociación.";
                                ejecutadoCorrectamente = false;
                            }
                        }
                    }
                    else
                    {
                        MensajeError = "ERROR: La cuenta bancaría se encuentra asociada a un trabajador de la empresa.";
                        ejecutadoCorrectamente = false;
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
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

        #endregion alm_negociacion_proveedores

        #region consulta Num_cuenta
        public DataTable ValidarNumCuenta(String NUM_CUENTA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_NEGOCIACION_PROVEEDOR_VALIDAR_NUM_CUENTA ";

            #region validaciones
            if (String.IsNullOrEmpty(NUM_CUENTA) == false)
            {
                sql += "'" + NUM_CUENTA + "' ";
            }
            else
            {
                MensajeError = "El campo NUM_CUENTA no puede ser vacio.";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return _dataTable;
        }
        #endregion

        #endregion metodos
    }
}