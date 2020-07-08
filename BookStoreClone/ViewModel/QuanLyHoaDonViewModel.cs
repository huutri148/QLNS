using BookStoreClone.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    internal class QuanLyHoaDonViewModel : BaseViewModel
    {
        private ObservableCollection<HoaDon> _listHoaDon;
        private ObservableCollection<CTHD> _listCTHD_BanSach;
        public ObservableCollection<HoaDon> ListHoaDon { get => _listHoaDon; set { _listHoaDon = value; OnPropertyChanged(); } }
        public ObservableCollection<CTHD> ListCTHD_BanSach { get => _listCTHD_BanSach; set { _listCTHD_BanSach = value; } }
        public ObservableCollection<int> ListChonSoLuong { get; set; }

        private DateTime _selectedDateTime;
        public DateTime SelectedDateTime { get => _selectedDateTime; set { _selectedDateTime = value; OnPropertyChanged(); } }

        DateTime _DateTimeStart;
        DateTime _DateTimeEnd;
        public DateTime DateTimeStart { get => _DateTimeStart; set { _DateTimeStart = value;  OnPropertyChanged(); CapNhatHoaDonVaTimKiem(); } }
        public DateTime DateTimeEnd { get => _DateTimeEnd; set { _DateTimeEnd = value; OnPropertyChanged(); CapNhatHoaDonVaTimKiem();  } }


        string _TimKiemHoaDon;
        public string TextTimKiemHoaDon { get => _TimKiemHoaDon; set { _TimKiemHoaDon = value; OnPropertyChanged(); CapNhatHoaDonVaTimKiem(); } }
        #region Thêm Sách done

        private int _tongGiaBan;

        private CTHD _selectedItemCTHD;
        private QuanLyDuLieuSachViewModel QuanLyDuLieuSachVM;
        public ICommand LoadCardThemSach { get; set; }
        public ICommand ThemSachVaoHoaDonCommand { get; set; }
        public ICommand CellEditEndingThemSachCommand { get; set; }
        public ICommand XoaChonSachCommand { get; set; }
        public ICommand ShowListChonSachCommnad { get; set; }
        public ICommand AnListChonSachComamnd { get; set; }

        public int TongGiaBan { get => _tongGiaBan; set { _tongGiaBan = value; OnPropertyChanged(); } }

        public CTHD SelectedItemCTHD
        {
            get => _selectedItemCTHD;
            set
            {
                _selectedItemCTHD = value;
                OnPropertyChanged();
                if (SelectedItemCTHD == null)
                    return;

                for (int i = 2; i < SelectedItemCTHD.Sach.SoLuongTon - Const.QuyDinh_TonToiThieuSauKhiBan; i++)
                {
                    ListChonSoLuong.Add(i);
                }
            }
        }

        #endregion Thêm Sách done



        #region Khách hàng done

        public QuanLyKhachHangViewModel QuanLyKhachHangVM;
        private KhachHang _selectedKhachHang;
        public KhachHang SelectedKhachHang { get => _selectedKhachHang; set { _selectedKhachHang = value; OnPropertyChanged(); } }
        public ICommand ChonKhachHangCommand { get; set; }
        public ICommand ShowListChonKhachHangCommand { get; set; }
        public ICommand AnListChonKhachHangCommad { get; set; }
        public ICommand LoadCardKhachHang { get; set; }

        #endregion Khách hàng done

        #region Lưu hóa đơn

        private string _soTienTra;
        private HoaDon _SelectedHoaDon;
        public ICommand LuuHoaDonCommand { get; set; }
        public ICommand TaoMoiHoaDonCommand { get; set; }
        public string SoTienTra { get => _soTienTra; set { _soTienTra = value; OnPropertyChanged(); } }
        public HoaDon SelectedHoaDon { get => _SelectedHoaDon; set { _SelectedHoaDon = value; OnPropertyChanged(); XulyHienThemHoaDon(-1); } }

        #endregion Lưu hóa đơn

        #region Người dùng

        private NguoiDung _user;
        public NguoiDung User { get => _user; set { _user = value; OnPropertyChanged(); } }

        #endregion Người dùng

        #region Giao diện

        private Visibility _visibilityChonKhachHang;
        private Visibility _visibilityChonSach;
        private Visibility _visibilityXemHoaDon;
        private Visibility _visibilityTaoHoaDon;

        private Visibility _visibilitBtnChonSach;
        private Visibility _visibilitBtnChonKhachHang;

        private bool _btnThemMoiKhachHang;
        private bool _btnThemHoaDonMoi;
        public Visibility VisibilityChonKhachHang { get => _visibilityChonKhachHang; set { _visibilityChonKhachHang = value; OnPropertyChanged(); VisibilitBtnChonKhachHang = VisibilityChonKhachHang == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility VisibilityChonSach { get => _visibilityChonSach; set { _visibilityChonSach = value; OnPropertyChanged(); VisibilitBtnChonSach = VisibilityChonSach == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible; } }
        public Visibility VisibilityXemHoaDon { get => _visibilityXemHoaDon; set { _visibilityXemHoaDon = value; OnPropertyChanged(); } }
        public Visibility VisibilityTaoHoaDon { get => _visibilityTaoHoaDon; set { _visibilityTaoHoaDon = value; OnPropertyChanged(); BtnThemHoaDonMoi = VisibilitBtnChonSach == Visibility.Visible ? true : false; } }

        public Visibility VisibilitBtnChonSach { get => _visibilitBtnChonSach; set { _visibilitBtnChonSach = value; OnPropertyChanged(); } }
        public Visibility VisibilitBtnChonKhachHang { get => _visibilitBtnChonKhachHang; set { _visibilitBtnChonKhachHang = value; OnPropertyChanged(); } }

        public bool BtnThemMoiKhachHang { get => _btnThemMoiKhachHang; set { _btnThemMoiKhachHang = value; OnPropertyChanged(); } }
        public bool BtnThemHoaDonMoi { get => _btnThemHoaDonMoi; set { _btnThemHoaDonMoi = value; OnPropertyChanged(); } }

        

        private void XulyHienThemHoaDon(int n)
        {
            if (n == 0)
            {
                VisibilityChonSach = Visibility.Collapsed;
                VisibilityTaoHoaDon = Visibility.Visible;
                VisibilityXemHoaDon = Visibility.Collapsed;
                VisibilityChonKhachHang = Visibility.Visible;
                BtnThemHoaDonMoi = false;
            }
            if (n == 1)
            {
                VisibilityChonSach = Visibility.Visible;
                VisibilityTaoHoaDon = Visibility.Visible;
                VisibilityXemHoaDon = Visibility.Collapsed;
                VisibilityChonKhachHang = Visibility.Collapsed;
                BtnThemHoaDonMoi = true;
            }
            if (n == -1)
            {
                VisibilityChonSach = Visibility.Collapsed;
                VisibilityTaoHoaDon = Visibility.Collapsed;
                VisibilityXemHoaDon = Visibility.Visible;
                VisibilityChonKhachHang = Visibility.Collapsed;
                BtnThemHoaDonMoi = false;
            }
            if (n == 2)
            {
                VisibilityChonSach = Visibility.Collapsed;
                VisibilityTaoHoaDon = Visibility.Visible;
                VisibilityXemHoaDon = Visibility.Collapsed;
                VisibilityChonKhachHang = Visibility.Collapsed;
                BtnThemHoaDonMoi = true;
            }
            SelectedDateTime = System.DateTime.Now;
        }

        #endregion Giao diện

        public QuanLyHoaDonViewModel()
        {
            DateTimeStart = new DateTime(2020, 01, 01, 01, 01, 01);
            DateTimeEnd = System.DateTime.Now;
            

            TextTimKiemHoaDon = "";
            //ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons);
            



            ListCTHD_BanSach = new ObservableCollection<CTHD>();

            ListChonSoLuong = new ObservableCollection<int>();
            ListChonSoLuong.Add(1);

            XulyHienThemHoaDon(-1);

            User = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == Const.IDNguoiDung).First();

            #region Thêm sách done

            ThemSachVaoHoaDonCommand = new RelayCommand<Sach>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < ListCTHD_BanSach.Count; i++)
                {
                    if (p.MaSach == ListCTHD_BanSach[i].Sach.MaSach)
                        return;
                }

                ListCTHD_BanSach.Add(new CTHD() { Sach = DataProvider.Ins.DB.Saches.Where(x => x.MaSach == p.MaSach).First(), DonGiaBan = p.DonGia, SoLuong = 1 });
                TongGiaBan = ListCTHD_BanSach.Sum(x => (int)x.SoLuong * (int)x.DonGiaBan);
                Const.IDNguoiDung = DataProvider.Ins.DB.NguoiDungs.ToList()[1].TenDangNhap;
            });

            CellEditEndingThemSachCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                try { TongGiaBan = ListCTHD_BanSach.Sum(x => (int)x.SoLuong * (int)x.DonGiaBan); }
                catch { }
            });
            ShowListChonSachCommnad = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                XulyHienThemHoaDon(1);
            });
            AnListChonSachComamnd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                VisibilityChonSach = Visibility.Collapsed;
            });
            XoaChonSachCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < ListCTHD_BanSach.Count; i++)
                {
                    if (ListCTHD_BanSach[i].Sach.MaSach == SelectedItemCTHD.Sach.MaSach)
                    {
                        ListCTHD_BanSach.Remove(ListCTHD_BanSach[i]);
                        TongGiaBan = ListCTHD_BanSach.Sum(x => (int)x.SoLuong * (int)x.DonGiaBan);
                        return;
                    }
                }
            });
            LoadCardThemSach = new RelayCommand<Card>((p) => { return true; }, (p) =>
            {
                QuanLyDuLieuSachVM = p.DataContext as QuanLyDuLieuSachViewModel;
            });

            #endregion Thêm sách done

            #region Chọn khách hàng done

            ChonKhachHangCommand = new RelayCommand<KhachHang>((p) => { return true; }, (p) =>
            {
                SelectedKhachHang = p;
            });

            ShowListChonKhachHangCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                XulyHienThemHoaDon(0);
            });
            AnListChonKhachHangCommad = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                VisibilityChonKhachHang = Visibility.Collapsed;
            });
            LoadCardKhachHang = new RelayCommand<Card>((p) => { return true; }, (p) =>
            {
                QuanLyKhachHangVM = p.DataContext as QuanLyKhachHangViewModel;
            });

            #endregion Chọn khách hàng done

            #region Hóa đơn

            LuuHoaDonCommand = new RelayCommand<Button>(
                (p) =>
                {
                    if (ListCTHD_BanSach.Count == 0) return false;
                    p.IsEnabled = false;

                    if (SelectedKhachHang == null) return false;

                    if (User == null) return false;

                    if (KiemTraSo(SoTienTra) == false) return false;

                    if (int.Parse(SoTienTra) > TongGiaBan) return false;

                    for (int i = 0; i < ListCTHD_BanSach.Count; i++)
                        if (ListCTHD_BanSach[i].SoLuong < 1) return false;

                    p.IsEnabled = true;
                    return true;
                },
                (p) =>
                {
                    while (true)
                        try
                        {
                            HoaDon hoaDon = new HoaDon() { CTHDs = new ObservableCollection<CTHD>(ListCTHD_BanSach), KhachHang = SelectedKhachHang, NguoiDung = User, TongTien = TongGiaBan, NgayBan = SelectedDateTime };
                            hoaDon.SoTienTra = int.Parse(SoTienTra);

                            DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[HoaDon] ON");
                            DataProvider.Ins.DB.HoaDons.Add(hoaDon);
                            DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[HoaDon] OFF");
                            SelectedKhachHang.SoTienNo += TongGiaBan - int.Parse(SoTienTra);
                            for (int i = 0; i < ListCTHD_BanSach.Count; i++)
                                ListCTHD_BanSach[i].Sach.SoLuongTon -= ListCTHD_BanSach[i].SoLuong;

                            DataProvider.Ins.DB.SaveChanges();
                       

                            SoTienTra = 0 + "";
                            TongGiaBan = 0;
                            SelectedKhachHang = new KhachHang();
                            ListCTHD_BanSach.Clear();
                            SelectedDateTime = new DateTime();
                            QuanLyDuLieuSachVM.TimKiemSach();
                            QuanLyKhachHangVM.TimKiemKhachHang();
                            SelectedHoaDon = hoaDon;
                            CapNhatHoaDonVaTimKiem();
                            XulyHienThemHoaDon(-1);
                            
                            return;
                        }
                        catch { }

                });

            TaoMoiHoaDonCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                XulyHienThemHoaDon(2);
            });

            #endregion Hóa đơn

        }

        #region Kiểm tra số

        private bool KiemTraSo(string str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch { return false; }
        }

        #endregion Kiểm tra số

        #region Cập nhật dữ liệu

        private void CapNhatHoaDonVaTimKiem()
        {


            //var date = DateTimeEnd.AddHours(11).AddMinutes(59).AddSeconds(59);
            var date = DateTimeEnd.AddDays(1);

            if (TextTimKiemHoaDon=="")
                ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(x=>x.NgayBan>=DateTimeStart&&x.NgayBan<=date));
            else
                ListHoaDon = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(x => x.NgayBan >= DateTimeStart && x.NgayBan <=date && x.KhachHang.TenKH.ToLower().Contains(TextTimKiemHoaDon.ToLower())));
            
            
        }

        #endregion Cập nhật dữ liệu
    }
}