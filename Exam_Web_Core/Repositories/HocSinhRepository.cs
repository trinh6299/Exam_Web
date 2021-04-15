using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.Repositories
{
    public class HocSinhRepository:GenericRepository<HocSinh>
    {
        private Exam_WebEntities _context;
        public HocSinhRepository(Exam_WebEntities context):base(context)
        {
            _context = context;
        }
        public HocSinh GetHocSinhByTaiKhoanID(int TaiKhoanID)
        {
            return _context.HocSinhs.Where(t => t.TaiKhoanID == TaiKhoanID).FirstOrDefault();
        }
    }
}
