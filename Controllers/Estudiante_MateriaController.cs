using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prueba_Colegios.Models;

namespace Prueba_Colegios.Controllers
{
    public class Estudiante_MateriaController : Controller
    {
        private Prueba_ColegiosEntities5 db = new Prueba_ColegiosEntities5();

        // GET: Estudiante_Materia
        public ActionResult Index(int id)
        {
            var estudiante_Materia = db.Estudiante_Materia.Include(e => e.Estudiante).Where(e => e.Id_Materia == id);
            return View(estudiante_Materia.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Estudiante,Id_Materia")] Estudiante_Materia estudiante_materia, int id)
        {
            estudiante_materia.Id_Materia = id;
            string Usuario = (string)TempData["Usuario"];
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            var IdE = db.Estudiante.SingleOrDefault(p => p.Usuario == Usuario);
            if (IdE == null)
            {
                return HttpNotFound("No se encontró un estudiante con el usuario proporcionado.");
            }
            estudiante_materia.Id_Estudiante = IdE.Id;
            if (ModelState.IsValid)
            {
                db.Estudiante_Materia.Add(estudiante_materia);
                db.SaveChanges();
                return RedirectToAction("Index2", "Materia");
            }
            return View("Index2", "Materia");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
