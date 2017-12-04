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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            panelTableau.Hide();
        }

        private void buttonLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Au clic du bouton de recherche, récupération des données et recherche LIKE Nom et prénom
        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // Si champs de recherche vide
            if (textSearchEleve.TextLength == 0)
            {
                toolStripStatusLabel1.Text = "Veuillez entrer une valeur dans le champs de recherche.";
                toolStripStatusLabel1.ForeColor = Color.Red;
            }
            // Sinon lancement de la recherche
            else
            {
                SearchEleve recherche = new SearchEleve(textSearchEleve.Text);
                string Eleves = recherche.NbSearchResult();
                int nbEleves = 0;
                bool result = Int32.TryParse(Eleves, out nbEleves);
                // Récupération du nombre d'élèves correspondant à la recherche. Si 0 : pas d'élèves trouvés, si > 10 : recherche trop large
                if (result)
                {
                    if (nbEleves == 0)
                    {
                        toolStripStatusLabel1.Text = "Aucun élève trouvé pour le champs saisi.";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                    }
                    else if (nbEleves > 10)
                    {
                        toolStripStatusLabel1.Text = nbEleves + " élèves trouvés. Veuillez, s'il vous plaît, affiner vos recherches !";
                        toolStripStatusLabel1.ForeColor = Color.Red;
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "";
                        //SearchResults.Rows.Add("dfs","dfg","fgdgfdgf");
                        panelTableau.Show();
                        //SearchEleve newRecherche = new SearchEleve(textSearchEleve.Text);
                        SearchEleve lesEleves = new SearchEleve(textSearchEleve.Text);
                        DataTable results = lesEleves.Results();

                        SearchResults.DataSource = results;

                        

                    }
                }
                
                //labelStudent.Text = nbEleves;
            }
        }

        // Au double clique d'une ligne du tableau, récupération des informations de l'élève, affichage du frm StudentDetails
        private void SearchResults_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EleveDetail EleveSelectionne = new EleveDetail(this.SearchResults.CurrentRow.Cells[0].Value.ToString(), this.SearchResults.CurrentRow.Cells[1].Value.ToString());
            string[] detailEleve = EleveSelectionne.informationsEleve();

            List<string> lesClassesEleve = new List<string>();
            InfosClasses recupInfosClasse = new InfosClasses();
            lesClassesEleve = recupInfosClasse.RecupClassesFirstSelect(Convert.ToInt32(detailEleve[4]));


            
            StudentDetails frmDetails = new StudentDetails(this.SearchResults.CurrentRow.Cells[0].Value.ToString(), this.SearchResults.CurrentRow.Cells[1].Value.ToString(), lesClassesEleve, detailEleve[0], detailEleve[1], detailEleve[2], detailEleve[3], detailEleve[4]);
            this.Hide();
            frmDetails.Show();
        }


        // Bouton Menu accès à l'ajout d'un nouvel élève
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
    }
}
