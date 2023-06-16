using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_PhoneShop.Model
{
    internal class Gallery : INotifyPropertyChanged
    {

        private ObservableCollection<Phone> phonesList;

        public ObservableCollection<Phone> PhonesList
        {
            get { return phonesList; }
            set { phonesList = value; OnPropertyChanged("PhonesList"); }
        }

        public Gallery(ObservableCollection<Phone> phones)
        {
            PhonesList = phones;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string args = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(args));
            }
        }

    }
}
