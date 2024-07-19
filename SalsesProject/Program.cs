using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Sales.Db;
using Sales.Services.Customer;
using Sales.Services.Item;
using Sales.Services.MasterDetail;
using Sales.Services.PurchaseMasterDetail;
using Sales.Services.User;
using Sales.Services.Vender;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection")));
builder.Services.AddScoped<ICustomerServices, CustomerService>();
builder.Services.AddTransient<IItemServices,ItemServices>();
builder.Services.AddScoped<ISalesDetailsServices, SalesDetailsServices>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IVenderServices, VenderServices>();
builder.Services.AddTransient<IPurchaseMasterSercices,PurchaseMasterServices>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        //config.ExpireTimeSpan = TimeSpan.FromMinutes(25);
        config.LoginPath = "/Login/Index";
        config.AccessDeniedPath = "/Home/AccessDenied";
    });

//builder.Services.AddAuthorization(options =>
//{
//    //options.AddPolicy("admin",
//    //   policy => policy.RequireRole("admin"));

//    //options.FallbackPolicy = new AuthorizationPolicyBuilder()
//    //.RequireAuthenticatedUser()
//    //.Build();

//    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
//    options.AddPolicy("UserOnly", policy => policy.RequireRole("user"));

//    // Add this line to require authentication by default
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});

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
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
