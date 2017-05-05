using Models;
using System.ComponentModel;

namespace Notification_PI.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged(new PropertyChangedEventArgs("User"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
