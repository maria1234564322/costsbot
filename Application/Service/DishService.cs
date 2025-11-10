using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;


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

            _dishRepository.Add(dish);
            _dishRepository.SaveChanges();
        }

        public void UpdateDish(Dish dish)
        {
            if (dish == null)
                throw new ArgumentNullException(nameof(dish));

            _dishRepository.Update(dish);
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