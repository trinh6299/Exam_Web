using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.ViewModels
{
    public class DeThiDetail_Model
    {
        public int DeThiID { get; set; }
        public Nullable<int> MonHocID { get; set; }
        public string TenDeThi { get; set; }
        public Nullable<int> ThoiGianLamBai { get; set; }
        public Nullable<System.DateTime> ThoiGianBatDauLamBai { get; set; }
        public string LoaiDe { get; set; }
        public Nullable<int> GiaoVienID { get; set; }
        public int NumberQuestion { get; set; }
    }
}
