using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;
using TelegramBot.ChatEngine.Commands.Dto;

namespace Application.Service
{
    public class DishProductService : IDishProductService
    {
        private readonly IDishProductRepository _dishProductRepository;

        public DishProductService(IDishProductRepository dishProductRepository)
        {
            _dishProductRepository = dishProductRepository;
        }

        public void Add(DishProduct dishProduct)
        {
            if (dishProduct == null)
                throw new ArgumentNullException(nameof(dishProduct));

            _dishProductRepository.Add(dishProduct);
            _dishProductRepository.SaveChanges();
        }

        public List<DishProduct> GetAll()
        {
            return _dishProductRepository.GetAll();
        }

        public List<DishProductDto> GetAllDto()
        {
            var entities = _dishProductRepository.GetAll();

            return entities.Select(dp => new DishProductDto
            {
                Id = dp.Id,
                DishId = dp.DishId,
                DishName = dp.Dish?.Name ?? string.Empty,
                ProductId = dp.ProductId,
                ProductName = dp.Product?.Name ?? string.Empty,
                Quantity = dp.Quantity,
                Unit = dp.Unit
            }).ToList();
        }

        public DishProduct? GetById(int id)
        {
            var dishProduct = _dishProductRepository.GetById(id);
            if (dishProduct == null)
                throw new InvalidOperationException($"Dish product with ID {id} not found.");

            return dishProduct;
        }

        public void Remove(DishProduct dishProduct)
        {
            if (dishProduct == null)
                throw new ArgumentNullException(nameof(dishProduct));

            var existing = _dishProductRepository.GetById(dishProduct.Id)
                ?? throw new InvalidOperationException($"DishProduct with ID {dishProduct.Id} not found.");

            _dishProductRepository.Remove(existing);
            _dishProductRepository.SaveChanges();
        }

        public void SaveChanges()
        {
            _dishProductRepository.SaveChanges();
        }

        public DishProduct UpdateFromDto(DishProductUpdateDto dto)
        {
            var existing = _dishProductRepository.GetById(dto.Id)
                ?? throw new InvalidOperationException($"DishProduct with ID {dto.Id} not found.");

            existing.Quantity = dto.Quantity;
            existing.Unit = dto.Unit;

            if (dto.DishId.HasValue)
            {
                existing.DishId = dto.DishId.Value;
            }

            if (dto.ProductId.HasValue)
            {
                existing.ProductId = dto.ProductId.Value;
            }

            _dishProductRepository.SaveChanges();

            return existing;
        }



        object IDishProductService.GetAllDto()
        {
            return GetAllDto();
        }

        public DishProduct AddFromDto(DishProductCreateDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var entity = new DishProduct
            {
                Quantity = dto.Quantity,
                Unit = dto.Unit
            };

           
            if (dto.DishId.HasValue)
            {
                entity.DishId = dto.DishId.Value;
            }
            else
            {
               
                entity.Dish = new Dish
                {
                    Name = dto.DishName,
                    Calories = 0 
                };
            }

          
            if (dto.ProductId.HasValue)
            {
                entity.ProductId = dto.ProductId.Value;
            }
            else
            {
                entity.Product = new Product
                {
                    Name = dto.ProductName
                };
            }

            _dishProductRepository.Add(entity);
            _dishProductRepository.SaveChanges();

            return entity;
        }

      
    }
}



