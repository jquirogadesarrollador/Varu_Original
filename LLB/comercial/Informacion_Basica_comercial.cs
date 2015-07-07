using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.comercial
{
    public class Informacion_Basica_comercial
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        #endregion

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
        #endregion

        #region constructores

        public Informacion_Basica_comercial(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        public DataTable ObtenerinformacionBasicaComercial(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_INFO_BASICA_COMERCIAL ";

            if (REGISTRO != 0) sql += REGISTRO;
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0. \n";
                ejecutar = false;
            }

            if (ejecutar == true)
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

        public DataTable ObtenerInformacionBasicaporId_Empresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_INFO_BASICA_COMERCIAL ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA;
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar == true)
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


        public DataTable ActualizarInformacion_BASICA(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_INFO_BASICA_COMERCIAL_adicionar ";

            if (REGISTRO != 0) sql += REGISTRO;
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0. \n";
                ejecutar = false;
            }

            if (ejecutar == true)
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

        public Decimal Info_basica_existe(String PUESTO, Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            Decimal REGISTRO_CONTRATO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_INFO_BASICA_COMERCIAL_CUENTA  ";

            if (ID_EMPRESA != 0)
            {
                sql += " '" + PUESTO + "', " + " '" + ID_EMPRESA + "'";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0.";
                ejecutar = false;
            }
            if (ejecutar)
            {
                try
                {
                    REGISTRO_CONTRATO = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            return REGISTRO_CONTRATO;
        }

        public Decimal AdicionarInformacionBasicaComercial(String PUESTO, String ACLARACION, Decimal ID_EMPRESA, String SiNo)
        {

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            Decimal REGISTRO_CONTRATO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_INFO_BASICA_COMERCIAL_adicionar  ";

            if (ID_EMPRESA != 0)
            {
                sql += " '" + PUESTO + "', " + " '" + ACLARACION + "', '" + ID_EMPRESA + "', '" + SiNo + "'";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0.";
                ejecutar = false;
            }
            if (ejecutar)
            {
                try
                {
                    REGISTRO_CONTRATO = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    if (REGISTRO_CONTRATO <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar la información del contrato de servicio.";
                        REGISTRO_CONTRATO = 0;
                    }
                    else
                    {
                        MensajeError = "ERROR: Al intentar ingresar la auditoria del contrato de servicio.";
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO_CONTRATO = 0;
                }
            }
            return REGISTRO_CONTRATO;
        }
        public Decimal ActualizarInformacionBasicaComercial(String PUESTO, String ACLARACION, Decimal ID_EMPRESA, String SiNo)
        {

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            Decimal REGISTRO_CONTRATO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_INFO_BASICA_COMERCIAL_ACTUALIZAR  ";

            if (ID_EMPRESA != 0)
            {
                sql += " '" + PUESTO + "', " + " '" + ACLARACION + "', '" + ID_EMPRESA + "', '" + SiNo + "'";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0.";
                ejecutar = false;
            }
            if (ejecutar)
            {
                try
                {
                    REGISTRO_CONTRATO = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    if (REGISTRO_CONTRATO <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar la información del contrato de servicio.";
                        REGISTRO_CONTRATO = 0;
                    }
                    else
                    {
                        MensajeError = "ERROR: Al intentar ingresar la auditoria del contrato de servicio.";
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO_CONTRATO = 0;
                }
            }
            return REGISTRO_CONTRATO;
        }
    }
}
