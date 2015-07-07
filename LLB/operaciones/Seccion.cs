using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.operaciones
{
    public class Seccion
    {
        #region variables
        private enum seccionesDelManual
        {
            ComercialContactos = 1,
            ComercialUnidadNegocio,
            ComercialCobertura,
            ComercialCondicionesEconomicas
        }
        private Boolean _comercial_contactos;
        private Boolean _comercial_unidad_negocio;
        private Boolean _comercial_cobertura;
        private Boolean _comercial_condiciones_economicas;

        #endregion variables

        #region propiedades

        public Boolean ComercialContactos
        {
            get { return _comercial_contactos; }
            set { _comercial_contactos = value; }
        }

        public Boolean ComercialUnidadNegocio
        {
            get { return _comercial_unidad_negocio; }
            set { _comercial_unidad_negocio = value; }
        }

        public Boolean ComercialCobertura
        {
            get { return _comercial_cobertura; }
            set { _comercial_cobertura = value; }
        }

        public Boolean ComercialCondicionesEconomicas
        {
            get { return _comercial_condiciones_economicas; }
            set { _comercial_condiciones_economicas = value; }
        }

        #endregion propiedades

        #region constructores
        public Seccion()
        {
            ComercialContactos = false;
            ComercialUnidadNegocio = false;
            ComercialCobertura = false;
            ComercialCondicionesEconomicas = false;
        }
        #endregion constructores

        #region metodos
        private String Homologar(tabla.proceso procesoManual)
        {
            String procesoHomologado = null;
            switch (procesoManual)
            {
                case tabla.proceso.ContactoComercial:
                    procesoHomologado = Proceso.procesosDelManual.Comercial.ToString();
                    break;
                case tabla.proceso.ContactoSeleccion:
                    procesoHomologado = Proceso.procesosDelManual.Seleccion.ToString();
                    break;
                case tabla.proceso.ContactoContratacion:
                    procesoHomologado = Proceso.procesosDelManual.Contratacion.ToString();
                    break;
                case tabla.proceso.Nomina:
                    procesoHomologado = Proceso.procesosDelManual.Nomina.ToString();
                    break;
                case tabla.proceso.ContactoFinanciera:
                    procesoHomologado = Proceso.procesosDelManual.Financiera.ToString();
                    break;
                case tabla.proceso.ContactoJuridica:
                    procesoHomologado = Proceso.procesosDelManual.Juridica.ToString();
                    break;
                case tabla.proceso.ContactoSaludIntegral:
                    procesoHomologado = Proceso.procesosDelManual.SaludIntegral.ToString();
                    break;
                case tabla.proceso.ContactoOperaciones:
                    procesoHomologado = Proceso.procesosDelManual.Operaciones.ToString();
                    break;
                case tabla.proceso.ContactoBienestarSocial:
                    procesoHomologado = Proceso.procesosDelManual.BienestarSocial.ToString();
                    break;
                case tabla.proceso.ContactoRse:
                    procesoHomologado = Proceso.procesosDelManual.Rse.ToString();
                    break;
                case tabla.proceso.ContactoComprasEInventario:
                    procesoHomologado = Proceso.procesosDelManual.ComprasEInventario.ToString();
                    break;
            }
            return procesoHomologado;
        }
        public void Obtener(tabla.proceso procesoManualDeServicio, String empresa)
        {
            Conexion datos = new Conexion(empresa);
            DataSet dataSet = new DataSet();
            DataView dataView = new DataView();
            DataTable dataTable = new DataTable();
            String sql = null;
            String proceso = Homologar(procesoManualDeServicio);

            sql = "usp_manual_servicio_obtener_permisos ";
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
        }

        public void Cargar(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!DBNull.Value.Equals(dataRow["seccion_manual_servicio"]))
                {
                    if (dataRow["seccion_manual_servicio"].ToString().Equals(seccionesDelManual.ComercialContactos.ToString())) ComercialContactos = true;
                    if (dataRow["seccion_manual_servicio"].ToString().Equals(seccionesDelManual.ComercialUnidadNegocio.ToString())) ComercialUnidadNegocio = true;
                    if (dataRow["seccion_manual_servicio"].ToString().Equals(seccionesDelManual.ComercialCobertura.ToString())) ComercialCobertura = true;
                    if (dataRow["seccion_manual_servicio"].ToString().Equals(seccionesDelManual.ComercialCondicionesEconomicas.ToString())) ComercialCondicionesEconomicas = true;
                }
            }
        }

        #endregion metodos
    }
}
