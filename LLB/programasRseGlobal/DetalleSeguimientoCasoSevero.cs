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
    public class DetalleSeguimientoCasoSevero
    {
        #region variables
        private Decimal _id_detalle_seguimiento = 0;
        private Decimal _id_maestra_caso_severo = 0;
        private DateTime _fecha_seguimiento = new DateTime();
        private String _seguimiento = null;
        private Boolean _genera_compromiso = false;
        private Decimal _id_compromiso_generado = 0;
        private String _encargado_compromiso = null;
        private String _obsevaciones_compromiso = null;
        private DateTime _fecha_compromiso = new DateTime();

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

        public Decimal ID_DETALLE_SEGUIMIENTO
        {
            get { return _id_detalle_seguimiento; }
            set { _id_detalle_seguimiento = value; }
        }

        public Decimal ID_MAESTRA_CASO_SEVERO
        {
            get { return _id_maestra_caso_severo; }
            set { _id_maestra_caso_severo = value; }
        }

        public DateTime FECHA_SEGUIMIENTO
        {
            get { return _fecha_seguimiento; }
            set { _fecha_seguimiento = value; }
        }

        public String SEGUIMIENTO
        {
            get { return _seguimiento; }
            set { _seguimiento = value; }
        }

        public Boolean GENERA_COMPROMISO
        {
            get { return _genera_compromiso; }
            set { _genera_compromiso = value; }
        }

        public Decimal ID_COMPROMISO_GENERADO
        {
            get { return _id_compromiso_generado; }
            set { _id_compromiso_generado = value; }
        }

        public String ENCARGADO_COMPROMISO
        {
            get { return _encargado_compromiso; }
            set { _encargado_compromiso = value; }
        }

        public String OBSERVACIONES
        {
            get { return _obsevaciones_compromiso; }
            set { _obsevaciones_compromiso = value; }
        }

        public DateTime FECHA_COMPROMISO
        {
            get { return _fecha_compromiso; }
            set { _fecha_compromiso = value; }
        }
        #endregion propiedades

        #region constructores
        public DetalleSeguimientoCasoSevero()
        {

        }
        public DetalleSeguimientoCasoSevero(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        #endregion metodos

    }
}
