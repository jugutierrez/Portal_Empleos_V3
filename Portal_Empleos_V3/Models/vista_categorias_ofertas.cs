﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class vista_categorias_ofertas
    {
        [Key]
        [Column(Order = 1)]
        public int id_oferta_categoria { get; set; }

        public string nombre_categoria { get; set; }

      
    }
}