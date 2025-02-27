
using BddtournoiContext; 

namespace DllTournois
{
    public class Bddtournoi
    {
        //paramètres de connexion à la BDD
        private static string host = "localhost";
        private static string user = "adminTournois";
        private static string password = "Password1234@";
        private static string dataBase = "bddtournois";
        private static string port = "3306"; 

        //objet BDD du modèle linQ
        private BddtournoiDataContext DB;

        //instance du singleton
        private static Bddtournoi instance; 

        private Bddtournoi()
        {
            DB = new BddtournoiDataContext("User Id=" + user + ";Password=" + password + ";Host=" + host +
                                                ";Port=" + port + ";Database=" + dataBase + ";Persist Security Info=True");
        }

        public Bddtournoi getInstance()
        {
            if (instance == null)
            {
                instance = new Bddtournoi(); 
            }
            return instance;
        }


        public List<Tournoi> GetTournois()
        {
            return DB.Tournois.ToList();
        }
    }
}
