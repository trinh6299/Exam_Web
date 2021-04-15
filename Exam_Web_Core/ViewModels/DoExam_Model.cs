using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.ViewModels
{
    public class DoExam_Model
    {
        public int DeThiID { get; set; }
        public int TimePast { get; set; }
        public List<Question_Model> Questions { get; set; }
        public DoExam_Model()
        {
            Questions = new List<Question_Model>();
        }
    }
}
