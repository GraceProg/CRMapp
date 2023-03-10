using CRM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionstring = builder.Configuration.GetConnectionString("crm");
Constants.ConnectionString = connectionstring;
var services = builder.Services;
services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionstring));


//services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();
services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
services.AddControllersWithViews();


services.AddSingleton<LanguageService>();
services.AddLocalization(options => options.ResourcesPath = "Resources");

services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options => {

    options.DataAnnotationLocalizerProvider = (type, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(ShareResource).GetTypeInfo().Assembly.FullName);
        return factory.Create("ShareResource", assemblyName.Name);
    };

});

services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
    };

    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(culture: "en-US", uiCulture: "en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;

    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});



services.AddRazorPages();
//services.AddControllersWithViews().AddRazorRuntimeCompilation();
//services.AddRazorPages();
services.AddControllersWithViews().AddRazorRuntimeCompilation();


builder.Services.AddAuthentication(Constants.CookieName).AddCookie(Constants.CookieName, options =>
{
    options.Cookie.Name = Constants.CookieName;
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ManagerOnly", policy => policy.RequireClaim("Manager"));
    options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("Employee"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseFastReport();

var locOptions = ((IApplicationBuilder)app).ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

var dbContext = new AppDbContext(connectionstring);
await dbContext.Database.MigrateAsync();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Customers}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Customers}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
