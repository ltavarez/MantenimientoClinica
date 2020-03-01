using System;
using System.Collections.Generic;

namespace Mantenimiento.Models
{
    public partial class Especialidad
    {
        public Especialidad()
        {
            DoctorEspecialidad = new HashSet<DoctorEspecialidad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<DoctorEspecialidad> DoctorEspecialidad { get; set; }
    }
}
