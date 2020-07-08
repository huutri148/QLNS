using BookStoreClone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    class BaoCaoViewModel : BaseViewModel
    {
        private Visibility _visibilityBaoCaoTon;
        private Visibility _visibilityBCCongNo;
        private int _thang;
        private int _nam;
        private int _tongSoSachNhap;
        private int _tongSoSachBan;
        private int _tongSoNo;
        private int _tongSoTra;
        private ObservableCollection<CTHD> _listCTHD;
        private ObservableCollection<HoaDon> _listHD;
        private ObservableCollection<PhieuThuTien> _listPTT;
        private ObservableCollection<CTPhieuNhap> _listCTPN;
        private ObservableCollection<CTBaoCaoCongNo> _listCTBaoCaoCongNo;
        private ObservableCollection<CTBaoCaoTon> _listCTBaoCaoTon;
        private DataGrid _dGBaoCaoNo;
        private DataGrid _dGBaoCaoTon;
        public ICommand ShowBCCongNoCommand { get; set; }
        public ICommand ShowBCTonCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public Visibility VisibilityBaoCaoTon { get => _visibilityBaoCaoTon; set { _visibilityBaoCaoTon = value; OnPropertyChanged(); } }
        public Visibility VisibilityBCCongNo { get => _visibilityBCCongNo; set { _visibilityBCCongNo = value; OnPropertyChanged(); } }

        public ObservableCollection<CTBaoCaoCongNo> ListCTBaoCaoCongNo { get => _listCTBaoCaoCongNo; set { _listCTBaoCaoCongNo = value; OnPropertyChanged(); } }

        public ObservableCollection<CTBaoCaoTon> ListCTBaoCaoTon { get => _listCTBaoCaoTon; set { _listCTBaoCaoTon = value; OnPropertyChanged(); } }

        public int Thang { get => _thang; set { _thang = value; OnPropertyChanged(); } }
        public int Nam { get => _nam; set { _nam = value; OnPropertyChanged(); } }

        public DataGrid DGBaoCaoNo { get => _dGBaoCaoNo; set { _dGBaoCaoNo = value; OnPropertyChanged(); } }
        public DataGrid DGBaoCaoTon { get => _dGBaoCaoTon; set { _dGBaoCaoTon = value; OnPropertyChanged(); } }

        public int TongSoSachNhap { get => _tongSoSachNhap; set { _tongSoSachNhap = value; OnPropertyChanged(); }  }
        public int TongSoSachBan { get => _tongSoSachBan; set { _tongSoSachBan = value; OnPropertyChanged(); }}
        public int TongSoNo { get => _tongSoNo; set { _tongSoNo = value; OnPropertyChanged(); }  }
        public int TongSoTra { get => _tongSoTra; set { _tongSoTra = value; OnPropertyChanged(); } }

        public BaoCaoViewModel()
        {
            Nam = 2020;
            ListCTBaoCaoCongNo = new ObservableCollection<CTBaoCaoCongNo>(DataProvider.Ins.DB.CTBaoCaoCongNoes);
            ListCTBaoCaoTon = new ObservableCollection<CTBaoCaoTon>(DataProvider.Ins.DB.CTBaoCaoTons);


            VisibilityBaoCaoTon = Visibility.Collapsed;
            VisibilityBCCongNo = Visibility.Collapsed;
            DGBaoCaoTon = new DataGrid();
            ShowBCCongNoCommand = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                showdgNo();
                VisibilityBaoCaoTon = Visibility.Collapsed;
                VisibilityBCCongNo = Visibility.Visible;
                p.Children.Clear();
                p.Children.Add(DGBaoCaoNo);
               
            });
            ShowBCTonCommand = new RelayCommand<Grid>((p) => { return true; }, (p) =>

             {

                 showdgTon();
                 VisibilityBaoCaoTon = Visibility.Visible;
                 VisibilityBCCongNo = Visibility.Collapsed;
                 p.Children.Clear();
                 p.Children.Add(DGBaoCaoTon);
             });
            TimKiemCommand = new RelayCommand<Grid>((p) => { return true; }, (p) =>
             {
                 TongSoNo = 0;
                 TongSoSachBan = 0;
                 TongSoSachNhap = 0;
                 TongSoTra = 0;
                
                 if((Nam == DateTime.Now.Year&& Thang < DateTime.Now.Month) || (Nam < DateTime.Now.Year))
                 {
                     try
                     {
                         ListCTBaoCaoCongNo = new ObservableCollection<CTBaoCaoCongNo>(DataProvider.Ins.DB.CTBaoCaoCongNoes.Where(x => x.BaoCaoCongNo.Thang == Thang && x.BaoCaoCongNo.Nam == Nam));
                         ListCTBaoCaoTon = new ObservableCollection<CTBaoCaoTon>(DataProvider.Ins.DB.CTBaoCaoTons.Where(x => x.BaoCaoTon.Nam == Nam && x.BaoCaoTon.Thang == Thang));
                         _listHD = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(x => x.NgayBan.Value.Month == Thang && x.NgayBan.Value.Year == Nam));
                         _listPTT = new ObservableCollection<PhieuThuTien>(DataProvider.Ins.DB.PhieuThuTiens.Where(x => x.NgayThuTien.Value.Month == Thang && x.NgayThuTien.Value.Year == Nam));
                         _listCTHD = new ObservableCollection<CTHD>(DataProvider.Ins.DB.CTHDs.Where(x => x.HoaDon.NgayBan.Value.Month == Thang && x.HoaDon.NgayBan.Value.Year == Nam));
                         _listCTPN = new ObservableCollection<CTPhieuNhap>(DataProvider.Ins.DB.CTPhieuNhaps.Where(x => x.PhieuNhap.NgayNhap.Value.Month == Thang && x.PhieuNhap.NgayNhap.Value.Year == Nam));
                         foreach(HoaDon a in _listHD)
                         {
                             TongSoNo +=(int)(a.TongTien - a.SoTienTra); 
                         }    
                         foreach(PhieuThuTien a in _listPTT)
                         {
                             TongSoTra += (int)a.SoTienThu;
                         }    
                         foreach( CTHD a in _listCTHD)
                         {
                             TongSoSachBan +=(int) a.SoLuong;
                         }    
                         foreach(CTPhieuNhap a in _listCTPN)
                         {
                             TongSoSachNhap += (int)a.SoLuongNhap;
                         }    

                     }
                    catch
                     {
                         MessageBox.Show("Tạm thời chưa có dữ liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                     }
                 }
                 else
                 {
                     MessageBox.Show("Chọn khoảng tìm kiếm không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                 }
               

             });
        }
       public void showdgNo()
        {
            
            DGBaoCaoNo = new DataGrid();
            DGBaoCaoNo.AutoGenerateColumns = false;
            DGBaoCaoNo.IsReadOnly = true;
            
            Binding b = new Binding("ListCTBaoCaoCongNo")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            DGBaoCaoNo.SetBinding(DataGrid.ItemsSourceProperty, b);
           

           
            DataGridTextColumn _maKH = new DataGridTextColumn();
            _maKH.Header = "Mã Khách Hàng";
            _maKH.Binding = new Binding("MaKH");
            DGBaoCaoNo.Columns.Add(_maKH);

            DataGridTextColumn _tenKH = new DataGridTextColumn();
            _tenKH.Header = " Tên Khách Hàng";
            _tenKH.Binding = new Binding("KhachHang.TenKH");
            DGBaoCaoNo.Columns.Add(_tenKH);

            DataGridTextColumn _noDau = new DataGridTextColumn();
            _noDau.Header = "Số tiền nợ đầu";
            _noDau.Binding = new Binding("SoTienNoDau");
            DGBaoCaoNo.Columns.Add(_noDau);

            DataGridTextColumn _noCuoi = new DataGridTextColumn();
            _noCuoi.Header = "Số nợ cuối";
            _noCuoi.Binding = new Binding("SoTienNoCuoi");
            DGBaoCaoNo.Columns.Add(_noCuoi);

        }
        void showdgTon()
        {
            DGBaoCaoTon = new DataGrid();
            DGBaoCaoTon.AutoGenerateColumns = false;
            DGBaoCaoTon.IsReadOnly = true;
            Binding b = new Binding("ListCTBaoCaoTon")
            {
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            DGBaoCaoTon.SetBinding(DataGrid.ItemsSourceProperty, b);
            DataGridTextColumn _maSach = new DataGridTextColumn();
            _maSach.Header = "Mã Sách";
            _maSach.Binding = new Binding("MaSach");
            DGBaoCaoTon.Columns.Add(_maSach);

            DataGridTextColumn _NXB = new DataGridTextColumn();
            _NXB.Header = " Nhà Xuất Bản";
            _NXB.Binding = new Binding("Sach.NhaXuatBan.TenNXB");
            DGBaoCaoTon.Columns.Add(_NXB);

            DataGridTextColumn _tonDau = new DataGridTextColumn();
            _tonDau.Header = "Số tồn đầu";
            _tonDau.Binding = new Binding("SoLuongTonDau");
            DGBaoCaoTon.Columns.Add(_tonDau);

            DataGridTextColumn _tonCuoi= new DataGridTextColumn();
            _tonCuoi.Header = "Số tồn đầu";
            _tonCuoi.Binding = new Binding("SoLuongTonCuoi");
            DGBaoCaoTon.Columns.Add(_tonCuoi);
        }
       




    }
}
