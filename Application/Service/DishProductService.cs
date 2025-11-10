using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class DishProductService: IDishProductService
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

        public DishProduct? GetById(int id)
        {
            var dishProduct = _dishProductRepository.GetById(id);
            if (dishProduct == null)
                throw new InvalidOperationException($"Dish product with ID {id} not found.");

            return dishProduct;
        }

        public void Remove(DishProduct dishProduct)
        {
            var existing = _dishProductRepository.GetById(dishProductId);
            if (existing == null)
                throw new InvalidOperationException($"DishProduct with ID {dishProductId} not found.");

            _dishProductRepository.Remove(existing);
            _dishProductRepository.SaveChanges();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(DishProduct dishProduct)
        {
            throw new NotImplementedException();
        }
    }
}
