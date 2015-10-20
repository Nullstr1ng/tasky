using System.Diagnostics;
using System.Windows;

namespace tasky
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            btnMinimize.Click += BtnMinimize_Click;
            btnMaximize.Click += BtnMaximize_Click;
            btnRestore.Click += BtnRestore_Click;
            btnClose.Click += BtnClose_Click;
            this.gridTitle.MouseDown += GridTitle_MouseDown;
        }

        private void GridTitle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                DragMove();
            }
        }

        // new feature here for test 2

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void BtnRestore_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.btnMaximize.Visibility = Visibility.Visible;
            this.btnRestore.Visibility = Visibility.Collapsed;
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            this.btnMaximize.Visibility = Visibility.Collapsed;
            this.btnRestore.Visibility = Visibility.Visible;
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
