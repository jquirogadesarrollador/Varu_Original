using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.contratacion
{
    public class DocumentoEntregable
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
        public DocumentoEntregable()
        {

        }

        public DocumentoEntregable(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos
        public DataTable ObtenerTodos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_documentos_entregables_obtener_todos";

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

        public Decimal AdicionarRegistro(String NOMBRE)
        {
            Decimal ID_DOCUMENTO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_con_reg_documentos_entregables_adicionar ";

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

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();

                try
                {
                    #region adicionar entidad
                    ID_DOCUMENTO = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    #endregion adicionar entidad

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_DOCUMENTOS_ENTREGABLES, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                    {
                        ejecutadoCorrectamente = false;
                    }
                    #endregion auditoria

                    conexion.AceptarTransaccion();
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

            if (ejecutadoCorrectamente)
            {
                return ID_DOCUMENTO;
            }
            else
            {
                return 0;
            }
        }

        public DataTable ObtenerPorId(Decimal ID_DOCUMENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_documentos_entregables_obtener_por_id ";

            #region validaciones
            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO;
            }
            else
            {
                MensajeError = "El campo ID_DOCUMENTO no puede ser vacio.";
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

        public DataTable ObtenerPorEstado(Boolean ACTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_documentos_entregables_obtener_por_estado ";

            #region validaciones
            if (ACTIVO == true)
            {
                sql += "'True'";
            }
            else
            {
                sql += "'False'";
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

        public Boolean Actualizar(Decimal ID_DOCUMENTO,
            String Nombre,
            Boolean ACTIVO)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_con_reg_documentos_entregables_actualizar ";

            #region validaciones

            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
                informacion += "ID_DOCUMENTO = '" + ID_DOCUMENTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(Nombre) == false)
            {
                sql += "'" + Nombre + "', ";
                informacion += "nombre = '" + Nombre + "', ";
            }
            else
            {
                MensajeError += "El campo Nombre no puede ser nulo\n";
                ejecutar = false;
            }

            if (ACTIVO == true)
            {
                sql += "'True', ";
                informacion += "ACTIVO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "ACTIVO = 'False', ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar entidad
                    if (conexion.ExecuteNonQuery(sql) == 0) ejecutadoCorrectamente = false;
                    #endregion adicionar entidad

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.CON_REG_DOCUMENTOS_ENTREGABLES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
                    #endregion auditoria

                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (ejecutadoCorrectamente) return true;
            else return false;
        }
        #endregion metodos
    }
}