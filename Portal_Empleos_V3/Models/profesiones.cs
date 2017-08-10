using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("profesiones")]
    public class profesiones
    {
        [Key]
        [Column(Order = 1)]

        public int id_profesion { get; set; }

        public string nombre_profesion { get; set; }
    }
}