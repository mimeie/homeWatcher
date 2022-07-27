using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NLog;
using JusiBase;

namespace homeWatcher
{
    public sealed class WatcherLogic
    {

        private static volatile WatcherLogic _instance;
        private static object _syncRoot = new object();

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private MetricBase metric = new MetricBase();

        private int _statusCounter;


        public static WatcherLogic Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new WatcherLogic();
                        }
                    }
                }

                return _instance;
            }
        }

        public void Stop()
        {
        }

        public void Start()
        {
            metric.stopwatch.Restart();

            logger
               .Info($"WatcherLogic wird initalisiert");

            _statusCounter = 0;

            logger
               .WithProperty("Prozessdauer", metric.stopwatch.ElapsedMilliseconds)
               .Info($"WatcherLogic Logic gestartet");
        }


        public int StatusCounter
        {
            get {

                metric.stopwatch.Restart();
                if (_statusCounter > 10000)
                {
                    _statusCounter = 0;
                    logger
                        .Info($"StatusCounter rückgesetzt");
                }

                _statusCounter++;
                logger
                    .WithProperty("Prozessdauer", metric.stopwatch.ElapsedMilliseconds)
                    .WithProperty("StatusCounter", _statusCounter)
                        .Info($"StatusCounter aktueller Wert");
                return _statusCounter;
            
            }
        }




    }
}
