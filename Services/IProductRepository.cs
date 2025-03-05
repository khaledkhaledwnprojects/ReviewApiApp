using ReviewApiApp.Domain;

namespace ReviewApiApp.Services
{
    public interface IProductRepository
    {
       Task<List<Production>> GetProductionAsync();
       Task<Production?> GetProductionAsync(int ProductId);

       Task<List<Brand>> GetBrandsForProductionAsync(int ProductId);
       Task<List<Brand>> GetBrandForProductionAsync(int ProductId, int BrandId);
    }
}
