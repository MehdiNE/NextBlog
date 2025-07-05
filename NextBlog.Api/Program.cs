using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NextBlog.Api.Database;
using NextBlog.Api.Hubs;
using NextBlog.Api.Models;
using NextBlog.Api.Repositories;
using NextBlog.Api.Repositories.Like;
using NextBlog.Api.Services;
using NextBlog.Api.Services.Like;
using NextBlog.Api.Settings;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAppCorsPolicy",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:5173") // Your frontend URL
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials(); // Important for SignalR authentication
                      });
});

builder.Services.AddControllers();
builder.Services.AddSignalR();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
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
            Array.Empty<string>()
        }
    });
});

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IFollowRepository, FollowRepository>();
builder.Services.AddScoped<IFollowService, FollowService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient<TokenProvider>();

builder.Services.Configure<JwtAuthOptions>(builder.Configuration.GetSection("Jwt"));
JwtAuthOptions jwtAuthOptions = builder.Configuration.GetSection("Jwt").Get<JwtAuthOptions>()!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = jwtAuthOptions.Issuer,
            ValidAudience = jwtAuthOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthOptions.Key)),
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
    });
}



app.UseHttpsRedirection();
app.UseCors("myAppCorsPolicy");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
