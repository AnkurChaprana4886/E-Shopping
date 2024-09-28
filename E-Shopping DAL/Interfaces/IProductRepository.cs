using E_Shopping_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_DAL.Interfaces
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(long productID);
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByID(long productID);
        Task<IEnumerable<Product>> GetAllByCategory(int categoryID);
        Task<IEnumerable<Product>> GetAllBySubCategory(int subCategoryID);
        Task<IEnumerable<Product>> GetByVendor(int vendorId);

    }
}
