using System.Windows;

namespace BookStoreClone
{
    internal class Program
    {
        public static MainWindow mainWindow = null;

        private Program()
        {
            //noteaa
        }

        private void Creater()
        {
        
            mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }
    }
}