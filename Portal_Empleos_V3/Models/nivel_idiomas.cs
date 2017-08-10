using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{

    [Table("nivel_idiomas")]
    public class nivel_idiomas
    {
        [Key]
        [Column(Order = 1)]

        public int id_nivel_idioma { get; set; }

        public string nombre_nivel_idioma { get; set; }
    }
}