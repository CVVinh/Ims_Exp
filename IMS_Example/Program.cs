using FluentValidation.AspNetCore;
using IMS_Example.DAL;
using IMS_Example.Data.Contexts;
using IMS_Example.Data.Models;
using IMS_Example.Helpers;
using IMS_Example.Mapper;
using IMS_Example.Services.PaginationServices;
using IMS_Example.Services.TokenServices;
using IMS_Example.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// add services to the containner
builder.Services.AddControllers().AddFluentValidation();

#region
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion


builder.Services.AddScoped<IPaginationServices<Users>, PaginationServices<Users>>();
builder.Services.AddScoped<IPaginationServices<Projects>, PaginationServices<Projects>>();



// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(opts => opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration["DbContextSetting:ConnectionString"], b => b.MigrationsAssembly("BE"));
});


// configure cores
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("AppCorsPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAutoMapperCollection();

// register instance
// configure
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));

// add instance
builder.Services.AddScoped<JwtHeader>();
builder.Services.AddScoped<EncryptionHelper>();
builder.Services.AddScoped<TokenServices>();
builder.Services.AddScoped<TokenHelper>();


// add repository
builder.Services.AddScoped(typeof(GenericRepository<>));


// Configure Authentication
var secretKey = builder.Configuration["JwtSetting:Secret"];
var secretKeyByte = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    // Get Jwt Setting
    var jwtSetting = builder.Configuration.GetSection("JwtSetting").Get<JwtSetting>();
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // default True
        ValidateAudience = false, // default True

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Secret)),

        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AppCorsPolicy");

//app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();


app.MapControllers();

app.Run();
