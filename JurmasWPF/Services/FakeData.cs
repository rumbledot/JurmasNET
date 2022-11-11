using JurmasDAL;
using JurmasModels;
using JurmasWPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurmasWPF.Services;

internal class FakeData
{
    public List<Recipee> Recipees { get; set; }
    public List<Category> Categories { get; set; }
    public List<RecipeeStatus> Statuses { get; set; }
    public List<Ingredient> Ingredients { get; set; }
    private int index = 5;

    public FakeData()
    {
        this.Statuses = new List<RecipeeStatus>()
        {
            new RecipeeStatus()
            {
                Id =1,
                Name="New"
            },
            new RecipeeStatus()
            {
                Id =2,
                Name="Tried"
            },
            new RecipeeStatus()
            {
                Id =3,
                Name="Favourite"
            },
        };

        this.Categories = new List<Category>()
        {
            new Category()
            {
                Id = 0,
                Name="Uncategorized"
            },
            new Category()
            {
                Id = 1,
                Name="Baking"
            },
            new Category()
            {
                Id = 2,
                Name="Western"
            },
            new Category()
            {
                Id = 3,
                Name="Chinese"
            },
            new Category()
            {
                Id = 4,
                Name="Indonesian"
            }
        };

        this.Ingredients = new List<Ingredient>()
        {
            new Ingredient()
            {
                Id=1,
                Name="butter",
                Description="hmm buttery",
                UOM="gram"
            },
            new Ingredient()
            {
                Id=2,
                Name="garlic",
                Description="savory",
                UOM="clove"
            },
            new Ingredient()
            {
                Id=3,
                Name="shallot",
                Description="reddish savory",
                UOM="clove"
            },
            new Ingredient()
            {
                Id=4,
                Name="potato",
                Description="the spuds",
                UOM="spudnik"
            },
        };

        this.Recipees = new List<Recipee>()
        {
            new Recipee()
            {
                Id = 1,
                Title = "Opera cake",
                Description = "Desc 01",
                CategoryId = 1,
                RecipeeStatusId = 1,
                DateCreated = DateTime.Now,
            },
            new Recipee()
            {
                Id = 2,
                Title = "Steak",
                Description = "Desc 02",
                CategoryId = 2,
                RecipeeStatusId = 1,
                DateCreated = DateTime.Now,
            },
            new Recipee()
            {
                Id = 3,
                Title = "Stir fry",
                Description = "Desc 03",
                CategoryId = 3,
                RecipeeStatusId = 1,
                DateCreated = DateTime.Now,
            },
            new Recipee()
            {
                Id = 4,
                Title = "Rendang",
                Description = "Desc 04",
                CategoryId= 4,
                RecipeeStatusId = 2,
                DateCreated = DateTime.Now,
            },
            new Recipee()
            {
                Id = 5,
                Title = "Pancake",
                Description = "Desc 05",
                CategoryId = 0,
                RecipeeStatusId = 3,
                DateCreated = DateTime.Now,
            },
        };
    }

    public IEnumerable<Ingredient> GetAllIngredients()
    {
        foreach (var item in this.Ingredients)
        {
            yield return item;
        }
    }

    public IEnumerable<Recipee> GetAllRecipes()
    {
        foreach (var item in this.Recipees)
        {
            item.RecipeeStatus = this.Statuses.Where(s => s.Id == item.RecipeeStatusId).FirstOrDefault();
            item.Category = this.Categories.Where(c => c.Id == item.CategoryId).FirstOrDefault();
            item.Ingredients = new List<RecipeeIngredient>();
            item.Steps = new List<RecipeeStep>();

            yield return item;
        }
    }

    public void AddRecipee(Recipee recipee)
    {
        var existing = this.Recipees.Where(r => r.Id == recipee.Id).DefaultIfEmpty(null).FirstOrDefault();

        if (existing is null)
        {
            recipee.Id = ++index;
            this.Recipees.Add(recipee);
            return;
        }

        this.Recipees[this.Recipees.IndexOf(existing)] = recipee;
    }

    internal void DeleteRecipee(Recipee selectedRecipee)
    {
        if (!this.Recipees.Contains(selectedRecipee)) return;

        this.Recipees.Remove(selectedRecipee);
    }
}
