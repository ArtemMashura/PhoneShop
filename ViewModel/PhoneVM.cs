using MVVM_PhoneShop.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_PhoneShop.ViewModel
{
    internal class PhoneVM : INotifyPropertyChanged
    {
        //private Phone phone;

        //public Phone MyPhone
        //{
        //    get { return phone; } 
        //    set { phone = value; OnPropertyChanged("MyPhone"); }
        //}
        public PhoneVM(/*Phone inputPhone*/) 
        {
            //phone = inputPhone;
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
