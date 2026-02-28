using Iot.Device.Adc;
using System.Device.I2c;

namespace SunDOG.Symposium.Pi.Core
{
    public class INA219Service : IDisposable
    {
        private readonly Ina219 _Sensor;

        private readonly I2cDevice _Device;

        private const double SHUNT_WRAP = 0.65536; // 65536 * 10uV

        public INA219Service(int busId, int address = 0x40)
        {
            I2cConnectionSettings settings = new I2cConnectionSettings(busId, address);
            _Device = I2cDevice.Create(settings);
            _Sensor = new Ina219(_Device);

            // Resets the chip to default 12-bit continuous mode
            _Sensor.Reset();

            // Apply Python's fractional LSB
            // 2. Configure Calibration
            // We use the driver's built-in method instead of manual I2C writes.
            // This updates the hardware chip AND the internal C# math variables.
            // ushort calibrationValue = 4096;
            // float currentLsb = 0.00001f;            
            ushort calibrationValue = 4194;
            float currentLsb = 0.00009765625f;
            _Sensor.SetCalibration(calibrationValue, currentLsb);
        }

        private double CorrectReading(double rawValue, double physicalMax, double wrapConstant)
        {
            // If the value is higher than physically possible, it is a wrapped negative number
            if (rawValue > physicalMax)
            {
                return rawValue - wrapConstant;
            }
            return rawValue;
        }

        public void Test()
        {
            double busVoltage = _Sensor.ReadBusVoltage().Volts;
            double rawShunt = _Sensor.ReadShuntVoltage().Volts;
            double rawCurrent = _Sensor.ReadCurrent().Milliamperes;

            // 1. Fix Two's Complement on the Shunt Voltage
            double shuntVoltage = CorrectReading(rawShunt, 0.320, SHUNT_WRAP);

            // 2. Ohm's Law calculates pure, uncorrupted current (I = V / R)
            // I replace _Sensor.ReadCurrent() with this:
            double current = shuntVoltage / 0.1;

            // 3. Power Law (P = I * V)
            // I replace _Sensor.ReadPower() with this:
            double power = current * busVoltage;

            double shuntMilliVolts = shuntVoltage * 1000.0;
            double currentMilliAmps = current * 1000.0;
            double powerMilliWatts = power * 1000.0;

            Console.WriteLine($"Bus Voltage:   {busVoltage:F3} V");
            Console.WriteLine($"Shunt Voltage: {shuntMilliVolts:F3} mV");
            Console.WriteLine($"Bus Current:   {currentMilliAmps:F3} mA Raw : {rawCurrent} mA");
            Console.WriteLine($"Supply Power:  {powerMilliWatts:F3} mW");
            Console.WriteLine();
        }

        public double ReadCurrentMilli()
        {
            double rawCurrent = _Sensor.ReadCurrent().Milliamperes;

            return rawCurrent;
        }

        public (double Volts, double Amps) ReadData()
        {
            // 1. Read standard bus voltage
            double voltage = _Sensor.ReadBusVoltage().Volts;

            // 2. Read Shunt Voltage and fix the C# negative number bug
            double rawShunt = _Sensor.ReadShuntVoltage().Volts;
            double shuntVoltage = CorrectReading(rawShunt, 0.320, SHUNT_WRAP);

            // 3. Ohm's Law calculates pure, uncorrupted current (I = V/R)
            double current = shuntVoltage / 0.1;

            return (voltage, current);
        }

        public void Dispose()
        {
            _Sensor?.Dispose();
            _Device?.Dispose();
        }
    }
}
