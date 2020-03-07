using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Mantenimiento.DTO
{
    public class DoctorDto
    {
        public int Id { get; set; }


        [StringLength(15)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Este campo es necesario")]
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public IFormFile Photo { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
    }
}
