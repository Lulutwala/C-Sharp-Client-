using IdentityModel;
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
    public partial class getById : Form
    {
        public getById()
        {
            InitializeComponent();
           
        }
        private const string BaseUrl = "http://localhost:4000/clients";
        private string authToken;
        private void button1_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }
        

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                authToken = textBox2.Text;

                if (string.IsNullOrEmpty(authToken))
                {
                    MessageBox.Show("Unauthorized access", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int clientId = Convert.ToInt32(textBox1.Text);
                var clientsList = await GetClientById(authToken, clientId);

                if (clientsList != null)
                {
                    var bindingList = new BindingList<Client>(clientsList);
                    var source = new BindingSource(bindingList, null);
                    dataGridView1.DataSource = source;
                }
                else
                {
                    MessageBox.Show("Failed to get client data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during client request: {ex.Message}");
                MessageBox.Show($"Error during client request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<List<Client>> GetClientById(string token, int clientId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{BaseUrl}/{clientId}");

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                 
                        JObject json = JObject.Parse(responseData);
                        JArray dataArray = json["data"] as JArray;

                        if (dataArray != null)
                        {
                            // Deserializing the JSON array to a list of Client objects
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

        private void button3_Click(object sender, EventArgs e)
        {
            token token = new token();
            token.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
