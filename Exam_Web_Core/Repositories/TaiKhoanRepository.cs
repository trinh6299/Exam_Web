using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Web_Core.Repositories
{
    public class TaiKhoanRepository:GenericRepository<TaiKhoan>
    {
        private Exam_WebEntities _context;
        public TaiKhoanRepository(Exam_WebEntities context):base(context)
        {
            _context = context;
        }
        public TaiKhoan GetByUsername(string username)
        {
            return _context.TaiKhoans.Where(t => t.UserName == username).FirstOrDefault();
        }

        public TaiKhoan GetTaiKhoanByUserNameAndPassword(string username, string password)
        {
            return _context.TaiKhoans.Where(t => t.UserName == username && t.Password == password).FirstOrDefault();
        }

        public bool CheckExistsUsername(string username)
        {
            if (_context.TaiKhoans.Where(x=>x.UserName==username).ToList().Count!=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
