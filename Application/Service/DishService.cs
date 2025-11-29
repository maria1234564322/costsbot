using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace Application.Service
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;

        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public void AddDish(Dish dish)
        {
            if (dish == null)
                throw new ArgumentNullException(nameof(dish));

          
            if (dish.Id == 0)
            {
                var dishes = _dishRepository.GetAll();
                dish.Id = dishes.Any() ? dishes.Max(d => d.Id) + 1 : 1;
            }

          
            var allDishProducts = _dishRepository.GetAll()
                .SelectMany(d => d.DishProducts)
                .ToList();

            foreach (var dp in dish.DishProducts)
            {
                if (dp.Id == 0)
                    dp.Id = allDishProducts.Any() ? allDishProducts.Max(x => x.Id) + 1 : 1;

                
                if (dp.Product != null && dp.Product.Id > 0)
                {
                   
                    var context = _dishRepository.GetDbContext();
                    context.Entry(dp.Product).State = EntityState.Unchanged;
                }
            }

            _dishRepository.Add(dish);
            _dishRepository.SaveChanges();
        }


        public void UpdateDish(Dish dish)
        {
            if (dish == null)
                throw new ArgumentNullException(nameof(dish));

            
            var existingDish = _dishRepository.GetById(dish.Id);
            if (existingDish == null)
                throw new InvalidOperationException($"Dish with ID {dish.Id} not found.");

      
            existingDish.Name = dish.Name;
            existingDish.Calories = dish.Calories;

            
            existingDish.DishProducts.Clear();
            foreach (var dp in dish.DishProducts)
            {
                existingDish.DishProducts.Add(new DishProduct
                {
                    ProductId = dp.ProductId,
                    Quantity = dp.Quantity,
                    Unit = dp.Unit
                });
            }

           
            _dishRepository.SaveChanges();
        }



        public void DeleteDish(int id)
        {
            var dish = _dishRepository.GetById(id);
            if (dish == null)
                throw new InvalidOperationException($"Dish with ID {id} not found.");

            _dishRepository.Remove(dish);
            _dishRepository.SaveChanges();
        }

        public List<Dish> GetAllDishes()
        {
            return _dishRepository.GetAll();
        }

        public Dish GetDishById(int id)
        {
            var dish = _dishRepository.GetById(id);
            if (dish == null)
                throw new InvalidOperationException($"Dish with ID {id} not found.");

            return dish;
        }
    }
}