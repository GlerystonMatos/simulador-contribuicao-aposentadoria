using AutoMapper;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimuladorContribuicaoAposentadoria.Api.Configuracoes;
using SimuladorContribuicaoAposentadoria.Api.MiddlewareCuston;
using SimuladorContribuicaoAposentadoria.CrossCutting;
using SimuladorContribuicaoAposentadoria.Data.Context;
using SimuladorContribuicaoAposentadoria.Service.AutoMapper;

namespace SimuladorContribuicaoAposentadoria.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Version = Configuration.GetValue<string>("Application:Version");
            Name = Configuration.GetValue<string>("Application:Name");

            using (var context = new SimuladorContribuicaoAposentadoriaContext(Configuration))
            {
                context.Database.Migrate();
            }
        }

        public IConfiguration Configuration { get; }

        public string Version { get; }

        public string Name { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddOData();
            services.AddJwtSetup();
            services.AddControllers();
            services.AddSwaggerSetup(Name, Version);
            services.AddAutoMapper(typeof(AutoMapping));
            services.RegisterDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseExceptionHandlerCuston();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.EnableFilter();
                s.DocumentTitle = Name + " | Documenta&ccedil;&atilde;o";
                s.InjectStylesheet("/swagger-ui/custom.css");
                s.SwaggerEndpoint("/swagger/v" + Version + "/swagger.json", Name + " V" + Version);
            });

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                endpoints.MapODataRoute("OData", "OData", EdmModelConfig.GetEdmModel());
                endpoints.EnableDependencyInjection();
            });
        }
    }
}