

using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Univali.Api.Features.Addresses.Commands.CreateAddress;
using Univali.Api.Features.Addresses.Commands.UpdateAddress;
using Univali.Api.Features.Publishers.Commands.CreatePublisher;
using Univali.Api.Features.Publishers.Commands.UpdatePublisher;
using Univali.Api;
using Univali.Api.Configuration;
using Univali.Api.DbContexts;
using Univali.Api.Extensions;
using Univali.Api.Features.Customers.Commands.CreateCustomer;
using Univali.Api.Repositories;
using Microsoft.OpenApi.Models;
using Univali.Api.Features.Courses.Commands.UpdateCourse;
using Univali.Api.Features.Courses.Commands.CreateCourse;
using Univali.Api.Features.Authors.Commands.UpdateAuthor;
using Univali.Api.Features.Authors.Commands.CreateAuthor;
using Univali.Api.Features.Customers.Commands.CreateCustomerWithAddresses;
using Univali.Api.Features.Customers.Commands.UpdateCustomer;
using Univali.Api.Features.Customers.Commands.UpdateCustomerWithAddresses;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => {
   options.ListenLocalhost(5000);
});

builder.Services.AddLogging();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<Data>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();



builder.Services.AddControllers()
.AddJsonOptions(opt =>
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddAuthentication("Bearer").AddJwtBearer( options =>
{
   options.TokenValidationParameters = new()
   {
       // Declaramos o que deverá ser validado
       // O tempo de expiração do token é validado automaticamente.
       // Obriga a validação do emissor
       ValidateIssuer = true,
       // Obriga a validação da audiência
       ValidateAudience = true,
       // Obriga a validação da chave de assinatura`
       ValidateIssuerSigningKey = true,

       // Agora declaramos os valores das propriedades que serão validadas
       // Apenas tokens  gerados por esta api serão considerados válidos.
       ValidIssuer = builder.Configuration["Authentication:Issuer"],
       // Apenas tokens desta audiência serão considerados válidos.
       ValidAudience = builder.Configuration["Authentication:Audience"],
       // Apenas tokens com essa assinatura serão considerados válidos.
       IssuerSigningKey = new SymmetricSecurityKey(
           Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!))
   };
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddScoped<IValidator<CreateCustomerCommand>, CreateCustomerCommandValidator>();
builder.Services.AddScoped<IValidator<CreateAddressCommand>, CreateAddressCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateAddressCommand>, UpdateAddressCommandValidator>();
builder.Services.AddScoped<IValidator<CreatePublisherCommand>, CreatePublisherCommandValidator>();
builder.Services.AddScoped<IValidator<UpdatePublisherCommand>, UpdatePublisherCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateCourseCommand>, UpdateCourseCommandValidator>();
builder.Services.AddScoped<IValidator<CreateCourseCommand>, CreateCourseCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateAuthorCommand>, UpdateAuthorCommandValidator>();
builder.Services.AddScoped<IValidator<CreateAuthorCommand>, CreateAuthorCommandValidator>();
builder.Services.AddScoped<IValidator<CreateCustomerWithAddressesCommand>, CreateCustomerWithAddressesCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateCustomerCommand>, UpdateCustomerCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateCustomerWithAddressesCommand>, UpdateCustomerWithAddressesCommandValidator>();

builder.Services.AddDbContext<CustomerContext>(options => 
{
    options
    .UseNpgsql("Host=localhost;Database=Univali;Username=postgres;Password=123");
});

builder.Services.AddDbContext<PublisherContext>(options => 
{
    options
    .UseNpgsql("Host=localhost;Database=Univali;Username=postgres;Password=123");
});

builder.Services.AddControllers(options =>{
    options.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
})


.ConfigureApiBehaviorOptions(setupAction =>
       {
            setupAction.SuppressModelStateInvalidFilter = true;
            setupAction.InvalidModelStateResponseFactory = context =>
            {
                // Cria a fábrica de um objeto de detalhes de problema de validação
                var problemDetailsFactory = context.HttpContext.RequestServices
                    .GetRequiredService<ProblemDetailsFactory>();

                // Cria um objeto de detalhes de problema de validação
                var validationProblemDetails = problemDetailsFactory
                    .CreateValidationProblemDetails(
                        context.HttpContext,
                        context.ModelState);

                // Adiciona informações adicionais não adicionadas por padrão
                validationProblemDetails.Detail =
                    "See the errors field for details.";
                validationProblemDetails.Instance =
                    context.HttpContext.Request.Path;

                // Relata respostas do estado de modelo inválido como problemas de validação
                validationProblemDetails.Type =
                    "https://courseunivali.com/modelvalidationproblem";
                validationProblemDetails.Status =
                    StatusCodes.Status422UnprocessableEntity;
                validationProblemDetails.Title =
                    "One or more validation errors occurred.";

                return new UnprocessableEntityObjectResult(
                    validationProblemDetails)
                {
                    ContentTypes = { "application/problem+json" }
                };
            };
        });

// É embutido no ASP.NET Core, expõe informações da API, tipo os endpoints e como interagir com eles.
// É usado internamente pelo Swashbuckle para gerar a especificação OpenAPI
builder.Services.AddEndpointsApiExplorer();
// Registra os serviços que são usados para efetivamente gerar a especificação
builder.Services.AddSwaggerGen(setupAction =>
{
   /*
   Retorna o nome do assembly atual como uma string por reflection

   "Assembly.GetExecutingAssembly()" retorna uma referência para o assembly
   que contém o código que está sendo executado atualmente.

   "GetName()" é chamado na referência do assembly para obter um objeto do tipo AssemblyName,
   que contém informações sobre o assembly, como seu nome, versão, cultura e chave pública.

   "Name" é lido a partir do objeto AssemblyName para obter o nome do assembly como uma string.
   */
   var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
   
   // "Path.Combine" cria um formato de caminho válido com os parâmetros
   // "AppContext.BaseDirectory" é uma propriedade que retorna o caminho base do diretório em que a aplicação está sendo executada
   var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);


   // Inclui os comentários XML na documentação do Swagger.
   setupAction.IncludeXmlComments(xmlCommentsFullPath);
});




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "UnivaliAPI - HTTP API",
        Version = "v1",
        Description = "My project HTTP API."
    });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyProj HTTP API v1");
});
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.ResetDatabaseAsync();

app.Run();
