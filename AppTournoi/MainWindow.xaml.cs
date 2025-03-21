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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BddtournoiContext;
using System.Data.SqlClient;
using System.ComponentModel;

namespace AppTournoi
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool connected;
        public bool Connected
        {
            get { return connected; }
            set
            {
                if (connected != value)
                {
                    connected = value;
                    if (connected)
                    {
                        this.MenuItemGestionParticipants.IsEnabled = true;
                        this.MenuItemGestionSports.IsEnabled = true;
                        this.MenuItemGestionTournois.IsEnabled = true; 
                    }
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.ListBoxSports.SelectionChanged += OnSportItemClick;
            this.ListBoxTournois.SelectionChanged += OnTournoiItemClick;
            connected = false; 
        }


        private void AfficheSports()
        {
            try
            {
                List<Sport> sports = Bddtournoi.GetSports();
                foreach(Sport sport in sports)
                {
                    this.ListBoxSports.Items.Add(sport); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void AfficheTournois(List<Tournoi> tournois)
        {
            this.ListBoxTournois.Items.Clear();
            foreach (Tournoi tournoi in tournois)
            {
                this.ListBoxTournois.Items.Add(tournoi);
            }
        }

        private void AfficheParticipants(List<Participant> participants)
        {
            this.ListBoxParticipants.Items.Clear();
            foreach(Participant participant in participants)
            {
                this.ListBoxParticipants.Items.Add(participant);
            }
        }


        private void OnSportItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Tournoi> tournois = Bddtournoi.GetTournois(((Sport)this.ListBoxSports.SelectedItem).IdSport);
                AfficheTournois(tournois);
                this.ListBoxParticipants.Items.Clear(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void OnTournoiItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Participant> participants = Bddtournoi.GetParticipants(((Tournoi)this.ListBoxTournois.SelectedItem).IdTournoi);
                AfficheParticipants(participants);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        /// <summary>
        /// handler pour le MenuItem "Connexion BDD"
        /// </summary>
        private void ConBddOnClick(Object sender, RoutedEventArgs e)
        {
            try
            {
                Bddtournoi.Connect(Properties.Settings.Default.DB_host, Properties.Settings.Default.DB_user, Properties.Settings.Default.DB_password,
                                    Properties.Settings.Default.DB_dataBase, Properties.Settings.Default.DB_port);
                AfficheSports();
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 18456: // Erreur d'authentification (mauvais identifiants)
                        MessageBox.Show("Erreur : Identifiants de connexion incorrects.");
                        break;

                    case 4060: // Base de données inconnue
                        MessageBox.Show("Erreur : La base de données spécifiée n'existe pas.");
                        break;

                    case 53:   // Serveur injoignable
                    case 2:    // Impossible d'établir une connexion
                        MessageBox.Show("Erreur : Impossible de se connecter au serveur SQL.");
                        break;

                    default: // Autres erreurs SQL
                        MessageBox.Show($"Erreur SQL : {ex.Message} (Code : {ex.Number})");
                        break;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Erreur de configuration LINQ : {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Autre erreur : {ex.Message}");
            }
        }

        private void MenuItemParamBdd_Click(object sender, RoutedEventArgs e)
        {
            new DBWindow().Show(); 
        }

        private void MenuItemListTsParticipants_Click(object sender, RoutedEventArgs e)
        {
            new AllParticipantsWindow().Show();
        }

        private void MenuItemGestionnaire_Click(object sender, RoutedEventArgs e)
        {
            new ConnectionWindow(this).Show(); 
        }

        private void MenuItemGestionTournois_Click(object sender, RoutedEventArgs e)
        {
            new GestionTournoiWindow().Show(); 
        }

        private void MenuItemGestionSports_Click(object sender, RoutedEventArgs e)
        {
            new GestionSportsWindow().Show(); 
        }

        private void MenuItemGestionParticipants_Click(object sender, RoutedEventArgs e)
        {
            new GestionParticipantsWindow().Show(); 
        }
    }
}
