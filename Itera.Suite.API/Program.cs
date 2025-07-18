﻿using Itera.Suite.Application;
using Itera.Suite.Infrastructure.Data;
using Itera.Suite.Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Itera.Suite.Infrastructure.Services;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Infrastructure.Repositories;
using Itera.Suite.Application.Handlers.Clientes;
using Itera.Suite.Application.Handlers.Fornecedores;
using Itera.Suite.Application.Handlers.ItensDeCusto;
using Itera.Suite.Application.Handlers.ProjetosDeViagem;
using Itera.Suite.Application.Handlers.BasesDeCalculo;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURAÇÕES DO BANCO
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// IDENTITY
builder.Services.AddIdentity<UsuarioIdentity, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// JWT AUTHENTICATION
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var chave = builder.Configuration["Jwt:Secret"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave!))
        };
    });

builder.Services.AddAuthorization();

// CORS PARA PERMITIR O FRONT NO localhost:7142
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
        policy.WithOrigins("https://localhost:7133")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});

builder.Services.AddHttpContextAccessor();

// REGISTRE SEU TOKEN SERVICE AQUI ⬇️
builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<IPagamentoDaOrdemDePagamentoRepository, PagamentoDaOrdemDePagamentoRepository>();


builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteQuery, ClienteRepository>();
builder.Services.AddScoped<CriarClienteCommandHandler>();
builder.Services.AddScoped<AtualizarClienteCommandHandler>();


builder.Services.AddScoped<IFornecedorRepository, FornecedorRepository>();
builder.Services.AddScoped<IFornecedorQuery, FornecedorRepository>();
builder.Services.AddScoped<CriarFornecedorCommandHandler>();
builder.Services.AddScoped<AtualizarFornecedorCommandHandler>();


builder.Services.AddScoped<IItemDeCustoRepository, ItemDeCustoRepository>();
builder.Services.AddScoped<IItemDeCustoQuery, ItemDeCustoRepository>();
builder.Services.AddScoped<CriarItemDeCustoCommandHandler>();
builder.Services.AddScoped<AtualizarItemDeCustoCommandHandler>();
builder.Services.AddScoped<AlterarStatusItemDeCustoHandler>();


builder.Services.AddScoped<IProjetoDeViagemRepository, ProjetoDeViagemRepository>();
builder.Services.AddScoped<IProjetoDeViagemQuery, ProjetoDeViagemRepository>();
builder.Services.AddScoped<CriarProjetoDeViagemCommandHandler>();
builder.Services.AddScoped<AtualizarProjetoDeViagemCommandHandler>();

builder.Services.AddScoped<DefinirQuantidadesDaBaseCommandHandler>();
builder.Services.AddScoped<IBaseDeCalculoRepository, BaseDeCalculoRepository>();
builder.Services.AddScoped<ConfirmarBaseHandler>();

// PARA USO DO STORAGE NO CLOUDFLARE
builder.Services.AddScoped<IArquivoStorageService, CloudflareR2ArquivoStorageService>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));


// CONTROLLERS
builder.Services.AddControllers();

// SWAGGER (com suporte a autenticação via Bearer)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Itera Suite API", Version = "v1" });

    // Configurar o Swagger para aceitar JWT
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Informe o token JWT no formato: Bearer {seu_token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// MIDDLEWARES
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowBlazorClient");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
