using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        private Label titleLabel;
        private Label inputLabel;
        private TextBox inputEdit;
        private Button submitButton;
        private Label temperatureLabel;
        private Label descriptionLabel;

        public MainWindow()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeComponent()
        {
            Width = 520;
            Height = 270;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Title = "Sk. Salahuddin Morning 01 Batch";

            Grid mainGrid = new Grid();
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition());
            mainGrid.RowDefinitions.Add(new RowDefinition());

            Content = mainGrid;
        }

        private void InitializeUI()
        {
            titleLabel = new Label();
            titleLabel.Content = "Weather App";
            titleLabel.FontSize = 18;
            titleLabel.FontWeight = FontWeights.Bold;
            titleLabel.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetRow(titleLabel, 0);
            ContentGrid.Children.Add(titleLabel);

            inputLabel = new Label();
            inputLabel.Content = "Enter City Name:";
            inputLabel.FontSize = 12;
            Grid.SetRow(inputLabel, 1);
            ContentGrid.Children.Add(inputLabel);

            inputEdit = new TextBox();
            inputEdit.FontSize = 12;
            Grid.SetRow(inputEdit, 1);
            Grid.SetColumn(inputEdit, 1);
            ContentGrid.Children.Add(inputEdit);

            submitButton = new Button();
            submitButton.Content = "Submit";
            submitButton.FontSize = 12;
            submitButton.Click += UpdateWeather;
            Grid.SetRow(submitButton, 2);
            ContentGrid.Children.Add(submitButton);

            temperatureLabel = new Label();
            temperatureLabel.FontSize = 14;
            Grid.SetRow(temperatureLabel, 3);
            ContentGrid.Children.Add(temperatureLabel);

            descriptionLabel = new Label();
            descriptionLabel.FontSize = 14;
            Grid.SetRow(descriptionLabel, 4);
            ContentGrid.Children.Add(descriptionLabel);
        }

        private void UpdateWeather(object sender, RoutedEventArgs e)
        {
            string city = inputEdit.Text;
            string apiKey = "API KEY";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            using (WebClient client = new WebClient())
            {
                string response = client.DownloadString(url);
                dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(response);

                if (data.main == null)
                {
                    temperatureLabel.Content = "City not found";
                    descriptionLabel.Content = "";
                }
                else
                {
                    double temp = data.main.temp;
                    string description = data.weather[0].description;

                    temperatureLabel.Content = $"Temperature: {temp} °C";
                    descriptionLabel.Content = $"Description: {description}";
                }
            }
        }
    }
}
