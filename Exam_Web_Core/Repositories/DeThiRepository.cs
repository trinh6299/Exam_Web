using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.Repositories
{
    public class DeThiRepository : GenericRepository<DeThi>
    {
        private Exam_WebEntities _context;
        public DeThiRepository(Exam_WebEntities context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<DeThi> GetDeThiByMaMonHoc(int maMonHoc)
        {
            return _context.DeThis.AsNoTracking().Where(d => d.MonHocID == maMonHoc);
            //add asnotracking
        }

        public int CountQuestionByMaDeThi(int id)
        {
            return _context.CauHois.Where(c => c.DeThis.Any(d => d.DeThiID == id)).Count();
        }
    }
}
