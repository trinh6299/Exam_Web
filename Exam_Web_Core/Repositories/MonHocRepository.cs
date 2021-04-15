namespace Exam_Web_Core.Repositories
{
    public class MonHocRepository : GenericRepository<MonHoc>
    {
        private static Exam_WebEntities _context;
        public MonHocRepository(Exam_WebEntities context):base(context)
        {
            _context = context;
        }
    }
}
