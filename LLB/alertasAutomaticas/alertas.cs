using System;
using System.Data;
using Brainsbits.LDA;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.Alertas
{
    public class Alerta
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
        public Alerta()
        {
        }

        public Alerta(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos
        private DataTable ObtenerContratosQueNecesitanNotificacionEmail(Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_r_contratos_obtener_pendiente_aviso";

            #region validaciones
            #endregion validaciones

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

        public Boolean ActualizarFechaAvisoNenRContratos(Decimal REGISTRO,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_r_contratos_actualizar_fecha_aviso ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }


        private DataTable ObtenerDatosUsuarioPorEmpreUnidadNegocio(Decimal ID_EMPRESA
            , String UNIDAD_NEGOCIO
            , Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_crt_unidad_negocio_obtener_por_id_empresa_y_unidad_negocio ";

            sql += ID_EMPRESA + ", ";

            if (String.IsNullOrEmpty(UNIDAD_NEGOCIO) == false)
            {
                sql += "'" + UNIDAD_NEGOCIO + "'";
            }
            else
            {
                MensajeError = "El campo UNIDAD_NEGOCIO no puede ser vacio.";
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

        public Boolean enviarNotificacionesObjetosContrato()
        {
            tools _tools = new tools();

            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                DataTable tablaContratosPorNotificar = ObtenerContratosQueNecesitanNotificacionEmail(conexion);

                String correos = "";
                Decimal ID_EMPRESA = 0;
                String mensajeMail;

                String RAZ_SOCIAL;
                String NUMERO_CONTRATO;
                String FECHA;
                String FCH_VENCE;
                Decimal REGISTRO;

                foreach (DataRow filaContrato in tablaContratosPorNotificar.Rows)
                {
                    try
                    {
                        ID_EMPRESA = Convert.ToDecimal(filaContrato["ID_EMPRESA"]);
                    }
                    catch
                    {
                        ID_EMPRESA = 0;
                    }

                    DataTable tablaUsuarios = ObtenerDatosUsuarioPorEmpreUnidadNegocio(ID_EMPRESA, "REP. COMERCIAL", conexion);
                    DataRow filaUsuario;

                    correos = String.Empty;

                    for (int i = 0; i < tablaUsuarios.Rows.Count; i++)
                    {
                        filaUsuario = tablaUsuarios.Rows[i];

                        if (filaUsuario["USU_MAIL"] != DBNull.Value)
                        {
                            if (i == 0)
                            {
                                correos = filaUsuario["USU_MAIL"].ToString().Trim();
                            }
                            else
                            {
                                correos += ";" + filaUsuario["USU_MAIL"].ToString().Trim();
                            }
                        }
                    }

                    if (String.IsNullOrEmpty(correos) == false)
                    {
                        RAZ_SOCIAL = filaContrato["RAZ_SOCIAL"].ToString().Trim().ToUpper();
                        NUMERO_CONTRATO = filaContrato["NUMERO_CONTRATO"].ToString();
                        REGISTRO = Convert.ToDecimal(filaContrato["REGISTRO"]);

                        try
                        {
                            FECHA = Convert.ToDateTime(filaContrato["FECHA"]).ToLongDateString();
                        }
                        catch
                        {
                            FECHA = "DESCONOCIDA";
                        }
                        try
                        {
                            FCH_VENCE = Convert.ToDateTime(filaContrato["FCH_VENCE"]).ToLongDateString();
                        }
                        catch
                        {
                            FCH_VENCE = "DESCONOCIDA";
                        }

                        mensajeMail = String.Format("<div style=\"text-align:center\"><b>AVISO -CONTRATO/OFERTA MERCANTIL- POR VENCER</b></div><br /><div style=\"text-align:justify\">Señor(a) Representante comercial de <b>{0}</b> el contrato/Oferta mercantil Numero: <b>{1}</b> esta por vencer.<br /><br /><br /><b>Numero Contrato/Oferta Mercantil:</b> {0}.<br /><b>Fecha Inicio:</b> {2}.<br /><b>Fecha Vencimiento:</b> {3}.</div>", RAZ_SOCIAL, NUMERO_CONTRATO, FECHA, FCH_VENCE);

                        if (_tools.enviarCorreoConCuerpoHtml(correos, "CONTRATO/OFERTA MERCANTIL POR VENCER", mensajeMail) == true)
                        {
                            if (ActualizarFechaAvisoNenRContratos(REGISTRO, conexion) == false)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                                break;
                            }
                        }
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