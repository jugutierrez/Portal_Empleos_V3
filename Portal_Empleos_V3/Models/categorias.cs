using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{

    [Table("categorias")]
    public class categorias
    {
        [Key]
        [Column(Order = 1)]
        public int id_categoria { get; set; }

        public string nombre_categoria { get; set; }
    }
}