using AppTournoi.Properties;
using DllTournois;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace AppTournoi
{
    public partial class ConnectionWindow : Window
    {
        private MainWindow mainWindow; 
        public ConnectionWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            if (Settings.Default.Login.Length > 0)
                this.TextBoxLogin.Text = Settings.Default.Login; 
        }

        private void connect(string login)
        {
            mainWindow.Connected = true;
            Properties.Settings.Default.Login = login;
            Properties.Settings.Default.Save();
            this.Close();
        }
        private void ButtonConnexion_Click(object sender, RoutedEventArgs e)
        {
            if (this.TextBoxLogin.Text.Length == 0 || this.PasswordBoxMDP.Password.Length == 0)
                MessageBox.Show("Vous devez saisir le login et le mot de passe.", "saisies manquants");
            else
            {
                try
                {
                    bool ComptExist = Bddtournoi.AccountExists(this.TextBoxLogin.Text);
                    if (ComptExist)
                    {
                        bool mdpCorrect = Bddtournoi.CheckPassword(this.TextBoxLogin.Text, this.PasswordBoxMDP.Password);
                        if (mdpCorrect)
                            connect(this.TextBoxLogin.Text); 
                        else
                            MessageBox.Show("Le mot de passe saisi est incorrect.", "MDP incorrect");
                    }
                    else
                    {
                        MessageBox.Show("Aucun compte n'existe avec le login que vous avez saisi", "Compte non trouvé"); 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Vous n'êtes pas connecté à la base de données.", "une erreur est survenue");
                }
            }
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }
    }
}
