
using EzyBill.BLL;
using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.Auth;
using EzyBill.BLL.Services;
using EzyBill.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace EzyBill.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(config =>
            {
                config.AddDefaultPolicy(pC =>
                {
                    pC.AllowAnyOrigin();
                    pC.AllowAnyMethod(); 
                    pC.AllowAnyHeader();
                });
            });
            var jwtops = new JwtOptions
            {
                Issuer = builder.Configuration["JwtOptions:Issuer"] ?? string.Empty,
                SecretKey = builder.Configuration["JwtOptions:SecretKey"] ?? string.Empty
            };

            var a = builder.Configuration["JwtOptions"];
            builder.Services
                .AddDbContext<WebDbContext>(optionsAction => optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"))
                .AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<ICurrentUserContext<string>, CurrentUserContext>();
            builder.Services.AddEzyBillServices();



            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {


                    var jwtService = new JwtService(Options.Create(jwtops));
                    options.TokenValidationParameters = jwtService.TokenValidationParameters;

                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = (context) =>
                        {
                            var authHeader = string.IsNullOrEmpty(context.Request.Headers.Authorization) ? " " : context.Request.Headers.Authorization.ToString();

                            var match = Regex.Match(authHeader, @"(?<=Bearer\s+).+", RegexOptions.IgnoreCase);
                            if (match.Success)
                            {
                                var tkn = match.Value;
                                jwtService.ValidateToken(tkn, out SecurityToken? outputTkn);
                                if (outputTkn == null)
                                {
                                    var refreshToken = context.Request.Cookies[JwtService.RefreshTokenCookieKey];
                                    if (refreshToken is string refreshTokenStr && jwtService.ValidateToken(refreshTokenStr, out _) is not null)
                                    {
                                        var d1 = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration["JwtTokenOptions:TokenTimeSpanMinutes"]));
                                        var d2 = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration["JwtTokenOptions:RefreshTokenTimeSpanMinutes"]));
                                        (string? newTkn, string? refreshTkn) = jwtService.NewTokenPair(refreshTokenStr, d1, d2);
                                        if (newTkn is string newTknStr && refreshTkn is string refreshTknStr)
                                        {
                                            context.Response.Cookies.Append(JwtService.TokenCookieKey, newTknStr);
                                            context.Response.Cookies.Append(JwtService.TokenCookieKey, refreshTknStr);
                                            context.Request.Headers.Authorization = $"Bearer {newTknStr}";
                                        }
                                    }
                                }
                            }


                            return Task.CompletedTask;
                        }
                    };
                })
                .AddOpenIdConnect("OIDC", config =>
                {
                    config.Authority = "https://accounts.google.com";
                    config.ClientId = "525351881996-hjlqjuh97p9r76gb9pinm0p0ln0srvm3.apps.googleusercontent.com";
                    config.ClientSecret = "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";
                    config.ResponseType = OpenIdConnectResponseType.IdTokenToken;
                    config.ResponseMode = OpenIdConnectResponseMode.Query;
                    config.CallbackPath = "https://tsholofelo.growthlytix.co.za/api/oauth/google-cb";


                });
         
            builder.Services.AddControllersWithViews();
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
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
