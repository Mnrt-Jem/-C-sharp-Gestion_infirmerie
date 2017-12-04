using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilisateursBLL;
using UtilisateursBO;
using System.Data.SqlClient;

namespace UtilisateursGUI
{
    public partial class ConnexionGUI : Form
    {
        public ConnexionGUI()
        {
            InitializeComponent();
        }

        // Passage souris LOGIN BOX
        private void loginBox_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Entrez votre nom d'utilisateur";
            toolStripStatusLabel1.ForeColor = Color.Black;
        }
        // Passage souris OUT LOGIN BOX
        private void loginBox_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel1.ForeColor = Color.Black;
        }


        // Passage souris MDP BOX
        private void passBox_MouseHover(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Entrez votre mot de passe";
            toolStripStatusLabel1.ForeColor = Color.Black;
        }
        // Passage souris OUT MDP BOX
        private void passBox_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            toolStripStatusLabel1.ForeColor = Color.Black;
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            Connection connect = new Connection(loginBox.Text, passBox.Text);
            bool verifLogin = connect.verifConnection();
            
            if (verifLogin)
            {
                toolStripStatusLabel1.Text = "Connexion réussie.";
                Main form = new Main();
                form.Show();
                this.Hide();

            }
            else
            {
                toolStripStatusLabel1.Text = "Connexion échouée. Vérifiez vos identifiants et votre mot de passe.";
                toolStripStatusLabel1.ForeColor = Color.Red;
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        

        


    }
}
