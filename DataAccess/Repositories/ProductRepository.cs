using DataAccess.Entities;
using DataAccess.IRepositories;


namespace DataAccess.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                throw new KeyNotFoundException($"Product with id {id} not found");

            _context.Products.Remove(product);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
