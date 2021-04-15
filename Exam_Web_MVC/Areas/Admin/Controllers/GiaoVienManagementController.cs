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
    public class GiaoVienManagementController : Controller
    {
        //private Exam_WebEntities db = new Exam_WebEntities();
        private static Exam_WebEntities _context = new Exam_WebEntities();
        GiaoVienRepository giaoVienRepository = new GiaoVienRepository(_context);
        TaiKhoanRepository taiKhoanRepository = new TaiKhoanRepository(_context);
        HocViRepository hocViRepository = new HocViRepository(_context);
        MonHocRepository monHocRepository = new MonHocRepository(_context);

        // GET: Admin/GiaoVienManagement
        public ActionResult Index()
        {
            return View(giaoVienRepository.GetAll());
        }

        // GET: Admin/GiaoVienManagement/Create
        public ActionResult Create()
        {
            ViewBag.HocViID = new SelectList(hocViRepository.GetAll(), "HocViID", "TenHocVi");
            ViewBag.MonHocID = new SelectList(monHocRepository.GetAll(), "MonHocID", "TenMH");
            ViewBag.TaiKhoanID = new SelectList(taiKhoanRepository.GetAll(), "TaiKhoanID", "UserName");
            return View();
        }

        // POST: Admin/GiaoVienManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GiaoVien_TaiKhoan_Model viewModel)
        {
            if (ModelState.IsValid)
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.UserName = viewModel.UserName;
                taiKhoan.Password = viewModel.Password;
                taiKhoan.Role = viewModel.Role;
                taiKhoanRepository.Add(taiKhoan);

                GiaoVien giaoVien = new GiaoVien();
                giaoVien.TaiKhoanID = taiKhoanRepository.GetByUsername(taiKhoan.UserName).TaiKhoanID;
                giaoVien.TenGV = viewModel.TenGV;
                giaoVien.NgaySinh = viewModel.NgaySinh;
                giaoVien.GioiTinh = viewModel.GioiTinh;
                giaoVien.Email = viewModel.Email;
                giaoVien.MonHocID = viewModel.MonHocID;
                giaoVien.HocViID = viewModel.HocViID;
                giaoVienRepository.Add(giaoVien);
                return RedirectToAction("Index");
            }

            //ViewBag.HocViID = new SelectList(db.HocVis, "HocViID", "TenHocVi", giaoVien.HocViID);
            //ViewBag.MonHocID = new SelectList(db.MonHocs, "MonHocID", "TenMH", giaoVien.MonHocID);
            //ViewBag.TaiKhoanID = new SelectList(db.TaiKhoans, "TaiKhoanID", "UserName", giaoVien.TaiKhoanID);
            //return View(giaoVien);
            return View();
        }

        // GET: Admin/GiaoVienManagement/Edit/5
        public ActionResult Edit(int id)
        {
            GiaoVien_TaiKhoan_Model viewModel = new GiaoVien_TaiKhoan_Model();
            GiaoVien giaoVien = giaoVienRepository.GetById(id);
            TaiKhoan taiKhoan = taiKhoanRepository.GetById((int)giaoVien.TaiKhoanID);
            viewModel.GiaoVienID = giaoVien.GiaoVienID;
            viewModel.TaiKhoanID = giaoVien.TaiKhoanID;
            viewModel.TenGV = giaoVien.TenGV;
            viewModel.NgaySinh = giaoVien.NgaySinh;
            viewModel.GioiTinh = giaoVien.GioiTinh;
            viewModel.Email = giaoVien.Email;
            viewModel.MonHocID = giaoVien.MonHocID;
            viewModel.HocViID = giaoVien.HocViID;

            viewModel.UserName = taiKhoan.UserName;
            viewModel.Password = taiKhoan.Password;
            viewModel.Role = taiKhoan.Role;
            ViewBag.HocViID = new SelectList(hocViRepository.GetAll(), "HocViID", "TenHocVi", giaoVien.HocViID);
            ViewBag.MonHocID = new SelectList(monHocRepository.GetAll(), "MonHocID", "TenMH", giaoVien.MonHocID);
            //ViewBag.TaiKhoanID = new SelectList(taiKhoanRepository.GetAll(), "TaiKhoanID", "UserName", viewModel.giaoVien.TaiKhoanID);
            return View(viewModel);
        }

        // POST: Admin/GiaoVienManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GiaoVien_TaiKhoan_Model viewModel)
        {
            if (ModelState.IsValid)
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.UserName = viewModel.UserName;
                taiKhoan.Password = viewModel.Password;
                taiKhoan.Role = viewModel.Role;
                taiKhoan.TaiKhoanID = (int)viewModel.TaiKhoanID;
                taiKhoanRepository.Update(taiKhoan);
                var taiKhoan_updated = taiKhoanRepository.GetByUsername(viewModel.UserName);

                GiaoVien giaoVien = new GiaoVien();
                giaoVien.GiaoVienID = viewModel.GiaoVienID;
                giaoVien.TaiKhoanID = taiKhoan_updated.TaiKhoanID;
                giaoVien.TenGV = viewModel.TenGV;
                giaoVien.NgaySinh = viewModel.NgaySinh;
                giaoVien.GioiTinh = viewModel.GioiTinh;
                giaoVien.Email = viewModel.Email;
                giaoVien.MonHocID = viewModel.MonHocID;
                giaoVien.HocViID = viewModel.HocViID;
                giaoVienRepository.Update(giaoVien);
                return RedirectToAction("Index");
            }
            //ViewBag.HocViID = new SelectList(db.HocVis, "HocViID", "TenHocVi", giaoVien.HocViID);
            //ViewBag.MonHocID = new SelectList(db.MonHocs, "MonHocID", "TenMH", giaoVien.MonHocID);
            //ViewBag.TaiKhoanID = new SelectList(db.TaiKhoans, "TaiKhoanID", "UserName", giaoVien.TaiKhoanID);
            //return View(giaoVien);
            return RedirectToAction("Index");
        }

        // GET: Admin/GiaoVienManagement/Delete/5
        public ActionResult Delete(int id)
        {
            GiaoVien delete_GiaoVien = giaoVienRepository.GetById(id);
            TaiKhoan delete_TaiKhoan = taiKhoanRepository.GetById((int)delete_GiaoVien.TaiKhoanID);
            giaoVienRepository.Delete(delete_GiaoVien);
            taiKhoanRepository.Delete(delete_TaiKhoan);
            return RedirectToAction("Index");
        }
    }
}
