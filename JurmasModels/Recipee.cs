namespace JurmasModels;

public class Recipee
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    //Status:
    //0: New
    //1: Want to cook
    //2: Tried
    public int StatusId { get; set; } = default(int);
    public JurmasStatus? Status { get; set; } = null;
    public int CategoryId { get; set; } = default(int);
    public Category? Category { get; set; } = null;
    public bool IsFavourite { get; set; } = false;
    public int CreatedBy { get; set; } = -1;
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }

    public virtual List<RecipeeIngredient> Ingredients { get; set; } = new List<RecipeeIngredient>();
    public virtual List<RecipeeStep> Steps { get; set; }= new List<RecipeeStep>();
}

public class RecipeeIngredient
{
    public int Id { get; set; }
    public int RecipeeId { get; set; } = -1;
    public int IngredientId { get; set; } = -1;
    public Ingredient? Ingredient { get; set; } = null;
    public decimal Amount { get; set; } = default(decimal);
}

public class RecipeeStep
{
    public int Id { get; set; }
    public int RecipeeId { get; set; } = -1; 
    public int StepNum { get; set; } = 1;
    public string Description { get; set; } = string.Empty;
}