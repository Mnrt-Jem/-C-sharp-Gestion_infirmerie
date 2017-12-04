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
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
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

        private void AddStudent_Load(object sender, EventArgs e)
        {
            InfosClasses recupAllClasses = new InfosClasses();
            List<string> lesClasses = new List<string>();

            lesClasses = recupAllClasses.RecupInfosClasses();
            comboBoxClasse.DataSource = lesClasses;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonAddStudent_Click(object sender, EventArgs e)
        {
            // Création liste lesErreurs pour stocker les différentes erreurs de saisies rencontrées.
            List<string> lesErreurs = new List<string>();
            
            // Vérification de la saisie du Nom de Famille
            VerifSaisies verifSaisieAjout = new VerifSaisies();
            bool verifInt = verifSaisieAjout.verifInteger(textBoxNom.Text);

            if (verifInt || textBoxNom.TextLength == 0)
            {
                lesErreurs.Add("Nom pas rempli ou contient des chiffres.");
            }
            else
            {
                // Nom en majuscule avant INSERT BDD
                string leNom = verifSaisieAjout.strToUpper(textBoxNom.Text);
            }

            // Vérification saisie prénom
            bool verifIntPrenom = verifSaisieAjout.verifInteger(textBoxPrenom.Text);
            if (verifIntPrenom || textBoxPrenom.TextLength == 0)
            {
                lesErreurs.Add("Prénom pas rempli ou contient des chiffres.");
            }
            else
            {
                // Première lettre du prénom en Majuscule
                string lePrenom = verifSaisieAjout.strToFirstUpper(textBoxPrenom.Text);
            }


            // Vérification saisie des numéros de téléphones
            bool verifTelEleve = verifSaisieAjout.verifTel(textBoxtelEleve.Text);
            bool verifTelParent = verifSaisieAjout.verifTel(textBoxtelParent.Text);
            if (verifTelEleve && verifTelParent)
            {
                string leTelEleve = textBoxtelEleve.Text;
                string leTelParent = textBoxtelParent.Text;
            }
            else
            {
                lesErreurs.Add("Le(s) numéro(s) de téléphone saisi(s) ne sont pas correct(s).");
            }

            // Vérification du nombre d'erreurs trouvées. Si aucune erreur, traitement.
            if (lesErreurs.Count() == 0)
            {
                toolStripStatusLabel1.Text = "";
            }
            else
            {
                string toutesLesErreurs = "";
                foreach(string uneErreur in lesErreurs)
                {
                    toutesLesErreurs += " " + uneErreur;
                }
                toolStripStatusLabel1.Text = toutesLesErreurs;
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            



        }

        
    }
}
