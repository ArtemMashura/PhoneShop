using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_PhoneShop.Model
{
    public class Phone : INotifyPropertyChanged
    {
        public Phone(int id, string phoneImage, string name, double price, bool isAvaliable, string code, string isAvaliableString) 
        {
            ID = id;
            Image = phoneImage;
            Name = name;
            Price = price;
            IsAvaliable = isAvaliable;
            Code = code;
            IsAvaliableString = isAvaliableString;
        }
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; OnPropertyChanged("ID"); }
        }
        private string image;

        public string Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged("Image"); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged("Price"); }
        }

        private bool isAvaliable;

        public bool IsAvaliable
        {
            get { return isAvaliable; }
            set { isAvaliable = value; OnPropertyChanged("IsAvaliable"); }
        }

        private string code;

        public string Code
        {
            get { return code; }
            set { code = value; OnPropertyChanged("Code"); }
        }

        private string isAvaliableString;

        public string IsAvaliableString
        {
            get { return isAvaliableString; }
            set { isAvaliableString = value; OnPropertyChanged("IsAvaliableString"); }
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
