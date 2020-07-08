using BookStoreClone.View;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BookStoreClone.ViewModel
{
    internal class SachViewModel : BaseViewModel
    {
        public ICommand HienThoThongTinSachCommand { get; set; }
        public ICommand MouseEnterCommand { get; set; }
        public ICommand MouseLeaveCommand { get; set; }
        public ShadowDepth ShadowDepthA { get => shadowDepth; set { shadowDepth = value; OnPropertyChanged(); } }
        private ShadowDepth shadowDepth;

        public SachViewModel()
        {
            HienThoThongTinSachCommand = new ViewModel.RelayCommand<TextBlock>((p) => { return true; }, (p) =>
            {
                OnPropertyChanged();
                FrameworkElement frameworkElement = p;
                while (frameworkElement.Parent as TimKiemSachUC == null)
                    frameworkElement = frameworkElement.Parent as FrameworkElement;
                DockPanel dockPanel = frameworkElement as DockPanel;
                for (int i = 0; i < dockPanel.Children.Count; i++)
                    if (dockPanel.Children[i] is Card)
                    {
                        TimKiemSachViewModel timKiemSachViewModel = (TimKiemSachViewModel)(dockPanel.Parent as TimKiemSachUC).DataContext;
                        timKiemSachViewModel.IDSach = p.Text;
                        dockPanel.Children[i].Visibility = Visibility.Visible;
                    }
            }
            );
            MouseEnterCommand = new ViewModel.RelayCommand<Card>((p) => { return true; }, (p) =>
             {
                 ShadowDepthA = ShadowDepth.Depth5;
             }
            );
            MouseLeaveCommand = new ViewModel.RelayCommand<Card>((p) => { return true; }, (p) =>
            {
                ShadowDepthA = ShadowDepth.Depth0;
            }
           );
        }
    }
}