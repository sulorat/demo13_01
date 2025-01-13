using Avalonia.Controls;
using Avalonia.Interactivity;

namespace demo1301
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToMenuClickButton(object? sender, RoutedEventArgs e)
        {
            var menuWin = new MenuWindow();
            menuWin.Show();
            this.Hide();
        }
    }
}