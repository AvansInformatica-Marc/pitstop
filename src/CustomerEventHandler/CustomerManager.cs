namespace Pitstop.CustomerEventHandler;

using Pitstop.CustomerEventHandler.Events;

class CustomerManager : IHostedService, IMessageHandlerCallback {
    IMessageHandler _messageHandler; 

    public CustomerManager(IMessageHandler messageHandler) {
        this._messageHandler = messageHandler;
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        _messageHandler.Start(this);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        _messageHandler.Stop();
        return Task.CompletedTask;
    }
    
    public async Task<bool> HandleMessageAsync(string messageType, string message) {
        try {
            JObject messageObject = MessageSerializer.Deserialize(message);
            switch (messageType) {
                case "CustomerRegistered":
                    await HandleAsync(messageObject.ToObject<CustomerRegistered>());
                    break;
            }
        } catch (Exception ex) {
            Log.Error(ex, $"Error while handling {messageType} event.");
        }

        return true;
    }

    private Task HandleAsync(CustomerRegistered cr) {
        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff")} CustomerManager: New customer {cr.Name} ({cr.CustomerId}) was registered");
        return Task.CompletedTask;
    }
}