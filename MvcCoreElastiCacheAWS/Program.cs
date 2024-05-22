using Microsoft.Extensions.Options;
using MvcCoreElastiCacheAWS.Repository;
using MvcCoreElastiCacheAWS.Services;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<CochesRepository>();
builder.Services.AddTransient<AWSCacheService>();
string connectionStringRedis = builder.Configuration.GetConnectionString("CacheRedis");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = connectionStringRedis;
    options.InstanceName = "cache-coches";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
