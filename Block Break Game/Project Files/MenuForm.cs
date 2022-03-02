using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameProject2
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            //hide the menu form and show the game form
            FormGameNormal f1 = new FormGameNormal();
            this.Hide();
            f1.Show();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //exit the application
            Application.Exit();
        }
    }
}
