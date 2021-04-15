using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.ViewModels
{
    public class Question_Model
    {
        public int CauHoiID { get; set; }
        public string NoiDungCauHoi { get; set; }
        public string Answer_A { get; set; }
        public string Answer_B { get; set; }
        public string Answer_C { get; set; }
        public string Answer_D { get; set; }
        public string CauTraLoiDung { get; set; }
        public string DoKho { get; set; }
        public string SelectedAnswer { get; set; }
    }
}
