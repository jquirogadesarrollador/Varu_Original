using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.comercial
{
    public class Venta
    {
        #region variables

        private string _empresa = null;
        private string _usuario = null;

        #endregion variables

        #region propiedades

        private string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        #endregion propiedades

        #region constructores

        public Venta(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        #endregion constructores

        #region metodos
        public DataTable ObtenerComparativoMensualVentas(string periodoContable)
        {
            Conexion datos = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            String sql = null;

            sql = "usp_comercial_comparativo_mensual_ventas ";
            sql += periodoContable;

            try
            {
                dataSet = datos.ExecuteReader(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                datos.Desconectar();
            }
            return dataSet.Tables[0].DefaultView.Table;
        }

        public DataTable ObtenerCarteraCritica(string periodoContable)
        {
            Conexion datos = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            String sql = null;

            sql = "usp_comercial_cartera_critica ";
            sql += periodoContable;

            try
            {
                dataSet = datos.ExecuteReader(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                datos.Desconectar();
            }
            return dataSet.Tables[0].DefaultView.Table;
        }

        public DataTable ObtenerEstadisticaComportamiento(string año, string idRegional, string idCiudad)
        {
            Conexion datos = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            String sql = null;

            sql = "usp_comercial_estadistica_comportamiento ";
            sql += "'" + año + "', ";
            sql += "'" + idRegional + "', ";
            sql += "'" + idCiudad + "'";

            try
            {
                dataSet = datos.ExecuteReader(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                datos.Desconectar();
            }
            return dataSet.Tables[0].DefaultView.Table;
        }

        public DataTable ObtenerCumplimentoVentas(string año)
        {
            Conexion datos = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            String sql = null;

            sql = "usp_comercial_cumplimiento_ventas ";
            sql += año;

            try
            {
                dataSet = datos.ExecuteReader(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                datos.Desconectar();
            }
            return dataSet.Tables[0].DefaultView.Table;
        }

        public DataTable ObtenerVentasAIU(string periodoContable)
        {
            Conexion datos = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            String sql = null;

            sql = "usp_comercial_ventas_aiu ";
            sql += periodoContable;

            try
            {
                dataSet = datos.ExecuteReader(sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                datos.Desconectar();
            }
            return dataSet.Tables[0].DefaultView.Table;
        }
        #endregion metodos
    }
}