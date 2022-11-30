using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace SerilogTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ba�lang�� : Log.Logger a set edilerek serilog ayarlar� yap�l�r. 2 t�rl� ayar yap�labilir. 1 deki gibi kod dan veya 2 deki gibi appsettings den 

            //1. serilog ayarlar�
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()//konsola kay�t yazma
                .WriteTo.File("logs.txt")//proje dizininde logs dosyas� �retip koyar
                .WriteTo.Seq("http://localhost:5341/")//loglar� g�rselle�tirme. seq i aktif etmek i�in �nce https://datalust.co/download indirip locale kurulum var. kurulum s�ras�nda bize verilen monitor adresini(localhost olan adresi) buraya yaz�yoruz
                .MinimumLevel.Error()//minimum error hatalar� yaz�lcak
                .CreateLogger();

            ////2. serilogu sql ve mongodb ye de atabiliriz. appsettings de ms sql ba�lant�s� ve seq i�in ayarlar var, istenildi�i gibi de�i�tirilebilir
            //IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
            //Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        ///UseSerilog komutu Microsoft.Extensions.Logging a ait ILogger interface i i�in art�k serilog kullan anlam�na gelir. e�er useserilog u eklemeseydik .net in kendi loglar�na kayd� atar
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
