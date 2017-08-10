using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal_Empleos_V3.Models
{
    public class modelo_descarga_curriculum
    {
     

        public List<capacitaciones_curriculum_vista> capacitaciones_curiculums_vista { get; set; }

        public List<habilidades_curriculum_vista> habilidades_curriculums_vista { get; set; }

        public curriculums curriculums { get; set; }

        public List<curriculums> curriculums_lista { get; set; }

        public persona_curriculum_vista persona_curriculum_vista { get; set; }

        public List<experiencias_laborales_curriculum_vista> experiencias_laborales_curriculums_vista { get; set; }

        public List<estudios_curriculum_vista> estudios_curriculums_vista { get; set; }

        public List<idiomas_curriculum_vista> idiomas_curriculums_vista { get; set; }

        public List<softwares_curriculum_vista> softwares_curriculums_vista { get; set; }

        public List<vista_documentos_curriculum> documentos_curriculums { get; set; }
    }


  

    public class super_modelo_curriculum : modelo_descarga_curriculum
    {

    }
}