using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class vista_habilidades_ofertas
    {
        [Key]
        [Column(Order = 1)]
        public int id_especificacion_oferta_habilidad { get; set; }
      
        public string nombre_habilidad { get; set; }
  
        public string nombre_grado_habilidad { get; set; }

        public string  nombre_nivel_importancia { get; set; }
    }
}