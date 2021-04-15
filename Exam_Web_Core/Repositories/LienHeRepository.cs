using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.Repositories
{
    public class LienHeRepository:GenericRepository<LienHe>
    {
        private Exam_WebEntities _context = new Exam_WebEntities();
        public LienHeRepository(Exam_WebEntities context):base(context)
        {
            _context = context;
        }
    }
}
