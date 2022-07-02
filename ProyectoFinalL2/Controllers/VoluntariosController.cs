using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinalL2.Models;

namespace ProyectoFinalL2.Controllers
{
    public class VoluntariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Voluntarios
        public async Task<ActionResult> Index()
        {
            var voluntarios = db.Voluntarios.Include(v => v.Mascotas);
            return View(await voluntarios.ToListAsync());
        }

        // GET: Voluntarios/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voluntario voluntario = await db.Voluntarios.FindAsync(id);
            if (voluntario == null)
            {
                return HttpNotFound();
            }
            return View(voluntario);
        }

        // GET: Voluntarios/Create
        public ActionResult Create()
        {
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre");
            return View();
        }

        // POST: Voluntarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<ActionResult> Create([Bind(Include = "VoluntarioID,NombreV,CedulaV,Correo,MascotasID")] Voluntario voluntario)
        {
            if (ModelState.IsValid)
            {
                db.Voluntarios.Add(voluntario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre", voluntario.MascotasID);
            return View(voluntario);
        }

        // GET: Voluntarios/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voluntario voluntario = await db.Voluntarios.FindAsync(id);
            if (voluntario == null)
            {
                return HttpNotFound();
            }
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre", voluntario.MascotasID);
            return View(voluntario);
        }

        // POST: Voluntarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "VoluntarioID,NombreV,CedulaV,Correo,MascotasID")] Voluntario voluntario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voluntario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MascotasID = new SelectList(db.Mascotas, "MascotasID", "Nombre", voluntario.MascotasID);
            return View(voluntario);
        }

        // GET: Voluntarios/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voluntario voluntario = await db.Voluntarios.FindAsync(id);
            if (voluntario == null)
            {
                return HttpNotFound();
            }
            return View(voluntario);
        }

        // POST: Voluntarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Voluntario voluntario = await db.Voluntarios.FindAsync(id);
            db.Voluntarios.Remove(voluntario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
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
