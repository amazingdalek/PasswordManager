using PasswordsManager.Models;
using PasswordsManager.Utils;
using PasswordsManager.ViewModels;
using PasswordsManager.Views.UC;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordsManager.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
            ViewModel.IsShown = true;
            DataContext = ViewModel;
            TbMDP.MouseDoubleClick += Tbgenrated_MouseDoubleClick;
            btnGenerate.Click += BtnGenerate_Click;
            btnsauvegarder.Click += Btnsauvegarder_Click;
            //dpEditionMode.Children.Add(new EditionUserControl(ViewModel.SelectedItem));
        }
            

        private void Btnsauvegarder_Click(object sender, RoutedEventArgs e)
        {
            Password password = new Password()
            {
                Pass = Crypto.Encrypt(ViewModel.Pass),
                Label = ViewModel.Label,
                Login = ViewModel.Login,
                Url = ViewModel.Url,
            };
            DataAccess.PasswordManagerDbContext.Current.Passwords.Add(password);
            try
            {
                if (DataAccess.PasswordManagerDbContext.Current.SaveChanges() != 0)
                {
                    MessageBox.Show("Votre mot de passe a été enregistré", "Bravo !", MessageBoxButton.OK);
                    ViewModel.passwords.Add(ViewModel);
                    ViewModel = new MainViewModel();
                    DataContext = ViewModel;
                }
                else
                {
                    MessageBox.Show("Erreur lors de la sauvegarde de votre mot de passe, veuillez réessayer...", "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException ee)
            {
                MessageBox.Show("Veuillez remplir tous les champs..", "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void BtnGenerate_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Pass = Tool.RandomPassword(10);
        }

        private void Tbgenrated_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(TbMDP.Text);
            
        }

        private void btnShowPass_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainViewModel viewmodel = button.DataContext as MainViewModel;
            viewmodel.IsDecrypting = true;
            if (viewmodel.IsShown)
            {
                string str = Crypto.Encrypt(viewmodel.Pass);
                viewmodel.IsDecrypting = false;
                viewmodel.IsShown = false;
                viewmodel.Pass = str;
                viewmodel.IsShown = false;
            }
            else
            {
                viewmodel.Pass = Crypto.Decrypt(viewmodel.Pass);
                viewmodel.IsShown = true;
            }
            viewmodel.IsDecrypting = false;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.isEditionModeEnabled = true;
            dpEditionMode.Children.Clear();
            dpEditionMode.Children.Add( new EditionUserControl( ViewModel.SelectedItem));
        }
    }
}
