using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("tipo_estudios")]
    public class tipo_estudios
    {
        [Key]
        [Column(Order = 1)]

        public int  id_tipo_estudio { get; set; }

        public string nombre_tipo_estudio { get; set; }
    }
}