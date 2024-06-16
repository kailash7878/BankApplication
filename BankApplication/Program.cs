using BankApplication.Data;
using BankApplication.Intereface;
using BankApplication.Repository;
using BankApplication.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBank, BankServices>();
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IAccount, AccountService>();

builder.Services.AddDbContext<BankAppContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connString"));
    
});

builder.Services.AddControllers().AddJsonOptions(x=>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "BankApplication");    
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();