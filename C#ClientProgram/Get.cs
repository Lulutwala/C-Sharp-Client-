using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientProgram
{
    public partial class Get : Form
    {
        public Get()
        {
            InitializeComponent();
        }
        private const string BaseUrl = "http://localhost:4000/clients";
        private string authToken;
        private static readonly HttpClient client = new HttpClient();
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                authToken = textBox1.Text;

                if (string.IsNullOrEmpty(authToken))
                {
                    MessageBox.Show("Unauthorized access.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Instead of expecting a string, expect a List<Client>
                var clientsList = await GetClients(authToken);

                if (clientsList != null)
                {
                    // Display the data in the DataGridView
                    dataGridView1.DataSource = clientsList;

                    MessageBox.Show("Clients data loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to retrieve clients data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during clients request: {ex.Message}");
                MessageBox.Show("Error during clients request. Please check the console for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<List<Client>> GetClients(string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{BaseUrl}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        JObject json = JObject.Parse(responseData);
                        JArray dataArray = json["data"] as JArray;

                        if (dataArray != null)
                        {
                            List<Client> clientsList = dataArray.ToObject<List<Client>>();
                            return clientsList;
                        }
                        else
                        {
                            Console.WriteLine("Failed to find 'data' JSON response.");
                            MessageBox.Show("Failed to find 'data' property in the JSON response.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Cannot get the client. Status code: {response.StatusCode}");
                        MessageBox.Show($"Cannot get the client. Status code: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during HTTP request: {ex.Message}");
                    MessageBox.Show($"Error during HTTP request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
        private class Client
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            token tok = new token();
            tok.Show();
            this.Hide();
        }
    }
}
