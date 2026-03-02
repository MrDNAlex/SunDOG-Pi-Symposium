using SunDOG.Symposium.Pi.Core;

namespace SunDOG.Symposium.Pi.Display
{
    public class DataCollectionService : BackgroundService
    {
        /// <summary>
        /// Time Stamp of the Start of the Service Recording
        /// </summary>
        private DateTime _StartTime { get; }

        /// <summary>
        /// First Current Sensor Belonging to the Service
        /// </summary>
        private INA219Service _Sensor1 { get; }

        /// <summary>
        /// Second Current Sensor Belonging to the Service
        /// </summary>
        private INA219Service _Sensor2 { get; }

        /// <summary>
        /// Number of Milliseconds between each Sensor Ping Read
        /// </summary>
        private int _FrameDelayMs { get; }

        /// <summary>
        /// Number of Queries per Second that are requested to the Sensor
        /// </summary>
        private int QPS { get; set; } = 5;

        public SensorDataStats DataStats { get; }

        public DataCollectionService(SensorDataStats stats, [FromKeyedServices("Sensor1")] INA219Service s1,
        [FromKeyedServices("Sensor2")] INA219Service s2)
        {
            DataStats = stats;

            _StartTime = DateTime.Now;

            _Sensor1 = s1;
            _Sensor2 = s2;

            _FrameDelayMs = (1000 / QPS);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                double time = SecondsSinceStart();

                DataPoint s1Point = new DataPoint() { Time = time, Value = _Sensor1.ReadCurrentMilli()};
                DataPoint s2Point = new DataPoint() { Time = time, Value = _Sensor2.ReadCurrentMilli() };

                DataStats.AddReadings(s1Point, s2Point);

                await Task.Delay(_FrameDelayMs);
            }
        }
        
        /// <summary>
        /// Updates the Number of Queries per Second that are requested to the Sensors
        /// </summary>
        /// <param name="qps">Updated Queuries per Second</param>
        public void UpdateQPS (int qps)
        {
            QPS = qps;
            DataStats.UpdateQPS(qps);
        }

        /// <summary>
        /// Calculates the Number of Seconds that have elapsed since the start of the recording
        /// </summary>
        /// <returns>Number of Seconds that have elapsed as a Double</returns>
        private double SecondsSinceStart()
        {
            return (DateTime.Now - _StartTime).TotalSeconds;
        }
    }

    public class DataPoint
    {
        public double Time { get; set; }
        public double Value { get; set; }
    }
}
