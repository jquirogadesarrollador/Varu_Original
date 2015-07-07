using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.almacen
{
    public class nivelesAutorizacionOrden
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        Decimal _ID_FIRMA = 0;
        Decimal _ID_ROL = 0;
        Decimal _TOPE = 0;
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
        public Decimal ID_FIRMA
        {
            get { return _ID_FIRMA; }
            set { _ID_FIRMA = value; }
        }
        public Decimal ID_ROL
        {
            get { return _ID_ROL; }
            set { _ID_ROL = value; }
        }
        public Decimal TOPE
        {
            get { return _TOPE; }
            set { _TOPE = value; }
        }
        #endregion propiedades

        #region constructores
        public nivelesAutorizacionOrden(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerRolConMaximoPrivilegio()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_firmas_orden_compra_obtener_rol_maximo_acceso";

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

        public DataTable ObtenerListaNivelesAutorizacionSinMAximoNivel()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_firmas_orden_compra_obtener_lista_permisos_sin_maximo_nivel";

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

        public DataTable ObtenerListaNivelesAutorizacionSinMAximoNivel(Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_firmas_orden_compra_obtener_lista_permisos_sin_maximo_nivel";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return _dataTable;
        }


        public Decimal AdicionarAlmFirmaOrdenCompraMaximoNivel(Decimal ID_ROL, String NOMBRE_IMG_FIRMA)
        {
            Decimal ID_FIRMA = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_firmas_orden_compra_adicionar ";

            #region validaciones
            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_IMG_FIRMA) == false)
            {
                sql += "'" + NOMBRE_IMG_FIRMA + "', ";
                informacion += "NOMBRE_IMG_FIRMA = '" + NOMBRE_IMG_FIRMA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NOMBRE_IMG_FIRMA = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    ID_FIRMA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_FIRMA <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para asignar rol a maximo nivel.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_FIRMAS_ORDEN_COMPRA, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro para asignar rol a maximo nivel.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
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
                return ID_FIRMA;
            }
            else
            {
                return 0;
            }
        }


        public Decimal AdicionarNivelAutorizacion(Decimal ID_ROL, String NOMBRE_IMG_FIRMA, Decimal TOPE, Conexion conexion)
        {
            Decimal ID_FIRMA = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_alm_firmas_orden_compra_adicionar_niveles ";

            #region validaciones
            if (ID_ROL != 0)
            {
                sql += ID_ROL + ", ";
                informacion += "ID_ROL = '" + ID_ROL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ROL no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE_IMG_FIRMA) == false)
            {
                sql += "'" + NOMBRE_IMG_FIRMA + "', ";
                informacion += "NOMBRE_IMG_FIRMA = '" + NOMBRE_IMG_FIRMA + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "NOMBRE_IMG_FIRMA = 'NULL', ";
            }

            if (TOPE != 0)
            {
                sql += TOPE + ", ";
                informacion += "TOPE = '" + TOPE + "', ";
            }
            else
            {
                MensajeError = "El campo TOPE no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_FIRMA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_FIRMA <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para asignar rol a un nivel de autorización.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.ALM_FIRMAS_ORDEN_COMPRA, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro para asignar rol a un nivel de autorización.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return ID_FIRMA;
            }
            else
            {
                return 0;
            }
        }


        public Boolean ActualizarListaNivelesAutorizacion(List<nivelesAutorizacionOrden> listaniveles)
        {
            Boolean resultado = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                producto _producto = new producto(Empresa, Usuario);
                DataTable tablaNivelesActuales = ObtenerListaNivelesAutorizacionSinMAximoNivel(conexion);
                Boolean verificador = true;
                foreach (DataRow fila in tablaNivelesActuales.Rows)
                {
                    verificador = true;
                    foreach (nivelesAutorizacionOrden infoNiveles in listaniveles)
                    {
                        if (infoNiveles.ID_FIRMA == Convert.ToDecimal(fila["ID_FIRMA"]))
                        {
                            verificador = false;
                            break;
                        }
                    }

                    if (verificador == true)
                    {
                        if (DesactivarNivelAutorizacion(Convert.ToDecimal(fila["ID_FIRMA"]), conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            resultado = false;
                            break;
                        }
                    }
                }

                if (resultado == true)
                {
                    foreach (nivelesAutorizacionOrden infoNivel in listaniveles)
                    {
                        if (infoNivel.ID_FIRMA == 0)
                        {
                            if (AdicionarNivelAutorizacion(infoNivel.ID_ROL, null, infoNivel.TOPE, conexion) <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                resultado = false;
                                break;
                            }
                        }
                    }
                }

                if (resultado == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                resultado = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return resultado;
        }


        public Boolean DesactivarNivelAutorizacion(Decimal ID_FIRMA, Conexion conexion)
        {
            int cantidadRegistrosActualizados = 0;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_firmas_orden_compra_desactivar_nivel ";

            if (ID_FIRMA != 0)
            {
                sql += ID_FIRMA;
            }
            else
            {
                MensajeError += "El campo ID_FIRMA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion metodos
    }
}
