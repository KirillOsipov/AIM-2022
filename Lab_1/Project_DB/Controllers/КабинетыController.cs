using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OsipovCourseWork.Models;

namespace OsipovCourseWork.Controllers
{
    public class КабинетыController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: Кабинеты
        public async Task<ActionResult> Index()
        {
            return View(await db.Кабинеты.ToListAsync());
        }

        public async Task<ActionResult> IndexU()
        {
            return View(await db.Кабинеты.ToListAsync());
        }

        public async Task<ActionResult> IndexG()
        {
            return View(await db.Кабинеты.ToListAsync());
        }

        // GET: Кабинеты/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Кабинеты кабинеты = await db.Кабинеты.FindAsync(id);
            if (кабинеты == null)
            {
                return HttpNotFound();
            }
            return View(кабинеты);
        }

        // GET: Кабинеты/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Кабинеты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "НомерКабинета,Корпус,Этаж")] Кабинеты кабинеты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Кабинеты.Add(кабинеты);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                { 
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании."; 
                    return View("Error"); 
                }
            }

            return View(кабинеты);
        }

        // GET: Кабинеты/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Кабинеты кабинеты = await db.Кабинеты.FindAsync(id);
            if (кабинеты == null)
            {
                return HttpNotFound();
            }
            return View(кабинеты);
        }

        // POST: Кабинеты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "НомерКабинета,Корпус,Этаж")] Кабинеты кабинеты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(кабинеты).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            return View(кабинеты);
        }

        public async Task<ActionResult> EditU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Кабинеты кабинеты = await db.Кабинеты.FindAsync(id);
            if (кабинеты == null)
            {
                return HttpNotFound();
            }
            return View(кабинеты);
        }

        // POST: Кабинеты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditU([Bind(Include = "НомерКабинета,Корпус,Этаж")] Кабинеты кабинеты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(кабинеты).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexU");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            return View(кабинеты);
        }

        // GET: Кабинеты/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Кабинеты кабинеты = await db.Кабинеты.FindAsync(id);
            if (кабинеты == null)
            {
                return HttpNotFound();
            }
            return View(кабинеты);
        }

        // POST: Кабинеты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Кабинеты кабинеты = await db.Кабинеты.FindAsync(id);
                db.Кабинеты.Remove(кабинеты);
                await db.SaveChangesAsync();
            }
            catch
            {
                ViewBag.Error = "Ошибка! Нарушение ссылочной целостности при удалении.";
                return View("Error");
            }
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
