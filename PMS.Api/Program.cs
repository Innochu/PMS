using Microsoft.EntityFrameworkCore;
using PMS.Api.Data;
using PMS.Api.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core InMemory database (swap to SqlServer/Postgres in production)
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Application services
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Seed a demo user
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
    // Ensure database created
   // db.Database.EnsureCreated();
    // Create a demo user if none exists
    var demoUser = await userService.FindByUsernameAsync("demo");
    if (demoUser == null)
    {
        await userService.CreateUserAsync(
            username: "demo",
            password: "Password123!",
            email: "demo@example.com",
            firstName: "Demo",
            lastName: "User",
            role: "Admin",
            department: "IT"
        );
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
