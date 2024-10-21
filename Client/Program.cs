using Client.Models;
using Client.Services;
using System;
using System.Threading.Tasks;

public class Program
{
    private static string _serverAddress;
    private static HttpService _httpService;

    private static int _previousVoltageScaler;
    private static int _previousCurrentScaler;
    private static int _previousPowerScaler;

    static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var configService = new ConfigurationService();
        _serverAddress = configService.ServerAddress;
        _httpService = new HttpService();

        while (true)
        {
            var sensorService = new SensorService();
            var voltageSensorData = sensorService.GetSensorData("Voltage");
            var currentSensorData = sensorService.GetSensorData("Current");
            var powerSensorData = sensorService.GetSensorData("Power");

            await SendData(voltageSensorData, currentSensorData, powerSensorData);

            Console.WriteLine("Данные отправлены. Ожидание следующего ввода через 30 секунд...");
            await Task.Delay(30000);
        }
    }

    private static async Task SendData(Sensor voltage, Sensor current, Sensor power)
    {
        try
        {
            await _httpService.SendSensorDataAsync("voltage/value", voltage.Value, _serverAddress);
            if (_previousVoltageScaler != voltage.Scaler)
            {
                await _httpService.SendSensorDataAsync("voltage/scaler", voltage.Scaler, _serverAddress);
                _previousVoltageScaler = voltage.Scaler;
            }

            await _httpService.SendSensorDataAsync("current/value", current.Value, _serverAddress);
            if (_previousCurrentScaler != current.Scaler)
            {
                await _httpService.SendSensorDataAsync("current/scaler", current.Scaler, _serverAddress);
                _previousCurrentScaler = current.Scaler;
            }

            await _httpService.SendSensorDataAsync("power/value", power.Value, _serverAddress);
            if (_previousPowerScaler != power.Scaler)
            {
                await _httpService.SendSensorDataAsync("power/scaler", power.Scaler, _serverAddress);
                _previousPowerScaler = power.Scaler;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при отправке данных: {ex.Message}");
        }
    }
}
