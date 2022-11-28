using WatchDog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddWatchDogServices(opt =>
{
    opt.IsAutoClear = false;
    opt.ClearTimeSchedule = WatchDog.src.Enums.WatchDogAutoClearScheduleEnum.Daily;
    opt.SetExternalDbConnString = builder.Configuration.GetConnectionString(name: "DB");
    opt.SqlDriverOption = WatchDog.src.Enums.WatchDogSqlDriverEnum.PostgreSql;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseWatchDogExceptionLogger();
app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "admin";
    opt.WatchPagePassword = "admin";

});
app.Run();
