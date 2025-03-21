using BddtournoiContext;
using DllTournois;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Logique d'interaction pour GestionTournoiWindow.xaml
    /// </summary>
    public partial class GestionTournoiWindow : Window
    {
        private ObservableCollection<Tournoi> tournois = new ObservableCollection<Tournoi>();
        private ObservableCollection<Sport> sports = new ObservableCollection<Sport>();
        public GestionTournoiWindow()
        {
            InitializeComponent();
            GetTournois();
            GetSports(); 
            this.ListBoxTournois.ItemsSource = tournois;
            this.ComboBoxSports.ItemsSource = sports;
        }


        private void GetTournois()
        {
            tournois.Clear(); 
            List<Tournoi> list = Bddtournoi.GetTournois(); 
            foreach(Tournoi tournoi in list)
            {
                tournois.Add(tournoi); 
            }
        }

        private void GetSports()
        {
            sports.Clear();
            List<Sport> list = Bddtournoi.GetSports();
            foreach (Sport sport in list)
            {
                sports.Add(sport);
            }
        }

        private void Button_Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxIntitule.Text) || DatePickerTournoi.SelectedDate == null || ComboBoxSports.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur");
                return;
            }

            if(DatePickerTournoi.SelectedDate.Value < DateTime.Today)
            {
                MessageBox.Show("Vous ne pouvez pas ajouter un tournoi dont la date est inférieure à la date d'aujourd'hui", "Erreur");
                return;
            }

            try
            {
                Bddtournoi.ajouterTournoi(TextBoxIntitule.Text, DatePickerTournoi.SelectedDate.Value, (Sport) ComboBoxSports.SelectedItem);
                MessageBox.Show("Tournoi enregistré avec succès.", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                GetTournois(); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Une erreur est survenue");
            }
        }

        private void Button_Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void ModifierMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
            if (ListBoxTournois.SelectedItem != null)
            {
                var selectedTournoi = (Tournoi)ListBoxTournois.SelectedItem;
                new ModifierTournoiWindow(selectedTournoi, sports).Show();
                this.Close(); 
            }
        }

        private void SupprimerMenuItem_Click(object sender, RoutedEventArgs e)
        {
           
            if (ListBoxTournois.SelectedItem != null)
            {
                var selectedTournoi = (Tournoi)ListBoxTournois.SelectedItem;
                Bddtournoi.supprimerTournoi(selectedTournoi);
                tournois.Remove(selectedTournoi); 
            }
        }
    }
}
