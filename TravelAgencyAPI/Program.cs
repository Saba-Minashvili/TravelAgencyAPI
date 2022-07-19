using Domain.Entities;
using Domain.Entities.Authentication;
using Domain.Repositories;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Middleware.Filters;
using Middleware.Validators;
using Persistence;
using Persistence.Authentication;
using Persistence.Mapper;
using Persistence.Repositories;
using Services;
using Services.Abstractions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("TravelAgencyDbConnectionString")));

builder.Services.AddIdentity<User, IdentityRole>()
	.AddDefaultTokenProviders()
	.AddUserManager<UserManager<User>>()
	.AddSignInManager<SignInManager<User>>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
	options.User.RequireUniqueEmail = true;
	options.User.AllowedUserNameCharacters =
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

	// Identity : Default password settings
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Password.RequiredLength = 8;
});

builder.Services.Configure<Token>(builder.Configuration.GetSection("JWT"));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
	options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddCors(options =>
	options.AddPolicy("TravelAgencyPolicy", builder =>
	{
		builder.WithOrigins("http://localhost:3000")
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials();
	})
);

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]);
	o.SaveToken = true;
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidAudience = builder.Configuration["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Key),
		ClockSkew = TimeSpan.Zero
	};
});


// Add services to the container.

builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();

builder.Services.AddAutoMapper(typeof(ObjectMapper).Assembly);

builder.Services.AddControllers(options =>
{
	options.Filters.Add<ValidationFilter>();
})
	.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssembly(typeof(RegisterUserDtoValidator).Assembly));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},

			new string[] {}
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "TravelAgency v1"));
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("TravelAgencyPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
