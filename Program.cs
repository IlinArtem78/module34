
using Microsoft.AspNetCore.Hosting;
using HomeApi.Configuration;
using module34;

class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();

    }

    /// <summary>
    /// —татический метод, создающий и настраивающий IHostBuilder -
    /// объект, который в свою очередь создает хост дл€ развертывани€ нашего приложени€
    /// </summary>

    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder => {
               webBuilder.UseStartup<Startup>();
               /* //дл€ переопределени€ пути до статических файлов по умолчанию
                webBuilder.UseWebRoot("Views");
                */
           });


}
