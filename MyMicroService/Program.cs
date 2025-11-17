using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using MyMicroservice.Data;
using MyMicroservice.Security;
using MyMicroservice.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

var auth = new JwtAuthenticationService(builder.Configuration);
auth.ConfigureAuthentication(builder.Services);
auth.ConfigureAuthorization(builder.Services);

builder.Services.AddAuthorization();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SecureSwaggenGenOption>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ProductsDBContext>((s) => new ProductsDBContext());
builder.Services.AddDbContext<ParkingLotDBContext>();
builder.Services.AddScoped<ParkingLotInitializer>();
builder.Services.AddScoped<IParkingLotService, ParkingLotService>();
builder.Services.AddScoped<IVehicleRegisterService, VehicleRegisterService>();
builder.Services.AddMediatRServices();

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(policy => policy
        .WithOrigins("http://localhost:44471"));
});

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

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    Console.WriteLine("Endpoint: {0}", context.GetEndpoint()?.DisplayName);
    await next.Invoke();
});

app.Run();
