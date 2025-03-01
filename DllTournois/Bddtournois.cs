using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BddtournoiContext; 

namespace DllTournois
{   
    /// <summary>
    /// Classe permettant d'exploiter la  BDD Bddtournoi.
    /// </summary>
    public class Bddtournoi
    {

        //Objet de la classe de la BDD générée par LinQ
        private static BddtournoiDataContext DB;

        /// <summary>
        /// essaye de se connecter et d'ouvrir la connexion à la BDD en utilisant les valeurs passés en paramètres.
        /// </summary>
        public static void Connect(string host, string user, string password, string dataBase, string port)
        {
            DB = new BddtournoiDataContext("User Id=" + user + ";Password=" + password + ";Host=" + host +
                                                ";Port=" + port + ";Database=" + dataBase + ";Persist Security Info=True");
            DB.Connection.Open();
        }

        /// <summary>
        /// permet de tester si la connexion à la BDD est ouverte
        /// </summary>
        public static bool TestConnection()
        {
            return DB.Connection.State == System.Data.ConnectionState.Open; 
        } 
       
    }
}
