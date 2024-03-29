using Entidades;
using Stores;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Mapeos;
using Stores.IContext;
using Data.Queries;
using basededatos;
using Microsoft.EntityFrameworkCore;
using Logica.Session;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using cursonetcore.Middleware;
using Logica.User;
using Logica.Policies;
using Microsoft.AspNetCore.Authorization;

namespace cursonetcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<ICosasStore, CosasStore>();
            builder.Services.AddScoped<IUserLogic, UserLogic>();
            builder.Services.AddScoped<ISessionLogic, SessionLogic>();
            builder.Services.AddScoped<IUserStore, UserStore>();
            builder.Services.AddScoped<ISessionStore, SessionStore>();
            builder.Services.AddScoped<IMapeosCosas, MapeosCosas>();
            builder.Services.AddScoped<IMapeosUser, MapeosUser>();
            builder.Services.AddScoped<ITransactionQuery, TransactionQuery>();
            builder.Services.AddScoped(typeof(IEntityQuery<>), typeof(EntityQuery<>));
            builder.Services.AddDbContext<CosasContext>(
                options => options.UseSqlServer("name=ConnectionStrings:ServiceContext"));
            //builder.Services.AddSwaggerGen();

            /* Clase 9: El siguiente fragmento (invocaci�n) es una modificaci�n al m�todo 
             * builder.Services.AddSwaggerGen() para agregar el Sagger el bot�n de 
             * Authorize.
             */
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AFIP Login practice",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            /* Clase 9: El siguiente fragmento (invocaci�n) de datos configura el 
             * comportamiento de la autentificaci�n basada en JWT.
             * Los datos para configurar el compotamiento se toman desde el appsettings,
             * por lo tanto es necesario agregar al appsettings la siguiente secci�n
             * "Jwt": {
             *   "Key": "Contrase�a secreta para generar la clave sim�trica",
             *   "Issuer": "Emisor: URL del que provee el token, por ejemplo: http://localhost:5063",
             *   "Audience": "Usuario: URL del que provee el token, por ejemplo: http://localhost:5063",
             *   "Subject":  "Tema del token"
             * }
             * 
             * Recordatorio: para usar esta caracter�stica hay que usar la dependencia: Microsoft.AspNetCore.Authentication.JwtBearer
             */
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Clase 9: Se indica que hay que verificar la firma a partir de la clave configurada.
                        ValidateIssuerSigningKey = true,
                        // Clase 9: Se crea la clave de cifrado sim�trico a partir de la contrase�a
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "")),

                        // Clase 9: Se indica que hay que verificar el Issuer
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],

                        // Clase 9: Se indica que hay que verificar la Audience
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        // Clase 9: Se indica que hay que verificar el tiempo de validez
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });



            /* Clase 14: Authorizaci�n basada en pol�ticas, primero se inyecta como dependencia el manipulador de la pol�tica, 
             * este manipulador est�a relacionado con la clase RoleRequirement que se usa para definir las pol�ticas.
             */
            builder.Services.AddScoped<IAuthorizationHandler, RoleHandler>();

            // Clase 14: Authorizaci�n basada en pol�ticas, se define la pol�tica que requiere el rol admin. Pueden definirse otras para otros roles.
            builder.Services.AddAuthorization(options =>
                options.AddPolicy("AdminRole", policy => policy.Requirements.Add(new RoleRequirement("admin"))));

            builder.Services.AddAuthorization(options =>
                options.AddPolicy("OperatorRole", policy => policy.Requirements.Add(new RoleRequirement("operator"))));



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            /* Clase 11: Los middleware se cargan usando metodos que geenralmente comienzan con Use...
             * As� mismo pueden cargarse usando �l m�todo UseMiddleware o usando una extensi�n ad hoc
             */
            //app.UseMiddleware<LoggingHandlerMiddleware>();
            app.UseLoggingHandlerMiddleware();
            /* Clase 9: Los siguientes dos comando configuran el sistema para usar la 
             * authentificaci�n y la autorizaci�n.
             * Es necesario que se escriban antes del comando app.MapControllers();
             */
            app.UseAuthentication();
            app.UseAuthorization();
            /* Clase 11: Los middleware se cargan usando metodos que geenralmente comienzan con Use...
             * As� mismo pueden cargarse usando �l m�todo UseMiddleware o usando una extensi�n ad hoc
             */
            //app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseErrorHandlerMiddleware();

            app.MapControllers();

            app.Run();
        }
    }
}
