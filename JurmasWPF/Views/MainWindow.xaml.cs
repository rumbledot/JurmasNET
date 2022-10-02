using JurmasModels;
using JurmasWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JurmasWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isMaximized = false;

        public MainWindow()
        {
            InitializeComponent();

            var vm = this.DataContext as MainViewModel;
        }

        private void BorderMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BorderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
            {
                return;
            }

            if (this.isMaximized)
            {
                this.WindowState = WindowState.Normal;
                this.Height = 600;
                this.Width = 800;

                this.isMaximized = false;
                return;
            }

            this.WindowState = WindowState.Maximized;
            this.isMaximized = true;
        }
    }
}
