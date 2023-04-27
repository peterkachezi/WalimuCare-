using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace WalimuCare.ViewModels
{
    public class ComboBoxViewModel : INotifyPropertyChanged

    {
        public ComboBoxViewModel()

        {

            Colors = new ObservableCollection<object>
            {
                "Red",

                "Pink",

                "Orange",

                "Blue",

                "Violet",

                "Yellow",

                "Green"
            };

            SelectedItem = new ObservableCollection<object> { "Red", "Blue" };

        }



        private ObservableCollection<object> _colors;



        private ObservableCollection<object> _selectedItem;



        public event PropertyChangedEventHandler PropertyChanged;



        public void RaisePropertyChanged(string name)

        {

            if (PropertyChanged != null)

            {

                PropertyChanged(this, new PropertyChangedEventArgs(name));

            }

        }

        public ObservableCollection<object> Colors

        {

            get

            {

                return _colors;

            }

            set

            {

                _colors = value;

                RaisePropertyChanged("Colors");

            }

        }



        public ObservableCollection<object> SelectedItem

        {

            get

            {

                return _selectedItem;

            }

            set

            {

                _selectedItem = value;

                RaisePropertyChanged("SelectedItem");

            }

        }

    }
}
