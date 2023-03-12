using System.Linq.Expressions;
using Avonale.Products.Domain.Entities;
using Avonale.Products.Domain.Interfaces;
using Core.DomainObjects.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Avonale.Products.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductContext _context;

    public ProductRepository(ProductContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Product> FindByIdAsync(Guid id)
        => await _context.Products.FindAsync(id);

    public async Task<IEnumerable<Product>> FindAsync()
        => await _context.Products.AsNoTracking().ToListAsync();

    public void Update(Product product)
        => _context.Products.Update(product);

    public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
        => await _context.Products.AsNoTracking().Where(predicate).ToListAsync();

    public void Add(Product entity)
        => _context.Products.Add(entity);

    public void Remove(Product product)
        => _context.Products.Remove(product);
    
    public void Dispose()
        => _context?.Dispose();
}