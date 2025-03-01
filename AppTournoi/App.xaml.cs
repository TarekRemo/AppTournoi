using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AppTournoi.Properties;
using DllTournois;

namespace AppTournoi
{
    public partial class App : Application
    {
        public App()
        {
            Startup += ConnectDB; 
        }

        /// <summary>
        ///Essaye de se connecter la BDD en utilisant les paramètres enregistrés. 
        ///si la connexion échoue, une boite de dialogue s'affiche puis la fenêtre de connexion s'ouvre.
        /// </summary>
        private void ConnectDB(Object sender, EventArgs e)
        {
            //On récupère les paramètres enregistrés
            string user = Settings.Default.DB_user; string password = Settings.Default.DB_password;
            string dataBase = Settings.Default.DB_dataBase; string host = Settings.Default.DB_host;
            string port = Settings.Default.DB_port;

            //On essaye de se connecter
            try
            {
                Bddtournoi.Connect(host, user, password, dataBase, port);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erreur de connexion à la BDD : " + ex.Message);
                //Ouvre la fenêtre de connexion
                var DBwindow = new DBWindow();
                DBwindow.Height = 300;
                DBwindow.ShowDialog();
            }
        }
    }
}
