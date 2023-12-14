
using Admin.Core;
using Admin.Core.AppBuilder;
using Admin.Core.Services;
using Admin.Dsta;
using Admin.Services.Services;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RBSAuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("RBSAuthDbContextConnection"));
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RBSAppDbContextConnection"));
});



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<ITutorService, TutorService>();
builder.Services.AddTransient<ILessonTypeService, LessonTypeService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IBranchService, BranchService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();



builder.Services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<RBSAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddCors(options =>
{
    options.AddPolicy("corspolicy", (policy) =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


// register all services (repositories)
builder.Services.AddScoped<AuthSecurityService>();
//builder.Services.AddScoped<TutorService>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", (policy) =>
    {
        policy.RequireRole("Administrator");
    });

    options.AddPolicy("AdminManagerPolicy", (policy) =>
    {
        policy.RequireRole("Administrator", "Manager");
    });

    options.AddPolicy("AdminManagerClerkPolicy", (policy) =>
    {
        policy.RequireRole("Administrator", "Manager", "Clerk");
    });
});
// Read the Secret Key from the appsettings.json
byte[] secretKey = Convert.FromBase64String(builder.Configuration["JWTCoreSettings:SecretKey"]);
// set the Authentication Scheme
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    // Validate the token bt receivig the token from the Authorization Request Header
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
    x.Events = new JwtBearerEvents()
    {
        // If the Token is expired the respond
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Authentication-Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
}).AddCookie(options =>
{
    options.Events.OnRedirectToAccessDenied =
    options.Events.OnRedirectToLogin = c =>
    {
        c.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.FromResult<object>(null);
    };
});



builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JSON Web Token Authorization header using the Bearer scheme.  \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });


    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            new List<string>()
        }
    });
});





// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("corspolicy");
app.UseAuthentication();
app.UseAuthorization();

// Create DefaultAdminsistrator User
IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
await GlobalOps.CreateApplicationAdministrator(serviceProvider);

app.MapControllers();

app.Run();
