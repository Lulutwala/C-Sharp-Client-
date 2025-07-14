using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;
using System.Net.Http.Json;

namespace ClientProgram
{
    public partial class token : Form
    {
        private const string BaseUrl = "http://localhost:4000";
        private string authToken;
        public token()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string enteredUsername = textBox2.Text;
            string enteredPassword = textBox3.Text;

            // Check if the entered username and password match the expected values
            if (enteredUsername == "Casandra" && enteredPassword == "NAndola")
            {
                authToken = await GetAuthToken($"{BaseUrl}/aut");
                if (authToken != null)
                {
                    textBox1.Text = authToken;
                    MessageBox.Show("Token obtained successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to obtain token. Please enter username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task<string> GetAuthToken(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
                        return result?.Token;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to get token. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during token request: {ex.Message}");
            }

            return null;
        }

        private class TokenResponse
        {
            public string Token { get; set; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }
    }
    

}
