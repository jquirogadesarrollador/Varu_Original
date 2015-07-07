using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;

namespace Brainsbits.LLB
{

    public class rendimiento
    {
        int _codigo;
        decimal _id_empleado;
        decimal _id_contrato;
        int _n_contrato;
        string _apellidos;
        string _nombres;
        string _tip_doc_id;
        string _doc_identidad;
        decimal _id_solicitud;
        decimal _id_empresa;
        decimal _id_centro_c;
        string _cod_cc;
        decimal _id_sub_c;
        DateTime _fch_ingreso;
        string empresa;
        DataTable _dataTable;

        public rendimiento(String idEmpresa)
        {
            empresa = idEmpresa;
        }

        public Int32 Adicionar()
        {
            Conexion conexion = new Conexion(empresa);
            DataView _dataView;
            DataTable _dataTableOrigen;
            DateTime horaInicio;

            horaInicio = System.DateTime.Now;
            Configurar();
            string sql = "select top 2000 id_empleado, id_contrato, n_contrato, apellidos, nombres, tip_doc_id, doc_identidad, id_solicitud, id_empresa, id_centro_c, cod_cc, id_sub_c, fch_ingreso "
                + "from NOM_EMPLEADOS";
            DataSet _dataSet = conexion.ExecuteReader(sql);
            _dataView = _dataSet.Tables[0].DefaultView;
            _dataTableOrigen = _dataView.Table;
            foreach (DataRow _dataRow in _dataTableOrigen.Rows)
            {
                _codigo++;
                _id_empleado = !DBNull.Value.Equals(_dataRow["id_empleado"]) ? Convert.ToDecimal(_dataRow["id_empleado"]) : 0;
                _id_contrato = !DBNull.Value.Equals(_dataRow["id_contrato"]) ? Convert.ToDecimal(_dataRow["id_contrato"]) : 0;
                _n_contrato = !DBNull.Value.Equals(_dataRow["n_contrato"]) ? Convert.ToInt32(_dataRow["n_contrato"]) : 0;
                _apellidos = !DBNull.Value.Equals(_dataRow["apellidos"]) ? _dataRow["apellidos"].ToString() : String.Empty;
                _nombres = !DBNull.Value.Equals(_dataRow["nombres"]) ? _dataRow["nombres"].ToString() : String.Empty;
                _tip_doc_id = !DBNull.Value.Equals(_dataRow["tip_doc_id"]) ? _dataRow["tip_doc_id"].ToString() : String.Empty;
                _doc_identidad = !DBNull.Value.Equals(_dataRow["doc_identidad"]) ? _dataRow["doc_identidad"].ToString() : String.Empty;
                _id_solicitud = !DBNull.Value.Equals(_dataRow["id_solicitud"]) ? Convert.ToDecimal(_dataRow["id_solicitud"]) : 0;
                _id_empresa = !DBNull.Value.Equals(_dataRow["id_empresa"]) ? Convert.ToDecimal(_dataRow["id_empresa"]) : 0;
                _id_centro_c = !DBNull.Value.Equals(_dataRow["id_centro_c"]) ? Convert.ToDecimal(_dataRow["id_centro_c"]) : 0;
                _cod_cc = !DBNull.Value.Equals(_dataRow["cod_cc"]) ? _dataRow["cod_cc"].ToString() : String.Empty;
                _id_sub_c = !DBNull.Value.Equals(_dataRow["id_sub_c"]) ? Convert.ToDecimal(_dataRow["id_sub_c"]) : 0;
                _fch_ingreso = !DBNull.Value.Equals(_dataRow["fch_ingreso"]) ? Convert.ToDateTime(_dataRow["fch_ingreso"].ToString()) : System.DateTime.Now;

                AdicionarRow();
            }

            int minutos = System.DateTime.Now.Subtract(horaInicio).Minutes;
            return minutos;
        }

        private void Configurar()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("codigo");
            _dataTable.Columns.Add("id_empleado");
            _dataTable.Columns.Add("id_contrato");
            _dataTable.Columns.Add("n_contrato");
            _dataTable.Columns.Add("apellidos");
            _dataTable.Columns.Add("nombres");
            _dataTable.Columns.Add("tip_doc_id");
            _dataTable.Columns.Add("doc_identidad");
            _dataTable.Columns.Add("id_solicitud");
            _dataTable.Columns.Add("id_empresa");
            _dataTable.Columns.Add("id_centro_c");
            _dataTable.Columns.Add("cod_cc");
            _dataTable.Columns.Add("id_sub_c");
            _dataTable.Columns.Add("fch_ingreso");
        }

        private void AdicionarRow()
        {
            DataRow _dataRow = _dataTable.NewRow();
            _dataRow["codigo"] = _codigo;
            _dataRow["id_empleado"] = _id_empleado;
            _dataRow["id_contrato"] = _id_contrato;
            _dataRow["n_contrato"] = _n_contrato;
            _dataRow["apellidos"] = _apellidos;
            _dataRow["nombres"] = _nombres;
            _dataRow["tip_doc_id"] = _tip_doc_id;
            _dataRow["doc_identidad"] = _doc_identidad;
            _dataRow["id_solicitud"] = _id_solicitud;
            _dataRow["id_empresa"] = _id_empresa;
            _dataRow["id_centro_c"] = _id_centro_c;
            _dataRow["cod_cc"] = _cod_cc;
            _dataRow["id_sub_c"] = _id_sub_c;
            _dataRow["fch_ingreso"] = _fch_ingreso;
            _dataTable.Rows.Add(_dataRow);
            _dataTable.AcceptChanges();
        }
    }
}
