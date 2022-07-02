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
using PagedList;
using PagedList.Mvc;


namespace ProyectoFinalL2.Controllers
{
    public class MascotasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var mascotas = from s in db.Mascotas
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                mascotas = mascotas.Where(s => s.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    mascotas = mascotas.OrderByDescending(s => s.Nombre);

                    break;
                default:
                    mascotas = mascotas.OrderBy(s => s.Nombre);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(mascotas.ToPagedList(pageNumber, pageSize));
        }

        //// GET: Mascotas
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.Mascotas.ToListAsync());
        //}

        // GET: Mascotas/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascotas mascotas = await db.Mascotas.FindAsync(id);
            if (mascotas == null)
            {
                return HttpNotFound();
            }
            return View(mascotas);
        }
        [Authorize(Roles = "Admin")]
        // GET: Mascotas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mascotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "MascotasID,Nombre,Edad,Sexo,Tamano,Area")] Mascotas mascotas)
        {
            if (ModelState.IsValid)
            {
                db.Mascotas.Add(mascotas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mascotas);
        }

        // GET: Mascotas/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascotas mascotas = await db.Mascotas.FindAsync(id);
            if (mascotas == null)
            {
                return HttpNotFound();
            }
            return View(mascotas);
        }

        // POST: Mascotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "MascotasID,Nombre,Edad,Sexo,Tamano,Area")] Mascotas mascotas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mascotas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mascotas);
        }

        // GET: Mascotas/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascotas mascotas = await db.Mascotas.FindAsync(id);
            if (mascotas == null)
            {
                return HttpNotFound();
            }
            return View(mascotas);
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Mascotas mascotas = await db.Mascotas.FindAsync(id);
            db.Mascotas.Remove(mascotas);
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
