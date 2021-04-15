using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.ViewModels
{
    public class GiaoVien_TaiKhoan_Model
    {
        //GiaoVien
        public int GiaoVienID { get; set; }
        public Nullable<int> TaiKhoanID { get; set; }
        public string TenGV { get; set; }
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public Nullable<bool> GioiTinh { get; set; }
        public string Email { get; set; }
        public Nullable<int> MonHocID { get; set; }
        public Nullable<int> HocViID { get; set; }

        //TaiKhoan
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
