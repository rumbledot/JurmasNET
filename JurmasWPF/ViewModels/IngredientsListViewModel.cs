using JurmasDAL;
using JurmasModels;
using JurmasWPF.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace JurmasWPF.ViewModels;

internal class IngredientsListViewModel : ViewModelBase
{
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
        set { filterString = value; NotifyPropertyChanged(); this.IngredientCollectionView.Refresh(); }
    }

    private readonly ObservableCollection<Ingredient> Ingredients;
    public ICollectionView IngredientCollectionView { get; set; }

    private FakeData FakeData { get; set; }
    public Recipee SelectedIngredient { get; set; }

    public IngredientsListViewModel()
    {
        this.FakeData = App.AppHost!.Services.GetService<FakeData>();

        this.IngredientCollectionView = CollectionViewSource.GetDefaultView(this.FakeData!.Ingredients);

        this.RefreshInformation();
    }

    private void RefreshInformation()
    {
        this.IngredientCollectionView.Refresh();
    }
}
