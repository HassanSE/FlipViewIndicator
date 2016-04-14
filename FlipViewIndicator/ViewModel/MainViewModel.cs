using FlipViewIndicator.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using GalaSoft.MvvmLight.Command;

namespace FlipViewIndicator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            this.Colors = new ObservableCollection<Item>(typeof(Colors).GetRuntimeProperties().Take(25).Select
                (c => new Item
            {
                Color = (Windows.UI.Color)c.GetValue(null),
                Name = c.Name
            }));
            this.SelectedColor = Colors[0];
            IndicatorTapCommand = new RelayCommand<Item>( (s) => OnIndicatorTap(s), (s) => true );
        }

        private Item selectedColor;
        public Item SelectedColor
        {
            get { return selectedColor; }
            set
            {
                Set(() => SelectedColor, ref selectedColor, value);
                foreach (var color in this.Colors.Where(x => x.IsSelected))
                    color.IsSelected = false;
                if (value != null)
                    value.IsSelected = true;
            }
        }

        private ObservableCollection<Item> color;
        public ObservableCollection<Item> Colors
        {
            get { return color; }
            set
            {
                Set(() => Colors, ref color, value);
            }
        }

        public RelayCommand<Item> IndicatorTapCommand { get; private set; }

        void OnIndicatorTap(Item selectedItem)
        {
            this.SelectedColor = selectedItem;
            this.IndicatorTapCommand.RaiseCanExecuteChanged();
        }
    }
}
