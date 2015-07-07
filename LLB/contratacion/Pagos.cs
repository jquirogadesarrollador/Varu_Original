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
    public class Pagos
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
        public Pagos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region Metodos
        public DataTable ObtenerPorIdContrato(Decimal ID_CONTRATO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "HOJA_DE_VIDA_ACUMULADOS_EMPLEADOS ";

            if (ID_CONTRATO != 0)
            {
                sql += ID_CONTRATO;
                informacion += "ID_CONTRATO = " + ID_CONTRATO;
            }
            else
            {
                MensajeError += "El campo ID_CONTRATO no puede ser nulo. \n";
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
        public DataTable ObtenerdetallePorIdContrato(Decimal ID_LIQ_NOMINA_EMPLEADOS)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "HOJA_DE_VIDA_ACUMULADOS_EMPLEADOS_DETALLE_PAGO ";

            if (ID_LIQ_NOMINA_EMPLEADOS != 0)
            {
                sql += ID_LIQ_NOMINA_EMPLEADOS;
                informacion += "ID_CONTRATO = " + ID_LIQ_NOMINA_EMPLEADOS;
            }
            else
            {
                MensajeError += "El campo ID_CONTRATO no puede ser nulo. \n";
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
        #endregion Metodos

    }
}
