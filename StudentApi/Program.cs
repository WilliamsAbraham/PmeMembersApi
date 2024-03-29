using StudentsApi.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));
// Add services to the container.
builder.Services.ConfigureCore();
builder.Services.ConfigureIISOptions();
builder.Services.ConfigureSqlConnection(builder.Configuration);
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryWrapper();
builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsplocy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
