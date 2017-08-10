using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("ciudades")]
    public class ciudades
   {
        [Key]
        [Column(Order = 1)]
        public int id_ciudad { get; set; }

        public string nombre_ciudad { get; set; }
        public int id_region { get; set; }

    }
}