using MVVM_PhoneShop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM_PhoneShop.View.Custom.Gallery
{
    /// <summary>
    /// Interaction logic for Gallery.xaml
    /// </summary>
    public partial class Gallery : UserControl, INotifyPropertyChanged
    {
        //private static DependencyProperty Phone_ImagePathProperty = DependencyProperty.Register("Phone_ImgPath", typeof(string), typeof(Model.Phone));
        //public string Phone_ImgPath
        //{
        //    get { return (string)GetValue(Phone_ImagePathProperty); }
        //    set { SetValue(Phone_ImagePathProperty, value); OnPropertyChanged("Phone_ImgPath"); }
        //}
        //private static DependencyProperty Phone1_ImagePathProperty = DependencyProperty.Register("Phone1_ImgPath", typeof(string), typeof(Model.Phone));
        //public string Phone1_ImgPath
        //{
        //    get { return (string)GetValue(Phone1_ImagePathProperty); }
        //    set { SetValue(Phone1_ImagePathProperty, value); OnPropertyChanged("Phone1_ImgPath"); }
        //}
        public Gallery()
        {
            this.DataContext = new GalleryVM();
            InitializeComponent();

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
