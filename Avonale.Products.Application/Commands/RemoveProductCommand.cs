using Core.Messages;

namespace Avonale.Products.Application.Commands;

public class RemoveProductCommand : Command
{
    public Guid Id { get; private set; }

    public RemoveProductCommand(Guid id)
    {
        Id = id;
    }
}