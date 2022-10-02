namespace JurmasModels;

public class RecipeeHistory
{
    public int Id { get; set; }
    public int RecipeeId { get; set; } = -1;
    public DateTime LastCooked { get; set; }
    public int NumOfCooked { get; set; } = 0;
}