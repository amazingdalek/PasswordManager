using PasswordsManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordsManager.ViewModels
{
    public class MainViewModel : BaseNotifyPropertyChanged
    {
        
        private MainViewModel _selectedItem;
        public MainViewModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }

        }
        public int Id { get; set; }
        private Visibility _editionModeVisibility = Visibility.Collapsed;
        public Visibility EditionModeVisibility
        {
            get
            {
                return _editionModeVisibility;
            }
            set
            {
                _editionModeVisibility = value;
                OnPropertyChanged();
            }

        }

        private bool _isEditionModeEnabled = false;
        public bool isEditionModeEnabled
        {
            get
            {
                return _isEditionModeEnabled;
            }
            set
            {
                _isEditionModeEnabled = value;
                EditionModeVisibility = Visibility.Visible;
                OnPropertyChanged();
                OnPropertyChanged("EditionModeVisibility");
                
            }

        }
        

        private string _iconPath = "/icon/show.svg"; // a faire : image source avec bitmap image
        public string IconPath
        {
            get
            {
                return _iconPath;
            }
            set
            {
                _iconPath = value;
                OnPropertyChanged();
            }
        }
        private bool _isShown = false;
        public bool IsShown
        {
            get
            {
                return _isShown;
            }
            set
            {
                _isShown = value;
                OnPropertyChanged();
            }

        }
        private bool _isDecrypting = false;
        public bool IsDecrypting
        {
            get
            {
                return _isDecrypting;
            }
            set
            {
                _isDecrypting = value;
                OnPropertyChanged();
            }

        }
        private string _label { get; set; }
        public string Label
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
        public string Url
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

        private string _pass = "";
        public string Pass
        {
            get
            {
                if(IsShown == false && IsDecrypting == false)
                {
                    return Tool.ReplaceToCharPass(_pass);
                }
                return _pass;
            }
            set
            {
                _pass = value;
                OnPropertyChanged();
            }

        }
        private string _login { get; set; }
        public string Login
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
        private bool _firstInitList = true;
        private ObservableCollection<MainViewModel> _passwords = new ObservableCollection<MainViewModel>();
        public ObservableCollection<MainViewModel> passwords
        {
            get
            {
                if(_firstInitList && DataAccess.PasswordManagerDbContext.Current.Passwords.Count() != 0)
                {
                    foreach (var item in DataAccess.PasswordManagerDbContext.Current.Passwords)
                    {
                        _passwords.Add(
                            new MainViewModel()
                            {
                                Pass = item.Pass,
                                Login = item.Login,
                                Label = item.Label,
                                Url = item.Url,
                                Id = item.Id
                            });
                    }
                    _firstInitList = false;
                    return _passwords;
                }
                return _passwords;
               
            }
            set
            {
                _passwords = value;
            }
        }

    }
}
