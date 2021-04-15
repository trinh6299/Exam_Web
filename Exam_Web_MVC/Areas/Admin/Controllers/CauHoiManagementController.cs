using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exam_Web_Core;
using Exam_Web_Core.Repositories;
using Exam_Web_Core.ViewModels;

namespace Exam_Web_MVC.Areas.Admin.Controllers
{
    public class CauHoiManagementController : Controller
    {
        private static Exam_WebEntities _context = new Exam_WebEntities();
        private CauHoiRepository cauHoiRepository = new CauHoiRepository(_context);
        private MonHocRepository monHocRepository = new MonHocRepository(_context);
        private DoKhoRepository doKhoRepository = new DoKhoRepository(_context);

        // GET: Admin/CauHoiManagement
        //public ActionResult Index()
        //{
        //    return View(cauHoiRepository.GetAll());
        //}

        public ActionResult Index(int? id)
        {
            int pageIndex = 1;
            if (id > 1)
            {
                pageIndex = (int)id;
            }
            var source = cauHoiRepository.GetAll();
            int pageSize = 10;
            ViewBag.TotalPages = Math.Ceiling(source.Count() / (double)pageSize);
            ViewBag.PageIndex = pageIndex;
            return View(cauHoiRepository.Paging(source, pageIndex, pageSize));
        }

        [HttpPost]
        //[HttpGet]
        public ActionResult Index(string searchString)
        {
            return RedirectToAction("Search", new {searchString});
        }

        public ActionResult Create()
        {

            ViewBag.MonHocID = new SelectList(monHocRepository.GetAll(), "MonHocID", "TenMH");
            ViewBag.DoKhoID = new SelectList(doKhoRepository.GetAll(), "DoKhoID", "TenDoKho");
            return View();
        }

        // POST: Admin/CauHoiManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                cauHoiRepository.Add(cauHoi);
                return RedirectToAction("Index");
            }
            //ViewBag.MonHocID = new SelectList(db.MonHocs, "MonHocID", "TenMH", cauHoi.MonHocID);
            return View(cauHoi);
        }

        // GET: Admin/CauHoiManagement/Edit/5
        public ActionResult Edit(int id)
        {
            CauHoi cauHoi = cauHoiRepository.GetById(id);
            ViewBag.MonHocID = new SelectList(monHocRepository.GetAll(), "MonHocID", "TenMH", cauHoi.MonHocID);
            return View(cauHoi);
        }

        // POST: Admin/CauHoiManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CauHoi cauHoi)
        {
            if (ModelState.IsValid)
            {
                cauHoiRepository.Update(cauHoi);
                return RedirectToAction("Index");
            }
            ViewBag.MonHocID = new SelectList(monHocRepository.GetAll(), "MonHocID", "TenMH", cauHoi.MonHocID);
            return View(cauHoi);
        }

        // GET: Admin/CauHoiManagement/Delete/5
        public ActionResult Delete(int id)
        {
            CauHoi entity = cauHoiRepository.GetById(id);
            cauHoiRepository.Delete(entity);
            return RedirectToAction("Index");
        }

        public ActionResult Search(string searchString)
        {

            return View(cauHoiRepository.FindCauHoi(searchString));
        }
    }
}
