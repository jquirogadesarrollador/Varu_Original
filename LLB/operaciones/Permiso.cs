using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.operaciones
{
    public class Permiso
    {
        #region variables

        public enum Procesos
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
        public enum Secciones
        {
            ComercialContactos = 1,
            ComercialUnidadNegocio,
            ComercialCobertura,
            ComercialCondicionesEconomicas
        }

        private string _proceso;
        private string _proceso_permitido;
        private string _seccion;

        #region variables procesos
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
        private Boolean _contabilidad;

        #endregion variables procesos

        #region variables secciones
        private Boolean _comercial_contactos;
        private Boolean _comercial_unidad_negocio;
        private Boolean _comercial_cobertura;
        private Boolean _comercial_condiciones_economicas;

        #endregion variables secciones

        private string _empresa = null;
        private string _usuario = null;
        #endregion variables

        #region propiedades
        public string Proceso
        {
            get { return _proceso; }
            set { _proceso = value; }
        }
        public string ProcesoPermitido
        {
            get { return _proceso_permitido; }
            set { _proceso_permitido = value; }
        }
        public string Seccion
        {
            get { return _seccion; }
            set { _seccion = value; }
        }

        #region propiedades procesos
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

        public Boolean Contabilidad
        {
            get { return _contabilidad; }
            set { _contabilidad = value; }
        }
        #endregion propiedades procesos

        #region propiedades secciones
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
        #endregion propiedades secciones

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
        public Permiso(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public Boolean Adicionar(List<Permiso> permisos)
        {
            String sql = null;
            Boolean ejecutado = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            foreach (Permiso permiso in permisos)
            {
                sql = "usp_oper_permisos_adicionar ";
                sql += "'" + permiso.Proceso.ToString() + "', ";
                sql += "'" + permiso.ProcesoPermitido.ToString() + "', ";
                sql += "'" + permiso.Seccion.ToString() + "'";

                try
                {
                    conexion.ExecuteNonQuery(sql);
                }
                catch (ApplicationException e)
                {
                    ejecutado = false;
                    throw new Exception(e.Message);
                }
            }

            if (ejecutado)
            {
                conexion.AceptarTransaccion();
            }
            else
            {
                conexion.DeshacerTransaccion();
                conexion.Desconectar();
            }
            return ejecutado;
        }
        public Boolean Actualizar(List<Permiso> permisos)
        {
            String sql = null;
            Boolean ejecutado = true;
            Boolean borrado = false;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            foreach (Permiso permiso in permisos)
            {
                try
                {
                    if (borrado.Equals(false))
                    {
                        sql = "usp_oper_permisos_borrar ";
                        sql += "'" + permiso.Proceso.ToString() + "'";
                        conexion.ExecuteNonQuery(sql);
                        borrado = true;
                    }

                    sql = "usp_oper_permisos_adicionar ";
                    sql += "'" + permiso.Proceso.ToString() + "', ";
                    sql += "'" + permiso.ProcesoPermitido.ToString() + "', ";
                    sql += "'" + permiso.Seccion.ToString() + "'";

                    conexion.ExecuteNonQuery(sql);
                }
                catch (ApplicationException e)
                {
                    ejecutado = false;
                    throw new Exception(e.Message);
                }
            }

            if (ejecutado)
            {
                conexion.AceptarTransaccion();
            }
            else
            {
                conexion.DeshacerTransaccion();
                conexion.Desconectar();
            }
            return ejecutado;
        }
        public void ObtenerPorProceso(Procesos proceso)
        {
            Conexion datos = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            DataView dataView = new DataView();
            DataTable dataTable = new DataTable();
            String sql = null;

            sql = "usp_oper_permisos_obtenerPorProceso ";
            sql += "'" + proceso.ToString() + "'";

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
                Cargar(dataTable, proceso.ToString());
                if (dataTable != null) dataTable.Dispose();
                if (dataView != null) dataView.Dispose();
                if (dataSet != null) dataSet.Dispose();
                datos.Desconectar();
            }
        }
        public DataTable ObtenerTodosLosProcesos()
        {
            Conexion datos = new Conexion(Empresa);
            DataSet dataSet = new DataSet();
            String sql = null;

            sql = "usp_oper_permisos_obtenerTodosLosProcesos ";

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

        private String Homologar(tabla.proceso procesoManual)
        {
            String procesoHomologado = null;
            switch (procesoManual)
            {
                case tabla.proceso.ContactoComercial:
                    procesoHomologado = Procesos.Comercial.ToString();
                    break;
                case tabla.proceso.ContactoSeleccion:
                    procesoHomologado = Procesos.Seleccion.ToString();
                    break;
                case tabla.proceso.ContactoContratacion:
                    procesoHomologado = Procesos.Contratacion.ToString();
                    break;
                case tabla.proceso.Nomina:
                    procesoHomologado = Procesos.Nomina.ToString();
                    break;
                case tabla.proceso.ContactoFinanciera:
                    procesoHomologado = Procesos.Financiera.ToString();
                    break;
                case tabla.proceso.ContactoJuridica:
                    procesoHomologado = Procesos.Juridica.ToString();
                    break;
                case tabla.proceso.ContactoSaludIntegral:
                    procesoHomologado = Procesos.SaludIntegral.ToString();
                    break;
                case tabla.proceso.ContactoOperaciones:
                    procesoHomologado = Procesos.Operaciones.ToString();
                    break;
                case tabla.proceso.ContactoBienestarSocial:
                    procesoHomologado = Procesos.BienestarSocial.ToString();
                    break;
                case tabla.proceso.ContactoRse:
                    procesoHomologado = Procesos.Rse.ToString();
                    break;
                case tabla.proceso.ContactoComprasEInventario:
                    procesoHomologado = Procesos.ComprasEInventario.ToString();
                    break;
                case tabla.proceso.ContactoContabilidad:
                    procesoHomologado = Procesos.Contabilidad.ToString();
                    break;
            }
            return procesoHomologado;
        }
        public void Cargar(DataTable dataTable, string proceso)
        {
            Proceso = proceso;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (!DBNull.Value.Equals(dataRow["PROCESO_PERMITIDO"]))
                {
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Comercial.ToString())) Comercial = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Seleccion.ToString())) Seleccion = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Contratacion.ToString())) Contratacion = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Nomina.ToString())) Nomina = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Facturacion.ToString())) Facturacion = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Financiera.ToString())) Financiera = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Juridica.ToString())) Juridica = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.SaludIntegral.ToString())) SaludIntegral = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Operaciones.ToString())) Operaciones = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.BienestarSocial.ToString())) BienestarSocial = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Rse.ToString())) Rse = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.ComprasEInventario.ToString())) ComprasEInventario = true;
                    if (dataRow["PROCESO_PERMITIDO"].ToString().Equals(Procesos.Contabilidad.ToString())) Contabilidad = true;

                }

                if (!DBNull.Value.Equals(dataRow["SECCION"]))
                {
                    if (dataRow["SECCION"].ToString().Equals(Secciones.ComercialContactos.ToString())) ComercialContactos = true;
                    if (dataRow["SECCION"].ToString().Equals(Secciones.ComercialUnidadNegocio.ToString())) ComercialUnidadNegocio = true;
                    if (dataRow["SECCION"].ToString().Equals(Secciones.ComercialCobertura.ToString())) ComercialCobertura = true;
                    if (dataRow["SECCION"].ToString().Equals(Secciones.ComercialCondicionesEconomicas.ToString())) ComercialCondicionesEconomicas = true;
                }
            }
        }
        #endregion metodos
    }
}