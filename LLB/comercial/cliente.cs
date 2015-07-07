using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.operaciones;

namespace Brainsbits.LLB.comercial
{
    public class cliente
    {
        #region varialbes

        String _empresa = null;
        String _mensaje_error = null;
        Decimal _ID_CLIENTE = 0;
        String _COD_EMPRESA = null;
        String _usuario = null;

        private Dictionary<String, String> diccionarioCamposVenEmpresas;
        #endregion varialbes

        #region propiedades
        public String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public Decimal ID_CLIENTE
        {
            get { return _ID_CLIENTE; }
            set { _ID_CLIENTE = value; }
        }

        public String COD_EMPRESA
        {
            get { return _COD_EMPRESA; }
            set { _COD_EMPRESA = value; }
        }

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        #endregion propiedades

        #region constructores
        public cliente(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;

            diccionarioCamposVenEmpresas = new Dictionary<string, string>();

            diccionarioCamposVenEmpresas.Add("RAZ_SOCIAL", "Razón Social");
            diccionarioCamposVenEmpresas.Add("ACT_ECO", "Actividad Económica");
            diccionarioCamposVenEmpresas.Add("NIT_EMPRESA", "NIT");
            diccionarioCamposVenEmpresas.Add("DIG_VER", "Digito de Verificación del NIT");
            diccionarioCamposVenEmpresas.Add("NOM_REP_LEGAL", "Representante Legal");
            diccionarioCamposVenEmpresas.Add("TIP_DOC_REP_LEGAL", "Tipo de Documento del Representante Legal");
            diccionarioCamposVenEmpresas.Add("CC_REP_LEGAL", "Número de identificación del Representante Legal");
            diccionarioCamposVenEmpresas.Add("ID_CIUDAD_CC_REP_LEGAL", "Ciudad de expedición del documento del Representante Legal");
            diccionarioCamposVenEmpresas.Add("DIR_EMP", "Dirección");
            diccionarioCamposVenEmpresas.Add("CIU_EMP", "Ciudad");
            diccionarioCamposVenEmpresas.Add("TEL_EMP", "Telefono");
            diccionarioCamposVenEmpresas.Add("TEL_EMP1", "Telefono");
            diccionarioCamposVenEmpresas.Add("NUM_CELULAR", "Celular");
        }
        #endregion constructores

        #region metodos

