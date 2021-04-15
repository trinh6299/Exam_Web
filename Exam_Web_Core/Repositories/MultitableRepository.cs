using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.Repositories
{
    public class MultitableRepository
    {
        private readonly Exam_WebEntities _context;
        public MultitableRepository()
        {
            _context = new Exam_WebEntities();
        }
        IEnumerable<CauHoi> GetCauHoiByMaDe(int id)
        {
            var result = from cauHoi in _context.CauHois
                         where cauHoi.DeThis.Any(d => d.DeThiID == id)
                         select cauHoi;
            return result;
        }
    }
}
