using FluentValidation;

namespace Core.DomainObjects;

public class Card
{
    public string Holder { get; private set; }
    public string Number { get; private set; }
    public DateTime ExpireAt { get; private set; }
    public string Brand { get; private set; }
    public string Cvv { get; private set; }

    public Card(string holder, string number, DateTime expireAt, string brand, string cvv)
    {
        Holder = holder;
        Number = number;
        ExpireAt = expireAt;
        Brand = brand;
        Cvv = cvv;
    }
    
}