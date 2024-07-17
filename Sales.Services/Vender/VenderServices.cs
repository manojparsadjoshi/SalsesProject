using Sales.Db;
using Sales.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Services.Vender
{
    public class VenderServices : IVenderServices
    {
        private readonly ApplicationDbContext _context;
        public VenderServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Create(VenderModel vender)
        {
            if(vender == null)
            {
                return false;
            }
            _context.venders.Add(vender);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var existingData = _context.venders.Find(id);
            if(existingData != null)
            {
                _context.venders.Remove(existingData);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<VenderModel> GetAll()
        {
           return _context.venders.ToList();
        }

        public VenderModel GetById(int id)
        {
            return _context.venders.Find(id);    
        }

        public bool Update(VenderModel vender)
        {
            if(vender != null)
            {
                _context.venders.Update(vender);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
