using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("grado_conocimiento_softwares")]
    public class grado_conocimiento_softwares
    {
        [Key]
        [Column(Order = 1)]
        public int id_grado_conocimiento_software { get; set; }

        public string nombre_grado_conocimiento_software { get; set; }
    }
}