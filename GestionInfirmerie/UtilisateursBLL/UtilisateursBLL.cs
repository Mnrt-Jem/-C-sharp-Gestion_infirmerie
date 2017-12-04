using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using UtilisateursDAL;
using System.Text.RegularExpressions;

namespace UtilisateursBLL
{
    public class UtilisateursBLL
    {

    }

    public class VerifSaisies
    {
        

        public VerifSaisies()
        {
            
        }

        // Return TRUE si la chaîne passée en paramètre contient un chiffre
        public bool verifInteger(string str)
        {
            System.Text.RegularExpressions.Regex myRegex = new Regex("[0-9]");
            bool verifSaisie = myRegex.IsMatch(str);

            return verifSaisie;
        }

        public string strToUpper(string str)
        {
            string upperString = str.ToUpper();
            return upperString;
        }

        public string strToFirstUpper(string str)
        {
            string upperFirstStr = str.First().ToString().ToUpper();
            string sansFirstStr = str.Substring(1);

            return upperFirstStr + sansFirstStr;
        }

        // Retourne vrai si le numéro saisi est correct, faux sinon.
        public bool verifTel(string str)
        {
            System.Text.RegularExpressions.Regex myRegex = new Regex("^0[1-8][0-9]{8}$"); // <--- Commence par un 0, puis un chiffre de 1 à 8 puis 8 fois des chiffres de 0 à 9 
            bool verifSaisie = myRegex.IsMatch(str);

            return verifSaisie;
        }
    }

    /// <summary>
    /// Classe d'accès à la BDD et de vérification des identifiants et mot de passe fournis pour la connexion UTILISATEUR (GUI CONNEXION)
    /// </summary>
    public class Connection
    {
        private string username;
        private string password;
        
        public Connection (string username, string password) {
            this.username = username;
            this.password = password;
        }

        public bool verifConnection()
        {
            Users userConnection = new Users();
            string verif = userConnection.verifuser(this.username, this.password);

            if (verif == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }

    public class SearchEleve
    {
        private string search;

        public SearchEleve(string search)
        {
            this.search = search;
        }

        public string NbSearchResult()
        {
            Student recherche = new Student();
            string nbEleves = recherche.nbRecherche(search);
            return nbEleves;
        }

        public DataTable Results()
        {
            Student recherche = new Student();
            DataTable lesEleves = new DataTable();
            lesEleves = recherche.rechercheEleves(search);

            return lesEleves;
        }
    }

    public class EleveDetail
    {
        private string nom;
        private string prenom;
        private string classe;
        private string date;
        private string sante;
        private string telParent;
        private string telEleve;
        private string ideleve;

        public EleveDetail(string nom, string prenom)
        {
            this.nom = nom;
            this.prenom = prenom;
        }

        public EleveDetail(string nom, string prenom, string classe, string date, string sante, string telParent, string telEleve, string ideleve)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.classe = classe;
            this.date = date;
            this.sante = sante;
            this.telParent = telParent;
            this.telEleve = telEleve;
            this.ideleve = ideleve;
        }

        public string[] informationsEleve()
        {
            Student eleveSelectionne = new Student();
            string[] detailEleve = eleveSelectionne.infoSuppEleve(this.nom, this.prenom);

            return detailEleve;
            
        }

        public bool ModificationEleve()
        {
            Student unEleve = new Student();
            bool result = unEleve.updateStudent(this.nom, this.prenom, this.classe, this.date, this.sante, this.telParent, this.telEleve, this.ideleve);
            
            return result;
        }
    }

    public class InfosClasses
    {
        public InfosClasses()
        {

        }

        // Récupération de la totalité des classes
        public List<string> RecupInfosClasses()
        {
            List<string> lesClasses = new List<string>();
            Classes informationClasse = new Classes();
            lesClasses = informationClasse.RecupClasses();
            return lesClasses;
        }

        // Récupération de la totalité des classes avec pour premier élément de la liste celui actuellement sélectionné pour l'élève
        public List<string> RecupClassesFirstSelect(int ideleve)
        {
            List<string> lesClasses = new List<string>();
            Classes informationClasse = new Classes();
            lesClasses = informationClasse.RecupClassesFirstSelect(ideleve);
            return lesClasses;
        }
    }
}
