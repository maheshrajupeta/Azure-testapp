var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable default files and static files
app.UseDefaultFiles();
app.UseStaticFiles();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Health check
app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "Healthy",
        timestamp = DateTime.UtcNow
    });
});

// Get all employees
app.MapGet("/api/employees", () =>
{
    var employees = new[]
    {
        new Employee(1, "Mahesh", "Developer", "mahesh@example.com"),
        new Employee(2, "Ravi", "Tester", "ravi@example.com"),
        new Employee(3, "Suresh", "DevOps Engineer", "suresh@example.com")
    };

    return Results.Ok(employees);
});

// Get employee by ID
app.MapGet("/api/employees/{id:int}", (int id) =>
{
    var employee = new Employee(
        id,
        "Mahesh",
        "Developer",
        "mahesh@example.com"
    );

    return Results.Ok(employee);
});

// Create employee
app.MapPost("/api/employees", (Employee employee) =>
{
    return Results.Created(
        $"/api/employees/{employee.Id}",
        employee
    );
});

// Delete employee
app.MapDelete("/api/employees/{id:int}", (int id) =>
{
    return Results.Ok(new
    {
        message = $"Employee {id} deleted successfully"
    });
});

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Serve wwwroot/index.html
app.UseDefaultFiles();
app.UseStaticFiles();

// API
app.MapGet("/api/employees", () =>
{
    return new[]
    {
        new { Id = 1, Name = "John", Department = "IT" },
        new { Id = 2, Name = "Mary", Department = "HR" },
        new { Id = 3, Name = "David", Department = "Finance" }
    };
});

app.MapGet("/health", () => "Application is healthy");


app.Run();

public record Employee(
    int Id,
    string Name,
    string Role,
    string Email
);
