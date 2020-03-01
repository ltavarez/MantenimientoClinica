using System;
using System.Collections.Generic;

namespace Mantenimiento.Models
{
    public partial class Visits
    {
        public int VisitId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? VisitedAt { get; set; }
        public string Phone { get; set; }
        public int StoreId { get; set; }
    }
}
