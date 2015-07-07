using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seleccion;

namespace Brainsbits.LLB.contratacion
{
    public class auditoriaContratos
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
        public auditoriaContratos(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region REG_AUDITORIA_CONTRATOS
        public DataTable ObtenerUltimaAuditoriaPorTablaYEmpleado(String TABLA_AUDITADA,
            Decimal ID_EMPLEADO)
        {


            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_reg_auditoria_contratos_obtener_ultima_auditoria_de_tabla_y_empleado ";

            #region validaciones
            if (String.IsNullOrEmpty(TABLA_AUDITADA) == false)
            {
                sql += "'" + TABLA_AUDITADA + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo TABLA_AUDITADA no puede ser vacio.";
            }

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO;
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser 0\n";
                ejecutar = false;
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

        public Decimal AdicionarAuditoriaContratos(Decimal ID_EMPLEADO,
            String TABLA_AUDITADA,
            Decimal ID_TABLA_AUDITADA,
            DateTime FECHA_AUDITORIA,
            Conexion conexion)
        {
            String ID_AUDITORIA = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_auditoria_contratos_adicionar ";

            #region validaciones

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TABLA_AUDITADA) == false)
            {
                sql += "'" + TABLA_AUDITADA + "', ";
                informacion += "TABLA_AUDITADA = '" + TABLA_AUDITADA + "', ";
            }
            else
            {
                MensajeError = "El campo TABLA_AUDITADA no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_TABLA_AUDITADA != 0)
            {
                sql += ID_TABLA_AUDITADA + ", ";
                informacion += "ID_TABLA_AUDITADA = '" + ID_TABLA_AUDITADA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_TABLA_AUDITADA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_AUDITORIA) + "', ";
            informacion += "FECHA_AUDITORIA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_AUDITORIA) + "', ";

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_AUDITORIA = conexion.ExecuteScalar(sql);

                    if (Convert.ToDecimal(ID_AUDITORIA) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_AUDITORIA_CONTRATOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                    else
                    {
                        MensajeError = "ERROR: intenatar ingresar en la bd la auditoria de contratos.";
                        ejecutadoCorrectamente = false;
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }

            if (ejecutadoCorrectamente)
            {
                return Convert.ToDecimal(ID_AUDITORIA);
            }
            else
            {
                return 0;
            }
        }


        public Decimal AdicionarAuditoriaContratos(Decimal ID_EMPLEADO,
            String TABLA_AUDITADA,
            Decimal ID_TABLA_AUDITADA,
            DateTime FECHA_AUDITORIA)
        {
            String ID_AUDITORIA = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_reg_auditoria_contratos_adicionar ";

            #region validaciones

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TABLA_AUDITADA) == false)
            {
                sql += "'" + TABLA_AUDITADA + "', ";
                informacion += "TABLA_AUDITADA = '" + TABLA_AUDITADA + "', ";
            }
            else
            {
                MensajeError = "El campo TABLA_AUDITADA no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_TABLA_AUDITADA != 0)
            {
                sql += ID_TABLA_AUDITADA + ", ";
                informacion += "ID_TABLA_AUDITADA = '" + ID_TABLA_AUDITADA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_TABLA_AUDITADA no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_AUDITORIA) + "', ";
            informacion += "FECHA_AUDITORIA = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_AUDITORIA) + "', ";

            sql += "'" + Usuario.ToString() + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    ID_AUDITORIA = conexion.ExecuteScalar(sql);

                    if (Convert.ToDecimal(ID_AUDITORIA) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.REG_AUDITORIA_CONTRATOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                    else
                    {
                        MensajeError = "ERROR: intenatar ingresar en la bd la auditoria de contratos.";
                        ejecutadoCorrectamente = false;
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (ejecutadoCorrectamente)
            {
                return Convert.ToDecimal(ID_AUDITORIA);
            }
            else
            {
                return 0;
            }
        }

        public Decimal ActualizarAuditoriaContratosPorSeccionYEstadoProceso(Decimal ID_EMPLEADO, String TABLA_AUDITADA, Decimal ID_AUDITADO, Boolean ACTUALIZAR_ESTADO_PROCESO, Decimal ID_SOLICITUD, String ESTADO_PROCESO)
        {

            Decimal ID_AUDITORIA = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_AUDITORIA = AdicionarAuditoriaContratos(ID_EMPLEADO, TABLA_AUDITADA, ID_AUDITADO, DateTime.Now, conexion);

                if (ID_AUDITORIA <= 0)
                {
                    conexion.DeshacerTransaccion();
                    ID_AUDITORIA = 0;
                }
                else
                {
                    if (ACTUALIZAR_ESTADO_PROCESO == true)
                    {
                        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                        if (_radicacionHojasDeVida.ActualizarEstadoProcesoRegSolicitudesIngresoAuditoria(ID_SOLICITUD, ESTADO_PROCESO, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = _radicacionHojasDeVida.MensajeError;
                            ID_AUDITORIA = 0;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                    else
                    {
                        conexion.AceptarTransaccion();
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                ID_AUDITORIA = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_AUDITORIA;
        }

        #endregion REG_AUDITORIA_CONTRATOS

        #endregion metodos
    }
}