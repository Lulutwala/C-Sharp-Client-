using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ClientProgram
{
    public partial class Post : Form
    {
        public Post()
        {
            InitializeComponent();
        }
        private const string BaseUrl = "http://localhost:4000/clients";
        private string authToken;
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();

        }

        private async void button1_Click(object sender, EventArgs e)
        {

            try
            {
               
                string authToken = textBox1.Text;
                string clientName = textBox3.Text;
                string clientPassword = textBox4.Text;

                
                if (string.IsNullOrEmpty(authToken))
                {
                    MessageBox.Show("Unauthorized acceess.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                var addClientResponse = await AddClient(authToken, clientName, clientPassword);
                if (addClientResponse != null)
                {
                    MessageBox.Show("Add Client Response: " + addClientResponse, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during add client request: {ex.Message}");
                MessageBox.Show("Error during add client request. Please check the console for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<string> AddClient(string token, string name, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var content = new FormUrlEncodedContent(new[]
                {
            new KeyValuePair<string, string>("name", name),
            new KeyValuePair<string, string>("password", password)
        });

                HttpResponseMessage response = await client.PostAsync(BaseUrl, content);
                return await response.Content.ReadAsStringAsync();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        { 
            token tok = new token();
            tok.Show();
            this.Hide();
        }

    }
}
