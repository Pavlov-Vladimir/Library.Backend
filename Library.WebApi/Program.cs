
var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddPersistence(configuration);
services.AddDataProvidersAndServices();
services.AddAutoMapper(typeof(Program).Assembly);
services.AddValidators();

services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestMethod |
        HttpLoggingFields.RequestHeaders |
        HttpLoggingFields.RequestQuery |
        HttpLoggingFields.RequestBody |
        HttpLoggingFields.ResponseStatusCode;
    //options.MediaTypeOptions.AddText("application/javascript");
});

services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("Redis");
    options.InstanceName = "Library_";
});

//services.AddScoped<IRedisCacheService, RedisCacheMPService>();
services.AddScoped<IRedisCacheService, RedisCacheJSONService>();

services.AddControllers();

services.AddScoped<CheckIfExistsAttribute>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(options =>
{
	options.AddPolicy("LibCors1", policy =>
	{
		policy.WithOrigins("http://localhost:5173", "http://localhost:5174");
		policy.AllowAnyHeader();
		policy.AllowAnyMethod();
	});
});

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;
	try
	{
		var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
		context.Database.Migrate();
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

app.UseHttpLogging();
app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("LibCors1");

app.UseAuthorization();

app.MapControllers();

app.Run();
