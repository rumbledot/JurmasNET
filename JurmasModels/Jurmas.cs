namespace JurmasModels;

public class Jurmas
{
    public int Id { get; set; }
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime DateCreated { get; set; }
}