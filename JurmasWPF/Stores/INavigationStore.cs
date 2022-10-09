using JurmasWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurmasWPF.Stores
{
    internal class INavigationStore
    {
    }

    public class NavigationStore
    {
        ViewModelBase CurrentVM { get; set; }
    }
}
