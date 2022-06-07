using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TodoList.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoList.DB;
using TodoList.Repositories;
using TodoList.Interfaces;
using TodoList.Services;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using System;
using TodoList.Jobs;
using TodoList.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace TodoList
{
    public class Startup
    {
        //private static ITodoItemService todoItemService;
        //private static IMailDailyService sendMailDailyService;
        //private readonly JobDaily jobscheduler = new JobDaily(sendMailDailyService, todoItemService);
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<DapperContext>();
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITodoItemDapperRepository, TodoItemDapperRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ITodoItemService, TodoItemService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<IMailDailyService, MailDailyService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddDbContext<TodoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TodoContext")));
           
            //Configure Hangfire  
            services.AddHangfire(c => c.UseSqlServerStorage(Configuration.GetConnectionString("TodoContext")));
            GlobalConfiguration.Configuration.UseSqlServerStorage(Configuration.GetConnectionString("TodoContext")).WithJobExpirationTimeout(TimeSpan.FromDays(7));

            // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 8;
                opt.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<TodoContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });


            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
            });

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["Redis:PublicPoint"];
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
            }
            
            //Configure Hangfire  
            app.UseHangfireServer();

            //Basic Authentication added to access the Hangfire Dashboard  
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                AppPath = null,
                DashboardTitle = "Hangfire Dashboard",
                Authorization = new[]{
                    new HangfireCustomBasicAuthenticationFilter{
                        User = Configuration.GetSection("HangfireCredentials:UserName").Value,
                        Pass = Configuration.GetSection("HangfireCredentials:Password").Value
                    }
                },
            });

            //Seeder 
            SeedData.Inittialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Recurring Job
            //recurringJobManager.AddOrUpdate("Email every 9AM",
            //    () => jobscheduler.SendMailDaily(Configuration.GetSection("RecurringJob:Email").Value),
            //    Cron.Daily(int.Parse(Configuration.GetSection("RecurringJob:Hour").Value), int.Parse(Configuration.GetSection("RecurringJob:Minute").Value)),
            //    TimeZoneInfo.Local
            //);

            //Fire and forget job   
            //var jobId = backgroundJobClient.Enqueue(() => jobscheduler.SendMailDaily(Configuration.GetSection("RecurringJob:Email").Value));

            //Continous Job  
            //backgroundJobClient.ContinueJobWith(jobId, () => jobscheduler.CreateTodoItem());
            //backgroundJobClient.ContinueJobWith(jobId, () => jobscheduler.CreateTodoItem());
            //backgroundJobClient.ContinueJobWith(jobId, () => jobscheduler.CreateTodoItem());
            //backgroundJobClient.ContinueJobWith(jobId, () => jobscheduler.CreateTodoItem());
            //backgroundJobClient.ContinueJobWith(jobId, () => jobscheduler.CreateTodoItem());

            //Schedule Job / Delayed Job  
            //backgroundJobClient.Schedule(() => jobscheduler.SendMailDaily(email), TimeSpan.FromMinutes(1));
        }
    }
}