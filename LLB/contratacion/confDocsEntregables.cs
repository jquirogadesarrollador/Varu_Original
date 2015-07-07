using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.contratacion
{
    public class ConfDocEntregable
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
        public ConfDocEntregable()
        {

        }

        public ConfDocEntregable(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public DataTable ObtenerPorEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_configuracion_docs_entregables_obtenerPorIdEmpresa ";

            #region validaciones

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = " + ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser 0\n";
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

        public DataTable ObtenerPorEmpresa(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_configuracion_docs_entregables_obtenerPorIdEmpresa ";

            #region validaciones

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = " + ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser 0\n";
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


        public Decimal Adicionar(Decimal ID_EMPRESA,
            Boolean ENTREGA_DOCUMENTOS,
            String DOCUMENTOS_SELECCION,
            String DOCUMENTOS_CONTRATACION,
            Decimal ID_CONTACTO_SELECCION,
            Decimal ID_CONTACTO_CONTRATACION)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_con_configuracion_docs_entregables_adicionar ";

            #region validaciones

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ENTREGA_DOCUMENTOS == true)
            {
                sql += "'True', ";
                informacion += "ENTREGA_DOCUMENTOS = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "ENTREGA_DOCUMENTOS = 'False', ";
            }

            if (!(String.IsNullOrEmpty(DOCUMENTOS_SELECCION)))
            {
                sql += "'" + DOCUMENTOS_SELECCION + "', ";
                informacion += "DOCUMENTOS_SELECCION = '" + DOCUMENTOS_SELECCION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DOCUMENTOS_SELECCION = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(DOCUMENTOS_CONTRATACION)))
            {
                sql += "'" + DOCUMENTOS_CONTRATACION + "', ";
                informacion += "DOCUMENTOS_CONTRATACION = '" + DOCUMENTOS_CONTRATACION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DOCUMENTOS_CONTRATAION = 'NULL', ";
            }

            if (ID_CONTACTO_SELECCION != 0)
            {
                sql += ID_CONTACTO_SELECCION + ", ";
                informacion += "ID_CONTACTO_SELECCION = '" + ID_CONTACTO_SELECCION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CONTACTO_SELECCION = 'NULL', ";
            }

            if (ID_CONTACTO_CONTRATACION != 0)
            {
                sql += ID_CONTACTO_CONTRATACION + ", ";
                informacion += "ID_CONTACTO_CONTRATACION = '" + ID_CONTACTO_CONTRATACION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CONTACTO_CONTRATACION = 'NULL', ";
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
                    #region adicionar entidad
                    ID = conexion.ExecuteScalar(sql);
                    #endregion adicionar entidad

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.CON_CONFIGURACION_DOCS_ENTREGABLES, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
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

            if (ejecutadoCorrectamente) return Convert.ToDecimal(ID);
            else return 0;
        }

        public Boolean Actualizar(Decimal ID_CONFIGURACION,
            Boolean ENTREGA_DOCUMENTOS,
            String DOCUMENTOS_SELECCION,
            String DOCUMENTOS_CONTRATACION,
            Decimal ID_CONTACTO_SELECCION,
            Decimal ID_CONTACTO_CONTRATACION)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_con_configuracion_docs_entregables_actualizar ";

            #region validaciones

            if (ID_CONFIGURACION != 0)
            {
                sql += ID_CONFIGURACION + ", ";
                informacion += "ID_CONFIGURACION = '" + ID_CONFIGURACION + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONFIGURACION no puede ser 0\n";
                ejecutar = false;
            }

            if (ENTREGA_DOCUMENTOS == true)
            {
                sql += "'True', ";
                informacion += "ENTREGA_DOCUMENTOS = '" + ENTREGA_DOCUMENTOS + "', ";
            }
            else
            {
                sql += "True, ";
                informacion += "ENTREGA_DOCUMENTOS = 'True', ";
            }

            if (!(String.IsNullOrEmpty(DOCUMENTOS_SELECCION)))
            {
                sql += "'" + DOCUMENTOS_SELECCION + "', ";
                informacion += "DOCUMENTOS_SELECCION = '" + DOCUMENTOS_SELECCION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DOCUMENTOS_SELECCION = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(DOCUMENTOS_CONTRATACION)))
            {
                sql += "'" + DOCUMENTOS_CONTRATACION + "', ";
                informacion += "DOCUMENTOS_CONTRATACION = '" + DOCUMENTOS_CONTRATACION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DOCUMENTOS_CONTRATACION = 'NULL', ";
            }

            if (ID_CONTACTO_SELECCION != 0)
            {
                sql += ID_CONTACTO_SELECCION + ", ";
                informacion += "ID_CONTACTO_SELECCION = '" + ID_CONTACTO_SELECCION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CONTACTO_SELECCION = 'NULL', ";
            }

            if (ID_CONTACTO_CONTRATACION != 0)
            {
                sql += ID_CONTACTO_CONTRATACION + ", ";
                informacion += "ID_CONTACTO_CONTRATACION = '" + ID_CONTACTO_CONTRATACION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CONTACTO_CONTRATACION = 'NULL', ";
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
                    if (!(_auditoria.Adicionar(Usuario, tabla.CON_CONFIGURACION_DOCS_ENTREGABLES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
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

            if (ejecutadoCorrectamente) return true;
            else return false;
        }
        #endregion metodos
    }
}