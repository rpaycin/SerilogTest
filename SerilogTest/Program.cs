using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SerilogTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //serilog ayarlarý
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()//konsola kayýt yazma
                .WriteTo.File("logs.txt")//proje dizininde logs dosyasý üretip koyar
                .WriteTo.Seq("http://localhost:5341/")//loglarý görselleþtirme. seq i aktif etmek için önce https://datalust.co/download indirip locale kurulum var. kurulum sýrasýnda bize verilen monitor adresini(localhost olan adresi) buraya yazýyoruz
                .MinimumLevel.Error()//minimum error hatalarý yazýlcak
                .CreateLogger();

            //serilogu sql ve mongodb ye de atabiliriz

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
