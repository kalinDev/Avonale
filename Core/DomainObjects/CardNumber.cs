using Core.Util;

namespace Core.DomainObjects;

public class CardNumber
{
    public const int NumberMaxLenght = 16;
    public const int NumberMinLenght = 15;

    public string Number { get; private set; }
    
    //For EF
    protected CardNumber()
    { }

    public CardNumber(string cardNumber)
    {
        if (!Validate(cardNumber)) throw new DomainException("Invalid CPF");
        Number = cardNumber;
    }

    public static bool Validate(string cardNumber)
    {
        cardNumber = cardNumber.OnlyNumbers(cardNumber);

        if (cardNumber.Length is not NumberMaxLenght or NumberMinLenght)
            return false;
        
        int sum = 0;
        bool alternate = false;
        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int n = int.Parse(cardNumber.Substring(i, 1));
            if (alternate)
            {
                n *= 2;
                if (n > 9)
                {
                    n = (n % 10) + 1;
                }
            }
            sum += n;
            alternate = !alternate;
        }
        
        return sum % 10 == 0;
    }
}