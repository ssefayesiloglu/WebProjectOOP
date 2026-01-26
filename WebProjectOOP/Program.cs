using Microsoft.EntityFrameworkCore;
using System.Net.ServerSentEvents;
using WebProjectOOP.Business;
using WebProjectOOP.Business.Abstract;
using WebProjectOOP.Business.Concrete;
using WebProjectOOP.DataAccess;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.
builder.Services.AddControllers();
// 1. CORS Servisini Ekliyoruz (React'in konuþabilmesi için)
/*builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactIzni", policy =>
    {
        policy.AllowAnyOrigin()  // Þimdilik herkese izin ver (Localhost vs.)
              .AllowAnyMethod()  // GET, POST, PUT, DELETE hepsine izin ver
              .AllowAnyHeader(); // Tüm baþlýklara izin ver
    });
});*/

builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<IAiService, OllamaAiService>();
builder.Services.AddScoped<ITaskService, TaskService>();
/*Eðer bu satýrý yazmasaydýk, Controller içinde her seferinde var service = new TaskService(_context); yazmak zorunda kalýrdýk.
//Bu durumda Controller, TaskService'e ve _context'e göbekten baðýmlý olurdu.
Þimdi ise Controller sadece "Bana ITaskService lazým" diyor; sistem arka planda her þeyi hazýrlayýp ona sunuyor.*/
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
if (!app.Environment.IsDevelopment())
{


    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
// 2. CORS'u Devreye Alýyoruz (Sýralama çok önemli!)
//app.UseCors("ReactIzni");

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllers();
  


app.Run();
