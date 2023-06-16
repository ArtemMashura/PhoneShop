using System;
using MVVM_PhoneShop.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.IO;
using MVVM_PhoneShop.View.Custom.Phone;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using MVVM_PhoneShop.View.Custom.Gallery;

namespace MVVM_PhoneShop.ViewModel
{
    internal class GalleryVM : INotifyPropertyChanged
    {
        string strConn = "Server=localhost\\SQLEXPRESS;" +
                    "Database=phone_snop_db;" +
                    "Trusted_Connection=True;" +
                    "TrustServerCertificate=True;";
        public int GalleryLength = 8;
        private int currentPage;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        private string phone1_ImgPath;
        public string Phone1_ImgPath
        {
            get { return phone1_ImgPath; }
            set { phone1_ImgPath = value; OnPropertyChanged("Phone1_ImgPath"); }
        }


        private Model.Gallery gallery;
        public Model.Gallery MyGallery 
        {   get { return gallery; } 
            set { gallery = value; OnPropertyChanged("Gallery"); }
        }
        private ObservableCollection<Model.Phone> dataset;
        public ObservableCollection<Model.Phone> Dataset
        {
            get { return dataset; }
            set { dataset = value; OnPropertyChanged("Dataset"); }
        }

        private ObservableCollection<Model.Phone> shownGallery;
        public ObservableCollection<Model.Phone> ShownGallery
        {
            get { return shownGallery; }
            set { shownGallery = value; OnPropertyChanged("ShownGallery"); }
        }

        private ObservableCollection<Model.Phone> shownDataset;
        public ObservableCollection<Model.Phone> ShownDataset
        {
            get { return shownDataset; }
            set { shownDataset = value; OnPropertyChanged("ShownGallery"); }
        }


        private string moreThan;
        public string MoreThan
        {
            get { return moreThan; }
            set { moreThan = value; OnPropertyChanged("MoreThan"); }
        }
        private string equalTo;
        public string EqualTo
        {
            get { return equalTo; }
            set { equalTo = value; OnPropertyChanged("EqualTo"); }
        }
        private string lessThan;
        public string LessThan
        {
            get { return lessThan; }
            set { lessThan = value; OnPropertyChanged("LessThan"); }
        }
        public GalleryVM()
        {
            MoreThan = "True";
            EqualTo = "True";
            LessThan = "True";  
            CurrentPage = 1;
            Dataset = new ObservableCollection<Model.Phone>();
            ShownGallery = new ObservableCollection<Model.Phone>();
            ShownDataset = new ObservableCollection<Model.Phone>();
            string sqlComm = "SELECT * FROM [phones];";
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("CONNECTED");

                    SqlCommand command = new SqlCommand(sqlComm, conn);

                    SqlDataReader reader = command.ExecuteReader();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write($"{reader.GetName(i)} ({reader.GetFieldType(i).ToString()})\t");
                    }
                    Console.WriteLine();

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i += 7)
                        {
                            Dataset.Add(new Model.Phone((int)reader[i], (string)reader[i + 1], (string)reader[i + 2], (double)reader[i + 3], (bool)reader[i + 4], (string)reader[i + 5], (string)reader[i + 6]));
                        } 
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            MyGallery = new Model.Gallery(Dataset);             // Тут можно просто удалить модель галереи из Model и работать с этим dataset, но я решил сделать так
            foreach (Model.Phone phone in MyGallery.PhonesList) // потому что если бы это было реальное приложение, то модель все равно бы рано или поздно понадобилась бы
            {
                ShownDataset.Add(phone);
            }
            for (int i = 0; i < GalleryLength; i++)
            {
                ShownGallery.Add(ShownDataset[i]);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string args = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(args));
            }
        }

        public RelayCommand PrevPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CurrentPage == 1)
                    {
                        return;
                    }
                    else
                    {
                        ShownGallery.Clear();
                        CurrentPage -= 1;
                        for (int i = 0; i < GalleryLength; i++)
                        {
                            ShownGallery.Add(ShownDataset[(CurrentPage - 1) * GalleryLength + i]);
                        }
                    }
                });
            }
            set
            {
            }
        }

        public RelayCommand NextPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (CurrentPage == ShownDataset.Count / 8 + 1)
                    {
                        return;
                    }
                    else
                    {
                        ShownGallery.Clear();
                        CurrentPage += 1;
                        for (int i = 0; i < GalleryLength; i++)
                        {
                            if (ShownDataset.Count > (CurrentPage - 1) * GalleryLength + i)
                            {
                                ShownGallery.Add(ShownDataset[(CurrentPage - 1) * GalleryLength + i]);

                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                });
            }
            set
            {
            }
        }

        public RelayCommand ResetFilter
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MoreThan = "True";
                    EqualTo = "True";
                    LessThan = "True";
                    ShownGallery.Clear();
                    ShownDataset.Clear();
                    CurrentPage = 1;
                    foreach (Model.Phone phone in MyGallery.PhonesList)
                    {
                        ShownDataset.Add(phone);
                    }
                    for (int i = 0; i < GalleryLength; i++)
                    {
                        ShownGallery.Add(ShownDataset[i]);
                    }
                });
            }
            set
            {
            }
        }

        public RelayCommand SortLowPrice
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MoreThan = "True";
                    EqualTo = "True";
                    LessThan = "False";
                    ShownGallery.Clear();
                    ShownDataset.Clear();
                    CurrentPage = 1;
                    int j = 0;
                    foreach (Model.Phone phone in MyGallery.PhonesList)
                    {
                        if (phone.Price < 123423.123)
                        {
                            ShownDataset.Add(phone);
                            j++;
                        }
                    }
                    int count = 0;
                    foreach (Model.Phone phone1 in ShownDataset)
                    {
                        ShownGallery.Add(phone1);
                        count++;
                        if (count == GalleryLength)
                        {
                            break;
                        }
                    }
                });
            }
            set
            {
            }
        }

        public RelayCommand SortMediumPrice
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MoreThan = "True";
                    EqualTo = "False";
                    LessThan = "True";
                    ShownGallery.Clear();
                    ShownDataset.Clear();
                    CurrentPage = 1;
                    int j = 0;
                    foreach (Model.Phone phone in MyGallery.PhonesList)
                    {
                        if (phone.Price == 123423.123)
                        {
                            ShownDataset.Add(phone);
                            j++;
                        }
                    }
                    int count = 0;
                    foreach (Model.Phone phone1 in ShownDataset)
                    {
                        ShownGallery.Add(phone1);
                        count++;
                        if (count == GalleryLength)
                        {
                            break;
                        }
                    }

                });
            }
            set
            {
            }
        }

        public RelayCommand SortHighPrice
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MoreThan = "False";
                    EqualTo = "True";
                    LessThan = "True";
                    ShownGallery.Clear();
                    ShownDataset.Clear();
                    CurrentPage = 1;
                    int j = 0;
                    foreach (Model.Phone phone in MyGallery.PhonesList)
                    {
                        if (phone.Price > 123423.123)
                        {
                            ShownDataset.Add(phone);
                            j++;
                        }
                    }
                    int count = 0;
                    foreach (Model.Phone phone1 in ShownDataset)
                    {
                        ShownGallery.Add(phone1);
                        count++;
                        if (count == GalleryLength)
                        {
                            break;
                        }
                    }
                });
            }
            set
            {
            }
        }
    }
}
