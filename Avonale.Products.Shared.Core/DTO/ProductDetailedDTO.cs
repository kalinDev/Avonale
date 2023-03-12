namespace Avonale.Products.Shared.Core.DTO;

public record ProductDetailedDTO(Guid Id, string Name, string Description,
    decimal Price, int StockQuantity, DateTime RegisteredAt);
