using UssJuniorTest.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.RegisterAppServices();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) 
    app.UseCustomizeSwagger();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();