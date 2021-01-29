using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BowlingScore
{
    class Program
    {
        // TODO: Group in folders
        static void Main(string[] args)
        {
            //var inputFilePath = @"./input.txt";
            ////InputReader reader = new InputReader();
            //IInputReader reader = new InputReader();
            ////var scoreArray = reader.GetScoreArray(inputFilePath);
            //ScoreCalculator calculator = new ScoreCalculator(reader, inputFilePath);
            //calculator.CalculateScore();
            ////var nextThrows = calculator.GetNextTwoThrows(2);
            ////calculator.test2Throws(0);
            using IHost host = CreateHostBuilder(args).Build();
            DoStuff(host.Services, "scope 1");
            //host.Run();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IInputReader, InputReader>());

        private static void DoStuff(IServiceProvider services, string scope)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            var inputFilePath = @"./input.txt";
            IInputReader reader = provider.GetRequiredService<IInputReader>();
            ScoreCalculator sc = new ScoreCalculator(reader, inputFilePath);
            sc.CalculateScore();

            Console.WriteLine("END");
        }
    }
}
