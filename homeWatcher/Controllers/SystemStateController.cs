using Microsoft.AspNetCore.Mvc;
using NLog;
using JusiBase;

namespace homeWatcher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemStateController : ControllerBase
    {

        //private readonly ILogger<SystemStateController> _logger;

        //public SystemStateController(ILogger<SystemStateController> logger)
        //{
        //    _logger = logger;
        //}

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MetricBase metric = new MetricBase();

        [HttpGet(Name = "GetSystemState")]
        public SystemState Get()
        {
            logger
                .Debug($"get SystemState");
            return new SystemState
            {
                StatusCounter = WatcherLogic.Instance.StatusCounter,
                StatusMessage = "OK",
                StatusTimestamp = DateTime.Now
            };
           
        }
    }
}