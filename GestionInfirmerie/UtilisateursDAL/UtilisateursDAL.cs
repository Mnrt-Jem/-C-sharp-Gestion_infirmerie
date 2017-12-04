using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace UtilisateursDAL
{
    public class UtilisateursDAL
    {
        
    }

    /// <summary>
    /// Verification des informations de connexions saisies dans la BDD
    /// </summary>
    public class Users
    {
        public Users()
        {
            
        }

        public string verifuser(string name, string password)
        {
            SqlConnection maconnection = new SqlConnection("Data Source=(local);Initial Catalog=BDDInfirmerie;Integrated Security=True;Pooling=False");
            SqlDataAdapter sda = new SqlDataAdapter("SELECT username FROM users WHERE username = '" + name + "' and password = '" + password + "'", maconnection);
            DataTable dt = new DataTable();
            string result = sda.Fill(dt).ToString();
            
            return result;
        }
    }

    public class Student
    {
        
        public Student()
        {

        }

        public string nbRecherche(string search)
        {
            SqlDataReader myReader = null;
            string nbEleves = "";
            SqlConnection maconnection = new SqlConnection("Data Source=(local);Initial Catalog=BDDInfirmerie;Integrated Security=True;Pooling=False");
            SqlCommand maCommand = new SqlCommand("SELECT COUNT(*) as nblignes FROM student WHERE prenom + ' ' + nom LIKE '%" + search + "%'", maconnection);
            maconnection.Open();
            myReader = maCommand.ExecuteReader();
            while(myReader.Read())
            {
                nbEleves = myReader["nblignes"].ToString();
            }
            maconnection.Close();
            return nbEleves;
        }

        public DataTable rechercheEleves(string search)
        {
            List<Student> lesEleves = new List<Student>();
            SqlDataReader myReader = null;
            SqlConnection maconnection = new SqlConnection("Data Source=(local);Initial Catalog=BDDInfirmerie;Integrated Security=True;Pooling=False");
            SqlCommand maCommand = new SqlCommand("SELECT nom, prenom, libelle FROM student, classe WHERE student.id_classe = classe.id_classe AND prenom + ' ' + nom LIKE '%" + search + "%'", maconnection);
            maconnection.Open();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(maCommand);
            adapter.Fill(table);

            myReader = maCommand.ExecuteReader();
            
            maconnection.Close();
            return table;
        }

        public string[] infoSuppEleve(string nom, string prenom)
        {
            string[] eleve = new string[5];
            SqlDataReader myReader = null;
            SqlConnection maconnection = new SqlConnection("Data Source=(local);Initial Catalog=BDDInfirmerie;Integrated Security=True;Pooling=False");
            SqlCommand maCommand = new SqlCommand("SELECT id_eleve, datenaissance, sante, telparent, teleleve FROM student WHERE nom = '" + nom + "' AND prenom = '" + prenom + "'", maconnection);
            maconnection.Open();
            myReader = maCommand.ExecuteReader();
            while (myReader.Read())
            {
                eleve[4] = myReader["id_eleve"].ToString();
                eleve[0] = myReader["datenaissance"].ToString();
                eleve[1] = myReader["sante"].ToString();
                eleve[2] = myReader["telparent"].ToString();
                eleve[3] = myReader["teleleve"].ToString();
            }

            return eleve;

        }

        public bool updateStudent(string nom, string prenom, string classe, string date, string sante, string telParent, string telEleve, string ideleve)
        {
            int idClasse = 0;
            SqlDataReader myReader = null;
            SqlConnection maconnection = new SqlConnection("Data Source=(local);Initial Catalog=BDDInfirmerie;Integrated Security=True;Pooling=False");
            maconnection.Open();
            SqlCommand recupIdClasse = new SqlCommand("SELECT classe.id_classe FROM classe WHERE libelle = '" + classe + "'", maconnection);
            myReader = recupIdClasse.ExecuteReader();

            while (myReader.Read())
            {
                idClasse = Convert.ToInt32(myReader["id_classe"]);
            }
            myReader.Close();

            SqlCommand maCommand = new SqlCommand("UPDATE student SET nom = @Nom, prenom = @Prenom, id_classe = @classe, datenaissance = @date, sante = @sante, telparent = @telparent, teleleve = @teleleve WHERE id_eleve = '" + Convert.ToInt32(ideleve) + "'", maconnection);
            
            // Déclaration paramètres req SQL
            maCommand.Parameters.Add(new SqlParameter("@Nom", System.Data.SqlDbType.NVarChar, 50));
            maCommand.Parameters.Add(new SqlParameter("@Prenom", System.Data.SqlDbType.NVarChar, 50));
            maCommand.Parameters.Add(new SqlParameter("@classe", System.Data.SqlDbType.Int, 4));
            maCommand.Parameters.Add(new SqlParameter("@date", System.Data.SqlDbType.NVarChar, 50));
            maCommand.Parameters.Add(new SqlParameter("@sante", System.Data.SqlDbType.NVarChar, 50));
            maCommand.Parameters.Add(new SqlParameter("@telparent", System.Data.SqlDbType.NVarChar, 50));
            maCommand.Parameters.Add(new SqlParameter("@teleleve", System.Data.SqlDbType.NVarChar, 50));

            // Initialisation des paramètres déclarés
            maCommand.Parameters["@Nom"].Value = nom;
            maCommand.Parameters["@Prenom"].Value = prenom;
            maCommand.Parameters["@classe"].Value = idClasse;
            maCommand.Parameters["@date"].Value = date;
            maCommand.Parameters["@sante"].Value = sante;
            maCommand.Parameters["@telparent"].Value = telParent;
            maCommand.Parameters["@teleleve"].Value = telEleve;

            try
            {
                
                maCommand.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class Classes
    {
        public Classes()
        {

        }

        public List<string> RecupClasses()
        {
            List<string> lesClasses = new List<string>();
            SqlConnection maconnection = new SqlConnection("Data Source=(local);Initial Catalog=BDDInfirmerie;Integrated Security=True;Pooling=False");
            SqlCommand maCommand = new SqlCommand("SELECT libelle, id_classe FROM classe", maconnection);
            maconnection.Open();
            SqlDataReader myReader = null;
            myReader = maCommand.ExecuteReader();
            while (myReader.Read())
            {
                lesClasses.Add(myReader["libelle"].ToString());
            }

            return lesClasses;



        }

        public List<string> RecupClassesFirstSelect(int ideleve)
        {
            string classeSelectionnee = "";
            List<string> lesClasses = new List<string>();
            SqlConnection maconnection = new SqlConnection("Data Source=(local);Initial Catalog=BDDInfirmerie;Integrated Security=True;Pooling=False");
            SqlCommand maCommand = new SqlCommand("SELECT libelle FROM classe, student WHERE student.id_classe = classe.id_classe AND id_eleve = '"+ideleve+"'", maconnection);
            maconnection.Open();
            SqlDataReader myReader = null;
            myReader = maCommand.ExecuteReader();
            while (myReader.Read())
            {
                lesClasses.Add(myReader["libelle"].ToString());
                classeSelectionnee = myReader["libelle"].ToString();
            }
            myReader.Close();

            SqlCommand nouvCommand = new SqlCommand("SELECT libelle, id_classe FROM classe WHERE libelle != '" + classeSelectionnee + "' ", maconnection);
            myReader = nouvCommand.ExecuteReader();
            while (myReader.Read())
            {
                lesClasses.Add(myReader["libelle"].ToString());
            }


            return lesClasses;
        }
    }

}
