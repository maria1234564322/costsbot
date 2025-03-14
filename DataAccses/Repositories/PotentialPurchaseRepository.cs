using DataAccess.Entities;
using DataAccess.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PotentialPurchaseRepository : Repository<PotentialPurchase>, IPotentialPurchaseRepository
    {
        private readonly ApplicationDbContext _context;

        public PotentialPurchaseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

       // public void SaveChanges()
       // {
        //    _context.SaveChanges();
        //}

        public List<PotentialPurchase> GetAllPotentialPurchase()
        {
         return _context.PotentialPurchase.ToList();
        }

    

        public void AddPotentialPurchase(PotentialPurchase potentialPurchase)
        {
            _context.PotentialPurchase.Add(potentialPurchase);
            _context.SaveChanges();
        }

        public void DeletePotentialPurchase(int id)
        {
            var purchase = _context.PotentialPurchase.Find(id);
            if (purchase != null)
            {
                _context.PotentialPurchase.Remove(purchase);
                _context.SaveChanges();
            }
        }
    }

}
