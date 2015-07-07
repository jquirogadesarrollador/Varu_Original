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
    public class Asistencia
    {
        #region variables


        private Decimal _id_emplado = 0;
        private Decimal _id_detalle = 0;
        private Decimal _id_solicitud = 0;

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        #endregion variables

        #region propiedades

        public Decimal ID_EMPLEADO
        {
            get { return _id_emplado; }
            set { _id_emplado = value; }
        }

        public Decimal ID_DETALLE
        {
            get { return _id_detalle; }
            set { _id_detalle = value; }
        }

        public Decimal ID_SOLICITUD
        {
            get { return _id_solicitud; }
            set { _id_solicitud = value; }
        }

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
        public Asistencia()
        {

        }
        public Asistencia(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        #endregion metodos

    }
}
