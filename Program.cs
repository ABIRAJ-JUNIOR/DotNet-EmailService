
using MailSend.DB;
using MailSend.Models;
using MailSend.Repo;
using MailSend.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register EmailConfig
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));

// Register services
builder.Services.AddScoped<sendmailService>();
builder.Services.AddScoped<SendMailRepository>();
builder.Services.AddScoped<EmailServiceProvider>();

// Register DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Dbconn")));

// Ensure EmailConfig is available as a singleton if needed
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailConfig>>().Value);


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
