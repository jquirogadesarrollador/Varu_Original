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
    public class categoriasProveedorProducto
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
        public categoriasProveedorProducto(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region alm_categoria
        public DataTable ObtenerCategoriasProveedorTodas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_categoria_obtener_todos ";

            #region validaciones
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

        public DataTable ObtenerCategoriasProveedorTodasConCriteriosEvaluacion()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_categoria_obtener_todos_cpn_criterios_evaluacion ";

            #region validaciones
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

        public Decimal AdicionarNuevaCategoria(String NOMBRE, String DESCRIPCION)
        {
            Decimal ID_CATEGORIA = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_CATEGORIA_ADICIONAR ";

            #region validaciones

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError = "El campo DESCRIPCION no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    ID_CATEGORIA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_CATEGORIA <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para la categoria.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CATEGORIA, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de la categoría.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
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

            if (ejecutadoCorrectamente)
            {
                return ID_CATEGORIA;
            }
            else
            {
                return 0;
            }
        }

        public DataTable ObtenerCategoriasProveedorPorId(Decimal ID_CATEGORIA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_ALM_CATEGORIA_OBTENER_POR_ID ";

            #region validaciones
            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_CATEGORIA no puede ser 0";
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

        public Boolean ActualizarCategoriaProveedor(Decimal ID_CATEGORIA,
            String NOMBRE,
            string DESCRIPCION)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "USP_ALM_CATEGORIA_ACTUALIZAR ";

            #region validaciones

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CATEGORIA no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError = "El campo DESCRIPCION no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar actualizar el registro de la categoría.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CATEGORIA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro de la categoría.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
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

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion alm_categoria

        #endregion metodos
    }
}