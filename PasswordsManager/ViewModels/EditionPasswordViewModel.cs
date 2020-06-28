using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordsManager.ViewModels
{
    public class EditionPasswordViewModel : BaseNotifyPropertyChanged
    {
        public static MainViewModel Viewmodel;
        public EditionPasswordViewModel(MainViewModel viewmodel)
        {
            Viewmodel = viewmodel;
            if(viewmodel != null)
            {
                _label = Viewmodel.Label;
                _Url = Viewmodel.Url;
                _pass = Viewmodel.Pass;
                _login = Viewmodel.Login;
                Id = Viewmodel.Id;
            }
        }
        public int Id { get; set; }
        private string _label { get; set; }
        public string EditionLabel
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                OnPropertyChanged();
            }

        }

        private string _Url { get; set; }
        public string EditionUrl
        {
            get
            {
                return _Url;
            }
            set
            {
                _Url = value;
                OnPropertyChanged();
            }

        }

        private string _pass;
        public string EditionPass
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = value;
                OnPropertyChanged();
            }

        }
        private string _login { get; set; }
        public string EditionLogin
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                OnPropertyChanged();
            }

        }
    }
}
