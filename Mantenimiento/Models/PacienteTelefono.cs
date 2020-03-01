using System;
using System.Collections.Generic;

namespace Mantenimiento.Models
{
    public partial class PacienteTelefono
    {
        public int IdPaciente { get; set; }
        public string Telefonos { get; set; }

        public virtual Paciente IdPacienteNavigation { get; set; }
    }
}
