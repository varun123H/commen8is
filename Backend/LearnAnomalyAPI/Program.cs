using MongoDB.Driver;
using LearnAnomalyAPI.Services;
using LearnAnomalyAPI.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// MongoDB Connection
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://localhost:27017";
var mongoClient = new MongoClient(mongoConnectionString);
var mongoDatabase = mongoClient.GetDatabase("LearnAnomaly");

builder.Services.AddSingleton(mongoDatabase);
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<LearningSessionService>();
builder.Services.AddScoped<AnomalyDetectionService>();

// Controllers & SignalR
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularJS",
        corsBuilder => corsBuilder
            .WithOrigins("http://localhost:3000", "http://localhost:8080")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure static files serving for frontend
var frontendPath = Path.Combine(Directory.GetCurrentDirectory(), "../../Frontend/public");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.GetFullPath(frontendPath)),
    RequestPath = ""
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularJS");
app.UseAuthorization();

app.MapControllers();
app.MapHub<AnomalyHub>("/anomalyHub");

// SPA Fallback - serve index.html for all non-API routes
app.MapFallbackToFile("index.html", new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.GetFullPath(frontendPath)),
    RequestPath = ""
});

// Seed sample data on startup
using (var scope = app.Services.CreateScope())
{
    var studentService = scope.ServiceProvider.GetRequiredService<StudentService>();
    await studentService.SeedSampleData();
}

app.Run();
