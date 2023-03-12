using Avonale.Products.Application.Commands;
using Avonale.Products.Domain.Interfaces;
using Avonale.Products.Shared.Core.DTO;
using Core.DomainObjects;
using Core.DomainObjects.Interfaces;
using Core.MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Avonale.Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ApiController
{
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IProductQueries _productQueries;
    
    public ProductController(INotifier notifier,
        IMediatorHandler mediatorHandler,
        IProductQueries productQueries) : base(notifier)
    {
        _mediatorHandler = mediatorHandler;
        _productQueries = productQueries;
    }

    [HttpGet("{guid:int}")]
    public async Task<ActionResult<ProductDetailedDTO>> FindOneAsync(Guid id)
    {
        var product = await _productQueries.FindOneAsync(id);
        return product == null ? NotFound() : CustomResponse(product);
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> FindAsync()
         => CustomResponse( await _productQueries.FindAsync());

    [HttpPost("")]
    public async Task<ActionResult> AddAsync([FromBody] RegisterProductCommand product)
        => CustomResponse(await _mediatorHandler.SendCommand(product));

    [HttpDelete("{guid:int}")]
    public async Task<ActionResult> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var command = new RemoveProductCommand(id);
        await _mediatorHandler.SendCommand(command, cancellationToken);
        
        return CustomResponse();
    }

    [HttpPost("purchase")]
    public async Task<ActionResult> PurchaseAsync([FromBody] PurchaseProductCommand command)
        => CustomResponse(await _mediatorHandler.SendCommand(command));
}