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
using BddtournoiContext;
using System.Collections.ObjectModel;

namespace AppTournoi
{
    /// <summary>
    /// Logique d'interaction pour AllParticipantsWindow.xaml
    /// </summary>
    public partial class AllParticipantsWindow : Window
    {

        private ObservableCollection<Participant> participants = new ObservableCollection<Participant>();

        public AllParticipantsWindow()
        {
            InitializeComponent();
            try
            {
                List<Participant> list = this.GetParticipants(this.TextBoxNomParticipant.Text);
                foreach (Participant participant in list)
                {
                    participants.Add(participant);
                }
                this.DataGridParticipants.ItemsSource = participants;
            }
            catch
            {
                MessageBox.Show("Vous devez vous connecter à la BDD", "Aucune Connexion à la BDD");
            }

        }

        private List<Participant> GetParticipants(string nom)
        {
            if(nom.Length == 0)
                return Bddtournoi.GetParticipants();
            else
                return Bddtournoi.GetParticipants(nom);
        }

        private void TextBoxNomParticipant_TextChanged(object sender, TextChangedEventArgs e)
        {
            participants.Clear();
            try
            {
                List<Participant> list = this.GetParticipants(this.TextBoxNomParticipant.Text);
                foreach (Participant participant in list)
                {
                    participants.Add(participant);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }

        }
    }
}
