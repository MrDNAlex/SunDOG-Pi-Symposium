
namespace SunDOG.Symposium.Pi.Display
{
    public class SensorDataStats
    {
        public RollingAverage Sensor1_5SAverage { get; set; }
        public RollingAverage Sensor2_5SAverage { get; set; }

        public RollingAverage Sensor1_10SAverage { get; set; }
        public RollingAverage Sensor2_10SAverage { get; set; }

        public RollingAverage Sensor1_30SAverage { get; set; }
        public RollingAverage Sensor2_30SAverage { get; set; }

        public RollingAverage Sensor1_120SAverage { get; set; }
        public RollingAverage Sensor2_120SAverage { get; set; }

        /// <summary>
        /// Number of Queries per Second that are requested to the Sensor
        /// </summary>
        public int QPS { get; set; }

        public SensorDataStats()
        {
            QPS = 5;

            Sensor1_5SAverage = new RollingAverage(5, QPS);
            Sensor2_5SAverage = new RollingAverage(5, QPS);

            Sensor1_10SAverage = new RollingAverage(10, QPS);
            Sensor2_10SAverage = new RollingAverage(10, QPS);

            Sensor1_30SAverage = new RollingAverage(30, QPS);
            Sensor2_30SAverage = new RollingAverage(30, QPS);

            Sensor1_120SAverage = new RollingAverage(120, QPS);
            Sensor2_120SAverage = new RollingAverage(120, QPS);
        }

        /// <summary>
        /// Adds Sesnor Readings to the Rolling Averages
        /// </summary>
        /// <param name="s1Data">Sensor 1 Data Point</param>
        /// <param name="s2Data">Sensor 2 Data Point</param>
        public void AddReadings(DataPoint s1Data, DataPoint s2Data)
        {
            Sensor1_5SAverage.AddReading(s1Data);
            Sensor1_10SAverage.AddReading(s1Data);
            Sensor1_30SAverage.AddReading(s1Data);
            Sensor1_120SAverage.AddReading(s1Data);

            Sensor2_5SAverage.AddReading(s2Data);
            Sensor2_10SAverage.AddReading(s2Data);
            Sensor2_30SAverage.AddReading(s2Data);
            Sensor2_120SAverage.AddReading(s2Data);
        }

        /// <summary>
        /// Updates the Rolling Average Window Size based off the new Number of Queries per Second that are requested to the Sensors
        /// </summary>
        /// <param name="qps">Updated Queuries per Second</param>
        public void UpdateQPS (int qps)
        {
            QPS = qps;

            Sensor1_5SAverage.UpdateQPS(qps);
            Sensor1_10SAverage.UpdateQPS(qps);
            Sensor1_30SAverage.UpdateQPS(qps);
            Sensor1_120SAverage.UpdateQPS(qps);

            Sensor2_5SAverage.UpdateQPS(qps);
            Sensor2_10SAverage.UpdateQPS(qps);
            Sensor2_30SAverage.UpdateQPS(qps);
            Sensor2_120SAverage.UpdateQPS(qps);
        }
    }
}
