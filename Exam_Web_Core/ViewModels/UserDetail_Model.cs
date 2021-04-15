using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.ViewModels
{
    public class UserDetail_Model
    {
        public int HocSinhID { get; set; }
        public string TenHS { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public bool? GioiTinh { get; set; }
        public string NgaySinh { get; set; }
    }
}
