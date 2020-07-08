using BookStoreClone.Model;
using BookStoreClone.ViewModel;
using System.Linq;

namespace BookStoreClone
{
    public class Const : BaseViewModel
    {
        public static int QuyDinh_Count { set; get; } = DataProvider.Ins.DB.QuyDinhs.Count();
        public static int QuyDinh_TonToiThieuSauKhiBan { set; get; } = DataProvider.Ins.DB.QuyDinhs.ToList()[QuyDinh_Count - 1].SoLuongSachTonToiThieuSauKhiBan;
        public static int QuyDinh_TienNoToiDa { set; get; } = DataProvider.Ins.DB.QuyDinhs.ToList()[QuyDinh_Count - 1].TienNoToiDa;
        public static int QuyDinh_SoLuongSachTonToiThieuDeNhap { get; set; } = DataProvider.Ins.DB.QuyDinhs.ToList()[QuyDinh_Count - 1].SoLuongSachTonToiThieuDeNhap;
        public static int QuyDinh_SoLuongSachNhapToiThieu { get; set; } = DataProvider.Ins.DB.QuyDinhs.ToList()[QuyDinh_Count - 1].SoLuongSachNhapToiThieu;
        public static int QuyDinh_SoLuongSachTonToiDaDeNhap { get; set; } = DataProvider.Ins.DB.QuyDinhs.ToList()[QuyDinh_Count - 1].SoLuongSachTonToiThieuDeNhap;
        public static int QuyDinh_HeSoDonGia { get; set; } = 115;
        public static int QuyDinh_SoLuongSachNhapToiDa { get; set; } = 300;
        public static string IDNguoiDung { get; set; } 
    }
}