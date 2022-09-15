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
    public class ИзоляцияНаКарантинController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: ИзоляцияНаКарантин
        public async Task<ActionResult> Index()
        {
            var изоляцияНаКарантин = db.ИзоляцияНаКарантин.Include(и => и.Пациенты);
            return View(await изоляцияНаКарантин.ToListAsync());
        }

        public async Task<ActionResult> IndexU()
        {
            var изоляцияНаКарантин = db.ИзоляцияНаКарантин.Include(и => и.Пациенты);
            return View(await изоляцияНаКарантин.ToListAsync());
        }

        // GET: ИзоляцияНаКарантин/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ИзоляцияНаКарантин изоляцияНаКарантин = await db.ИзоляцияНаКарантин.FindAsync(id);
            if (изоляцияНаКарантин == null)
            {
                return HttpNotFound();
            }
            return View(изоляцияНаКарантин);
        }

        // GET: ИзоляцияНаКарантин/Create
        public ActionResult Create()
        {
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты");
            return View();
        }

        // POST: ИзоляцияНаКарантин/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "НомерИзолятора,НачалоКарантина,КонецКарантина,Заболевание,ИД_Пациента")] ИзоляцияНаКарантин изоляцияНаКарантин)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.ИзоляцияНаКарантин.Add(изоляцияНаКарантин);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                { 
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании."; 
                    return View("Error"); 
                }
            }

            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", изоляцияНаКарантин.ИД_Пациента);
            return View(изоляцияНаКарантин);
        }

        // GET: ИзоляцияНаКарантин/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ИзоляцияНаКарантин изоляцияНаКарантин = await db.ИзоляцияНаКарантин.FindAsync(id);
            if (изоляцияНаКарантин == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", изоляцияНаКарантин.ИД_Пациента);
            return View(изоляцияНаКарантин);
        }

        // POST: ИзоляцияНаКарантин/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "НомерИзолятора,НачалоКарантина,КонецКарантина,Заболевание,ИД_Пациента")] ИзоляцияНаКарантин изоляцияНаКарантин)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(изоляцияНаКарантин).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                { 
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении."; 
                    return View("Error"); 
                }
            }
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", изоляцияНаКарантин.ИД_Пациента);
            return View(изоляцияНаКарантин);
        }

        public async Task<ActionResult> EditU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ИзоляцияНаКарантин изоляцияНаКарантин = await db.ИзоляцияНаКарантин.FindAsync(id);
            if (изоляцияНаКарантин == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", изоляцияНаКарантин.ИД_Пациента);
            return View(изоляцияНаКарантин);
        }

        // POST: ИзоляцияНаКарантин/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditU([Bind(Include = "НомерИзолятора,НачалоКарантина,КонецКарантина,Заболевание,ИД_Пациента")] ИзоляцияНаКарантин изоляцияНаКарантин)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(изоляцияНаКарантин).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexU");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", изоляцияНаКарантин.ИД_Пациента);
            return View(изоляцияНаКарантин);
        }

        // GET: ИзоляцияНаКарантин/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ИзоляцияНаКарантин изоляцияНаКарантин = await db.ИзоляцияНаКарантин.FindAsync(id);
            if (изоляцияНаКарантин == null)
            {
                return HttpNotFound();
            }
            return View(изоляцияНаКарантин);
        }

        // POST: ИзоляцияНаКарантин/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                ИзоляцияНаКарантин изоляцияНаКарантин = await db.ИзоляцияНаКарантин.FindAsync(id);
                db.ИзоляцияНаКарантин.Remove(изоляцияНаКарантин);
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
