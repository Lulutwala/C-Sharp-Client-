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
    public partial class DeleteClient : Form
    {
        public DeleteClient()
        {
            InitializeComponent();
        }

        private const string BaseUrl = "http://localhost:4000/clients";
        private string authToken;


        private void button5_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            token token = new token();
            token.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                string authToken = textBox1.Text;
                int clientId = Convert.ToInt32(textBox2.Text); 

                if (string.IsNullOrEmpty(authToken))
                {
                    MessageBox.Show("Please enter a token first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

               
                var deleteClientResponse = await Delete(authToken, clientId);
                if (deleteClientResponse != null)
                {
                    MessageBox.Show("Delete Client Response: " + deleteClientResponse, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during delete client request: {ex.Message}");
                MessageBox.Show("Error during delete client request. Please check the console for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<string> Delete(string token, int clientId)
        {
            using (HttpClient client = new HttpClient())
            {
                
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                HttpResponseMessage response = await client.DeleteAsync($"{BaseUrl}/{clientId}");
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
