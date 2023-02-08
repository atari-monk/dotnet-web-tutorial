using Commander;
using Commander.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var connection = new DbConnConfig(builder).DbConnectionString;
    //builder.Configuration.GetConnectionString("ContainerConnection");
//Console.WriteLine(connection);
// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<CommanderContext>(options => 
    options.UseSqlServer(connection));
builder.Services.AddScoped<IMockCommanderRepo, MockCommanderRepo>();
builder.Services.AddScoped<ICommanderRepo, SqlCommanderRepo>();
builder.Services.AddControllers().AddNewtonsoftJson(
    s=>s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.WebHost.UseUrls(
//     "http://localhost:5010"
//     );
builder.WebHost.UseContentRoot(Directory.GetCurrentDirectory());

var app = builder.Build();
await PrepDatabase.PrepDbAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
