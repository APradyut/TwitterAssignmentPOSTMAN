using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TwitterAssignment.Entities;

namespace TwitterAssignment
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//Setting up JWT Token authentication
			//Please put this key in some place safe like the registry.
			string SecurityKey = Configuration["JWT:SecurityKey"];
			var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateIssuerSigningKey = true,

						ValidIssuer = Configuration["JWT:ValidIssuer"], 
						ValidAudience = Configuration["JWT:ValidAudience"], 
						IssuerSigningKey = SymmetricSecurityKey
					};
				});

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			//Setting up DI for database connection
			var connection = Configuration.GetConnectionString("mysqlConnection").ToString();
			services.AddDbContext<DBContext>(options => options.UseMySql(connection));	//Using an SQL Server
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseAuthentication();
			//app.UseHttpsRedirection();
			app.UseMvc();
			app.Run(async context =>
			{
				await context.Response.WriteAsync("{\"Message\":\"Youx hit a 404\"}");
			});
		}
	}
}
