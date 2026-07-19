using ResultPatternDemo.Employee;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

// Beispiel-Requests, zum Ausprobieren:
//   GET  /employees/123   -> 200 OK  { "id": 123, "name": "Max Mustermann" }
//   GET  /employees/404   -> 404 ProblemDetails { "title": "NotFound", "detail": "..." }
//   DELETE /employees/123 -> 204 NoContent
//   DELETE /employees/404 -> 404 ProblemDetails
