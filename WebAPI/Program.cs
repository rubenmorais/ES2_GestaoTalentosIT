using Microsoft.EntityFrameworkCore;
using DbLayer.Context;
using WebAPI.Interfaces;
using WebAPI.Repositories;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));  

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITalentoRepository, TalentoRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ITalentoHabilidadeRepository, TalentoHabilidadeRepository>();
builder.Services.AddScoped<IExperienciaRepository, ExperienciaRepository>();
builder.Services.AddScoped<IUtilizadorRepository, UtilizadorRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IHabilidadeRepository, HabilidadeRepository>();
builder.Services.AddScoped<IPropostaTrabalhoRepository, PropostaTrabalhoRepository>();
builder.Services.AddScoped<IPropostaHabilidadeRepository, PropostaHabilidadeRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();
builder.Services.AddScoped<IPropostaTalentoRepository, PropostaTalentoRepository>();
builder.Services.AddScoped<UtilizadorService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TalentoService>();
builder.Services.AddScoped<ExperienciaService>();
builder.Services.AddScoped<HabilidadeService>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<TalentoHabilidadeService>();
builder.Services.AddScoped<PropostaTrabalhoService>();
builder.Services.AddScoped<PropostaHabilidadeService>();
builder.Services.AddScoped<EstadoService>();
builder.Services.AddScoped<PropostaTalentoService>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();
builder.Services.AddScoped<RelatorioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();