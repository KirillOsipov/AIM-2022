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
    public class ГражданинController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: Гражданин
        public async Task<ActionResult> Index()
        {
            var гражданин = db.Гражданин.Include(г => г.МедицинскийПерсонал).Include(г => г.Пациенты);
            return View(await гражданин.ToListAsync());
        }

        // GET: Гражданин/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Гражданин гражданин = await db.Гражданин.FindAsync(id);
            if (гражданин == null)
            {
                return HttpNotFound();
            }
            return View(гражданин);
        }

        // GET: Гражданин/Create
        public ActionResult Create()
        {
            ViewBag.ИД = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность");
            ViewBag.ИД = new SelectList(db.Пациенты, "ИД", "НомерМедКарты");
            return View();
        }

        // POST: Гражданин/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ИД,НомерПаспорта,Фамилия,Имя,Отчество,ДатаРождения,Пол,Телефон")] Гражданин гражданин)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Гражданин.Add(гражданин);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании.";
                    return View("Error");
                }
                
            }

            ViewBag.ИД = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", гражданин.ИД);
            ViewBag.ИД = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", гражданин.ИД);
            return View(гражданин);
        }

        // GET: Гражданин/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Гражданин гражданин = await db.Гражданин.FindAsync(id);
            if (гражданин == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", гражданин.ИД);
            ViewBag.ИД = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", гражданин.ИД);
            return View(гражданин);
        }

        // POST: Гражданин/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ИД,НомерПаспорта,Фамилия,Имя,Отчество,ДатаРождения,Пол,Телефон")] Гражданин гражданин)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(гражданин).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.ИД = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", гражданин.ИД);
            ViewBag.ИД = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", гражданин.ИД);
            return View(гражданин);
        }

        // GET: Гражданин/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Гражданин гражданин = await db.Гражданин.FindAsync(id);
            if (гражданин == null)
            {
                return HttpNotFound();
            }
            return View(гражданин);
        }

        // POST: Гражданин/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Гражданин гражданин = await db.Гражданин.FindAsync(id);
                db.Гражданин.Remove(гражданин);
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
