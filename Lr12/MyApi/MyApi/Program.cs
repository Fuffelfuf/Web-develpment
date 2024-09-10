using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyApi.Services;
using System.Text;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MyApi.HealthChecks;
using HealthChecks.UI.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() 
    .WriteTo.Console()    
    .WriteTo.File("logs/log.txt")
    //.WriteTo.Seq("http://localhost:5341")
    //.WriteTo.Email(from: "app@example.com", to: "support@example.com", host: "smtp.example.com")
    .CreateLogger();

Log.Debug("This is a debug message");
Log.Information("This is an info message");
Log.Warning("This is a warning message");
Log.Error("This is an error message");
Log.Fatal("This is a fatal message");

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IVariativeService, VariativeService>();


builder.Services.AddHealthChecks()
    .AddCheck("Memory Health Check", new MemoryHealthCheck(500_000), tags: new[] { "memory" })
    .AddCheck("Database Health Check", new SqliteHealthCheck(connectionString: "Data Source=./BooksSQLite.db"), tags: new[] { "database" })
    .AddCheck("Ping Health Check", new PingHealthCheck("8.8.8.8"), tags: new[] { "network" });


builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(10);
    options.MaximumHistoryEntriesPerEndpoint(50);
    options.AddHealthCheckEndpoint("Basic HealthCheck", "/health");
    options.AddHealthCheckEndpoint("Ping Health Check", "/health/network");
    options.AddHealthCheckEndpoint("Memory HealthCheck", "/health/memory");
    options.AddHealthCheckEndpoint("Database HealthCheck", "/health/database");
})
.AddSqliteStorage("Data Source=healthchecks.db");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MyApi", Version = "v1" });
    c.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MyApi", Version = "v2" });
    c.SwaggerDoc("v3", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MyApi", Version = "v3" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "¬ведите ваш JWT токен в формате **Bearer {token}**"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddSignalR();
builder.Services.AddSingleton<StockPriceGenerator>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<StockHub>("/stockHub");
    endpoints.MapControllers();
});

app.UseHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = CustomHealthCheckResponseWriter.WriteResponse
});

app.UseHealthChecks("/health/network", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("network"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecks("/health/memory", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("memory"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseHealthChecks("/health/database", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("database"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.UseHealthChecksUI(config => config.UIPath = "/health-ui");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyApi v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "MyApi v2");
        c.SwaggerEndpoint("/swagger/v3/swagger.json", "MyApi v3");
    });
}

app.UseHttpsRedirection();


var stockPriceGenerator = app.Services.GetRequiredService<StockPriceGenerator>();
stockPriceGenerator.Start();


app.Run();