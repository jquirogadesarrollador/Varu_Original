using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB.nomina.reportes
{
    public class SoporteNomina
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        DataTable dataTable;
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

        public DataTable Soporte
        {
            get { return dataTable; }
            set { dataTable = value; }
        }

        #endregion propiedades

        #region constructores
        public SoporteNomina(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable Construir(string periodos)
        {
            Conexion conexion = new Conexion(Empresa);
            Soporte = new DataTable();
            DataSet dataSet;

            string sql = "usp_reporte_soporte_nomina_V2 ";
            sql += "'" + periodos + "'";

            dataSet = conexion.ExecuteReader(sql);

            Configurar(dataSet.Tables[0]);
            Cargar(dataSet.Tables[1]);

            return Soporte;
        }

        private void Configurar(DataTable dataTable)
        {
            try
            {
                Soporte.Columns.Add("empresa");
                Soporte.Columns.Add("periodo");
                Soporte.Columns.Add("ciudad");
                Soporte.Columns.Add("centro_costo");
                Soporte.Columns.Add("sub_centro_costo");
                Soporte.Columns.Add("numero_documento");
                Soporte.Columns.Add("cargo");
                Soporte.Columns.Add("id_empleado");
                Soporte.Columns.Add("apellidos");
                Soporte.Columns.Add("nombres");
                Soporte.Columns.Add("fecha_ingreso");
                Soporte.Columns.Add("fecha_liquidacion");
                Soporte.Columns.Add("sueldo");
                Soporte.Columns.Add("dias_trabajados");

                if (!dataTable.Rows.Count.Equals(0))
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Soporte.Columns.Add("$_" + dataRow["codigo_concepto"].ToString());
                        Soporte.Columns.Add("C_" + dataRow["codigo_concepto"].ToString());
                    }
                }

                Soporte.Columns.Add("total_devengados");
                Soporte.Columns.Add("total_deducidos");
                Soporte.Columns.Add("total_nomina");
                Soporte.AcceptChanges();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void Cargar(DataTable dataTable)
        {
            try
            {
                DataRow _dataRow = Soporte.NewRow();
                DataRow[] dataRows;
                if (!dataTable.Rows.Count.Equals(0))
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        dataRows = Soporte.Select("id_empleado = '" + dataRow["id_empleado"].ToString() + "'");
                        if (dataRows.Length.Equals(0))
                        {
                            _dataRow = Soporte.NewRow();
                            _dataRow["empresa"] = dataRow["empresa"];
                            _dataRow["periodo"] = dataRow["periodo"];
                            _dataRow["ciudad"] = dataRow["ciudad"];
                            _dataRow["centro_costo"] = dataRow["centro_costo"];
                            _dataRow["sub_centro_costo"] = dataRow["sub_centro_costo"];
                            _dataRow["numero_documento"] = dataRow["numero_documento"];
                            _dataRow["cargo"] = dataRow["cargo"];
                            _dataRow["id_empleado"] = dataRow["id_empleado"];
                            _dataRow["apellidos"] = dataRow["apellidos"];
                            _dataRow["nombres"] = dataRow["nombres"];

                            try
                            {
                                _dataRow["fecha_ingreso"] = Convert.ToDateTime(dataRow["fecha_ingreso"]).ToShortDateString();
                            }
                            catch
                            {
                                _dataRow["fecha_ingreso"] = DBNull.Value;
                            }

                            try
                            {
                                _dataRow["fecha_liquidacion"] = Convert.ToDateTime(dataRow["fecha_liquidacion"]).ToShortDateString();
                            }
                            catch
                            {
                                _dataRow["fecha_liquidacion"] = DBNull.Value;
                            }

                            _dataRow["sueldo"] = dataRow["sueldo"];
                            _dataRow["dias_trabajados"] = dataRow["dias_trabajados"];
                            _dataRow["total_devengados"] = dataRow["total_devengados"];
                            _dataRow["total_deducidos"] = dataRow["total_deducidos"];
                            _dataRow["total_nomina"] = dataRow["total_nomina"];

                            dataRows = dataTable.Select("numero_documento = '" + dataRow["numero_documento"].ToString() + "' and fecha_ingreso = '" + dataRow["fecha_ingreso"].ToString() + "'");
                            if (!dataRows.Length.Equals(0))
                            {
                                for (int i = 0; i < dataRows.Length; i++)
                                {
                                    _dataRow["$_" + dataRows[i]["descripcion_concepto"].ToString()] = dataRows[i]["valor"].ToString().Replace(",", ".");
                                    _dataRow["C_" + dataRows[i]["descripcion_concepto"].ToString()] = dataRows[i]["cantidad"].ToString().Replace(",", ".");
                                }
                            }
                            Soporte.Rows.Add(_dataRow);
                            Soporte.AcceptChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion metodos
    }
}
