using MassTransit;
using MassTransitShared;
using MassTransitShared.ForGetConsumers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(Assembly.GetExecutingAssembly());
    x.SetSnakeCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
        cfg.Host(new Uri(builder.Configuration["RabbitMqConfig:RabbitMqRootUri"]), h =>
        {
            h.Username(builder.Configuration["RabbitMqConfig:UserName"]);
            h.Password(builder.Configuration["RabbitMqConfig:Password"]);
        });
    });
}).AddMassTransitHostedService();
builder.Services.AddMediator(c =>
{
    c.AddConsumers(Assembly.GetExecutingAssembly());
});
builder.Services.AddSingleton<TypesDictionary>(x => new TypesDictionary(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<MediatorAdapter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
