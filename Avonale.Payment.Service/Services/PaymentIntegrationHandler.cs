using Avonale.MessageBus;
using Avonale.Payment.Application.Commands;
using Core.MediatR;
using Core.Messages.IntegrationEvent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Avonale.Payment.Service.Services
{
    public class PaymentIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;
        
        public PaymentIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }
        
        private void SetResponder()
        {
            _bus.RespondAsync<PaymentIntegrationEvent, ResponseMessage>(async request
                => await Pay(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }
        
        private async Task<ResponseMessage> Pay(PaymentIntegrationEvent message)
        {
            var payCommand = new PaymentCommand(message.Price, message.Card);
            
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            var success = await mediator.SendCommand(payCommand);

            return new ResponseMessage(success);
        }
    }
}