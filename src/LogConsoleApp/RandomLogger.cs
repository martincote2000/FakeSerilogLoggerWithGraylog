using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bogus;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace ConsoleApp1
{
    public class RandomLogger
    {
        private readonly Random _random;
        private ILogger _backendLogger;
        private ILogger _frontendLogger;

        public RandomLogger()
        {
            _random = new Random();
        }

        public void InitializeLogger()
        {
            _backendLogger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Graylog("localhost", 12201, TransportType.Udp)
                .CreateLogger();

            _frontendLogger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.WithProperty("Application", "AskicaCT-Angular")
                .Enrich.WithProperty("UserName", "Joe Nathan")
                .WriteTo.Graylog("localhost", 12201, TransportType.Http)
                .CreateLogger();
        }

        public void Start(CancellationTokenSource token)
        {
            InitializeLogger();
            
            var lorem = new Bogus.DataSets.Lorem();
            var sw = Stopwatch.StartNew();

            while (!token.IsCancellationRequested)
            {
                int logType = _random.Next(1, 4);
                int appId = _random.Next(1, 6);
                var message = lorem.Sentence(_random.Next(3, 10));

                _backendLogger
                    .ForContext("Application", GetAppName(appId))
                    .ForContext(nameof(Environment.UserName), Environment.UserName)
                    .ForContext("ElapsedMilliseconds", sw.ElapsedMilliseconds);

                sw.Restart();

                switch (logType)
                {
                    case 2:
                        {
                            _backendLogger.Warning(message);
                            break;
                        }
                    case 3:
                        {
                            _backendLogger.Error(new Exception(message), "An error occured");
                            break;
                        }
                    case 4:
                    {
                        _frontendLogger.Error(new Exception(message), "Frontend - An error occured");
                        break;
                    }
                    default:
                        {
                            _backendLogger.Information(message);
                            break;
                        }
                }

                int delay = _random.Next(200, 1300);
                Thread.Sleep(delay);
            }

        }

        private string GetAppName(int app)
        {
            switch (app)
            {
                case 1:
                    return "AskidaCT";
                case 2:
                    return "Askida.Microservice.Accounting";
                case 3:
                    return "Askida.Microservice.Crm";
                case 4:
                    return "Askida.Microservice.Scheduler";
                case 5:
                    return "Askida.Microservice.Mailer";
                default:
                    return "Askida.LoggerApp";
            }
        }



    }
}
