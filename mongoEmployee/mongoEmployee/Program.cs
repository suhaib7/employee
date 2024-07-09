using DataAccess;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;



var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddTransient<ISectionRepository, SectionRepository>();
builder.Services.AddTransient<ITrainingRepository,TrainingRepository>();
builder.Services.AddTransient<IProductRepository,ProductRepository>();

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddControllersWithViews()
     .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
         options.SerializerSettings.Formatting = Formatting.Indented;
         options.SerializerSettings.ContractResolver = new DefaultContractResolver
         {
             NamingStrategy = new CamelCaseNamingStrategy()
         };
     });

builder.Services.Configure<Settings>(options =>
{
    options.ConnectionString = builder.Configuration.GetSection("MongoDB:ConnectionString").Value;
    options.Database = builder.Configuration.GetSection("MongoDB:Database").Value;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
