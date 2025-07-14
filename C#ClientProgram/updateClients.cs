using IdentityModel;
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
    public partial class updateClients : Form
    {
        public updateClients()
        {
            InitializeComponent();
        }

        private const string BaseUrl = "http://localhost:4000/clients";
        private string authToken;

        private async Task<string> UpdateClient(string token, int clientId, string name, string password)
        {
            using (HttpClient client = new HttpClient())
            {
         
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("name", name),
                    new KeyValuePair<string, string>("password", password)
                });

                HttpResponseMessage response = await client.PutAsync($"{BaseUrl}/{clientId}", content);
                return await response.Content.ReadAsStringAsync();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                string authToken = textBox1.Text;
                int clientId = Convert.ToInt32(textBox2.Text); 
                string clientName = textBox3.Text; 
                string clientPassword = textBox4.Text; 

                if (string.IsNullOrEmpty(authToken))
                {
                    MessageBox.Show("Unauthorized access.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Example: Make a PUT request to update a client
                var updateClientResponse = await UpdateClient(authToken, clientId, clientName, clientPassword);
                if (updateClientResponse != null)
                {
                    MessageBox.Show("Update Client Response: " + updateClientResponse, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during update client request: {ex.Message}");
                MessageBox.Show("Error during update client request. Please check the console for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();

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
