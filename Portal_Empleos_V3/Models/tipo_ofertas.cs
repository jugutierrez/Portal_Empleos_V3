using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("tipo_ofertas")]
    public class tipo_ofertas
    {
        [Key]
        [Column(Order = 1)]
        public int id_tipo_oferta { get; set; }

        public string nombre_tipo_oferta { get; set; }
    }
}