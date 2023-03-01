using Library.Business;
using Library.DataAccess;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddPersistence();
services.AddDataProvidersAndServices();

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;
	try
	{
		var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
		DatabaseService.Seed(context);
	}
	catch (Exception ex)
	{
        Console.WriteLine("An error occurred while application initialization...\n" + ex.Message);
	}
}

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
