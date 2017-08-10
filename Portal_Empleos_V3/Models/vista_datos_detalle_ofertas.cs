using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class vista_datos_detalle_ofertas
    {
       
            [Key]
            [Column(Order = 1)]
            public int id_oferta { get; set; }

            public string nombre_oferta { get; set; }

            public string descripcion_oferta { get; set; }

            public DateTime fecha_creacion_oferta { get; set; }
   
            public int monto_oferta { get; set; }

            public string   nombre_tipo_oferta { get; set; }
           
            public string nombre_jornada_oferta { get; set; }

            public string nombre_contrato_oferta { get; set; }

            public string nombre_area { get; set; }

            public string nombre_departamento { get; set; }

            public string nombre_direccion { get; set; }

        public int oferta_inclusiva { get; set; }







    }
}