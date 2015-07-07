using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.nomina;

namespace Brainsbits.LLB.fonPromover
{
    public class novedadFonpromover
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
        private Decimal _idEmpresa = 0;
        private Decimal _idEmpleado = 0;
        private Decimal _idConcepto = 0;
        private String _idCiudad = null;
        private Decimal _idCentroCosto = 0;
        private Decimal _idSubCentroCosto = 0;
        private Decimal _idPeriodo = 0;

        private enum estado
        {
            ACTIVO = 0,
            LIQUIDACION = 1
        }

        private enum origen
        {
            I = 0,
            M = 1
        }

        private enum estadoEmpleado
        {
            Si = 0,
            No = 1
        }

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

        public Decimal IdEmpleado
        {
            get { return _idEmpleado; }
            set { _idEmpleado = value; }
        }

        public Decimal IdConcepto
        {
            get { return _idConcepto; }
            set { _idConcepto = value; }
        }

        public Decimal IdPeriodo
        {
            get { return _idPeriodo; }
            set { _idPeriodo = value; }
        }

        public String Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }

        public String Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        #endregion propiedades

        #region constructores
        public novedadFonpromover(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public int Adicionar(string idEmpresa, string usuario, string nombre, string numeroDocumento, string codigoConcepto, string cantidad, string valor, Conexion conexion)
        {
            string validado = "9";
            string sql = null;
            try
            {
                sql = "usp_tmp_novedades_fonpromover_adicionar";
                sql += "'" + idEmpresa.ToString() + "',";
                sql += "'" + usuario.ToString() + "',";
                sql += "'" + nombre.ToString() + "',";
                sql += "'" + numeroDocumento.ToString() + "',";
                sql += "'" + codigoConcepto.ToString() + "',";
                sql += "'" + cantidad.ToString() + "',";
                sql += "'" + valor.ToString() + "'";
                validado = conexion.ExecuteScalar(sql);  
            }
            catch (Exception e)
            {
                throw new Exception("Error originado en capa de datos. " + e.Message);
            }
            return Convert.ToInt32(validado);
        }

        public DataTable Importar(StreamReader streamReader, Decimal ID_EMPRESA)
        {
            Configurar();
            Cargar(streamReader, ID_EMPRESA.ToString());
            if (_dataTable.Rows.Count > 0)
            {
                _dataTable.DefaultView.Sort = "nombre, codigoConcepto ASC";
                MensajeError = "El cargue de novedades presento inconsistencias, corregir e intentar nuevamente.";
            }
            else MensajeError = "El proceso de cargue de novedades finalizao satisfactoriamente.";

            return _dataTable;
        }

        private DataTable Limpiar(DataTable dataTable)
        {
            foreach (DataRow _dataRow in dataTable.Rows)
            {
                if (String.IsNullOrEmpty(_dataRow["Inconsistencia"].ToString())) _dataRow.Delete();
            }
            return dataTable;
        }

        public DataTable CargarEmpresas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_empresas_fonpromover ";

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
            return _dataTable;
        }

        public DataTable CargarConceptos(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_obtener_conceptos_fonpromover ";

            sql += ID_EMPRESA.ToString();

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
            return _dataTable;
        }

        #endregion metodos

        #region reglas
        private void Configurar()
        {
            _dataTable = new DataTable();
            _dataTable.Columns.Add("registro");
            _dataTable.Columns.Add("numeroDocumento");
            _dataTable.Columns.Add("nombre");
            _dataTable.Columns.Add("codigoConcepto");
            _dataTable.Columns.Add("cantidad");
            _dataTable.Columns.Add("valor");
            _dataTable.Columns.Add("inconsistencia");
            _dataTable.Columns.Add("idEmpleado");
            _dataTable.Columns.Add("idConcepto");
            _dataTable.Columns.Add("idEmpresa");
            _dataTable.Columns.Add("idCiudad");
            _dataTable.Columns.Add("idPeriodo");
        }

        private void Cargar(StreamReader streamReader, string idEmpresa)
        {
            System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
            DataTable dataTable = new DataTable();
            int _int;
            Int32 registro = 0;
            Decimal _decimal;
            String linea = "";
            char[] delimitadores = { ';' };
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {

                while (linea != null)
                {
                    linea = streamReader.ReadLine();
                    registro++;
                    if (linea != null)
                    {
                        arrayList.Add(linea);
                        string[] datos = linea.Split(delimitadores);
                        if (datos.Length == 5)
                        {

                            if (Decimal.TryParse(datos[1].ToString(), out _decimal))
                            {
                                if (Int32.TryParse(datos[2].ToString(), out _int))
                                {
                                    if (!string.IsNullOrEmpty(datos[3].ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(datos[4].ToString()))
                                        {
                                            int ValideRegistro = 0;
                                            ValideRegistro = Adicionar(idEmpresa, Usuario, datos[0].ToString(), datos[1].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), conexion);
                                            if (ValideRegistro > 0)
                                            {
                                                if (ValideRegistro == 1) Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "La cedula reportada no esta registrada en las solicitudes de ingreso.");
                                                if (ValideRegistro == 2) Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Este trabajador no tiene contratos activos.");
                                                if (ValideRegistro == 3) Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Este trabajador tiene mas de un contrato activo.");
                                                if (ValideRegistro == 9) Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Error desconocido, consulte con el administrador.");
                                            }
                                        }
                                        else Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: El valor no puede estar vacio, sino existe debe estar en cero(0)");
                                    }
                                    else Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: La cantidad no puede estar vacia, sino existe debe estar en cero(0)");
                                }
                                else Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: El concepto no es númerico");
                            }
                            else Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: El número de documento no es númerico");
                        }
                        else Adicionar(datos[1].ToString(), datos[0].ToString(), datos[2].ToString(), datos[3].ToString(), datos[4].ToString(), "Inconsistencia: El registro " + registro.ToString() + ", no tienen la cantidad de columnas requerida");
                    }
                }
                dataTable = Procesar(idEmpresa, Usuario, conexion);
                if (!dataTable.Rows.Count.Equals(0)) Cargar(dataTable);
                conexion.AceptarTransaccion();
            }
            catch (Exception e)
            {
                conexion.DeshacerTransaccion();
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        private void Cargar(DataTable dataTable)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Adicionar(dataRow["numero_documento"].ToString(), dataRow["nombre"].ToString(), dataRow["codigo_concepto"].ToString(), dataRow["cantidad"].ToString(), dataRow["valor"].ToString(), dataRow["inconsistencia"].ToString());
            }
        }

        private DataTable Procesar(string idEmpresa, string usuario, Conexion conexion)
        {
            string sql = null;
            DataSet dataSet = new DataSet();
            try
            {
                sql = "usp_tmp_novedades_fonpromover_procesar ";
                sql += "'" + idEmpresa.ToString() + "',";
                sql += "'" + usuario.ToString() + "'";
                dataSet = conexion.ExecuteReaderConTransaccion(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Error originado en capa de datos. " + e.Message);
            }
            return dataSet.Tables[0].DefaultView.Table;
        }
        private void Limpiar()
        {
            _registro = String.Empty;
            _nombre = String.Empty;
            _numeroDocumento = String.Empty;
            _codigoConcepto = String.Empty;
            _cantidad = String.Empty;
            _valor = String.Empty;
            _idEmpleado = 0;
            _idConcepto = 0;
            _idCiudad = String.Empty;
            _idCentroCosto = 0;
            _idSubCentroCosto = 0;
            _idPeriodo = 0;
        }

        private void Adicionar(String inconsistencia)
        {
            DataRow _dataRow = _dataTable.NewRow();
            _dataRow["registro"] = _registro;
            _dataRow["numeroDocumento"] = _numeroDocumento;
            _dataRow["nombre"] = _nombre;
            _dataRow["codigoConcepto"] = _codigoConcepto;
            _dataRow["cantidad"] = _cantidad;
            _dataRow["valor"] = _valor;
            _dataRow["idEmpresa"] = _idEmpresa;
            _dataRow["idEmpleado"] = _idEmpleado;
            _dataRow["idConcepto"] = _idConcepto;
            _dataRow["idCiudad"] = _idCiudad;
            _dataRow["idCentorCosto"] = _idCentroCosto;
            _dataRow["idSubCentorCosto"] = _idSubCentroCosto;
            _dataRow["idPeriodo"] = _idPeriodo;
            _dataRow["inconsistencia"] = inconsistencia;
            _dataTable.Rows.Add(_dataRow);
            _dataTable.AcceptChanges();
        }

        private void Adicionar(string numeroDocumento, string nombre, string codigoConcepto, string cantidad, string valor, string inconsistencia)
        {
            DataRow dataRow = _dataTable.NewRow();
            dataRow["numeroDocumento"] = numeroDocumento;
            dataRow["nombre"] = nombre;
            dataRow["codigoConcepto"] = codigoConcepto;
            dataRow["cantidad"] = cantidad;
            dataRow["valor"] = valor;
            dataRow["inconsistencia"] = inconsistencia;
            _dataTable.Rows.Add(dataRow);
            _dataTable.AcceptChanges();
        }

        public Boolean ValidarNumeroDocumento(String numeroDocumento)
        {
            Boolean encontrado = true;
            radicacionHojasDeVida solicitudIngreso = new radicacionHojasDeVida(Empresa, Usuario);
            DataTable _dataTable = solicitudIngreso.ObtenerRegSolicitudesingresoPorNumDocIdentidad(numeroDocumento);
            if (_dataTable.Rows.Count == 0) encontrado = false;
            return encontrado;
        }

        public DataRow ObtenerModeloNomina(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C)
        {
            condicionComercial _condicionComercial = new condicionComercial(Empresa, Usuario);
            DataTable _dataTable = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C);
            DataRow _dataRow = null;
            if (_dataTable.Rows.Count > 0) _dataRow = _dataTable.Rows[0];
            return _dataRow;
        }

        public DataRow ObtenerEmpleado(String numeroDocumento)
        {
            registroContrato _contrato = new registroContrato(Empresa, Usuario);
            DataTable _dataTable = _contrato.ObtenerPorNumeroIdentificacion(numeroDocumento);
            DataRow _dataRow = null;
            if (_dataTable.Rows.Count > 0)
            {
                foreach (DataRow _dataRow_Origen in _dataTable.Rows)
                {
                    if (_dataRow_Origen["ACTIVO"].ToString() == "Si")
                    {
                        _idCiudad = !String.IsNullOrEmpty(_dataRow_Origen["ID_CIUDAD"].ToString()) ? _dataRow_Origen["ID_CIUDAD"].ToString() : null;
                        _idCentroCosto = !String.IsNullOrEmpty(_dataRow_Origen["ID_CENTRO_C"].ToString()) ? Convert.ToDecimal(_dataRow_Origen["ID_CENTRO_C"].ToString()) : 0;
                        _idSubCentroCosto = !String.IsNullOrEmpty(_dataRow_Origen["ID_SUB_C"].ToString()) ? Convert.ToDecimal(_dataRow_Origen["ID_SUB_C"].ToString()) : 0;

                        return _dataRow_Origen;
                    }
                }
            }
            _dataTable.Dispose();

            return _dataRow;
        }

        public Decimal ObtenerIdConcepto(String codigoConcepto)
        {
            Decimal idConcepto = 0;
            conceptosNomina _conceptosNomina = new conceptosNomina(Empresa, Usuario);
            DataTable _dataTable = _conceptosNomina.ObtenerPorCodigo(codigoConcepto);
            if (_dataTable.Rows.Count > 0)
            {
                DataRow _dataRow = _dataTable.Rows[0];
                _dataTable.Dispose();
                idConcepto = Convert.ToDecimal(_dataRow["ID_CONCEPTO"]);
            }
            return idConcepto;
        }

        #endregion reglas
    }
}