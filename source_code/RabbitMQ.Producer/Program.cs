var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(mediatRServiceConfiguration => mediatRServiceConfiguration.RegisterServicesFromAssembly(typeof(Program).Assembly));

var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqConfig>() ??
                     throw new InvalidOperationException();

builder.Services.AddSingleton(rabbitMqConfig);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();