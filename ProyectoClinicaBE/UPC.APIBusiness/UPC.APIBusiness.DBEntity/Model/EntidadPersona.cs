using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntidadPersona : EntityBase
    {
        public int CoDNI { get; set; }
        public string NoNombres { get; set; }
        public string NoApellidos { get; set; }
        public string TxCorreo { get; set; }
        public string NuTelefono { get; set; }
        public string TxPassword { get; set; }
        public DateTime FeNacimiento { get; set; }
    }
}
