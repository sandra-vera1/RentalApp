using RentalApp;
using RentalApp.Services.PropertyService;
//using RentalApp.Services.PropertyServices;

var builder = WebApplication.CreateBuilder(args);
// Bind the connection string from configuration
builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection("ConnectionStrings"));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add interface injection to controller class
builder.Services.AddScoped<IPropertyService, PropertyService>();


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
