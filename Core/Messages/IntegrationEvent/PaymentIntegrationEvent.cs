using Core.DomainObjects;

namespace Core.Messages.IntegrationEvent;

public class PaymentIntegrationEvent : IntegrationEvent
{
    public decimal Price { get; private set; }

    public Card Card { get; private set; }

    public PaymentIntegrationEvent(decimal price, Card card)
    {
        Price = price;
        Card = card;
    }
}