using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Data;
using PeopleManager.Services;
using PeopleManager.Services.Abstractions;

namespace PeopleManager.RestApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public static readonly ILoggerFactory ConsoleLoggerFactory
			= LoggerFactory.Create(builder => { builder.AddConsole(); });

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "PeopleManager.RestApi", Version = "v1" });
			});

			services.AddDbContext<PeopleManagerDbContext>(options =>
			{
				options.UseInMemoryDatabase("PeopleManager");

				//***Enable this code when using SQL SERVER database***
				//options.UseLoggerFactory(ConsoleLoggerFactory);
				//options.EnableSensitiveDataLogging();
				//options.UseSqlServer("Server=.\\SqlExpress;Database=PeopleManager;Trusted_Connection=True;");

			});
			services.AddTransient<IPersonService, PersonService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PeopleManagerDbContext dbContext)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PeopleManager.RestApi v1"));

				if (dbContext.Database.IsInMemory())
				{
					dbContext.Seed();
				}
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
