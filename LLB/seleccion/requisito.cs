using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class requisito
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
        public requisito(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerPorIdentificador(Decimal ID_REQUERIMIENTO, Decimal ID_SOLICITUD)
        {
            return Llenar(ID_REQUERIMIENTO, ID_SOLICITUD);
        }

        private DataTable Llenar(Decimal ID_REQUERIMIENTO, Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_control_requisitos_obtenerPorIdentificador ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO + ", ";
                informacion += "ID_REQUERIMIENTO = " + ID_REQUERIMIENTO + ", ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = " + ID_SOLICITUD;
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser 0\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_CONTROL_REQUISITOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable VerificarRequisitos(Decimal ID_REQUERIMIENTO, Decimal ID_SOLICITUD)
        {
            return Llenar(ID_REQUERIMIENTO, ID_SOLICITUD);
        }

        public Boolean Derogar(DataTable _dataTable)
        {
            return Actualizar(_dataTable);
        }

        public Boolean Cumplir(DataTable _dataTable)
        {
            return Actualizar(_dataTable);
        }

        public Boolean CumplirSinDatosDeDerogacion(DataTable _dataTable)
        {
            return ActualizarSinDatosDeDerogacion(_dataTable);
        }

        private Boolean Actualizar(DataTable _dataTable)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            if (_dataTable.Rows.Count == 0)
            {
                MensajeError += "No existen filas para ser actualizadas\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    foreach (DataRow dataRow in _dataTable.Rows)
                    {
                        sql = "usp_sel_control_requisitos_actualizar ";

                        sql += String.IsNullOrEmpty(dataRow["ID_CONTROL_REQUISITOS"].ToString()) ? "Null, " : dataRow["ID_CONTROL_REQUISITOS"].ToString() + ",";
                        sql += String.IsNullOrEmpty(dataRow["ID_PRUEBA"].ToString()) ? "Null, " : dataRow["ID_PRUEBA"] + ",";
                        sql += String.IsNullOrEmpty(dataRow["ID_DOCUMENTO"].ToString()) ? "Null, " : dataRow["ID_DOCUMENTO"] + ",";
                        sql += String.IsNullOrEmpty(dataRow["CUMPLIDO"].ToString()) ? "Null, " : dataRow["CUMPLIDO"] + ",";
                        sql += String.IsNullOrEmpty(dataRow["DEROGADO"].ToString()) ? "Null, " : dataRow["DEROGADO"] + ",";
                        sql += String.IsNullOrEmpty(dataRow["OBSERVACIONES"].ToString()) ? "Null " : "'" + dataRow["OBSERVACIONES"] + "' ";
                        cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                        #region auditoria
                        #endregion auditoria
                    }
                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                    conexion.DeshacerTransaccion();
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            return ejecutadoCorrectamente;
        }




        private Boolean ActualizarSinDatosDeDerogacion(DataTable _dataTable)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            if (_dataTable.Rows.Count == 0)
            {
                MensajeError += "No existen filas para ser actualizadas.\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    foreach (DataRow dataRow in _dataTable.Rows)
                    {
                        sql = "usp_sel_control_requisitos_actualizar_para_cumplir_docs_solamente ";

                        sql += String.IsNullOrEmpty(dataRow["ID_CONTROL_REQUISITOS"].ToString()) ? "Null, " : dataRow["ID_CONTROL_REQUISITOS"].ToString() + ",";
                        sql += String.IsNullOrEmpty(dataRow["ID_PRUEBA"].ToString()) ? "Null, " : dataRow["ID_PRUEBA"] + ",";
                        sql += String.IsNullOrEmpty(dataRow["ID_DOCUMENTO"].ToString()) ? "Null, " : dataRow["ID_DOCUMENTO"] + ",";
                        sql += String.IsNullOrEmpty(dataRow["CUMPLIDO"].ToString()) ? "Null" : dataRow["CUMPLIDO"];
                        cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                        #region auditoria
                        #endregion auditoria
                    }
                    conexion.AceptarTransaccion();
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                    conexion.DeshacerTransaccion();
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            return ejecutadoCorrectamente;
        }



        #endregion metodos
    }
}
