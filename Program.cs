using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);// design pattern

// Add services to the container.

builder.Services.AddControllers(Options =>
{
    Options.ReturnHttpNotAcceptable = true; // 406 code
}

).AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();// app is a final instance

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
