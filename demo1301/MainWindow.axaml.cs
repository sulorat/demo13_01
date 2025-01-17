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
            if (PasswordTextBox.Text == "0000")
            {
                MenuWindow menuWindow = new MenuWindow(true);
                menuWindow.Show();
                this.Hide();
            }
            else
            {
                var menuWin = new MenuWindow(false);
                menuWin.Show();
                this.Hide();
            }

        }
    }
}