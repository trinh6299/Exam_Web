using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.Repositories
{
    public class LanThiRepository:GenericRepository<LanThi>
    {
        private Exam_WebEntities _context;
        public LanThiRepository(Exam_WebEntities context) : base(context)
        {
            _context = context;
        }
        public int GetLanThiSo(LanThi lanThi)
        {
            if (_context.LanThis.Where(l=>l.HocSinhID==lanThi.HocSinhID && l.DeThiID==lanThi.DeThiID).Count()==0)
            {
                return 1;
            }
            else
            {
                return ((int)_context.LanThis.Where(l => l.HocSinhID == lanThi.HocSinhID && l.DeThiID == lanThi.DeThiID).OrderByDescending(o => o.LanThiSo).FirstOrDefault().LanThiSo+1);
            }
        }

        public LanThi GetLastestRow()
        {
            return _context.LanThis.OrderByDescending(o => o.LanThiID).FirstOrDefault();
        }

        public IEnumerable<LanThi> GetLanThiByHocSinhID(int hocSinhID)
        {
            return _context.LanThis.Where(x => x.HocSinhID == hocSinhID);
        }
    }
}
