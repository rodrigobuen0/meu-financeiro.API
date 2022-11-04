using meu_financeiro.API.Authorization;
using meu_financeiro.API.Entities;
using meu_financeiro.API.Helpers;
using meu_financeiro.API.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddDbContext<DataContext>();
    services.AddCors();
    services.AddControllers()
        .AddJsonOptions(x => x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IReceitasService, ReceitasService>();
    services.AddScoped<IDespesasService, DespesasService>();
    services.AddScoped<ICategoriasService, CategoriasService>();
    services.AddScoped<IContasService, ContasService>();
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// add hardcoded test user to db on startup
//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
//    var testUser = new User
//    {
//        PrimeiroNome = "Test",
//        UltimoNome = "User",
//        Email = "test",
//        HashSenha = BCrypt.Net.BCrypt.HashPassword("test")
//    };
//    context.Users.Add(testUser);
//    context.SaveChanges();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// global cors policy
app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
