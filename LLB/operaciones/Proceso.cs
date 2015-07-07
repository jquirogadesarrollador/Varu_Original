using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.operaciones
{
    public class Proceso
    {
        #region variables
        public enum procesosDelManual
        {
            Comercial = 1,
            Seleccion,
            Contratacion,
            Nomina,
            Facturacion,
            Contabilidad,
            Financiera,
            Juridica,
            SaludIntegral,
            Operaciones,
            BienestarSocial,
            Rse,
            ComprasEInventario,
        }

        private Seccion _seccion;
        private Boolean _comercial;
        private Boolean _seleccion;
        private Boolean _contratacion;
        private Boolean _nomina;
        private Boolean _facturacion;
        private Boolean _financiera;
        private Boolean _juridica;
        private Boolean _salud_integral;
        private Boolean _operaciones;
        private Boolean _bienestar_social;
        private Boolean _rse;
        private Boolean _compras_e_inventario;

        #endregion variables

        #region propiedades
        public Seccion Secciones
        {
            get { return _seccion; }
            set { _seccion = value; }
        }

        public Boolean Comercial
        {
            get { return _comercial; }
            set { _comercial = value; }
        }

        public Boolean Seleccion
        {
            get { return _seleccion; }
            set { _seleccion = value; }
        }

        public Boolean Contratacion
        {
            get { return _contratacion; }
            set { _contratacion = value; }
        }

        public Boolean Nomina
        {
            get { return _nomina; }
            set { _nomina = value; }
        }

        public Boolean Facturacion
        {
            get { return _facturacion; }
            set { _facturacion = value; }
        }

        public Boolean Financiera
        {
            get { return _financiera; }
            set { _financiera = value; }
        }

        public Boolean Juridica
        {
            get { return _juridica; }
            set { _juridica = value; }
        }

        public Boolean SaludIntegral
        {
            get { return _salud_integral; }
            set { _salud_integral = value; }
        }

        public Boolean Operaciones
        {
            get { return _operaciones; }
            set { _operaciones = value; }
        }

        public Boolean BienestarSocial
        {
            get { return _bienestar_social; }
            set { _bienestar_social = value; }
        }

        public Boolean Rse
        {
            get { return _rse; }
            set { _rse = value; }
        }

        public Boolean ComprasEInventario
        {
            get { return _compras_e_inventario; }
            set { _compras_e_inventario = value; }
        }

        #endregion propiedades

        #region constructores
        public Proceso()
        {
            Comercial = false;
            Seleccion = false;
            Contratacion = false;
            Nomina = false;
            Facturacion = false;
            Financiera = false;
            Juridica = false;
            SaludIntegral = false;
            Operaciones = false;
            BienestarSocial = false;
            Rse = false;
            ComprasEInventario = false;
        }
        #endregion constructores

        #region metodos
        private String Homologar(tabla.proceso procesoManual)
        {
            String procesoHomologado = null;
            switch (procesoManual)
            {
                case tabla.proceso.ContactoComercial:
                    procesoHomologado = procesosDelManual.Comercial.ToString();
                    break;
                case tabla.proceso.ContactoSeleccion:
                    procesoHomologado = procesosDelManual.Seleccion.ToString();
                    break;
                case tabla.proceso.ContactoContratacion:
                    procesoHomologado = procesosDelManual.Contratacion.ToString();
                    break;
                case tabla.proceso.Nomina:
                    procesoHomologado = procesosDelManual.Nomina.ToString();
                    break;
                case tabla.proceso.ContactoFinanciera:
                    procesoHomologado = procesosDelManual.Financiera.ToString();
                    break;
                case tabla.proceso.ContactoJuridica:
                    procesoHomologado = procesosDelManual.Juridica.ToString();
                    break;
                case tabla.proceso.ContactoSaludIntegral:
                    procesoHomologado = procesosDelManual.SaludIntegral.ToString();
                    break;
                case tabla.proceso.ContactoOperaciones:
                    procesoHomologado = procesosDelManual.Operaciones.ToString();
                    break;
                case tabla.proceso.ContactoBienestarSocial:
                    procesoHomologado = procesosDelManual.BienestarSocial.ToString();
                    break;
                case tabla.proceso.ContactoRse:
                    procesoHomologado = procesosDelManual.Rse.ToString();
                    break;
                case tabla.proceso.ContactoComprasEInventario:
                    procesoHomologado = procesosDelManual.ComprasEInventario.ToString();
                    break;
            }
            return procesoHomologado;
        }
        public Proceso ObteneroPorId(tabla.proceso procesoManualDeServicio, String empresa)
        {
            Conexion datos = new Conexion(empresa);
            DataSet dataSet = new DataSet();
            DataView dataView = new DataView();
            DataTable dataTable = new DataTable();
            String sql = null;
            String proceso = Homologar(procesoManualDeServicio);

            sql = "usp_oper_permisos_obtenerPorProceso ";
            sql += "'" + proceso + "'";

            try
            {
                dataSet = datos.ExecuteReader(sql);
                dataView = dataSet.Tables[0].DefaultView;
                dataTable = dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                Cargar(dataTable);
                if (dataTable != null) dataTable.Dispose();
                if (dataView != null) dataView.Dispose();
                if (dataSet != null) dataSet.Dispose();
                datos.Desconectar();
            }
            return this;
        }

        public void Cargar(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!DBNull.Value.Equals(dataRow["proceso_manual_servicio"]))
                {
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.Comercial.ToString())) Comercial = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.Seleccion.ToString())) Seleccion = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.Contratacion.ToString())) Contratacion = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.Nomina.ToString())) Nomina = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.Facturacion.ToString())) Facturacion = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.Financiera.ToString())) Financiera = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.Juridica.ToString())) Juridica = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.SaludIntegral.ToString())) SaludIntegral = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.Operaciones.ToString())) Operaciones = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.BienestarSocial.ToString())) BienestarSocial = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.Rse.ToString())) Rse = true;
                    if (dataRow["proceso_manual_servicio"].ToString().Equals(procesosDelManual.ComprasEInventario.ToString())) ComprasEInventario = true;
                }
            }
        }
        #endregion metodos
    }
}