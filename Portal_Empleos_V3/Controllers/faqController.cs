using Portal_Empleos_V3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal_Empleos_V3.Controllers
{
    public class faqController : Controller
    {
        private PersonaDBContext db = new PersonaDBContext();
        // GET: faq
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult listar_faq()
        {



            List<vista_preguntas_faq> p_f = db.vista_preguntas_faq.ToList();

            return Json(new { success = true, pregunta_f = p_f }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult listar_categorias_faq()
        {

            List<vista_categorias_faq> p_f = db.vista_categorias_faq.ToList();

            return Json(new { success = true, pregunta_f_c = p_f }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult rellenar_regiones()
        {
            List<regiones> k = db.regiones.ToList();
            return Json(new { success = true, regiones = k }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult rellenar_ciudades(int id)
        {
            List<ciudades> ke = db.Database.SqlQuery<ciudades>("select * from ciudades where id_region = {0}", id).ToList();
            return Json(new { success = true, ciudades = ke }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult rellenar_comunas(int id)
        {
            List<comunas> k = db.Database.SqlQuery<comunas>("select * from comunas where id_ciudad = {0}", id).ToList();
            return Json(new { success = true, comunas = k }, JsonRequestBehavior.AllowGet);
        }

    }
}