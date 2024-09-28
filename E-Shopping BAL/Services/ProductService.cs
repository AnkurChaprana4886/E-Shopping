using E_Shopping_BAL.Dto;
using E_Shopping_BAL.Interfaces;
using E_Shopping_BAL.Models;
using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shopping_BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;
        private readonly string cacheKeyPrefix = "Product_";
        //private object cacheKeyPrefix;

        public ProductService(IProductRepository productRepository, IMemoryCache cache)
        {
            _productRepository = productRepository;
            _cache = cache;
        }
        public async Task<long> AddProduct(ProductDto product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            try
            {
                var newProduct = new Product
                {
                    VendorId = product.VendorId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    QuantityInStock = product.QuantityInStock,
                    CategoryId = product.CategoryId,
                    SubcategoryId = product.SubcategoryId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                };
                await _productRepository.Add(newProduct);
                return newProduct.ProductId;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the product.", ex);
            }
        }
        public async Task UpdateProduct(ProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentNullException(nameof(productDto));

            try
            {
                // Check if the product exists in the database
                var existingProduct = await _productRepository.GetByID(productDto.ProductId);
                if (existingProduct == null)
                    throw new KeyNotFoundException($"Product with ID {productDto.ProductId} not found.");

                // Map the updated fields from the DTO to the existing product
                existingProduct.ProductName = productDto.ProductName;
                existingProduct.Description = productDto.Description;
                existingProduct.Price = productDto.Price;
                existingProduct.QuantityInStock = productDto.QuantityInStock;
                existingProduct.CategoryId = productDto.CategoryId;
                existingProduct.SubcategoryId = productDto.SubcategoryId;
                existingProduct.UpdatedDate = DateTime.UtcNow;

                // Update the product in the repository
                await _productRepository.Update(existingProduct);

                // Optionally, you can return the updated product if needed
                // return existingProduct;
                string cacheKey = $"{cacheKeyPrefix}{productDto.ProductId}";
                _cache.Remove(cacheKey);
            }
            catch (KeyNotFoundException knfEx)
            {
                // Optionally log the exception here
                throw; // Rethrow the original exception
            }
            catch (Exception ex)
            {
                // Handle and log the exception as necessary
                throw new ApplicationException("An error occurred while updating the product.", ex);
            }
        }

        public async Task DeleteProduct(long productID)
        {
            try
            {
                await _productRepository.Delete(productID);
            }
            catch (KeyNotFoundException knfEx)
            {
                // Handle product not found scenario
                throw new ApplicationException("The product was not found.", knfEx);
            }
            catch (Exception ex)
            {
                // Handle and log general exceptions as necessary
                throw new ApplicationException("An error occurred while deleting the product.", ex);
            }
        }


        public async Task<IEnumerable<ProductDto>> GetAllProduct()
        {
            try
            {
                // Retrieve all products from the repository
                var products = await _productRepository.GetAll();
                var productDTOs = products.Select(product => new ProductDto
                {
                    ProductId = product.ProductId,
                    VendorId = product.VendorId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    QuantityInStock = product.QuantityInStock,
                    CategoryId = product.CategoryId,
                    SubcategoryId = product.SubcategoryId,
                    UpdatedDate = DateTime.UtcNow
                }).ToList();
                return productDTOs;
            }
            catch (Exception ex)
            {
                // Handle and log the exception as necessary
                throw new ApplicationException("An error occurred while retrieving the products.", ex);
            }
        }

        public async Task<ProductDto> GetProductByID(long productID)
        {
            string cacheKey = $"{cacheKeyPrefix}{productID}";

            if (!_cache.TryGetValue(cacheKey, out ProductDto productDto))
            {
                var product = await _productRepository.GetByID(productID);
                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found.");
                }

                // Convert entity to DTO
                productDto = new ProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    VendorId = product.VendorId,
                    SubcategoryId = product.SubcategoryId,
                    CategoryId = product.CategoryId,
                    QuantityInStock = product.QuantityInStock,
                };

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
                _cache.Set(cacheKey, productDto,cacheEntryOptions);
            }
            return productDto;
        }

        public async Task<IEnumerable<Product>> GetAllProductByCategory(int categoryID)
        {
            return Enumerable.Empty<Product>();
        }
        public async Task<IEnumerable<Product>> GetAllProductBySubCategory(int subCategoryID)
        {
            return Enumerable.Empty<Product>();
        }
        public async Task<IEnumerable<Product>> GetProductsByVendor(int vendorId)
        {
            return Enumerable.Empty<Product>();
        }
    }
}
