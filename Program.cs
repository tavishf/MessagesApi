using MessagesApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging(o => { });

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<IMessagesRepository, InMemoryMessagesRepository>();

var app = builder.Build();
app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
