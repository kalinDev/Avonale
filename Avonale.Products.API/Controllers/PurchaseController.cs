using Avonale.Products.Application.Commands;
using Core.DomainObjects;
using Core.DomainObjects.Interfaces;
using Core.MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Avonale.Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ApiController
{
    private readonly IMediatorHandler _mediatorHandler;
    
    public PurchaseController(INotifier notifier,
        IMediatorHandler mediatorHandler) : base(notifier)
    {
        _mediatorHandler = mediatorHandler;
    }

    [HttpPost("")]
    public async Task<ActionResult> PurchaseAsync([FromBody] PurchaseProductCommand command)
        => CustomResponse(await _mediatorHandler.SendCommand(command));
}