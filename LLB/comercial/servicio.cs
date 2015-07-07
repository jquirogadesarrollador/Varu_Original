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
    public class servicio
    {
        #region variables
        enum Empresas
        {
            Sertempo = 1,
            EYS = 3
        }
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
        Decimal _ID_SERVICIO_POR_EMPRESA = 0;
        Decimal _ID_SERVICIO = 0;
        String _NOMBRE_SERVICIO = null;
        Decimal _AIU = 0;
        Decimal _IVA = 0;
        Decimal _VALOR = 0;
        String _ACCION = null;
        String _DESCRIPCION = null;

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

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public Decimal ID_SERVICIO_POR_EMPRESA
        {
            get { return _ID_SERVICIO_POR_EMPRESA; }
            set { _ID_SERVICIO_POR_EMPRESA = value; }
        }

        public Decimal ID_SERVICIO
        {
            get { return _ID_SERVICIO; }
            set { _ID_SERVICIO = value; }
        }

        public String NOMBRE_SERVICIO
        {
            get { return _NOMBRE_SERVICIO; }
            set { _NOMBRE_SERVICIO = value; }
        }

        public Decimal AIU
        {
            get { return _AIU; }
            set { _AIU = value; }
        }

        public Decimal IVA
        {
            get { return _IVA; }
            set { _IVA = value; }
        }

        public Decimal VALOR
        {
            get { return _VALOR; }
            set { _VALOR = value; }
        }

        public String ACCION
        {
            get { return _ACCION; }
            set { _ACCION = value; }
        }

        public String DESCRIPCION
        {
            get { return _DESCRIPCION; }
            set { _DESCRIPCION = value; }
        }

        #endregion propiedades

        #region constructores
        public servicio(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public Decimal AdicionarServicio(String Nombre_Servicio, Decimal AIU, Decimal IVA, Decimal VALOR, String observaciones, Conexion conexion)
        {
            String sql = null;
            String ID_SERVICIO = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_servicio_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(Nombre_Servicio)))
            {
                sql += "'" + Nombre_Servicio + "', ";
                informacion += "NOMBRE_SERVICIO = '" + Nombre_Servicio.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_SERVICIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (AIU != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AIU) + ", ";
                informacion += "AIU = '" + AIU + "' ";
            }
            else
            {
                sql += "0, ";
                informacion += "AIU = 0 ";
            }

            if (IVA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(IVA) + ", ";
                informacion += "IVA = '" + IVA + "' ";
            }
            else
            {
                sql += "0, ";
                informacion += "IVA = 0 ";
            }

            if (VALOR != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR) + ",";
                informacion += "VALOR = '" + VALOR + "' ";
            }
            else
            {
                sql += "0,";
                informacion += "VALOR = 0 ";
            }
            if (!(String.IsNullOrEmpty(observaciones)))
            {
                sql += "'" + observaciones + "'";
                informacion += "observaciones = '" + observaciones.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo observaciones no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_SERVICIO = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(ID_SERVICIO))) return Convert.ToDecimal(ID_SERVICIO);
            else return 0;
        }
        public DataTable BuscarServicioporId(Decimal ID_SERVICIO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_servicio_buscar_por_id";

            if (ID_SERVICIO != 0)
            {
                sql += "'" + ID_SERVICIO + "'";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO no puede ser nulo\n";
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

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public Boolean ActualizarServicio(Decimal ID_SERVICIO, String NOMBRE_SERVICIO, Decimal AIU, Decimal IVA, Decimal VALOR, String observaciones, Conexion datos)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_servicio_actualizar ";

            #region validaciones

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = " + ID_SERVICIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SERVICIO no puede ser 0\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(NOMBRE_SERVICIO)))
            {
                sql += "'" + NOMBRE_SERVICIO + "', ";
                informacion += "NOMBRE_SERVICIO = '" + NOMBRE_SERVICIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_SERVICIO no puede ser nulo\n";
                ejecutar = false;
            }
            if (AIU != 0)
            {
                sql += "'" + AIU + "', ";
                informacion += "AIU = '" + AIU + "' ";
            }
            else
            {
                sql += "0, ";
                informacion += "AIU = 0 ";
            }
            if (IVA != 0)
            {
                sql += "'" + IVA + "', ";
                informacion += "IVA = '" + IVA + "' ";
            }
            else
            {
                sql += "0, ";
                informacion += "IVA = 0 ";
            }
            if (VALOR != 0)
            {
                sql += "'" + VALOR + "', ";
                informacion += "VALOR = '" + VALOR + "' ";
            }
            else
            {
                sql += "0, ";
                informacion += "VALOR = 0 ";
            }
            if (!(String.IsNullOrEmpty(observaciones)))
            {
                sql += "'" + observaciones + "' ";
                informacion += "observaciones = '" + observaciones.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo observaciones no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = datos.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO, tabla.ACCION_ACTUALIZAR, sql, informacion, datos);
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

        public DataTable obtenerServiciosComplementariosTodosPArametrizables(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_servicio_complementario_obtener_todos_parametrizables ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo NOMBRE_SERVICIO no puede ser nulo\n";
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

        public DataTable ObtenerServiciosComplementariosPorUbicacion(String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_servicio_complementario_ObtenerPorUbicacion ";

            #region validaciones
            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "NULL";
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


        public DataTable obtenerServiciosInfoServicioPorIdServicio(Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_ven_servicio_obtener_info_por_id_servicio ";

            #region validaciones
            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO no puede ser nulo\n";
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

        public DataTable obtenerServiciosComplementariosTodos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "USP_SERVICIO_COMPLEMENTARIO_OBTENER_TODOS ";


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
