using Avonale.Products.Shared.Core.DTO;

namespace Avonale.Products.Domain.Interfaces;

public interface IProductQueries
{
    Task<ProductDetailedDTO> FindOneAsync(Guid id);
    Task<IEnumerable<ProductDTO>> FindAsync();
}