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
    public class parametroSeguridadSocial
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
        public parametroSeguridadSocial(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public Decimal Adicionar(String DESCRIPCION, Decimal PORCENTAJE, String COD_CONTABLE)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_par_seguridad_social_adicionar ";

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
                    if (!(_auditoria.Adicionar(Usuario, tabla.PAR_SEGURIDAD_SOCIAL, tabla.ACCION_ADICIONAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
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

        public Boolean Actualizar(int ID_SEGURIDAD_SOCIAL, String DESCRIPCION, Decimal PORCENTAJE, String COD_CONTABLE)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_par_prestacionales_actualizar ";

            #region validaciones
            if (ID_SEGURIDAD_SOCIAL != 0)
            {
                sql += ID_SEGURIDAD_SOCIAL + ", ";
                informacion += "ID_SEGURIDAD_SOCIAL = " + ID_SEGURIDAD_SOCIAL + ", ";
            }
            else
            {
                MensajeError += "El campo ID_SEGURIDAD_SOCIAL no puede ser 0\n";
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
                    #region actualizar
                    if (conexion.ExecuteNonQuery(sql) == 0) ejecutadoCorrectamente = false;
                    #endregion actualizar

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.PAR_SEGURIDAD_SOCIAL, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
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

            sql = "usp_par_seguridad_social_obtenerTodos ";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.PAR_SEGURIDAD_SOCIAL, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPorIdSeguridadSocial(int ID_SEGURIDAD_SOCIAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_par_seguridad_social_obtenerPorIdSeguridadSocial " + ID_SEGURIDAD_SOCIAL;

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.PAR_SEGURIDAD_SOCIAL, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
