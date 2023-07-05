// To run this code, you'll need to create a new Windows Forms project in your preferred C# development environment.

using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebBrowserApp
{
    public class MyWebBrowser : Form
    {
        private TextBox searchBox;
        private Button goButton;
        private Button backButton;
        private Button forwardButton;
        private WebBrowser webBrowser;

        public MyWebBrowser()
        {
            InitializeComponents();
            SetupBrowser();
        }

        private void InitializeComponents()
        {
            // Window setup
            Text = "Sk. Salahuddin - Morning 01 Batch";
            Width = 800;
            Height = 600;

            // Search box
            searchBox = new TextBox
            {
                PlaceholderText = "Search Google",
                Height = 30,
                Left = 10,
                Top = 10,
                Width = 500,
                Font = new Font("Arial", 12)
            };

            // Go button
            goButton = new Button
            {
                Text = "GO",
                Height = 30,
                Left = searchBox.Right + 10,
                Top = 10,
                Width = 50
            };

            // Back button
            backButton = new Button
            {
                Text = "<",
                Height = 30,
                Left = goButton.Right + 10,
                Top = 10,
                Width = 30
            };

            // Forward button
            forwardButton = new Button
            {
                Text = ">",
                Height = 30,
                Left = backButton.Right + 5,
                Top = 10,
                Width = 30
            };

            // Web browser control
            webBrowser = new WebBrowser
            {
                Dock = DockStyle.Fill
            };

            // Event handlers
            goButton.Click += GoButton_Click;
            backButton.Click += BackButton_Click;
            forwardButton.Click += ForwardButton_Click;
            searchBox.KeyPress += SearchBox_KeyPress;

            // Adding controls to the form
            Controls.Add(searchBox);
            Controls.Add(goButton);
            Controls.Add(backButton);
            Controls.Add(forwardButton);
            Controls.Add(webBrowser);
        }

        private void SetupBrowser()
        {
            webBrowser.Url = new Uri("https://www.google.com/");
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            Navigate(searchBox.Text);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoBack)
                webBrowser.GoBack();
        }

        private void ForwardButton_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoForward)
                webBrowser.GoForward();
        }

        private void SearchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Navigate(searchBox.Text);
                e.Handled = true;
            }
        }

        private void Navigate(string url)
        {
            if (!url.StartsWith("http"))
                url = "https://www.google.com/search?q=" + url;

            webBrowser.Navigate(url);
        }

        [STAThread]
        public static void Main()
        {
            Application.Run(new MyWebBrowser());
        }
    }
}
