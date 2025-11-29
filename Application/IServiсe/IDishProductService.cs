using DataAccess.Entities;
using TelegramBot.ChatEngine.Commands.Dto;


namespace Application.IServiсe
{
    public interface IDishProductService
    {
        void Add(DishProduct dishProduct);
        public DishProduct UpdateFromDto(DishProductUpdateDto dto);
        void Remove(DishProduct dishProduct);
        DishProduct? GetById(int id);
        List<DishProduct> GetAll();
        object GetAllDto();
        void SaveChanges();
       
        public DishProduct AddFromDto(DishProductCreateDto dto);
    }
}
