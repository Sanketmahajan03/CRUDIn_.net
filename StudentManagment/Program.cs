using Microsoft.EntityFrameworkCore;
using StudentManagment.Data;
using StudentManagment.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add CORS first
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("*")
            .SetIsOriginAllowed(origin => true)  // Allow any origin
            .AllowCredentials();
    });
});

// Other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StudentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentConnectionString")));

builder.Services.AddScoped<IStudentRepository, SQLStudentRepository>();

var app = builder.Build();

// Important: Place UseCors() before other middleware
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();