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
    public class DeThiManagementController : Controller
    {
        private Exam_WebEntities db = new Exam_WebEntities();
        private static Exam_WebEntities _context = new Exam_WebEntities();
        DeThiRepository deThiRepository = new DeThiRepository(_context);
        CauHoiRepository cauHoiRepository = new CauHoiRepository(_context);

        // GET: Admin/DeThiManagement
        public ActionResult Index()
        {
            ViewBag.ListDeThi = deThiRepository.GetAll();
            return View();
        }

        // GET: Admin/DeThiManagement/Details/5
        public ActionResult Details(int id)
        {
            DeThi deThi = deThiRepository.GetById(id);
            ViewBag.CauHois = cauHoiRepository.GetCauHoiByMaDe(id);
            Session["DeThiID"] = id;
            return View(deThi);
        }

        // GET: Admin/DeThiManagement/Create
        public ActionResult Create()
        {
            ViewBag.GiaoVienID = new SelectList(db.GiaoViens, "GiaoVienID", "TenGV");
            ViewBag.MonHocID = new SelectList(db.MonHocs, "MonHocID", "TenMH");
            return View();
        }

        // POST: Admin/DeThiManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeThi deThi)
        {
            if (ModelState.IsValid)
            {
                deThiRepository.Add(deThi);
                return RedirectToAction("Index");
            }

            //ViewBag.GiaoVienID = new SelectList(db.GiaoViens, "GiaoVienID", "TenGV", deThi.GiaoVienID);
            //ViewBag.MonHocID = new SelectList(db.MonHocs, "MonHocID", "TenMH", deThi.MonHocID);
            return View();
        }

        // GET: Admin/DeThiManagement/Edit/5
        public ActionResult Edit(int id)
        {
            DeThi deThi = deThiRepository.GetById(id);
            ViewBag.GiaoVienID = new SelectList(db.GiaoViens, "GiaoVienID", "TenGV", deThi.GiaoVienID);
            ViewBag.MonHocID = new SelectList(db.MonHocs, "MonHocID", "TenMH", deThi.MonHocID);
            return View(deThi);
        }

        // POST: Admin/DeThiManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeThiID,MonHocID,TenDeThi,ThoiGianLamBai,ThoiGianBatDauLamBai,LoaiDe,GiaoVienID")] DeThi deThi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deThi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GiaoVienID = new SelectList(db.GiaoViens, "GiaoVienID", "TenGV", deThi.GiaoVienID);
            ViewBag.MonHocID = new SelectList(db.MonHocs, "MonHocID", "TenMH", deThi.MonHocID);
            return View(deThi);
        }

        // GET: Admin/DeThiManagement/Delete/5
        public ActionResult Delete(int id)
        {
            DeThi deThi = deThiRepository.GetById(id);
            deThiRepository.Delete(deThi);
            return RedirectToAction("Index");
        }

        public ActionResult AddQuestionManual(int id)
        {
            ViewBag.TenDeThi = deThiRepository.GetById(id).TenDeThi;
            int DeThi_MonHocId = (int)deThiRepository.GetById(id).MonHocID;
            ViewBag.CauHois = cauHoiRepository.GetCauHoiForDeThi(id, DeThi_MonHocId);
            return View();
        }

        //Add question to de thi
        public ActionResult AddQuestion(int id)
        {
            //TempData.Keep("DeThiID");
            CauHoi cauHoi = cauHoiRepository.GetById(id);
            DeThi deThi = deThiRepository.GetById((int)Session["DeThiID"]);
            cauHoi.DeThis.Add(deThi);
            cauHoiRepository.Update(cauHoi);
            return RedirectToAction("AddQuestionManually", new { id = (int)Session["DeThiID"] });
        }

        //Remove question from de thi
        public ActionResult RemoveQuestion(int id)
        {
            CauHoi cauHoi = cauHoiRepository.GetById(id);
            DeThi deThi = deThiRepository.GetById((int)Session["DeThiID"]);
            deThi.CauHois.Remove(cauHoi);
            deThiRepository.Update(deThi);
            return RedirectToAction("Details", new { id = (int)Session["DeThiID"] });
        }

        public ActionResult AddQuestionAuto(int id)
        {
            ViewBag.TenDeThi = deThiRepository.GetById(id).TenDeThi;
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestionAuto(AddQuestionAuto_Model model)
        {
            DeThi deThi = deThiRepository.GetById((int)Session["DeThiID"]);
            var listCauDe = cauHoiRepository.GetRandomCauHoiByDoKho(1, (int)deThi.MonHocID, model.SoCauDe);
            foreach (var item in listCauDe)
            {
                //o day nhung cau hoi nao trung se khong duoc add nen khong can phai loc cau hoi da co trong de thi
                deThi.CauHois.Add(item);
            }
            var listCauTrungBinh = cauHoiRepository.GetRandomCauHoiByDoKho(2, (int)deThi.MonHocID, model.SoCauDe);
            foreach (var item in listCauTrungBinh)
            {
                //o day nhung cau hoi nao trung se khong duoc add nen khong can phai loc cau hoi da co trong de thi
                deThi.CauHois.Add(item);
            }
            var listCauKho = cauHoiRepository.GetRandomCauHoiByDoKho(3, (int)deThi.MonHocID, model.SoCauDe);
            foreach (var item in listCauKho)
            {
                //o day nhung cau hoi nao trung se khong duoc add nen khong can phai loc cau hoi da co trong de thi
                deThi.CauHois.Add(item);
            }
            var listCauRatKho = cauHoiRepository.GetRandomCauHoiByDoKho(4, (int)deThi.MonHocID, model.SoCauDe);
            foreach (var item in listCauRatKho)
            {
                //o day nhung cau hoi nao trung se khong duoc add nen khong can phai loc cau hoi da co trong de thi
                deThi.CauHois.Add(item);
            }
            deThiRepository.Update(deThi);
            return RedirectToAction("Details", "DeThiManagement", new { id = (int)Session["DeThiID"] });
        }
    }
}
