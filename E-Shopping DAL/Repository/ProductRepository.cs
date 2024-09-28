using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly EshoppingContext _context;

        public ProductRepository(EshoppingContext context)
        {
            _context = context;
        }

        public async Task Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetByID(long productID)
        {
            var product = await _context.Products.FindAsync(productID);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }
            return product;
        }

        public async Task Delete(long productID)
        {
            var product = await GetByID(productID);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Product not found.");
            }
        }


        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByCategory(int categoryID)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryID).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllBySubCategory(int subCategoryID)
        {
            return await _context.Products.Where(p => p.SubcategoryId == subCategoryID).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByVendor(int vendorId)
        {
            return await _context.Products.Where(p => p.VendorId == vendorId).ToListAsync();
        }
    }
}
