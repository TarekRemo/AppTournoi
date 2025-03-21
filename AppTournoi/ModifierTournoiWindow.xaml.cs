using BddtournoiContext;
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
using System.Collections.ObjectModel;
using DllTournois;

namespace AppTournoi
{
    /// <summary>
    /// Logique d'interaction pour ModifierTournoiWindow.xaml
    /// </summary>
    public partial class ModifierTournoiWindow : Window
    {
        public Tournoi Tournoi { get; set; }
        public ObservableCollection<Sport> Sports { get; set; }

        public ModifierTournoiWindow(Tournoi tournoi, ObservableCollection<Sport> sports)
        {
            InitializeComponent();
            this.Tournoi = tournoi;
            this.Sports = sports;
            this.ComboBoxSports.ItemsSource = sports;
            this.TextBoxIntitule.Text = Tournoi.Intitule;
            this.DatePickerTournoi.SelectedDate = Tournoi.DateTournoi;
            this.ComboBoxSports.SelectedItem = Tournoi.Sport1; 
            DataContext = this; // Définit le DataContext pour la liaison
        }

        // Gestion des événements de boutons (si nécessaire)
        private void Button_Enregistrer_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TextBoxIntitule.Text) || DatePickerTournoi.SelectedDate == null || ComboBoxSports.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur");
                return;
            }
            Tournoi.Intitule = TextBoxIntitule.Text;
            Tournoi.DateTournoi = (DateTime) DatePickerTournoi.SelectedDate;
            Tournoi.Sport = ((Sport) ComboBoxSports.SelectedItem).IdSport; 
            Bddtournoi.modifierTournoi(Tournoi);
            new GestionTournoiWindow().Show(); 
            this.Close(); 
        }

        private void Button_Annuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
