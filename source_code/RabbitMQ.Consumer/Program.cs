var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<Context>(opcoes => opcoes.UseNpgsql());
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMqConfig>() ??
                     throw new InvalidOperationException();

builder.Services.AddSingleton(rabbitMqConfig);
builder.Services.AddHostedService<LogConsumerService>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));

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