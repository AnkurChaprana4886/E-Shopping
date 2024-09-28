using E_Shopping_BAL.Dto;
using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Interfaces
{
    public interface IProductService
    {
        Task<long> AddProduct(ProductDto product);
        Task UpdateProduct(ProductDto product);
        Task DeleteProduct(long productID);
        Task<IEnumerable<ProductDto>> GetAllProduct();
        Task<ProductDto> GetProductByID(long productID);
        Task<IEnumerable<Product>> GetAllProductByCategory(int categoryID);
        Task<IEnumerable<Product>> GetAllProductBySubCategory(int subCategoryID);
        Task<IEnumerable<Product>> GetProductsByVendor(int vendorId);
    }
}
