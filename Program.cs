var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS
app.UseHttpsRedirection();

// Home endpoint
app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        message = "Azure .NET API is running successfully!",
        environment = "Azure App Service"
    });
});

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
        new Employee(1, "Mahesh", "Developer"),
        new Employee(2, "Ravi", "Tester"),
        new Employee(3, "Suresh", "DevOps Engineer")
    };

    return Results.Ok(employees);
});

// Get employee by ID
app.MapGet("/api/employees/{id:int}", (int id) =>
{
    var employee = new Employee(
        id,
        "Mahesh",
        "Developer"
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

app.Run();

public record Employee(
    int Id,
    string Name,
    string Role
);
