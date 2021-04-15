using System.Collections.Generic;

namespace Exam_Web_Core.Repositories
{
    interface IGenericRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        bool Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        IEnumerable<T> Paging(IEnumerable<T> source, int pageIndex, int pageSize);
    }
}
