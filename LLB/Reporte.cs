using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Brainsbits.LDA;
using Brainsbits.LLB.programasRseGlobal;
using Brainsbits.LLB.GestionHumana;

namespace Brainsbits.LLB
{
    public class Reporte
    {
        #region variables
        public enum Proceso
        {
            Comercial = 0,
            Seleccion,
            Contratacion,
            Contabilidad,
            Compras,
            programas,
            SaludOcupacional,
            operaciones,
            juridica,
            Nomina,
            Facturacion,
            Lps,
            GestionHumana,
            Autoliquidacion
        }
        private string _idEmpresa;
        private string _usuario;
        private string _clave;
        private string _base_datos;
        private string _servidor;

        #endregion variables

        #region propiedades

        public string IdEmpresa
        {
            get { return _idEmpresa; }
            set { _idEmpresa = value; }
        }
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        public string Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }

        public string BaseDatos
        {
            get { return _base_datos; }
            set { _base_datos = value; }
        }

        public string Servidor
        {
            get { return _servidor; }
            set { _servidor = value; }
        }

        #endregion propiedades

        #region constructor
        public Reporte(string idEmpresa)
        {
            IdEmpresa = idEmpresa;

            Usuario = "sa";
            Clave = "Acceso2013";
            if (idEmpresa.Equals("1")) BaseDatos = "SISER_V3";
            else BaseDatos = "FENIX_E_S_V3";
            if (idEmpresa.Equals("1")) Servidor = "198.49.128.226";
            else Servidor = "198.49.128.226";
        }
        #endregion constructor

