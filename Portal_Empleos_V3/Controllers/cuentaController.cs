using Portal_Empleos_V3.Models;
using Portal_Empleos_V3.seguridad;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal_Empleos_V3.Controllers
{
    public class cuentaController : Controller
    {
        private PersonaDBContext db = new PersonaDBContext();
        //mail ms = new mail();
        [CustomAuthorize]
        public ActionResult Index()
        {
            try
            {
                if (Session.Contents.Count < 1)
                {
                    return RedirectToAction("LogOff", "login");
                }
                ViewBag.id_region = new SelectList(db.regiones, "id_region", "nombre_region");


                ViewBag.id_comuna = new SelectList(db.comunas, "id_comuna", "nombre_comuna");

                ViewBag.id_ciudad = new SelectList(db.ciudades, "id_ciudad", "nombre_ciudad");
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }

        }

        [CustomAuthorize]
        public PartialViewResult cuenta_cambio_contraseña(int? id)
        {
            ViewBag.id_persona = id;
            return PartialView("cuenta_actualizar/_cuenta_actualizar_contraseña");
        }

        [CustomAuthorize]
        public PartialViewResult cuenta_desactiva_cuenta(int? id)
        {
            ViewBag.id_persona = id;
            return PartialView("cuenta_desactivar/_cuenta_desactivar_cuenta");
        }


        public JsonResult obtener_ciudades(string ciudadId)
        {
            try
            {
                int Id = Convert.ToInt32(ciudadId);
                List<ciudades> results = db.Database.SqlQuery<ciudades>("Exec sp_obtener_ciudades @id_region = {0}  ", Id).ToList();
                return Json(results, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult obtener_comunas(string comunaId)
        {
            try
            {
                int Id = Convert.ToInt32(comunaId);


                //var states = from a in db.ciudades where a.id_region == Id select a;
                List<comunas> results2 = db.Database.SqlQuery<comunas>("Exec sp_obtener_comunas @id_ciudad = {0}  ", Id).ToList();
                return Json(results2, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult agregar_cuenta_persona(modelo_agregar_cuenta_persona datos_cuenta_personas)
        {
            try
            {
                db.Database.ExecuteSqlCommand("exec sp_inserta_persona_curriculum  @nombre_persona  = {0}, @apellido_paterno_persona = {1} ,@apellido_materno_persona = {2} , " +
                "@identificacion_persona ={3} , @digito_verificador_identificacion_persona = {4}, @correo_electronico_persona  = {5}, @fecha_nacimiento_persona = {6} , @clave_persona = {7} ," +
                " @id_comuna = {8} , @id_tipo_identificacion_persona  = {9}, @id_tipo_persona = {10}, @direccion_curriculum = {11} ,@telefono_curriculum_1  ={12}, " +
                " @telefono_curriculum_2 = {13},@sueldo_esperado = {14}", datos_cuenta_personas.nombre_persona, datos_cuenta_personas.apellido_paterno_persona, datos_cuenta_personas.apellido_materno_persona,
               datos_cuenta_personas.identificacion_persona, datos_cuenta_personas.digito_verificador_identificacion_persona, datos_cuenta_personas.correo_electronico_persona,
               datos_cuenta_personas.fecha_nacimiento_persona, datos_cuenta_personas.clave_persona_1, datos_cuenta_personas.id_comuna, datos_cuenta_personas.id_tipo_identificacion_persona,
               datos_cuenta_personas.id_tipo_persona, datos_cuenta_personas.direccion_curriculum, datos_cuenta_personas.telefono_curriculum_1,
               datos_cuenta_personas.telefono_curriculum_2, datos_cuenta_personas.sueldo_esperado);

               // ms.enviar_correo(null, datos_cuenta_personas.correo_electronico_persona, 0);

                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult recordar_cuenta_persona(modelo_recordar_cuenta_persona recordar_cuenta_personas)
        {
            try
            {

                // ms.enviar_correo(null, recordar_cuenta_personas.correo_electronico_persona, 0);

                // ms.Correo_recupera_credenciales(recordar_cuenta_personas.correo_electronico_persona,recordar_cuenta_personas.identificacion_persona,recordar_cuenta_personas.digito_identificacion_persona,"juan","123123");

                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
        [CustomAuthorize]
        public JsonResult actualiza_pass(modelo_actualiza_pass actualiza_pass)
        {
            try
            {
                if (actualiza_pass.clave_usuario_1.Length > 8)
                {
                    if (actualiza_pass.clave_usuario_1 == actualiza_pass.clave_usuario_2)
                    {

                        db.Database.ExecuteSqlCommand("UPDATE personas SET clave_persona = {0}  WHERE id_persona = {1}", actualiza_pass.clave_usuario_1, Convert.ToInt32(Session["persona_id"]));
                        return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
        [CustomAuthorize]
        public JsonResult desactiva_cuenta()
        {
            try

            {


                return Json(new { success = true, respuesta = new { caca = "caca" } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }


    }
}