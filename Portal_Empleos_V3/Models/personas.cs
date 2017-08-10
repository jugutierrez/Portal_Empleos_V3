using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Portal_Empleos_V3.Models;

namespace Portal_Empleos_V3.Models
{
    [Table("personas")]
    public class personas
    {
        [Key]
        [Column(Order = 1)]
        public int id_persona { get; set; }

        public string nombre_persona { get; set; }

        public string apellido_paterno_persona { get; set; }

        public string apellido_materno_persona { get; set; }

        public string correo_electronico_persona { get; set; }

        public DateTime fecha_nacimiento_persona { get; set; }

        public DateTime fecha_creacion_persona { get; set; }

        public DateTime fecha_modificacion_persona { get; set; }

        public string identificacion_persona { get; set; }

        public string clave_persona { get; set; }

        public int id_tipo_persona { get; set; }

        public int id_estado_persona { get; set; }

        public int id_curriculum { get; set; }

        public int id_comuna { get; set; }

        public int id_tipo_identificacion_persona { get; set; }



    }
    public class PersonaDBContext : DbContext
    {
        public DbSet<personas> personas { get; set; }
        public DbSet<curriculums> curriculums { get; set; }
        public DbSet<experiencias_laborales_curriculum_vista> experiencias_laborales_curriculum_vista { get; set; }
        public DbSet<estudios_curriculum_vista> estudios_curriculum_vista { get; set; }
        public DbSet<tipo_identificacion_personas> tipo_identificacion_personas { get; set; }
        public DbSet<categorias> categorias { get; set; }
        public DbSet<jornada_ofertas> jornada_ofertas { get; set; }
        public DbSet<tipo_ofertas> tipo_ofertas { get; set; }
        public DbSet<contrato_ofertas> contrato_ofertas { get; set; }
        public DbSet<habilidades> habilidades { get; set; }
        public DbSet<grado_habilidades> grado_habilidades { get; set; }
        public DbSet<idiomas> idiomas { get; set; }
        public DbSet<nivel_idiomas> nivel_idiomas { get; set; }
        public DbSet<profesiones> profesiones { get; set; }
        public DbSet<softwares> softwares { get; set; }
        public DbSet<grado_conocimiento_softwares> grado_conocimiento_softwares { get; set; }
        public DbSet<instituciones> instituciones { get; set; }
        public DbSet<capacitaciones> capacitaciones { get; set; }
        public DbSet<estado_capacitaciones> estado_capacitaciones { get; set; }
        public DbSet<estudios> estudios { get; set; }
        public DbSet<tipo_estudios> tipo_estudios { get; set; }
        public DbSet<estado_estudios> estado_estudios { get; set; }
        public DbSet<cargo_experiencias_laborales> cargo_experiencias_laborales { get; set; }
        public DbSet<area_experiencias_laborales> area_experiencias_laborales { get; set; }
        public DbSet<estado_postulaciones> estado_postulaciones { get; set; }
        public DbSet<regiones> regiones { get; set; }
        public DbSet<discapacidad_personas> discapacidad_personas { get; set; }
        public DbSet<tipo_personas> tipo_personas { get; set; }
        public DbSet<ciudades> ciudades { get; set; }
        public DbSet<comunas> comunas { get; set; }
        public DbSet<vista_preguntas_faq> vista_preguntas_faq { get; set; }
        public DbSet<vista_categorias_faq> vista_categorias_faq { get; set; }
    }
}