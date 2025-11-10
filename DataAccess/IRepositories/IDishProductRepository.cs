using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IDishProductRepository
    {
        void Add(DishProduct dishProduct);
        void Update(DishProduct dishProduct);
        DeleteDishProduct(int id);
        DishProduct? GetById(int id);
        List<DishProduct> GetAll();
        void SaveChanges();
    }
}
