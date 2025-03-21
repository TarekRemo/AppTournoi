using BddtournoiContext;
using DllTournois;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Logique d'interaction pour GestionParticipantsWindow.xaml
    /// </summary>
    public partial class GestionParticipantsWindow : Window
    {
        private ObservableCollection<Participant> participants = new ObservableCollection<Participant>();
        private ObservableCollection<Tournoi> tournois = new ObservableCollection<Tournoi>();
        byte[] photoEnBytes = null; 
        public GestionParticipantsWindow()
        {
            InitializeComponent();
            this.ListBoxParticipants.ItemsSource = participants;
            this.ListBoxTournois.ItemsSource = tournois;
            GetParticipants();
            GetTournois();
        }



        private void GetParticipants()
        {
            participants.Clear();
            List<Participant> list = Bddtournoi.GetParticipants();
            foreach (Participant p in list)
            {
                participants.Add(p);
            }
        }

        private void GetTournois()
        {
            tournois.Clear();
            List<Tournoi> list = Bddtournoi.GetTournois();
            foreach (Tournoi tournoi in list)
            {
                tournois.Add(tournoi);
            }
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void ButtonChoisirPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choisir une photo",
                Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                photoEnBytes = ConvertirImageEnBytes(openFileDialog.FileName);

                // Afficher l’image dans l’UI
                ImageUtilisateur.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private byte[] ConvertirImageEnBytes(string cheminImage)
        {
            try
            {
                return File.ReadAllBytes(cheminImage); // Charge l’image en tant que tableau de bytes
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la conversion de l'image : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private void ButtonEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxNom.Text) || DatePickerNaissance.SelectedDate == null || ListBoxTournois.SelectedItem == null ||
                string.IsNullOrWhiteSpace(TextBoxPrenom.Text) || ComboBoxSex.SelectedItem == null)
            {
                MessageBox.Show("Veuillez remplir tous les champs.", "Erreur");
                return;
            }

            if(DatePickerNaissance.SelectedDate.Value > DateTime.Today)
            {
                MessageBox.Show("Vous ne pouvez pas saisir une date de naissance supérieure à aujourd'hui", "Erreur");
                return;
            }

            if (DateTime.Today.Year - DatePickerNaissance.SelectedDate.Value.Year <= 8)
            {
                MessageBox.Show("Le participant doit avoir au moins 8 ans", "Erreur");
                return;
            }

            try
            {
                Bddtournoi.ajouterParticipants(TextBoxNom.Text, TextBoxPrenom.Text, ((TextBlock) (ComboBoxSex.SelectedItem)).Text, DatePickerNaissance.SelectedDate.Value,
                                                    (Tournoi)ListBoxTournois.SelectedItem, photoEnBytes);
                MessageBox.Show("Le participant a été ajouté avec succès ! ", "Participant ajouté");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "erreur"); 
            }


            
        }
    }
}
