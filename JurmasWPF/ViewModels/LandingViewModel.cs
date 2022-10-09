using JurmasWPF.Commands;
using JurmasWPF.Controls;
using JurmasWPF.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JurmasWPF.ViewModels
{
    internal class LandingViewModel : ViewModelBase
    {
        public NavButton SelectedMenu { get; set; }

        private ViewModelBase currentVM;

        public ViewModelBase CurrentVM
        {
            get { return currentVM; }
            set { currentVM = value; NotifyPropertyChanged(); }
        }

        private ICommand selectingMenuCommand;
        public ICommand SelectingMenuCommand
        {
            get => selectingMenuCommand;
            set => selectingMenuCommand = value;
        }

        public Window _MainWindow { get; private set; } = new MainWindow();
        public Window _CreateRecipeeWindow { get; private set; } = new AddRecipeeWindow();

        public LandingViewModel()
        {
            this.CurrentVM = App.AppHost!.Services.GetService<RecipeesListViewModel>();

            this.SelectingMenuCommand = new CommandBase(ExecSelectingMenuCommand);
        }

        private void ExecSelectingMenuCommand(object obj)
        {
            if(obj is null) return;
            if (obj is NavButton item)
            {
                if (item.NavLink.Equals("Ingredients"))
                {
                    this.CurrentVM = App.AppHost!.Services.GetService<IngredientsListViewModel>();
                }
                if (item.NavLink.Equals("Recipees"))
                {
                    this.CurrentVM = App.AppHost!.Services.GetService<RecipeesListViewModel>();
                }
                else 
                {
                    this.CurrentVM = App.AppHost!.Services.GetService<RecipeesListViewModel>();
                }
            }
        }
    }
}
