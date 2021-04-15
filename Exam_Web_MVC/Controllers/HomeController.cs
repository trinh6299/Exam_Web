using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam_Web_Core.ViewModels;
using Exam_Web_Core;
using Exam_Web_Core.Repositories;

namespace Exam_Web_MVC.Controllers
{
    public class HomeController : Controller
    {
        private static Exam_WebEntities _context = new Exam_WebEntities();
        TaiKhoanRepository taiKhoanRepository = new TaiKhoanRepository(_context);
        HocSinhRepository hocSinhRepository = new HocSinhRepository(_context);
        DeThiRepository deThiRepository = new DeThiRepository(_context);
        MonHocRepository monHocRepository = new MonHocRepository(_context);
        LienHeRepository lienHeRepository = new LienHeRepository(_context);
        LanThiRepository lanThiRepository = new LanThiRepository(_context);
        // GET: Home
        public ActionResult Index()
        {
            return View(deThiRepository.GetAll().OrderByDescending(x=>x.DeThiID));
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                TaiKhoan user = taiKhoanRepository.GetTaiKhoanByUserNameAndPassword(taiKhoan.UserName, taiKhoan.Password);
                if (user!=null)
                {
                    Session["TaiKhoanID_session"] = user.TaiKhoanID;
                    if (user.Role == "admin")
                    {
                        return RedirectToAction("Index", "HomeAdmin", new { Area = "Admin" });
                    }
                    else if (user.Role == "student")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.Role=="teacher")
                    {
                        return RedirectToAction("Index", "HomeTeacher", new { Area = "Teacher" });
                    }
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(HocSinh_TaiKhoan_Model viewModel)
        {
            if (ModelState.IsValid)
            {
                TaiKhoan taiKhoan = new TaiKhoan();
                taiKhoan.UserName = viewModel.UserName;
                taiKhoan.Password = viewModel.Password;
                taiKhoan.Role = "student";
                taiKhoanRepository.Add(taiKhoan);

                HocSinh hocSinh = new HocSinh();
                hocSinh.TaiKhoanID = taiKhoanRepository.GetByUsername(viewModel.UserName).TaiKhoanID;
                hocSinh.TenHS = viewModel.TenHS;
                hocSinh.NgaySinh = viewModel.NgaySinh;
                hocSinh.GioiTinh = viewModel.GioiTinh;
                hocSinh.Email = viewModel.Email;
                hocSinhRepository.Add(hocSinh);
                return RedirectToAction("Login");
            }
            return View(viewModel);
        }

        public ActionResult ListExam(int id)
        {
            List<DeThiDetail_Model> listDeThiDetail = new List<DeThiDetail_Model>();
            List<DeThi> DeThis = deThiRepository.GetDeThiByMaMonHoc(id).ToList();
            for (int i = 0; i < DeThis.Count(); i++)
            {
                DeThiDetail_Model deThi = new DeThiDetail_Model();
                deThi.DeThiID = DeThis[i].DeThiID;
                deThi.MonHocID = DeThis[i].MonHocID;
                deThi.TenDeThi = DeThis[i].TenDeThi;
                deThi.ThoiGianLamBai = DeThis[i].ThoiGianLamBai;
                deThi.ThoiGianBatDauLamBai = DeThis[i].ThoiGianBatDauLamBai;
                deThi.LoaiDe = DeThis[i].LoaiDe;
                deThi.GiaoVienID = DeThis[i].GiaoVienID;
                deThi.NumberQuestion = deThiRepository.CountQuestionByMaDeThi(DeThis[i].DeThiID);
                listDeThiDetail.Add(deThi);
            }
            ViewBag.DeThi = listDeThiDetail;
            ViewBag.MonHoc = monHocRepository.GetById(id);
            return View();
        }

        public ActionResult Logout()
        {
            Session["TaiKhoanID_session"] = null;
            return RedirectToAction("Login","Home");
        }

        public ActionResult UserDetail()
        {
            UserDetail_Model model = new UserDetail_Model();
            var hocSinh = hocSinhRepository.GetHocSinhByTaiKhoanID((int)Session["TaiKhoanID_session"]);
            var taiKhoan = taiKhoanRepository.GetById((int)Session["TaiKhoanID_session"]);
            model.HocSinhID = hocSinh.HocSinhID;
            model.TenHS = hocSinh.TenHS;
            model.Email = hocSinh.Email;
            model.Username = taiKhoan.UserName;
            model.Role = taiKhoan.Role;
            model.GioiTinh = hocSinh.GioiTinh;
            DateTime ngaySinh = (DateTime)hocSinh.NgaySinh;
            model.NgaySinh = ngaySinh.ToString("yyyy-MM-dd");

            return View(model);
        }

        [HttpPost]
        public ActionResult UserDetail(UserDetail_Model model)
        {
            HocSinh hocSinh = hocSinhRepository.GetHocSinhByTaiKhoanID((int)Session["TaiKhoanID_session"]);
            TaiKhoan taiKhoan = taiKhoanRepository.GetById((int)Session["TaiKhoanID_session"]);
            hocSinh.TenHS = model.TenHS;
            hocSinh.Email = model.Email;
            hocSinh.GioiTinh = model.GioiTinh;
            hocSinh.NgaySinh = DateTime.ParseExact(model.NgaySinh, "yyyy-MM-dd", null);
            hocSinhRepository.Update(hocSinh);
            model.Role = taiKhoan.Role;
            model.Username = taiKhoan.UserName;
            //Quy uoc success la 1 
            ViewBag.Message = "1";
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePassword_Model model)
        {
            var taiKhoan = taiKhoanRepository.GetById((int)Session["TaiKhoanID_session"]);
            if (taiKhoan.Password == model.OldPassword)
            {
                taiKhoan.Password = model.NewPassword;
                taiKhoanRepository.Update(taiKhoan);
                ViewBag.AlertMessage = "1";
                return View();
            }
            else
            {
                ViewBag.AlertMessage = "0";
                return View();
            }
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(LienHe lienHe)
        {
            lienHe.NgayGui = DateTime.Now;
            lienHeRepository.Add(lienHe);
            ViewBag.Message = "1";
            return View();
        }

        public ActionResult ListResult()
        {
            HocSinh hocSinh = hocSinhRepository.GetHocSinhByTaiKhoanID((int)Session["TaiKhoanID_session"]);
            ViewBag.TenHS = hocSinh.TenHS;
            List<ListResult_Model> listResults = new List<ListResult_Model>();
            List<LanThi> listLanThi = lanThiRepository.GetLanThiByHocSinhID(hocSinh.HocSinhID).ToList();
            for (int i = 0; i < listLanThi.Count(); i++)
            {
                ListResult_Model model = new ListResult_Model();
                model.MonHoc = listLanThi[i].DeThi.MonHoc.TenMH;
                model.TenDeThi = listLanThi[i].DeThi.TenDeThi;
                model.LoaiDe = listLanThi[i].DeThi.LoaiDe;
                model.LanThi = (int)listLanThi[i].LanThiSo;
                model.ThoiGianLamBai = listLanThi[i].ThoiGianLamBai.ToString();
                model.ThoiGianNopBai = listLanThi[i].ThoiGianNopBai.ToString();
                model.Diem = (double)listLanThi[i].KetQuaThi;
                listResults.Add(model);
            }
            return View(listResults);
        }
    }
}