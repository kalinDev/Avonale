using Avonale.MessageBus;
using Avonale.Products.Domain.Entities;
using Avonale.Products.Domain.Interfaces;
using Core.Messages;
using Core.Messages.IntegrationEvent;
using FluentValidation.Results;
using MediatR;

namespace Avonale.Products.Application.Commands;

public class PurchaseProductCommandHandler : CommandHandler, IRequestHandler<PurchaseProductCommand, ValidationResult>
{
    private readonly IProductRepository _productRepository;
    private IMessageBus _bus;
    
    public PurchaseProductCommandHandler(IProductRepository productRepository, IMessageBus bus)
    {
        _productRepository = productRepository;
        _bus = bus;
    }

    public async Task<ValidationResult> Handle(PurchaseProductCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return request.ValidationResult;

        var product  = await FindProductById(request.ProductId);
        if (product is null)
            return ValidationResult;

        if (!CheckStockAvailability(product, request.Quantity))
            return ValidationResult;
        
        product.DecreaseStockQuantity(request.Quantity);
        _productRepository.Update(product);

        var price = product.Price * request.Quantity;
        var paymentIntegrationEvent = new PaymentIntegrationEvent(price, request.Card);
        
        var paymentResponse = await SendPaymentRequest(paymentIntegrationEvent);
        if (!paymentResponse.ValidationResult.IsValid) 
            return paymentResponse.ValidationResult;
        
        return await PersistData(_productRepository.UnitOfWork);
    }
    
    private async Task<Product> FindProductById(Guid productId)
    {
        var product = await _productRepository.FindByIdAsync(productId);
        if (product is null)
            AddError("Doesn't exist product with this id");
        return product;
    }
    
    private bool CheckStockAvailability(Product product, int quantity)
    {
        var hasStock = product.CheckStockAvailability(quantity);
        if (!hasStock)
            AddError("There is not enough stock to fulfill this request.");
        return hasStock;
    }
    
    private async Task<ResponseMessage> SendPaymentRequest(PaymentIntegrationEvent paymentIntegrationEvent)
    {
        var paymentResponse = await _bus.RequestAsync<PaymentIntegrationEvent, ResponseMessage>(paymentIntegrationEvent);
        return paymentResponse;
    }
}