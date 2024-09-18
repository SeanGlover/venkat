using MongoDB.Driver;
using venkat.Models;
using venkat.Services;

// https://www.pragimtech.com/courses/learn-azure-from-scratch/
// What is Swagger  https://www.youtube.com/watch?v=jH8hNdVNCN0 see also --> https://swagger.io/docs/specification/about/
// swagger tools & slides https://www.pragimtech.com/blog/azure/what-is-swagger/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<StudentStoreDatabaseSettings>(
    builder.Configuration.GetSection(nameof(StudentStoreDatabaseSettings)));

builder.Services.AddSingleton<IStudentStoreDatabaseSettings>(sp =>
sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<StudentStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
new MongoClient(builder.Configuration.GetValue<string>("StudentStoreDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "blazorServer v1");
        //c.SwaggerEndpoint("/mycoolapi/swagger/v1/swagger.json", "My Cool API V1");
        //c.RoutePrefix = "mycoolapi/swagger";
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    //endpoints.MapBlazorHub();
    //endpoints.MapFallbackToPage("/_Host");
});

app.Run();
