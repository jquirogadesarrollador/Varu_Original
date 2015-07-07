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
    public class ServicioRespectivo
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;

        Int32 _ID_SERVICIO_RESPECTIVO;
        Int32 _REGISTRO_CONTRATO;
        DateTime _FECHA_INICIO;
        DateTime _FECHA_VENCE;
        String _TIPO_CONTRATO;
        String _DESCRIPCION;

        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public Int32 ID_SERVICIO_RESPECTIVO
        {
            get { return _ID_SERVICIO_RESPECTIVO; }
            set { _ID_SERVICIO_RESPECTIVO = value; }
        }

        public Int32 REGISTRO_CONTRATO
        {
            get { return _REGISTRO_CONTRATO; }
            set { _REGISTRO_CONTRATO = value; }
        }

        public DateTime FECHA_INICIO
        {
            get { return _FECHA_INICIO; }
            set { _FECHA_INICIO = value; }
        }

        public DateTime FECHA_VENCE
        {
            get { return _FECHA_VENCE; }
            set { _FECHA_VENCE = value; }
        }

        public String TIPO_CONTRATO
        {
            get { return _TIPO_CONTRATO; }
            set { _TIPO_CONTRATO = value; }
        }

        public String DESCRIPCION
        {
            get { return _DESCRIPCION; }
            set { _DESCRIPCION = value; }
        }

        #endregion propiedades

        #region constructores
        public ServicioRespectivo(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        public ServicioRespectivo()
        {

        }
        #endregion

        #region metodos

        public DataTable ObtenerServicioRespectivosVigentesPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_servicios_respectivos_buscarPorIdEmpresaVigentes ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA;
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0. \n";
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

        public DataTable ObtenerServiciosRespectivosPorRegistroContrato(Decimal REGISTRO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_servicios_respectivos_buscarPorIdContrato ";
            sql += REGISTRO;

            if (ejecutar == true)
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

        public DataTable ObtenerServiciosRespectivosPorRegistroContrato(Decimal REGISTRO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_servicios_respectivos_buscarPorIdContrato ";
            sql += REGISTRO;

            if (ejecutar == true)
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

        public Boolean DesactivarServicioRespectivo(Decimal ID_SERVICIO_RESPECTIVO,
            String USU_MOD,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_r_servicios_respectivos_desactivar ";

            if (ID_SERVICIO_RESPECTIVO != 0)
            {
                sql += ID_SERVICIO_RESPECTIVO + ", ";
                informacion += "ID_SERVICIO_RESPECTIVO = " + ID_SERVICIO_RESPECTIVO + ", ";
            }
            else
            {
                MensajeError = "El campo ID DE SERVICIO RESPECTIVO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_MOD)))
            {
                sql += "'" + USU_MOD + "'";
                informacion += "USU_MOD = '" + USU_MOD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo USUARIO QUE MODIFICÓ no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_MOD, tabla.VEN_R_SERVICIOS_RESPECTIVOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Decimal AdicionarServicioRespectivoAContrato(Decimal registroContrato,
            String descripcion,
            DateTime fechaInicio,
            DateTime fechaVence,
            String activo,
            String usuCre,
            String tipoServicioRespectivo,
            Conexion conexion)
        {
            Decimal idServicioRespectivoCreado = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_r_servicios_respectivos_adicionar ";

            if (registroContrato != 0)
            {
                sql += registroContrato + ", ";
                informacion += "REGISTRO_CONTRATO = " + registroContrato + ", ";
            }
            else
            {
                MensajeError = "El campo ID DEL CONTRATO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(descripcion)))
            {
                sql += "'" + descripcion + "', ";
                informacion += "DESCRIPCION = '" + descripcion + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DESCRIPCION = 'NULL', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaInicio) + "', ";
            informacion += "FECHA_INICIO = '" + fechaInicio.ToShortDateString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaVence) + "', ";
            informacion += "FECHA_VENCE = '" + fechaVence.ToShortDateString() + "', ";

            if (!(String.IsNullOrEmpty(activo)))
            {
                sql += "'" + activo + "', ";
                informacion += "ACTIVO = '" + activo + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(usuCre)))
            {
                sql += "'" + usuCre + "', ";
                informacion += "USU_CRE = '" + usuCre + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(tipoServicioRespectivo)))
            {
                sql += "'" + tipoServicioRespectivo + "'";
                informacion += "TIPO_SERVICIO_RESPECTIVO = '" + tipoServicioRespectivo + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_SERVICIO_RESPECTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    idServicioRespectivoCreado = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    if (idServicioRespectivoCreado <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar la información del servicio respectivo.";
                        idServicioRespectivoCreado = 0;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(usuCre, tabla.VEN_R_SERVICIOS_RESPECTIVOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoria del servicio_respectivo.";
                            idServicioRespectivoCreado = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    idServicioRespectivoCreado = 0;
                }
            }

            return idServicioRespectivoCreado;
        }

        public Boolean ActualizarServicioRespectivo(Decimal idServicioRespectivo,
            String descripcion,
            DateTime fechaInicio,
            DateTime fechaVence,
            String usuMod,
            String tipoServicioRespectivo,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_r_servicios_respectivos_actualizar ";

            if (idServicioRespectivo != 0)
            {
                sql += idServicioRespectivo + ", ";
                informacion += "ID_SERVICIO_RESPECTIVO = " + idServicioRespectivo + ", ";
            }
            else
            {
                MensajeError = "El campo ID DE SERVICIO RESPECTIVO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(descripcion)))
            {
                sql += "'" + descripcion + "', ";
                informacion += "DESCRIPCION = '" + descripcion + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DESCRIPCION = 'NULL', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaInicio) + "', ";
            informacion += "FECHA_INICIO = '" + fechaInicio.ToShortDateString() + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(fechaVence) + "', ";
            informacion += "FECHA_VENCE = '" + fechaVence + "', ";

            if (!(String.IsNullOrEmpty(usuMod)))
            {
                sql += "'" + usuMod + "', ";
                informacion += "USU_MOD = '" + usuMod + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO QUE MODIFICÓ no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(tipoServicioRespectivo)))
            {
                sql += "'" + tipoServicioRespectivo + "'";
                informacion += "TIPO_SERVICIO_RESPECTIVO = '" + tipoServicioRespectivo + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_SERVICIO_RESPECTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(usuMod, tabla.VEN_R_SERVICIOS_RESPECTIVOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public Boolean ActualizarServiciosRespectivosDeContratoDeServicio(Int32 registroContrato,
            List<ServicioRespectivo> listaServiciosRespectivos,
            String usuMod)
        {
            Boolean correcto = true;
            Boolean srEncontrado = false;
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                DataTable tablaServiciosActuales = ObtenerServiciosRespectivosPorRegistroContrato(registroContrato, conexion);

                foreach (DataRow filaServicioActual in tablaServiciosActuales.Rows)
                {
                    srEncontrado = false;
                    Int32 idServicioRespectivoActual = Convert.ToInt32(filaServicioActual["ID_SERVICIO_RESPECTIVO"].ToString());

                    foreach (ServicioRespectivo srNuevo in listaServiciosRespectivos)
                    {
                        if (idServicioRespectivoActual == srNuevo.ID_SERVICIO_RESPECTIVO)
                        {
                            srEncontrado = true;
                            break;
                        }
                    }

                    if (srEncontrado == false)
                    {
                        if (DesactivarServicioRespectivo(Convert.ToDecimal(idServicioRespectivoActual), usuMod, conexion) == false)
                        {
                            correcto = false;
                            conexion.DeshacerTransaccion();
                            break;
                        }
                    }
                }

                foreach (ServicioRespectivo sr in listaServiciosRespectivos)
                {
                    if (sr.ID_SERVICIO_RESPECTIVO <= 0)
                    {
                        Decimal idServicioRespectivoCreado = AdicionarServicioRespectivoAContrato(Convert.ToDecimal(sr.REGISTRO_CONTRATO), sr.DESCRIPCION, sr.FECHA_INICIO, sr.FECHA_VENCE, "S", usuMod, sr.TIPO_CONTRATO, conexion);

                        if (idServicioRespectivoCreado <= 0)
                        {
                            correcto = false;
                            conexion.DeshacerTransaccion();
                            break;
                        }
                    }
                    else
                    {
                        if (ActualizarServicioRespectivo(sr.ID_SERVICIO_RESPECTIVO, sr.DESCRIPCION, sr.FECHA_INICIO, sr.FECHA_VENCE, usuMod, sr.TIPO_CONTRATO, conexion) == false)
                        {
                            correcto = false;
                            conexion.DeshacerTransaccion();
                            break;
                        }
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
