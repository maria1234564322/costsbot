using Application.IServiсe;
using DataAccess.Entities;
using DataAccess.IRepositories;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void Add(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product));

        _productRepository.Add(product);
        _productRepository.SaveChanges();
    }

    public void Delete(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
            throw new InvalidOperationException($"Product with ID {id} not found.");

        _productRepository.Delete(id);
        _productRepository.SaveChanges();
    }

    public List<Product> GetAll()
    {
        return _productRepository.GetAll();
    }

    public void SaveChanges()
    {
        _productRepository.SaveChanges();
    }

  
}
