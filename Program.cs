using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using MyMicroservice.Data;
using MyMicroservice.Security;
using MyMicroservice.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

var auth = new JwtAuthenticationService(builder.Configuration);
auth.ConfigureAuthentication(builder.Services);
auth.ConfigureAuthorization(builder.Services);

builder.Services.AddAuthorization();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductsDBContext>((s) => new ProductsDBContext());
builder.Services.AddDbContext<ParkingLotDBContext>();
builder.Services.AddScoped<ParkingLotInitializer>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<ParkingLotInitializer>().SeedData();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    IdentityModelEventSource.ShowPII = true;
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    Console.WriteLine("Hello from Middleware:/");
    await next.Invoke();
});

app.Run();
