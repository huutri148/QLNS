using BookStoreClone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    class QuanLyHeThongViewModel : BaseViewModel
    {
        private ObservableCollection<NguoiDung> _listNV;
        private QuyDinh _selectedQuyDinh;
        private bool _setTaiKhoan;
        private bool _checkCo;
        private bool _checkKhong;
        private bool _nam;
        private bool _nu;
        private bool _admin;
        private bool _quanLyKho;
        private bool _cNV;
        private int _soLuongSachTonToiThieuDeNhap;
        private int _soLuongSachNhapToiThieu;
        private int _tienNoToiDa;
        private int _soLuongSachTonToiThieuSauKhiBan;
        private string _textTimKiemNhanVien;
        private NguoiDung _selectedNhanVien;
        private Visibility _visibilityThayDoiQuyDinh;
        private Visibility _visbilityThemNhanVien;
        private int n = 0;
        private int log;
        public ObservableCollection<NguoiDung> ListNV { get => _listNV; set { _listNV = value; OnPropertyChanged(); } }

        public QuyDinh SelectedQuyDinh { get => _selectedQuyDinh; set { _selectedQuyDinh = value; OnPropertyChanged(); } }

        public bool CheckCo
        {
            get => _checkCo; set
            {
                _checkCo = value; OnPropertyChanged();


            }
        }
        public ICommand LoadChiTietNhanVienCommand { get; set; }
        public ICommand CapNhatQuyDinhCommand { get; set; }
        public ICommand LuuNguoiDungCommand { get; set; }
        public ICommand XoaNhanVienCommand { get; set; }
        public ICommand ShowSuaDoiQuyDinhCommand { get; set; }
        public ICommand ShowThemNhanVienCommand { get; set; }
        public bool CheckKhong { get => _checkKhong; set { _checkKhong = value; OnPropertyChanged(); } }

        public NguoiDung SelectedNhanVien { get => _selectedNhanVien; set { _selectedNhanVien = value; OnPropertyChanged(); } }

        public Visibility VisibilityThayDoiQuyDinh { get => _visibilityThayDoiQuyDinh; set { _visibilityThayDoiQuyDinh = value; OnPropertyChanged(); } }
        public Visibility VisbilityThemNhanVien
        {
            get => _visbilityThemNhanVien; set { _visbilityThemNhanVien = value; OnPropertyChanged(); }
        }

        public bool Nam { get => _nam; set { _nam = value; OnPropertyChanged(); } }
        public bool Nu { get => _nu; set { _nu = value; OnPropertyChanged(); } }

        public bool Admin { get => _admin; set { _admin = value; OnPropertyChanged(); } }
        public bool QuanLyKho { get => _quanLyKho; set { _quanLyKho = value; OnPropertyChanged(); } }
        public bool CNV { get => _cNV; set { _cNV = value; OnPropertyChanged(); } }


        public int TienNoToiDa { get => _tienNoToiDa; set { _tienNoToiDa = value; OnPropertyChanged(); } }

        public int SoLuongSachTonToiThieuDeNhap { get => _soLuongSachTonToiThieuDeNhap; set { _soLuongSachTonToiThieuDeNhap = value; OnPropertyChanged(); } }
        public int SoLuongSachNhapToiThieu { get => _soLuongSachNhapToiThieu; set { _soLuongSachNhapToiThieu = value; OnPropertyChanged(); } }
        public int SoLuongSachTonToiThieuSauKhiBan { get => _soLuongSachTonToiThieuSauKhiBan; set { _soLuongSachTonToiThieuSauKhiBan = value; OnPropertyChanged(); } }

        public string TextTimKiemNhanVien { get => _textTimKiemNhanVien; set { _textTimKiemNhanVien = value; OnPropertyChanged(); TimKiemNhanVien(); } }

        public bool SetTaiKhoan { get => _setTaiKhoan; set { _setTaiKhoan = value; OnPropertyChanged(); } }

        public QuanLyHeThongViewModel()
        {
            SelectedQuyDinh = new QuyDinh();
            int a = DataProvider.Ins.DB.QuyDinhs.Count();
            SelectedQuyDinh = DataProvider.Ins.DB.QuyDinhs.ToList()[a - 1];
            SoLuongSachTonToiThieuDeNhap = SelectedQuyDinh.SoLuongSachTonToiThieuDeNhap;
            SoLuongSachNhapToiThieu = SelectedQuyDinh.SoLuongSachNhapToiThieu;
            SelectedQuyDinh.SoLuongSachTonToiThieuDeNhap = SelectedQuyDinh.SoLuongSachTonToiThieuDeNhap;
            SoLuongSachTonToiThieuSauKhiBan = SelectedQuyDinh.SoLuongSachTonToiThieuSauKhiBan;
            TienNoToiDa = SelectedQuyDinh.TienNoToiDa;


            if (SelectedQuyDinh.DuocThuVuotSoTienKhachHangDangNo == true)
            {
                CheckCo = true;
                CheckKhong = false;
            }
            else
            {
                CheckKhong = true;
                CheckCo = false;
            }


            VisbilityThemNhanVien = Visibility.Collapsed;
            VisibilityThayDoiQuyDinh = Visibility.Collapsed;

            ListNV = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
            XoaNhanVienCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                MessageBoxResult x = MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (x == MessageBoxResult.Yes)
                {
                    DataProvider.Ins.DB.NguoiDungs.Remove(SelectedNhanVien);
                    DataProvider.Ins.DB.SaveChanges();
                    ListNV = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
                }
            });
            ShowThemNhanVienCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                if (n == 1)
                {
                    VisbilityThemNhanVien = Visibility.Collapsed;
                    n = -1;
                    p.IsEnabled = true;
                }
                else
                {
                    n = 1;
                    XulyHien(1);
                    p.IsEnabled = false;
                    log = 1;
                    SelectedNhanVien = new NguoiDung();
                    SelectedNhanVien.NgaySinh = DateTime.Today;
                    SetTaiKhoan = true;
                }
            });
            ShowSuaDoiQuyDinhCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (n == 2)
                {
                    VisibilityThayDoiQuyDinh = Visibility.Collapsed;
                    n = -2;

                }
                else
                {

                    n = 2;
                    XulyHien(2);

                }
            });
            LuuNguoiDungCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {

                if (!(string.IsNullOrEmpty(SelectedNhanVien.TenND) || string.IsNullOrEmpty(SelectedNhanVien.SDT) || string.IsNullOrEmpty(SelectedNhanVien.TenDangNhap) || string.IsNullOrEmpty(SelectedNhanVien.MatKhau)))
                {
                    if (log == 1)
                    {

                        int i = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == SelectedNhanVien.TenDangNhap).Count();
                        if (i == 0)
                        {
                            MessageBoxResult ans = MessageBox.Show("Bạn có chắc muốn thêm người dùng này không?", "Hệ Thống", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            if (ans == MessageBoxResult.Yes)
                            {
                                SelectedNhanVien.Admin = Admin;
                                SelectedNhanVien.NhanVienKho = QuanLyKho;
                                SelectedNhanVien.NhanVienBan = CNV;
                                SelectedNhanVien.MatKhau = MD5Hash(Base64Encode(SelectedNhanVien.MatKhau));
                                if (Nam == true)
                                    SelectedNhanVien.GioiTinh = true;
                                else
                                    SelectedNhanVien.GioiTinh = false;
                                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[NguoiDung] ON");
                                DataProvider.Ins.DB.NguoiDungs.Add(SelectedNhanVien);
                                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT[dbo].[NguoiDung] OFF");
                                DataProvider.Ins.DB.SaveChanges();
                                ListNV = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
                                SelectedNhanVien = null;

                                Nam = false;
                                Nu = false;

                            }
                        }
                        else
                        {
                            MessageBox.Show("Đã có người sử dụng tên tài khoản này", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBoxResult ans = MessageBox.Show("Bạn có chắc muốn sửa thông tin người dùng này không?", "Hệ Thống", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (ans == MessageBoxResult.Yes)
                        {

                            SelectedNhanVien.Admin = Admin;
                            SelectedNhanVien.NhanVienKho = QuanLyKho;
                            SelectedNhanVien.NhanVienBan = CNV;
                            SelectedNhanVien.MatKhau = MD5Hash(Base64Encode(SelectedNhanVien.MatKhau));
                            if (Nam == true)
                                SelectedNhanVien.GioiTinh = true;
                            else
                                SelectedNhanVien.GioiTinh = false;

                            DataProvider.Ins.DB.SaveChanges();
                            ListNV = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
                            SelectedNhanVien = null;
                            Nam = false;
                            Nu = false;
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
            LoadChiTietNhanVienCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                log = 0;
                XulyHien(1);
                SelectedNhanVien.MatKhau = "";
                SelectedNhanVien = p.SelectedItem as NguoiDung;

                n = 1;
                p.IsEnabled = false;
                SetTaiKhoan = false;
                if (SelectedNhanVien.GioiTinh == true)
                {
                    Nam = true;
                    Nu = false;
                }
                else
                {
                    Nu = true;
                    Nam = false;
                }
                Admin = SelectedNhanVien.Admin;
                QuanLyKho = SelectedNhanVien.NhanVienKho;
                CNV = SelectedNhanVien.NhanVienBan;

            });
            CapNhatQuyDinhCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SelectedQuyDinh = new QuyDinh();
                SelectedQuyDinh.SoLuongSachTonToiThieuDeNhap = SoLuongSachTonToiThieuDeNhap;
                SelectedQuyDinh.SoLuongSachTonToiThieuSauKhiBan = SoLuongSachTonToiThieuSauKhiBan;
                SelectedQuyDinh.SoLuongSachNhapToiThieu = SoLuongSachNhapToiThieu;
                SelectedQuyDinh.TienNoToiDa = TienNoToiDa;
                SelectedQuyDinh.NgayCapNhat = DateTime.Now;
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[QuyDinh] ON");
                DataProvider.Ins.DB.QuyDinhs.Add(SelectedQuyDinh);
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT[dbo].[QuyDinh] OFF");
                DataProvider.Ins.DB.SaveChanges();


            });
        }
        void XulyHien(int n)
        {
            if (n == 2)
            {
                VisibilityThayDoiQuyDinh = Visibility.Visible;
                VisbilityThemNhanVien = Visibility.Collapsed;

            }
            if (n == 1)
            {
                VisbilityThemNhanVien = Visibility.Visible;
                VisibilityThayDoiQuyDinh = Visibility.Collapsed;
            }

        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public void TimKiemNhanVien()
        {
            if (TextTimKiemNhanVien == null)
                ListNV = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
            else
                ListNV = new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenND.ToLower().Contains(TextTimKiemNhanVien.ToLower()) || x.SDT.Contains(TextTimKiemNhanVien)));

        }
    }
}
