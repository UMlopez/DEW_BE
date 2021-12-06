using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntidadMedico : EntityBase
    {
        public int CoColegiatura { get; set; }
        public string NoNombre { get; set; }
        public string NoApellido { get; set; }
        public int CoEspecialidad { get; set; }
    }
}
