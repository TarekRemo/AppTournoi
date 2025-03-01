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

namespace AppTournoi
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //On ajoute les handlers pour les boutons du menu principal
            MenuHandlers();
        }

        private void MenuHandlers()
        {
            this.MenuItemConBdd.Click += ConBddOnClick; //MenuItem Connexion BDD
        }

        /// <summary>
        /// handler pour le MenuItem "Connexion BDD"
        /// </summary>
        private void ConBddOnClick(Object sender, RoutedEventArgs e)
        {
            new DBWindow().Show();
        }
    }
}
