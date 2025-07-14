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
using System.Windows.Forms;


namespace ClientProgram
{
    public partial class Main : Form
    {
       
        public Main()
        {
            InitializeComponent();
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            Get get = new Get();
            get.Show();
            this.Hide();
        }
        

        private void button6_Click(object sender, EventArgs e)
        {
            token tok = new token();
            tok.Show();
            this.Hide();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            getById getID = new getById();
            getID.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            updateClients update = new updateClients();
            update.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Post post = new Post();
            post.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeleteClient deleteClient = new DeleteClient();
            deleteClient.Show();
            this.Hide();
        }
    }
}
