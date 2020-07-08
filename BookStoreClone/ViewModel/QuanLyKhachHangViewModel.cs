using BookStoreClone.Model;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    public class QuanLyKhachHangViewModel : BaseViewModel
    {
        private ObservableCollection<KhachHang> _listKH;
        private KhachHang _selectedKhachHang;
        private int _tongHoaDon;

        #region Cập nhật

        private string _suaTen;
        private string _suaEmail;
        private string _suaSDT;
        private string _suaDiaChi;
        private bool _IconSuaKhachHang;
        public string TextSuaTen { get => _suaTen; set { _suaTen = value; OnPropertyChanged(); } }
        public string TextSuaEmail { get => _suaEmail; set { _suaEmail = value; OnPropertyChanged(); } }
        public string TextSuaSDT { get => _suaSDT; set { _suaSDT = value; OnPropertyChanged(); } }
        public string TextSuaDiaChi { get => _suaDiaChi; set { _suaDiaChi = value; OnPropertyChanged(); } }
        public ICommand LuuCapNhatCommand { get; set; }
        public ICommand CapNhatCommand { get; set; }
        public bool IconSuaKhachHang { get => _IconSuaKhachHang; set { _IconSuaKhachHang = value; OnPropertyChanged(); } }

        #endregion Cập nhật

        #region Thêm khách hàng

        private string _TextTimKiem;
        private string _textThemTen;
        private string _textThemDiaChi;
        private string _textThemSDT;
        private string _textThemEmail;
        public string TextThemTen { get => _textThemTen; set { _textThemTen = value; OnPropertyChanged(); } }
        public string TextThemDiaChi { get => _textThemDiaChi; set { _textThemDiaChi = value; OnPropertyChanged(); } }
        public string TextTimKiem { get => _TextTimKiem; set { _TextTimKiem = value; OnPropertyChanged(); TimKiemKhachHang(); } }
        public string TextThemSDT { get => _textThemSDT; set { _textThemSDT = value; OnPropertyChanged(); } }
        public string TextThemEmail { get => _textThemEmail; set { _textThemEmail = value; OnPropertyChanged(); } }
        public ICommand ThemMoiCommand { set; get; }
        public ICommand LuuThemMoiCommand { set; get; }

        #endregion Thêm khách hàng

        public ObservableCollection<KhachHang> ListKH { get => _listKH; set { _listKH = value; OnPropertyChanged(); } }

        public KhachHang SelectedKhachHang
        {
            get => _selectedKhachHang;

            set
            {
                _selectedKhachHang = value;
                if (SelectedKhachHang == null) return;
                TongHoaDon = (int)SelectedKhachHang.HoaDons.Sum(x => x.TongTien);
                TextSuaTen = SelectedKhachHang.TenKH;
                TextSuaSDT = SelectedKhachHang.SDT;
                TextSuaEmail = SelectedKhachHang.Email;
                TextSuaDiaChi = SelectedKhachHang.DiaChi;
                OnPropertyChanged();
            }
        }

        public int TongHoaDon { get => _tongHoaDon; set { _tongHoaDon = value; OnPropertyChanged(); } }

        public QuanLyKhachHangViewModel()
        {
            TimKiemKhachHang();
            SelectedKhachHang = ListKH.Count > 0 ? ListKH[0] : new KhachHang();
            IconSuaKhachHang = false;

            CapNhatCommand = new RelayCommand<DockPanel>((p) => { return p == null ? false : true; }, (p) =>
            {
                TextSuaTen = SelectedKhachHang.TenKH;
                TextSuaSDT = SelectedKhachHang.SDT;
                TextSuaEmail = SelectedKhachHang.Email;
                TextSuaDiaChi = SelectedKhachHang.DiaChi;

                IconSuaKhachHang = true;
                for (int i = 1; i < 5; i++)
                {
                    StackPanel stackPanel = p.Children[i] as StackPanel;
                    stackPanel.Children[1].Visibility = Visibility.Collapsed;
                    stackPanel.Children[2].Visibility = Visibility.Visible;
                }
                (p.Children[5] as Canvas).Visibility = Visibility.Visible;
            });
            LuuCapNhatCommand = new RelayCommand<DockPanel>((p) => { return p == null ? false : true; }, (p) =>
            {
                SelectedKhachHang.TenKH = TextSuaTen;
                SelectedKhachHang.SDT = TextSuaSDT;
                SelectedKhachHang.Email = TextSuaEmail;
                SelectedKhachHang.DiaChi = TextSuaDiaChi;
                DataProvider.Ins.DB.SaveChanges();

                TimKiemKhachHang();
                for (int i = 1; i < 5; i++)
                {
                    StackPanel stackPanel = p.Children[i] as StackPanel;
                    stackPanel.Children[2].Visibility = Visibility.Collapsed;

                    stackPanel.Children[1].Visibility = Visibility.Visible;
                    //(stackPanel.Children[1] as TextBlock).Text = (stackPanel.Children[2] as TextBox).Text;
                }
                IconSuaKhachHang = false;
                (p.Children[5] as Canvas).Visibility = Visibility.Collapsed;
            });

            ThemMoiCommand = new RelayCommand<PopupBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.IsPopupOpen = true;
            });
            LuuThemMoiCommand = new RelayCommand<PopupBox>((p) => { return TextThemTen != "" ? true : false; }, (p) =>
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[KhachHang] ON");
                DataProvider.Ins.DB.KhachHangs.Add(new KhachHang() { TenKH = TextThemTen, DiaChi = TextThemDiaChi, Email = TextThemEmail, SDT = TextThemSDT });
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[KhachHang] OFF");
                DataProvider.Ins.DB.SaveChanges();
                TimKiemKhachHang();
                p.IsPopupOpen = false;
            });
        }

        public void TimKiemKhachHang()
        {
            if (TextTimKiem == null)
                ListKH = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);
            else
                ListKH = new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs.Where(x => x.TenKH.ToLower().Contains(TextTimKiem.ToLower()) || x.SDT.Contains(TextTimKiem)));
            for (int i = 0; i < ListKH.Count; i++)
            {
                ListKH[i].TongSoTien = (int)ListKH[i].HoaDons.Sum(x => x.TongTien);
                ListKH[i].IsEnable_BanSach = ListKH[i].SoTienNo >= Const.QuyDinh_TienNoToiDa ? false : true;
            }
        }
    }
}