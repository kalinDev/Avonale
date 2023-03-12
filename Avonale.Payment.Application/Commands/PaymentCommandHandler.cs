using Core.Messages;
using FluentValidation.Results;
using MediatR;

namespace Avonale.Payment.Application.Commands;

public class PaymentCommandHandler : CommandHandler, IRequestHandler<PaymentCommand, ValidationResult>
{

    public Task<ValidationResult> Handle(PaymentCommand request, CancellationToken cancellationToken)
    {
        request.IsValid();
        
        // If there was a real payment flow, it would be here
        
        return Task.FromResult(ValidationResult);
    }
}