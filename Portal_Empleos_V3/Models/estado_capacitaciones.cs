﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    [Table("estado_capacitaciones")]
    public class estado_capacitaciones
    {
        [Key]
        [Column(Order = 1)]
        public int id_estado_capacitacion { get; set; }

        public string nombre_estado_capacitacion { get; set; }
    }
}