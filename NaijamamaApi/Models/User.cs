namespace naijamama.Models
{
    public class User
{
    public int Id { get; set; }
    public required string Name { get; set; } // Mark as required
    public required string Email { get; set; }
    public required string Password { get; set; }

    public string Phone { get; set;}
}
}