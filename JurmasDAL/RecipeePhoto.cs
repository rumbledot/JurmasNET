using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace JurmasDAL;

public class RecipeePhoto
{
    public int Id { get; set; }

    [Column("varchar(100)")]
    public string Description { get; set; } = string.Empty;

    public byte[] ImageBinary { get; set; } = Array.Empty<byte>();

    public int? RecipeeId { get; set; }
    public Recipee Recipee { get; set; }
}