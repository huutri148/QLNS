using BookStoreClone.Model;
using BookStoreClone.View;
using MaterialDesignThemes.Wpf;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        private Visibility _setQuanLy;
        private Visibility _setKinhDoanh;
        private Visibility _setAdmin;
        private Visibility _setAccount;
        private bool _setpbThongTinTK;
        private bool _nam;
        private bool _nu;
        private NguoiDung _user;
        private string _mKCu;
        private string _mKMoi;
        private string _xNMK;
        private bool _setChucNang;
        private bool flag1 = false, flag2 = true;
        private Visibility _visibilityThongTinTaiKhoan;

        public ICommand CapNhatTKCommand { get; set; }
        public ICommand PasswordCuChangedCommand { get; set; }
        public ICommand PasswordMoiChangedCommand { get; set; }
        public ICommand XNPasswordChangedCommand { get; set; }
        public ICommand TroLaiCommand { get; set; }
        public ICommand ChucNangTKCommand { get; set; }
        public ICommand ThongTinTKCommand { get; set; }
        public ICommand TextChangedCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand DangXuatCommand { get; set; }
        public ICommand CloseApp { get; set; }
        public ICommand Minimize { get; set; }
        public ICommand Maximize { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }

        public ICommand ChonTinhNangCommand { get; set; }
        public ICommand LoadContentCommand { get; set; }

        public PackIconKind Maximize_Icon { get => _maximize_Icon; set { _maximize_Icon = value; OnPropertyChanged(); } }

        public Grid PnlContent { get; set; }

        public string TextTimKiem
        {
            get => _textTimKiem;
            set { _textTimKiem = value; OnPropertyChanged(TextTimKiem); }
        }

        public Visibility SetQuanLy { get => _setQuanLy; set { _setQuanLy = value; OnPropertyChanged(); } }

        public Visibility SetKinhDoanh { get => _setKinhDoanh; set { _setKinhDoanh = value; OnPropertyChanged(); } }
        public Visibility SetAdmin { get => _setAdmin; set { _setAdmin = value; OnPropertyChanged(); } }

        public bool SetpbThongTinTK { get => _setpbThongTinTK; set { _setpbThongTinTK = value; OnPropertyChanged(); } }

        public NguoiDung User { get => _user; set { _user = value; OnPropertyChanged(); } }

        public bool Nam { get => _nam; set { _nam = value; OnPropertyChanged(); } }
        public bool Nu { get => _nu; set { _nu = value; OnPropertyChanged(); } }

        public Visibility SetAccount { get => _setAccount; set { _setAccount = value; OnPropertyChanged(); } }

        public bool SetChucNang { get => _setChucNang; set { _setChucNang = value; OnPropertyChanged(); } }

        public Visibility VisibilityThongTinTaiKhoan { get => _visibilityThongTinTaiKhoan; set { _visibilityThongTinTaiKhoan = value; OnPropertyChanged(); } }

        public string MKCu { get => _mKCu; set { _mKCu = value; OnPropertyChanged(); } }
        public string MKMoi { get => _mKMoi; set { _mKMoi = value; OnPropertyChanged(); } }
        public string XNMK { get => _xNMK; set { _xNMK = value; OnPropertyChanged(); } }

        public string TitleApp { get => _titleApp; set { _titleApp = value; OnPropertyChanged(); } }

        private PackIconKind _maximize_Icon;
        private string _textTimKiem;

        string _titleApp;
        public MainViewModel()
        {

            Maximize_Icon = PackIconKind.WindowMaximize;
            TextTimKiem = "";
            SetpbThongTinTK = false;
            MKCu = "";

            DangXuatCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                LoginWindow login = new LoginWindow();
                login.Show();
                p.Close();
            });
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                //p.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

                //if (p == null)
                //    return;
                //p.Hide();
                
                //LoginWindow loginWindow = new LoginWindow();
                //loginWindow.ShowDialog();
                //if (loginWindow.DataContext == null)
                //    return;

                //var loginVM = loginWindow.DataContext as LoginViewModel;
                if (LoginViewModel.IsLogin)
                {
                    p.Show();
                    string a = Const.IDNguoiDung;
                    User = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == a).First();
                    if (User.NhanVienKho)
                        PQQuanLy();
                    else if (User.NhanVienBan)
                        PQKinhDoanh();
                    else if (User.Admin)
                        PQAdmin();

                    if (User.GioiTinh == true)
                    {
                        Nam = true;
                        Nu = false;

                    }
                    else
                    {
                        Nam = false;
                        Nu = true;
                    }

                }
                else
                {
                    p.Show();
                    PQKhachHang();
                }
            });

            TextChangedCommand = new RelayCommand<TreeView>((p) => { if (p != null) return true; return false; }, (p) =>
            {
                for (int i = 0; i < p.Items.Count; i++)
                {
                    for (int j = 0; j < (p.Items[i] as TreeViewItem).Items.Count; j++)
                    {
                        TreeViewItem treeViewItem = (p.Items[i] as TreeViewItem).Items[j] as TreeViewItem;
                        if (TextTimKiem == "")
                            for (int k = 0; k < treeViewItem.Items.Count; k++)
                            {
                                treeViewItem.IsExpanded = false;
                            }
                        else if (treeViewItem.Header.ToString().ToLower().Contains(TextTimKiem.ToLower()))

                            for (int k = 0; k < treeViewItem.Items.Count; k++)
                            {
                                TreeViewItem treeViewItem1 = treeViewItem.Items[k] as TreeViewItem;
                                treeViewItem1.Visibility = Visibility.Visible;
                                treeViewItem.IsExpanded = true;
                            }
                        else
                        {
                            int count1 = 0;
                            for (int k = 0; k < treeViewItem.Items.Count; k++)
                            {
                                TreeViewItem treeViewItem1 = treeViewItem.Items[k] as TreeViewItem;
                                if (treeViewItem1.Header.ToString().ToLower().Contains(TextTimKiem.ToLower()))
                                {
                                    count1++;
                                    treeViewItem1.Visibility = Visibility.Visible;
                                }
                                else treeViewItem1.Visibility = Visibility.Collapsed;
                            }
                            if (count1 > 0)
                                treeViewItem.IsExpanded = true;
                            else treeViewItem.IsExpanded = false;
                        }
                    }
                }
            }
             );

            CloseApp = new RelayCommand<Window>((p) => { if (p != null) return true; return false; }, (p) =>
            {
                p.Close();
            }
            );

            Maximize = new RelayCommand<Window>((p) => { if (p != null) return true; return false; }, (p) =>
            {
                if (p.WindowState == WindowState.Normal)
                {
                    Maximize_Icon = PackIconKind.DockWindow;
                    p.WindowState = WindowState.Maximized;
                }
                else
                {
                    Maximize_Icon = PackIconKind.WindowMaximize;
                    p.WindowState = WindowState.Normal;
                    TextTimKiem = "adadasdas";
                }
            }
            );

            Minimize = new RelayCommand<Window>((p) => { if (p != null) return true; return false; }, (p) =>
            {
                p.WindowState = WindowState.Minimized;
            }
            );
            MouseMoveWindowCommand = new RelayCommand<Window>((p) => { if (p != null) return true; return false; }, (p) =>
            {
                p.DragMove();
            }
           );
            ChonTinhNangCommand = new RelayCommand<TreeView>((a) => { if (a != null) return true; return false; }, (a) =>
            {
                FrameworkElement tmp = a;

                while (!(tmp is Expander))
                {
                    tmp = tmp.Parent as FrameworkElement;
                }
                (tmp as Expander).IsExpanded = false;

                switch ((a.SelectedItem as TreeViewItem).Header.ToString())
                {
                    case "Tìm kiếm sách":
                        using (null)
                        
                            PnlContent.Children.Clear();
                            PnlContent.Children.Add(new TimKiemSachUC() { });
                            TitleApp = "Tìm kiếm sách";
                        
                        break;

                    case "Dữ Liệu Sách":
                        
                            PnlContent.Children.Clear();
                            PnlContent.Children.Add(new QuanLyDuLieuSach() { });
                            TitleApp = "Dữ Liệu Sách";
                    
                        break;

                    case "Khách Hàng":
                        PnlContent.Children.Clear();
                        PnlContent.Children.Add(new QuanLyKhachHangUC() { });
                        TitleApp = "Khách Hàng";
                        break;

                    case "Nhập Sách":
                        PnlContent.Children.Clear();
                        PnlContent.Children.Add(new QuanLyNhapSachUC() { });
                        TitleApp = "Nhập Sách";
                        break;

                    case "Lập Hóa Đơn Bán Sách":
                        PnlContent.Children.Clear();
                        PnlContent.Children.Add(new QuanLyHoaDonUC() { });
                        TitleApp = "Lập Hóa Đơn Bán Sách";
                        break;

                    case "Lập Phiếu Thu Tiền":
                        PnlContent.Children.Clear();
                        PnlContent.Children.Add(new PhieuThuTienUC());
                        TitleApp = "Lập Phiếu Thu Tiền";
                        break;

                    case "Báo Cáo":
                        PnlContent.Children.Clear();
                        PnlContent.Children.Add(new BaoCaoUC());
                        TitleApp = "Báo Cáo Tồn - Nợ ";
                        break;

                    

                    case "Quản Lý Hệ Thống":
                        PnlContent.Children.Clear();
                        PnlContent.Children.Add(new QuanLyHeThongVM());
                        TitleApp = "Quản Lý Hệ Thống";
                        break;
                }
            }
            );

            LoadContentCommand = new RelayCommand<Grid>((p) => { if (p != null) return true; return false; }, (p) =>
            {
                PnlContent = p;
                p.Children.Add(new TimKiemSachUC());
                TitleApp = "Tìm kiếm sách";
            }

        );
            ChucNangTKCommand = new RelayCommand<PopupBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.IsPopupOpen = true;
            });
            ThongTinTKCommand = new RelayCommand<PopupBox>((p) => { return p == null ? false : true; }, (p) =>
            {
                p.IsPopupOpen = true;
                p.StaysOpen = true;
                SetpbThongTinTK = false;
            });
            TroLaiCommand = new RelayCommand<PopupBox>((p) => { return p == null ? false : true; }, (p) =>
            {

                p.IsPopupOpen = false;
                SetChucNang = true;

            });
            PasswordCuChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { MKCu = p.Password; });
            PasswordMoiChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                MKMoi = p.Password;
            });
            XNPasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) =>
            {
                XNMK = p.Password;
            });
            CapNhatTKCommand = new RelayCommand<PopupBox>((p) => { return true; }, (p) =>
            {
                if (Nam == false)
                    User.GioiTinh = false;
                else
                    User.GioiTinh = true;

                if (!string.IsNullOrEmpty(MKCu))
                {
                    string passEncode = MD5Hash(Base64Encode(MKCu));
                    if (passEncode == User.MatKhau)
                    {
                        if (!string.IsNullOrEmpty(MKMoi) && !string.IsNullOrEmpty(XNMK))
                        {
                            if (MKMoi == XNMK)
                            {
                                flag2 = true;
                                flag1 = true;
                                User.MatKhau = MD5Hash(Base64Encode(MKMoi));
                            }
                        }
                        else
                        {
                            flag1 = false;
                            MessageBox.Show("Nhập mật khẩu mới", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        flag1 = false;
                        MessageBox.Show("Mật khẩu cũ không khớp", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    flag1 = true;
                }


                if (flag1 == true || (flag1 == true && flag2 == true))
                {


                    DataProvider.Ins.DB.SaveChanges();
                }

            });
        }
        void PQQuanLy()
        {
            SetAdmin = Visibility.Collapsed;
            SetKinhDoanh = Visibility.Visible;
            SetQuanLy = Visibility.Visible;
            VisibilityThongTinTaiKhoan = Visibility.Visible;
        }
        void PQKinhDoanh()
        {
            SetAdmin = Visibility.Collapsed;
            SetKinhDoanh = Visibility.Visible;
            SetQuanLy = Visibility.Collapsed;
            VisibilityThongTinTaiKhoan = Visibility.Visible;
        }
        void PQAdmin()
        {
            SetAdmin = Visibility.Visible;
            SetKinhDoanh = Visibility.Visible;
            SetQuanLy = Visibility.Visible;
            VisibilityThongTinTaiKhoan = Visibility.Visible;
        }
        void PQKhachHang()
        {
            SetAdmin = Visibility.Collapsed;
            SetKinhDoanh = Visibility.Collapsed;
            SetQuanLy = Visibility.Collapsed;
            VisibilityThongTinTaiKhoan = Visibility.Collapsed;
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
    }
}