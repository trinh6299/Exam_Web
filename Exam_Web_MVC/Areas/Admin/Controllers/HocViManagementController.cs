using Exam_Web_Core;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Exam_Web_Core.Repositories;

namespace Exam_Web_MVC.Areas.Admin.Controllers
{
    public class HocViManagementController : Controller
    {
        private static Exam_WebEntities _context = new Exam_WebEntities();
        private HocViRepository hocViRepository = new HocViRepository(_context);
        // GET: Admin/HocViManagement
        public ActionResult Index()
        {
            return View(hocViRepository.GetAll());
        }

        // GET: Admin/HocViManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HocViManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HocVi hocVi)
        {
            if (ModelState.IsValid)
            {
                hocViRepository.Add(hocVi);
                return RedirectToAction("Index");
            }
            return View(hocVi);
        }

        // GET: Admin/HocViManagement/Edit/5
        public ActionResult Edit(int id)
        {
            HocVi hocVi = hocViRepository.GetById(id);
            return View(hocVi);
        }

        // POST: Admin/HocViManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HocVi hocVi)
        {
            if (ModelState.IsValid)
            {
                hocViRepository.Update(hocVi);
                return RedirectToAction("Index");
            }
            return View(hocVi);
        }

        // GET: Admin/HocViManagement/Delete/5
        public ActionResult Delete(int id)
        {
            HocVi hocVi = hocViRepository.GetById(id);
            hocViRepository.Delete(hocVi);
            return RedirectToAction("Index");
        }
    }
}
