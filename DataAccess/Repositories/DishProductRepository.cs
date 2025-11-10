using DataAccess.Entities;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class DishProductRepository : IDishProductRepository
    {
        private readonly ApplicationDbContext _context;

        public DishProductRepository (ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(DishProduct dishProduct)
        {
            _context.DishProducts.Add(dishProduct);
        }

        public List<DishProduct> GetAll()
        {
            throw new NotImplementedException();
        }

        public DishProduct? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteDishProduct(DishProduct dishProduct)
        {
            if (dishProduct == null)
                throw new ArgumentNullException(nameof(dishProduct));

            _context.DishProducts.Remove(dishProduct);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(DishProduct dishProduct)
        {
            _context.DishProducts.Update(dishProduct);
           
        }
    }
}
