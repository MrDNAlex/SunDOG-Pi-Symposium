namespace SunDOG.Symposium.Pi.Display
{
    public class RollingAverage
    {
        /// <summary>
        /// Number of Queries per Second that are requested to the Sensor
        /// </summary>
        public int QPS { get; set; }

        /// <summary>
        /// The Time Frame the Rolling Average covers in Seconds
        /// </summary>
        public int TimeFrameSeconds { get; }

        /// <summary>
        /// List of DataPoints collected from the Sensors
        /// </summary>
        private List<DataPoint> _Data { get; }

        /// <summary>
        /// The Window Size of the Rolling Average
        /// </summary>
        private int _WindowSize { get; set; }

        /// <summary>
        /// The Sum of the DataPoints values
        /// </summary>
        private double _Sum { get; set; }

        /// <summary>
        /// The Average of the DataPoints values
        /// </summary>
        public double Average => _Data.Count == 0 ? 0 : _Sum / _Data.Count;

        /// <summary>
        /// Returns the DataPoint History contained in the Rolling Average
        /// </summary>
        /// <returns></returns>
        public List<DataPoint> History()
        {
            return _Data.ToList();
        }

        public RollingAverage(int timeSec, int fps)
        {
            _Data = new List<DataPoint>();

            QPS = fps;
            TimeFrameSeconds = timeSec;
            _WindowSize = TimeFrameSeconds * QPS;

            _Sum = 0;
        }

        /// <summary>
        /// Adds a Reading to the Rolling Average
        /// </summary>
        /// <param name="data">DataPoint added to the Rolling Average</param>
        public void AddReading(DataPoint data)
        {
            _Sum += data.Value;
            _Data.Add(data);

            if (_Data.Count > _WindowSize)
            {
                _Sum -= _Data[0].Value;
                _Data.RemoveAt(0);
            }
        }

        /// <summary>
        /// Updates the Rolling Average Window Size based off the new Number of Queries per Second that are requested to the Sensors
        /// </summary>
        /// <param name="qps">Updated Queuries per Second</param>
        public void UpdateQPS(int qps)
        {
            QPS = qps;
            _WindowSize = QPS * TimeFrameSeconds;
        }
    }
}
