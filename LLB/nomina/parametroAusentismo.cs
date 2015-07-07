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
    public class parametroAusentismo
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
        public parametroAusentismo(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public Decimal Adicionar(Decimal ID_CONCEPTO, Decimal PORCENTAJE)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_par_ausentismos_adicionar ";

            #region validaciones
            if (ID_CONCEPTO != 0)
            {
                sql += "'" + ID_CONCEPTO + "', ";
                informacion += "ID_CONCEPTO = '" + ID_CONCEPTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONCEPTO no puede ser cero\n";
                ejecutar = false;
            }

            if (PORCENTAJE != 0)
            {
                sql += "'" + PORCENTAJE + "', ";
                informacion += "PORCENTAJE = '" + PORCENTAJE + "', ";
            }
            else
            {
                MensajeError += "El campo PORCENTAJE no puede ser cero\n";
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
                    if (!(_auditoria.Adicionar(Usuario, tabla.PAR_AUSENTISMOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
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

        public Boolean Actualizar(int ID_PAR_AUSENTISMOS, Decimal PORCENTAJE)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_par_ausentismos_actualizar ";

            #region validaciones
            if (ID_PAR_AUSENTISMOS != 0)
            {
                sql += ID_PAR_AUSENTISMOS + ", ";
                informacion += "ID_PAR_AUSENTISMOS = " + ID_PAR_AUSENTISMOS + ", ";
            }
            else
            {
                MensajeError += "El campo ID_PAR_AUSENTISMOS no puede ser 0\n";
                ejecutar = false;
            }

            if (PORCENTAJE != 0)
            {
                sql += "'" + PORCENTAJE + "', ";
                informacion += "PORCENTAJE = '" + PORCENTAJE + "', ";
            }
            else
            {
                MensajeError += "El campo PORCENTAJE no puede ser cero\n";
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
                    #region actualizar
                    if (conexion.ExecuteNonQuery(sql) == 0) ejecutadoCorrectamente = false;
                    #endregion actualizar

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.PAR_AUSENTISMOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
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

            sql = "usp_par_ausentismos_obtenerTodos ";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.PAR_AUSENTISMOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPorId(int ID_PAR_ausentismos)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_par_ausentismos_obtenerPorId " + ID_PAR_ausentismos;

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.PAR_AUSENTISMOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
