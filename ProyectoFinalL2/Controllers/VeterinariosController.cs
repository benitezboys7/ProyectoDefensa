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
    public class VeterinariosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

            var veterinario = from s in db.Veterinarios
                          select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                veterinario = veterinario.Where(s => s.Edad.ToString().Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    veterinario = veterinario.OrderByDescending(s => s.Edad);

                    break;
                default:
                    veterinario = veterinario.OrderBy(s => s.Edad);
                    break;
            }
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            return View(veterinario.ToPagedList(pageNumber, pageSize));
        }

        // GET: Veterinarios
        //[Authorize(Roles = "Admin")]
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.Veterinarios.ToListAsync());
        //}

        // GET: Veterinarios/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinario veterinario = await db.Veterinarios.FindAsync(id);
            if (veterinario == null)
            {
                return HttpNotFound();
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Veterinarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "VeterinarioID,Nombre,Edad,Cedula,Telefono")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                db.Veterinarios.Add(veterinario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(veterinario);
        }

        // GET: Veterinarios/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinario veterinario = await db.Veterinarios.FindAsync(id);
            if (veterinario == null)
            {
                return HttpNotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "VeterinarioID,Nombre,Edad,Cedula,Telefono")] Veterinario veterinario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veterinario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(veterinario);
        }

        // GET: Veterinarios/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinario veterinario = await db.Veterinarios.FindAsync(id);
            if (veterinario == null)
            {
                return HttpNotFound();
            }
            return View(veterinario);
        }

        // POST: Veterinarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Veterinario veterinario = await db.Veterinarios.FindAsync(id);
            db.Veterinarios.Remove(veterinario);
            await db.SaveChangesAsync();
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
