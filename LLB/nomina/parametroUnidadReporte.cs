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
    public class parametroUnidadReporte
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
        public parametroUnidadReporte(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public Decimal Adicionar(String DESCRIPCION, String DESC_ABREV, Boolean ACTIVO)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_par_unidad_reporte_adicionar ";

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

            if (!(String.IsNullOrEmpty(DESC_ABREV)))
            {
                sql += "'" + DESC_ABREV + "', ";
                informacion += "DESC_ABREV = '" + DESC_ABREV + "', ";
            }
            else
            {
                MensajeError += "El campo DESC_ABREV no puede ser nulo\n";
                ejecutar = false;
            }

            informacion += "ACTIVO = '" + ACTIVO + "' ";

            if (ACTIVO) sql += "1,";
            else sql += "0,";

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
                    if (!(_auditoria.Adicionar(Usuario, tabla.PAR_UNIDAD_REPORTE, tabla.ACCION_ADICIONAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
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

        public Boolean Actualizar(int ID_UNIDAD_REPORTE, String DESCRIPCION, String DESC_ABREV, Boolean ACTIVO)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_par_unidad_reporte_actualizar ";

            #region validaciones
            if (ID_UNIDAD_REPORTE != 0)
            {
                sql += ID_UNIDAD_REPORTE + ", ";
                informacion += "ID_UNIDAD_REPORTE = " + ID_UNIDAD_REPORTE + ", ";
            }
            else
            {
                MensajeError += "El campo ID_UNIDAD_REPORTE no puede ser 0\n";
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

            if (!(String.IsNullOrEmpty(DESC_ABREV)))
            {
                sql += "'" + DESC_ABREV + "', ";
                informacion += "DESC_ABREV = '" + DESC_ABREV + "', ";
            }
            else
            {
                MensajeError += "El campo DESC_ABREV no puede ser nulo\n";
                ejecutar = false;
            }

            informacion += "ACTIVO = '" + ACTIVO + "' ";

            if (ACTIVO) sql += "1,";
            else sql += "0,";


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
                    if (!(_auditoria.Adicionar(Usuario, tabla.PAR_UNIDAD_REPORTE, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
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

            sql = "usp_par_unidad_reporte_obtenerTodos ";

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

        public DataTable ObtenerPorIdUnidadReporte(int ID_UNIDAD_REPORTE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_par_unidad_reporte_obtenerPorIdUnidadReporte " + ID_UNIDAD_REPORTE;
            informacion = "ID_UNIDAD_REPORTE = " + ID_UNIDAD_REPORTE;
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.PAR_UNIDAD_REPORTE, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
