using System.Linq.Expressions;
using Avonale.Products.Domain.Entities;
using Core.DomainObjects.Interfaces;

namespace Avonale.Products.Domain.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate);
    Task<Product> FindByIdAsync(Guid id);
    Task<IEnumerable<Product>> FindAsync();
    void Update(Product entity);
    void Add(Product entity);
    void Remove(Product entity);
}