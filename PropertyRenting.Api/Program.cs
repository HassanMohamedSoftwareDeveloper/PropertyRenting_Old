using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PropertyRenting.Api.Helpers;
using PropertyRenting.Api.Interceptors;
using PropertyRenting.Api.Models.Helpers;
using PropertyRenting.Api.Repositories;
using PropertyRenting.Api.Services.Token;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
    builder.Configuration.AddUserSecrets("PropertyRenting_Old_Dev");
else if (builder.Environment.IsProduction())
    builder.Configuration.AddUserSecrets("PropertyRenting_Old_Prod");
else if (builder.Environment.IsStaging())
    builder.Configuration.AddUserSecrets("PropertyRenting_Old_Stg");
builder.Services.AddSingleton<AutidableInterceptor>();
builder.Services.AddSingleton<ActionsInterceptor>();
builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    var autidableInterceptor = sp.GetService<AutidableInterceptor>();
    var actionLogInterceptor = sp.GetService<ActionsInterceptor>();

    options.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
    .EnableSensitiveDataLogging()
    .AddInterceptors(autidableInterceptor, actionLogInterceptor);
});
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<QueryHepler>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(opt =>
    {
        opt.Password.RequireDigit = false;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequiredLength = 4;

        opt.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Property_Renting_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                          });
});
builder.Services.AddControllers();

builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidateIssuer = true,
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization();
//options =>
//{
//    options.AddPolicy("AdminDev", md =>
//    {
//        md.RequireClaim("JobTitle", "Dev");
//        md.RequireRole("Admin");
//    });
//    options.AddPolicy("ManagerDev", md =>
//    {
//        md.RequireClaim("JobTitle", "Dev");
//        md.RequireRole("Manager");
//    });
//}

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
await DatabaseCreater.Create(app.Services);
app.Run();

