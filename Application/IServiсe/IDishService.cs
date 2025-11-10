using DataAccess.Entities;


namespace Application.IServiсe
{
    public interface IDishService
    {
        void AddDish(Dish dish);
        void UpdateDish(Dish dish);
        void DeleteDish(int id);
        List<Dish> GetAllDishes();
        Dish GetDishById(int id);
    }
}
