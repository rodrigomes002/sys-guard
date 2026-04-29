using Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<GuardWorker>();

var host = builder.Build();
host.Run();