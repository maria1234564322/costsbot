using DataAccess.Entities;


namespace DataAccess.IRepositories;

public interface IPotentialPurchaseRepository : IRepository<PotentialPurchase>
{
   // void SaveChanges();
    List<PotentialPurchase> GetAllPotentialPurchase();
    void AddPotentialPurchase(PotentialPurchase potentialPurchase);
    void DeletePotentialPurchase(int id);
}
