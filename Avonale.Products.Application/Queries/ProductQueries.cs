using AutoMapper;
using Avonale.Products.Domain.Entities;
using Avonale.Products.Domain.Interfaces;
using Avonale.Products.Shared.Core.DTO;

namespace Avonale.Products.Application.Queries;

public class ProductQueries : IProductQueries
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public ProductQueries(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDetailedDTO> FindOneAsync(Guid id) 
        => _mapper.Map<ProductDetailedDTO>(await _productRepository.FindByIdAsync(id));

    public async Task<IEnumerable<ProductDTO>> FindAsync()
    {
        var products = await _productRepository.SearchAsync(p => p.StockQuantity > 0);
        return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(products);
    }
}