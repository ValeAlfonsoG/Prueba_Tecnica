using Prueba_Colegios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Prueba_Colegios.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private Prueba_ColegiosEntities dbP = new Prueba_ColegiosEntities();
        private Prueba_ColegiosEntities1 dbE = new Prueba_ColegiosEntities1();

        // GET: Login
        public ActionResult Index( string Usuario, string Contraseña)
        {
            // Verificar si los datos de inicio de sesión existen en la tabla de Profesores
            var Profesor = dbP.Profesor.FirstOrDefault( p => p.Usuario == Usuario && p.Contraseña == Contraseña);

            if (Profesor != null)
            {
                // Usuario válido, asignar el valor del usuario a ViewBag y redireccionar al layout de profesor
                TempData["Usuario"] = Usuario;
                TempData.Keep("Usuario");
                return RedirectToAction("Index", "Home");

            }

            // Verificar si los datos de inicio de sesión existen en la tabla de Estudiantes
            var Estudiante = dbE.Estudiante.FirstOrDefault(a => a.Usuario == Usuario && a.Contraseña == Contraseña);

            if (Estudiante != null)
            {
                // usuario válido, asignar el valor del usuario a ViewBag y redireccionar al layout de estudiante
                TempData["Usuario"] = Usuario;
                TempData.Keep("Usuario");
                return RedirectToAction("Index2", "Home");
            }

            // Datos de inicio de sesión inválidos o cédula no proporcionada, mostrar mensaje de error
            ModelState.AddModelError("", "Usuario o contraseña incorrecta.");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbE.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}