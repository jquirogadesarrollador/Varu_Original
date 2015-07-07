using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.contratacion
{
    public class Autoliquidacion
    {
        #region variables
        DataTable _reg1_tipo_registro = null;
        DataTable _reg2_tipo_registro = null;
        DataTable _inconsistencias = null;
        DataTable _novedades = null;
        DataTable _reliquidaciones = null;
        DataTable _nomina = null;
        DataTable _vacaciones = null;
        DataTable _liquidacion = null;
        DataTable _retiros_lps = null;
        DataTable _retiros_nomina_meses_anteriores = null;
        DataTable _diferencias_seguridad_social = null;


        String _empresa = null;
        String _usuario = null;
        #endregion variables

        #region propiedades
        public DataTable Reg1TipoRegistro
        {
            get { return _reg1_tipo_registro; }
            set { _reg1_tipo_registro = value; }
        }

        public DataTable Reg2TipoRegistro
        {
            get { return _reg2_tipo_registro; }
            set { _reg2_tipo_registro = value; }
        }

        public DataTable Inconsistencias
        {
            get { return _inconsistencias; }
            set { _inconsistencias = value; }
        }

        public DataTable Novedades
        {
            get { return _novedades; }
            set { _novedades = value; }
        }

        public DataTable Reliquidaciones
        {
            get { return _reliquidaciones; }
            set { _reliquidaciones = value; }
        }

        public DataTable Nomina
        {
            get { return _nomina; }
            set { _nomina = value; }
        }

        public DataTable Vacaciones
        {
            get { return _vacaciones; }
            set { _vacaciones = value; }
        }

        public DataTable Liquidacion
        {
            get { return _liquidacion; }
            set { _liquidacion = value; }
        }

        public DataTable RetirosLps
        {
            get { return _retiros_lps; }
            set { _retiros_lps = value; }
        }

        public DataTable RetirosNominaMesesAnteriores
        {
            get { return _retiros_nomina_meses_anteriores; }
            set { _retiros_nomina_meses_anteriores = value; }
        }

        public DataTable DiferenciasSeguridadSocial
        {
            get { return _diferencias_seguridad_social; }
            set { _diferencias_seguridad_social = value; }
        }


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
        #endregion propiedades

        #region constructores
        public Autoliquidacion(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        public void Dispose()
        {
            if (Reg1TipoRegistro != null) Reg1TipoRegistro.Dispose();
            if (Reg2TipoRegistro != null) Reg2TipoRegistro.Dispose();
            if (Inconsistencias != null) Inconsistencias.Dispose();
            if (Novedades != null) Novedades.Dispose();
            if (Reliquidaciones != null) Reliquidaciones.Dispose();
            if (Nomina != null) Nomina.Dispose();
            if (Vacaciones != null) Vacaciones.Dispose();
            if (Liquidacion != null) Liquidacion.Dispose();
        }

        #region metodos

        public bool Identificar(string año, string mes)
        {
            Conexion conexion = new Conexion(Empresa);
            string sql = null;
            bool identificado = false;
            sql = "usp_autoliquidacion_identificarEmpresas '" + Construir(año, mes) + "'";

            try
            {
                conexion.ExecuteReader(sql);
                identificado = true;
            }
            catch (Exception e)
            {
                throw new Exception("Error al identificar las empresas de autoliquidación para el período. " + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return identificado;
        }

        public DataTable ObtenerPorPeriodoContable(string año, string mes)
        {
            Conexion conexion = new Conexion(Empresa);
            DataTable dataTable = null;
            String sql = null;

            sql = "usp_autoliquidacion_obtenerPorPeriodoContable '" + Construir(año, mes) + "'";

            try
            {
                dataTable = conexion.ExecuteReader(sql).Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception("Error al identificar las empresas de autoliquidación para el período. " + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public void Liquidar(string año, string mes, string empresas)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;

            sql = "usp_autoliquidacion_liquidar ";
            sql += "'" + Construir(año, mes) + "'";
            sql += ", '" + empresas + "'";

            try
            {
                Cargar(conexion.ExecuteReader(sql));
            }
            catch (Exception e)
            {
                throw new Exception("Error al liquidar autoliquidación. " + e.Message);

            }
            finally
            {
                conexion.Desconectar();
            }
        }

        private string Construir(string año, string mes)
        {
            return año.Substring(2, 2) + Convert.ToInt32(mes).ToString("00");
        }

        private void Cargar(DataSet dataSet)
        {
            if (dataSet.Tables[0].Rows.Count > 0) Reg1TipoRegistro = dataSet.Tables[0];
            if (dataSet.Tables[1].Rows.Count > 0) Reg2TipoRegistro = dataSet.Tables[1];
            if (dataSet.Tables[2].Rows.Count > 0) Inconsistencias = dataSet.Tables[2];
            if (dataSet.Tables[3].Rows.Count > 0) Novedades = dataSet.Tables[3];
            if (dataSet.Tables[4].Rows.Count > 0) Reliquidaciones = dataSet.Tables[4];
            if (dataSet.Tables[5].Rows.Count > 0) Nomina = dataSet.Tables[5];
            if (dataSet.Tables[6].Rows.Count > 0) Vacaciones = dataSet.Tables[6];
            if (dataSet.Tables[7].Rows.Count > 0) Liquidacion = dataSet.Tables[7];
            if (dataSet.Tables[8].Rows.Count > 0) RetirosLps = dataSet.Tables[8];
            if (dataSet.Tables[9].Rows.Count > 0) RetirosNominaMesesAnteriores = dataSet.Tables[9];
            if (dataSet.Tables[10].Rows.Count > 0) DiferenciasSeguridadSocial = dataSet.Tables[10];

        }

        public void Actualizar(string año, string mes, string empresas, string id_estado)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;

            sql = "usp_autoliquidacion_actualizar ";
            sql += "'" + Construir(año, mes) + "'";
            sql += ", '" + empresas + "'";
            sql += ", '" + id_estado + "'";

            try
            {
                conexion.ExecuteReader(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar autoliquidacion. " + e.Message);

            }
            finally
            {
                conexion.Desconectar();
            }
        }
        #endregion metodos
    }
}