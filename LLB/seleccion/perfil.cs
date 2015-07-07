using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.operaciones;

namespace Brainsbits.LLB.seleccion
{
    public class perfil
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Dictionary<String, String> diccionarioCamposPerfil;
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
        public perfil(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;

            diccionarioCamposPerfil = new Dictionary<string, string>();

            diccionarioCamposPerfil.Add("ID_OCUPACION", "Cargo");
            diccionarioCamposPerfil.Add("EDAD_MAX", "Edad Máxima");
            diccionarioCamposPerfil.Add("EDAD_MIN", "Edad Mínima");
            diccionarioCamposPerfil.Add("SEXO", "Sexo");
            diccionarioCamposPerfil.Add("EXPERIENCIA", "Experiencia");
            diccionarioCamposPerfil.Add("NIV_ESTUDIOS", "Nivel de Estudios");
        }
        #endregion constructores

        #region metodos

        private Decimal AdicionarVenDPerfiles(Decimal ID_EMPRESA,
            Decimal ID_OCUPACION,
            String EDAD_MIN,
            String EDAD_MAX,
            String SEXO,
            String EXPERIENCIA,
            String NIV_ESTUDIOS,
            String OBSERVACIONES_ESPECIALES,
            String TIPO_ENTREVISTA,
            Boolean ESTADO,
            Decimal ID_CATEGORIA_REFERENCIA,
            Decimal ID_ASSESMENT_CENTER,
            String NIVEL_REQUERIMIENTO,
            Conexion conexion)
        {
            String sql = null;
            Decimal REGISTRO = 0;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_d_perfiles_adicionar ";

            #region validaciones

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_OCUPACION != 0)
            {
                sql += ID_OCUPACION + ", ";
                informacion += "ID_OCUPACION = '" + ID_OCUPACION + "', ";
            }
            else
            {
                MensajeError += "El campo ID_OCUPACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EDAD_MIN)))
            {
                sql += "'" + EDAD_MIN + "', ";
                informacion += "EDAD_MIN = '" + EDAD_MIN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo EDAD_MIN no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(EDAD_MAX)))
            {
                sql += "'" + EDAD_MAX + "', ";
                informacion += "EDAD_MAX = '" + EDAD_MAX.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo EDAD_MAX no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(SEXO)))
            {
                sql += "'" + SEXO + "', ";
                informacion += "SEXO = '" + SEXO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SEXO no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(EXPERIENCIA)))
            {
                sql += "'" + EXPERIENCIA + "', ";
                informacion += "EXPERIENCIA = '" + EXPERIENCIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo EXPERIENCIA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(NIV_ESTUDIOS)))
            {
                sql += "'" + NIV_ESTUDIOS + "', ";
                informacion += "NIV_ESTUDIOS = '" + NIV_ESTUDIOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NIV_ESTUDIOS no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(OBSERVACIONES_ESPECIALES)))
            {
                sql += "'" + OBSERVACIONES_ESPECIALES + "', ";
                informacion += "OBSERVACIONES_ESPECIALES = '" + OBSERVACIONES_ESPECIALES + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES_ESPECIALES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_ENTREVISTA)))
            {
                sql += "'" + TIPO_ENTREVISTA + "', ";
                informacion += "TIPO_ENTREVISTA = '" + TIPO_ENTREVISTA + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_ENTREVISTA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ESTADO == true)
            {
                sql += "'ACTIVO', ";
                informacion += "ESTADO = 'ACTIVO', ";
            }
            else
            {
                sql += "'OCULTO', ";
                informacion += "ESTADO = 'OCULTO', ";
            }

            if (ID_CATEGORIA_REFERENCIA != 0)
            {
                sql += ID_CATEGORIA_REFERENCIA + ", ";
                informacion += "ID_CATEGORIA_REFERENCIA = '" + ID_CATEGORIA_REFERENCIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA_REFERENCIA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ASSESMENT_CENTER != 0)
            {
                sql += ID_ASSESMENT_CENTER + ", ";
                informacion += "ID_ASSESMENT_CENTER = '" + ID_ASSESMENT_CENTER + "',";
            }
            else
            {
                sql += "NULL,";
                informacion += "ID_ASSESMENT_CENTER = 'NULL',";
            }

            if (String.IsNullOrEmpty(NIVEL_REQUERIMIENTO) == false)
            {
                sql += "'" + NIVEL_REQUERIMIENTO + "'";
                informacion += "NIVEL_REQUERIMIENTO = '" + NIVEL_REQUERIMIENTO + "'";
            }
            else
            {
                MensajeError += "El campo NIVEL_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    REGISTRO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_D_PERFILES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    REGISTRO = 0;
                }
            }

            return REGISTRO;
        }

        public Decimal Adicionar(Decimal ID_EMPRESA,
            Decimal ID_OCUPACION,
            String EDAD_MIN,
            String EDAD_MAX,
            String SEXO,
            String EXPERIENCIA,
            String NIV_ESTUDIOS,
            List<documentoPerfil> documentos,
            List<pruebaPerfil> pruebas,
            String OBSERVACIONES_ESPECIALES,
            String TIPO_ENTREVISTA,
            Decimal ID_CATEGORIA_REFERENCIA,
            Decimal ID_ASSESMENT_CENTER,
            Boolean ESTADO,
            String NIVEL_REQUERIMIENTO)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            Boolean continuarNormalmente = true;
            ManualServicio _manual = new ManualServicio(Empresa, Usuario);

            Boolean correcto = true;
            Decimal ID_PERFIL = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion); 

                if (realizarVersionamientoManual == true)
                {
                    ID_VERSIONAMIENTO = _manual.RegistrarInsersionRegistroTabla(ID_VERSIONAMIENTO, ManualServicio.ListaSecciones.Seleccion, "PERFIL", "Perfil", ID_OCUPACION.ToString(), ManualServicio.AccionesManual.Insertar, ID_EMPRESA, conexion);
                    if (ID_VERSIONAMIENTO == -1)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        continuarNormalmente = false;
                        ID_VERSIONAMIENTO = 0;
                    }
                }

                if (continuarNormalmente == true)
                {
                    ID_PERFIL = AdicionarVenDPerfiles(ID_EMPRESA, ID_OCUPACION, EDAD_MIN, EDAD_MAX, SEXO, EXPERIENCIA, NIV_ESTUDIOS, OBSERVACIONES_ESPECIALES, TIPO_ENTREVISTA, ESTADO, ID_CATEGORIA_REFERENCIA, ID_ASSESMENT_CENTER, NIVEL_REQUERIMIENTO, conexion);

                    if (ID_PERFIL <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        ID_PERFIL = 0;
                    }
                    else
                    {
                        documentoPerfil _documentoPerfil = new documentoPerfil(Empresa, Usuario);

                        foreach (documentoPerfil d in documentos)
                        {
                            if (_documentoPerfil.Adicionar(ID_PERFIL, Convert.ToDecimal(d.IDDOCUMENTO), conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_PERFIL = 0;
                                MensajeError = _documentoPerfil.MensajeError;
                                break;
                            }
                        }

                        if (correcto == true)
                        {
                            pruebaPerfil _pruebaPerfil = new pruebaPerfil(Empresa, Usuario);
                            foreach (pruebaPerfil p in pruebas)
                            {
                                if (_pruebaPerfil.Adicionar(ID_PERFIL, Convert.ToDecimal(p.IDPRUEBA), conexion) == false)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    ID_PERFIL = 0;
                                    MensajeError = _pruebaPerfil.MensajeError;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                correcto = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_PERFIL;
        }


        public Boolean ActualizarVenDPerfil(Decimal REGISTRO,
            Decimal ID_EMPRESA,
            String EDAD_MIN,
            String EDAD_MAX,
            String SEXO,
            String EXPERIENCIA,
            String NIV_ESTUDIOS,
            String OBSERVACIONES_ESPECIALES,
            String TIPO_ENTREVISTA,
            Boolean ESTADO,
            Decimal ID_CATEGORIA_REFERENCIA,
            Decimal ID_ASSESMENT_CENTER,
            String NIVEL_REQUERIMIENTO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_ven_d_perfiles_actualizar ";

            #region validaciones

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = '" + REGISTRO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EDAD_MIN)))
            {
                sql += "'" + EDAD_MIN + "', ";
                informacion += "EDAD_MIN = '" + EDAD_MIN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo EDAD_MIN no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EDAD_MAX)))
            {
                sql += "'" + EDAD_MAX + "', ";
                informacion += "EDAD_MAX = '" + EDAD_MAX.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo EDAD_MAX no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SEXO)))
            {
                sql += "'" + SEXO + "', ";
                informacion += "SEXO = '" + SEXO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SEXO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(EXPERIENCIA)))
            {
                sql += "'" + EXPERIENCIA + "', ";
                informacion += "EXPERIENCIA = '" + EXPERIENCIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo EXPERIENCIA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NIV_ESTUDIOS)))
            {
                sql += "'" + NIV_ESTUDIOS + "', ";
                informacion += "NIV_ESTUDIOS = '" + NIV_ESTUDIOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NIV_ESTUDIOS no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (!(String.IsNullOrEmpty(OBSERVACIONES_ESPECIALES)))
            {
                sql += "'" + OBSERVACIONES_ESPECIALES + "', ";
                informacion += "OBSERVACIONES_ESPECIALES = '" + OBSERVACIONES_ESPECIALES + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES_ESPECIALES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_ENTREVISTA)))
            {
                sql += "'" + TIPO_ENTREVISTA + "', ";
                informacion += "TIPO_ENTREVISTA = '" + TIPO_ENTREVISTA + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_ENTREVISTA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ESTADO == true)
            {
                sql += "'ACTIVO', ";
                informacion += "ESTADO = 'ACTIVO', ";
            }
            else
            {
                sql += "'OCULTO', ";
                informacion += "ESTADO = 'OCULTO', ";
            }

            if (ID_CATEGORIA_REFERENCIA != 0)
            {
                sql += ID_CATEGORIA_REFERENCIA + ", ";
                informacion += "ID_CATEGORIA_REFERENCIA = '" + ID_CATEGORIA_REFERENCIA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA_REFERENCIA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ASSESMENT_CENTER != 0)
            {
                sql += ID_ASSESMENT_CENTER + ",";
                informacion += "ID_ASSESMENT_CENTER = '" + ID_ASSESMENT_CENTER + "',";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_ASSESMENT_CENTER = 'NULL', ";
            }

            if (String.IsNullOrEmpty(NIVEL_REQUERIMIENTO) == false)
            {
                sql += "'" + NIVEL_REQUERIMIENTO + "'";
                informacion += "NIVEL_REQUERIMIENTO = '" + NIVEL_REQUERIMIENTO + "'";
            }
            else
            {
                MensajeError += "El campo NIVEL_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        ejecutadoCorrectamente = false;
                    }

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (_auditoria.Adicionar(Usuario, tabla.VEN_D_PERFILES, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion) == false) ejecutadoCorrectamente = false;
                    #endregion auditoria
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

            return ejecutadoCorrectamente;
        }

        public Decimal ActualizarDocumentosDePerfil(Decimal ID_PERFIL, List<documentoPerfil> documentos, Boolean realizarVersionamientoManual, Decimal ID_VERSIONAMIENTO, ManualServicio _manual, Decimal ID_EMPRESA, Conexion conexion)
        {
            Boolean continuarNormalmente = true;

            documentoPerfil _documentoPerfil = new documentoPerfil(Empresa, Usuario);
            Boolean eliminarDocumento = true;
            Boolean insertarDocumento = true;

            DataTable tablaDocumentosActuales = _documentoPerfil.ObtenerPorIdPerfil(ID_PERFIL, conexion);

            for (int i = 0; i < tablaDocumentosActuales.Rows.Count; i++)
            {
                DataRow filaDocumentoActual = tablaDocumentosActuales.Rows[i];
                Decimal ID_DOCUMENTO_ACTUAL = Convert.ToDecimal(filaDocumentoActual["Código Documento"]);

                eliminarDocumento = true;

                foreach (documentoPerfil d in documentos)
                {
                    if (d.IDDOCUMENTO == ID_DOCUMENTO_ACTUAL.ToString())
                    {
                        eliminarDocumento = false;
                        break;
                    }
                }

                if (eliminarDocumento == true)
                {
                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.RegistrarDesactivacionRegistroTabla(ID_EMPRESA, ManualServicio.ListaSecciones.Seleccion, ManualServicio.AccionesManual.Eliminar, "DOCUMENTO_PERFIL", "Documento de Perfil", ID_DOCUMENTO_ACTUAL.ToString(), ID_VERSIONAMIENTO, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                            return -1;
                        }
                    }

                    if (continuarNormalmente == true)
                    {
                        if (_documentoPerfil.EliminarDocumentoDePerfil(ID_PERFIL, ID_DOCUMENTO_ACTUAL, conexion) == false)
                        {
                            MensajeError = _documentoPerfil.MensajeError;
                            return -1;
                        }
                    }
                }
            }

            foreach (documentoPerfil d in documentos)
            {
                insertarDocumento = true;

                for (int i = 0; i < tablaDocumentosActuales.Rows.Count; i++)
                {
                    DataRow filaDocumentoActual = tablaDocumentosActuales.Rows[i];
                    Decimal ID_DOCUMENTO_ACTUAL = Convert.ToDecimal(filaDocumentoActual["Código Documento"]);

                    if (d.IDDOCUMENTO == ID_DOCUMENTO_ACTUAL.ToString())
                    {
                        insertarDocumento = false;
                        break;
                    }
                }

                if (insertarDocumento == true)
                {
                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.RegistrarInsersionRegistroTabla(ID_VERSIONAMIENTO, ManualServicio.ListaSecciones.Seleccion, "DOCUMENTO_PERFIL", "Documento de Perfil", d.IDDOCUMENTO, ManualServicio.AccionesManual.Insertar, ID_EMPRESA, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                            return -1;
                        }
                    }

                    if (continuarNormalmente == true)
                    {
                        if (_documentoPerfil.Adicionar(ID_PERFIL, Convert.ToDecimal(d.IDDOCUMENTO), conexion) == false)
                        {
                            MensajeError = _documentoPerfil.MensajeError;
                            return -1;
                        }
                    }
                }
            }

            return ID_VERSIONAMIENTO;
        }

        public Decimal ActualizarPruebasPerfil(Decimal ID_PERFIL, List<pruebaPerfil> pruebas, Boolean realizarVersionamientoManual, Decimal ID_VERSIONAMIENTO, ManualServicio _manual, Decimal ID_EMPRESA, Conexion conexion)
        {
            Boolean continuarNormalmente = true;

            pruebaPerfil _pruebaPerfil = new pruebaPerfil(Empresa, Usuario);
            Boolean eliminarPrueba = true;
            Boolean insertarPrueba = true;

            DataTable tablaPruebasActuales = _pruebaPerfil.ObtenerPorIdPerfil(ID_PERFIL, conexion);

            for (int i = 0; i < tablaPruebasActuales.Rows.Count; i++)
            {
                DataRow filaPruebaActual = tablaPruebasActuales.Rows[i];
                Decimal ID_PRUEBA_ACTUAL = Convert.ToDecimal(filaPruebaActual["Código Prueba"]);

                eliminarPrueba = true;

                foreach (pruebaPerfil p in pruebas)
                {
                    if (p.IDPRUEBA == ID_PRUEBA_ACTUAL.ToString())
                    {
                        eliminarPrueba = false;
                        break;
                    }
                }

                if (eliminarPrueba == true)
                {
                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.RegistrarDesactivacionRegistroTabla(ID_EMPRESA, ManualServicio.ListaSecciones.Seleccion, ManualServicio.AccionesManual.Eliminar, "PRUEBA_PERFIL", "Prueba de Perfil", ID_PRUEBA_ACTUAL.ToString(), ID_VERSIONAMIENTO, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                            return -1;
                        }
                    }

                    if (continuarNormalmente == true)
                    {
                        if (_pruebaPerfil.EliminarPruebaDePerfil(ID_PERFIL, ID_PRUEBA_ACTUAL, conexion) == false)
                        {
                            MensajeError = _pruebaPerfil.MensajeError;
                            return -1;
                        }
                    }
                }
            }

            foreach (pruebaPerfil p in pruebas)
            {
                insertarPrueba = true;

                for (int i = 0; i < tablaPruebasActuales.Rows.Count; i++)
                {
                    DataRow filaPruebaActual = tablaPruebasActuales.Rows[i];
                    Decimal ID_PRUEBA_ACTUAL = Convert.ToDecimal(filaPruebaActual["Código Prueba"]);

                    if (p.IDPRUEBA == ID_PRUEBA_ACTUAL.ToString())
                    {
                        insertarPrueba = false;
                        break;
                    }
                }

                if (insertarPrueba == true)
                {
                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.RegistrarInsersionRegistroTabla(ID_VERSIONAMIENTO, ManualServicio.ListaSecciones.Seleccion, "PRUEBA_PERFIL", "Prueba de Perfil", p.IDPRUEBA, ManualServicio.AccionesManual.Insertar, ID_EMPRESA, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                            return -1;
                        }
                    }

                    if (continuarNormalmente == true)
                    {
                        if (_pruebaPerfil.Adicionar(ID_PERFIL, Convert.ToDecimal(p.IDPRUEBA), conexion) == false)
                        {
                            MensajeError = _pruebaPerfil.MensajeError;
                            return -1;
                        }
                    }
                }
            }

            return ID_VERSIONAMIENTO;
        }


        public Boolean Actualizar(Decimal REGISTRO,
            Decimal ID_EMPRESA,
            String EDAD_MIN,
            String EDAD_MAX,
            String SEXO,
            String EXPERIENCIA,
            String NIV_ESTUDIOS,
            List<documentoPerfil> documentos,
            List<pruebaPerfil> pruebas,
            String OBSERVACIONES_ESPECIALES,
            Boolean actualizaTipoEntrevista, 
            String TIPO_ENTREVISTA,
            Decimal ID_CATEGORIA_REFERENCIA,
            Decimal ID_ASSESMENT_CENTER,
            Boolean ESTADO,
            String NIVEL_REQUERIMIENTO)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            Boolean continuarNormalmente = true;
            ManualServicio _manual = new ManualServicio(Empresa, Usuario);

            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion); 

                DataTable tablaDatosAnteriores = _manual.ObtenerInfoRegistroTabla(diccionarioCamposPerfil, "VEN_D_PERFILES", "REGISTRO", REGISTRO.ToString(), conexion);

                if (ActualizarVenDPerfil(REGISTRO, ID_EMPRESA, EDAD_MIN, EDAD_MAX, SEXO, EXPERIENCIA, NIV_ESTUDIOS, OBSERVACIONES_ESPECIALES, TIPO_ENTREVISTA, ESTADO, ID_CATEGORIA_REFERENCIA, ID_ASSESMENT_CENTER, NIVEL_REQUERIMIENTO, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    DataTable tablaDatosNuevos = _manual.ObtenerInfoRegistroTabla(diccionarioCamposPerfil, "VEN_D_PERFILES", "REGISTRO", REGISTRO.ToString(), conexion);

                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.DeterminarYRegistrarModificacionesEnCampos(diccionarioCamposPerfil, tablaDatosAnteriores, tablaDatosNuevos, ID_VERSIONAMIENTO, ID_EMPRESA, ManualServicio.ListaSecciones.Seleccion, ManualServicio.AccionesManual.Actualizar, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                        }
                    }

                    if (continuarNormalmente == true)
                    {
                        ID_VERSIONAMIENTO = ActualizarDocumentosDePerfil(REGISTRO, documentos, realizarVersionamientoManual, ID_VERSIONAMIENTO, _manual, ID_EMPRESA, conexion);

                        if (ID_VERSIONAMIENTO == -1)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                        }

                        if (continuarNormalmente == true)
                        {
                            ID_VERSIONAMIENTO = ActualizarPruebasPerfil(REGISTRO, pruebas, realizarVersionamientoManual, ID_VERSIONAMIENTO, _manual, ID_EMPRESA, conexion);

                            if (ID_VERSIONAMIENTO == -1)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                continuarNormalmente = false;
                                ID_VERSIONAMIENTO = 0;
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public DataTable ObtenerPorRegistro(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_perfiles_obtener_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_D_PERFILES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataTable ObtenerPorRegistroConinfoOcupacion(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_perfiles_obtener_por_registro_con_info_ocupacion ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser nulo\n";
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



        public DataTable ObtenerTodasLasOcupacionesPorIdEmpresa(int ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_perfiles_obtenerTodasLasOcupacionesPorIdEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_D_PERFILES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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



        public DataTable ObtenerPerfilesPorEmpresaYSexo(Decimal ID_EMPRESA, String SEXO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_perfiles_obtener_por_id_empresa_sexo ";

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

            if (String.IsNullOrEmpty(SEXO) == false)
            {
                sql += "'" + SEXO + "'";
                informacion += "SEXO = '" + SEXO + "'";
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


        public DataTable ObtenerVenDPerfilesConOcupacionPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_perfiles_obtener_con_ocupacion_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_D_PERFILES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerVenDPerfilesConNomOcupacionDocumentosYPruebasPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_perfiles_obtener_con_ocupacion_con_documentos_y_con_pruebas_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
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


        public DataTable ObtenerVenDPerfilesConNomOcupacionExamenesDocumentosYRequermientosPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_obtener_por_IdEmpresa_con_examenes_doc_entregados_y_requerimientos ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
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





        public DataTable ObtenerVenDPerfilesConOcupacionPorIdEmpresaYNomOcupacion(Decimal ID_EMPRESA,
            String NOM_OCUPACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_perfiles_obtener_con_ocupacion_por_id_empresa_nom_ocupacion ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOM_OCUPACION) == false)
            {
                sql += "'" + NOM_OCUPACION + "'";
            }
            else
            {
                MensajeError += "El campo NOM_OCUPACION no puede ser nulo\n";
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




        public DataTable ObtenerVenDPerfilesConOcupacionPorIdEmpresaYIdOcupacion(Decimal ID_EMPRESA,
            Decimal ID_OCUPACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_perfiles_obtener_con_ocupacion_por_id_empresa_id_cargo ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_OCUPACION != 0)
            {
                sql += ID_OCUPACION;
            }
            else
            {
                MensajeError += "El campo ID_OCUPACION no puede ser nulo\n";
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


        public DataTable ObtenerVenDPerfilesConOcupacionPorIdEmpresa(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_d_perfiles_obtener_con_ocupacion_por_id_empresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

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

        public DataTable ObtenerTodasLasOcupacionesPorIdEmpresaYNomOcupacion(int ID_EMPRESA, String nomOcupacion)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_rec_ocupaciones_obtenerTodasLasOcupacionesPorIdEmpresaYNombreOcupacion ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(nomOcupacion) == false)
            {
                sql += "'" + nomOcupacion + "'";
            }
            else
            {
                MensajeError += "El campo NOM_OCUPACION no puede ser nulo\n";
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





        #region VEN_D_CALIFICACIONES_ENTREVISTA_PERFIL
        #endregion VEN_D_CALIFICACIONES_ENTREVISTA_PERFIL

        #region sel_reg_aplicacion_competencias
        #endregion sel_reg_aplicacion_competencias


        #endregion metodos
    }
}
