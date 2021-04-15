using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Exam_Web_Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly Exam_WebEntities _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(Exam_WebEntities context)
        {
            //_context = new Exam_WebEntities();
            //_dbSet = _context.Set<T>();
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public bool Add(T entity)
        {
            _dbSet.Add(entity);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
            //asnostracking để sửa 1 lỗi là khi update dữ liệu vào db mà getall() vẫn cho ra dữ liệu cũ,
            //vì đang dùng chung 1 dbcontext
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            _dbSet.AddOrUpdate(entity);
            return _context.SaveChanges() > 0;
        }
        public IEnumerable<T> Paging(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}
