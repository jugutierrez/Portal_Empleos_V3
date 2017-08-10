using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class vista_listado_postulaciones
    {

        [Key]
        [Column(Order = 1)]
        public int id_oferta { get; set; }

        public string  nombre_oferta { get; set; }

        public DateTime fecha_postulacion { get; set; }

        public int id_estado_postulacion { get; set; }

        public int cantidad_postulantes { get; set; }
    }
}