using cuna.Repository;
using cuna.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IThirdPartyService, ThirdPartyService>();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSingleton<IThirdPartyStatusRepository, MemoryRepository>();
    builder.Services.AddTransient<ITransportService, LocalTransportService>();
}
else
{
    //builder.Services.AddSingleton<IThirdPartyStatusRepository, ThirdPartyStatusRepository>();
    builder.Services.AddSingleton<IThirdPartyStatusRepository, MemoryRepository>();
    builder.Services.AddTransient<ITransportService, RemoteTransportService>();
}

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
