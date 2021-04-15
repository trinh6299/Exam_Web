using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam_Web_Core.Repositories;

namespace Exam_Web_Core.Repositories
{
    public class CauHoiRepository : GenericRepository<CauHoi>
    {
        //private Exam_WebEntities _context = new Exam_WebEntities();
        private Exam_WebEntities _context = new Exam_WebEntities();
        public CauHoiRepository(Exam_WebEntities context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<CauHoi> GetCauHoiByMaDe(int maDe)
        {
            var result = from cauHoi in _context.CauHois
                         where cauHoi.DeThis.Any(d => d.DeThiID == maDe)
                         select cauHoi;
            return result;
        }

        public IEnumerable<CauHoi> GetCauHoiForDeThi(int deThiId, int monHocId)
        {
            var allQuestionInMonHoc = from cauHoi in _context.CauHois.AsNoTracking()
                                      where cauHoi.MonHocID == monHocId
                                      select cauHoi;
            var allQuestionInDeThi = from cauHoi in _context.CauHois.AsNoTracking()
                                     where cauHoi.DeThis.Any(d => d.DeThiID == deThiId)
                                     select cauHoi;
            var result = allQuestionInMonHoc.Except(allQuestionInDeThi);
            return result;
        }

        public IEnumerable<CauHoi> FindCauHoi(string searchString)
        {
            return _context.CauHois.AsNoTracking().Where(x => x.NoiDungCauHoi.Contains(searchString));
        }

        public IEnumerable<CauHoi> GetRandomCauHoiByDoKho(int doKhoId, int monHocId, int numberOfQuestion)
        {
            return _context.CauHois.Where(x => x.MonHocID == monHocId && x.DoKhoID == doKhoId).OrderBy(x => Guid.NewGuid()).Take(numberOfQuestion);
        }
    }
}
