using Core.DomainObjects;
using Core.DomainObjects.Interfaces;

namespace Avonale.Products.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    public Product(string name, string description, decimal price, int stockQuantity, DateTime registeredAt)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        RegisteredAt = registeredAt;
    }
    
    public bool CheckStockAvailability(int quantity)
    {
        return StockQuantity >= quantity;
    }
    
    public void DecreaseStockQuantity(int quantity)
    {
        if (StockQuantity >= quantity)
        {
            StockQuantity -= quantity;
        }
        else
        {
            throw new Exception("There is not enough stock to fulfill this request.");
        }
    } 
}