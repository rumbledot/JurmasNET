using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace JurmasDAL;

public class Recipee
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Title { get; set; } = string.Empty;

    [Column(TypeName = "varchar(500)")]
    public string Description { get; set; } = string.Empty;

    public bool IsFavourite { get; set; } = false;
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    //Status:
    //0: New
    //1: Want to cook
    //2: Tried
    public int? RecipeeStatusId { get; set; } = default(int);
    public RecipeeStatus RecipeeStatus { get; set; }
    public int? CategoryId { get; set; } = default(int);
    public Category Category { get; set; }
    public int? JurmasId { get; set; } = -1;
    public Jurmas Jurmas { get; set; }


    public virtual List<RecipeeIngredient> Ingredients { get; set; }
    public virtual List<RecipeeStep> Steps { get; set; }
    public virtual List<RecipeeHistory> History { get; set; }
    public virtual List<RecipeePhoto> Photos { get; set; }
}

public class RecipeeIngredient
{
    public int Id { get; set; }
    [Column("decimal(14,2)")]
    public decimal Amount { get; set; } = default(decimal);

    public int RecipeeId { get; set; }
    public Recipee Recipee { get; set; }

    public int IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
}

public class RecipeeStep
{
    public int Id { get; set; }
    public int StepNum { get; set; } = 1;

    [Column("varchar(200)")]
    public string Description { get; set; } = string.Empty;

    public int RecipeeId { get; set; }
    public Recipee Recipee { get; set; }
}