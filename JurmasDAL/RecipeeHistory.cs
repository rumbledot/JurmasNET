using System.ComponentModel.DataAnnotations.Schema;

namespace JurmasDAL;

public class RecipeeHistory
{
    public int Id { get; set; }
    public DateTime LastCooked { get; set; }

    [Column("varchar(500)")]
    public string Note { get; set; }

    public int? RecipeeId { get; set; }
    public Recipee Recipee { get; set; }
}