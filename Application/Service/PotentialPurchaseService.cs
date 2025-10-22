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

        public List<PotentialPurchase> GetAllPotentialPurchases()
        {
            return _repository.GetAllPotentialPurchase();
        }
        

        public bool DeletePotentialPurchase(int id)
        {
            var purchase = _repository.GetAllPotentialPurchase()
                                      .FirstOrDefault(x => x.Id == id);

            if (purchase == null)
                return false;

            _repository.DeletePotentialPurchase(id);
            return true;
        }

        public void AddPotentialPurchase(PotentialPurchase purchase)
        {
            _repository.AddPotentialPurchase(purchase);
        }

       
    }
}

