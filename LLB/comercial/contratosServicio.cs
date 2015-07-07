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
    public class contratosServicio
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;
        private Decimal _ID_SERVICIO_RESPECTIVO_SR = 0;
        private DateTime _FECHA_INICIO_SR = new DateTime();
        private DateTime _FECHA_VENCE_SR = new DateTime();
        private String _DESCRIPCION_SR = null;
        private String _TIPO_SERVICIO_RESPECTIVO = null;
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

        public Decimal ID_SERVICIO_RESPECTIVO_SR
        {
            get { return _ID_SERVICIO_RESPECTIVO_SR; }
            set { _ID_SERVICIO_RESPECTIVO_SR = value; }
        }

        public DateTime FECHA_INICIO_SR
        {
            get { return _FECHA_INICIO_SR; }
            set { _FECHA_INICIO_SR = value; }
        }

        public DateTime FECHA_VENCE_SR
        {
            get { return _FECHA_VENCE_SR; }
            set { _FECHA_VENCE_SR = value; }
        }

        public String DESCRIPCION_SR
        {
            get { return _DESCRIPCION_SR; }
            set { _DESCRIPCION_SR = value; }
        }

        public String TIPO_SERVICIO_RESPECTIVO
        {
            get { return _TIPO_SERVICIO_RESPECTIVO; }
            set { _TIPO_SERVICIO_RESPECTIVO = value; }
        }
        #endregion propiedades

        #region constructores
        public contratosServicio(String idEmpresa)
        {
            Empresa = idEmpresa;
        }
        #endregion

        #region metodos
        public Decimal Adicionar(Decimal ID_EMPRESA,
            DateTime FECHA,
            String OBJ_CONTRATO,
            DateTime FCH_VENCE,
            String FIRMADO,
            String ENVIO_CTE,
            String USU_CRE,
            String NUMERO_CONTRATO,
            String TIPO_CONTRATO,
            List<HistorialEnvioDevolucion> listaEnviosDevoluciones,
            DateTime FECHA_INI_CONTRATO,
            DateTime FECHA_FIN_CONTRATO
            )
        {
            Decimal REGISTRO_CONTRATO = 0;

            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            try
            {
                REGISTRO_CONTRATO = AdicionarContratoServicio(ID_EMPRESA, FECHA, OBJ_CONTRATO, FCH_VENCE, FIRMADO, ENVIO_CTE, USU_CRE, NUMERO_CONTRATO, TIPO_CONTRATO, FECHA_INI_CONTRATO, FECHA_FIN_CONTRATO, conexion);

                if (REGISTRO_CONTRATO <= 0)
                {
                    conexion.DeshacerTransaccion();
                    verificador = false;
                }
                else
                {
                    foreach (HistorialEnvioDevolucion _envio in listaEnviosDevoluciones)
                    {
                        if (AdicionarEnvioDevolucion(REGISTRO_CONTRATO, _envio.FECHA_ACCION, _envio.TIPO_ACCION, _envio.OBSERVACIONES, USU_CRE, conexion) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            verificador = false;
                        }
                    }

                    if (verificador == true)
                    {
                        conexion.AceptarTransaccion();
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            if (verificador == true)
            {
                return REGISTRO_CONTRATO;
            }
            else
            {
                return 0;
            }
        }

        public Decimal AdicionarContratoServicio(Decimal ID_EMPRESA, DateTime FECHA,
            String OBJ_CONTRATO,
            DateTime FCH_VENCE,
            String FIRMADO,
            String ENVIO_CTE,
            String USU_CRE,
            String NUMERO_CONTRATO,
            String TIPO_CONTRATO,
            DateTime FECHA_INI_CONTRATO,
            DateTime FECHA_FIN_CONTRATO,
            Conexion conexion)
        {
            Decimal REGISTRO_CONTRATO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_r_contratos_adicionar ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";

            if (!(String.IsNullOrEmpty(OBJ_CONTRATO)))
            {
                sql += "'" + OBJ_CONTRATO + "', ";
                informacion += "OBJ_CONTRATO = '" + OBJ_CONTRATO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBJETO DEL CONTRATO no puede ser nulo.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_VENCE) + "', ";
            informacion += "FCH_VENCE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_VENCE) + "', ";

            if (!(String.IsNullOrEmpty(FIRMADO)))
            {
                sql += "'" + FIRMADO + "', ";
                informacion += "FIRMADO = '" + FIRMADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FIRMADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ENVIO_CTE)))
            {
                sql += "'" + ENVIO_CTE + "', ";
                informacion += "ENVIO_CTE = '" + ENVIO_CTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ENVIO CLIENTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_CRE)))
            {
                sql += "'" + USU_CRE + "', ";
                informacion += "USU_CRE = '" + USU_CRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUMERO_CONTRATO)))
            {
                sql += "'" + NUMERO_CONTRATO + "', ";
                informacion += "NUMERO_CONTRATO = '" + NUMERO_CONTRATO + "', ";
            }
            else
            {
                MensajeError += "El campo NUMERO DE CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_CONTRATO)))
            {
                sql += "'" + TIPO_CONTRATO + "', ";
                informacion += "TIPO_CONTRATO = '" + TIPO_CONTRATO + "'";
            }
            else
            {
                MensajeError += "El campo TIPO_CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }


            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INI_CONTRATO) + "', ";
            informacion += "FCH_INI_CONTRATO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INI_CONTRATO) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN_CONTRATO) + "'";
            informacion += "FCH_INI_CONTRATO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN_CONTRATO) + "'";

            if (ejecutar)
            {
                try
                {
                    REGISTRO_CONTRATO = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    if (REGISTRO_CONTRATO <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar la información del contrato de servicio.";
                        REGISTRO_CONTRATO = 0;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(USU_CRE, tabla.VEN_R_CONTRATOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoria del contrato de servicio.";
                            REGISTRO_CONTRATO = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO_CONTRATO = 0;
                }
            }

            return REGISTRO_CONTRATO;
        }


        public Decimal AdicionarServicioRespectivoAContrato(Decimal REGISTRO_CONTRATO,
            String DESCRIPCION,
            DateTime FECHA_INICIO,
            DateTime FECHA_VENCE,
            String ACTIVO,
            String USU_CRE,
            String TIPO_SERVICIO_RESPECTIVO,
            Conexion conexion)
        {
            Decimal ID_SERVICIO_RESPECTIVO = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_r_servicios_respectivos_adicionar ";

            if (REGISTRO_CONTRATO != 0)
            {
                sql += REGISTRO_CONTRATO + ", ";
                informacion += "REGISTRO_CONTRATO = " + REGISTRO_CONTRATO + ", ";
            }
            else
            {
                MensajeError = "El campo ID_DEL CONTRATO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DESCRIPCION = 'NULL', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO) + "', ";
            informacion += "FECHA_INICIO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_VENCE) + "', ";
            informacion += "FECHA_VENCE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_VENCE) + "', ";

            if (!(String.IsNullOrEmpty(ACTIVO)))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO = '" + ACTIVO + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_CRE)))
            {
                sql += "'" + USU_CRE + "', ";
                informacion += "USU_CRE = '" + USU_CRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_SERVICIO_RESPECTIVO)))
            {
                sql += "'" + TIPO_SERVICIO_RESPECTIVO + "'";
                informacion += "TIPO_SERVICIO_RESPECTIVO = '" + TIPO_SERVICIO_RESPECTIVO + "'";
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
                    ID_SERVICIO_RESPECTIVO = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    if (ID_SERVICIO_RESPECTIVO <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar la información del servicio respectivo.";
                        ID_SERVICIO_RESPECTIVO = 0;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(USU_CRE, tabla.VEN_R_SERVICIOS_RESPECTIVOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoria del servicio_respectivo.";
                            ID_SERVICIO_RESPECTIVO = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_SERVICIO_RESPECTIVO = 0;
                }
            }

            return ID_SERVICIO_RESPECTIVO;
        }


        public Decimal AdicionarEnvioDevolucion(Decimal ID_CONTRATO,
            DateTime FECHA_ACCION,
            String TIPO_ACCION,
            String OBSERVACIONES,
            String USU_CRE,
            Conexion conexion)
        {
            Decimal ID_ACCION = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_hist_envios_devoluciones_adicionar ";

            if (ID_CONTRATO != 0)
            {
                sql += ID_CONTRATO + ", ";
                informacion += "ID_CONTRATO = " + ID_CONTRATO + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CONTRATO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACCION) + "', ";
            informacion += "FECHA_ACCION = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_ACCION) + "', ";

            if (!(String.IsNullOrEmpty(TIPO_ACCION)))
            {
                sql += "'" + TIPO_ACCION + "', ";
                informacion += "TIPO_ACCION = '" + TIPO_ACCION + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_ACCION no puede ser 0.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBSERVACIONES)))
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo OBSERVACIONES no puede ser 0.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_CRE)))
            {
                sql += "'" + USU_CRE + "'";
                informacion += "USU_CRE = '" + USU_CRE.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo.";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    ID_ACCION = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                    if (ID_ACCION <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar la información del nvio o devolución.";
                        ID_ACCION = 0;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(USU_CRE, tabla.VEN_HIST_ENVIO_DEVOLUCIONES, tabla.ACCION_ADICIONAR, sql, informacion, conexion) == false)
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoria del envío o devolución.";
                            ID_ACCION = 0;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_ACCION = 0;
                }
            }

            return ID_ACCION;
        }

        public Boolean Actualizar(Decimal REGISTRO,
            String NUMERO_CONTRATO,
            DateTime FECHA,
            DateTime FCH_VENCE,
            String TIPO_CONTRATO,
            String OBJ_CONTRATO,
            String FIRMADO,
            String ENVIO_CTE,
            String USU_MOD,
            List<HistorialEnvioDevolucion> listaEnviosDevoluciones,
            DateTime FECHA_INI_CONTRATO,
            DateTime FECHA_FIN_CONTRATO
            )
        {
            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarContratoDeServicio(REGISTRO, NUMERO_CONTRATO, FECHA, FCH_VENCE, TIPO_CONTRATO, OBJ_CONTRATO, FIRMADO, ENVIO_CTE, USU_MOD, FECHA_INI_CONTRATO, FECHA_FIN_CONTRATO, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    verificador = false;
                }
                else
                {
                    foreach (HistorialEnvioDevolucion _envio in listaEnviosDevoluciones)
                    {
                        if (AdicionarEnvioDevolucion(REGISTRO, _envio.FECHA_ACCION, _envio.TIPO_ACCION, _envio.OBSERVACIONES, USU_MOD, conexion) <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            verificador = false;
                        }
                    }

                    if (verificador == true)
                    {
                        conexion.AceptarTransaccion();
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                verificador = false;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return verificador;
        }

        public Boolean ActualizarDescripcionServicioRespectivo(Decimal REGISTRO,
            String USU_MOD,
            List<contratosServicio> listaServicios)
        {
            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                foreach (contratosServicio infoServicoLista in listaServicios)
                {
                    if (ActualizarServicioRespectivo(infoServicoLista.ID_SERVICIO_RESPECTIVO_SR, infoServicoLista.DESCRIPCION_SR, infoServicoLista.FECHA_INICIO_SR, infoServicoLista.FECHA_VENCE_SR, USU_MOD, infoServicoLista.TIPO_SERVICIO_RESPECTIVO, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        verificador = false;
                        break;
                    }
                }

                if (verificador == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                verificador = false;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return verificador;
        }

        public Boolean ActualizarContratoDeServicio(Decimal REGISTRO,
            String NUMERO_CONTRATO,
            DateTime FECHA,
            DateTime FCH_VENCE,
            String TIPO_CONTRATO,
            String OBJ_CONTRATO,
            String FIRMADO,
            String ENVIO_CTE,
            String USU_MOD,
            DateTime FECHA_INI_CONTRATO,
            DateTime FECHA_FIN_CONTRATO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_r_contratos_actualizar ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = " + REGISTRO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NUMERO_CONTRATO) == false)
            {
                sql += "'" + NUMERO_CONTRATO + "', ";
                informacion += "NUMERO_CONTRATO = '" + NUMERO_CONTRATO + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NUMERO_CONTRATO = 'NULL', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_VENCE) + "', ";
            informacion += "FCH_VENCE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_VENCE) + "', ";

            if (!(String.IsNullOrEmpty(TIPO_CONTRATO)))
            {
                sql += "'" + TIPO_CONTRATO + "', ";
                informacion += "TIPO_CONTRATO = '" + TIPO_CONTRATO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO DE CONTRATO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBJ_CONTRATO)))
            {
                sql += "'" + OBJ_CONTRATO + "', ";
                informacion += "OBJ_CONTRATO = '" + OBJ_CONTRATO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBJETO DEL CONTRATO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FIRMADO)))
            {
                sql += "'" + FIRMADO + "', ";
                informacion += "FIRMADO = '" + FIRMADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FIRMADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ENVIO_CTE)))
            {
                sql += "'" + ENVIO_CTE + "', ";
                informacion += "ENVIO_CTE = '" + ENVIO_CTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ENVIO CLIENTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_MOD)))
            {
                sql += "'" + USU_MOD + "', ";
                informacion += "USU_MOD = '" + USU_MOD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO QUE MODIFICA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INI_CONTRATO) + "', ";
            informacion += "FCH_INI_CONTRATO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INI_CONTRATO) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN_CONTRATO) + "'";
            informacion += "FCH_FIN_CONTRATO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN_CONTRATO) + "'";

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_MOD, tabla.VEN_R_CONTRATOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public Boolean ActualizarServicioRespectivo(Decimal ID_SERVICIO_RESPECTIVO,
            String DESCRIPCION,
            DateTime FECHA_INICIO,
            DateTime FECHA_VENCE,
            String USU_MOD,
            String TIPO_SERVICIO_RESPECTIVO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_r_servicios_respectivos_actualizar ";

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

            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "DESCRIPCION = 'NULL', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO) + "', ";
            informacion += "FECHA_INICIO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INICIO) + "', ";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_VENCE) + "', ";
            informacion += "FECHA_VENCE = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_VENCE) + "', ";

            if (!(String.IsNullOrEmpty(USU_MOD)))
            {
                sql += "'" + USU_MOD + "', ";
                informacion += "USU_MOD = '" + USU_MOD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO QUE MODIFICÓ no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_SERVICIO_RESPECTIVO)))
            {
                sql += "'" + TIPO_SERVICIO_RESPECTIVO + "'";
                informacion += "TIPO_SERVICIO_RESPECTIVO = '" + TIPO_SERVICIO_RESPECTIVO + "'";
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


        public DataTable ObtenerContratosDeServicioPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_contratos_buscarPorIdEmpresa ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA;
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
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

        public DataTable ObtenerInformacionBasicaporId_Empresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_INFO_BASICA_COMERCIAL ";

            if (ID_EMPRESA != 0) sql += " '" + ID_EMPRESA + "'";
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
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
        public DataTable ObtenerITEMSInformacionBasicapor(int operaciones, String Codigo, String Descripcion)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "USP_INFO_BASICA_COMERCIAL_ADMINISTRADOR ";

            sql += " '" + operaciones + "', " + "'" + Codigo + "', " + "'" + Descripcion + "'";
            _dataSet = conexion.ExecuteReader(sql);
            _dataView = _dataSet.Tables[0].DefaultView;
            _dataTable = _dataView.Table;
            return _dataTable;
        }

        public DataTable ObtenerContratosDeServicioPorRazSocial(String razSocial)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_contratos_buscarPorRazSocial ";
            sql += "'" + razSocial + "'";

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

        public DataTable ObtenerHistorialObservacionesPorEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_contratos_obtener_observaciones_por_empresa ";

            if (ID_EMPRESA != 0) sql += ID_EMPRESA;
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
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

        public DataTable ObtenerSemaforoContratosDeServico()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_contratos_obtener_semaforo_contratos";

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

        public DataTable ObtenerContratosDeServicioIdContrato(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_contratos_buscarPorIdContrato ";

            if (REGISTRO != 0) sql += REGISTRO;
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0. \n";
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

        public DataTable ObtenerContratoDeServicioVigentePorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_contratos_buscarPorIdEmpresaContratoVigente ";

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

        public DataTable ObtenerServiciosRespectivosPorContrato(Decimal REGISTRO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_servicios_respectivos_buscarPorIdContrato ";

            if (REGISTRO != 0) sql += REGISTRO;
            else
            {
                MensajeError = "El campo REGISTRO DEL CONTRATO no puede ser 0. \n";
                ejecutar = false;
            }

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


        public DataTable ObtenerHistorialEnviosDevolucionesPorIdContrato(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_hist_envios_devoluciones_obtener_por_id_contrato ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0. \n";
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
        #endregion metodos
    }
}
