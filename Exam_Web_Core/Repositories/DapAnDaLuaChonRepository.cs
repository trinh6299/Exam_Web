using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.Repositories
{
    public class DapAnDaLuaChonRepository:GenericRepository<DapAnDaLuaChon>
    {
        private Exam_WebEntities _context;
        public DapAnDaLuaChonRepository(Exam_WebEntities context) : base(context)
        {
            _context = context;
        }
        
        public IEnumerable<DapAnDaLuaChon> GetAllByLanThiID(int LanThiID)
        {
            return _context.DapAnDaLuaChons.Where(k => k.LanThiID == LanThiID);
        }
    }
}
