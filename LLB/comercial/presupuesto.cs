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
    public class Presupuesto
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;

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
        #endregion propiedades

        #region constructores
        public Presupuesto(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        public Presupuesto()
        {
        }
        #endregion

        #region metodos
        public DataTable ObtenerAniosYPresupuestos()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_presupuestos_anios_obtener_anios_presupuestados";

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

            Decimal valorCero = 0.00M;

            if (_dataTable.Rows.Count <= 0)
            {
                _dataTable = new DataTable();
                _dataTable.Columns.Add("ANIO");
                _dataTable.Columns.Add("PRESUPUESTO");

                DataRow _dataRow = _dataTable.NewRow();

                _dataRow["ANIO"] = DateTime.Now.Year;
                _dataRow["PRESUPUESTO"] = valorCero;

                _dataTable.Rows.Add(_dataRow);
                _dataTable.AcceptChanges();
            }
            else
            {
                DataRow _dataRow = _dataTable.Rows[0];
                Int32 _anio = Convert.ToInt32(_dataRow["ANIO"]);
                Int32 _anioActual = DateTime.Now.Year;

                if (_anio < _anioActual)
                {
                    DataTable _dataTable1 = new DataTable();
                    _dataTable1.Columns.Add("ANIO");
                    _dataTable1.Columns.Add("PRESUPUESTO");

                    DataRow _dataRow1 = _dataTable1.NewRow();

                    _dataRow1["ANIO"] = _anioActual;
                    _dataRow1["PRESUPUESTO"] = valorCero;

                    _dataTable1.Rows.Add(_dataRow1);

                    foreach (DataRow _dataRow2 in _dataTable.Rows)
                    {
                        DataRow _dataRow3 = _dataTable1.NewRow();

                        _dataRow3["ANIO"] = _dataRow2["ANIO"];
                        _dataRow3["PRESUPUESTO"] = _dataRow2["PRESUPUESTO"];

                        _dataTable1.Rows.Add(_dataRow3);
                    }

                    _dataTable = _dataTable1;
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerRegionalesConPresupuestoPorAnio(Int32 anio)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_presupuestos_anios_obtener_regionales_presupuestos ";

            if (anio != 0)
            {
                sql += anio;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo AÑO no puede ser o o vacio.";
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

        public DataTable ObtenerCiudadesConPresupuestoPorIdRegional(Decimal ID_REGIONAL, Int32 anio)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_presupuestos_anios_obtener_ciudades_presupuestos ";

            if (ID_REGIONAL != 0)
            {
                sql += ID_REGIONAL + ", ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo REGIONAL no puede ser o o vacio.";
            }

            if (anio != 0)
            {
                sql += anio;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ANIO no puede ser o o vacio.";
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

        public DataTable ObtenerPresupuestosAnioDeUnaCiudad(String ID_CIUDAD, Int32 anio)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_presupuestos_anios_obtener_presupuestos_anio_de_ciudad ";

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_CIUDAD no puede ser o o vacio.";
            }

            if (anio != 0)
            {
                sql += anio;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ANIO no puede ser o o vacio.";
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

        public Boolean AdicionarActualizarPresupuestosAniosCiudad(Int32 ANIO,
            String ID_CIUDAD,
            Decimal P_ENERO,
            Decimal P_FEBRERO,
            Decimal P_MARZO,
            Decimal P_ABRIL,
            Decimal P_MAYO,
            Decimal P_JUNIO,
            Decimal P_JULIO,
            Decimal P_AGOSTO,
            Decimal P_SEPTIEMBRE,
            Decimal P_OCTUBRE,
            Decimal P_NOVIEMBRE,
            Decimal P_DICIEMBRE)
        {
            String sql = null;
            Decimal REGISTRO = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_presupuestos_anio_adicionar_actualizar ";

            #region validaciones

            if (ANIO != 0)
            {
                sql += ANIO + ", ";
                informacion += "ANIO = '" + ANIO + "', ";
            }
            else
            {
                MensajeError += "El campo ANIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (P_ENERO != 0)
            {
                sql += P_ENERO.ToString().Replace(',', '.') + ", ";
                informacion += "P_ENERO = '" + P_ENERO + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_ENERO = '" + P_ENERO + "', ";
            }

            if (P_FEBRERO != 0)
            {
                sql += P_FEBRERO.ToString().Replace(',', '.') + ", ";
                informacion += "P_FEBRERO = '" + P_FEBRERO + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_FEBRERO = '" + P_FEBRERO + "', ";
            }

            if (P_MARZO != 0)
            {
                sql += P_MARZO.ToString().Replace(',', '.') + ", ";
                informacion += "P_MARZO = '" + P_MARZO + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_MARZO = '" + P_MARZO + "', ";
            }

            if (P_ABRIL != 0)
            {
                sql += P_ABRIL.ToString().Replace(',', '.') + ", ";
                informacion += "P_ABRIL = '" + P_ABRIL + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_ABRIL = '" + P_ABRIL + "', ";
            }

            if (P_MAYO != 0)
            {
                sql += P_MAYO.ToString().Replace(',', '.') + ", ";
                informacion += "P_MAYO = '" + P_MAYO + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_MAYO = '" + P_MAYO + "', ";
            }

            if (P_JUNIO != 0)
            {
                sql += P_JUNIO.ToString().Replace(',', '.') + ", ";
                informacion += "P_JUNIO = '" + P_JUNIO + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_JUNIO = '" + P_JUNIO + "', ";
            }

            if (P_JULIO != 0)
            {
                sql += P_JULIO.ToString().Replace(',', '.') + ", ";
                informacion += "P_JULIO = '" + P_JULIO + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_JULIO = '" + P_JULIO + "', ";
            }

            if (P_AGOSTO != 0)
            {
                sql += P_AGOSTO.ToString().Replace(',', '.') + ", ";
                informacion += "P_AGOSTO = '" + P_AGOSTO + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_AGOSTO = '" + P_AGOSTO + "', ";
            }

            if (P_SEPTIEMBRE != 0)
            {
                sql += P_SEPTIEMBRE.ToString().Replace(',', '.') + ", ";
                informacion += "P_SEPTIEMBRE = '" + P_SEPTIEMBRE + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_SEPTIEMBRE = '" + P_SEPTIEMBRE + "', ";
            }

            if (P_OCTUBRE != 0)
            {
                sql += P_OCTUBRE.ToString().Replace(',', '.') + ", ";
                informacion += "P_OCTUBRE = '" + P_OCTUBRE + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_OCTUBRE = '" + P_OCTUBRE + "', ";
            }

            if (P_NOVIEMBRE != 0)
            {
                sql += P_NOVIEMBRE.ToString().Replace(',', '.') + ", ";
                informacion += "P_NOVIEMBRE = '" + P_NOVIEMBRE + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_NOVIEMBRE = '" + P_NOVIEMBRE + "', ";
            }

            if (P_DICIEMBRE != 0)
            {
                sql += P_DICIEMBRE.ToString().Replace(',', '.') + ", ";
                informacion += "P_DICIEMBRE = '" + P_DICIEMBRE + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "P_DICIEMBRE = '" + P_DICIEMBRE + "', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

                try
                {
                    REGISTRO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_PRESUPUESTOS_ANIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (REGISTRO >= 1)
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
