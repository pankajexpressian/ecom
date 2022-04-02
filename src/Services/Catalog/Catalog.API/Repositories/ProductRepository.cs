using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteOp = await _catalogContext.Products.DeleteOneAsync(p => p.Id == id);

            return deleteOp.IsAcknowledged && deleteOp.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            return await _catalogContext.Products.Find(p => p.Category.Contains(categoryName)).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _catalogContext.Products.Find(p => p.Name.Contains(name)).ToListAsync();

        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var replaceOp = await _catalogContext.Products.ReplaceOneAsync(p => p.Id == product.Id, replacement: product);

            return replaceOp.IsAcknowledged && replaceOp.ModifiedCount > 0;
        }
    }
}
