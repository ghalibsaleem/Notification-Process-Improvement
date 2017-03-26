using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notification_PI.ViewModels
{
    public class ItemControlViewModel
    {

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

        public SIT2_Item SitObject
        {
            get { return _sit_Object; }
            set { _sit_Object = value; }
        }

        public bool Initial { get; set; }

        public bool Final { get; set; }

    }
}
