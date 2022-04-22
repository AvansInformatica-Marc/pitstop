IHost host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) => {
        services.UseRabbitMQMessageHandler(hostContext.Configuration);

        services.AddHostedService<CustomerManager>();
    })
    .UseSerilog((hostContext, loggerConfiguration) => {
        loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();