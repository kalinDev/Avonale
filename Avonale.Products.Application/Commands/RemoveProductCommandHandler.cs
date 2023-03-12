using Avonale.Products.Domain.Interfaces;
using Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace Avonale.Products.Application.Commands;

public class RemoveProductCommandHandler : CommandHandler, IRequestHandler<RemoveProductCommand, ValidationResult>
{
    private readonly IProductRepository _productRepository;

    public RemoveProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ValidationResult> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindByIdAsync(request.Id);

        if (product is null)
        {
            AddError("product doesn't exist");
            return ValidationResult;
        }
        
        _productRepository.Remove(product); 
        
        return await PersistData(_productRepository.UnitOfWork, cancellationToken);
    }

}