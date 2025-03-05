using DataAccessLayer.DbContexts;
using Microsoft.EntityFrameworkCore;
using ReviewApiApp.Domain;

namespace ReviewApiApp.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApiReviewDbContext context;

        public ProductRepository(ApiReviewDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Brand>> GetBrandForProductionAsync(int ProductId, int BrandId)
        {
            return await context.Brands.Where(b => b.ProductionId == ProductId && b.Id == BrandId).ToListAsync();
        }
            public async Task<List<Brand>> GetBrandsForProductionAsync(int ProductId)
        {
            return await context.Brands.Where(b => b.ProductionId== ProductId).ToListAsync();
        }

        public async Task<List<Production>> GetProductionAsync()
        {
            return await context.Products.OrderBy(na => na.Name).ToListAsync();
        }

        public async Task<Production?> GetProductionAsync(int ProductId)
        {
            return await context.Products.Where(d => d.Id == ProductId).FirstOrDefaultAsync();
        }
    }
}
