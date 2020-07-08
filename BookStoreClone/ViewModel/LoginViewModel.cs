using BookStoreClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    class LoginViewModel:BaseViewModel
    {
        public static bool IsLogin { get; set; }

        private string _userName;
        private string _password;
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }
        public string UserName { get => _userName; set { _userName = value; OnPropertyChanged(); } }

        

        public ICommand PasswordChangedCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand BoQuaDangNhapCommand { get; set; }
        public LoginViewModel()
        {
            IsLogin = false;
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                Login(p);
            });
            BoQuaDangNhapCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                IsLogin = false;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                p.Hide();
            });
        }
        void Login(Window p)
        {
            if (p == null)
                return;
            string pass = Password;
            string passEncode = MD5Hash(Base64Encode(Password));
            var account = DataProvider.Ins.DB.NguoiDungs.Where(x => x.TenDangNhap == UserName && x.MatKhau == passEncode).Count();
            if (account > 0)
            {
                IsLogin = true;
                Const.IDNguoiDung = UserName;
                MessageBox.Show("Đăng nhập thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                p.Hide();

            }
            else
            {
                MessageBox.Show("Nhập sai tài khoản hoặc mật khẩu");
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
    
    }
}
