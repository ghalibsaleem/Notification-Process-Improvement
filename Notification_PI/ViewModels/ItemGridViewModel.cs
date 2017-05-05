using MaterialDesignThemes.Wpf;
using Models;
using Notification_PI.Commands;
using Notification_PI.CustomControl;
using Notification_PI.ModelsHelper;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notification_PI.ViewModels
{
    public class ItemGridViewModel : INotifyPropertyChanged
    {
        public ItemGridViewModel()
        {
            _sit_ItemCollection = new ObservableCollection<SIT2_Item>();
        }



        public ICommand RunDialogCommand => new CommandHandler(ExecuteRunDialog);

        private ObservableCollection<SIT2_Item> _sit_ItemCollection;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public ObservableCollection<SIT2_Item> Sit_ItemsCollection
        {
            get { return _sit_ItemCollection; }
            set
            {
                _sit_ItemCollection = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sit_ItemsCollection"));
            }
        }

        public async Task FillCollection(User user)
        {
            ItemHelper helper = new ItemHelper();
            
            ObservableCollection<SIT2_Item> temp = await helper.GetItems(user);
            Sit_ItemsCollection = new ObservableCollection<SIT2_Item>(temp);
            
        }


        private async void ExecuteRunDialog(object o)
        {
            var view = new ItemControl()
            {
                DataContext = new ItemControlViewModel(o as SIT2_Item)
            };
            
            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog");

        }
    }
}
