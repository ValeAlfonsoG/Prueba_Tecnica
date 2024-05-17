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
    public class MateriaController : Controller
    {
        private Prueba_ColegiosEntities6 db = new Prueba_ColegiosEntities6();
        private Prueba_ColegiosEntities5 db2 = new Prueba_ColegiosEntities5();


        // GET: Materia
        public ActionResult Index()
        {
            string Usuario = (string)TempData["Usuario"];
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            var profesor = db.Profesor.SingleOrDefault(p => p.Usuario == Usuario);
            if (profesor == null)
            {
                return HttpNotFound("No se encontró un profesor con el usuario proporcionado.");
            }

            var materia = db.Materia.Include(m => m.Profesor).Where(m => m.Id_Profesor == profesor.Id); ;
            return View(materia.ToList());
        }

        public ActionResult Index2()
        {
            string Usuario = (string)TempData["Usuario"];
            TempData["Usuario"] = Usuario;
            TempData.Keep("Usuario");
            var estudiante = db2.Estudiante.SingleOrDefault(e => e.Usuario == Usuario);
            if (estudiante == null)
            {
                return HttpNotFound("No se encontró un estudiante con el usuario proporcionado.");
            }
            var estudiante_materia = db2.Estudiante_Materia.Where( em => em.Id_Estudiante == estudiante.Id);
            var materia = db.Materia.Include(m => m.Profesor);//.Where( m => m.Id = estudiante_materia.Id_); 
            return View(materia.ToList());
        }

        // GET: Materia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materia materia = db.Materia.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        // GET: Materia/Create
        public ActionResult Create()
        {
            ViewBag.Id_Profesor = new SelectList(db.Profesor, "Id", "Nombre");
            return View();
        }

        // POST: Materia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Id_Profesor,Nombre,Grado,Curso")] Materia materia)
        {
            if (ModelState.IsValid)
            {
                db.Materia.Add(materia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Profesor = new SelectList(db.Profesor, "Id", "Nombre", materia.Id_Profesor);
            return View(materia);
        }

        // GET: Materia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materia materia = db.Materia.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Profesor = new SelectList(db.Profesor, "Id", "Nombre", materia.Id_Profesor);
            return View(materia);
        }

        // POST: Materia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Id_Profesor,Nombre,Grado,Curso")] Materia materia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Profesor = new SelectList(db.Profesor, "Id", "Nombre", materia.Id_Profesor);
            return View(materia);
        }

        // GET: Materia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Materia materia = db.Materia.Find(id);
            if (materia == null)
            {
                return HttpNotFound();
            }
            return View(materia);
        }

        // POST: Materia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Materia materia = db.Materia.Find(id);
            db.Materia.Remove(materia);
            db.SaveChanges();
            return RedirectToAction("Index");
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
