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
            //baþlangýç : Log.Logger a set edilerek serilog ayarlarý yapýlýr. 2 türlü ayar yapýlabilir. 1 deki gibi kod dan veya 2 deki gibi appsettings den 

            //1. serilog ayarlarý
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()//konsola kayýt yazma
                .WriteTo.File("logs.txt")//proje dizininde logs dosyasý üretip koyar
                .WriteTo.Seq("http://localhost:5341/")//loglarý görselleþtirme. seq i aktif etmek için önce https://datalust.co/download indirip locale kurulum var. kurulum sýrasýnda bize verilen monitor adresini(localhost olan adresi) buraya yazýyoruz
                .MinimumLevel.Error()//minimum error hatalarý yazýlcak
                .CreateLogger();

            ////2. serilogu sql ve mongodb ye de atabiliriz. appsettings de ms sql baðlantýsý ve seq için ayarlar var, istenildiði gibi deðiþtirilebilir
            //IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
            //Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        ///UseSerilog komutu Microsoft.Extensions.Logging a ait ILogger interface i için artýk serilog kullan anlamýna gelir. eðer useserilog u eklemeseydik .net in kendi loglarýna kaydý atar
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
