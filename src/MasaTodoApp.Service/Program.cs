var builder = WebApplication.CreateBuilder(args);

var app = builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MasaTodoAppApp", Version = "v1", Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "MasaTodoAppApp", } });
        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml")) options.IncludeXmlComments(item, true);
        options.DocInclusionPredicate((docName, action) => true);
    })
    .AddEventBus()
    .AddMasaDbContext<TodoAppDbContext>(opt =>
    {
        opt.UseSqlite();
    })
    .AddAutoInject()
    .AddServices(builder, option => option.MapHttpMethodsForUnmatched = new string[] { "Post" });

app.UseMasaExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "MasaTodoAppApp"));

    #region MigrationDb
    using var context = app.Services.CreateScope().ServiceProvider.GetService<TodoAppDbContext>();
    {
        //context!.Database.Migrate();
        // if (context!.GetService<IRelationalDatabaseCreator>().HasTables() == false)
        // {
        //     context!.GetService<IRelationalDatabaseCreator>().CreateTables();
        // }
        context!.Database.EnsureCreated();
    }
    #endregion
}

app.Run();
