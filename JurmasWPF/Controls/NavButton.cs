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

namespace JurmasWPF.Controls;

public class NavButton : ListBoxItem
{
    static NavButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton), new FrameworkPropertyMetadata(typeof(NavButton)));
    }

    public string NavLink
    {
        get { return (string)GetValue(NavLinkProperty); }
        set { SetValue(NavLinkProperty, value); }
    }
    public static readonly DependencyProperty NavLinkProperty =
        DependencyProperty.Register("NavLink", typeof(string), typeof(NavButton), new PropertyMetadata(string.Empty));

    public string NavButtonText
    {
        get { return (string)GetValue(NavButtonTextProperty); }
        set { SetValue(NavButtonTextProperty, value); }
    }
    public static readonly DependencyProperty NavButtonTextProperty =
        DependencyProperty.Register("NavButtonText", typeof(string), typeof(NavButton), new PropertyMetadata(string.Empty));



    public Brush NavButtonTextColor
    {
        get { return (Brush)GetValue(NavButtonTextColorProperty); }
        set { SetValue(NavButtonTextColorProperty, value); }
    }

    // Using a DependencyProperty as the backing store for NavButtonTextColor.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NavButtonTextColorProperty =
        DependencyProperty.Register("NavButtonTextColor", typeof(Brush), typeof(NavButton), new PropertyMetadata(Brushes.Transparent));



    public ImageSource Icon
    {
        get { return (ImageSource)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }
    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(ImageSource), typeof(NavButton), new PropertyMetadata(null));

    public ImageSource IconHover
    {
        get { return (ImageSource)GetValue(IconHoverProperty); }
        set { SetValue(IconHoverProperty, value); }
    }
    public static readonly DependencyProperty IconHoverProperty =
        DependencyProperty.Register("IconHover", typeof(ImageSource), typeof(NavButton), new PropertyMetadata(null));
}
