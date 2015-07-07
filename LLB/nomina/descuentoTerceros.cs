using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.nomina
{
    public class descuentoTerceros
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
        public descuentoTerceros(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public Decimal Adicionar(String NATURALEZA_JURIDICA, String NUMERO_IDENTIFICACION, String RAZON_SOCIAL, String PRIMER_NOMBRE, String SEGUNDO_NOMBRE,
            String PRIMER_APELLIDO, String SEGUNDO_APELLIDO, String ID_CIUDAD, String DIRECCION, String TELEFONO, String EMAIL,
            String CONTACTO, String CARGO_CONTACTO, String DESCRIPCION, Decimal PORCENTAJE_ADMON, Int32 DIAS_PARA_PAGO, Int32 ORDEN_DESCUENTO)
        {
            Decimal ID_TERCERO_CREDITO = 0;
            Decimal ID_TERCERO = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                tercero _tercero = new tercero(Empresa, Usuario);
                DataTable _dataTable = _tercero.ObtenerPorNumeroIdentificacion(NUMERO_IDENTIFICACION);

                if (_dataTable.Rows.Count == 0)
                {
                    if (NATURALEZA_JURIDICA.ToString() == "PERSONA NATURAL") RAZON_SOCIAL = PRIMER_NOMBRE.ToString() + " " + SEGUNDO_NOMBRE.ToString() + " " + PRIMER_APELLIDO.ToString() + " " + SEGUNDO_APELLIDO;

                    ID_TERCERO = _tercero.Adicionar(NATURALEZA_JURIDICA, NUMERO_IDENTIFICACION, RAZON_SOCIAL, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, "ACTIVO", conexion);
                    if (ID_TERCERO == 0) conexion.DeshacerTransaccion();
                }
                else
                {
                    if (String.IsNullOrEmpty(_tercero.MensajeError))
                    {
                        DataRow _dataRow = _dataTable.Rows[0];
                        ID_TERCERO = Convert.ToDecimal(_dataRow["ID_TERCERO"]);
                    }
                }

                if (String.IsNullOrEmpty(_tercero.MensajeError))
                {
                    ID_TERCERO_CREDITO = Adicionar(ID_TERCERO, ID_CIUDAD, DIRECCION, TELEFONO, EMAIL, CONTACTO, CARGO_CONTACTO, PORCENTAJE_ADMON, DIAS_PARA_PAGO, DESCRIPCION, "ACTIVO", ORDEN_DESCUENTO, conexion);

                    if (ID_TERCERO_CREDITO == 0) conexion.DeshacerTransaccion();
                    else conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_TERCERO_CREDITO;
        }

        private Decimal Adicionar(Decimal ID_TERCERO, String ID_CIUDAD, String DIRECCION, String TELEFONO, String EMAIL, String CONTACTO, String CARGO_CONTACTO,
            Decimal PORCENTAJE_ADMON, Int32 DIAS_PARA_PAGO, String DESCRIPCION, String ESTADO, Int32 ORDEN_DESCUENTO, Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_desc_terceros_adicionar ";

            #region validaciones
            if (ID_TERCERO != 0)
            {
                sql += ID_TERCERO + ", ";
                informacion += "ID_TERCERO = '" + ID_TERCERO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_TERCERO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIRECCION)))
            {
                sql += "'" + DIRECCION + "', ";
                informacion += "DIRECCION = '" + DIRECCION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DIRECCION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + TELEFONO + "', ";
            informacion += "TELEFONO = '" + TELEFONO.ToString() + "', ";

            sql += "'" + EMAIL + "', ";
            informacion += "EMAIL = '" + EMAIL + "', ";
            sql += "'" + CONTACTO + "', ";
            informacion += "CONTACTO = '" + CONTACTO + "', ";
            sql += "'" + CARGO_CONTACTO + "', ";
            informacion += "CARGO_CONTACTO = '" + CARGO_CONTACTO + "', ";
            sql += PORCENTAJE_ADMON + ", ";
            informacion += "PORCENTAJE_ADMON = '" + PORCENTAJE_ADMON.ToString() + "', ";
            sql += DIAS_PARA_PAGO + ", ";
            informacion += "DIAS_PARA_PAGO = '" + DIAS_PARA_PAGO.ToString() + "', ";
            sql += "'" + DESCRIPCION + "', ";
            informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += ORDEN_DESCUENTO + ", ";
            informacion += "ORDEN_DESCUENTO = '" + ORDEN_DESCUENTO.ToString() + "', ";

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.TERCEROS_CREDITOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Boolean Actualizar(Decimal ID_TERCEROS_CREDITOS, String ID_CIUDAD, String DIRECCION, String TELEFONO, String EMAIL, String CONTACTO, String CARGO_CONTACTO,
            Decimal PORCENTAJE_ADMON, Int32 DIAS_PARA_PAGO, String DESCRIPCION, String ESTADO, Int32 ORDEN_DESCUENTO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_desc_terceros_actualizar ";

            #region validaciones
            if (ID_TERCEROS_CREDITOS != 0)
            {
                sql += ID_TERCEROS_CREDITOS + ", ";
                informacion += "ID_TERCEROS_CREDITOS = '" + ID_TERCEROS_CREDITOS + "', ";
            }
            else
            {
                MensajeError += "El campo ID_TERCEROS_CREDITOS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIRECCION)))
            {
                sql += "'" + DIRECCION + "', ";
                informacion += "DIRECCION = '" + DIRECCION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DIRECCION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TELEFONO)))
            {
                sql += "'" + TELEFONO + "', ";
                informacion += "TELEFONO = '" + TELEFONO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EMAIL)))
            {
                sql += "'" + EMAIL + "', ";
                informacion += "EMAIL = '" + EMAIL + "', ";
            }
            else
            {
                MensajeError += "El campo EMAIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONTACTO)))
            {
                sql += "'" + CONTACTO + "', ";
                informacion += "CONTACTO = '" + CONTACTO + "', ";
            }
            else
            {
                MensajeError += "El campo CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CARGO_CONTACTO)))
            {
                sql += "'" + CARGO_CONTACTO + "', ";
                informacion += "CARGO_CONTACTO = '" + CARGO_CONTACTO + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO_CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (PORCENTAJE_ADMON != 0)
            {
                sql += PORCENTAJE_ADMON + ", ";
                informacion += "PORCENTAJE_ADMON = '" + PORCENTAJE_ADMON.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PORCENTAJE_ADMON no puede ser cero\n";
                ejecutar = false;
            }

            if (DIAS_PARA_PAGO != 0)
            {
                sql += DIAS_PARA_PAGO + ", ";
                informacion += "DIAS_PARA_PAGO = '" + DIAS_PARA_PAGO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DIAS_PARA_PAGO no puede ser cero\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ORDEN_DESCUENTO != 0)
            {
                sql += ORDEN_DESCUENTO + ", ";
                informacion += "ORDEN_DESCUENTO = '" + ORDEN_DESCUENTO.ToString() + "', ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.TERCEROS_CREDITOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerPorNombre(String NOMBRE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_desc_terceros_obtenerPorNombre ";

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + NOMBRE.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PROVEEDOR, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerPorNumeroIdentificacion(String NIT)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_desc_terceros_obtenerPorNumeroIdentificacion ";

            if (!(String.IsNullOrEmpty(NIT)))
            {
                sql += "'" + NIT + "'";
                informacion += "NIT= '" + NIT.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NIT no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PROVEEDOR, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerPorId(Decimal REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_desc_terceros_obtenerPorId ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
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

        public DataTable ObtenerTodos()
        {
            DataSet _dataSet = new DataSet();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_desc_terceros_obtenerTodos ";

            Conexion conexion = new Conexion(Empresa);
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataTable = _dataSet.Tables[0];
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

        public String ObtenerMaximoOrdenDescuento()
        {
            String sql = null;
            String maximo = null;

            sql = "usp_desc_terceros_obtenerOrdenMaximo ";

            Conexion conexion = new Conexion(Empresa);
            try
            {
                maximo = conexion.ExecuteScalar(sql);
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return maximo;
        }

        public String ObtenerMinimoOrdenDescuento()
        {
            String sql = null;
            String minimo = null;

            sql = "usp_desc_terceros_obtenerOrdenMinimo ";

            Conexion conexion = new Conexion(Empresa);
            try
            {
                minimo = conexion.ExecuteScalar(sql);
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return minimo;
        }
        #endregion
    }
}