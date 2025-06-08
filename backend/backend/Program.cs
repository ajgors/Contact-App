using backend;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure CORS to allow requests from the frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5173", "http://localhost:4040") // local frontend dev //docker compose
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // Allow any origin
                .AllowCredentials();
        });
});

// Configure JSON serialization to use string enum converter
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Configure Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configure application cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;

    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        // Seed the database with initial data
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        string userEmail = "user@example.com";
        string userPassword = "User1234!";

        var user = new User { UserName = userEmail, Email = userEmail, EmailConfirmed=true};
        var result = await userManager.CreateAsync(user, userPassword);

        db.Contacts.Add(new Contact
        {
            Name = "John",
            Surname = "Doe",
            Email = userEmail,
            ContactDetails = new ContactDetails
            {
                Category = ContactCategory.Business,
                Subcategory = ContactSubcategory.Client.ToString(),
                Phone = "123-456-7890",
                BirthDay = "1990-01-01"
            }
        });

        db.Contacts.Add(new Contact
        {
            Name = "Alice",
            Surname = "Nowak",
            Email = "ala@gmail.com",
            ContactDetails = new ContactDetails
            {
                Category = ContactCategory.Private,
                Phone = "444-456-7890",
                BirthDay = "1996-02-03"
            }
        });


        db.SaveChanges();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