        #region metodos
        public DataTable Listar(Proceso proceso)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;
            sql = "select alias as id, alias as nombre from reportes where proceso  = '" + proceso.ToString() + "' order by alias";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable Listar(String proceso, String Tipo)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;
            sql = "select id=rtrim(ltrim(rpt))+'*'+rtrim(ltrim(usp)), alias as nombre from reportes where proceso  = '" + proceso.ToString() + "' and ESTADO='ACTIVO' order by alias";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarClientes()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select COD_EMPRESA as id, RAZ_SOCIAL as nombre from VEN_EMPRESAS where ACTIVO = 'S' order by RAZ_SOCIAL";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;

        }


        public DataTable ListarClientesIdEmpresa()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select ID_EMPRESA as id, RAZ_SOCIAL as nombre from VEN_EMPRESAS where ACTIVO = 'S' order by RAZ_SOCIAL";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;

        }
        public DataTable ListarPsicologos()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select USU_LOG as id, USU_LOG as nombre from CRT_USUARIOS where (ESTADO = 'ACTIVO') AND (ID_ROL = 2) order by USU_LOG";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }
        public DataTable ListarPsicologos_RECLUTAMIENTO()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "Reclutamiento_PSICOLOGOS";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarEmpleadosActivosEmpleador(Decimal idEmpleador)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "SELECT RSI.ID_SOLICITUD AS id, (RSI.NOMBRES + ' ' + RSI.APELLIDOS) AS nombre FROM NOM_EMPLEADOS NE INNER JOIN REG_SOLICITUDES_INGRESO RSI ON RSI.ID_SOLICITUD = NE.ID_SOLICITUD WHERE (ID_EMPRESA = " + idEmpleador.ToString() + ") AND (ACTIVO = 'S') ORDER BY RSI.NOMBRES, RSI.APELLIDOS";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }


        public DataTable ListarPerCont(int numFilas)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "SELECT TOP " + numFilas.ToString() + " PER.PER_CONT AS id, PER.PER_CONT AS nombre FROM NOM_PERIODO PER WHERE NOT(PER.PER_CONT IN ('809','149'))  GROUP BY PER.PER_CONT ORDER BY PER_CONT DESC";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }


        public DataTable ListarAniosPresupuestados(Programa.Areas area)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select ANNO AS nombre, ANNO AS id FROM PROG_PRESUPUESTOS PRES WHERE PRES.ID_AREA = '" + area.ToString() + "' GROUP BY ANNO ORDER BY ANNO DESC";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }


        public DataTable ListarProveedores()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select PROV.ID_PROVEEDOR as id, TER.RAZON_SOCIAL as nombre from ALM_REG_PROVEEDOR PROV INNER JOIN TERCEROS TER ON TER.ID_TERCERO = PROV.ID_TERCERO WHERE (PROV.ESTADO = 'S') OR (PROV.ESTADO = 'A') ORDER BY TER.RAZON_SOCIAL";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarTipoEntrada()
        {
            DataTable tablaEntradas = new DataTable();

            tablaEntradas.Columns.Add("id");
            tablaEntradas.Columns.Add("nombre");

            DataRow filaTabla;

            filaTabla = tablaEntradas.NewRow();
            filaTabla["id"] = "AJUSTE";
            filaTabla["nombre"] = "AJUSTE";
            tablaEntradas.Rows.Add(filaTabla);

            filaTabla = tablaEntradas.NewRow();
            filaTabla["id"] = "FACTURA";
            filaTabla["nombre"] = "COMPRA";
            tablaEntradas.Rows.Add(filaTabla);

            filaTabla = tablaEntradas.NewRow();
            filaTabla["id"] = "TRASLADO";
            filaTabla["nombre"] = "TRASLADO";
            tablaEntradas.Rows.Add(filaTabla);

            return tablaEntradas;
        }

        public DataTable ListarTipoEvaluacion()
        {
            DataTable tablaEvaluacion = new DataTable();

            tablaEvaluacion.Columns.Add("id");
            tablaEvaluacion.Columns.Add("nombre");

            DataRow filaTabla;

            filaTabla = tablaEvaluacion.NewRow();
            filaTabla["id"] = EvaluacionPlanta.TiposEvaluacion.ACTITUDINAL.ToString();
            filaTabla["nombre"] = EvaluacionPlanta.TiposEvaluacion.ACTITUDINAL.ToString();
            tablaEvaluacion.Rows.Add(filaTabla);

            filaTabla = tablaEvaluacion.NewRow();
            filaTabla["id"] = EvaluacionPlanta.TiposEvaluacion.DESEMPENO.ToString();
            filaTabla["nombre"] = EvaluacionPlanta.TiposEvaluacion.DESEMPENO.ToString();
            tablaEvaluacion.Rows.Add(filaTabla);

            filaTabla = tablaEvaluacion.NewRow();
            filaTabla["id"] = EvaluacionPlanta.TiposEvaluacion.PRUEBA.ToString();
            filaTabla["nombre"] = EvaluacionPlanta.TiposEvaluacion.PRUEBA.ToString();
            tablaEvaluacion.Rows.Add(filaTabla);

            return tablaEvaluacion;
        }


        public DataTable ListarCiudades()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select ID_CIUDAD as id, NOMBRE from CIUDAD order by NOMBRE";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;

        }

        public DataTable ListarCiudadesPorRegional(String ID_REGIONAL)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select ID_CIUDAD as id, NOMBRE from CIUDAD WHERE ID_REGIONAL = '" + ID_REGIONAL + "' order by NOMBRE";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarRegionales()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select ID_REGIONAL as id, NOMBRE from REGIONAL order by NOMBRE";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarPeriodos(String Empresa)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "Desprendibles_De_Pago_web_X_Empresa_PERIODOS";
            sql += " " + " '" + Empresa + " '";
            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarCargos(String Empresa)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "Contratacion_Cargos";
            sql += " " + Empresa;

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;

        }
        public DataTable ListarRegional()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "RECLUTAMIENTO_OPTIENE_REGIONALES";


            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }
        public DataTable ListarCiudad(String @REGIONAL)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "RECLUTAMIENTO_OPTIENE_CIUDADES";
            sql += " '" + @REGIONAL + "'";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;

        }

        public DataTable ListarReclutadores()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "Contratacion_Usuarios";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;

        }
        public DataTable ListarCentrosDeCosto(String Empresa)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "USP_SOPORTE_Centros_De_Costo_Por_Empresa";
            sql += " " + Empresa;
            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarPeriodosEmpresaYCC(Decimal idEmpresa, String nomCC)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "usp_obtener_periodos_empresa_o_centro_c ";
            sql += idEmpresa.ToString() + ", '" + nomCC + "'";
            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }





        public DataTable ListarEspecialistasActividades(Programa.Areas area)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select USU.USU_LOG as id, CASE WHEN USU.USU_TIPO = 'PLANTA' THEN USU.NOMBRES + ' ' + USU.APELLIDOS ELSE USU.NOMBRES_EXTERNO + ' ' + USU.APELLIDOS_EXTERNO END as NOMBRE from PROG_DETALLE_ACTIVIDADES DET_ACT INNER JOIN PROG_MAESTRA_PROGRAMAS PROG ON PROG.ID_PROGRAMA = DET_ACT.ID_PROGRAMA INNER JOIN View_INFORMACION_USUARIOS_SISTEMA USU ON USU.USU_LOG = DET_ACT.ENCARGADO WHERE PROG.ID_AREA = '" + area.ToString() + "' GROUP BY USU.USU_LOG, USU.USU_TIPO, USU.NOMBRES, USU.APELLIDOS, USU.NOMBRES_EXTERNO, USU.APELLIDOS_EXTERNO order by NOMBRE";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarSubProgramas(Programa.Areas area)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select ID_SUB_PROGRAMA as id, NOMBRE from PROG_SUB_PROGRAMAS WHERE ID_AREA = '" + area.ToString() + "' AND ACTIVO = 'True' ORDER BY NOMBRE";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarTiposActividad(Programa.Areas area)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select ID_TIPO_ACTIVIDAD as id, NOMBRE from PROG_TIPOS_ACTIVIDAD WHERE ID_AREA = '" + area.ToString() + "' AND ACTIVA = 'True' ORDER BY NOMBRE";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarTiposCancelacion(Programa.Areas area)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select ID_MOTIVO as id, MOTIVO as NOMBRE from PROG_MOTIVOS_CANCELACION_REPROGRAMACION WHERE ID_AREA = '" + area.ToString() + "' AND TIPO = 'CANCELACION' AND ACTIVO = 'True' ORDER BY NOMBRE";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }

        public DataTable ListarEstadosAccidentes()
        {
            DataTable tablaEstados = new DataTable();
            tablaEstados.Columns.Add("id");
            tablaEstados.Columns.Add("NOMBRE");

            DataRow filaEstado = tablaEstados.NewRow();
            filaEstado["id"] = SeguimientoAccidentes.EstadosAccidente.ABIERTO.ToString();
            filaEstado["NOMBRE"] = SeguimientoAccidentes.EstadosAccidente.ABIERTO.ToString();
            tablaEstados.Rows.Add(filaEstado);

            filaEstado = tablaEstados.NewRow();
            filaEstado["id"] = SeguimientoAccidentes.EstadosAccidente.CERRADO.ToString();
            filaEstado["NOMBRE"] = SeguimientoAccidentes.EstadosAccidente.CERRADO.ToString();
            tablaEstados.Rows.Add(filaEstado);

            filaEstado = tablaEstados.NewRow();
            filaEstado["id"] = SeguimientoAccidentes.EstadosAccidente.SEGUIMIENTO.ToString();
            filaEstado["NOMBRE"] = SeguimientoAccidentes.EstadosAccidente.SEGUIMIENTO.ToString();
            tablaEstados.Rows.Add(filaEstado);

            return tablaEstados;
        }

        public DataTable ListarPeriodos()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = "usp_traer_periodos ";
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return _dataTable;
        }

        public DataTable ListarParametros(string tabla)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataSet _dataSet = new DataSet();
            DataTable _dataTable = new DataTable();
            String sql = "select CODIGO as id, DESCRIPCION as NOMBRE from PARAMETROS where TABLA = '" + tabla + "'";
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataTable = _dataSet.Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return _dataTable;
        }

        public DataTable ListarComerciales()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataSet _dataSet = new DataSet();
            DataTable _dataTable = new DataTable();
            String sql = "select ne.ID_EMPLEADO as id,  rtrim(ltrim(ne.NOMBRES)) + ' ' + rtrim(ltrim(ne.APELLIDOS)) as NOMBRE from nom_empleados ne  inner join CRT_USUARIOS cu on ne.ID_EMPLEADO = cu.ID_EMPLEADO inner join crt_empresas_por_usuario cepu on cepu.ID_USUARIO = cu.Id_Usuario left join crt_unidad_negocio cun on cun.ID_EMPRESA_USUARIO = cepu.ID_EMPRESA_USUARIO and cun.UNIDAD_NEGOCIO = 'REP. COMERCIAL' and ne.ACTIVO = 'S' group by ne.ID_EMPLEADO, rtrim(ltrim(ne.NOMBRES)) + ' ' + rtrim(ltrim(ne.APELLIDOS))  order by rtrim(ltrim(ne.NOMBRES)) + ' ' + rtrim(ltrim(ne.APELLIDOS)) ";
            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataTable = _dataSet.Tables[0];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return _dataTable;
        }

        public DataTable ListarEmpresas(String filtro)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_empresas_busqueda_multiple_filtro '" + filtro + "'";

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
                    throw new Exception(e.Message);
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            return _dataTable;
        }
        public DataTable ListarCobertura(String @Id_Empresa)
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "LISTAR_COBERTURA" + " '" + @Id_Empresa.ToString() + "' ";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }
        public DataTable ListarBasesDatos()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select name AS nombre ,name AS id from master.dbo.sysdatabases where cmptlevel <> 100";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }
        public DataTable ListarBasesDatosID()
        {
            Conexion conexion = new Conexion(IdEmpresa);
            DataTable dataTable = new DataTable();
            string sql = null;

            sql = "select NOMBRE AS nombre, ID as id  from PAR_IMAGENES ";

            try
            {
                DataSet _dataSet = conexion.ExecuteReader(sql);
                dataTable = _dataSet.Tables[0].DefaultView.Table;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return dataTable;
        }
        #endregion metodos
    }
}
