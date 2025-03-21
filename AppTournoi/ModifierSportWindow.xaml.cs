using BddtournoiContext;
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

namespace AppTournoi
{
    /// <summary>
    /// Logique d'interaction pour ModifierSportWindow.xaml
    /// </summary>
    public partial class ModifierSportWindow : Window
    {
        private Sport sport; 
        public ModifierSportWindow(Sport sport)
        {
            InitializeComponent();
            this.sport = sport;
            this.TextBoxIntitule.Text = sport.Intitule; 
        }

        private void ButtonAjouter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxIntitule.Text))
            {
                MessageBox.Show("Veuillez saisir un nom pour le sport.", "Erreur");
                return;
            }

            sport.Intitule = this.TextBoxIntitule.Text;
            Bddtournoi.modifierSport(sport); 
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
