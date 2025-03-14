using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class PotentialPurchaseService: IPotentialPurchaseService
    {
        private readonly IPotentialPurchaseRepository _repository;

        public PotentialPurchaseService(IPotentialPurchaseRepository repository)
        {
            _repository = repository;
        }

        public void AddPotentialPurchase(string name)
        {
            var purchase = new PotentialPurchase { Name = name };
            _repository.AddPotentialPurchase(purchase);
        }


        public List<PotentialPurchase> GetAllPotentialPurchases()
        {
            return _repository.GetAllPotentialPurchase();
        }
        public void DeletePotentialPurchase(int id)
        {
            _repository.DeletePotentialPurchase(id);
        }
    }
}

