using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.ChatEngine.Commands.Dto;

namespace Application.IServiсe
{
    public interface IProductService
    {
        void Add(Product product);
        List<Product> GetAll();
        void SaveChanges();
        void Delete(int id);
    }
}
