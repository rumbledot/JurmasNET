using System.ComponentModel.DataAnnotations.Schema;

namespace JurmasDAL;

public class Jurmas
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(20)")]
    public string Username { get; set; } = String.Empty;

    [Column(TypeName = "varchar(20)")]
    public string Password { get; set; } = String.Empty;

    [Column(TypeName = "varchar(20)")]
    public string FirstName { get; set; } = String.Empty;

    [Column(TypeName = "varchar(20)")]
    public string LastName { get; set; } = String.Empty;

    public DateTime DateCreated { get; set; }
}