using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IDishRepository
    {
        void Add(Dish dish);
        void Update(Dish dish);
        void Remove(Dish dish);
        Dish? GetById(int id);
        List<Dish> GetAll();
        void SaveChanges();
    }
}
