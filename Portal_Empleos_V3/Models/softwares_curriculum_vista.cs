using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class softwares_curriculum_vista
    {
        [Key]
        [Column(Order = 1)]

        public int id_software_curriculum { get; set; }

        public string nombre_software { get; set; }

        public string nombre_grado_conocimiento_software { get; set; }
    }
}