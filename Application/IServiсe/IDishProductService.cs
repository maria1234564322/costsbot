using DataAccess.Entities;


namespace Application.IServiсe
{
    public interface IDishProductService
    {
        void Add(DishProduct dishProduct);
        void Update(DishProduct dishProduct);
        void Remove(DishProduct dishProduct);
        DishProduct? GetById(int id);
        List<DishProduct> GetAll();
        void SaveChanges();
    }
}
