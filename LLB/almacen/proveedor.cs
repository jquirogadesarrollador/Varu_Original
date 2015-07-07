using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.almacen
{
    public class proveedor
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
        public proveedor(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarAlmRegProveedor(String NATURALEZA_JURIDICA,
            String NUMERO_IDENTIFICACION,
            String RAZON_SOCIAL,
            String PRIMER_NOMBRE,
            String SEGUNDO_NOMBRE,
            String PRIMER_APELLIDO,
            String SEGUNDO_APELLIDO,
            Decimal ID_CATEGORIA,
            String ID_CIUDAD,
            String DIRECCION,
            String TELEFONO,
            String MAIL,
            String CONTACTO,
            String CARGO_CONTACTO,
            String DESCRIPCION,
            String CLASIFICACION,
            Int32 GARANTIA,
            Int32 PERIODOENTREGA,
            Int32 PERIODOPAGO,
            String FORMA_PAGO,
            Decimal ID_ENTIDAD_BANCARIA,
            String CUENTA_BANCARIA,
            String TIPO_CUENTA,
            String CONDICIONES_ENTREGA,
            String UBI_SECTOR)
        {
            Decimal ID_PROVEEDOR = 0;
            Decimal ID_TERCERO = 0;
            Decimal ID_NEGOCIACION = 0;

            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                tercero _tercero = new tercero(Empresa, Usuario);
                DataTable tablainfoTercero = _tercero.ObtenerPorNumeroIdentificacion(NUMERO_IDENTIFICACION);

                if (tablainfoTercero.Rows.Count <= 0)
                {
                    ID_TERCERO = _tercero.Adicionar(NATURALEZA_JURIDICA, NUMERO_IDENTIFICACION, RAZON_SOCIAL, PRIMER_NOMBRE, SEGUNDO_NOMBRE, PRIMER_APELLIDO, SEGUNDO_APELLIDO, "ACTIVO", conexion);

                    if (ID_TERCERO <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = _tercero.MensajeError;
                        verificador = false;
                        ID_PROVEEDOR = 0;
                        ID_TERCERO = 0;
                        ID_NEGOCIACION = 0;
                    }
                }
                else
                {
                    if (_tercero.MensajeError == null)
                    {
                        DataRow filaInfoTervero = tablainfoTercero.Rows[0];
                        ID_TERCERO = Convert.ToDecimal(filaInfoTervero["ID_TERCERO"]);
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = _tercero.MensajeError;
                        verificador = false;
                        ID_PROVEEDOR = 0;
                        ID_TERCERO = 0;
                        ID_NEGOCIACION = 0;
                    }
                }

                if (verificador == true)
                {
                    ID_PROVEEDOR = AdicionarInfoProveedor(ID_TERCERO, ID_CATEGORIA, ID_CIUDAD, DIRECCION, TELEFONO, MAIL, CONTACTO, CARGO_CONTACTO, "N", DESCRIPCION, CLASIFICACION, UBI_SECTOR, conexion);

                    if (ID_PROVEEDOR <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        verificador = false;
                        ID_PROVEEDOR = 0;
                        ID_TERCERO = 0;
                        ID_NEGOCIACION = 0;
                    }
                    else
                    {
                        negociacionProveedores _negociacion = new negociacionProveedores(Empresa, Usuario);

                        ID_NEGOCIACION = _negociacion.AdicionarNuevaNegociacion(ID_PROVEEDOR, GARANTIA, PERIODOENTREGA, PERIODOPAGO, FORMA_PAGO, ID_ENTIDAD_BANCARIA, CUENTA_BANCARIA, TIPO_CUENTA, CONDICIONES_ENTREGA, conexion);

                        if (ID_NEGOCIACION <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = _negociacion.MensajeError;
                            ID_PROVEEDOR = 0;
                            ID_TERCERO = 0;
                            ID_NEGOCIACION = 0;
                        }
                    }
                }

                if (verificador == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                verificador = false;
                ID_PROVEEDOR = 0;
                ID_TERCERO = 0;
                ID_NEGOCIACION = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            if (verificador == true)
            {
                return ID_PROVEEDOR;
            }
            else
            {
                return 0;
            }
        }

        public Decimal AdicionarInfoProveedor(Decimal ID_TERCERO,
            Decimal ID_CATEGORIA,
            String ID_CIUDAD,
            String DIRECCION,
            String TELEFONO,
            String MAIL,
            String CONTACTO,
            String CARGO_CONTACTO,
            String ESTADO,
            String DESCRIPCION,
            String CLASIFICACION,
            String UBI_SECTOR,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_REG_PROVEEDOR_ADICIONAR ";

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

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
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

            if (!(String.IsNullOrEmpty(MAIL)))
            {
                sql += "'" + MAIL + "', ";
                informacion += "MAIL = '" + MAIL + "', ";
            }
            else
            {
                MensajeError += "El campo MAIL no puede ser nulo\n";
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

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(CLASIFICACION) == false)
            {
                sql += "'" + CLASIFICACION + "', ";
                informacion += "CLASIFICACION = '" + CLASIFICACION + "', ";
            }
            else
            {
                MensajeError += "El campo CLASIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(UBI_SECTOR) == false)
            {
                sql += "'" + UBI_SECTOR + "'";
                informacion += "UBI_SECTOR = '" + UBI_SECTOR + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "UBI_SECTOR = 'NULL'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PROVEEDOR, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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


        public Boolean ActualizarAlmRegProveedor(Decimal ID_PROVEEDOR,
            Decimal ID_CATEGORIA,
            String ID_CIUDAD,
            String DIRECCION,
            String TELEFONO,
            String MAIL,
            String CONTACTO,
            String CARGO_CONTACTO,
            String DESCRIPCION,
            String CLASIFICACION,
            String UBI_SECTOR,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_REG_PROVEEDOR_ACTUALIZAR ";

            #region validaciones
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR + ", ";
                informacion += "ID_PROVEEDOR = '" + ID_PROVEEDOR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIRECCION)))
            {
                sql += "'" + DIRECCION + "', ";
                informacion += "DIRECCION= '" + DIRECCION.ToString() + "', ";
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

            if (!(String.IsNullOrEmpty(MAIL)))
            {
                sql += "'" + MAIL + "', ";
                informacion += "MAIL = '" + MAIL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MAIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONTACTO)))
            {
                sql += "'" + CONTACTO + "', ";
                informacion += "CONTACTO = '" + CONTACTO.ToString() + "', ";
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

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(CLASIFICACION) == false)
            {
                sql += "'" + CLASIFICACION + "', ";
                informacion += "CLASIFICACION = '" + CLASIFICACION + "', ";
            }
            else
            {
                MensajeError += "El campo CLASIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(UBI_SECTOR) == false)
            {
                sql += "'" + UBI_SECTOR + "'";
                informacion += "UBI_SECTOR = '" + UBI_SECTOR + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "UBI_SECTOR = 'NULL'";
            }


            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_REG_PROVEEDOR, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean ActualizarProveedorYNegociacion(Decimal ID_PROVEEDOR,
            Decimal ID_CATEGORIA,
            String ID_CIUDAD,
            String DIRECCION,
            String TELEFONO,
            String MAIL,
            String CONTACTO,
            String CARGO_CONTACTO,
            String DESCRIPCION,
            String CLASIFICACION,
            Decimal ID_NEGOCIACION,
            Int32 GARANTIA,
            Int32 PERIODOENTREGA,
            Int32 PERIODOPAGO,
            String FORMA_PAGO,
            Decimal ID_ENTIDAD_BANCARIA,
            String CUENTA_BANCARIA,
            String TIPO_CUENTA,
            String CONDICIONES_ENTREGA,
            String UBI_SECTOR)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarAlmRegProveedor(ID_PROVEEDOR, ID_CATEGORIA, ID_CIUDAD, DIRECCION, TELEFONO, MAIL, CONTACTO, CARGO_CONTACTO, DESCRIPCION, CLASIFICACION, UBI_SECTOR, conexion) == false)
                {
                    correcto = false;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    negociacionProveedores _negociacion = new negociacionProveedores(Empresa, Usuario);
                    if (ID_NEGOCIACION <= 0)
                    {
                        ID_NEGOCIACION = _negociacion.AdicionarNuevaNegociacion(ID_PROVEEDOR, GARANTIA, PERIODOENTREGA, PERIODOPAGO, FORMA_PAGO, ID_ENTIDAD_BANCARIA, CUENTA_BANCARIA, TIPO_CUENTA, CONDICIONES_ENTREGA, conexion);

                        if (ID_NEGOCIACION <= 0)
                        {
                            correcto = false;
                            MensajeError = _negociacion.MensajeError;
                            conexion.DeshacerTransaccion();
                        }
                    }
                    else
                    {
                        if (_negociacion.ActualizarNegociacionProveedor(ID_NEGOCIACION, ID_PROVEEDOR, GARANTIA, PERIODOENTREGA, PERIODOPAGO, FORMA_PAGO, ID_ENTIDAD_BANCARIA, CUENTA_BANCARIA, TIPO_CUENTA, CONDICIONES_ENTREGA, conexion) == false)
                        {
                            correcto = false;
                            MensajeError = _negociacion.MensajeError;
                            conexion.DeshacerTransaccion();
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public DataTable ObtenerAlmRegProveedorPorNombre(String NOM_PROVEEDOR)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PROVEEDOR_OBTENER_POR_NOMBRE ";

            if (!(String.IsNullOrEmpty(NOM_PROVEEDOR)))
            {
                sql += "'" + NOM_PROVEEDOR + "'";
                informacion += "NOM_PROVEEDOR= '" + NOM_PROVEEDOR.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NOM_PROVEEDOR no puede ser nulo\n";
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




        public DataTable ObtenerGarantiasDeUnProveedor(Decimal ID_PROVEEDOR)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_proveedor_obtenerGarantias ";

            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR;
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
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


        public DataTable ObtenerAlmRegProveedorLaboratoriosPorNombre(String NOM_PROVEEDOR)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PROVEEDOR_OBTENER_POR_NOMBRE "; 

            if (!(String.IsNullOrEmpty(NOM_PROVEEDOR)))
            {
                sql += "'" + NOM_PROVEEDOR + "'";
                informacion += "NOM_PROVEEDOR= '" + NOM_PROVEEDOR.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo NOM_PROVEEDOR no puede ser nulo\n";
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

        public DataTable ObtenerAlmRegProveedorPorNombreConInfoCriteriosCalificacion(String NOM_PROVEEDOR, String TIPO_PROCESO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_proveedor_obtener_por_nombre_y_tipo_con_info_criterios ";

            if (!(String.IsNullOrEmpty(NOM_PROVEEDOR)))
            {
                sql += "'" + NOM_PROVEEDOR + "', ";
                informacion += "NOM_PROVEEDOR= '" + NOM_PROVEEDOR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PROCESO)))
            {
                sql += "'" + TIPO_PROCESO + "'";
                informacion += "TIPO_PROCESO = '" + TIPO_PROCESO + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_PROCESO no puede ser nulo\n";
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


        public DataTable ObtenerAlmRegProveedorPorNombreConInfoNegociacion(String RAZON_SOCIAL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_proveedor_obtener_por_razonSocial_con_info_negociacion ";

            if (!(String.IsNullOrEmpty(RAZON_SOCIAL)))
            {
                sql += "'" + RAZON_SOCIAL + "'";
                informacion += "RAZON_SOCIAL = '" + RAZON_SOCIAL + "'";
            }
            else
            {
                MensajeError += "El campo RAZON_SOCIAL no puede ser nulo\n";
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

        public DataTable ObtenerAlmRegProveedoresConInfoCategoriaConCriterios(String TIPO_PROCESO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_proveedor_obtener_todos_con_info_criterios_por_tipo ";

            if (!(String.IsNullOrEmpty(TIPO_PROCESO)))
            {
                sql += "'" + TIPO_PROCESO + "'";
                informacion += "TIPO_PROCESO = '" + TIPO_PROCESO + "'";
            }
            else
            {
                MensajeError += "El campo NOM_PROVEEDOR no puede ser nulo\n";
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

        public DataTable ObtenerAlmRegProveedoresConInfoNegociacion()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_proveedor_obtener_activos_con_info_negociacion ";

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

        public DataTable ObtenerProveedoresActivos()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_proveedor_obtener_todos_activos";

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

        public DataTable ObtenerProveedoresLaboratoriosActivos()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_proveedor_laboratorio_obtener_todos_activos";

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


        public DataTable ObtenerAlmRegProveedorPorNit(String NIT)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PROVEEDOR_OBTENER_POR_NIT ";

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

        public DataTable ObtenerAlmRegProveedorLaboratoriosPorNit(String NIT)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PROVEEDOR_OBTENER_POR_NIT_SOLO_LABORATORIOS ";

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

        public DataTable ObtenerAlmRegProveedorPorNitConinfoCriteriosCalificacion(String NIT, String TIPO_PROCESO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_proveedor_obtener_por_nit_y_tipo_con_info_criterios ";

            if (!(String.IsNullOrEmpty(NIT)))
            {
                sql += "'" + NIT + "', ";
                informacion += "NIT= '" + NIT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NIT no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PROCESO)))
            {
                sql += "'" + TIPO_PROCESO + "'";
                informacion += "NIT= '" + TIPO_PROCESO + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_PROCESO no puede ser nulo\n";
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

        public DataTable ObtenerAlmRegProveedorPorNitConInfoNegociacion(String NUMERO_IDENTIFICACION)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_proveedor_obtener_por_nit_con_info_negociacion ";

            if (!(String.IsNullOrEmpty(NUMERO_IDENTIFICACION)))
            {
                sql += "'" + NUMERO_IDENTIFICACION + "'";
                informacion += "NIT= '" + NUMERO_IDENTIFICACION + "'";
            }
            else
            {
                MensajeError += "El campo NUMERO_IDENTIFICACION no puede ser nulo\n";
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


        public DataTable ObtenerAlmRegProveedorPorRegistro(Decimal REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_REG_PROVEEDOR_OBTENER_POR_REGISTRO ";

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



        #region con_reg_productos_provedor

        #endregion con_reg_productos_provedor

        #endregion
    }
}
