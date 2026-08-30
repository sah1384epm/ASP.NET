using Microsoft.EntityFrameworkCore;
using API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // ثبت سرویس سواگر

builder.Services.AddDbContext<ApplicationDBContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // تولید فایل JSON
    app.UseSwaggerUI(); // ساخت UI گرافیکی
}

// app.UseHttpsRedirection(); // کامنت بماند

app.MapControllers();
app.Run();