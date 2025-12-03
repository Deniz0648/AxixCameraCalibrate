using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace CameraCalibrate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            // Null-safe eriþim: comboBox1 null olabilir veya Text boþ olabilir
            if (string.IsNullOrWhiteSpace(comboBox1?.Text))
            {
                MessageBox.Show("Lütfen bir IP seçin!");
                return;
            }

            string cameraIP = comboBox1.Text.Trim();
            string url = $"http://{cameraIP}/axis-cgi/com/ptz.cgi?auxiliary=reset";

            try
            {
                using var handler = new HttpClientHandler
                {
                    Credentials = new NetworkCredential("User", "Pass")
                };

                using var httpClient = new HttpClient(handler);
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Reset komutu gönderildi! HTTP Status: " + response.StatusCode);
                }
                else
                {
                    MessageBox.Show($"Hata: {response.StatusCode}");
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show("Hata: " + httpEx.Message);
            }
        }
    }
}
