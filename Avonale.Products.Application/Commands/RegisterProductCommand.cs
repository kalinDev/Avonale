using Core.Messages;
using FluentValidation;

namespace Avonale.Products.Application.Commands;

public class RegisterProductCommand : Command
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public DateTime RegisteredAt { get; private set; }


    public RegisterProductCommand(string name, string description, decimal price, int stockQuantity)
    {
        Name = name;
        Description = description;
        Price = price;
        StockQuantity = stockQuantity;
        RegisteredAt = DateTime.Now;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterCustomerValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RegisterCustomerValidation : AbstractValidator<RegisterProductCommand>
{
    public RegisterCustomerValidation()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(p => p.Description)
            .NotEmpty()
            .MaximumLength(600);
        
        RuleFor(p => p.Price)
            .PrecisionScale(38, 2, false)
            .GreaterThan(0);

        RuleFor(p => p.StockQuantity)
            .GreaterThanOrEqualTo(0);

    }

}