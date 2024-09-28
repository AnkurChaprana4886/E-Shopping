using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Caching.Memory;
using E_Shopping_BAL.Services;
using E_Shopping_DAL.Entities;
using E_Shopping_DAL.Interfaces;
using E_Shopping_BAL.Dto;

namespace E_Shopping_Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMemoryCache> _cacheMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _cacheMock = new Mock<IMemoryCache>();
            _productService = new ProductService(_productRepositoryMock.Object, _cacheMock.Object);
        }

        #region AddProduct Tests

        [Fact]
        public async Task AddProduct_ShouldThrowArgumentNullException_WhenProductIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _productService.AddProduct(null));
        }

        [Fact]
        public async Task AddProduct_ShouldCallRepository_WhenProductIsValid()
        {
            // Arrange
            var productDto = new ProductDto { ProductName = "Test Product", VendorId = 1 };

            // Act
            await _productService.AddProduct(productDto);

            // Assert
            _productRepositoryMock.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Once);
        }

        #endregion

        #region UpdateProduct Tests

        [Fact]
        public async Task UpdateProduct_ShouldThrowArgumentNullException_WhenProductDtoIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _productService.UpdateProduct(null));
        }

        [Fact]
        public async Task UpdateProduct_ShouldThrowKeyNotFoundException_WhenProductDoesNotExist()
        {
            // Arrange
            var productDto = new ProductDto { ProductId = 1 };
            _productRepositoryMock.Setup(repo => repo.GetByID(1)).ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.UpdateProduct(productDto));
        }

        [Fact]
        public async Task UpdateProduct_ShouldUpdateProduct_WhenProductIsValid()
        {
            // Arrange
            var existingProduct = new Product { ProductId = 1, ProductName = "Old Product" };
            _productRepositoryMock.Setup(repo => repo.GetByID(1)).ReturnsAsync(existingProduct);
            var productDto = new ProductDto { ProductId = 1, ProductName = "New Product" };

            // Act
            await _productService.UpdateProduct(productDto);

            // Assert
            _productRepositoryMock.Verify(repo => repo.Update(It.Is<Product>(p => p.ProductName == "New Product")), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ShouldInvalidateCache_WhenProductIsUpdated()
        {
            // Arrange
            var existingProduct = new Product { ProductId = 1 };
            _productRepositoryMock.Setup(repo => repo.GetByID(1)).ReturnsAsync(existingProduct);
            var productDto = new ProductDto { ProductId = 1, ProductName = "New Product" };

            // Act
            await _productService.UpdateProduct(productDto);

            // Assert
            _cacheMock.Verify(cache => cache.Remove(It.IsAny<string>()), Times.Once);
        }

        #endregion

        #region DeleteProduct Tests

        [Fact]
        public async Task DeleteProduct_ShouldThrowApplicationException_WhenProductNotFound()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.Delete(It.IsAny<long>())).ThrowsAsync(new KeyNotFoundException());

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _productService.DeleteProduct(1));
        }

        [Fact]
        public async Task DeleteProduct_ShouldCallRepository_WhenProductIsValid()
        {
            // Act
            await _productService.DeleteProduct(1);

            // Assert
            _productRepositoryMock.Verify(repo => repo.Delete(1), Times.Once);
        }

        #endregion

        #region GetAllProduct Tests

        [Fact]
        public async Task GetAllProduct_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Product>());

            // Act
            var result = await _productService.GetAllProduct();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllProduct_ShouldReturnProducts_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Product 1" },
                new Product { ProductId = 2, ProductName = "Product 2" }
            };
            _productRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProduct();

            // Assert
            Assert.Equal(2, result.Count());
        }

        #endregion

        #region GetProductByID Tests

        [Fact]
        public async Task GetProductByID_ShouldThrowKeyNotFoundException_WhenProductNotFound()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetByID(1)).ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.GetProductByID(1));
        }

        [Fact]
        public async Task GetProductByID_ShouldReturnProductFromCache_WhenProductIsInCache()
        {
            // Arrange
            var productDto = new ProductDto { ProductId = 1, ProductName = "Cached Product" };
            object cacheValue = productDto;

            // Mocking TryGetValue to simulate cache hit
            _cacheMock
                .Setup(x => x.TryGetValue(It.IsAny<object>(), out cacheValue))
                .Returns(true);

            // Act
            var result = await _productService.GetProductByID(1);

            // Assert
            Assert.Equal("Cached Product", result.ProductName);
            _productRepositoryMock.Verify(repo => repo.GetByID(It.IsAny<long>()), Times.Never); // Verify repository is not called
        }

        [Fact]
        public async Task GetProductByID_ShouldReturnProductFromRepository_WhenNotInCache()
        {
            // Arrange
            var productEntity = new Product { ProductId = 1, ProductName = "New Product" };
            ProductDto productDto = new ProductDto { ProductId = 1, ProductName = "New Product" };

            // Mocking the cache to return false (not found in cache)
            object cacheValue = productDto;
            _cacheMock.Setup(cache => cache.TryGetValue(It.IsAny<object>(), out cacheValue)).Returns(false);

            // Mocking the repository to return a product when not found in cache
            _productRepositoryMock.Setup(repo => repo.GetByID(1)).ReturnsAsync(productEntity);

            // Mocking the Set method to prevent null references
            _cacheMock.Setup(cache => cache.Set(It.IsAny<object>(), It.IsAny<ProductDto>(), It.IsAny<MemoryCacheEntryOptions>()));

            // Act
            var result = await _productService.GetProductByID(1);

            // Assert
            Assert.Equal("New Product", result.ProductName);
            _productRepositoryMock.Verify(repo => repo.GetByID(1), Times.Once); // Verify that the repository was called once
            _cacheMock.Verify(cache => cache.Set(It.IsAny<object>(), It.IsAny<ProductDto>(), It.IsAny<MemoryCacheEntryOptions>()), Times.Once); // Verify that the product was added to cache
        }




        #endregion
    }
}
