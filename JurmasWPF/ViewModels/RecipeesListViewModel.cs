using JurmasDAL;
using JurmasModels;
using JurmasWPF.Commands;
using JurmasWPF.Services;
using JurmasWPF.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace JurmasWPF.ViewModels;

internal class RecipeesListViewModel : ViewModelBase
{
    private ICommand createRecipeeCommand;
    public ICommand CreateRecipeeCommand
    {
        get { return createRecipeeCommand; }
        set { createRecipeeCommand = value; }
    }

    private ICommand deleteRecipeeCommand;
    public ICommand DeleteRecipeeCommand
    {
        get { return deleteRecipeeCommand; }
        set { deleteRecipeeCommand = value; }
    }

    private ICommand editRecipeeCommand;
    public ICommand EditRecipeeCommand
    {
        get { return editRecipeeCommand; }
        set { editRecipeeCommand = value; }
    }

    private string infoMessage = String.Empty;
    public string InfoMessage
    {
        get { return infoMessage; }
        set { infoMessage = value; NotifyPropertyChanged(); }
    }

    private string filterString = string.Empty;
    public string FilterString
    {
        get { return filterString; }
        set { filterString = value; NotifyPropertyChanged(); this.RecipeeCollectionView.Refresh(); }
    }

    private readonly ObservableCollection<Recipee> Recipees;
    public ICollectionView RecipeeCollectionView { get; set; }

    private FakeData FakeData { get; set; }
    public Recipee SelectedRecipee { get; set; }
    private int RecipeeCount { get { return this.RecipeeCollectionView.Cast<Recipee>().Count(); } }
    private Random random = new Random();
    private string[] greetings = new string[]
    {
            "On your way to Maaster Chef! Whopping {0} recipees!",
            "You for {0} recipees chef!",
            "Well done! Add more to your {0} recipees."
    };

    public RecipeesListViewModel()
    {
        this.Recipees = new ObservableCollection<Recipee>();

        this.CreateRecipeeCommand = new CommandBase(ExecuteOpenCreateRecipee);
        this.EditRecipeeCommand = new CommandBase(ExecuteEditRecipee, CanExecuteDeleteRecipee);
        this.DeleteRecipeeCommand = new CommandBase(ExecuteDeleteRecipee, CanExecuteDeleteRecipee);

        this.FakeData = App.AppHost.Services.GetService<FakeData>()!;
        this.RecipeeCollectionView = CollectionViewSource.GetDefaultView(this.FakeData!.GetAllRecipes());

        this.RecipeeCollectionView.Filter = RecipeeFilter;

        this.RefreshInformation();
    }

    private void RefreshInformation()
    {
        this.RecipeeCollectionView.Refresh();

        if (this.RecipeeCount <= 0)
        {
            this.infoMessage = "Add your first recipee!";
        }
        else if (this.RecipeeCount == 1)
        {
            this.infoMessage = "Congratulation! You have a recipee, try add more!";
        }
        else
        {
            this.InfoMessage = string.Format(this.RandomGreeting(), this.RecipeeCollectionView.Cast<Recipee>().Count().ToString());
        }
    }

    private string RandomGreeting()
    {
        return this.greetings[random.Next(0, this.greetings.Length - 1)];
    }

    private bool RecipeeFilter(object obj)
    {
        if (obj is Recipee recipee)
        {
            return recipee.Title.Contains(this.FilterString, StringComparison.InvariantCultureIgnoreCase)
                || recipee.Description.Contains(this.FilterString, StringComparison.InvariantCultureIgnoreCase);
        }

        return false;
    }

    private void ExecuteOpenCreateRecipee(object obj)
    {
        var window = App.AppHost.Services.GetService<AddRecipeeWindow>();
        window.ShowDialog();

        this.RefreshInformation();
    }

    private void ExecuteEditRecipee(object obj)
    {
        if (obj is null) return;

        var window = App.AppHost.Services.GetService<AddRecipeeWindow>();

        if (obj is Recipee r)
        {
            window!.SetRecipee(r);
        }

        window!.ShowDialog();
        this.RefreshInformation();
    }

    private bool CanExecuteDeleteRecipee(object obj)
    {
        return this.SelectedRecipee != null;
    }

    private void ExecuteDeleteRecipee(object obj)
    {
        if (obj is null) return;

        if (obj is Recipee r)
        {
            this.FakeData.DeleteRecipee(r);
            this.RefreshInformation();
        }

    }
}
