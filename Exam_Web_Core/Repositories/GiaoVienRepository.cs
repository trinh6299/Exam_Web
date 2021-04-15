using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.Repositories
{
    public class GiaoVienRepository:GenericRepository<GiaoVien>
    {
        public GiaoVienRepository(Exam_WebEntities context):base(context)
        {

        }
    }
}
