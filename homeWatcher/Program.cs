using NLog;
using JusiBase;
using NLog.Targets.ElasticSearch;

namespace homeWatcher
{
    public class Program
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            ElasticSearchTarget elastictarget = new ElasticSearchTarget
            {
                Name = "elastic",
                Uri = "http://jhistorian.prod.j1:9200/",  //Uri = "http://192.168.2.41:32120", 
                Index = "homeWatcher-${level}-${date:format=yyyy-MM-dd}",
                //Index = "historianWriter-${level}-${date:format=yyyy-MM-dd}",
                //Layout = "${logger} | ${threadid} | ${message}",
                //Layout = "${longdate}|${event-properties:item=EventId_Id}|${threadid}|${uppercase:${level}}|${logger}|${hostname}|${message} ${exception:format=tostring}",
                Layout = "${message}",
                IncludeAllProperties = true,
                RequireAuth = true,
                Username = "elastic",
                Password = "9FUR8qkJ14p53mbUXL00"
            };

            JusiBase.LoggingBase logging = new LoggingBase(elastictarget, NLog.LogLevel.Debug, NLog.LogLevel.Fatal);

            logger
                .Info($"Main Startup run");




            WatcherLogic.Instance.Start();

            app.Run();

                        

            logger
                .Info($"Main Startup beendet");
        }
    }
}