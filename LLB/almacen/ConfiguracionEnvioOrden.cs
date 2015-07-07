using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.almacen
{
    public class ConfiguracionEnvioOrden
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _id_configuracion = 0;
        private Decimal _id_usuario = 0;
        private String _tipo_configuracion = null;
        private String _usu_mail = null;
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

        public Decimal ID_CONFIGURACION
        {
            get { return _id_configuracion; }
            set { _id_configuracion = value; }
        }

        public Decimal ID_USUARIO
        {
            get { return _id_usuario; }
            set { _id_usuario = value; }
        }

        public String TIPO_CONFIGURACION
        {
            get { return _tipo_configuracion; }
            set { _tipo_configuracion = value; }
        }

        public String USU_MAIL
        {
            get { return _usu_mail; }
            set { _usu_mail = value; }
        }
        #endregion propiedades

        #region constructores
        public ConfiguracionEnvioOrden(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        public ConfiguracionEnvioOrden()
        {

        }
        #endregion

        #region metodos

        public DataTable ObtenerCrtEnvioOrdenesPorTipoConfiguracion(String TIPO_CONFIGURACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_conf_envio_ordenes_obtenerPorTipoConfiguracion ";

            #region validaciones
            if (String.IsNullOrEmpty(TIPO_CONFIGURACION) == false)
            {
                sql += "'" + TIPO_CONFIGURACION + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo TIPO_CONFIGURACION no puede ser vacio.";
            }

            #endregion validaciones

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

        public DataTable ObtenerCrtEnvioOrdenesTodasActivas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_conf_envio_ordenes_obtenerTodasActivas";

            #region validaciones
            #endregion validaciones

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

        public DataTable ObtenerCrtEnvioOrdenesTodasActivas(Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_conf_envio_ordenes_obtenerTodasActivas";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerListaInfoUsuariosActivosEnSistema()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_crt_usuarios_obtnerUsuariosActivos";

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

        public Decimal AdicionarNuevoRegistroEnvioOrdenPrincipal(Decimal ID_USUARIO,
            String TIPO_CONFIGURACION,
            Conexion conexion)
        {
            Decimal ID_CONFIGURACION_NUEVA = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_crt_envio_ordenes_adicionarPrincipal ";

            #region validaciones
            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO + ", ";
                informacion += "ID_USUARIO = '" + ID_USUARIO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_CONFIGURACION) == false)
            {
                sql += "'" + TIPO_CONFIGURACION + "', ";
                informacion += "TIPO_CONFIGURACION = '" + TIPO_CONFIGURACION + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_CONFIGURACION no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_CONFIGURACION_NUEVA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_CONFIGURACION_NUEVA <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para configuración de envio de ordenes de compra.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CRT_ENVIO_ORDENES, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la configuración de envío de ordenes de compra.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return ID_CONFIGURACION_NUEVA;
            }
            else
            {
                return 0;
            }
        }

        public Decimal AdicionarNuevoRegistroEnvioOrdenCopia(Decimal ID_USUARIO,
           String TIPO_CONFIGURACION,
           Conexion conexion)
        {
            Decimal ID_CONFIGURACION_NUEVA = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_crt_envio_ordenes_adicionarCopia ";

            #region validaciones
            if (ID_USUARIO != 0)
            {
                sql += ID_USUARIO + ", ";
                informacion += "ID_USUARIO = '" + ID_USUARIO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_USUARIO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_CONFIGURACION) == false)
            {
                sql += "'" + TIPO_CONFIGURACION + "', ";
                informacion += "TIPO_CONFIGURACION = '" + TIPO_CONFIGURACION + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_CONFIGURACION no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_CONFIGURACION_NUEVA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_CONFIGURACION_NUEVA <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para configuración de envio de ordenes de compra.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CRT_ENVIO_ORDENES, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la configuración de envío de ordenes de compra.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return ID_CONFIGURACION_NUEVA;
            }
            else
            {
                return 0;
            }
        }

        public Boolean DesactivarRegistroConfiguracion(Decimal ID_CONFIGURACION, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_crt_envio_ordenes_desactivar ";

            #region validaciones

            if (ID_CONFIGURACION != 0)
            {
                sql += ID_CONFIGURACION + ", ";
                informacion += "ID_CONFIGURACION = '" + ID_CONFIGURACION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CONFIGURACION no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar desactivar el registro de configuración de envios de ordenes de compra.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_CRT_ENVIO_ORDENES, tabla.ACCION_ELIMINAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para la desactivación de la configuración de envios de ordenes de compra.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean ActualizarListaConfiguracionesCopia(List<ConfiguracionEnvioOrden> listaConfiguraciones, Conexion conexion)
        {
            Boolean correcto = true;

            DataTable tablaConfiguracionesActuales = ObtenerCrtEnvioOrdenesTodasActivas(conexion);

            DataRow[] filasAnteriores = tablaConfiguracionesActuales.Select("TIPO_CONFIGURACION <> 'PRINCIPAL'");

            Boolean configuracionEncontrada = false;

            foreach (DataRow configuracionAnterior in filasAnteriores)
            {
                configuracionEncontrada = false;

                Decimal ID_CONFIGURACION_ANTERIOR = 0;

                foreach (ConfiguracionEnvioOrden configuracionLista in listaConfiguraciones)
                {
                    ID_CONFIGURACION_ANTERIOR = Convert.ToDecimal(configuracionAnterior["ID_CONFIGURACION"]);

                    if (ID_CONFIGURACION_ANTERIOR == configuracionLista.ID_CONFIGURACION)
                    {
                        configuracionEncontrada = true;
                        break;
                    }
                }

                if (configuracionEncontrada == false)
                {
                    if (DesactivarRegistroConfiguracion(ID_CONFIGURACION_ANTERIOR, conexion) == false)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            foreach (ConfiguracionEnvioOrden configuracionLista in listaConfiguraciones)
            {
                if (configuracionLista.ID_CONFIGURACION <= 0)
                {
                    if (AdicionarNuevoRegistroEnvioOrdenCopia(configuracionLista.ID_USUARIO, configuracionLista.TIPO_CONFIGURACION, conexion) <= 0)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            return correcto;
        }

        public Boolean ActualizarConfiguracionDeEnvioDeOrdenesDeCompra(Decimal ID_USUARIO_PRINCIPAL_ANTERIOR,
            Decimal ID_USUARIO_PRINCIPAL_NUEVO,
            String TIPO_CONFIGURACION_PRINCIPAL,
            List<ConfiguracionEnvioOrden> listaConfiguraciones)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ID_USUARIO_PRINCIPAL_ANTERIOR != ID_USUARIO_PRINCIPAL_NUEVO)
                {
                    if (AdicionarNuevoRegistroEnvioOrdenPrincipal(ID_USUARIO_PRINCIPAL_NUEVO, TIPO_CONFIGURACION_PRINCIPAL, conexion) <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                    }
                }

                if (correcto == true)
                {
                    if (ActualizarListaConfiguracionesCopia(listaConfiguraciones, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }
        #endregion metodos
    }
}