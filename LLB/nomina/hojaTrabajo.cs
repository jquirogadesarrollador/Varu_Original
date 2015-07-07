using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.nomina
{
    public class hojaTrabajo
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
        public hojaTrabajo(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerNominasHojaTrabajo(Decimal ID_EMPRESA, String ID_EMPRESAS, String ANALISTA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_nomina_proceso_hoja_trabajo ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + " ";
            }
            else
            {
                sql += "0" + " ";
            }

            if (!(String.IsNullOrEmpty(ID_EMPRESAS)))
            {
                sql += ", '" + ID_EMPRESAS.ToString() + "'";
            }
            else
            {
                sql += ",''";
            }

            if (!(String.IsNullOrEmpty(ANALISTA)))
            {
                sql += ", '" + ANALISTA.ToString() + "'";
            }
            else
            {
                sql += ",''";
            }

            #endregion

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
        #endregion metodos
    }
}