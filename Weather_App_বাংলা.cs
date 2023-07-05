using System;
using System.Net;
using System.Windows.Forms;

namespace WeatherApp
{
    public class WeatherApp : Form
    {
        private Label titleLabel;
        private FlowLayoutPanel inputLayout;
        private Label inputLabel;
        private TextBox inputEdit;
        private Button submitButton;
        private FlowLayoutPanel weatherLayout;
        private Label temperatureLabel;
        private Label descriptionLabel;
        
        public WeatherApp()
        {
            this.Text = "Sk. Salahuddin Morning 01 Batch";
            this.ClientSize = new System.Drawing.Size(550, 300);
            
            titleLabel = new Label();
            titleLabel.Text = "আবহাওয়ার তথ্য";
            titleLabel.Font = new System.Drawing.Font("Arial", 18);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Dock = DockStyle.Top;
            
            inputLayout = new FlowLayoutPanel();
            inputLabel = new Label();
            inputLabel.Text = "শহরের নাম লিখুন:";
            inputLabel.Font = new System.Drawing.Font("Arial", 12);
            
            inputEdit = new TextBox();
            inputEdit.Font = new System.Drawing.Font("Arial", 12);
            inputEdit.Multiline = false;
            inputEdit.Width = 200;
            
            inputLayout.Controls.Add(inputLabel);
            inputLayout.Controls.Add(inputEdit);
            
            submitButton = new Button();
            submitButton.Text = "অনুসন্ধান";
            submitButton.Font = new System.Drawing.Font("Arial", 12);
            submitButton.Click += UpdateWeather;
            
            weatherLayout = new FlowLayoutPanel();
            
            temperatureLabel = new Label();
            temperatureLabel.Font = new System.Drawing.Font("Arial", 14);
            
            descriptionLabel = new Label();
            descriptionLabel.Font = new System.Drawing.Font("Arial", 14);
            
            weatherLayout.Controls.Add(temperatureLabel);
            weatherLayout.Controls.Add(descriptionLabel);
            
            this.Controls.Add(titleLabel);
            this.Controls.Add(inputLayout);
            this.Controls.Add(submitButton);
            this.Controls.Add(weatherLayout);
        }
        
        private void UpdateWeather(object sender, EventArgs e)
        {
            string city = inputEdit.Text;
            string key = "your_api_key";  // Replace with your API key
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={key}&units=metric";
            
            using (WebClient client = new WebClient())
            {
                try
                {
                    string json = client.DownloadString(url);
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    
                    if (data.main == null)
                    {
                        temperatureLabel.Text = "City not found";
                        descriptionLabel.Text = "";
                    }
                    else
                    {
                        float temp = data.main.temp;
                        string description = data.weather[0].description;
                        
                        temperatureLabel.Text = $"Temperature: {temp} °C";
                        descriptionLabel.Text = $"Description: {description}";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        
        [STAThread]
        static void Main()
        {
            Application.Run(new WeatherApp());
        }
    }
}
