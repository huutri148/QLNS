using BookStoreClone.Model;
using BookStoreClone.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    internal class QuanLyNhapSachViewModel : BaseViewModel
    {
        private ObservableCollection<PhieuNhap> _ListPhieuNhap;
        public ObservableCollection<PhieuNhap> ListPhieuNhap { get => _ListPhieuNhap; set { _ListPhieuNhap = value; OnPropertyChanged(); } }

        #region Tìm kiếm
        DateTime _selectedDateTimeStart;
        DateTime _selectedDateTimeEnd;
        string _TextTimKiemPhieuNhap;
        public DateTime SelectedDateTimeStart { get => _selectedDateTimeStart; set { _selectedDateTimeStart = value; OnPropertyChanged(); try { TimKiemVaCapNhat(); } catch { } } }
        public DateTime SelectedDateTimeEnd { get => _selectedDateTimeEnd; set { _selectedDateTimeEnd = value; OnPropertyChanged(); try { TimKiemVaCapNhat(); } catch { } } }
        public string TextTimKiemPhieuNhap { get => _TextTimKiemPhieuNhap; set { _TextTimKiemPhieuNhap = value; OnPropertyChanged(); try { TimKiemVaCapNhat(); } catch { } } }
        #endregion



        QuanLyDuLieuSachViewModel quanLyDuLieuSachViewModel;

        #region Khởi tạo nhập sách
        public ICommand CellEditEndingThemSachCommand { get; set; }
        public ICommand XoaSachDaChonCommand { get; set; }
        public ICommand LuuPhieuNhapCommand { get; set; }
        public ICommand ThemSachVaoPhieuNhapCommand { get; set; }

       

       

        private NguoiDung _user;
        public NguoiDung User { get => _user; set { _user = value; OnPropertyChanged(); } }

        private ObservableCollection<int> _ListChonSoLuongSach;
        public ObservableCollection<int> ListChonSoLuongSach { get => _ListChonSoLuongSach; set { _ListChonSoLuongSach = value; OnPropertyChanged(); } }

        private CTPhieuNhap _selectedCTPhieuNhap;
        public CTPhieuNhap SelectedCTPhieuNhap { get => _selectedCTPhieuNhap; set { _selectedCTPhieuNhap = value; OnPropertyChanged(); } }

        private ObservableCollection<CTPhieuNhap> _ListCTPhieuNhap;
        public ObservableCollection<CTPhieuNhap> ListCTPhieuNhap { get => _ListCTPhieuNhap; set { _ListCTPhieuNhap = value; OnPropertyChanged(); } }

        int _TongSoTienNhapSach;
        public int TongSoTienNhapSach { get => _TongSoTienNhapSach; set { _TongSoTienNhapSach = value; OnPropertyChanged(); } }


        DateTime selectedDateTime;
        public DateTime SelectedDateTime { get => selectedDateTime; set { selectedDateTime = value;OnPropertyChanged(); } }

        PhieuNhap _selectedPhieuNhap;
        public PhieuNhap SelectedPhieuNhap { get => _selectedPhieuNhap; set { _selectedPhieuNhap = value; OnPropertyChanged(); CaiDatGiaoDien(0); } }



        #endregion

        #region Giao diện
        Visibility _visibilityXemChiTiet;
        Visibility _visibilityTaoMoi;
        Visibility _visibilityChonSach;
        Visibility _visibilityMoChonSach;
        bool _isBtnTaoMoi;
        

        public Visibility VisibilityXemChiTiet { get => _visibilityXemChiTiet; set { _visibilityXemChiTiet = value; OnPropertyChanged(); } }
        public Visibility VisibilityTaoMoi { get => _visibilityTaoMoi; set { _visibilityTaoMoi = value; OnPropertyChanged(); } }
        public Visibility VisibilityChonSach { get => _visibilityChonSach; set { _visibilityChonSach = value; OnPropertyChanged(); } }
        public Visibility VisibilityMoChonSach { get => _visibilityMoChonSach; set {_visibilityMoChonSach = value; OnPropertyChanged(); } }
        public bool IsBtnTaoMoi { get => _isBtnTaoMoi; set { _isBtnTaoMoi = value; OnPropertyChanged(); } }
      

        public ICommand LoadCardThemSach { get; set; }
        public ICommand ShowTaoHoaDonMoiCommand { get; set; }
        public ICommand AnListChonSachComamnd { get; set; }

        public ICommand HienListChonSachCommand { get; set; }
       

        void CaiDatGiaoDien(int i)
        {
            if(i==0)
            {
                IsBtnTaoMoi = false;
                VisibilityXemChiTiet = Visibility.Visible;
                VisibilityTaoMoi = Visibility.Collapsed;
                VisibilityChonSach = Visibility.Collapsed;
                VisibilityMoChonSach = Visibility.Visible;
            }
            if (i == 1)
            {
                IsBtnTaoMoi = true;
                VisibilityXemChiTiet = Visibility.Collapsed;
                VisibilityTaoMoi = Visibility.Visible;
                VisibilityChonSach = Visibility.Collapsed;
                VisibilityMoChonSach = Visibility.Visible;
            }
            if (i == 2)
            {
                VisibilityMoChonSach = Visibility.Visible;
                 VisibilityChonSach = Visibility.Collapsed;
            }
            if (i == 3)
            {
                VisibilityMoChonSach = Visibility.Collapsed;
                VisibilityChonSach = Visibility.Visible;
            }
        }
        #endregion

        public QuanLyNhapSachViewModel()
        {
            #region Khởi tạo

            SelectedDateTimeStart = new DateTime(2020, 1, 1, 1, 1, 1,1);
            SelectedDateTimeEnd = System.DateTime.Now;
            TimKiemVaCapNhat();
            ListCTPhieuNhap = new ObservableCollection<CTPhieuNhap>();
            User = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == Const.IDNguoiDung).First();
            ListChonSoLuongSach = new ObservableCollection<int>();
            for (int i = Const.QuyDinh_SoLuongSachNhapToiThieu; i <= Const.QuyDinh_SoLuongSachNhapToiDa; i++) { ListChonSoLuongSach.Add(i); }
            if(ListPhieuNhap.Count!=0)
                CaiDatGiaoDien(0);
            else CaiDatGiaoDien(1);
            ReserTaoMoiHoaDon();


            #endregion Khởi tạo

            #region Tạo Hóa Đơn
            ThemSachVaoPhieuNhapCommand = new RelayCommand<Sach>(
                (p) => { return true; }, (p) =>
                {
                
                    for (int i = 0; i < ListCTPhieuNhap.Count; i++)
                        if (ListCTPhieuNhap[i].Sach.MaSach == p.MaSach)
                            return;
                    ListCTPhieuNhap.Add(new CTPhieuNhap() { Sach = p, DonGiaNhap = 100000, SoLuongNhap = Const.QuyDinh_SoLuongSachNhapToiThieu }
                    );
                    CapNhatTongSoTienNhap();

                }
           );
            XoaSachDaChonCommand = new RelayCommand<CTPhieuNhap>( (p) => { return true; }, (p) => { ListCTPhieuNhap.Remove(p); CapNhatTongSoTienNhap(); } );
            CellEditEndingThemSachCommand = new RelayCommand<object>( (p) => { return true; }, (p) => { CapNhatTongSoTienNhap(); } );
            LuuPhieuNhapCommand = new RelayCommand<Button>((p) => {
                if (p == null) return false;
                p.IsEnabled = false;
               
                    for (int i = 0; i < ListCTPhieuNhap.Count; i++)
                        if (KiemTraSo(ListCTPhieuNhap[i].DonGiaNhap.ToString()) == false) return false;
                p.IsEnabled = true;
                return true;
            }, (p) => {

                PhieuNhap phieuNhap = new PhieuNhap() { NguoiDung = User, CTPhieuNhaps = new ObservableCollection<CTPhieuNhap>(ListCTPhieuNhap), NgayNhap = selectedDateTime };
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PhieuNhap] ON");
                DataProvider.Ins.DB.PhieuNhaps.Add(phieuNhap);
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PhieuNhap] OFF");
                
                        
                for (int i = 0; i < ListCTPhieuNhap.Count; i++)
                {
                    ListCTPhieuNhap[i].Sach.SoLuongTon += ListCTPhieuNhap[i].SoLuongNhap;
                    ListCTPhieuNhap[i].Sach.DonGia = ListCTPhieuNhap[i].DonGiaNhap * Const.QuyDinh_HeSoDonGia / 100;
                }
                DataProvider.Ins.DB.SaveChanges();
                TimKiemVaCapNhat();
                ReserTaoMoiHoaDon();
                quanLyDuLieuSachViewModel.TimKiemSach();
                SelectedPhieuNhap = phieuNhap;

                CaiDatGiaoDien(0);
            });
            #endregion

            #region Giao diện
            ShowTaoHoaDonMoiCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CaiDatGiaoDien(1);
                ReserTaoMoiHoaDon();
            });

            AnListChonSachComamnd = new RelayCommand<object>((p) => { return true; }, (p) => {
                     CaiDatGiaoDien(2);
              
                 });

            HienListChonSachCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CaiDatGiaoDien(3);

            });
            LoadCardThemSach = new RelayCommand<Card>((p) => { return true; }, (p) => {
                
                quanLyDuLieuSachViewModel = p.DataContext as QuanLyDuLieuSachViewModel;
            });
            #endregion

        }
        #region hàm
       
        void CapNhatTongSoTienNhap()
        {
            TongSoTienNhapSach = 0;
            try
            {
                TongSoTienNhapSach = ListCTPhieuNhap.Sum(x => x.DonGiaNhap * x.SoLuongNhap);
            }
            catch { TongSoTienNhapSach = 999999999; }
        }

        void ReserTaoMoiHoaDon()
        {
            ListCTPhieuNhap = new ObservableCollection<CTPhieuNhap>();
            SelectedDateTime = System.DateTime.Now;
            TongSoTienNhapSach = 0;

        }
        private bool KiemTraSo(string str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch { return false; }
        }
        #endregion
        #region Tìm kiếm
        private void TimKiemVaCapNhat()
        {
            var date = new DateTime(SelectedDateTimeEnd.Year, SelectedDateTimeEnd.Month, SelectedDateTimeEnd.Day, 23, 59, 59);
          
            if(TextTimKiemPhieuNhap == "" || TextTimKiemPhieuNhap ==null)
                ListPhieuNhap = new ObservableCollection<PhieuNhap>(DataProvider.Ins.DB.PhieuNhaps.Where(x=>x.NgayNhap>=SelectedDateTimeStart&&x.NgayNhap<=date));
            else ListPhieuNhap = new ObservableCollection<PhieuNhap>(DataProvider.Ins.DB.PhieuNhaps.Where(x => x.NgayNhap >= SelectedDateTimeStart && x.NgayNhap <= date && x.NguoiDung.TenND.ToLower().Contains(TextTimKiemPhieuNhap.ToLower())));
            for (int i = 0; i < ListPhieuNhap.Count; i++)
            {
                ListPhieuNhap[i].GiaTriPhieuNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.DonGiaNhap * x.SoLuongNhap);
                ListPhieuNhap[i].TongSoSachNhap = ListPhieuNhap[i].CTPhieuNhaps.Sum(x => x.SoLuongNhap);
            }
        }



        #endregion
    }
}