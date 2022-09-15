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
    public class ЛечениеController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: Лечение
        public async Task<ActionResult> Index()
        {
            var лечение = db.Лечение.Include(л => л.ЛекарственныеПрепараты).Include(л => л.МедицинскийПерсонал).Include(л => л.Оборудование).Include(л => л.Пациенты);
            return View(await лечение.ToListAsync());
        }

        public async Task<ActionResult> IndexU()
        {
            var лечение = db.Лечение.Include(л => л.ЛекарственныеПрепараты).Include(л => л.МедицинскийПерсонал).Include(л => л.Оборудование).Include(л => л.Пациенты);
            return View(await лечение.ToListAsync());
        }

        // GET: Лечение/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Лечение лечение = await db.Лечение.FindAsync(id);
            if (лечение == null)
            {
                return HttpNotFound();
            }
            return View(лечение);
        }

        // GET: Лечение/Create
        public ActionResult Create()
        {
            ViewBag.ИД_Препарата = new SelectList(db.ЛекарственныеПрепараты, "ИД_Препарата", "НазваниеПрепарата");
            ViewBag.ИД_Врача = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность");
            ViewBag.ИД_Оборудования = new SelectList(db.Оборудование, "ИД_Оборудования", "Наименование");
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты");
            return View();
        }

        // POST: Лечение/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ИД_Лечения,ДатаНачала,ДатаОкончания,Цена,ИД_Врача,ИД_Пациента,ИД_Препарата,ИД_Оборудования")] Лечение лечение)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Лечение.Add(лечение);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании.";
                    return View("Error");
                }
            }

            ViewBag.ИД_Препарата = new SelectList(db.ЛекарственныеПрепараты, "ИД_Препарата", "НазваниеПрепарата", лечение.ИД_Препарата);
            ViewBag.ИД_Врача = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", лечение.ИД_Врача);
            ViewBag.ИД_Оборудования = new SelectList(db.Оборудование, "ИД_Оборудования", "Наименование", лечение.ИД_Оборудования);
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", лечение.ИД_Пациента);
            return View(лечение);
        }

        // GET: Лечение/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Лечение лечение = await db.Лечение.FindAsync(id);
            if (лечение == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_Препарата = new SelectList(db.ЛекарственныеПрепараты, "ИД_Препарата", "НазваниеПрепарата", лечение.ИД_Препарата);
            ViewBag.ИД_Врача = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", лечение.ИД_Врача);
            ViewBag.ИД_Оборудования = new SelectList(db.Оборудование, "ИД_Оборудования", "Наименование", лечение.ИД_Оборудования);
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", лечение.ИД_Пациента);
            return View(лечение);
        }

        // POST: Лечение/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ИД_Лечения,ДатаНачала,ДатаОкончания,Цена,ИД_Врача,ИД_Пациента,ИД_Препарата,ИД_Оборудования")] Лечение лечение)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(лечение).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch 
                { 
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении."; 
                    return View("Error"); 
                }
            }
            ViewBag.ИД_Препарата = new SelectList(db.ЛекарственныеПрепараты, "ИД_Препарата", "НазваниеПрепарата", лечение.ИД_Препарата);
            ViewBag.ИД_Врача = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", лечение.ИД_Врача);
            ViewBag.ИД_Оборудования = new SelectList(db.Оборудование, "ИД_Оборудования", "Наименование", лечение.ИД_Оборудования);
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", лечение.ИД_Пациента);
            return View(лечение);
        }

        public async Task<ActionResult> EditU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Лечение лечение = await db.Лечение.FindAsync(id);
            if (лечение == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_Препарата = new SelectList(db.ЛекарственныеПрепараты, "ИД_Препарата", "НазваниеПрепарата", лечение.ИД_Препарата);
            ViewBag.ИД_Врача = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", лечение.ИД_Врача);
            ViewBag.ИД_Оборудования = new SelectList(db.Оборудование, "ИД_Оборудования", "Наименование", лечение.ИД_Оборудования);
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", лечение.ИД_Пациента);
            return View(лечение);
        }

        // POST: Лечение/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditU([Bind(Include = "ИД_Лечения,ДатаНачала,ДатаОкончания,Цена,ИД_Врача,ИД_Пациента,ИД_Препарата,ИД_Оборудования")] Лечение лечение)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(лечение).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexU");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.ИД_Препарата = new SelectList(db.ЛекарственныеПрепараты, "ИД_Препарата", "НазваниеПрепарата", лечение.ИД_Препарата);
            ViewBag.ИД_Врача = new SelectList(db.МедицинскийПерсонал, "ИД", "Должность", лечение.ИД_Врача);
            ViewBag.ИД_Оборудования = new SelectList(db.Оборудование, "ИД_Оборудования", "Наименование", лечение.ИД_Оборудования);
            ViewBag.ИД_Пациента = new SelectList(db.Пациенты, "ИД", "НомерМедКарты", лечение.ИД_Пациента);
            return View(лечение);
        }

        // GET: Лечение/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Лечение лечение = await db.Лечение.FindAsync(id);
            if (лечение == null)
            {
                return HttpNotFound();
            }
            return View(лечение);
        }

        // POST: Лечение/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Лечение лечение = await db.Лечение.FindAsync(id);
                db.Лечение.Remove(лечение);
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
