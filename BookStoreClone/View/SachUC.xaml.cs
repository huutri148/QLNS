using BookStoreClone.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BookStoreClone.View
{
    public partial class SachUC : UserControl
    {
        public SachUC(string name, string DonGia, string img, string soLuongTon)
        {
            InitializeComponent();

            DataContext = new SachViewModel();
            tbTenSach.Text = name;
            tbDonGia.Text = DonGia;

            string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(_localLink + @"Resources\img\" + img);
            bitmap.EndInit();
            imgAnhSach.Source = bitmap;

            if (int.Parse(soLuongTon) > Const.QuyDinh_TonToiThieuSauKhiBan)
                cardHetHang.Visibility = Visibility.Collapsed;
            else
                cardHetHang.Visibility = Visibility.Visible;
        }
    }
}