using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServiсe
{
    public interface IPotentialPurchaseService
    {
        List<PotentialPurchase> GetAllPotentialPurchases();
        
        void AddPotentialPurchase(PotentialPurchase purchase);
        bool DeletePotentialPurchase(int id);
        void AddPotentialPurchase();
    }
}
