using BookStoreClone.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    internal class HoaDonViewModel : BaseViewModel
    {
        public bool IsLoaded = false;
        public ICommand SelectionChangedSachCommand { get; set; }
        public ICommand XoaCTHDCommand { get; set; }
        public ICommand ThemCTHDCommand { get; set; }
        public ICommand LuuHDCommand { get; set; }
        private ObservableCollection<CTHD> _listCTHD;
        private string _tacGiaSach;
        private string _theLoaiSach;
        private double _donGiaBan;
        private int _thanhTien;
        private int _soLuongMua;
        private int _tongHoaDon;
        private int _thanhToan;
        private int _conLai;
        private KhachHang _khachHangSelected;
        private Sach _sachSelected;
        private ObservableCollection<KhachHang> _listKH;
        private ObservableCollection<Sach> _listSach;
        public ObservableCollection<CTHD> ListCTHD { get => _listCTHD; set { _listCTHD = value; OnPropertyChanged(); } }
        public string TheLoaiSach { get => _theLoaiSach; set { _theLoaiSach = value; OnPropertyChanged(); } }
        public string TacGiaSach { get => _tacGiaSach; set { _tacGiaSach = value; OnPropertyChanged(); } }
        public double DonGiaBan { get => _donGiaBan; set { _donGiaBan = value; OnPropertyChanged(); } }
        public ObservableCollection<KhachHang> ListKH { get => _listKH; set { _listKH = value; OnPropertyChanged(); } }
        public ObservableCollection<Sach> ListSach { get => _listSach; set { _listSach = value; OnPropertyChanged(); } }

        public int SoLuongMua { get => _soLuongMua; set { _soLuongMua = value; OnPropertyChanged(); } }

        public KhachHang KhachHangSelected { get => _khachHangSelected; set { _khachHangSelected = value; OnPropertyChanged(); } }
        public Sach SachSelected { get => _sachSelected; set { _sachSelected = value; OnPropertyChanged(); } }

        public int ThanhTien { get => _thanhTien; set { _thanhTien = value; OnPropertyChanged(); } }

        public int TongHoaDon { get => _tongHoaDon; set { _tongHoaDon = value; OnPropertyChanged(); } }

        public int ConLai { get => _conLai; set { _conLai = value; OnPropertyChanged(); } }
        public int ThanhToan { get => _thanhToan; set { _thanhToan = value; OnPropertyChanged(); } }

        public HoaDonViewModel()
        {
            ListCTHD = new ObservableCollection<CTHD>();
            ListSach = getSach();
            ListKH = getKH();
            //Sự kiện khi thay đổi select của Combobox sẽ thay đổi giá trị của Tác giả, Thể Loại và Giá bán
            SelectionChangedSachCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                TacGiaSach = "";
                TheLoaiSach = "";
                SachSelected = (p as ComboBox).SelectedItem as Sach;
                DonGiaBan = (double)SachSelected.DonGia * 1.15;
                int i = 0;
                foreach (var b in SachSelected.TacGias)
                {
                    if (i != SachSelected.TacGias.Count - 1)
                    {
                        TacGiaSach += b.TenTG + ", ";
                        i++;
                    }
                    else
                    {
                        i = 0;
                        TacGiaSach += b.TenTG;
                    }
                }
                foreach (var b in SachSelected.TheLoais)
                {
                    if (i != SachSelected.TheLoais.Count - 1)
                    {
                        TheLoaiSach += b.TenTL + ", ";
                        i++;
                    }
                    else
                    {
                        i = 0;
                        TheLoaiSach += b.TenTL;
                    }
                }
            });
            //Thêm CTHD vào ListCTHD
            ThemCTHDCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                if (KhachHangSelected != null)
                {
                    if (SachSelected != null)
                    {
                        if (SoLuongMua != 0)
                        {
                            if (SachSelected.SoLuongTon - SoLuongMua >= 20)
                            {
                                CTHD sach = new CTHD();
                                sach.SoLuong = SoLuongMua;
                                sach.MaSach = SachSelected.MaSach;
                                sach.Sach = SachSelected;
                                sach.DonGiaBan = (int)DonGiaBan;
                                ThanhTien = (int)DonGiaBan * SoLuongMua;
                                TongHoaDon += ThanhTien;
                                ListCTHD.Add(sach);
                                SachSelected = null;
                            }
                            else
                                MessageBox.Show("Không thể đáp ứng số lượng trên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                            MessageBox.Show("Bạn phải nhập số lượng sách cần mua", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                        MessageBox.Show("Bạn phải chọn sách cần mua", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Bạn phải chọn khách hàng ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            });
            //Xóa CTHD trong DataGridview
            XoaCTHDCommand = new RelayCommand<DataGrid>((p) => { return true; }, (p) =>
            {
                MessageBox.Show("Fuck");
                CTHD i = p.SelectedItem as CTHD;
                MessageBoxResult a = MessageBox.Show("Bạn có xóa " + i.Sach.TenSach + "ra khỏi hóa đơn không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (a == MessageBoxResult.Yes)
                {
                    ListCTHD.Remove(i);
                }
            });
            LuuHDCommand = new RelayCommand<UserControl>((p) => { return true; }, (p) =>
            {
                if (ThanhToan != 0)
                {
                    ConLai = TongHoaDon - ThanhToan;
                }
                else
                {
                    MessageBox.Show("Bạn chưa nhập số tiền thanh toán!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            });
        }

        //Lấy Khách Hàng từ Provider
        private ObservableCollection<KhachHang> getKH()
        {
            return new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);
        }

        //Lấy Sách từ Provider
        private ObservableCollection<Sach> getSach()
        {
            return new ObservableCollection<Sach>(DataProvider.Ins.DB.Saches);
        }

        //Trả về ListCTHD được tạo mới trong mỗi lần tạo Hóa Đơn
        private ObservableCollection<CTHD> getCTHD()
        {
            return ListCTHD;
        }
    }
}