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
    public class parametroPrestacional
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
        public parametroPrestacional(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public Decimal Adicionar(String DESCRIPCION, Decimal PORCENTAJE, Decimal VALOR, String COD_CONTABLE)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_par_prestacionales_adicionar ";

            #region validaciones
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

            if ((PORCENTAJE != 0)
                && (VALOR != 0))
            {
                MensajeError += "Solo debe ingresar PORCENTAJE ó VALOR\n";
                ejecutar = false;
            }

            if ((PORCENTAJE == 0)
                && (VALOR == 0))
            {
                MensajeError += "Debe ingresar PORCENTAJE ó VALOR\n";
                ejecutar = false;
            }
            else
            {
                if (PORCENTAJE != 0)
                {
                    sql += "'" + PORCENTAJE + "', ";
                    informacion += "PORCENTAJE = '" + PORCENTAJE + "', ";
                }

                else
                {
                    sql += "null,";
                    informacion += "PORCENTAJE = 'null',";

                }
                if (VALOR != 0)
                {
                    sql += "'" + VALOR + "', ";
                    informacion += "VALOR = '" + VALOR + "', ";
                }

                else
                {
                    sql += "null,";
                    informacion += "VALOR = 'null',";
                }
            }

            if (!(String.IsNullOrEmpty(COD_CONTABLE)))
            {
                sql += "'" + COD_CONTABLE + "', ";
                informacion += "COD_CONTABLE = '" + COD_CONTABLE + "', ";
            }
            else
            {
                MensajeError += "El campo COD_CONTABLE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar
                    ID = conexion.ExecuteScalar(sql);
                    #endregion adicionar

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.PAR_PRESTACIONALES, tabla.ACCION_ADICIONAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
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

            if (!(String.IsNullOrEmpty(ID))) return Convert.ToDecimal(ID);
            else return 0;
        }

        public Boolean Actualizar(int ID_PRESTACIONAL, String DESCRIPCION, Decimal PORCENTAJE, Decimal VALOR, String COD_CONTABLE)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_par_prestacionales_actualizar ";

            #region validaciones
            if (ID_PRESTACIONAL != 0)
            {
                sql += ID_PRESTACIONAL + ", ";
                informacion += "ID_PRESTACIONAL = " + ID_PRESTACIONAL + ", ";
            }
            else
            {
                MensajeError += "El campo ID_PRESTACIONAL no puede ser 0\n";
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

            if ((PORCENTAJE != 0)
                && (VALOR != 0))
            {
                MensajeError += "Solo debe ingresar PORCENTAJE ó VALOR\n";
                ejecutar = false;
            }

            if ((PORCENTAJE == 0)
                && (VALOR == 0))
            {
                MensajeError += "Debe ingresar PORCENTAJE ó VALOR\n";
                ejecutar = false;
            }
            else
            {
                if (PORCENTAJE != 0)
                {
                    sql += "'" + PORCENTAJE + "', ";
                    informacion += "PORCENTAJE = '" + PORCENTAJE + "', ";
                }

                else
                {
                    sql += "null,";
                    informacion += "PORCENTAJE = 'null',";

                }
                if (VALOR != 0)
                {
                    sql += "'" + VALOR + "', ";
                    informacion += "VALOR = '" + VALOR + "', ";
                }

                else
                {
                    sql += "null,";
                    informacion += "VALOR = 'null',";
                }
            }

            if (!(String.IsNullOrEmpty(COD_CONTABLE)))
            {
                sql += "'" + COD_CONTABLE + "', ";
                informacion += "COD_CONTABLE = '" + COD_CONTABLE + "', ";
            }
            else
            {
                MensajeError += "El campo COD_CONTABLE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar prestacion
                    if (conexion.ExecuteNonQuery(sql) == 0) ejecutadoCorrectamente = false;
                    #endregion adicionar entidad

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.PAR_PRESTACIONALES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
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
            else ejecutadoCorrectamente = false;

            if (ejecutadoCorrectamente) return true;
            else return false;
        }

        public DataTable ObtenerTodas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_par_prestacionales_obtenerTodos ";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.PAR_PRESTACIONALES, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

            return _dataTable;
        }

        public DataTable ObtenerPorIdPrestacional(int ID_PRESTACIONAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_par_prestacionales_obtenerPorIdPrestacional " + ID_PRESTACIONAL;

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.PAR_PRESTACIONALES, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

            return _dataTable;
        }
        #endregion metodos
    }
}
