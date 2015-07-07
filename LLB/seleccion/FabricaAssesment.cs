using System;
using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using System.Collections.Generic;
using System.Data;

namespace Brainsbits.LLB.seleccion
{
    public class FabricaAssesment
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
        public FabricaAssesment()
        {

        }
        public FabricaAssesment(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerAssesmentCenteActivos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_assesment_center_obtenerActivos";

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


        public DataTable ObtenerAssesmentCentePorId(Decimal ID_ASSESMENT_CENTER)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_assesment_center_obtenerPorId ";
            sql += ID_ASSESMENT_CENTER;

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

        public DataTable ObtenerDocumentoAssesmentPorIdDocumento(Decimal ID_DOCUMENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_documentos_assesment_obtenerPorIdDocumento ";
            sql += ID_DOCUMENTO;

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


        public DataTable ObtenerCompetenciasAssesmentCenteActivos(Decimal ID_ASSESMENT_CENTER, Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_competencias_assesment_center_obtenerActivos ";

            if (ID_ASSESMENT_CENTER != 0)
            {
                sql += ID_ASSESMENT_CENTER + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_ASSESMENT_CENTER no puede ser 0 o vacio.";
            }

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
            }
            else
            {
                sql += "0";
            }

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

        public DataTable ObtenerAplicacionCompetenciasPorSolicitudIngreso(Decimal ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_aplicacion_competencias_obtenerPorSolicitud ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

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