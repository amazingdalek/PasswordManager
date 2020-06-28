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

namespace PasswordsManager.Views.UC
{
    /// <summary>
    /// Logique d'interaction pour ModalSuperAdmin.xaml
    /// </summary>
    public partial class ModalSuperAdmin : Window
    {
        public ModalSuperAdmin()
        {
            InitializeComponent();

        }
        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if(DataAccess.PasswordManagerDbContext.Current.User.Any(x=> x.SuperPassword == txtAnswer.Text))
            {

                this.DialogResult = true;
            }
            else
            {

                this.DialogResult = true;
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }
    }
}
