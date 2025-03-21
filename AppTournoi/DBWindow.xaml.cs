using DllTournois;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DllTournois; 

namespace AppTournoi
{
    /// <summary>
    /// Logique d'interaction pour DBWindow.xaml
    /// </summary>
    public partial class DBWindow : Window
    {
        /// <summary>
        /// Constructeur de la fenêtre de connexion à la BDD.
        /// </summary>
        public DBWindow()
        {
            InitializeComponent();
            //on remplit les champs de saisis par les valeurs enregistrées
            this.TextBoxAdresseIP.Text = Properties.Settings.Default.DB_host;
            this.TextBoxPort.Text = Properties.Settings.Default.DB_port;
            this.TextBoxNomUtilisateur.Text = Properties.Settings.Default.DB_user;
            this.PasswordBoxMDP.Password = Properties.Settings.Default.DB_password;
        }

        /// <summary>
        /// Handler du bouton "Enregistrer". 
        /// Essaye de se connecter à la BDD avec les valeurs saisies. 
        /// Si la connexion est réussie, les valeurs sont enregistrées et la fenêtre se ferme.
        /// Si la connexion échoue, une boite de dialogue s'affiche.
        /// </summary>
        private void ButtonEnregistrerOnClick(Object sender, RoutedEventArgs e)
        {
            //Si la connexion réussie, on enregistre les valeurs et on ouvre la fenêtre principale
            SaveDBSettings();
            new MainWindow();
            this.Close();
        }

        private void ButtonAnnulerOnClick(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// récupère les valeurs saisies et les enregistre dans les paramètres de l'application.
        /// </summary>
        private void SaveDBSettings()
        {
            //On enregistre les valeurs
            Properties.Settings.Default.DB_host = this.TextBoxAdresseIP.Text;
            Properties.Settings.Default.DB_port = this.TextBoxPort.Text;
            Properties.Settings.Default.DB_user = this.TextBoxNomUtilisateur.Text;
            Properties.Settings.Default.DB_password = this.PasswordBoxMDP.Password;
            Properties.Settings.Default.Save();
        }
    }
}
