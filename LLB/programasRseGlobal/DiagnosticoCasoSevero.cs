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
    public class DiagnosticoCasoSevero
    {
        #region variables
        private Decimal _id_diagnostico = 0;
        private Decimal _id_maestra_caso_severo = 0;
        private Decimal _registro_diagnostico = 0;
        private String _dsc_diag = null;
        private String _clase_diagnostico = null;

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

        public Decimal ID_DIAGNOSTICO
        {
            get { return _id_diagnostico; }
            set { _id_diagnostico = value; }
        }
        public Decimal ID_MAESTRA_CASO_SEVERO
        {
            get { return _id_maestra_caso_severo; }
            set { _id_maestra_caso_severo = value; }
        }
        public Decimal REGISTRO_DIAGNOSTICO
        {
            get { return _registro_diagnostico; }
            set { _registro_diagnostico = value; }
        }
        public String DSC_DIAG
        {
            get { return _dsc_diag; }
            set { _dsc_diag = value; }
        }
        public String CLASE_DIAGNOSTICO
        {
            get { return _clase_diagnostico; }
            set { _clase_diagnostico = value; }
        }
        #endregion propiedades

        #region constructores
        public DiagnosticoCasoSevero()
        {

        }
        public DiagnosticoCasoSevero(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos

    }
}
