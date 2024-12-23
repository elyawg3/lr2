using Microsoft.EntityFrameworkCore;
using LunevAPP.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// ����������� DbContext � ���������� ������������
//builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AppDbContext"));
builder.Services.
    AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("AppDbContext")));

// ���������� ������������
builder.Services.AddControllers();

// ����������� Swagger (�� �������, ��� ������������)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ������������� Swagger UI (���� ����������)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
