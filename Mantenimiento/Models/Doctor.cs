using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mantenimiento.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            DoctorEspecialidad = new HashSet<DoctorEspecialidad>();
        }

        public int Id { get; set; }

        
        [StringLength(15)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Este campo es necesario")]
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string ProfilePhoto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<DoctorEspecialidad> DoctorEspecialidad { get; set; }
    }
}