        private Decimal AdicinarRegistroVEN_EMPRESAS(String ACTIVO
            , DateTime FCH_INGRESO
            , String NIT_EMPRESA
            , String ACT_ECO,
            String RAZ_SOCIAL
            , String DIR_EMP
            , String CIU_EMP
            , String TEL_EMP
            , String CUB_CIUDADES
            , String NOM_REP_LEGAL
            , String CC_REP_LEGAL
            , String TIPO_EMPRESA
            , String CIU_ORG_NEG
            , Int32 NUM_EMPLEADOS
            , String USU_CRE
            , String FAC_NAL
            , Decimal ID_ALIANZA
            , Int32 DIG_VER
            , String EMP_ESTADO
            , String EMP_EXC_IVA
            , String TEL_EMP1
            , String NUM_CELULAR
            , String ID_ACTIVIDAD
            , Decimal ID_GRUPO_EMPRESARIAL
            , String ID_CIUDAD_CC_REP_LEGAL
            , String ID_SERVICIO
            , String TIP_DOC_REP_LEGAL
            , Conexion conexion)
        {
            Decimal ID_EMPRESA = 0;

            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_adicionar ";

            if (!(String.IsNullOrEmpty(ACTIVO)))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO = '" + ACTIVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FCH_INGRESO.ToString())))
            {
                sql += "'" + FCH_INGRESO.ToShortDateString() + "', ";
                informacion += "FCH_INGRESO = '" + FCH_INGRESO.ToShortDateString() + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA DE INGRESO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NIT_EMPRESA)))
            {
                sql += "'" + NIT_EMPRESA + "', ";
                informacion += "NIT_EMPRESA = '" + NIT_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NIT DE EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ACT_ECO)))
            {
                sql += "'" + ACT_ECO + "', ";
                informacion += "ACT_ECO = '" + ACT_ECO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ACT_ECO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += "'" + RAZ_SOCIAL + "', ";
                informacion += "RAZ_SOCIAL = '" + RAZ_SOCIAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RAZON SOCIAL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIR_EMP)))
            {
                sql += "'" + DIR_EMP + "', ";
                informacion += "DIR_EMP = '" + DIR_EMP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DIRECCION DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIU_EMP)))
            {
                sql += "'" + CIU_EMP + "', ";
                informacion += "CIU_EMP = '" + CIU_EMP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_EMP)))
            {
                sql += "'" + TEL_EMP + "', ";
                informacion += "TEL_EMP = '" + TEL_EMP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CUB_CIUDADES)))
            {
                sql += "'" + CUB_CIUDADES + "', ";
                informacion += "CUB_CIUDADES = '" + CUB_CIUDADES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CUBRIMIENTO CIUDADES no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_REP_LEGAL)))
            {
                sql += "'" + NOM_REP_LEGAL + "', ";
                informacion += "NOM_REP_LEGAL = '" + NOM_REP_LEGAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DEL REPRESENTANTE LEGAL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CC_REP_LEGAL)))
            {
                sql += "'" + CC_REP_LEGAL + "', ";
                informacion += "CC_REP_LEGAL = '" + CC_REP_LEGAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD DEL REPRESENTANTE LEGAL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_EMPRESA)))
            {
                sql += "'" + TIPO_EMPRESA + "', ";
                informacion += "TIPO_EMPRESA = '" + TIPO_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO DE EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIU_ORG_NEG)))
            {
                sql += "'" + CIU_ORG_NEG + "', ";
                informacion += "CIU_ORG_NEG = '" + CIU_ORG_NEG.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD ORIGEN DEL NEGOCIO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (NUM_EMPLEADOS != 0)
            {
                sql += NUM_EMPLEADOS + ", ";
                informacion += "NUM_EMPLEADOS = " + NUM_EMPLEADOS.ToString() + ", ";
            }
            else
            {
                MensajeError += "El campo NUMERO DE EMPLEADOS no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_CRE)))
            {
                sql += "'" + USU_CRE + "', ";
                informacion += "USU_CRE = '" + USU_CRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FAC_NAL)))
            {
                sql += "'" + FAC_NAL + "', ";
                informacion += "FAC_NAL = '" + FAC_NAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FACTURA NACIONAL no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += ID_ALIANZA + ", ";
            informacion += "ID_ALIANZA = " + ID_ALIANZA.ToString() + ", ";

            if (DIG_VER != 0)
            {
                sql += DIG_VER + ", ";
                informacion += "DIG_VER = '" + DIG_VER.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "DIG_VER = '0', ";
            }

            if (!(String.IsNullOrEmpty(EMP_ESTADO)))
            {
                sql += "'" + EMP_ESTADO + "', ";
                informacion += "EMP_ESTADO = '" + EMP_ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EMP_EXC_IVA)))
            {
                sql += "'" + EMP_EXC_IVA + "', ";
                informacion += "EMP_EXC_IVA = '" + EMP_EXC_IVA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo EXCENTO DE IVA no puede vacio. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_EMP1)))
            {
                sql += "'" + TEL_EMP1 + "', ";
                informacion += "TEL_EMP1 = '" + TEL_EMP1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO 1 DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUM_CELULAR)))
            {
                sql += "'" + NUM_CELULAR + "', ";
                informacion += "NUM_CELULAR = '" + NUM_CELULAR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUMERO DE CELULAR no puede ser vacio. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_ACTIVIDAD)))
            {
                sql += "'" + ID_ACTIVIDAD + "', ";
                informacion += "ID_ACTIVIDAD = '" + ID_ACTIVIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVIDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += ID_GRUPO_EMPRESARIAL + ", ";
            informacion += "ID_GRUPO_EMPRESARIAL = " + ID_GRUPO_EMPRESARIAL.ToString() + ", ";


            if (String.IsNullOrEmpty(ID_CIUDAD_CC_REP_LEGAL) == false)
            {
                sql += "'" + ID_CIUDAD_CC_REP_LEGAL + "', ";
                informacion += "ID_CIUDAD_CC_REP_LEGAL = '" + ID_CIUDAD_CC_REP_LEGAL.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD_CC_REP_LEGAL = 'null', ";
            }

            if (!(String.IsNullOrEmpty(ID_SERVICIO)))
            {
                sql += "'" + ID_SERVICIO + "', ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SERVICIO no puede ser vacio. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIP_DOC_REP_LEGAL) == false)
            {
                sql += "'" + TIP_DOC_REP_LEGAL + "'";
                informacion += "TIP_DOC_REP_LEGAL = '" + TIP_DOC_REP_LEGAL + "' ";
            }
            else
            {
                MensajeError += "El campo TIP_DOC_REP_LEGAL no puede ser vacio. \n";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                ID_EMPRESA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                if (ID_EMPRESA <= 0)
                {
                    MensajeError = "Ocurrio un error al intentar insertar el nuevo cliente, error INSERT.";
                    ID_EMPRESA = 0;
                }
                else
                {
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(USU_CRE, tabla.VEN_EMPRESAS, tabla.ACCION_ADICIONAR.ToString(), sql, informacion, conexion) == false)
                    {
                        MensajeError = "Ocurrio un error al intentar insertar la auditoria para el nuevo cliente; error INSERT.";
                        ID_EMPRESA = 0;
                    }
                    #endregion auditoria
                }
            }
            else
            {
                ID_EMPRESA = 0;
            }

            return ID_EMPRESA;
        }
        
        public Decimal Adicionar(String ACTIVO, System.DateTime FCH_INGRESO, String NIT_EMPRESA, String ACT_ECO,
            String RAZ_SOCIAL, String DIR_EMP, String CIU_EMP, String TEL_EMP, String CUB_CIUDADES, String NOM_REP_LEGAL, String CC_REP_LEGAL,
            String TIPO_EMPRESA, String CIU_ORG_NEG, Int32 NUM_EMPLEADOS, String USU_CRE, String FAC_NAL,
            Decimal ID_ALIANZA, Int32 DIG_VER, String EMP_ESTADO, String EMP_EXC_IVA,
            String TEL_EMP1, String NUM_CELULAR, String ID_ACTIVIDAD, Decimal ID_GRUPO_EMPRESARIAL,
            List<cobertura> coberturas, String ID_CIUDAD_CC_REP_LEGAL, String ID_SERVICIO, String TIP_DOC_REP_LEGAL, List<empresasRiesgos> listaRiesgos)
        {
            Decimal ID_EMPRESA = 0;

            Boolean verificador = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_EMPRESA = AdicinarRegistroVEN_EMPRESAS(ACTIVO, FCH_INGRESO, NIT_EMPRESA, ACT_ECO, RAZ_SOCIAL, DIR_EMP, CIU_EMP, TEL_EMP, CUB_CIUDADES, NOM_REP_LEGAL, CC_REP_LEGAL, TIPO_EMPRESA, CIU_ORG_NEG, NUM_EMPLEADOS, USU_CRE, FAC_NAL, ID_ALIANZA, DIG_VER, EMP_ESTADO, EMP_EXC_IVA, TEL_EMP1, NUM_CELULAR, ID_ACTIVIDAD, ID_GRUPO_EMPRESARIAL, ID_CIUDAD_CC_REP_LEGAL, ID_SERVICIO, TIP_DOC_REP_LEGAL, conexion);

                if (ID_EMPRESA == 0)
                {
                    conexion.DeshacerTransaccion();
                    verificador = false;
                }
                else
                {
                    cobertura _cobertura = new cobertura(Empresa);
                    foreach (cobertura c in coberturas)
                    {
                        if (_cobertura.Adicionar(ID_EMPRESA, c.IDCIUDAD, USU_CRE, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            ID_EMPRESA = 0;
                            verificador = false;
                            break;
                        }
                    }

                    if (verificador == true)
                    {
                        empresasRiesgos _empresasRiesgos = new empresasRiesgos(Empresa, Usuario);
                        foreach (empresasRiesgos r in listaRiesgos)
                        {
                            if (_empresasRiesgos.Adicionar(ID_EMPRESA, r.DESCRIPCION_RIESGO, conexion) <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                ID_EMPRESA = 0;
                                verificador = false;
                                break;
                            }
                        }

                        if (verificador == true)
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                }
            }
            catch
            {
                conexion.DeshacerTransaccion();
                ID_EMPRESA = 0;
                verificador = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_EMPRESA;
        }

        public Boolean Actualizar(Decimal ID_EMPRESA, String ACTIVO, System.DateTime FCH_INGRESO, String NIT_EMPRESA, String ACT_ECO,
            String RAZ_SOCIAL, String DIR_EMP, String CIU_EMP, String TEL_EMP, String CUB_CIUDADES, String NOM_REP_LEGAL, String CC_REP_LEGAL,
            String TIPO_EMPRESA, String CIU_ORG_NEG, Int32 NUM_EMPLEADOS, String USU_MOD, String FAC_NAL,
            Decimal ID_ALIANZA, Int32 DIG_VER, String EMP_ESTADO, String EMP_EXC_IVA,
            String TEL_EMP1, String NUM_CELULAR, String ID_ACTIVIDAD, Decimal ID_GRUPO_EMPRESARIAL,
            List<cobertura> coberturas, String ID_CIUDAD_CC_REP_LEGAL, String ID_SERVICIO, String TIP_DOC_REG_LEGAL, List<empresasRiesgos> listaRiesgos, String RAZ_SOCIAL_ANTERIOR)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            Boolean continuarNormalmente = true;
            ManualServicio _manual = new ManualServicio(Empresa, Usuario);

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean verificador = true;
            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion); 

                DataTable tablaDatosAnteriores = _manual.ObtenerInfoRegistroTabla(diccionarioCamposVenEmpresas, "VEN_EMPRESAS", "ID_EMPRESA", ID_EMPRESA.ToString(), conexion);

                if (ActualizarRegistroVEN_EMPRESAS(ID_EMPRESA, ACTIVO, FCH_INGRESO, NIT_EMPRESA, ACT_ECO, RAZ_SOCIAL, DIR_EMP, CIU_EMP, TEL_EMP, CUB_CIUDADES, NOM_REP_LEGAL, CC_REP_LEGAL, TIPO_EMPRESA, CIU_ORG_NEG, NUM_EMPLEADOS, USU_MOD, FAC_NAL, ID_ALIANZA, DIG_VER, EMP_ESTADO, EMP_EXC_IVA, TEL_EMP1, NUM_CELULAR, ID_ACTIVIDAD, ID_GRUPO_EMPRESARIAL, ID_CIUDAD_CC_REP_LEGAL, ID_SERVICIO, TIP_DOC_REG_LEGAL, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    verificador = false;
                }
                else
                {
                    DataTable tablaDatosNuevos = _manual.ObtenerInfoRegistroTabla(diccionarioCamposVenEmpresas, "VEN_EMPRESAS", "ID_EMPRESA", ID_EMPRESA.ToString(), conexion);

                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.DeterminarYRegistrarModificacionesEnCampos(diccionarioCamposVenEmpresas, tablaDatosAnteriores, tablaDatosNuevos, ID_VERSIONAMIENTO, ID_EMPRESA, ManualServicio.ListaSecciones.Comercial, ManualServicio.AccionesManual.Actualizar, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            conexion.DeshacerTransaccion();
                            verificador = false;
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                        }
                    }

                    if (continuarNormalmente == true)
                    {
                        cobertura _cobertura = new cobertura(Empresa);
                        Boolean eliminarCobertura = true;
                        Boolean insertarCobertura = true;

                        DataTable tablaCoberturaActual = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA, conexion);

                        for (int i = 0; i < tablaCoberturaActual.Rows.Count; i++)
                        {
                            DataRow filaCoberturaActual = tablaCoberturaActual.Rows[i];
                            String ID_CIUDAD_COBERTURA_ACTUAL = filaCoberturaActual["Código Ciudad"].ToString().Trim();

                            eliminarCobertura = true;

                            foreach (cobertura c in coberturas)
                            {
                                if (c.IDCIUDAD == ID_CIUDAD_COBERTURA_ACTUAL)
                                {
                                    eliminarCobertura = false;
                                    break;
                                }
                            }

                            if (eliminarCobertura == true)
                            {
                                if (realizarVersionamientoManual == true)
                                {
                                    ID_VERSIONAMIENTO = _manual.RegistrarDesactivacionRegistroTabla(ID_EMPRESA, ManualServicio.ListaSecciones.Comercial, ManualServicio.AccionesManual.Eliminar, "COBERTURA", "Cobertura", ID_CIUDAD_COBERTURA_ACTUAL, ID_VERSIONAMIENTO, conexion);
                                    if (ID_VERSIONAMIENTO == -1)
                                    {
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        continuarNormalmente = false;
                                        ID_VERSIONAMIENTO = 0;
                                        break;
                                    }
                                }

                                if (continuarNormalmente == true)
                                {
                                    if (_cobertura.EliminarUnaCiudadDeLaCoberturaDeCliente(ID_EMPRESA, ID_CIUDAD_COBERTURA_ACTUAL, Usuario, conexion) == false)
                                    {
                                        MensajeError = _cobertura.MensajeError;
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        break;
                                    }
                                }
                            }
                        }

                        foreach (cobertura c in coberturas)
                        {
                            insertarCobertura = true;

                            for (int i = 0; i < tablaCoberturaActual.Rows.Count; i++)
                            {
                                DataRow filaCoberturaActual = tablaCoberturaActual.Rows[i];
                                String ID_CIUDAD_COBERTURA_ACTUAL = filaCoberturaActual["Código Ciudad"].ToString().Trim();

                                if (c.IDCIUDAD == ID_CIUDAD_COBERTURA_ACTUAL)
                                {
                                    insertarCobertura = false;
                                    break;
                                }
                            }

                            if (insertarCobertura == true)
                            {
                                if (realizarVersionamientoManual == true)
                                {
                                    ID_VERSIONAMIENTO = _manual.RegistrarInsersionRegistroTabla(ID_VERSIONAMIENTO, ManualServicio.ListaSecciones.Comercial, "COBERTURA", "Cobertura", c.IDCIUDAD, ManualServicio.AccionesManual.Insertar, ID_EMPRESA, conexion);
                                    if (ID_VERSIONAMIENTO == -1)
                                    {
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        continuarNormalmente = false;
                                        ID_VERSIONAMIENTO = 0;
                                        break;
                                    }
                                }

                                if (continuarNormalmente == true)
                                {
                                    if (_cobertura.Adicionar(ID_EMPRESA, c.IDCIUDAD, Usuario, conexion) == false)
                                    {
                                        verificador = false;
                                        conexion.DeshacerTransaccion();
                                        MensajeError = _cobertura.MensajeError;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (_cobertura.ActualizarPermisosParaCiudaYEmpresaEnGeneral(ID_EMPRESA, c.IDCIUDAD, Usuario, conexion) == false)
                                {
                                    verificador = false;
                                    conexion.DeshacerTransaccion();
                                    MensajeError = _cobertura.MensajeError;
                                    break;
                                }
                            }
                        }

                        if (verificador == true)
                        {
                            empresasRiesgos _empresasRiesgos = new empresasRiesgos(Empresa, Usuario);
                            DataTable tablaRiesgosActuales = _empresasRiesgos.ObtenerRoesgosPorEmpresa(ID_EMPRESA, conexion);

                            Boolean riesgoEncontrado = false;

                            foreach (DataRow filaRiegosActuales in tablaRiesgosActuales.Rows)
                            {
                                riesgoEncontrado = false;
                                foreach (empresasRiesgos filaRiesgosNuevos in listaRiesgos)
                                {
                                    if (filaRiesgosNuevos.DESCRIPCION_RIESGO == filaRiegosActuales["DESCRIPCION_RIESGO"].ToString())
                                    {
                                        riesgoEncontrado = true;
                                        break;
                                    }
                                }

                                if (riesgoEncontrado == false)
                                {
                                    if (_empresasRiesgos.desactivarRiesgoParaEmpresa(ID_EMPRESA, filaRiegosActuales["DESCRIPCION_RIESGO"].ToString(), conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        break;
                                    }
                                }
                            }

                            if (verificador == true)
                            {
                                foreach (empresasRiesgos filaRiesgosNuevos in listaRiesgos)
                                {
                                    if (_empresasRiesgos.Adicionar(ID_EMPRESA, filaRiesgosNuevos.DESCRIPCION_RIESGO, conexion) == 0)
                                    {
                                        verificador = false;
                                        MensajeError = _empresasRiesgos.MensajeError;
                                        conexion.DeshacerTransaccion();
                                        break;
                                    }
                                }

                                if (verificador == true)
                                {
                                    if (RAZ_SOCIAL != RAZ_SOCIAL_ANTERIOR)
                                    {
                                        if (AdicionarCambiorazonSocial(ID_EMPRESA, RAZ_SOCIAL_ANTERIOR, RAZ_SOCIAL, Usuario, conexion) <= 0)
                                        {
                                            conexion.DeshacerTransaccion();
                                            verificador = false;
                                        }
                                    }
                                    else
                                    {
                                        verificador = true;
                                    }

                                    if (verificador == true)
                                    {
                                        conexion.AceptarTransaccion();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                verificador = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return verificador;
        }



        private Boolean ActualizarRegistroVEN_EMPRESAS(Decimal ID_EMPRESA
            , String ACTIVO
            , DateTime FCH_INGRESO
            , String NIT_EMPRESA
            , String ACT_ECO
            , String RAZ_SOCIAL
            , String DIR_EMP
            , String CIU_EMP
            , String TEL_EMP
            , String CUB_CIUDADES
            , String NOM_REP_LEGAL
            , String CC_REP_LEGAL
            , String TIPO_EMPRESA
            , String CIU_ORG_NEG
            , Int32 NUM_EMPLEADOS
            , String USU_MOD
            , String FAC_NAL
            , Decimal ID_ALIANZA
            , Int32 DIG_VER
            , String EMP_ESTADO
            , String EMP_EXC_IVA
            , String TEL_EMP1
            , String NUM_CELULAR
            , String ID_ACTIVIDAD
            , Decimal ID_GRUPO_EMPRESARIAL
            , String ID_CIUDAD_CC_REP_LEGAL
            , String ID_SERVICIO
            , String TIP_DOC_REP_LEGAL
            , Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            tools _tools = new tools();

            Boolean verificador = true;

            sql = "usp_ven_empresas_actualizar ";

            #region validaciones
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

            if (!(String.IsNullOrEmpty(ACTIVO)))
            {
                sql += "'" + ACTIVO + "', ";
                informacion += "ACTIVO = '" + ACTIVO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FCH_INGRESO.ToString())))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INGRESO) + "', ";
                informacion += "FCH_INGRESO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INGRESO) + "', ";
            }
            else
            {
                MensajeError += "El campo FECHA DE INGRESO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NIT_EMPRESA)))
            {
                sql += "'" + NIT_EMPRESA + "', ";
                informacion += "NIT_EMPRESA = '" + NIT_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NIT DE EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ACT_ECO)))
            {
                sql += "'" + ACT_ECO + "', ";
                informacion += "ACT_ECO = '" + ACT_ECO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ACT_ECO = 'null', ";
            }

            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += "'" + RAZ_SOCIAL + "', ";
                informacion += "RAZ_SOCIAL = '" + RAZ_SOCIAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RAZON SOCIAL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIR_EMP)))
            {
                sql += "'" + DIR_EMP + "', ";
                informacion += "DIR_EMP = '" + DIR_EMP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DIRECCION DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIU_EMP)))
            {
                sql += "'" + CIU_EMP + "', ";
                informacion += "CIU_EMP = '" + CIU_EMP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_EMP)))
            {
                sql += "'" + TEL_EMP + "', ";
                informacion += "TEL_EMP = '" + TEL_EMP.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CUB_CIUDADES)))
            {
                sql += "'" + CUB_CIUDADES + "', ";
                informacion += "CUB_CIUDADES = '" + CUB_CIUDADES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CUBRIMIENTO CIUDADES no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_REP_LEGAL)))
            {
                sql += "'" + NOM_REP_LEGAL + "', ";
                informacion += "NOM_REP_LEGAL = '" + NOM_REP_LEGAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DEL REPRESENTANTE LEGAL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CC_REP_LEGAL)))
            {
                sql += "'" + CC_REP_LEGAL + "', ";
                informacion += "CC_REP_LEGAL = '" + CC_REP_LEGAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD DEL REPRESENTANTE LEGAL no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_EMPRESA)))
            {
                sql += "'" + TIPO_EMPRESA + "', ";
                informacion += "TIPO_EMPRESA = '" + TIPO_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO DE EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CIU_ORG_NEG)))
            {
                sql += "'" + CIU_ORG_NEG + "', ";
                informacion += "CIU_ORG_NEG = '" + CIU_ORG_NEG.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CIUDAD ORIGEN DEL NEGOCIO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (NUM_EMPLEADOS != 0)
            {
                sql += NUM_EMPLEADOS + ", ";
                informacion += "NUM_EMPLEADOS = " + NUM_EMPLEADOS.ToString() + ", ";
            }
            else
            {
                MensajeError += "El campo NUMERO DE EMPLEADOS no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_MOD)))
            {
                sql += "'" + USU_MOD + "', ";
                informacion += "USU_MOD = '" + USU_MOD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FAC_NAL)))
            {
                sql += "'" + FAC_NAL + "', ";
                informacion += "FAC_NAL = '" + FAC_NAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FACTURA NACIONAL no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += ID_ALIANZA + ", ";
            informacion += "ID_ALIANZA = " + ID_ALIANZA.ToString() + ", ";

            if (DIG_VER != 0)
            {
                sql += DIG_VER + ", ";
                informacion += "DIG_VER = '" + DIG_VER.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "DIG_VER = '0', ";
            }

            if (!(String.IsNullOrEmpty(EMP_ESTADO)))
            {
                sql += "'" + EMP_ESTADO + "', ";
                informacion += "EMP_ESTADO = '" + EMP_ESTADO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ESTADO DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EMP_EXC_IVA)))
            {
                sql += "'" + EMP_EXC_IVA + "', ";
                informacion += "EMP_EXC_IVA = '" + EMP_EXC_IVA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo EXCENTO DE IVA no puede vacio. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_EMP1)))
            {
                sql += "'" + TEL_EMP1 + "', ";
                informacion += "TEL_EMP1 = '" + TEL_EMP1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO 1 DE LA EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NUM_CELULAR)))
            {
                sql += "'" + NUM_CELULAR + "', ";
                informacion += "NUM_CELULAR = '" + NUM_CELULAR.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NUMERO DE CELULAR no puede ser vacio. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_ACTIVIDAD)))
            {
                sql += "'" + ID_ACTIVIDAD + "', ";
                informacion += "ID_ACTIVIDAD = '" + ID_ACTIVIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ACTIVIDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += ID_GRUPO_EMPRESARIAL + ", ";
            informacion += "ID_GRUPO_EMPRESARIAL = " + ID_GRUPO_EMPRESARIAL.ToString() + ", ";


            if (String.IsNullOrEmpty(ID_CIUDAD_CC_REP_LEGAL) == false)
            {
                sql += "'" + ID_CIUDAD_CC_REP_LEGAL + "', ";
                informacion += "ID_CIUDAD_CC_REP_LEGAL = '" + ID_CIUDAD_CC_REP_LEGAL.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD_CC_REP_LEGAL = 'null', ";
            }

            if (!(String.IsNullOrEmpty(ID_SERVICIO)))
            {
                sql += "'" + ID_SERVICIO + "', ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SERVICIO no puede ser vacio. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIP_DOC_REP_LEGAL) == false)
            {
                sql += "'" + TIP_DOC_REP_LEGAL + "'";
                informacion += "TIP_DOC_REP_LEGAL = '" + TIP_DOC_REP_LEGAL + "'";
            }
            else
            {
                MensajeError += "El campo TIP_DOC_REP_LEGAL no puede ser vacio. \n";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar == true)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        verificador = false;
                        MensajeError = "No se pudo realizar la actualización del cliente, error en UPDATE.";
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(USU_MOD, tabla.VEN_EMPRESAS, tabla.ACCION_ACTUALIZAR.ToString(), sql, informacion, conexion) == false)
                        {
                            verificador = false;
                            MensajeError = _auditoria.MensajError;
                        }
                        #endregion auditoria
                    }
                }
                catch (Exception ex)
                {
                    verificador = false;
                    MensajeError = ex.Message;
                }
            }
            else
            {
                verificador = false;
            }

            return verificador;
        }

        public Boolean Actualizar(Decimal ID_EMPRESA, String ACTIVO, System.DateTime FCH_INGRESO, String NIT_EMPRESA, String ACT_ECO,
            String RAZ_SOCIAL, String DIR_EMP, String CIU_EMP, String TEL_EMP, String CUB_CIUDADES, String NOM_REP_LEGAL, String CC_REP_LEGAL,
            String TIPO_EMPRESA, String CIU_ORG_NEG, Int32 NUM_EMPLEADOS, String USU_MOD, String FAC_NAL,
            Decimal ID_ALIANZA, Int32 DIG_VER, String EMP_ESTADO, String EMP_EXC_IVA, String TEL_EMP1, String NUM_CELULAR, String ID_ACTIVIDAD, Decimal ID_GRUPO_EMPRESARIAL,
            List<cobertura> coberturas, String ID_CIUDAD_CC_REP_LEGAL, historialActivacion activacion, String ID_SERVICIO, String TIP_DOC_REP_LEGAL, List<empresasRiesgos> listaRiesgos, String RAZ_SOCIAL_ANTERIOR)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            Boolean continuarNormalmente = true;
            Conexion conexion = new Conexion(Empresa);
            ManualServicio _manual = new ManualServicio(Empresa, Usuario);
            conexion.IniciarTransaccion();

            Boolean verificador = true;
            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion); 

                DataTable tablaDatosAnteriores = _manual.ObtenerInfoRegistroTabla(diccionarioCamposVenEmpresas, "VEN_EMPRESAS", "ID_EMPRESA", ID_EMPRESA.ToString(), conexion);

                if (ActualizarRegistroVEN_EMPRESAS(ID_EMPRESA, ACTIVO, FCH_INGRESO, NIT_EMPRESA, ACT_ECO, RAZ_SOCIAL, DIR_EMP, CIU_EMP, TEL_EMP, CUB_CIUDADES, NOM_REP_LEGAL, CC_REP_LEGAL, TIPO_EMPRESA, CIU_ORG_NEG, NUM_EMPLEADOS, USU_MOD, FAC_NAL, ID_ALIANZA, DIG_VER, EMP_ESTADO, EMP_EXC_IVA, TEL_EMP1, NUM_CELULAR, ID_ACTIVIDAD, ID_GRUPO_EMPRESARIAL, ID_CIUDAD_CC_REP_LEGAL, ID_SERVICIO, TIP_DOC_REP_LEGAL, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    verificador = false;
                }
                else
                {
                    DataTable tablaDatosNuevos = _manual.ObtenerInfoRegistroTabla(diccionarioCamposVenEmpresas, "VEN_EMPRESAS", "ID_EMPRESA", ID_EMPRESA.ToString(), conexion);

                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.DeterminarYRegistrarModificacionesEnCampos(diccionarioCamposVenEmpresas, tablaDatosAnteriores, tablaDatosNuevos, ID_VERSIONAMIENTO, ID_EMPRESA, ManualServicio.ListaSecciones.Comercial, ManualServicio.AccionesManual.Actualizar, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            conexion.DeshacerTransaccion();
                            verificador = false;
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                        }
                    }

                    if (continuarNormalmente == true)
                    {
                        cobertura _cobertura = new cobertura(Empresa);
                        Boolean eliminarCobertura = true;
                        Boolean insertarCobertura = true;

                        DataTable tablaCoberturaActual = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA, conexion);

                        for (int i = 0; i < tablaCoberturaActual.Rows.Count; i++)
                        {
                            DataRow filaCoberturaActual = tablaCoberturaActual.Rows[i];
                            String ID_CIUDAD_COBERTURA_ACTUAL = filaCoberturaActual["Código Ciudad"].ToString().Trim();

                            eliminarCobertura = true;

                            foreach (cobertura c in coberturas)
                            {
                                if (c.IDCIUDAD == ID_CIUDAD_COBERTURA_ACTUAL)
                                {
                                    eliminarCobertura = false;
                                    break;
                                }
                            }

                            if (eliminarCobertura == true)
                            {
                                if (realizarVersionamientoManual == true)
                                {
                                    ID_VERSIONAMIENTO = _manual.RegistrarDesactivacionRegistroTabla(ID_EMPRESA, ManualServicio.ListaSecciones.Comercial, ManualServicio.AccionesManual.Eliminar, "COBERTURA", "Cobertura", ID_CIUDAD_COBERTURA_ACTUAL, ID_VERSIONAMIENTO, conexion);
                                    if (ID_VERSIONAMIENTO == -1)
                                    {
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        continuarNormalmente = false;
                                        ID_VERSIONAMIENTO = 0;
                                        break;
                                    }
                                }

                                if (continuarNormalmente == true)
                                {
                                    if (_cobertura.EliminarUnaCiudadDeLaCoberturaDeCliente(ID_EMPRESA, ID_CIUDAD_COBERTURA_ACTUAL, Usuario, conexion) == false)
                                    {
                                        MensajeError = _cobertura.MensajeError;
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        break;
                                    }
                                }
                            }
                        }

                        foreach (cobertura c in coberturas)
                        {
                            insertarCobertura = true;

                            for (int i = 0; i < tablaCoberturaActual.Rows.Count; i++)
                            {
                                DataRow filaCoberturaActual = tablaCoberturaActual.Rows[i];
                                String ID_CIUDAD_COBERTURA_ACTUAL = filaCoberturaActual["Código Ciudad"].ToString().Trim();

                                if (c.IDCIUDAD == ID_CIUDAD_COBERTURA_ACTUAL)
                                {
                                    insertarCobertura = false;
                                    break;
                                }
                            }

                            if (insertarCobertura == true)
                            {
                                if (realizarVersionamientoManual == true)
                                {
                                    ID_VERSIONAMIENTO = _manual.RegistrarInsersionRegistroTabla(ID_VERSIONAMIENTO, ManualServicio.ListaSecciones.Comercial, "COBERTURA", "Cobertura", c.IDCIUDAD, ManualServicio.AccionesManual.Insertar, ID_EMPRESA, conexion);
                                    if (ID_VERSIONAMIENTO == -1)
                                    {
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        continuarNormalmente = false;
                                        ID_VERSIONAMIENTO = 0;
                                        break;
                                    }
                                }

                                if (continuarNormalmente == true)
                                {
                                    if (_cobertura.Adicionar(ID_EMPRESA, c.IDCIUDAD, Usuario, conexion) == false)
                                    {
                                        verificador = false;
                                        conexion.DeshacerTransaccion();
                                        MensajeError = _cobertura.MensajeError;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                if (_cobertura.ActualizarPermisosParaCiudaYEmpresaEnGeneral(ID_EMPRESA, c.IDCIUDAD, Usuario, conexion) == false)
                                {
                                    verificador = false;
                                    conexion.DeshacerTransaccion();
                                    MensajeError = _cobertura.MensajeError;
                                    break;
                                }
                            }
                        }


                        if (verificador == true)
                        {
                            empresasRiesgos _empresasRiesgos = new empresasRiesgos(Empresa, Usuario);
                            DataTable tablaRiesgosActuales = _empresasRiesgos.ObtenerRoesgosPorEmpresa(ID_EMPRESA, conexion);

                            Boolean riesgoEncontrado = false;

                            foreach (DataRow filaRiegosActuales in tablaRiesgosActuales.Rows)
                            {
                                riesgoEncontrado = false;
                                foreach (empresasRiesgos filaRiesgosNuevos in listaRiesgos)
                                {
                                    if (filaRiesgosNuevos.DESCRIPCION_RIESGO == filaRiegosActuales["DESCRIPCION_RIESGO"].ToString())
                                    {
                                        riesgoEncontrado = true;
                                        break;
                                    }
                                }

                                if (riesgoEncontrado == false)
                                {
                                    if (_empresasRiesgos.desactivarRiesgoParaEmpresa(ID_EMPRESA, filaRiegosActuales["DESCRIPCION_RIESGO"].ToString(), conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        break;
                                    }
                                }
                            }

                            if (verificador == true)
                            {
                                foreach (empresasRiesgos filaRiesgosNuevos in listaRiesgos)
                                {
                                    if (_empresasRiesgos.Adicionar(ID_EMPRESA, filaRiesgosNuevos.DESCRIPCION_RIESGO, conexion) == 0)
                                    {
                                        verificador = false;
                                        MensajeError = _empresasRiesgos.MensajeError;
                                        conexion.DeshacerTransaccion();
                                        break;
                                    }
                                }

                                if (verificador == true)
                                {
                                    historialActivacion _historialActivacion = new historialActivacion(Empresa);
                                    if (_historialActivacion.Adicionar(ID_EMPRESA, activacion.CLASE_REGISTRO, activacion.COMENTARIO, USU_MOD, conexion) == false)
                                    {
                                        conexion.DeshacerTransaccion();
                                        verificador = false;
                                        MensajeError = _historialActivacion.MensajeError;
                                    }
                                    else
                                    {
                                        if (RAZ_SOCIAL != RAZ_SOCIAL_ANTERIOR)
                                        {
                                            if (AdicionarCambiorazonSocial(ID_EMPRESA, RAZ_SOCIAL_ANTERIOR, RAZ_SOCIAL, Usuario, conexion) <= 0)
                                            {
                                                conexion.DeshacerTransaccion();
                                                verificador = false;
                                            }
                                        }
                                        else
                                        {
                                            verificador = true;
                                        }

                                        if (verificador == true)
                                        {
                                            conexion.AceptarTransaccion();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                verificador = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return verificador;
        }

        public Decimal AdicionarCambiorazonSocial(Decimal ID_EMPRESA
            , String RAZ_SOCIAL_ANTERIOR
            , String RAZ_SOCIAL_NUEVO
            , String USU_CRE
            , Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_reg_cambio_raz_social_adicionar ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RAZ_SOCIAL_ANTERIOR)))
            {
                sql += "'" + RAZ_SOCIAL_ANTERIOR + "', ";
                informacion += "RAZ_SOCIAL_ANTERIOR= '" + RAZ_SOCIAL_ANTERIOR + "', ";
            }
            else
            {
                MensajeError += "El campo RAZ_SOCIAL_ANTERIOR no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RAZ_SOCIAL_NUEVO)))
            {
                sql += "'" + RAZ_SOCIAL_NUEVO + "', ";
                informacion += "RAZ_SOCIAL_NUEVO= '" + RAZ_SOCIAL_NUEVO + "', ";
            }
            else
            {
                MensajeError += "El campo RAZ_SOCIAL_NUEVO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_REG_CAMBIO_RAZ_SOCIAL, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }


        public Boolean DesligarEmpresaDeGrupoEmpresarial(Decimal ID_EMPRESA
            , Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            tools _tools = new tools();

            Boolean verificador = true;

            sql = "usp_ven_empresas_quitar_grupo_empresarial ";

            #region validaciones
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

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar == true)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        verificador = false;
                        MensajeError = "No se pudo realizar la actualización del cliente, error en UPDATE.";
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false)
                        {
                            verificador = false;
                            MensajeError = _auditoria.MensajError;
                        }
                        #endregion auditoria
                    }
                }
                catch (Exception ex)
                {
                    verificador = false;
                    MensajeError = ex.Message;
                }
            }
            else
            {
                verificador = false;
            }

            return verificador;
        }


        public Boolean ActualizarGrupoEmpresarialCliente(Decimal ID_EMPRESA
            , Decimal ID_GRUPO_EMPRESARIAL
            , Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            String informacion = null;

            tools _tools = new tools();

            Boolean verificador = true;

            sql = "usp_ven_empresas_actualizar_grupo_empresarial ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_GRUPO_EMPRESARIAL != 0)
            {
                sql += ID_GRUPO_EMPRESARIAL + ", ";
                informacion += "ID_GRUPO_EMPRESARIAL = '" + ID_GRUPO_EMPRESARIAL.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID DE GRUPO EMPRESARIAL no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar == true)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        verificador = false;
                        MensajeError = "No se pudo realizar la actualización del cliente, error en UPDATE.";
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (_auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false)
                        {
                            verificador = false;
                            MensajeError = _auditoria.MensajError;
                        }
                        #endregion auditoria
                    }
                }
                catch (Exception ex)
                {
                    verificador = false;
                    MensajeError = ex.Message;
                }
            }
            else
            {
                verificador = false;
            }

            return verificador;
        }

        public DataTable ObtenerTodosLosGruposEmpresariales()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_grupos_empresariales_obtener_todos ";
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


        public DataTable ObtenerEmpresasAsociadasAGrupo(Decimal ID_GRUPOEMPRESARIAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_ven_empresas_obtener_por_id_grupo_empresarial ";

            if (ID_GRUPOEMPRESARIAL != 0)
            {
                sql += ID_GRUPOEMPRESARIAL;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_DEL GRUPO EMPRESARIAL no puede ser 0.";
            }

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


        public DataTable ObtenerEmpresasAsociadasAGrupo(Decimal ID_GRUPOEMPRESARIAL, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_ven_empresas_obtener_por_id_grupo_empresarial ";

            if (ID_GRUPOEMPRESARIAL != 0)
            {
                sql += ID_GRUPOEMPRESARIAL;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo ID_DEL GRUPO EMPRESARIAL no puede ser 0.";
            }

            if (ejecutar == true)
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

        public DataTable ObtenerEmpresasDelMismoGrupoEmpresarial(Decimal ID_GRUPO_EMPRESARIAL, Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscar_id_grupo_empresarial_con_condiciones_comerciales ";

            #region validaciones
            if (ID_GRUPO_EMPRESARIAL != 0)
            {
                sql += ID_GRUPO_EMPRESARIAL + ", ";
            }
            else
            {
                MensajeError = "El campo ID DEL GRUPO EMPRESARIAL no puede ser 0.";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0.";
                ejecutar = false;
            }
            #endregion validaciones

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


        public DataTable ObtenerNumEmpleadosActivosPorIdEmpresa(Decimal ID_EMPRESA, String ESTADO_EMPLEADO, String ESTADO_CONTRATO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_empleados_obtener_numero_emplados_activos_por_empresa ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError = "El campo ID DE LA EMPRESA no puede ser 0.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO_EMPLEADO) == false)
            {
                sql += "'" + ESTADO_EMPLEADO + "', ";
            }
            else
            {
                MensajeError = "El campo ESTADO DEL CONTRATO no puede ser 0.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ESTADO_CONTRATO) == false)
            {
                sql += "'" + ESTADO_CONTRATO + "'";
            }
            else
            {
                MensajeError = "El campo ESTADO DEL EMPLEADO no puede ser 0.";
                ejecutar = false;
            }
            #endregion validaciones

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


        public DataTable ObtenerEmpresasAlianza()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_empresas_obtenerEmpresasAlianza ";
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


        public DataTable ObtenerTodasLasEmpresasActivas()
        {

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_empresas_buscar_todas_activas ";

            sql += "'" + Usuario + "'";

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

        public DataTable ObtenerEmpresaConIdEmpresa(decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscar_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString();
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerEmpresaConIdEmpresa(decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscar_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString();
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar == true)
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

        public DataTable ObtenerTodosLosEjecutivosSertempo()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_ejecutivos_buscar_todos_activos ";
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

        public DataTable obtenerDatosEjecutivoPorIdEjecutivo(Decimal ID_EJECUTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_ven_ejecutivos_buscar_por_id_ejecutivo ";

            if (ID_EJECUTIVO != 0)
            {
                sql += ID_EJECUTIVO;
                informacion += "ID_EJECUTIVO = '" + ID_EJECUTIVO.ToString() + "'";
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EJECUTIVOS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
            else
            {
                MensajeError = "El campo id_ejecutivo no puede ser vacio. \n";
            }
            return _dataTable;
        }

        public DataTable ObtenerEmpresaConRazSocial(String RAZ_SOCIAL)
        {

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_ven_empresas_buscarPorRazonSocial ";

            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += "'" + RAZ_SOCIAL + "', ";
                informacion += "RAZ_SOCIAL = '" + RAZ_SOCIAL.ToString() + "', ";

                sql += "'" + Usuario + "'";
                informacion += "USU_LOG = '" + Usuario + "'";
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
            else
            {
                MensajeError = "El campoRAZ_SOCIAL no puede ser vacio. \n";
            }
            return _dataTable;
        }

        public DataTable ObtenerEmpresaConRazSocialSoloComercial(String RAZ_SOCIAL)
        {

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_ven_empresas_buscarPorRazonSocial_solo_comercial ";

            if (!(String.IsNullOrEmpty(RAZ_SOCIAL)))
            {
                sql += "'" + RAZ_SOCIAL + "', ";
                informacion += "RAZ_SOCIAL = '" + RAZ_SOCIAL.ToString() + "', ";

                sql += "'" + Usuario + "'";
                informacion += "USU_LOG = '" + Usuario + "'";
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
            else
            {
                MensajeError = "El campoRAZ_SOCIAL no puede ser vacio. \n";
            }
            return _dataTable;
        }

        public DataTable ObtenerEmpresaConGrupoEmpresarial(String GRUPO_EMPRESARIAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscarPorGrupoEmpresarial ";


            if (!(String.IsNullOrEmpty(GRUPO_EMPRESARIAL)))
            {
                sql += "'" + GRUPO_EMPRESARIAL + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo GRUPO EMPRESARAIL no puede ser vacio. \n";
            }

            sql += "'" + Usuario + "'";

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


        public DataTable ObtenerEmpresaConGrupoEmpresarialSoloComercial(String GRUPO_EMPRESARIAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscarPorGrupoEmpresarial_solo_comercial ";


            if (!(String.IsNullOrEmpty(GRUPO_EMPRESARIAL)))
            {
                sql += "'" + GRUPO_EMPRESARIAL + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo GRUPO EMPRESARAIL no puede ser vacio. \n";
            }

            sql += "'" + Usuario + "'";

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


        public DataTable ObtenerEmpresaConRegional(String REGIONAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscarPorRegional ";


            if (!(String.IsNullOrEmpty(REGIONAL)))
            {
                sql += "'" + REGIONAL + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo REGIONAL no puede ser vacio. \n";
            }

            sql += "'" + Usuario + "'";

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



        public DataTable ObtenerEmpresaConRegionalSoloComercial(String REGIONAL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscarPorRegional_solo_comercial ";


            if (!(String.IsNullOrEmpty(REGIONAL)))
            {
                sql += "'" + REGIONAL + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo REGIONAL no puede ser vacio. \n";
            }

            sql += "'" + Usuario + "'";

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


        public DataTable ObtenerEmpresaConCiudad(String CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscarPorCiudad ";

            if (!(String.IsNullOrEmpty(CIUDAD)))
            {
                sql += "'" + CIUDAD + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo CIUDAD no puede ser vacio. \n";
            }

            sql += "'" + Usuario + "'";

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




        public DataTable ObtenerEmpresaConCiudadSoloComercial(String CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscarPorCiudad_solo_comercial ";

            if (!(String.IsNullOrEmpty(CIUDAD)))
            {
                sql += "'" + CIUDAD + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo CIUDAD no puede ser vacio. \n";
            }

            sql += "'" + Usuario + "'";

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


        public DataTable ObtenerEmpresaConComercial(String COMERCIAL)
        {

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscarPorComercial ";


            if (!(String.IsNullOrEmpty(COMERCIAL)))
            {
                sql += "'" + COMERCIAL + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo COMERCIAL no puede ser vacio. \n";
            }

            sql += "'" + Usuario + "'";

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


        public DataTable ObtenerEmpresaConComercialSoloComercial(String COMERCIAL)
        {

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_empresas_buscarPorComercial_solo_comercial ";


            if (!(String.IsNullOrEmpty(COMERCIAL)))
            {
                sql += "'" + COMERCIAL + "', ";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo COMERCIAL no puede ser vacio. \n";
            }

            sql += "'" + Usuario + "'";

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



        public DataTable ObtenerEmpresaConCodEmpresa(String COD_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_ven_empresas_buscar_por_cod_empresa ";

            if (!(String.IsNullOrEmpty(COD_EMPRESA)))
            {
                sql += "'" + COD_EMPRESA + "', ";
                informacion += "COD_EMPRESA = '" + COD_EMPRESA.ToString() + "', ";

                sql += "'" + Usuario + "'";
                informacion += "USU_LOG = '" + Usuario + "'";

                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
            else
            {
                MensajeError = "El campo COD_EMPRESA no puede ser vacio. \n";
            }
            return _dataTable;
        }

        public DataTable ObtenerEmpresaConCodEmpresaSoloComercial(String COD_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_ven_empresas_buscar_por_cod_empresa_solo_comercial ";

            if (!(String.IsNullOrEmpty(COD_EMPRESA)))
            {
                sql += "'" + COD_EMPRESA + "', ";
                informacion += "COD_EMPRESA = '" + COD_EMPRESA.ToString() + "', ";

                sql += "'" + Usuario + "'";
                informacion += "USU_LOG = '" + Usuario + "'";

                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
            else
            {
                MensajeError = "El campo COD_EMPRESA no puede ser vacio. \n";
            }
            return _dataTable;
        }

        public DataTable ObtenerEmpresaConFechaNomina(String FECHA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_ven_empresas_buscar_por_fecha_nomina ";

            if (!(String.IsNullOrEmpty(FECHA.ToString())))
            {
                sql += "'" + FECHA + "'";
                informacion += "FECHA = '" + FECHA.ToString() + "'";
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
            else
            {
                MensajeError = "El campo FECHA no puede ser vacio. \n";
            }
            return _dataTable;
        }

        public DataTable ObtenerEmpresaConAnalistaNomina(String ANALISTA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_ven_empresas_buscar_por_analista ";

            if (!(String.IsNullOrEmpty(ANALISTA)))
            {
                sql += "'" + ANALISTA + "'";
                informacion += "ANALISTA = '" + ANALISTA.ToString() + "'";
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_EMPRESAS, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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
            else
            {
                MensajeError = "El campo ANALISTA no puede ser vacio. \n";
            }
            return _dataTable;
        }

        public DataTable ObtenerEmpresaConNitEmpresa(String NIT_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_ven_empresas_buscar_por_nit_empresa ";

            if (!(String.IsNullOrEmpty(NIT_EMPRESA)))
            {
                sql += "'" + NIT_EMPRESA + "', ";
                informacion += "NIT_EMPRESA = '" + NIT_EMPRESA.ToString() + "', ";

                sql += "'" + Usuario + "'";
                informacion += "USU_LOG = '" + Usuario + "'";

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
            else
            {
                MensajeError = "El campo NIT_EMPRESA no puede ser vacio. \n";
            }
            return _dataTable;
        }



        public DataTable ObtenerEmpresaConNitEmpresaSoloComercial(String NIT_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_ven_empresas_buscar_por_nit_empresa_solo_comercial ";

            if (!(String.IsNullOrEmpty(NIT_EMPRESA)))
            {
                sql += "'" + NIT_EMPRESA + "', ";
                informacion += "NIT_EMPRESA = '" + NIT_EMPRESA.ToString() + "', ";

                sql += "'" + Usuario + "'";
                informacion += "USU_LOG = '" + Usuario + "'";

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
            else
            {
                MensajeError = "El campo NIT_EMPRESA no puede ser vacio. \n";
            }
            return _dataTable;
        }

        #endregion metodos
    }
}