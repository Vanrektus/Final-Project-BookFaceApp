using BookFaceApp.ExternalLogin;
using BookFaceApp.Infrastructure.Data;
using BookFaceApp.Infrastructure.Data.Entities;
using BookFaceApp.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookFaceApp.Controllers.Constants.ControllersConstants;
using static BookFaceApp.Infrastructure.Data.DataConstants.UserConstants;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookFaceAppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedAccount");
    options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedEmail");
    options.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:RequireConfirmedPhoneNumber");
    options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:RequireNonAlphanumeric");
    options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:RequireUppercase");
    options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:RequireLowercase");
    options.Password.RequiredLength = MinUserPassword;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BookFaceAppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
    //options.AccessDeniedPath = "/Error/Forbidden";
});

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    });

builder.Services.AddApplicationServices();
builder.Services.AddResponseCaching();

builder.Services.AddCors(options =>
{
    options.AddPolicy("all", opt =>
    {
        opt.AllowAnyOrigin();
        opt.AllowAnyMethod();
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole(RolesNamesConstants.Admin);
        //policy.RequireClaim("AdminNumber", "123");
    });
});

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = ExternalLoginFacebook.AppId;
        options.AppSecret = ExternalLoginFacebook.AppSecret;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
	app.UseExceptionHandler("/Error/SomethingWentWrong");
}
else
{
    app.UseExceptionHandler("/Error/SomethingWentWrong");
    app.UseHsts();
}

app.Use((context, next) =>
{
    context.Request.Scheme = "https";

    return next();
});

app.UseCors("all");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
      name: "publicationDetails",
      pattern: "Publication/Details/{id}/{information?}",
      defaults: new { controller = "Publication", action = "Details" }
    );

    endpoints.MapControllerRoute(
     name: "userGroup",
     pattern: "Admin/Request/Accept/{groupId}/{userId}",
     defaults: new { area = "Admin", controller = "Request", action = "Accept" }
   );

    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

app.UseResponseCaching();

app.Run();
