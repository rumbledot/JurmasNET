using System.ComponentModel.DataAnnotations.Schema;

namespace JurmasDAL;

public class Ingredient
{
    public int Id { get; set; }

    [Column("varchar(100)")]
    public string Name { get; set; } = String.Empty;

    [Column("varchar(20)")]
    public string UOM { get; set; }= String.Empty;

    [Column("varchar(500)")]
    public string Description { get; set; } = String.Empty;
}
