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

app.Run();

public record Employee(
    int Id,
    string Name,
    string Role,
    string Email
);
