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
    public class DocumentoProveedor
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
        public DocumentoProveedor(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        public DocumentoProveedor()
        {

        }
        #endregion

        #region metodos

        public Decimal AdicionarDocumentoAProveedor(Decimal ID_PROVEEDOR,
            byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String TIPO_DOC,
            String DESCRIPCION)
        {
            Conexion conexion = new Conexion(Empresa);

            Decimal ID_DOCUMENTO_PROV = 0;
            try
            {
                ID_DOCUMENTO_PROV = Convert.ToDecimal(conexion.ExecuteEscalarParaAdicionaDocumentoProveedor(ID_PROVEEDOR, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE, TIPO_DOC, DESCRIPCION, Usuario));
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                ID_DOCUMENTO_PROV = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_DOCUMENTO_PROV;
        }

        public Boolean ActualizarDocumentoAProveedor(Decimal ID_DOCUMENTO_PROV,
            byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String TIPO_DOC,
            String DESCRIPCION)
        {
            Conexion conexion = new Conexion(Empresa);

            int numRegistrosActualizados = 0;

            try
            {
                numRegistrosActualizados = conexion.ExecuteNonQueryParaActualizarDocumentoParaProveedor(ID_DOCUMENTO_PROV, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE, TIPO_DOC, DESCRIPCION, Usuario);

                if (numRegistrosActualizados <= 0)
                {
                    MensajeError = "No se actualizó ningún registro enla base de datos. Error en USP. de actualziar documento de Proveedor.";
                    numRegistrosActualizados = 0;
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                numRegistrosActualizados = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            if (numRegistrosActualizados <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean EliminarDocumentoProveedor(Decimal ID_DOCUMENTO_PROV)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            sql = "usp_alm_reg_docs_proveedor_eliminar ";

            #region validaciones
            if (ID_DOCUMENTO_PROV != 0)
            {
                sql += ID_DOCUMENTO_PROV;
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO_PROV no puede ser nulo.\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                    numRegistrosAfectados = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (numRegistrosAfectados <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable ObtenerDocumentosAsociadosAProveedor(Decimal ID_PROVEEDOR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_docs_proveedor ";

            #region validaciones
            if (ID_PROVEEDOR != 0)
            {
                sql += ID_PROVEEDOR;
            }
            else
            {
                MensajeError += "El campo ID_PROVEEDOR no puede ser nulo\n";
                ejecutar = false;
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

        public DataTable ObtenerDocumentoProveedorPorIdDocumento(Decimal ID_DOCUMENTO_PROV)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_docs_proveedor_obtenerPorIdDocumento ";

            #region validaciones
            if (ID_DOCUMENTO_PROV != 0)
            {
                sql += ID_DOCUMENTO_PROV;
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO_PROV no puede ser nulo\n";
                ejecutar = false;
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
        #endregion metodos
    }
}