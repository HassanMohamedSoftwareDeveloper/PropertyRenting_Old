using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PropertyRenting.UI;
using PropertyRenting.UI.Middlewares;
using PropertyRenting.UI.Models.Contexts;
using PropertyRenting.UI.Models.Helpers;

var builder = WebApplication.CreateBuilder(args);
//SqlServer
builder.Services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies()
.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture("en-Us");
    options.AddSupportedUICultures("en-US", "ar");
    options.FallBackToParentUICultures = true;

    options
        .RequestCultureProviders
        .Remove(typeof(AcceptLanguageHeaderRequestCultureProvider));
});

builder.Services
    .AddRazorPages()
    .AddViewLocalization();

builder.Services.AddScoped<RequestLocalizationCookiesMiddleware>();
builder.Services.AddHttpContextAccessor();


//builder.Services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
//builder.Services.AddDbContext<AppDbContext>((sl, optionBuilder) =>
//{
//   var interceptor= sl.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
//    optionBuilder.AddInterceptors(interceptor);
//});
//builder.Services.AddQuartz(configure =>
//{
//    var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
//    configure
//    .AddJob<ProcessOutboxMessagesJob>(jobKey)
//    .AddTrigger(
//        trigger =>
//        trigger.ForJob(jobKey)
//        .WithSimpleSchedule(
//            schedule =>
//            schedule.WithIntervalInSeconds(10)
//            .RepeatForever()));

//    configure.UseMicrosoftDependencyInjectionJobFactory();
//});


DatabaseCreater.Create(builder.Services.BuildServiceProvider());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization();

app.UseRequestLocalizationCookies();

if (!app.Environment.IsDevelopment())
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "ClientApp/dist")),
        RequestPath = "/ClientApp/dist"
    });
}
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

if (app.Environment.IsDevelopment())
{
    app.UseSpa(spa =>
    {
        spa.Options.SourcePath = null;
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
    });
}



app.Run();