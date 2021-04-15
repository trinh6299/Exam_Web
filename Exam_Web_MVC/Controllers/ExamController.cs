using Exam_Web_Core;
using Exam_Web_Core.Repositories;
using Exam_Web_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Exam_Web_MVC.Controllers
{
    public class ExamController : Controller
    {
        private static Exam_WebEntities _context = new Exam_WebEntities();
        DeThiRepository deThiRepository = new DeThiRepository(_context);
        CauHoiRepository cauHoiRepository = new CauHoiRepository(_context);
        DapAnDaLuaChonRepository dapAnDaLuaChonRepository = new DapAnDaLuaChonRepository(_context);
        LanThiRepository lanThiRepository = new LanThiRepository(_context);
        HocSinhRepository hocSinhRepository = new HocSinhRepository(_context);
        // GET: Exam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetailExam(int id)
        {
            ViewBag.DeThi = deThiRepository.GetById(id);
            return View();
        }

        public ActionResult DoExam(int id)
        {
            ViewBag.DeThi = deThiRepository.GetById(id);
            ViewBag.SoCauHoi = deThiRepository.CountQuestionByMaDeThi(id);
            int hocSinhID = hocSinhRepository.GetHocSinhByTaiKhoanID((int)Session["TaiKhoanID_session"]).HocSinhID;
            ViewBag.TenHS = hocSinhRepository.GetById(hocSinhID).TenHS;
            DoExam_Model viewModel = new DoExam_Model();
            List<CauHoi> cauHois = cauHoiRepository.GetCauHoiByMaDe(id).ToList();
            viewModel.DeThiID = id;
            for (int i = 0; i < cauHois.Count(); i++)
            {
                Question_Model question = new Question_Model();
                question.CauHoiID = cauHois[i].CauHoiID;
                question.NoiDungCauHoi = cauHois[i].NoiDungCauHoi;
                question.Answer_A = cauHois[i].Answer_A;
                question.Answer_B = cauHois[i].Answer_B;
                question.Answer_C = cauHois[i].Answer_C;
                question.Answer_D = cauHois[i].Answer_D;
                question.CauTraLoiDung = cauHois[i].CauTraLoiDung;
                question.DoKho = cauHois[i].DoKho.TenDoKho;

                viewModel.Questions.Add(question);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DoExam(DoExam_Model viewModel)
        {
            int hocSinhID = hocSinhRepository.GetHocSinhByTaiKhoanID((int)Session["TaiKhoanID_session"]).HocSinhID;
            HocSinh hocSinh = hocSinhRepository.GetById(hocSinhID); 
            LanThi lanThi = new LanThi();
            lanThi.HocSinhID = hocSinh.HocSinhID;
            lanThi.DeThiID = viewModel.DeThiID;
            lanThi.LanThiSo = lanThiRepository.GetLanThiSo(lanThi);
            lanThi.ThoiGianLamBai = viewModel.TimePast;
            lanThi.ThoiGianNopBai = DateTime.Now;
            //add lan thi
            lanThiRepository.Add(lanThi);
            //Nen thay session bang cai khac
            Session["lanthiid_session"] = lanThiRepository.GetLastestRow().LanThiID;
            for (int i = 0; i < viewModel.Questions.Count; i++)
            {
                DapAnDaLuaChon dapAnDaLuaChon = new DapAnDaLuaChon();
                dapAnDaLuaChon.LanThiID = lanThiRepository.GetLastestRow().LanThiID;
                dapAnDaLuaChon.CauHoiID = viewModel.Questions[i].CauHoiID;
                dapAnDaLuaChon.DapAnDaChon = viewModel.Questions[i].SelectedAnswer;
                //add ket qua thi
                dapAnDaLuaChonRepository.Add(dapAnDaLuaChon);
            }
            return RedirectToAction("Result");
        }

        public ActionResult Result()
        {
            LanThi lanThi = lanThiRepository.GetById((int)Session["lanthiid_session"]);
            List<DapAnDaLuaChon> listAnswer = dapAnDaLuaChonRepository.GetAllByLanThiID((int)Session["lanthiid_session"]).ToList();
            int NumberCorrectAnser = 0;
            for (int i = 0; i < listAnswer.Count(); i++)
            {
                string correctAnswer = cauHoiRepository.GetById((int)listAnswer[i].CauHoiID).CauTraLoiDung;
                if (listAnswer[i].DapAnDaChon == correctAnswer)
                {
                    NumberCorrectAnser++;
                }
            }
            int hocSinhID = hocSinhRepository.GetHocSinhByTaiKhoanID((int)Session["TaiKhoanID_session"]).HocSinhID;
            HocSinh hocSinh = hocSinhRepository.GetById(hocSinhID);
            ViewBag.TenHocSinh = hocSinh.TenHS;
            ViewBag.TenDe = deThiRepository.GetById((int)lanThi.DeThiID).TenDeThi;
            double diem = (double)NumberCorrectAnser*10 / deThiRepository.CountQuestionByMaDeThi((int)lanThi.DeThiID);
            diem = (double)Math.Round(diem, 2);
            ViewBag.SoCauDung = NumberCorrectAnser;
            ViewBag.TongSoCau = deThiRepository.CountQuestionByMaDeThi((int)lanThi.DeThiID);
            ViewBag.Diem = diem;
            //update diem cho lanthi
            lanThi.KetQuaThi = diem;
            lanThiRepository.Update(lanThi);
            return View();
        }
    }
}