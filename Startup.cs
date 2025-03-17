using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using HomeApi.Configuration;
using System.Reflection;
using HomeApi;
using HomeApi.Data.Repos;
using HomeApi.Data;
using HomeApi.Contracts.Validation;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;

namespace module34
{
    public class Startup
    {
        /// <summary>
        /// Загрузка конфигурации из файла Json
        /// </summary>
        private IConfiguration Configuration
        { get; } = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json")
            .AddJsonFile("D:\\WORK\\module34\\Jsons\\HomeOptions.json")
            .Build();
          


        public void ConfigureServices(IServiceCollection services)
        {
            // Подключаем автомаппинг
            var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            services.AddAutoMapper(assembly);

            // регистрация сервиса репозитория для взаимодействия с базой данных
            services.AddSingleton<IDeviceRepository, DeviceRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HomeApiContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
            
          // Подключаем валидацию
          services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddDeviceRequestValidator>());
            
            // Добавляем новый сервис
            services.Configure<HomeOptions>(Configuration);

            // Загружаем только адресс (вложенный Json-объект))
            services.Configure<Address>(Configuration.GetSection("Address"));

            // Нам не нужны представления, но в MVC бы здесь стояло AddControllersWithViews()
            services.AddControllers();
            // поддерживает автоматическую генерацию документации WebApi с использованием Swagger
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeApi", Version = "v1" }); });
        }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Проставляем специфичные для запуска при разработке свойства
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // Сопоставляем маршруты с контроллерами
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
