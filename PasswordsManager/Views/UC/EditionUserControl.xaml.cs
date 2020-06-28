using PasswordsManager.Models;
using PasswordsManager.Utils;
using PasswordsManager.ViewModels;
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

namespace PasswordsManager.Views.UC
{
    /// <summary>
    /// Logique d'interaction pour EditionUserControl.xaml
    /// </summary>
    public partial class EditionUserControl : UserControl
    {
        private MainViewModel _viewmodelMain;
        private EditionPasswordViewModel _viewmodel;
        public EditionUserControl(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _viewmodelMain = mainViewModel;
            _viewmodel = new EditionPasswordViewModel(_viewmodelMain);
            DataContext = _viewmodel;
        }

        private void btnsauvegarderEdited_Click(object sender, RoutedEventArgs e)
        {
            Password pwd = DataAccess.PasswordManagerDbContext.Current.Passwords.First(x => x.Id == _viewmodel.Id);
            pwd.Label = _viewmodel.EditionLabel;
            pwd.Login = _viewmodel.EditionLogin;
            pwd.Pass = Crypto.Encrypt(_viewmodel.EditionPass);
            pwd.Url = _viewmodel.EditionUrl;
            DataAccess.PasswordManagerDbContext.Current.Passwords.Update(pwd);
            try
            {
                ModalSuperAdmin modal = new ModalSuperAdmin();
                if(modal.ShowDialog() == true)
                {
                    if (DataAccess.PasswordManagerDbContext.Current.SaveChanges() != 0)
                    {
                        MessageBox.Show("Votre mot de passe a été mdoifié", "Bravo !", MessageBoxButton.OK);
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la modification de votre mot de passe, veuillez réessayer...", "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    MainWindow.ViewModel.isEditionModeEnabled = false;
                    MainWindow.ViewModel.passwords.Remove(_viewmodelMain);
                    MainWindow.ViewModel.passwords.Add(new MainViewModel
                    {
                        Id = _viewmodel.Id,
                        Pass = _viewmodel.EditionPass,
                        Url = _viewmodel.EditionUrl,
                        Login = _viewmodel.EditionLogin,
                        Label = _viewmodel.EditionLabel
                    });
                    MainWindow.ViewModel.passwords.OrderByDescending(x => x.Id);
                }


            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ee)
            {
                MessageBox.Show("Veuillez remplir tous les champs..", "Attention !", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void btnGenerateEdited_Click(object sender, RoutedEventArgs e)
        {
            _viewmodel.EditionPass = Tool.RandomPassword(10);
        }
    }
}
