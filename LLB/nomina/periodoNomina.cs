using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.nomina
{
    public class periodoNomina
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        private DataTable _dataTable;
        private String _registro = null;
        private String _numeroDocumento = null;
        private String _nombre = null;
        private String _codigoConcepto = null;
        private String _cantidad = null;
        private String _valor = null;

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
        public periodoNomina(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public Decimal Adicionar(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C,
            Int32 PERIODO, String PER_CONT, DateTime FECHA_INI, DateTime FECHA_FIN, String ESTADO, String USU_CRE)
        {
            String sql = null;
            String id = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();

            sql = "usp_nom_periodo_adicionar ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD= '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = null, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C.ToString() + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CENTRO_C = null, ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C.ToString() + ", ";
                informacion += "ID_SUB_C = '" + ID_SUB_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_SUB_C = null, ";
            }


            if (PERIODO != 0)
            {
                sql += PERIODO.ToString() + ", ";
                informacion += "PERIODO = '" + PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError += "El PERIODO es requerido.\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PER_CONT)))
            {
                sql += "'" + PER_CONT + "', ";
                informacion += "PER_CONT = '" + PER_CONT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PER_CONT no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_INI) + "', ";
            informacion += "FECHA_INI = '" + FECHA_INI.ToString() + "'";

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_FIN) + "', ";
            informacion += "FECHA_FIN = '" + FECHA_FIN.ToString() + "'";

            if (!(String.IsNullOrEmpty(ESTADO)))
            {
                sql += "'" + ESTADO + "', ";
                informacion += "ESTADO = '" + ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    id = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(id))) return Convert.ToDecimal(id);
            else return 0;
        }

        public DataTable ObtenerPorIdEmpresa(Decimal ID_EMPRESA, Int32 ID_MODELO_NOMINA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_periodo_ObtenerPorIdEmpresa ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA debe ser ingresado.";
                ejecutar = false;
            }

            if (ID_MODELO_NOMINA > 0)
            {
                sql += ID_MODELO_NOMINA.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_MODELO_NOMINA debe ser ingresado.";
                ejecutar = false;
            }

            #endregion

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

        public DataTable ObtenerAnteriorPorIdEmpresa(Decimal ID_EMPRESA, Int32 ID_MODELO_NOMINA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_periodo_ObtenerAnteriorPorIdEmpresa ";

            #region validaciones
            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA debe ser ingresado.";
                ejecutar = false;
            }

            if (ID_MODELO_NOMINA > 0)
            {
                sql += ID_MODELO_NOMINA.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_MODELO_NOMINA debe ser ingresado.";
                ejecutar = false;
            }
            #endregion

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

        public DataTable ObtenerPorIdEmpresaCiudadCCSubCC(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_periodo_ObtenerPorId_Empresa_Ciudad_CC_SubCC ";

            #region validaciones
            if (ID_EMPRESA != 0) sql += ID_EMPRESA.ToString() + ", ";
            else
            {
                MensajeError = "El campo ID_EMPRESA debe ser ingresado.";
                ejecutar = false;
            }

            if ((String.IsNullOrEmpty(ID_CIUDAD))
                && (ID_CENTRO_C == 0)
                && (ID_SUB_C == 0))
            {
                MensajeError = "Se requiere CIUDAD y/ó CENTRO DE COSTO y/ó SUB CENTRO DE COSTO";
                ejecutar = false;
            }
            else
            {
                sql += String.IsNullOrEmpty(ID_CIUDAD) ? "0, " : "'" + ID_CIUDAD + "', ";
                sql += ID_CENTRO_C.ToString() + ", ";
                sql += ID_SUB_C.ToString();
            }
            #endregion

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

        public DataTable ObtenerPeriodoMemorando(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_periodo_ObtenerParaMemorando ";

            #region validaciones
            if (ID_EMPRESA != 0) sql += ID_EMPRESA.ToString() + ", ";
            else
            {
                MensajeError = "El campo ID_EMPRESA debe ser ingresado.";
                ejecutar = false;
            }

            if ((String.IsNullOrEmpty(ID_CIUDAD))
                && (ID_CENTRO_C == 0)
                && (ID_SUB_C == 0))
            {
                MensajeError = "Se requiere CIUDAD y/ó CENTRO DE COSTO y/ó SUB CENTRO DE COSTO";
                ejecutar = false;
            }
            else
            {
                sql += String.IsNullOrEmpty(ID_CIUDAD) ? "0, " : "'" + ID_CIUDAD + "', ";
                sql += ID_CENTRO_C.ToString() + ", ";
                sql += ID_SUB_C.ToString() + ", ";
                sql += "'" + Usuario.ToString() + "'";
            }
            #endregion

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
