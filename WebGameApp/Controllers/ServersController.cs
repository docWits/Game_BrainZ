using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebGameApp.Controllers
{
    /// <summary>
    /// <para>Контроллер для работы с списком серверов, см. также <see cref="WpfGameApp.Entities.Server"/>. Реализует методы:</para>        
    /// <list>
    /// <item>Index - список серверов</item>
    /// <item>Edit - редактирование сервера</item>
    /// </list>
    /// </summary>
    public class ServersController : Controller
    {
        /// <summary>
        /// База данных
        /// </summary>
        private WpfGameApp.DB db = new WpfGameApp.DB();

        // GET: Servers
        public ActionResult Index()
        {
            var list = db.Servers.OrderBy(x => x.Name).ToList();
            return View(list);
        }

        // GET: Servers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Servers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servers/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var server = db.Servers.Find(id);
            if (server == null)
            {
                return HttpNotFound();
            }
            return View(server);
        }

        // POST: Servers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WpfGameApp.Entities.Server server)
        {
            try
            {
                if (db.Vendors.Find(server.VendorID) == null)
                {
                    ModelState.AddModelError("VendorID", "В справочнике нет такого вендора");
                }
                if (ModelState.IsValid)
                {
                    db.Entry(server).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(server);

            }
            catch
            {
                return View(server);
            }
        }

        // GET: Servers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Servers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
