var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Serve wwwroot/index.html
app.UseDefaultFiles();
app.UseStaticFiles();

// Employee API
app.MapGet("/api/employees", () =>
{
    return new[]
    {
        new { Id = 1, Name = "John", Department = "IT" },
        new { Id = 2, Name = "Mary", Department = "HR" },
        new { Id = 3, Name = "David", Department = "Finance" }
    };
});

// Get employee by ID
app.MapGet("/api/employees/{id}", (int id) =>
{
    var employees = new[]
    {
        new { Id = 1, Name = "John", Department = "IT" },
        new { Id = 2, Name = "Mary", Department = "HR" },
        new { Id = 3, Name = "David", Department = "Finance" }
    };

    var employee = employees.FirstOrDefault(e => e.Id == id);

    return employee is not null
        ? Results.Ok(employee)
        : Results.NotFound();
});

// Health check
app.MapGet("/health", () => "Application is healthy");

app.Run();