using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IProductRepository
    {
        void Add(Product product);
        List<Product> GetAll();
        void SaveChanges();
        void Delete(int id);
        Product GetById(int id);
    }
}
