using System;
using System.Collections.Generic;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.seleccion
{
    public class ExperienciaLaboral
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;

        private Decimal _id_experiencia = 0;
        private Decimal _registro_entrevista = 0;
        private String _empresa_cliente = null;
        private String _cargo = null;
        private String _funciones = null;
        private DateTime _fecha_ingreso = new DateTime();
        private DateTime _fecha_retiro = new DateTime();
        private String _motivo_retiro = null;
        private Decimal _ultimo_salario = 0;
        private Boolean _activo = false;
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
        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }


        public Decimal ID_EXPERIENCIA
        {
            get { return _id_experiencia; }
            set { _id_experiencia = value; }
        }

        public Decimal REGISTRO_ENTREVISTA
        {
            get { return _registro_entrevista; }
            set { _registro_entrevista = value; }
        }

        public String EMPRESA_CLIENTE
        {
            get { return _empresa_cliente; }
            set { _empresa_cliente = value; }
        }

        public String CARGO
        {
            get { return _cargo; }
            set { _cargo = value; }
        }

        public String FUNCIONES
        {
            get { return _funciones; }
            set { _funciones = value; }
        }

        public DateTime FECHA_INGRESO
        {
            get { return _fecha_ingreso; }
            set { _fecha_ingreso = value; }
        }

        public DateTime FECHA_RETIRO
        {
            get { return _fecha_retiro; }
            set { _fecha_retiro = value; }
        }

        public String MOTIVO_RETIRO
        {
            get { return _motivo_retiro; }
            set { _motivo_retiro = value; }
        }

        public Decimal ULTIMO_SALARIO
        {
            get { return _ultimo_salario; }
            set { _ultimo_salario = value; }
        }

        public Boolean ACTIVO
        {
            get { return _activo; }
            set { _activo = value; }
        }
        #endregion propiedades

        #region constructores
        public ExperienciaLaboral()
        {

        }

        public ExperienciaLaboral(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region METODOS
        #endregion METODOS
    }
}
