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
    /// Logique d'interaction pour GestionSportsWindow.xaml
    /// </summary>
    public partial class GestionSportsWindow : Window
    {
        private ObservableCollection<Sport> sports = new ObservableCollection<Sport>();
        public GestionSportsWindow()
        {
            InitializeComponent();
            this.ListBoxSports.ItemsSource = sports; 
            GetSports(); 
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

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxIntitule.Text))
            {
                MessageBox.Show("Veuillez saisir un nom pour le sport.", "Erreur");
                return;
            }

            try
            {
                Bddtournoi.ajouterSport(TextBoxIntitule.Text);
                MessageBox.Show("Le sport a été ajouté avec succés", "Sport ajouté !!");
                GetSports(); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur"); 
            }
        }

        private void ModifierMenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (ListBoxSports.SelectedItem != null)
            {
                var sport = (Sport)ListBoxSports.SelectedItem;
                new ModifierSportWindow(sport).Show();
                this.Close();
            }
        }

        private void SupprimerMenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (ListBoxSports.SelectedItem != null)
            {
                var sport = (Sport)ListBoxSports.SelectedItem;
                Bddtournoi.supprimerSport(sport);
                sports.Remove(sport);
            }
        }
    }
}
