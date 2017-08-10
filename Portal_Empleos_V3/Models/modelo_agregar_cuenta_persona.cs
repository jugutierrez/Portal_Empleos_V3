using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_agregar_cuenta_persona

    {



  
        public string nombre_persona { get; set; }

        public string apellido_paterno_persona { get; set; }

        public string apellido_materno_persona { get; set; }

        public string correo_electronico_persona { get; set; }

        public DateTime fecha_nacimiento_persona { get; set; }

        public string identificacion_persona { get; set; }

        public string digito_verificador_identificacion_persona { get; set; }

        public string clave_persona_1 { get; set; }

        public string clave_persona_2 { get; set; }

        public string id_tipo_persona { get; set; }

        public string id_comuna { get; set; }
  
        public string id_tipo_identificacion_persona { get; set; }


        public string id_discapacidad_persona { get; set; }


        public string sueldo_esperado { get; set; }

 

        public string direccion_curriculum { get; set; }

        public string telefono_curriculum_1 { get; set; }

        public string telefono_curriculum_2 { get; set; }


    }
}