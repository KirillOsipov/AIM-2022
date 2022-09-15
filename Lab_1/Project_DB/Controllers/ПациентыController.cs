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
using System.Diagnostics;

namespace OsipovCourseWork.Controllers
{
    public class ПациентыController : Controller
    {
        private CourseworkOsipovEntities db = new CourseworkOsipovEntities();

        // GET: Пациенты
        public async Task<ActionResult> Index(string surname, string payLeft, string payRight,
                                              string percent, string wardNumber, string way, 
                                              string disease)
        {
            var пациенты = db.Пациенты.Include(п => п.Гражданин).Include(п => п.Заболевания).Include(п => п.Палаты);

            try
            {
                if (!string.IsNullOrEmpty(surname))
                {
                    пациенты = пациенты.Where(п => п.Гражданин.Фамилия == surname);
                }

                if (!string.IsNullOrEmpty(payLeft) && !string.IsNullOrEmpty(payRight))
                {
                    decimal left = decimal.Parse(payLeft);
                    decimal right = decimal.Parse(payRight);
                    пациенты = пациенты.Where(п => п.ПлатаЗаЛечение >= left && п.ПлатаЗаЛечение <= right);
                }

                if (!string.IsNullOrEmpty(percent) && !string.IsNullOrEmpty(wardNumber) && !string.IsNullOrEmpty(way))
                {
                    using (CourseworkOsipovEntities db = new CourseworkOsipovEntities())
                    {
                        System.Data.SqlClient.SqlParameter[] parameters = new System.Data.SqlClient.SqlParameter[]
                        {
                        new System.Data.SqlClient.SqlParameter("@percent", percent),
                        new System.Data.SqlClient.SqlParameter("@wardNumber", wardNumber),
                        new System.Data.SqlClient.SqlParameter("@way", way)
                        };
                        var patients = db.Database.SqlQuery<Пациенты>("IncreasePay @percent, @wardNumber, @way", parameters[0], parameters[1], parameters[2]);
                        foreach (var p in patients)
                            Trace.WriteLine($"{p.НомерПалаты} - {p.ПлатаЗаЛечение}");
                    }
                }

                if (!string.IsNullOrEmpty(disease))
                {
                    try
                    {
                        string sqlQuery = "SELECT [dbo].[CountDiseases] ({0})";
                        object[] parameters = { disease };
                        decimal avgPay = db.Database.SqlQuery<decimal>(sqlQuery, parameters).FirstOrDefault();
                        ViewBag.avgPay = avgPay;
                    }
                    catch
                    {
                        ViewBag.avgPay = "ошибка, введите верное заболевание";
                    }
                }
            }
            catch
            {
                ViewBag.Error = "Ошибка! Неправильно заполнены поля поиска, фильтрации или аргументы процедуры.";
                return View("Error");
            }

            return View(await пациенты.ToListAsync());
        }


        public async Task<ActionResult> IndexU(string surname, string payLeft, string payRight,
                                               string disease)
        {
            var пациенты = db.Пациенты.Include(п => п.Гражданин).Include(п => п.Заболевания).Include(п => п.Палаты);

            try
            {
                if (!string.IsNullOrEmpty(surname))
                {
                    пациенты = пациенты.Where(п => п.Гражданин.Фамилия == surname);
                }

                if (!string.IsNullOrEmpty(payLeft) && !string.IsNullOrEmpty(payRight))
                {
                    decimal left = decimal.Parse(payLeft);
                    decimal right = decimal.Parse(payRight);
                    пациенты = пациенты.Where(п => п.ПлатаЗаЛечение >= left && п.ПлатаЗаЛечение <= right);
                }

                if (!string.IsNullOrEmpty(disease))
                {
                    try
                    {
                        string sqlQuery = "SELECT [dbo].[CountDiseases] ({0})";
                        object[] parameters = { disease };
                        decimal avgPay = db.Database.SqlQuery<decimal>(sqlQuery, parameters).FirstOrDefault();
                        ViewBag.avgPay = avgPay;
                    }
                    catch
                    {
                        ViewBag.avgPay = "ошибка, введите верное заболевание";
                    }

                }
            }
            catch
            {
                ViewBag.Error = "Ошибка! Неправильно заполнены поля поиска, фильтрации или аргументы процедуры.";
                return View("Error");
            }

            return View(await пациенты.ToListAsync());
        }

        // GET: Пациенты/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пациенты пациенты = await db.Пациенты.FindAsync(id);
            if (пациенты == null)
            {
                return HttpNotFound();
            }
            return View(пациенты);
        }

        // GET: Пациенты/Create
        public ActionResult Create()
        {
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта");
            ViewBag.Диагноз = new SelectList(db.Заболевания, "Название", "Название");
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты");
            return View();
        }

        // POST: Пациенты/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ИД,НомерМедКарты,ДатаПоступления,ДатаВыписки,НомерПалаты,Диагноз,ПлатаЗаЛечение,РезультатЛечения")] Пациенты пациенты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Пациенты.Add(пациенты);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при создании.";
                    return View("Error");
                }
            }

            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", пациенты.ИД);
            ViewBag.Диагноз = new SelectList(db.Заболевания, "Название", "Название", пациенты.Диагноз);
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", пациенты.НомерПалаты);
            return View(пациенты);
        }

        // GET: Пациенты/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пациенты пациенты = await db.Пациенты.FindAsync(id);
            if (пациенты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", пациенты.ИД);
            ViewBag.Диагноз = new SelectList(db.Заболевания, "Название", "Название", пациенты.Диагноз);
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", пациенты.НомерПалаты);
            return View(пациенты);
        }

        // POST: Пациенты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ИД,НомерМедКарты,ДатаПоступления,ДатаВыписки,НомерПалаты,Диагноз,ПлатаЗаЛечение,РезультатЛечения")] Пациенты пациенты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(пациенты).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", пациенты.ИД);
            ViewBag.Диагноз = new SelectList(db.Заболевания, "Название", "Название", пациенты.Диагноз);
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", пациенты.НомерПалаты);
            return View(пациенты);
        }

        public async Task<ActionResult> EditU(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пациенты пациенты = await db.Пациенты.FindAsync(id);
            if (пациенты == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", пациенты.ИД);
            ViewBag.Диагноз = new SelectList(db.Заболевания, "Название", "Название", пациенты.Диагноз);
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", пациенты.НомерПалаты);
            return View(пациенты);
        }

        // POST: Пациенты/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditU([Bind(Include = "ИД,НомерМедКарты,ДатаПоступления,ДатаВыписки,НомерПалаты,Диагноз,ПлатаЗаЛечение,РезультатЛечения")] Пациенты пациенты)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(пациенты).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("IndexU");
                }
                catch
                {
                    ViewBag.Error = "Ошибка! Нарушение уникальности атрибута при изменении.";
                    return View("Error");
                }
            }
            ViewBag.ИД = new SelectList(db.Гражданин, "ИД", "НомерПаспорта", пациенты.ИД);
            ViewBag.Диагноз = new SelectList(db.Заболевания, "Название", "Название", пациенты.Диагноз);
            ViewBag.НомерПалаты = new SelectList(db.Палаты, "НомерПалаты", "НомерПалаты", пациенты.НомерПалаты);
            return View(пациенты);
        }

        // GET: Пациенты/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Пациенты пациенты = await db.Пациенты.FindAsync(id);
            if (пациенты == null)
            {
                return HttpNotFound();
            }
            return View(пациенты);
        }

        // POST: Пациенты/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Пациенты пациенты = await db.Пациенты.FindAsync(id);
                db.Пациенты.Remove(пациенты);
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
