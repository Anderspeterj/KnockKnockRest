using Microsoft.EntityFrameworkCore;
using KnockKnockRest.Repositories;
using KnockKnockRest;
using KnockKnockRest.Context;
using KnockKnockRest.RepositoriesDB;
using KnockKnockRest.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                              policy =>
                              {
                                  policy.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





bool useSql = true;
if (useSql)
{
    var optionsBuilder = new DbContextOptionsBuilder<KnockKnockContext>();
    optionsBuilder.UseSqlServer(Secrets.ConnectionString);
    KnockKnockContext context = new KnockKnockContext(optionsBuilder.Options);
    //builder.Services.AddSingleton(new ArrivalsRepositoryDb(context));
    builder.Services.AddSingleton<IStudentsRepository>(new StudentsRepositoryDb(context));
    builder.Services.AddSingleton<IArrivalsRepository>(new ArrivalsRepositoryDb(context));
}
else
{
    builder.Services.AddSingleton(new ArrivalsRepository());
    builder.Services.AddSingleton(new StudentsRepository());
}


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();