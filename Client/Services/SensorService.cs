using Client.Models;
using System;

namespace Client.Services
{
    public class SensorService
    {
        public Sensor GetSensorData(string sensorType)
        {
            var sensorData = new Sensor();

            while (true)
            {
                Console.WriteLine($"Введите scaler для {sensorType} датчика:");
                if (int.TryParse(Console.ReadLine(), out int scaler))
                {
                    sensorData.Scaler = scaler;
                    break;
                }
                else
                {
                    Console.WriteLine("Неверное значение! Пожалуйста, введите целое число.");
                }
            }

            while (true)
            {
                Console.WriteLine($"Введите value для {sensorType} датчика:");
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    sensorData.Value = value;
                    break;
                }
                else
                {
                    Console.WriteLine("Неверное значение! Пожалуйста, введите число с плавающей запятой.");
                }
            }

            return sensorData;
        }
    }
}
