using AutoFixture;
using AutoMapper;
using Avonale.Products.Application.AutoMapper;
using Avonale.Products.Application.Queries;
using Avonale.Products.Domain.Entities;
using Avonale.Products.Domain.Interfaces;
using Avonale.Products.Shared.Core.DTO;
using FluentAssertions;
using Moq;

namespace Avonale.Products.Test.Tests;

public class ProductQueriesTest
{
    public class ProductQueriesTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ProductQueries _productQueries;

        public ProductQueriesTests()
        {
            _fixture = new Fixture();
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            }));
            _productQueries = new ProductQueries(_productRepositoryMock.Object, _mapper);
        }

        [Fact(DisplayName = "FindOneAsync Should Return ProductDetailedDto When Product Exists")]
        [Trait("Queries", "Product")]
        public async void FindOneAsync_ShouldReturnProductDetailedDTO_WhenProductExists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = _fixture.Create<Product>();
            var productDetailedDtoExpected = _mapper.Map<ProductDetailedDTO>(product);
            _productRepositoryMock.Setup(x => x.FindByIdAsync(productId)).ReturnsAsync(product);

            // Act
            var result = await _productQueries.FindOneAsync(productId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(productDetailedDtoExpected);
        }
        
        [Fact(DisplayName = "FindAsync Should Return IEnumerable of ProductDTO When Products Exists")]
        [Trait("Queries", "Product")]
        public async void FindAsync_ShouldReturnIEnumerableOfProductDTO_WhenProductsExists()
        {
            // Arrange
            var products = _fixture.Build<Product>()
                .CreateMany(3);
            
            var productDTOsExpected = _mapper.Map<IEnumerable<ProductDTO>>(products);
            _productRepositoryMock.Setup(x => x.SearchAsync(p => p.StockQuantity > 0)).ReturnsAsync(products);

            // Act
            var result = await _productQueries.FindAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveSameCount(products);
            result.Should().BeEquivalentTo(productDTOsExpected, options => options.ExcludingMissingMembers());

            foreach (var productDTO in result)
            {
                var product = products.FirstOrDefault(p => p.Id == productDTO.Id);

                product.Should().NotBeNull();
                if (product != null)
                {
                    productDTO.Name.Should().Be(product.Name);
                    productDTO.Price.Should().Be(product.Price);
                    productDTO.StockQuantity.Should().Be(product.StockQuantity);
                }
            }
        }
    }

}