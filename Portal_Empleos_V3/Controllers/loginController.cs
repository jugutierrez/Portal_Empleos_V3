
using Portal_Empleos_V3.Models;
using Portal_Empleos_V3.ModelView;
using Portal_Empleos_V3.seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Portal_Empleos_V3.Controllers
{
    public class loginController : Controller
    {
        PersonaDBContext db = new PersonaDBContext();
        // GET: /Login/

        public ActionResult Index()
        {
            SessionPersister.username = string.Empty;
            Session.Clear();
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            return View();

        }

        public ActionResult Login(personas cv)
        {
            modelo_cuentas cv1 = new modelo_cuentas();
            if (string.IsNullOrEmpty(cv.correo_electronico_persona) || string.IsNullOrEmpty(cv.clave_persona) || cv1.login(cv.correo_electronico_persona, cv.clave_persona) == null)
            {
                ViewBag.Error = "cuenta invalida";
                ModelState.AddModelError("", "El Usuario o la Contraseña no Coinciden.");
                return View("index");
            }
           
            var k = db.Database.SqlQuery<datos_session>("select id_persona , id_curriculum from personas where correo_electronico_persona = {0}", cv.correo_electronico_persona).Single();
            Session["persona_id"] = k.id_persona;
            Session["curriculum_id"] = k.id_curriculum;
            SessionPersister.username = cv.correo_electronico_persona;
            return RedirectToAction("index", "Curriculum_mant");
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
           
            SessionPersister.username = string.Empty;
            Session.Clear();
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            
            }
            return RedirectToAction("Index", "inicio");
        }



       

        public ActionResult recordar_credenciales()
        {
            return PartialView("_recuperar_credenciales");
        }


        public ActionResult vista_registro()
        {

            return PartialView("_registrarse");
        }
    }
}