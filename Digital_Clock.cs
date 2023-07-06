using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace DigitalClockApp
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private string units;
        private bool isMetric;

        public MainWindow()
        {
            InitializeComponent();

            Title = "Digital Clock";
            Background = new SolidColorBrush(Color.FromRgb(39, 39, 39));

            dateLabel.FontSize = 60;
            dateLabel.HorizontalAlignment = HorizontalAlignment.Center;
            dateLabel.VerticalAlignment = VerticalAlignment.Center;
            dateLabel.Foreground = new SolidColorBrush(Colors.White);

            timeLabel.FontSize = 200;
            timeLabel.HorizontalAlignment = HorizontalAlignment.Center;
            timeLabel.VerticalAlignment = VerticalAlignment.Center;
            timeLabel.Foreground = new SolidColorBrush(Color.FromRgb(243, 156, 18));

            temperatureLabel.FontSize = 45;
            temperatureLabel.HorizontalAlignment = HorizontalAlignment.Center;
            temperatureLabel.VerticalAlignment = VerticalAlignment.Center;
            temperatureLabel.Foreground = new SolidColorBrush(Colors.White);

            unitButton.Content = "Switch to Fahrenheit";
            unitButton.Click += ToggleUnits;
            unitButton.Background = new SolidColorBrush(Color.FromRgb(52, 152, 219));
            unitButton.FontWeight = FontWeights.Bold;
            unitButton.Foreground = new SolidColorBrush(Colors.White);

            var gradient = new LinearGradientBrush();
            gradient.StartPoint = new Point(0, 0);
            gradient.EndPoint = new Point(1, 1);
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(52, 152, 219), 0));
            gradient.GradientStops.Add(new GradientStop(Color.FromRgb(231, 76, 60), 1));
            Background = gradient;

            units = "metric";
            isMetric = true;
            ToggleUnits(null, null);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += UpdateClock;
            timer.Start();
        }

        private void UpdateClock(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            var date = now.ToString("dddd, MMMM dd, yyyy");
            var time = now.ToString("hh:mm:ss tt");
            var temperature = GetTemperature();
            var temperatureCelsius = $"{temperature:F1} °C";
            var temperatureFahrenheit = $"{(temperature * 9 / 5) + 32:F1} °F";
            var temperatureStr = isMetric ? temperatureCelsius : $"{temperatureCelsius} / {temperatureFahrenheit}";

            dateLabel.Content = date;
            timeLabel.Content = time;
            temperatureLabel.Content = temperatureStr;
        }

        private double GetTemperature()
        {
            var apiKey = "API KEY";
            var city = "Khulna";
            var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units={units}";

            using (var client = new WebClient())
            {
                var response = client.DownloadString(url);
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                var temperature = (double)data.main.temp;
                return temperature;
            }
        }

        private void ToggleUnits(object sender, RoutedEventArgs e)
        {
            isMetric = !isMetric;
            units = isMetric ? "metric" : "imperial";
            unitButton.Content = isMetric ? "Switch to Fahrenheit" : "Switch to Celsius";
        }
    }
}
