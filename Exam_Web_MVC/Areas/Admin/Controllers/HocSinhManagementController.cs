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
    public class HocSinhManagementController : Controller
    {
        //private Exam_WebEntities db = new Exam_WebEntities();
        private static Exam_WebEntities _context = new Exam_WebEntities();
        TaiKhoanRepository taiKhoanRepository = new TaiKhoanRepository(_context);
        HocSinhRepository hocSinhRepository = new HocSinhRepository(_context);

        // GET: Admin/HocSinhManagement
        public ActionResult Index()
        {
            return View(hocSinhRepository.GetAll());
        }

        // GET: Admin/HocSinhManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/HocSinhManagement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HocSinh_TaiKhoan_Model viewModel)
        {
            if (ModelState.IsValid)
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.UserName = viewModel.UserName;
                taiKhoan.Password = viewModel.Password;
                taiKhoan.Role = viewModel.Role;
                taiKhoanRepository.Add(taiKhoan);

                HocSinh hocSinh = new HocSinh();
                hocSinh.TaiKhoanID = taiKhoanRepository.GetByUsername(viewModel.UserName).TaiKhoanID;
                hocSinh.TenHS = viewModel.TenHS;
                hocSinh.NgaySinh = viewModel.NgaySinh;
                hocSinh.GioiTinh = viewModel.GioiTinh;
                hocSinh.Email = viewModel.Email;
                hocSinhRepository.Add(hocSinh);
                return RedirectToAction("Index");
            }

            //ViewBag.TaiKhoanID = new SelectList(db.TaiKhoans, "TaiKhoanID", "UserName", hocSinh.TaiKhoanID);
            return View(viewModel);
        }

        // GET: Admin/HocSinhManagement/Edit/5
        public ActionResult Edit(int id)
        {
            HocSinh_TaiKhoan_Model viewModel = new HocSinh_TaiKhoan_Model();
            HocSinh hocSinh = hocSinhRepository.GetById(id);
            viewModel.HocSinhID = id;
            viewModel.TaiKhoanID = hocSinh.TaiKhoanID;
            viewModel.TenHS = hocSinh.TenHS;
            viewModel.NgaySinh = hocSinh.NgaySinh;
            viewModel.GioiTinh = hocSinh.GioiTinh;
            viewModel.Email = hocSinh.Email;

            if (hocSinh.TaiKhoanID!=null)
            {
                TaiKhoan taiKhoan = taiKhoanRepository.GetById((int)hocSinh.TaiKhoanID);
                viewModel.UserName = taiKhoan.UserName;
                viewModel.Password = taiKhoan.Password;
                viewModel.Role = taiKhoan.Role;
            }
            return View(viewModel);
        }

        // POST: Admin/HocSinhManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HocSinh_TaiKhoan_Model viewModel)
        {
            if (ModelState.IsValid)
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.TaiKhoanID = (int)viewModel.TaiKhoanID;
                taiKhoan.UserName = viewModel.UserName;
                taiKhoan.Password = viewModel.Password;
                taiKhoan.Role = viewModel.Role;
                taiKhoanRepository.Update(taiKhoan);

                HocSinh hocSinh = new HocSinh();
                hocSinh.HocSinhID = viewModel.HocSinhID;
                hocSinh.TaiKhoanID = taiKhoan.TaiKhoanID;
                hocSinh.TenHS = viewModel.TenHS;
                hocSinh.NgaySinh = viewModel.NgaySinh;
                hocSinh.GioiTinh = viewModel.GioiTinh;
                hocSinh.Email = viewModel.Email;
                hocSinhRepository.Update(hocSinh);
                return RedirectToAction("Index");
            }
            //ViewBag.TaiKhoanID = new SelectList(db.TaiKhoans, "TaiKhoanID", "UserName", viewModel.TaiKhoanID);
            return View(viewModel);
        }

        // GET: Admin/HocSinhManagement/Delete/5
        public ActionResult Delete(int id)
        {
            HocSinh hocSinh = hocSinhRepository.GetById(id);
            if (hocSinh.TaiKhoanID!=null)
            {
                TaiKhoan taiKhoan = taiKhoanRepository.GetById((int)hocSinh.TaiKhoanID);
                hocSinhRepository.Delete(hocSinh);
                taiKhoanRepository.Delete(taiKhoan);

            }
            else
            {
                hocSinhRepository.Delete(hocSinh);
            }
            return RedirectToAction("Index");
        }
    }
}
