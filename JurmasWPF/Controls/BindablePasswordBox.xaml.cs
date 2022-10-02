using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace JurmasWPF.Controls;

public partial class BindablePasswordBox : UserControl
{
    public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox));

    public string Password
    {
        get
        {
            return (string)GetValue(PasswordProperty);
        }
        set
        {
            SetValue(PasswordProperty, value);
        }
    }


    public BindablePasswordBox()
    {
        InitializeComponent();
        tboxPassword.PasswordChanged += OnPasswordChanged;
    }

    private void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        Password = tboxPassword.Password;
    }
}
