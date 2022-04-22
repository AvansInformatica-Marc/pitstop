namespace Pitstop.CustomerEventHandler.Events;

public class CustomerRegistered : Event
{
    public readonly string CustomerId;
    public readonly string Name;

    public CustomerRegistered(Guid messageId, string customerId, string name) : base(messageId)
    {
        CustomerId = customerId;
        Name = name;
    }
}