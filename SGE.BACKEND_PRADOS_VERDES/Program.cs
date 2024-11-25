using AutoMapper;
using Microsoft.OpenApi.Models;
using SGE.BACKEND_PRADOS_VERDES.Context;
using SGE.BACKEND_PRADOS_VERDES.Extensions;
using SGE.BACKEND_PRADOS_VERDES.Interfaces;
using SGE.BACKEND_PRADOS_VERDES.Mappers;
using SGE.BACKEND_PRADOS_VERDES.Services;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

builder.Services.AddAuthentication(Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();


builder.Services.AddSingleton(new Conexion(builder.Configuration.GetConnectionString("connection")));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IContratoService, ContratoService>();
builder.Services.AddScoped<IGeneralService, GeneralService>();
builder.Services.AddScoped<ICuotaService, CuotaService>();
builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                    builder => builder.AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()));

var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();



//Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowWebapp");
app.UseAuthorization();

app.MapControllers();

app.Run();