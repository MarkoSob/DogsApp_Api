using DogsApp_Api;
using DogsApp_DAL.Helpers;
using DogsApp_BL.Services.DogsService;
using DogsApp_DAL;
using DogsApp_DAL.GenericRepository;
using Microsoft.EntityFrameworkCore;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<EfDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(ISortHelper<>), typeof(SortHelper<>));
builder.Services.AddScoped<IDogsService, DogsService>();

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase();
app.UseIpRateLimiting();
app.Run();
