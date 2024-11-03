
using EzyBill.BLL;
using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.Auth;
using EzyBill.BLL.Services;
using EzyBill.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace EzyBill.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var jwtops = new JwtOptions
            {
                Issuer = builder.Configuration["JwtOptions:Issuer"] ?? string.Empty,
                SecretKey = builder.Configuration["JwtOptions:SecretKey"] ?? string.Empty
            };

           
            builder.Services
                .AddDbContext<WebDbContext>(optionsAction => optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.Configure<JwtOptions>(config =>
            {
                config.Issuer = jwtops.Issuer;
                config.SecretKey = jwtops.SecretKey;
            }).AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<ICurrentUserContext<string>, CurrentUserContext>();
            builder.Services.AddEzyBillServices();

           
           
            builder.Services.AddAuthentication()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
                {
                   
                    var jwtService = new JwtService(Options.Create(jwtops));
                    options.TokenValidationParameters = jwtService.TokenValidationParameters;
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = (context) =>
                        {
                            var authHeader  = string.IsNullOrEmpty(context.Request.Headers.Authorization) ? " ": context.Request.Headers.Authorization.ToString();
                            
                            var match = Regex.Match(authHeader, @"(?<=Bearer\s+).+", RegexOptions.IgnoreCase);
                            if (match.Success)
                            {
                                var tkn = match.Value;
                                jwtService.ValidateToken(tkn, out SecurityToken? outputTkn);
                                if (outputTkn == null)
                                {
                                   var refreshToken = context.Request.Cookies[JwtService.RefreshTokenCookieKey];
                                   if(refreshToken is string refreshTokenStr && jwtService.ValidateToken(refreshTokenStr, out _) is not null)
                                    {
                                        var d1 = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration["JwtTokenOptions:TokenTimeSpanMinutes"]));
                                        var d2 = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration["JwtTokenOptions:RefreshTokenTimeSpanMinutes"]));
                                        (string? newTkn, string? refreshTkn) = jwtService.NewTokenPair(refreshTokenStr,d1, d2);
                                        if(newTkn is string  newTknStr && refreshTkn is string refreshTknStr)
                                        {
                                            context.Response.Cookies.Append(JwtService.TokenCookieKey, newTknStr);
                                            context.Response.Cookies.Append(JwtService.TokenCookieKey, refreshTknStr);
                                        }
                                    }
                                }
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
