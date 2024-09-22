using FluentValidation;
using ShoppingApp.API.Extensions;
using ShoppingApp.API.Mapping;
using ShoppingApp.API.Repositories;
using ShoppingApp.API.Validators;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterSerilog();

builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();

builder.Services.AddTransient<IProductRepository>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<ProductRepository>>();
    return new ProductRepository(logger);
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173") // hardcoded for simplicity, can be obtained from config
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();

#region Endpoints

/* Configuring API endpoints based on naming convention of the configuration classes.
 * endpointsGroup can be used for applying filters.
 */

var endpointsGroup = app.MapGroup(string.Empty);

var endpointsConfigDelegates = System.Reflection.Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(x => x.Namespace != null && x.Namespace.Contains("ShoppingApp.API.Endpoints") && x.Name.EndsWith("Api"))
    .Select(t => (Action)Delegate.CreateDelegate(
        type: typeof(Action),
        firstArgument: endpointsGroup,
        method: t.GetMethod($"Configure{t.Name}", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)))
    .ToList();

foreach (var endpointsConfig in endpointsConfigDelegates)
{
    endpointsConfig.Invoke();
}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.Run();

