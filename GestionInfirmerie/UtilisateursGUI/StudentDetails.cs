using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UtilisateursBLL;

namespace UtilisateursGUI
{
    public partial class StudentDetails : Form
    {
        private string nom;
        private string prenom;
        private List<string> classe;
        private string dateNaissance;
        private string santeEleve;
        private string telParent;
        private string telEleve;
        private string ideleve;
        
        public StudentDetails()
        {
            InitializeComponent();
        }

        public StudentDetails(string nom, string prenom, List<string> classe, string dateNaissance, string santeEleve, string telParent, string telEleve, string id_eleve)
        {
            // Initialisation de la page de détail depuis le DATAGRIDVIEW page d'accueil
            this.nom = nom;
            this.prenom = prenom;
            this.classe = classe;
            this.dateNaissance = dateNaissance;
            this.santeEleve = santeEleve;
            this.telParent = telParent;
            this.telEleve = telEleve;
            this.ideleve = id_eleve;

            InitializeComponent();
        }

        private void StudentDetails_Load(object sender, EventArgs e)
        {
            // Remplissage des champs du formulaire de modification d'un élève
            this.textBoxNom.Text = nom;
            this.textBoxPrenom.Text = prenom;
            this.comboBoxClasse.DataSource = classe;
            this.textBoxDate.Text = dateNaissance;
            this.textBoxSante.Text = santeEleve;
            this.textBoxtelParent.Text = telParent;
            this.textBoxtelEleve.Text = telEleve;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            // Retour page d'accueil
            this.Hide();
            Main frmMain = new Main();
            frmMain.Show();
        }

        private void buttonModifEleve_Click(object sender, EventArgs e)
        {
            // Si les champs obligatoires sont vides, message d'erreur
            if (this.textBoxNom.TextLength == 0 || this.textBoxPrenom.TextLength == 0 || this.textBoxDate.TextLength == 0)
            {
                toolStripStatusLabel1.Text = "Le nom, prénom, date de naissance ou la classe n'ont pas été correctement renseignés.";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            else
            {
                toolStripStatusLabel1.Text = "";
                EleveDetail eleve = new EleveDetail(textBoxNom.Text, textBoxPrenom.Text, comboBoxClasse.Text, textBoxDate.Text, textBoxSante.Text, textBoxtelParent.Text, textBoxtelEleve.Text, this.ideleve);
                bool result = eleve.ModificationEleve();
                // Si la req SQL s'est bien passée, result vaut TRUE, modifications se sont bien passées
                if (result)
                {
                    //toolStripStatusLabel1.Text = "Modifications effectuées avec succès !";
                    //toolStripStatusLabel1.ForeColor = Color.Green;
                    var resultBox = MessageBox.Show("Modifications effectuées avec succès", "Fermeture formulaire", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (resultBox == DialogResult.OK)
                    {
                        this.Hide();
                        Main Index = new Main();
                        Index.Show();
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = "Une erreur est survenue lors de la modification des données. Réessayez plus tard.";
                    toolStripStatusLabel1.ForeColor = Color.Red;
                }
            }
        }

        private void panelAjoutEleve_Click(object sender, EventArgs e)
        {
            AddStudent AjoutEleve = new AddStudent();
            AjoutEleve.Show();
            this.Hide();
        }

        private void labelAjoutEleve_Click(object sender, EventArgs e)
        {
            AddStudent AjoutEleve = new AddStudent();
            AjoutEleve.Show();
            this.Hide();
        }



        private void panelAccueil_Click(object sender, EventArgs e)
        {
            Main Accueil = new Main();
            Accueil.Show();
            this.Hide();
        }

        private void labelAccueil_Click(object sender, EventArgs e)
        {
            Main Accueil = new Main();
            Accueil.Show();
            this.Hide();
        }

        
    }
}
