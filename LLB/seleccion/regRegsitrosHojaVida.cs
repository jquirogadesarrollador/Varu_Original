using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class regRegsitrosHojaVida
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
        public regRegsitrosHojaVida(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos


        #region reg_registros_hoja_vida

        public Decimal AdicionarRegistroHojaVida(Decimal ID_SOLICITUD,
            String CLASE_REGISTRO,
            String COMENTARIOS,
            String MOTIVO,
            Decimal ID_REQUERIMIENTO,
            Conexion conexion)
        {
            String sql = null;
            Decimal REGISTRO = 0;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_registros_hoja_vida_adicionar ";

            #region validaciones

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD + ", ";
                informacion += "ID_EMPRESA = '" + ID_SOLICITUD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CLASE_REGISTRO)))
            {
                sql += "'" + CLASE_REGISTRO + "', ";
                informacion += "CLASE_REGISTRO = '" + CLASE_REGISTRO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CLASE_REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COMENTARIOS)))
            {
                sql += "'" + COMENTARIOS + "', ";
                informacion += "COMENTARIOS = '" + COMENTARIOS.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "COMENTARIOS = 'null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario + "', ";

            if (!(String.IsNullOrEmpty(MOTIVO)))
            {
                sql += "'" + MOTIVO + "', ";
                informacion += "MOTIVO = '" + MOTIVO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "MOTIVO = 'null', ";
            }

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO + "'";
            }
            else
            {
                sql += "null";
                informacion += "ID_REQUERIMIENTO = 'null'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    REGISTRO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_REGISTROS_HOJA_VIDA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO = 0;
                }
            }

            return REGISTRO;
        }

        public Decimal AdicionarRegRegistrosHojaVida(Decimal ID_SOLICITUD,
            String CLASE_REGISTRO,
            String COMENTARIOS,
            String MOTIVO,
            Decimal ID_REQUERIMIENTO)
        {
            Boolean correcto = true;

            Decimal REGISTRO_HOJA_VIDA = 0;
            String ARCHIVO = "";

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                REGISTRO_HOJA_VIDA = AdicionarRegistroHojaVida(ID_SOLICITUD, CLASE_REGISTRO, COMENTARIOS, MOTIVO, ID_REQUERIMIENTO, conexion);

                if (REGISTRO_HOJA_VIDA <= 0)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                    REGISTRO_HOJA_VIDA = 0;
                }
                else
                {
                    if (CLASE_REGISTRO == "DESC. SELECCION")
                    {
                        ARCHIVO = "DESCARTADO SELECCION";
                    }
                    else
                    {
                        ARCHIVO = "DISPONIBLE";
                    }

                    radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Empresa, Usuario);

                    if (_radicacionHojasDeVida.ActualizarEstadoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, ARCHIVO, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        REGISTRO_HOJA_VIDA = 0;
                        MensajeError = _radicacionHojasDeVida.MensajeError;
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
                REGISTRO_HOJA_VIDA = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return REGISTRO_HOJA_VIDA;
        }

        public DataTable ObtenerPorIdSolicitud(int ID_SOLICITUD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_reg_registros_hoja_vida_obtener_por_id_solicitud ";

            if (ID_SOLICITUD != 0)
            {
                sql += ID_SOLICITUD;
                informacion += "ID_SOLICITUD = '" + ID_SOLICITUD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SOLICITUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.REG_REGISTROS_HOJA_VIDA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
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
        #endregion
        #endregion
    }
}
