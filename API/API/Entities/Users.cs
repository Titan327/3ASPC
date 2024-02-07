namespace API.Entities;

public class Users
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Pseudo { get; set; }
    public required string Password  { get; set; }
    public required char Role  { get; set; }
}