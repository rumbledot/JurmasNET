namespace JurmasModels;

public class JurmasPhoto
{
    public int Id { get; set; }
    public int RecipeeId { get; set; }= -1;
    public string Description { get; set; } = string.Empty;
    public byte[] ImageBinary { get; set; } = Array.Empty<byte>();
}