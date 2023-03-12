using Avonale.Products.Domain.Entities;
using Avonale.Products.Domain.Interfaces;
using Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace Avonale.Products.Application.Commands;

public class RegisterProductCommandHandler : CommandHandler, IRequestHandler<RegisterProductCommand, ValidationResult>
{
    private readonly IProductRepository _productRepository;

    public RegisterProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ValidationResult> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        var product = new Product(request.Name, request.Description, request.Price,
            request.StockQuantity, request.RegisteredAt);
        
        _productRepository.Add(product); 
        
        return await PersistData(_productRepository.UnitOfWork);
    }
}