using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class persona_curriculum_vista
    {
        [Key]
        [Column(Order = 1)]
        public int id_persona { get; set; }
        [Required]
        public string nombre_persona { get; set; }

        public string apellido_paterno_persona { get; set; }

        public string apellido_materno_persona { get; set; }

        public string correo_electronico_persona { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime fecha_nacimiento_persona { get; set; }

        public string identificacion_persona { get; set; }

        public string digito_verificador_identificacion_persona { get; set; }

        public string clave_persona { get; set; }

        public int id_tipo_persona { get; set; }
        public string nombre_tipo_persona { get; set; }


        public int id_comuna { get; set; }
        public string nombre_comuna { get; set; }
  

        public int id_ciudad { get; set; }
        public string nombre_ciudad { get; set; }
    

        public int id_region { get; set; }
        public string nombre_region { get; set; }
   

        public int id_tipo_identificacion_persona { get; set; }
        public string nombre_tipo_identificacion_persona { get; set; }
    




        public string sueldo_esperado { get; set; }

        public int id_curriculum { get; set; }

        public string direccion_curriculum { get; set; }

        public string telefono_curriculum_1 { get; set; }

        public string telefono_curriculum_2 { get; set; }




    }
}