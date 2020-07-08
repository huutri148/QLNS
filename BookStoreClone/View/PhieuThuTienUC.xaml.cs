
using System.Windows.Controls;

namespace BookStoreClone.View
{
    /// <summary>
    /// Interaction logic for PhieuThuTienUC.xaml
    /// </summary>
    public partial class PhieuThuTienUC : UserControl
    {
        //private ObservableCollection<PhieuThuTien> getPTT()
        //{
        //    return new ObservableCollection<PhieuThuTien>(DataProvider.Ins.DB.PhieuThuTiens);
        //}
        //private ObservableCollection<KhachHang> getKH()
        //{
        //    return new ObservableCollection<KhachHang>(DataProvider.Ins.DB.KhachHangs);
        //}
        //private ObservableCollection<NguoiDung> getND()
        //{
        //    return new ObservableCollection<NguoiDung>(DataProvider.Ins.DB.NguoiDungs);
        //}

        public PhieuThuTienUC()
        {
            InitializeComponent();
        }

        //public void LoadPhieuThu(object sender, RoutedEventArgs e)
        //{
        //    dataPhieuThu.ItemsSource = getPTT();
        //    cbKH.ItemsSource = getKH();
        //    cbNhanVien.ItemsSource = getND();
        //    btnHuy.IsEnabled = false;
        //    btnLuu.IsEnabled = false;

        //}
        //void XoaTrang()
        //{
        //    cbKH.SelectedItem = null;
        //    txbSoTienThu.Clear();
        //    txbSoTienThu.Clear();
        //    txbTienNo.Clear();
        //    cbNhanVien.SelectedItem = null;
        //}
        //private void btnThem_Click(object sender, RoutedEventArgs e)
        //{
        //    XoaTrang();
        //    setEnable();
        //    btnLuu.IsEnabled = true;
        //    btnHuy.IsEnabled = true;
        //    btnThem.IsEnabled = false;
        //}
        //void setEnable()
        //{
        //    cbKH.IsEnabled = true;
        //    cbNhanVien.IsEnabled = true;
        //    dpNgayLap.IsEnabled = true;
        //    txbSoTienThu.IsEnabled = true;
        //}
        //void setMutabel()
        //{
        //    XoaTrang();
        //    cbKH.IsEnabled = false;
        //    cbNhanVien.IsEnabled = false;
        //    dpNgayLap.IsEnabled = false;
        //    txbSoTienThu.IsEnabled = false;
        //}
        //private void btnHuy_Click(object sender, RoutedEventArgs e)
        //{
        //    XoaTrang();
        //    setMutabel();
        //    btnThem.IsEnabled = true;
        //    btnLuu.IsEnabled = false;
        //    btnHuy.IsEnabled = false;
        //}
        //private void btnXoa_Click(object sender, RoutedEventArgs e)
        //{
        //    Button seleted = (Button)sender;
        //    var item = seleted.DataContext as PhieuThuTien;
        //    var DeleteRecord = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu thu của  " + item.KhachHang.TenKH+ " không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    if (DeleteRecord == MessageBoxResult.Yes)
        //    {
        //        DataProvider.Ins.DB.PhieuThuTiens.Remove(item);
        //        DataProvider.Ins.DB.SaveChanges();
        //        dataPhieuThu.ItemsSource = getPTT();
        //    }
        //}
        //private void btnLuu_Click(object sender, RoutedEventArgs e)
        //{
        //    PhieuThuTien phieuThuTien = new PhieuThuTien();
        //    if(cbKH.SelectedItem != null && cbNhanVien.SelectedItem != null && txbSoTienThu.Text != null)
        //    {
        //        if((Convert.ToInt32(txbConLai.Text) >= 0))
        //        {
        //            var ans = MessageBox.Show("Bạn có chắc muốn thêm phiếu thu tiền không ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //            if( ans == MessageBoxResult.Yes)
        //            {
        //                phieuThuTien.SoTienThu = Convert.ToInt32(txbSoTienThu.Text);
        //                phieuThuTien.NgayThuTien = dpNgayLap.SelectedDate;
        //                KhachHang khachHang = cbKH.SelectedItem as KhachHang;
        //                NguoiDung nguoiDung = cbNhanVien.SelectedItem as NguoiDung;
        //                KhachHang find = DataProvider.Ins.DB.KhachHangs.Find(khachHang.MaKH);
        //                find.SoTienNo = Convert.ToInt32(txbConLai.Text);
        //                phieuThuTien.MaKH = khachHang.MaKH;
        //                phieuThuTien.MaND = nguoiDung.MaND;

        //                DataProvider.Ins.DB.SaveChanges();

        //                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PhieuThuTien] ON");
        //                DataProvider.Ins.DB.PhieuThuTiens.Add(phieuThuTien);
        //                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PhieuThuTien] OFF");
        //                DataProvider.Ins.DB.SaveChanges();

        //                dataPhieuThu.ItemsSource = getPTT();
        //                MessageBox.Show("Bạn đã thêm phiếu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

        //                XoaTrang();
        //                setMutabel();
        //                btnLuu.IsEnabled = false;
        //                btnThem.IsEnabled = true;
        //                btnHuy.IsEnabled = false;
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không được thu quá số tiền nợ của khách hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Chưa nhập đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }

        //}

        //private void txbSoTienThu_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if(!String.IsNullOrEmpty(txbSoTienThu.Text)&&!(String.IsNullOrEmpty(txbTienNo.Text)))
        //    {
        //        txbConLai.Text = (Convert.ToInt32(txbTienNo.Text) - Convert.ToInt32(txbSoTienThu.Text)).ToString();
        //    }
        //}

        //private void txbTienNo_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (!String.IsNullOrEmpty(txbSoTienThu.Text) && !(String.IsNullOrEmpty(txbTienNo.Text)))
        //    {
        //        txbConLai.Text = (Convert.ToInt32(txbTienNo.Text) - Convert.ToInt32(txbSoTienThu.Text)).ToString();
        //    }
        //}
       
    }
}