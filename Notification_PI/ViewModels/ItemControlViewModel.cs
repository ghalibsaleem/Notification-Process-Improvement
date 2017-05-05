using Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace Notification_PI.ViewModels
{
    public class ItemControlViewModel: INotifyPropertyChanged
    {

        public ItemControlViewModel()
        {

        }

        public ItemControlViewModel(SIT2_Item item)
        {
            ItemProperties =new ObservableCollection<PropertyInfo>( item.GetType().GetProperties());
            SitObject = item;
            Initial = false;
            Final = false;
        }


        public ItemControlViewModel(JSON_SIT_Model item)
        {
            ItemProperties = new ObservableCollection<PropertyInfo>(item.Item.GetType().GetProperties());
            SitObject = item.Item;
            Initial = item.Initial;
            Final = item.Final;
        }
        private ObservableCollection<PropertyInfo> _itemProperties;

        public ObservableCollection<PropertyInfo> ItemProperties
        {
            get { return _itemProperties; }
            set { _itemProperties = value; }
        }

        private SIT2_Item _sit_Object;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        public SIT2_Item SitObject
        {
            get { return _sit_Object; }
            set { _sit_Object = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SitObject"));
            }
        }

        public bool Initial { get; set; }

        public bool Final { get; set; }

    }
}
