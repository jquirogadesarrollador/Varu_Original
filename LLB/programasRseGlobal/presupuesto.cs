using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;

using System.IO;

namespace Brainsbits.LLB.programasRseGlobal
{
    public class presupuesto
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
        public presupuesto()
        {

        }
        public presupuesto(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerPresupuestosGeneralesArea(Programa.Areas area)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_presupuestos_generales_obtenerPorArea ";

            sql += "'" + area.ToString() + "'";

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

        public Decimal AdicionarPresupuestoGeneral(Int32 ANIO,
            Programa.Areas ID_AREA,
            Decimal MONTO,
            String DESCRIPCION)
        {
            Decimal ID_PRES_GEN = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_prog_presupuestos_generales_adicionar ";

            if (ANIO != 0)
            {
                sql += ANIO + ", ";
                informacion += "ANIO = '" + ANIO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo AÑO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + ID_AREA.ToString() + "', ";

            if (MONTO != 0)
            {
                sql += MONTO.ToString().Replace(",", ".") + ", ";
                informacion += "MONTO = '" + MONTO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo MONTO no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError = "El campo DESCRIPCION no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    ID_PRES_GEN = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_PRESUPUESTOS_GENERALES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_PRES_GEN = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return ID_PRES_GEN;
        }


        public Boolean ActualizarPresupuestoGeneral(Decimal ID_PRES_GEN,
            Decimal MONTO,
            String DESCRIPCION)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            int numRegistrosAfectados = 0;

            tools _tools = new tools();

            sql = "usp_prog_presupuestos_generales_actualizar ";

            #region validaciones
            if (ID_PRES_GEN != 0)
            {
                sql += ID_PRES_GEN + ", ";
                informacion += "ID_PRES_GEN = '" + ID_PRES_GEN + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PRES_GEN no puede ser vacio.";
                ejecutar = false;
            }

            if (MONTO != 0)
            {
                sql += MONTO.ToString().Replace(",", ".") + ", ";
                informacion += "MONTO = '" + MONTO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo MONTO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError = "El campo DESCRIPCION no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);

                    if (numRegistrosAfectados <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        numRegistrosAfectados = 0;
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);

                        if (_auditoria.Adicionar(Usuario, tabla.PROG_PRESUPUESTOS_GENERALES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            numRegistrosAfectados = 0;
                            ejecutadoCorrectamente = false;
                        }
                    }

                    if (ejecutadoCorrectamente == true)
                    {
                        conexion.AceptarTransaccion();
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                    numRegistrosAfectados = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
                numRegistrosAfectados = 0;
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

        public DataTable ObtenerHistPresupuestoGeneral(Decimal ID_PRES_GEN)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_hist_presupuestos_gen_obtenerPorIdPresGen ";

            if (ID_PRES_GEN != 0)
            {
                sql += ID_PRES_GEN;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PRES_GEN no puede ser vacio.";
            }

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

        public DataTable ObtenerHistPresupuestoPorIdPresupuesto(Decimal ID_PRESUPUESTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_hist_presupuestos_PorIdPresupuesto ";

            if (ID_PRESUPUESTO != 0)
            {
                sql += ID_PRESUPUESTO;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PRESUPUESTO no puede ser vacio.";
            }

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


        public DataTable ObtenerPresupuestosEmpresasesPorPresGen(Decimal ID_PRES_GEN, Int32 ANNO, Decimal idEmpresa)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_presupuestos_obtenerPorIdPresGen ";

            if (ID_PRES_GEN != 0)
            {
                sql += ID_PRES_GEN + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_PRES_GEN no puede ser vacio.";
            }

            if (ANNO != 0)
            {
                sql += ANNO + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ANNO no puede ser vacio.";
            }

            sql += idEmpresa + ", ";

            sql += "'" + Usuario + "'";

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


        public DataTable ObtenerPresupuestoAsiganadoEjecutadoParaEmpresaAñoMesArea(Int32 ANIO,
            Decimal ID_EMPRESA,
            Programa.Areas ID_AREA,
            Int32 MES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_detalle_actividades_ObtenerInfoPresupuesto_PorAnioEmpresaAreaMes ";

            if (ANIO != 0)
            {
                sql += ANIO + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ANIO no puede ser nulo.";
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_EMPRESA no puede ser nulo.";
            }

            sql += "'" + ID_AREA.ToString() + "', ";

            if (MES != 0)
            {
                sql += MES;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo MES no puede ser nulo.";
            }

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
        public DataTable ObtenerHistoricoPresupuestosPorEmpresaYArea(Decimal ID_EMPRESA, Programa.Areas area)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_presupuestos_obtenerPorIdEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + area.ToString() + "'";

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

        public DataTable ObtenerPresupuestoPorId(Decimal ID_PRESUPUESTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_presupuestos_obtenerPorIdPresupuesto ";

            if (ID_PRESUPUESTO != 0)
            {
                sql += ID_PRESUPUESTO;
            }
            else
            {
                MensajeError += "El campo ID_PRESUPUESTO no puede ser nulo. \n";
                ejecutar = false;
            }

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

        public Decimal AdicionarPresupuestoEmpresa(Decimal ID_EMPRESA,
            int ANNO,
            Decimal PRESUPUESTO,
            String OBSERVACIONES,
            Programa.Areas area,
            Decimal ID_PRES_GENERAL)
        {
            Decimal ID_PRESUPUESTO = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_prog_presupuestos_adicionar ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (ANNO != 0)
            {
                sql += ANNO + ", ";
                informacion += "ANNO = " + ANNO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (PRESUPUESTO != 0)
            {
                sql += PRESUPUESTO.ToString().Replace(',', '.') + ", ";
                informacion += "PRESUPUESTO = " + PRESUPUESTO.ToString().Replace(',', '.') + ", ";
            }
            else
            {
                MensajeError = "El campo PRESUPUESTO no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo OBSERVACIONES no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + area.ToString() + "', ";
            informacion += "ID_AREA = '" + area.ToString() + "', ";

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario + "', ";

            if (ID_PRES_GENERAL != 0)
            {
                sql += ID_PRES_GENERAL;
                informacion += "ID_PRES_GENERAL = '" + ID_PRES_GENERAL.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo ID_PRES_GENERAL no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    ID_PRESUPUESTO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_PRESUPUESTOS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_PRESUPUESTO = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return ID_PRESUPUESTO;
        }


        public Boolean ActualizarPresupuesto(Decimal ID_PRESUPUESTO,
            Decimal PRESUPUESTO,
            String OBSERVACIONES)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_presupuestos_actualizar ";

            #region validaciones
            if (ID_PRESUPUESTO != 0)
            {
                sql += ID_PRESUPUESTO + ", ";
                informacion += "ID_PRESUPUESTO = '" + ID_PRESUPUESTO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PRESUPUESTO no puede ser vacio.";
                ejecutar = false;
            }

            if (PRESUPUESTO != 0)
            {
                sql += PRESUPUESTO.ToString().Replace(',', '.') + ", ";
                informacion += "PRESUPUESTO = '" + PRESUPUESTO.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                MensajeError = "El campo ID_PRESUPUESTO no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError = "El campo OBSERVACIONES no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_PRESUPUESTOS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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
        #endregion metodos

    }
}
