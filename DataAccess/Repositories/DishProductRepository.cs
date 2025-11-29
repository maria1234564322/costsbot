using DataAccess.Entities;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class DishProductRepository : IDishProductRepository
    {
        private readonly ApplicationDbContext _context;

        public DishProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(DishProduct dishProduct)
        {
            if (dishProduct == null)
                throw new ArgumentNullException(nameof(dishProduct));

            if (dishProduct.Dish != null)
            {
             
                var existingDish = _context.Dishes
                    .FirstOrDefault(d => d.Name.ToLower() == dishProduct.Dish.Name.ToLower());

                if (existingDish != null)
                {
                    
                    dishProduct.DishId = existingDish.Id;
                    dishProduct.Dish = null;
                }
                else
                {
                    
                    dishProduct.Dish.Id = 0; 
                    _context.Dishes.Add(dishProduct.Dish);
                    _context.SaveChanges();

                    dishProduct.DishId = dishProduct.Dish.Id;
                    dishProduct.Dish = null; 
                }
            }

            if (dishProduct.Product != null)
            {
                var existingProduct = _context.Products
                    .FirstOrDefault(p => p.Name.ToLower() == dishProduct.Product.Name.ToLower());

                if (existingProduct != null)
                {
                    dishProduct.ProductId = existingProduct.Id;
                    dishProduct.Product = null;
                }
                else
                {
                    dishProduct.Product.Id = 0;
                    _context.Products.Add(dishProduct.Product);
                    _context.SaveChanges();

                    dishProduct.ProductId = dishProduct.Product.Id;
                    dishProduct.Product = null; 
                }
            }

            _context.DishProducts.Add(dishProduct);
        }


        public List<DishProduct> GetAll()
        {
            return _context.DishProducts
                .Include(dp => dp.Dish)
                .Include(dp => dp.Product)
                .ToList();
        }

        public DishProduct? GetById(int id)
        {
            return _context.DishProducts
                .Include(dp => dp.Dish)
                .Include(dp => dp.Product)
                .FirstOrDefault(dp => dp.Id == id);
        }

        public void SaveChanges() => _context.SaveChanges();

        public void Update(DishProduct dishProduct)
        {
            _context.DishProducts.Update(dishProduct);
        }

        public void Remove(DishProduct dishProduct)
        {
            if (dishProduct == null)
                throw new ArgumentNullException(nameof(dishProduct));

            _context.DishProducts.Remove(dishProduct);
        }
    }
}

