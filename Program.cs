
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
    /// ����������� �����, ��������� � ������������� IHostBuilder -
    /// ������, ������� � ���� ������� ������� ���� ��� ������������� ������ ����������
    /// </summary>

    public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
           .ConfigureWebHostDefaults(webBuilder => {
               webBuilder.UseStartup<Startup>();
               /* //��� ��������������� ���� �� ����������� ������ �� ���������
                webBuilder.UseWebRoot("Views");
                */
           });


}
