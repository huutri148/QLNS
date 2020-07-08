using BookStoreClone.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    class PhieuThuTienViewModel : BaseViewModel
    {
        DateTime _SelectedDateTime;


        QuanLyKhachHangViewModel quanLyKhachHangViewModel;
        Visibility visibilityPnlThuTien;
        string soTienTra;
        KhachHang _SelectedKhachHang;
        public ICommand XacNhanThuTienCommand { get; set; }
        public ICommand GetViewModelQuanLyKhachHang { get; set; }
        public Visibility VisibilityPnlThuTien { get => visibilityPnlThuTien; set { visibilityPnlThuTien = value; OnPropertyChanged(); } }
        public string SoTienTra { get => soTienTra; set { soTienTra = value; OnPropertyChanged(); } }
        public KhachHang SelectedKhachHang { get => _SelectedKhachHang; set { _SelectedKhachHang = value; OnPropertyChanged(); if(SelectedKhachHang==null) return;  if (SelectedKhachHang.SoTienNo > 0) VisibilityPnlThuTien = Visibility.Visible; else VisibilityPnlThuTien = Visibility.Collapsed;} }

        public DateTime SelectedDateTime { get => _SelectedDateTime; set { _SelectedDateTime = value; OnPropertyChanged(); } }

        public PhieuThuTienViewModel()
        {
            SelectedDateTime = System.DateTime.Now;
            XacNhanThuTienCommand = new RelayCommand<Button>((p) => {
                if (p == null) return false;
                p.IsEnabled = false;

                if (SelectedKhachHang == null) return false;
             
                if (KiemTraSo(SoTienTra) == false) return false;
               
                if (int.Parse(soTienTra) > SelectedKhachHang.SoTienNo|| int.Parse(soTienTra) <= 0) return false;
           
                p.IsEnabled = true;
                return true;
            }, (p) =>
            {
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PhieuThuTien] ON");
                DataProvider.Ins.DB.PhieuThuTiens.Add(new PhieuThuTien() { KhachHang = SelectedKhachHang, SoTienThu = int.Parse(soTienTra), NguoiDung = DataProvider.Ins.DB.NguoiDungs.Where(x=>x.TenDangNhap==Const.IDNguoiDung).First(), NgayThuTien = SelectedDateTime });
                DataProvider.Ins.DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PhieuThuTien] OFF");
                SelectedKhachHang.SoTienNo -= int.Parse(soTienTra);
                DataProvider.Ins.DB.SaveChanges();
                int idKhach = SelectedKhachHang.MaKH;
                SelectedKhachHang = new KhachHang();
                SelectedKhachHang = DataProvider.Ins.DB.KhachHangs.Where(x => x.MaKH == idKhach).First();
                SoTienTra = "";
                quanLyKhachHangViewModel.TimKiemKhachHang();
                SelectedDateTime = System.DateTime.Now;
            });
           GetViewModelQuanLyKhachHang = new RelayCommand<DockPanel>((p) => {
               
                return true;
            }, (p) =>
            {
                quanLyKhachHangViewModel = p.DataContext as QuanLyKhachHangViewModel;
            });
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
    }
}
