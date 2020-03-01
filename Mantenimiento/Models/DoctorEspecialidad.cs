﻿using System;
using System.Collections.Generic;

namespace Mantenimiento.Models
{
    public partial class DoctorEspecialidad
    {
        public int IdDoctor { get; set; }
        public int IdEspecialidad { get; set; }

        public virtual Doctor IdDoctorNavigation { get; set; }
        public virtual Especialidad IdEspecialidadNavigation { get; set; }
    }
}
