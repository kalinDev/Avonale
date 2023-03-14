using AutoFixture;
using Avonale.Products.Application.Commands;
using Core.DomainObjects;
using FluentAssertions;

namespace Avonale.Products.Test.Tests.ValidationsTests;

public class PurchaseProductCommandValidationTests
{
    private readonly IFixture _fixture;
    private PurchaseProductCommandValidation _validator;
    
    public PurchaseProductCommandValidationTests()
    {
        _fixture = new Fixture();
        _validator = new PurchaseProductCommandValidation();
    }
    
    [Fact(DisplayName = "PurchaseProduct Validation Should Return Error When Id is Invalid")]
    [Trait("Validation", "PurchaseProductCommand")]
    public void PurchaseProductValidation_ShouldReturnError_WhenIdIsInvalid()
    {
        // Arrange
        var card = _fixture.Create<Card>();
        var command = new PurchaseProductCommand(Guid.Empty, 10, card);

        // Act
        var result = _validator.Validate(command);
        
        // Assert
        result.Errors.Should().Contain(e => e.PropertyName == nameof(PurchaseProductCommand.ProductId));
    }
    
    [Fact(DisplayName = "PurchaseProduct Validation Should Return Success When Id is Valid")]
    [Trait("Validation", "PurchaseProductCommand")]
    public void PurchaseProductValidation_ShouldReturnSuccess_WhenIdIsValid()
    {
        // Arrange
        var card = _fixture.Create<Card>();
        var command = new PurchaseProductCommand(Guid.NewGuid(), 10, card);

        // Act
        var result = _validator.Validate(command);
        
        // Assert
        result.Errors.Should().NotContain(e => e.PropertyName == nameof(PurchaseProductCommand.ProductId));
    }
    
    [Fact(DisplayName = "PurchaseProduct Validation Should Return Error When Card is Invalid")]
    [Trait("Validation", "PurchaseProductCommand")]
    public void PurchaseProductValidation_ShouldReturnError_WhenCardIsInvalid()
    {
        // Arrange
        var card = _fixture.Create<Card>();
        var command = new PurchaseProductCommand(Guid.NewGuid(), 10, card);

        // Act
        var result = _validator.Validate(command);
        
        // Assert
        result.Errors.Should().Contain(e => e.PropertyName.StartsWith(nameof(PurchaseProductCommand.Card)));
    }
    
    [Fact(DisplayName = "PurchaseProduct Validation Should Return Success When Card is Valid")]
    [Trait("Validation", "PurchaseProductCommand")]
    public void PurchaseProductValidation_ShouldReturnSuccess_WhenCardIsValid()
    {
        // Arrange
        var card = new Card("John Smith", "6062824452860161", DateTime.Now.AddYears(1),"Visa", "123");
        var command = new PurchaseProductCommand(Guid.NewGuid(), 10, card);

        // Act
        var result = _validator.Validate(command);
        
        // Assert
        result.Errors.Should().NotContain(e => e.PropertyName.StartsWith(nameof(PurchaseProductCommand.Card)));
    }
    
    
}