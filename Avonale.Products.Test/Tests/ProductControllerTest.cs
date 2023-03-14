using AutoFixture;
using Avonale.Products.API.Controllers;
using Avonale.Products.Domain.Interfaces;
using Avonale.Products.Shared.Core.DTO;
using Core.DomainObjects.Interfaces;
using Core.MediatR;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Avonale.Products.Test.Tests;

public class ProductControllerTest
{
    private readonly Fixture _fixture;
    private readonly Mock<IMediatorHandler> _mediatorHandlerMock;
    private readonly Mock<IProductQueries> _productQueriesMock;
    private readonly Mock<INotifier> _notifierMock;
    private readonly ProductController _controller;

    public ProductControllerTest()
    {
        _fixture = new Fixture();
        _mediatorHandlerMock = new Mock<IMediatorHandler>();
        _productQueriesMock = new Mock<IProductQueries>();
        _notifierMock = new Mock<INotifier>();
        _controller = new ProductController(_notifierMock.Object, _mediatorHandlerMock.Object, _productQueriesMock.Object);
    }

    [Fact(DisplayName = "FindOneAsync Should Return NotFound When Product Does not Exists")]
    [Trait("Queries", "Product")]
    public async Task FindOneAsync_ShouldReturnsNotFound_WhenProductIsNull()
    {
        // Arrange
        var productId = Guid.NewGuid();
        _productQueriesMock.Setup(x => x.FindOneAsync(productId)).ReturnsAsync((ProductDetailedDTO)null);

        // Act
        var result = await _controller.FindOneAsync(productId);

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }
    
    [Fact(DisplayName = "FindOneAsync Should Return ProductDetailedDto When Product Exists")]
    [Trait("Queries", "Product")]
    public async Task FindOneAsync_ShouldReturnsProductDTO_WhenProductExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var productDetailedDto = _fixture.Create<ProductDetailedDTO>();
        _productQueriesMock.Setup(x => x.FindOneAsync(productId)).ReturnsAsync(productDetailedDto);

        // Act
        var result = await _controller.FindOneAsync(productId);

        // Assert
        result.Should().BeOfType<ActionResult<ProductDetailedDTO>>();

    }
    
    [Fact(DisplayName = "FindAsync Should Return IEnumerableProductDto When Product Exists")]
    [Trait("Queries", "Product")]
    public async Task FindOneAsync_ShouldIEnumerableProductDTO_WhenProductExists()
    {
        // Arrange
        var productDtos = _fixture.Build<ProductDTO>()
            .CreateMany(3);
        _productQueriesMock.Setup(x => x.FindAsync()).ReturnsAsync(productDtos);

        // Act
        var result = await _controller.FindAsync();

        // Assert
        var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
        var productDtoList = okResult.Value.Should().BeAssignableTo<IEnumerable<ProductDTO>>().Subject;
        productDtoList.Should().HaveCount(3);
    }

}