using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.almacen
{
    public class EntregaPorAprobar
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        Decimal _id_asignacion_sc = 0;
        Decimal _id_producto = 0;
        Int32 _cantidad_total = 0;
        DateTime _fecha_proyecta_entrega = new DateTime();


        private enum EstadosAsignacionSC
        {
            CREADA = 0,
            APROBADA,
            SIN_APROBACION
        }

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

        public Decimal ID_ASIGNACION_SC
        {
            get { return _id_asignacion_sc; }
            set { _id_asignacion_sc = value; }
        }

        public Decimal ID_PRODUCTO
        {
            get { return _id_producto; }
            set { _id_producto = value; }
        }

        public Int32 CANTIDAD_TOTAL
        {
            get { return _cantidad_total; }
            set { _cantidad_total = value; }
        }

        public DateTime FECHA_PROYECTADA_ENTREGA
        {
            get { return _fecha_proyecta_entrega; }
            set { _fecha_proyecta_entrega = value; }
        }
        #endregion propiedades

        #region constructores
        public EntregaPorAprobar(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        public EntregaPorAprobar()
        {
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarEscAsignacionSCPorAprobar(Decimal ID_EMPLEADO,
            Decimal ID_PRODUCTO,
            Int32 CANTIDAD_TOTAL,
            DateTime FCH_PROYECTA_ENTREGA,
            String ESTADO,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_esc_asignacion_s_c_Adicionar ";

            #region validaciones
            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (CANTIDAD_TOTAL != 0)
            {
                sql += CANTIDAD_TOTAL + ", ";
                informacion += "CANTIDAD_TOTAL = '" + CANTIDAD_TOTAL + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_TOTAL no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_PROYECTA_ENTREGA) + "', ";
            informacion += "FCH_PROYECTA_ENTREGA = '" + FCH_PROYECTA_ENTREGA.ToShortDateString() + "', ";

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ESC_ASIGNACION_S_C_POR_APROBAR, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public Decimal AutorizarAsignacionPorAutorizar(Decimal idAsignacionSC, DateTime fechaEntrega, Int32 cantidadEntrega)
        {
            Boolean correcto = true;

            Decimal idAsignacionCreada = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                DataTable tablaAsignacionPorAutorizar = ObtenerEntregaPorAprobarPorIdAsignacion(idAsignacionSC, conexion);
                DataRow filaAsignacionPorAutorizar = tablaAsignacionPorAutorizar.Rows[0];

                Decimal idProducto = Convert.ToDecimal(filaAsignacionPorAutorizar["ID_PRODUCTO"]);
                Decimal idEmpleado = Convert.ToDecimal(filaAsignacionPorAutorizar["ID_EMPLEADO"]);

                Entrega _entrega = new Entrega(Empresa, Usuario);

                idAsignacionCreada = _entrega.AdicionarEscAsignacionSC(idEmpleado, idProducto, cantidadEntrega, 0, fechaEntrega, "ESPECIAL", "ABIERTA", conexion);

                if (idAsignacionCreada <= 0)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                    idAsignacionCreada = 0;
                    MensajeError = _entrega.MensajeError;
                }
                else
                {
                    if (ActualizarEstadoAsignacionPorAutorizar(idAsignacionSC, EstadosAsignacionSC.APROBADA.ToString(), conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        idAsignacionCreada = 0;
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch
            {
                conexion.DeshacerTransaccion();
                correcto = false;
                idAsignacionCreada = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return idAsignacionCreada;
        }

        public Boolean AdicionarEntregaProductosPorAprobar(Decimal idEmpleado, List<EntregaPorAprobar> listaProductosPorAprobar)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean correcto = true;

            try
            {
                foreach (EntregaPorAprobar productoPorAprobar in listaProductosPorAprobar)
                {
                    Decimal ID_ASIGNACION_SC = AdicionarEscAsignacionSCPorAprobar(idEmpleado, productoPorAprobar.ID_PRODUCTO, productoPorAprobar.CANTIDAD_TOTAL, productoPorAprobar.FECHA_PROYECTADA_ENTREGA, EstadosAsignacionSC.CREADA.ToString(), conexion);

                    if (ID_ASIGNACION_SC <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        break;
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
                correcto = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public DataTable ObtenerEntregasPorAprobarPorEmpleado(Decimal ID_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_esc_asignacion_s_c_por_aprobar_obtenerPorIdEmpleado ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO;
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
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

        public DataTable ObtenerEntregaPorAprobarPorIdAsignacion(Decimal ID_ASIGNACION_SC,
            Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_esc_asignacion_s_c_por_aprobar_obtenerPorIdAsignacion ";

            if (ID_ASIGNACION_SC != 0)
            {
                sql += ID_ASIGNACION_SC;
            }
            else
            {
                MensajeError += "El campo ID_ASIGNACION_SC no puede ser nulo\n";
                ejecutar = false;
            }

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

        public Boolean ActualizarEstadoAsignacionPorAutorizar(Decimal ID_ASIGNACION_SC,
            String ESTADO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_esc_asignacion_s_c_por_aprobar_actualziar_estado ";

            #region validaciones
            if (ID_ASIGNACION_SC != 0)
            {
                sql += ID_ASIGNACION_SC + ", ";
                informacion += "ID_ASIGNACION_SC = '" + ID_ASIGNACION_SC + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ASIGNACION_SC no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO) == false)
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ESC_ASIGNACION_S_C_POR_APROBAR, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }
        #endregion metodos
    }
}
