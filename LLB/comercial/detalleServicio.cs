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
    public class detalleServicio
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
        Decimal _ID_SERVICIO = 0;
        Decimal _ID_SERVICIO_POR_EMPRESA = 0;
        Decimal _ID_DETALLE_SERVICIO = 0;
        Decimal _ID_SERVICIO_COMPLEMENTARIO = 0;
        String _NOMBRE_SERVICIO = null;
        Decimal _AIU = 0;
        Decimal _IVA = 0;
        Decimal _VALOR = 0;
        String _ACCION = null;

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

        public Decimal ID_DETALLE_SERVICIO
        {
            get { return _ID_DETALLE_SERVICIO; }
            set { _ID_DETALLE_SERVICIO = value; }
        }
        public Decimal ID_SERVICIO
        {
            get { return _ID_SERVICIO; }
            set { _ID_SERVICIO = value; }
        }

        public Decimal ID_SERVICIO_POR_EMPRESA
        {
            get { return _ID_SERVICIO_POR_EMPRESA; }
            set { _ID_SERVICIO_POR_EMPRESA = value; }
        }

        public Decimal ID_SERVICIO_COMPLEMENTARIO
        {
            get { return _ID_SERVICIO_COMPLEMENTARIO; }
            set { _ID_SERVICIO_COMPLEMENTARIO = value; }
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
        #endregion propiedades

        #region constructores
        public detalleServicio(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos


        public Decimal AdicionarDetalleServicio(Decimal ID_Servicio, Decimal ID_Servicio_Completario, Decimal AIU, Decimal IVA, Decimal VALOR, Conexion conexion)
        {
            String sql = null;
            String ID_SERVICIO = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();


            sql = "usp_detalle_servicio_adicionar ";

            #region validaciones

            sql += ID_Servicio + ", ";
            informacion += "ID_SERVICIO = '" + ID_Servicio + "' ";

            if (ID_Servicio_Completario != 0)
            {
                sql += ID_Servicio_Completario + ", ";
                informacion += "ID_SERVICIO_COMPLETARIO = '" + ID_Servicio_Completario + "' ";
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO_COMPLETARIO no puede ser nulo\n";
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
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR) + ", ";
                informacion += "VALOR = '" + VALOR + "' ";
            }
            else
            {
                sql += "0, ";
                informacion += "VALOR = 0 ";

            }

            sql += "'S', null";
            informacion += "ACIVO = 'S', FECHA_INACTIVO = NULL ";

            #endregion validaciones

            if (ejecutar)
            {

                try
                {
                    ID_SERVICIO = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_DETALLE_SERVICIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }

            }

            if (!(String.IsNullOrEmpty(ID_SERVICIO)))
                return Convert.ToDecimal(ID_SERVICIO);
            else return 0;
        }

        public DataTable ObtenerDetalleServicioPorIdServicio(Decimal ID_SERVICIO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_detalle_servicio_buscar_por_id_servicio";

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
                    _auditoria.Adicionar(Usuario, tabla.VEN_DETALLE_SERVICIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public Boolean ActualizarDetalleServicio(Decimal ID_DETALLE_SERVICIO, Decimal ID_SERVICIO, Decimal ID_SERVICIO_COMPLEMENTARIO, Decimal AIU, Decimal IVA, Decimal VALOR, String ACTIVO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_detalle_servicio_actualizar ";


            #region validaciones
            if (ID_DETALLE_SERVICIO != 0)
            {
                sql += ID_DETALLE_SERVICIO + ", ";
                informacion += "ID_DETALLE_SERVICIO = " + ID_DETALLE_SERVICIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE_SERVICIO no puede ser 0\n";
                ejecutar = false;
            }
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
            if (ID_SERVICIO_COMPLEMENTARIO != 0)
            {
                sql += ID_SERVICIO_COMPLEMENTARIO + ", ";
                informacion += "ID_SERVICIO_COMPLEMENTARIO = " + ID_SERVICIO_COMPLEMENTARIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SERVICIO_COMPLEMENTARIO no puede ser 0\n";
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
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(VALOR) + ", ";
                informacion += "VALOR = '" + VALOR + "' ";
            }
            else
            {
                sql += "0, ";
                informacion += "VALOR = 0 ";
            }
            sql += "'" + ACTIVO + "', ";
            informacion += "ACTIVO = '" + ACTIVO + "' ";

            if (ACTIVO.Equals("N"))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(System.DateTime.Now) + "'";
                informacion += "FECHA_INACTIVO = '" + _tools.obtenerStringConFormatoFechaSQLServer(System.DateTime.Now) + "'";
            }
            #endregion validaciones

            if (ejecutar)
            {

                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_DETALLE_SERVICIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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
        public Boolean BorrarDetalleServicio(Decimal ID_DETALLE_SERVICIO, Decimal ID_SERVICIO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_detalle_servicio_borrar ";


            #region validaciones
            if (ID_DETALLE_SERVICIO != 0)
            {
                sql += ID_DETALLE_SERVICIO + ", ";
                informacion += "ID_DETALLE_SERVICIO = " + ID_DETALLE_SERVICIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE_SERVICIO no puede ser 0\n";
                ejecutar = false;
            }
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

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_DETALLE_SERVICIO, tabla.ACCION_ELIMINAR, sql, informacion, conexion);
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

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }
        public DataTable ObtenerDetalleServicioPorIdServicioActivos(Decimal ID_SERVICIO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_detalle_servicio_buscar_por_id_servicio_activos ";

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
                    _auditoria.Adicionar(Usuario, tabla.VEN_DETALLE_SERVICIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public Decimal ObtenerDetalleServicioPorIdServicioIdServicioComplementario(Decimal ID_SERVICIO, Decimal ID_SERVICIO_COMPLEMENTARIO, Conexion datos)
        {
            Conexion conexion = new Conexion(Empresa);
            Decimal idDetalleServicio = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_detalle_servicio_buscar_por_id_servicio_id_servicio_complementario";

            if (ID_SERVICIO != 0)
            {
                sql += "'" + ID_SERVICIO + "',";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SERVICIO_COMPLEMENTARIO != 0)
            {
                sql += "'" + ID_SERVICIO_COMPLEMENTARIO + "',";
                informacion += "ID_SERVICIO_COMPLEMENTARIO = '" + ID_SERVICIO_COMPLEMENTARIO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO_COMPLEMENTARIO no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "null";
            if (ejecutar)
            {
                try
                {
                    idDetalleServicio = Convert.ToDecimal(datos.ExecuteScalar(sql));
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_DETALLE_SERVICIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            return idDetalleServicio;
        }
        #endregion metodos
    }
}
