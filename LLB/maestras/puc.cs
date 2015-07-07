using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.maestras
{
    public class puc
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
        public puc(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        private Boolean Eliminar(Conexion conexion, String TIPO)
        {
            Boolean ejecutar = true;
            String sql = null;
            Int32 i = 0;

            if (TIPO == "puc")
            {
                sql = "usp_par_contable_puc_eliminar ";
            }
            else
            {
                sql = "usp_par_contable_centros_eliminar ";
            }

            if (ejecutar)
            {
                try
                {
                    i = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_CONTABLE_PUC, tabla.ACCION_ELIMINAR, sql, sql, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            return true;

        }

        public Int32 Adicionar(StreamReader streamReader, String TIPO)
        {
            System.Collections.ArrayList miArray = new System.Collections.ArrayList();
            int iLinea = 0;
            string miLinea = "";
            string miSeparador = ",";
            char[] misDelimitadores = { ';' };

            String id;
            Int32 contador = 0;
            String cuenta;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();
            if (Eliminar(conexion, TIPO))
            {
                while (miLinea != null)
                {
                    miLinea = streamReader.ReadLine();
                    iLinea++;
                    if (miLinea != null)
                    {
                        miArray.Add(miLinea);
                        string[] misDatos = miLinea.Split(misDelimitadores);

                        if (misDatos.Length == 2) 
                        {
                            if ((!String.IsNullOrEmpty(Convert.ToString(misDatos[1]).TrimEnd())))
                            {
                                StringBuilder miSQL = new StringBuilder();
                                cuenta = Convert.ToString(misDatos[0]).TrimEnd().Replace("-", String.Empty);
                                cuenta = cuenta.TrimEnd().Replace("/", String.Empty);

                                if (TIPO == "puc")
                                {
                                    miSQL.Append("usp_par_contable_puc_adicionar ");
                                    miSQL.Append(cuenta != "" ? Convert.ToString("'") + cuenta + Convert.ToString("'") : Convert.ToString("Null"));
                                    miSQL.Append(miSeparador);
                                    miSQL.Append(Convert.ToString(misDatos[1]).TrimEnd() != "" ? Convert.ToString("'") + Convert.ToString(misDatos[1]).TrimEnd() + Convert.ToString("'") : Convert.ToString("Null"));
                                    miSQL.Append(miSeparador);
                                    miSQL.Append(Usuario != "" ? Convert.ToString("'") + Usuario + Convert.ToString("'") : Convert.ToString("Null"));
                                }
                                if (TIPO == "centros")
                                {
                                    miSQL.Append("usp_par_contable_centros_adicionar ");
                                    miSQL.Append(cuenta != "" ? Convert.ToString("'") + cuenta + Convert.ToString("'") : Convert.ToString("Null"));
                                    miSQL.Append(miSeparador);
                                    miSQL.Append(Convert.ToString(misDatos[1]).TrimEnd() != "" ? Convert.ToString("'") + Convert.ToString(misDatos[1]).TrimEnd() + Convert.ToString("'") : Convert.ToString("Null"));
                                    miSQL.Append(miSeparador);
                                    miSQL.Append(Usuario != "" ? Convert.ToString("'") + Usuario + Convert.ToString("'") : Convert.ToString("Null"));
                                }
                                try
                                {
                                    id = conexion.ExecuteScalar(miSQL.ToString());
                                    contador++;
                                }
                                catch (Exception e)
                                {
                                    MensajeError = e.Message;
                                }
                            }
                        }
                    }
                }
            }
            else conexion.DeshacerTransaccion();

            if (String.IsNullOrEmpty(MensajeError)) conexion.AceptarTransaccion();
            conexion.Desconectar();
            streamReader.Close();
            streamReader.Dispose();
            return contador;
        }

        public DataTable ObtenerPorCodigo(String CODIGO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_par_contable_puc_ObtenerPorCodigo ";

            if (!(String.IsNullOrEmpty(CODIGO))) sql += "'" + CODIGO + "', ";
            else
            {
                MensajeError += "El campo CODIGO no puede ser nulo\n";
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

        public DataTable ObtenerTodos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_par_contable_puc_ObtenerTodos ";

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
    }
}
