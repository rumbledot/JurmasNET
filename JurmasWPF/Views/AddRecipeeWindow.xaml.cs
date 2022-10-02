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
using System.Windows.Shapes;

namespace JurmasWPF.Views
{
    /// <summary>
    /// Interaction logic for AddRecipeeWindow.xaml
    /// </summary>
    public partial class AddRecipeeWindow : Window
    {
        private CreateRecipeeViewModel vm;
        private Recipee recipee;

        public AddRecipeeWindow()
        {
            InitializeComponent();

            this.Loaded += AddRecipeeWindow_Loaded;
            this.Closed += AddRecipeeWindow_Closed;
        }

        private void AddRecipeeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= AddRecipeeWindow_Loaded;

            if (this.DataContext is CreateRecipeeViewModel vm)
            {
                this.vm = vm;
                if (recipee != null)
                {
                    this.vm.SetRecipee(this.recipee);
                }
                this.vm.CancelCloseWindowEvent += Vm_CancelCloseWindowEvent;
            }
        }

        private void AddRecipeeWindow_Closed(object? sender, EventArgs e)
        {
            this.Closed -= AddRecipeeWindow_Closed;

            this.vm.CancelCloseWindowEvent -= Vm_CancelCloseWindowEvent;
        }

        internal void SetRecipee(Recipee recipee)
        {
            this.recipee = recipee;
        }

        private void Vm_CancelCloseWindowEvent()
        {
            this.Close();
        }
    }
}
