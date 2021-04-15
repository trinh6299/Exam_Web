using Exam_Web_Core;
using Exam_Web_Core.Repositories;
using System.Web.Mvc;

namespace Exam_Web_MVC.Areas.Admin.Controllers
{
    public class MonHocManagementController : Controller
    {
        //private Exam_WebEntities db = new Exam_WebEntities();
        private static Exam_WebEntities _context = new Exam_WebEntities();
        private MonHocRepository monHocRepository = new MonHocRepository(_context);

        // GET: Admin/MonHocManagement
        public ActionResult Index()
        {
            return View(monHocRepository.GetAll());
        }

        // GET: Admin/MonHocManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MonHocManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                monHocRepository.Add(monHoc);
                return RedirectToAction("Index");
            }
            return View(monHoc);
        }

        // GET: Admin/MonHocManagement/Edit/5
        public ActionResult Edit(int id)
        {
            MonHoc monHoc = monHocRepository.GetById(id);
            return View(monHoc);
        }

        // POST: Admin/MonHocManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MonHoc monHoc)
        {
            if (ModelState.IsValid)
            {
                monHocRepository.Update(monHoc);
                return RedirectToAction("Index");
            }
            return View(monHoc);
        }

        // GET: Admin/MonHocManagement/Delete/5
        public ActionResult Delete(int id)
        {
            MonHoc entity = monHocRepository.GetById(id);
            monHocRepository.Delete(entity);
            return RedirectToAction("Index");
        }
    }
}
