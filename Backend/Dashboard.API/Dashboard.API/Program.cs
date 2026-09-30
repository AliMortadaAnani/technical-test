using Dashboard.API.StartupExtension;

var builder = WebApplication.CreateBuilder(args);

// Calls all extension methods sequentially under the hood
builder.ConfigureAllServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(); // Custom Exception Handler Middleware : Global Exception Handling
//in Application layer , registered in StartupExtensions/ServiceExtensions.cs

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseCors("FrontendPolicy");

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();