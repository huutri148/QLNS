namespace BookStoreClone.ViewModel
{
    using BookStoreClone.Model;
    using MaterialDesignThemes.Wpf;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    internal class QuanLyDuLieuSachViewModel : BaseViewModel
    {
        #region Modal

        private string _localLink = System.Reflection.Assembly.GetExecutingAssembly().Location.Remove(System.Reflection.Assembly.GetExecutingAssembly().Location.IndexOf(@"bin\Debug"));

        private ObservableCollection<Sach> _listSach;
        private ObservableCollection<NhaXuatBan> _listNXB;

        private ObservableCollection<TacGia> _listTacGia;

        private ObservableCollection<TheLoai> _listTheLoai;

        public ObservableCollection<Sach> ListSach
        {
            get => _listSach; set { _listSach = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TacGia> ListTacGia
        {
            get => _listTacGia; set { _listTacGia = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TheLoai> ListTheLoai
        {
            get => _listTheLoai; set { _listTheLoai = value; OnPropertyChanged();  }
        }

        public ObservableCollection<NhaXuatBan> ListNXB { get => _listNXB; set { _listNXB = value; OnPropertyChanged(); } }

        #endregion Modal

        #region Tác giả

        private string _textTimKiemTacGia;
        private string _themTacGia;
        private WrapPanel _wrapPanelTacGia;
        public DataGrid DataTacGia { get; set; }

        public string TextThemTacGia { get => _themTacGia; set { _themTacGia = value; OnPropertyChanged(); } }
        public string TextTimKiemTacGia { get => _textTimKiemTacGia; set { _textTimKiemTacGia = value; OnPropertyChanged(); TimKiemTacGia(); } }
        public WrapPanel WrapPanelTacGia { get => _wrapPanelTacGia; set => _wrapPanelTacGia = value; }
        public ICommand LoadPanelTacGiaCommand { get; set; }
        public ICommand XoaTacGiaCommand { get; set; }
        public ICommand AddTacGiaVaoSachCommand { get; set; }
        public ICommand ThemTacGiaCommand { set; get; }

        public ICommand ShowListTacGiaCommand { get; set; }
        public ICommand LoadListTacGia { get; set; }

        #endregion Tác giả

        #region Nhà xuất bản

        public ICommand ThemNhaXBCommand { get; set; }
        public ICommand ShowListNXBCommamnd { get; set; }
        public ICommand LoadListNXB { get; set; }
        public ICommand AddNhaXBVaoSachCommand { get; set; }
        public ICommand XoaNhaXBCommand { get; set; }

        private string _textTimKiemNhaXB;
        private Visibility _visibilityNXB;
        private DataGrid _dataNXB;
        private NhaXuatBan _selectedNhaXB;
        private string _TextThemNhaXB;
        public string TextTimKiemNhaXB { get => _textTimKiemNhaXB; set { _textTimKiemNhaXB = value; OnPropertyChanged(); TimKiemNhaXB(); } }
        public string TextThemNhaXB { get => _TextThemNhaXB; set { _TextThemNhaXB = value; OnPropertyChanged(); } }

        public Visibility VisibilityNXB { get => _visibilityNXB; set { _visibilityNXB = value; OnPropertyChanged(); } }
        public DataGrid DataNXB { get => _dataNXB; set { _dataNXB = value; OnPropertyChanged(); } }
        public NhaXuatBan SelectedNhaXB { get => _selectedNhaXB; set { _selectedNhaXB = value; OnPropertyChanged(); } }

        #endregion Nhà xuất bản

        #region Sách

        private Sach _selectedSach;
        private Visibility _isEnableButtonThemSachMoi;
        private string _textContentButton;
        private string _tenSach;
        private string _moTaSach;
        private string _anhBia;
        private string _trangThai;
        private string _textTimKiemSach;
        private List<TacGia> _Wrap_ListTacGia;
        private List<TheLoai> _Wrap_ListTheLoai;

        public string TextTenSach { get => _tenSach; set { _tenSach = value; OnPropertyChanged(); } }
        public string TextMoTaSach { get => _moTaSach; set { _moTaSach = value; OnPropertyChanged(); } }
        public string LinkAnhBia { get => _anhBia; set { _anhBia = value; OnPropertyChanged(); } }
        public string TextTrangThai { get => _trangThai; set { _trangThai = value; OnPropertyChanged(); TextContentButton = TextTrangThai == "Cập nhật sách" ? "Lưu thay đổi" : "Lưu sách mới"; VisibilityButtonThemSachMoi = TextTrangThai == "Cập nhật sách" ? Visibility.Visible : Visibility.Collapsed; } }
        public ICommand ChonAnhCommmand { get; set; }
        public ICommand LuuSachCommand { get; set; }

        public ICommand LoadChiTietSachCommand { set; get; }
        public ICommand ResetTaoSachCommand { set; get; }

        public string TextContentButton { get => _textContentButton; set { _textContentButton = value; OnPropertyChanged(); } }

        public Visibility VisibilityButtonThemSachMoi { get => _isEnableButtonThemSachMoi; set { _isEnableButtonThemSachMoi = value; OnPropertyChanged(); } }

        public List<TacGia> Wrap_ListTacGia { get => _Wrap_ListTacGia; set { _Wrap_ListTacGia = value; OnPropertyChanged(); } }
        public List<TheLoai> Wrap_ListTheLoai { get => _Wrap_ListTheLoai; set { _Wrap_ListTheLoai = value; OnPropertyChanged(); } }

        public string TextTimKiemSach { get => _textTimKiemSach; set { _textTimKiemSach = value; TimKiemSach(); } }

        public ICommand CellChange { get; set; }

        #endregion Sách

        #region Thể loại

        private string _textTimKiemTheLoai;
        private string _themTheLoai;
        private WrapPanel _wrapPanelTheloai;
        public DataGrid DataTheLoai { get; set; }
        public string TextTimKiemTheLoai { get => _textTimKiemTheLoai; set { _textTimKiemTheLoai = value; OnPropertyChanged(); TimKiemTheLoai(); } }
        public string TextThemTheLoai { get => _themTheLoai; set { _themTheLoai = value; OnPropertyChanged(); } }
        public WrapPanel WrapPanelTheloai { get => _wrapPanelTheloai; set => _wrapPanelTheloai = value; }
        public ICommand LoadPanelTheLoaiCommand { get; set; }
        public ICommand XoaTheLoaiCommand { get; set; }
        public ICommand AddTheLoaiVaoSachCommand { get; set; }
        public ICommand ThemTheLoaiCommand { set; get; }
        public ICommand ShowListTheLoaiCommand { get; set; }
        public ICommand LoadListTheLoai { get; set; }

        #endregion Thể loại

        #region Giao diện

        public Visibility VisibilityButtonTheLoai { get => _visibilityButtonTheLoai; set { _visibilityButtonTheLoai = value; OnPropertyChanged(); } }
        public Visibility VisibilityCardTheLoai { get => _visibilityCardTheLoai; set { _visibilityCardTheLoai = value; OnPropertyChanged(); } }
        public bool ShowExpanderTheLoai { get => _showExpanderTheLoai; set { _showExpanderTheLoai = value; OnPropertyChanged(); if (ShowExpanderTheLoai == false) { VisibilityCardTheLoai = Visibility.Collapsed; VisibilityButtonTheLoai = Visibility.Visible; } } }
        public Visibility VisibilityButtonTacGia { get => _visibilityButtonTacGia; set { _visibilityButtonTacGia = value; OnPropertyChanged(); } }
        public Visibility VisibilityCardTacGia { get => _visibilityCardTacGia; set { _visibilityCardTacGia = value; OnPropertyChanged(); } }
        public bool ShowExpanderTacGia { get => _showExpanderTacGia; set { _showExpanderTacGia = value; OnPropertyChanged(); if (ShowExpanderTacGia == false) { VisibilityCardTacGia = Visibility.Collapsed; VisibilityButtonTacGia = Visibility.Visible; } } }
        public Visibility VisibilityButtonNhaXB { get => _visibilityButtonNhaXB; set { _visibilityButtonNhaXB = value; OnPropertyChanged(); } }
        public Visibility VisibilityCardNhaXB { get => _visibilityCardNhaXB; set { _visibilityCardNhaXB = value; OnPropertyChanged(); } }
        public bool ShowExpanderNhaXB { get => _showExpanderNhaXB; set { _showExpanderNhaXB = value; OnPropertyChanged(); if (ShowExpanderNhaXB == false) { VisibilityCardNhaXB = Visibility.Collapsed; VisibilityButtonNhaXB = Visibility.Visible; } } }

        private Visibility _visibilityButtonTheLoai;
        private Visibility _visibilityCardTheLoai;
        private bool _showExpanderTheLoai;
        private Visibility _visibilityButtonTacGia;
        private Visibility _visibilityCardTacGia;
        private bool _showExpanderTacGia;
        private Visibility _visibilityButtonNhaXB;
        private Visibility _visibilityCardNhaXB;
        private bool _showExpanderNhaXB;

        private void CaiDatGiaoDien(int x)
        {
            switch (x)
            {
                case 0:
                    {
                        VisibilityButtonTheLoai = Visibility.Visible;
                        VisibilityButtonTacGia = Visibility.Visible;
                        VisibilityButtonNhaXB = Visibility.Visible;

                        VisibilityCardNhaXB = Visibility.Collapsed;
                        VisibilityCardTacGia = Visibility.Collapsed;
                        VisibilityCardTheLoai = Visibility.Collapsed;

                        ShowExpanderNhaXB = false;
                        ShowExpanderTacGia = false;
                        ShowExpanderTheLoai = false;
                        break;
                    }
                case 1:
                    {
                        VisibilityButtonTheLoai = Visibility.Collapsed;
                        VisibilityButtonTacGia = Visibility.Visible;
                        VisibilityButtonNhaXB = Visibility.Visible;

                        VisibilityCardNhaXB = Visibility.Collapsed;
                        VisibilityCardTacGia = Visibility.Collapsed;
                        VisibilityCardTheLoai = Visibility.Visible;

                        ShowExpanderNhaXB = false;
                        ShowExpanderTacGia = false;
                        ShowExpanderTheLoai = true;
                        break;
                    }
                case 2:
                    {
                        VisibilityButtonTheLoai = Visibility.Visible;
                        VisibilityButtonTacGia = Visibility.Collapsed;
                        VisibilityButtonNhaXB = Visibility.Visible;

                        VisibilityCardNhaXB = Visibility.Collapsed;
                        VisibilityCardTacGia = Visibility.Visible;
                        VisibilityCardTheLoai = Visibility.Collapsed;

                        ShowExpanderNhaXB = false;
                        ShowExpanderTacGia = true;
                        ShowExpanderTheLoai = false;
                        break;
                    }
                case 3:
                    {
                        VisibilityButtonTheLoai = Visibility.Visible;
                        VisibilityButtonTacGia = Visibility.Visible;
                        VisibilityButtonNhaXB = Visibility.Collapsed;

                        VisibilityCardNhaXB = Visibility.Visible;
                        VisibilityCardTacGia = Visibility.Collapsed;
                        VisibilityCardTheLoai = Visibility.Collapsed;

                        ShowExpanderNhaXB = true;
                        ShowExpanderTacGia = false;
                        ShowExpanderTheLoai = false;
                        break;
                    }
            }
        }

        #endregion Giao diện

        public QuanLyDuLieuSachViewModel()
        {
            #region Giao diện

            CaiDatGiaoDien((int)0);

            ShowListTheLoaiCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
           {
               CaiDatGiaoDien(1);
           });
            ShowListNXBCommamnd = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CaiDatGiaoDien(3);
            });
            ShowListTacGiaCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CaiDatGiaoDien(2);
            });

            #endregion Giao diện

            #region Thể loại

            TextTimKiemTheLoai = "";
            Wrap_ListTheLoai = new List<TheLoai>();
            ListTheLoai = new ObservableCollection<TheLoai>(DataProvider.Ins.DB.TheLoais);
            XoaTheLoaiCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                for (int i = Wrap_ListTheLoai.Count - 1; i >= 0; i--)
                {
                    if (Wrap_ListTheLoai[i].MaTL == (p.SelectedItem as TheLoai).MaTL)
                        Wrap_ListTheLoai.Remove(Wrap_ListTheLoai[i]);
                }
                CapNhatChip_TheLoai();
                DataProvider.Ins.DB.TheLoais.Remove(p.SelectedItem as TheLoai);
                DataProvider.Ins.DB.SaveChanges();
                TextTimKiemTheLoai = "";
            });
            LoadListTheLoai = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                DataTheLoai = p;
            });

            ThemTheLoaiCommand = new RelayCommand<Button>((p) =>
            {
                if (p == null) return false;
                p.IsEnabled = false;
                if (TextThemTheLoai == null) return false;
                if (TextThemTheLoai == "") return false;
                if (ListTheLoai.Where(x => x.TenTL == TextThemTheLoai).ToList().Count > 0)
                    return false;
                p.IsEnabled = true;
                return true;
            }, (p) =>{
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TheLoai] ON");
                DataProvider.Ins.DB.TheLoais.Add(new TheLoai() { TenTL = TextThemTheLoai });
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TheLoai] OFF");
                DataProvider.Ins.DB.SaveChanges();

                TextThemTheLoai = "";
                TextTimKiemTheLoai = "";
            });

            LoadPanelTheLoaiCommand = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                WrapPanelTheloai = p;
            });

            AddTheLoaiVaoSachCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                for (int i = 0; i < Wrap_ListTheLoai.Count; i++)
                    if (Wrap_ListTheLoai[i].MaTL == (p.SelectedItem as TheLoai).MaTL)
                        return;
                Wrap_ListTheLoai.Add(p.SelectedItem as TheLoai);
                CapNhatChip_TheLoai();

                
            });

            #endregion Thể loại

            #region Tác giả

            TextTimKiemTacGia = "";
            Wrap_ListTacGia = new List<TacGia>();
            LoadListTacGia = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                DataTacGia = p;
            });
            ListTacGia = new ObservableCollection<TacGia>(DataProvider.Ins.DB.TacGias);
            XoaTacGiaCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                for (int i = Wrap_ListTacGia.Count - 1; i >= 0; i--)
                {
                    if (Wrap_ListTacGia[i].MaTG == (p.SelectedItem as TacGia).MaTG)
                        Wrap_ListTacGia.Remove(Wrap_ListTacGia[i]);
                }
                CapNhatChip_TacGia();
                DataProvider.Ins.DB.TacGias.Remove(p.SelectedItem as TacGia);
                DataProvider.Ins.DB.SaveChanges();
                TextTimKiemTacGia = "";
            });
            ThemTacGiaCommand = new RelayCommand<Button>((p) =>
            {
                if (p == null) return false;
                p.IsEnabled = false;
                if (TextThemTacGia == null) return false;
                if (TextThemTacGia == "") return false;
                if (ListTacGia.Where(x => x.TenTG == TextThemTacGia).ToList().Count > 0)
                    return false;
                p.IsEnabled = true;
                return true;
            }, (p) =>{
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TacGia] ON");
                DataProvider.Ins.DB.TacGias.Add(new TacGia() { TenTG = TextThemTacGia });
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[TacGia] OFF");
                DataProvider.Ins.DB.SaveChanges();

                TextTimKiemTacGia = "";
                TextThemTacGia = "";
            });
            LoadPanelTacGiaCommand = new RelayCommand<WrapPanel>((p) => { return true; }, (p) =>
            {
                WrapPanelTacGia = p;
            });
            AddTacGiaVaoSachCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
             {
                 for (int i = 0; i < Wrap_ListTacGia.Count; i++)
                     if (Wrap_ListTacGia[i].MaTG == (p.SelectedItem as TacGia).MaTG)
                         return;
                 Wrap_ListTacGia.Add(p.SelectedItem as TacGia);
                 CapNhatChip_TacGia();
             });

            #endregion Tác giả

            #region Sách

            LinkAnhBia = _localLink + @"Resources\img\" + "BookNull.png";
            TextTrangThai = "Tạo sách mới";
            TextTimKiemSach = "";
            TimKiemSach();
            LoadChiTietSachCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                TextTrangThai = "Cập nhật sách";

                Sach sach = p.SelectedItem as Sach;
                _selectedSach = p.SelectedItem as Sach;
                TextTenSach = sach.TenSach;
                TextMoTaSach = sach.MoTa;

                SelectedNhaXB = sach.NhaXuatBan;

                LinkAnhBia = _localLink + @"Resources\img\" + sach.AnhBia;

                Wrap_ListTacGia = sach.TacGias.ToList();
                CapNhatChip_TacGia();

                Wrap_ListTheLoai = sach.TheLoais.ToList();
                CapNhatChip_TheLoai();
            });
            CellChange = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
               
                    CapNhatChip_TheLoai();
                    CapNhatChip_TacGia();
                    DataProvider.Ins.DB.SaveChanges();

            });
            ChonAnhCommmand = new RelayCommand<Image>((p) => { return true; }, (p) =>
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
                if (open.ShowDialog() == true)
                {
                    LinkAnhBia = open.FileName;
                };
            });
            LuuSachCommand = new RelayCommand<Button>((p) =>
            {
                if (p == null) return false;
                p.IsEnabled = false;
                if (SelectedNhaXB == null) return false;
                if (TextTrangThai != "Cập nhật sách")
                {
                    if (TextTenSach != null && TextTenSach != "" && ListSach.Where(x => x.TenSach == TextTenSach).ToList().Count == 0)
                    {
                        p.IsEnabled = true;
                        return true;
                    }
                }
                else
                {
                    if (TextTenSach != null && TextTenSach != "")
                    {
                        p.IsEnabled = true;
                        return true;
                    }
                }
                return false;
            }, (p) =>
                {
                    Random random = new Random();
                    if (TextTrangThai != "Cập nhật sách")
                    {
                        Sach sach = new Sach() { TenSach = TextTenSach, MoTa = TextMoTaSach };
                        for (int i = 1; i < WrapPanelTacGia.Children.Count - 1; i++)
                        {
                            int index = (WrapPanelTacGia.Children[i] as Chip).TabIndex;
                            TacGia tacGia = (DataProvider.Ins.DB.TacGias.Where(x => x.MaTG == index)).ToList()[0];
                            sach.TacGias.Add(tacGia);
                        }

                        for (int i = 1; i < WrapPanelTheloai.Children.Count - 1; i++)
                        {
                            int index = (WrapPanelTheloai.Children[i] as Chip).TabIndex;
                            TheLoai theLoai = (DataProvider.Ins.DB.TheLoais.Where(x => x.MaTL == index)).ToList()[0];
                            sach.TheLoais.Add(theLoai);
                        }

                        sach.NhaXuatBan = SelectedNhaXB;
                        sach.SoLuongTon = 0;
                        sach.DonGia = 0;
                        DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sach] ON");
                        DataProvider.Ins.DB.Saches.Add(sach);
                        DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Sach] OFF");

                        DataProvider.Ins.DB.SaveChanges();
                        while (true)
                            try
                            {
                                sach.AnhBia = "book" + sach.MaSach.ToString() + "_" + random.Next(0, 100000).ToString() + ((LinkAnhBia.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                                File.Copy(LinkAnhBia, _localLink + @"Resources\img\" + sach.AnhBia, true);
                                break;
                            }
                            catch { }
                        TimKiemSach();
                    }
                    else
                    {
                        Sach sach = _selectedSach;
                        sach.TacGias.Clear();
                        for (int i = 1; i < WrapPanelTacGia.Children.Count - 1; i++)
                        {
                         

                            int index = (WrapPanelTacGia.Children[i] as Chip).TabIndex;
                            TacGia tacGia = (DataProvider.Ins.DB.TacGias.Where(x => x.MaTG == index)).ToList()[0];
                            sach.TacGias.Add(tacGia);
                        }

                        sach.TheLoais.Clear();
                        for (int i = 1; i < WrapPanelTheloai.Children.Count - 1; i++)
                        {
                        

                            int index = (WrapPanelTheloai.Children[i] as Chip).TabIndex;
                            TheLoai theLoai = (DataProvider.Ins.DB.TheLoais.Where(x => x.MaTL == index)).ToList()[0];
                            sach.TheLoais.Add(theLoai);
                        }

                        sach.TenSach = TextTenSach;
                        sach.MoTa = TextMoTaSach;

                        sach.NhaXuatBan = DataProvider.Ins.DB.NhaXuatBans.Where(x => x.MaNXB == SelectedNhaXB.MaNXB).First();

                        while (true)
                            try
                            {
                                sach.AnhBia = "book" + sach.MaSach.ToString() + "_" + random.Next(0, 100000).ToString() + ((LinkAnhBia.Contains(".jpg")) ? ".jpg" : ".png").ToString();
                                File.Copy(LinkAnhBia, _localLink + @"Resources\img\" + sach.AnhBia, true);
                                break;
                            }
                            catch { }

                        DataProvider.Ins.DB.SaveChanges();

                        ListSach = new ObservableCollection<Sach>(DataProvider.Ins.DB.Saches);
                    }

                    SelectedNhaXB = null;

                    TextTenSach = "";
                    TextMoTaSach = "";
                    while (WrapPanelTacGia.Children.Count > 2) WrapPanelTacGia.Children.RemoveAt(1);
                    while (WrapPanelTheloai.Children.Count > 2) WrapPanelTheloai.Children.RemoveAt(1);
                    TextTrangThai = "Tạo sách mới";
                    LinkAnhBia = _localLink + @"Resources\img\" + "BookNull.png";
                });
            ResetTaoSachCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                TextTenSach = "";
                TextMoTaSach = "";
                while (WrapPanelTacGia.Children.Count > 2) WrapPanelTacGia.Children.RemoveAt(1);
                while (WrapPanelTheloai.Children.Count > 2) WrapPanelTheloai.Children.RemoveAt(1);
                Wrap_ListTacGia.Clear();
                Wrap_ListTheLoai.Clear();
               
                TextTrangThai = "Tạo sách mới";
                LinkAnhBia = _localLink + @"Resources\img\" + "BookNull.png";
            });

            #endregion Sách

            #region Nhà xuất bản

            TextThemNhaXB = "";
            ListNXB = new ObservableCollection<NhaXuatBan>(DataProvider.Ins.DB.NhaXuatBans);
            LoadListNXB = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                DataNXB = p;
            });
            XoaNhaXBCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                //DataProvider.Ins.DB.NhaXuatBans.Remove(DataNXB.SelectedItem as NhaXuatBan);
                //DataProvider.Ins.DB.SaveChanges();

                //TimKiemNhaXB();
            });
            ThemNhaXBCommand = new RelayCommand<Button>((p) =>
            {
                if (p == null) return false;
                p.IsEnabled = false;
                if (TextThemNhaXB == null) return false;
                if (TextThemNhaXB == "") return false;
                if (DataProvider.Ins.DB.NhaXuatBans.Where(x => x.TenNXB.ToLower() == TextThemNhaXB.ToLower()).Count() > 0) return false;

                p.IsEnabled = true;
                return true;
            },

                (p) =>
                {
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[NhaXuatBan] ON");
                    DataProvider.Ins.DB.NhaXuatBans.Add(new NhaXuatBan() { TenNXB = TextThemNhaXB });
                    DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[NhaXuatBan] OFF");
                    DataProvider.Ins.DB.SaveChanges();
                    TextThemNhaXB = "";
                    TextTimKiemNhaXB = "";
                    TimKiemNhaXB();
                });

            AddNhaXBVaoSachCommand = new RelayCommand<Button>((p) =>
            {
                return true;
            }, (p) =>
            {
                SelectedNhaXB = DataNXB.SelectedItem as NhaXuatBan;
            });

            #endregion Nhà xuất bản
        }

        #region Xóa chip

        private void Chip_DeleteClickTacGia(object sender, RoutedEventArgs e)
        {
            
            for (int i = 0; i < Wrap_ListTacGia.Count; i++)
                if (Wrap_ListTacGia[i].TenTG.ToString() == (sender as Chip).Content.ToString())
                {
                    Wrap_ListTacGia.RemoveAt(i);
                    break;
                }
            CapNhatChip_TacGia();
        }

        private void Chip_DeleteClickTheLoai(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Wrap_ListTheLoai.Count; i++)
                if (Wrap_ListTheLoai[i].TenTL.ToString() == (sender as Chip).Content.ToString())
                {
                    Wrap_ListTheLoai.RemoveAt(i);
                    break;
                }
            CapNhatChip_TheLoai();
        }

        #endregion Xóa chip

        #region Tìm Kiếm

        public void TimKiemSach()
        {
            if (TextTimKiemSach == "")
                ListSach = new ObservableCollection<Sach>(DataProvider.Ins.DB.Saches);
            else
            {
                ListSach = new ObservableCollection<Sach>(DataProvider.Ins.DB.Saches.Where(x => x.TenSach.ToLower().Contains(TextTimKiemSach.ToLower())));
            }
            for (int i = 0; i < ListSach.Count; i++)
            {
                if (ListSach[i].SoLuongTon > Const.QuyDinh_TonToiThieuSauKhiBan)
                    ListSach[i].IsBtnBanSach = true;
                else ListSach[i].IsBtnBanSach = false;

                if (ListSach[i].SoLuongTon <= Const.QuyDinh_SoLuongSachTonToiThieuDeNhap)
                    ListSach[i].IsCoTheNhapThem = true;
                else ListSach[i].IsCoTheNhapThem = false;
            }
        }

        internal void TimKiemNhaXB()
        {
            if (TextTimKiemNhaXB == "")
                ListNXB = new ObservableCollection<NhaXuatBan>(DataProvider.Ins.DB.NhaXuatBans);
            else ListNXB = new ObservableCollection<NhaXuatBan>(DataProvider.Ins.DB.NhaXuatBans.Where(x => x.TenNXB.ToLower().Contains(TextTimKiemNhaXB.ToLower())));
        }

        internal void TimKiemTacGia()
        {
            if (TextTimKiemTacGia == "")
                ListTacGia = new ObservableCollection<TacGia>(DataProvider.Ins.DB.TacGias);
            else
                ListTacGia = new ObservableCollection<TacGia>(DataProvider.Ins.DB.TacGias.Where(x => x.TenTG.ToLower().Contains(TextTimKiemTacGia.ToLower())));
        }

        internal void TimKiemTheLoai()
        {
            if (TextTimKiemTheLoai == "")
                ListTheLoai = new ObservableCollection<TheLoai>(DataProvider.Ins.DB.TheLoais);
            else
                ListTheLoai = new ObservableCollection<TheLoai>(DataProvider.Ins.DB.TheLoais.Where(x => x.TenTL.ToLower().Contains(TextTimKiemTheLoai.ToLower())));
        }

        #endregion Tìm Kiếm

        #region Cập nhật chip

        private void CapNhatChip_TacGia()
        {
            while (WrapPanelTacGia.Children.Count > 2) WrapPanelTacGia.Children.RemoveAt(1);

            for (int i = 0; i < Wrap_ListTacGia.Count; i++)
            {
               
                Chip chip = new Chip()
                {
                    TabIndex = Wrap_ListTacGia[i].MaTG,
                    Content = Wrap_ListTacGia[i].TenTG,
                    Height = 20,
                    Margin = new Thickness(1) { },
                    IsDeletable = true,
                    Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
                };
                chip.DeleteClick += Chip_DeleteClickTacGia;
                WrapPanelTacGia.Children.Insert(1, chip);
            }
        }

        private void CapNhatChip_TheLoai()
        {

            while (WrapPanelTheloai.Children.Count > 2) WrapPanelTheloai.Children.RemoveAt(1);
           
            for (int i = 0; i < Wrap_ListTheLoai.Count; i++)
            {
          
                Chip chip = new Chip()
                {
                    TabIndex = Wrap_ListTheLoai[i].MaTL,
                    Content = Wrap_ListTheLoai[i].TenTL,
                 
                    Height = 20,
                    Margin = new Thickness(1) { },
                    IsDeletable = true,
                    Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)),
                };

                chip.DeleteClick += Chip_DeleteClickTheLoai;
                WrapPanelTheloai.Children.Insert(1, chip);
            }
        }

        #endregion Cập nhật chip
    }
}