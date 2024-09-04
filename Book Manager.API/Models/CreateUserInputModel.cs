namespace Book_Manager.API.Models;

public class CreateUserInputModel
{
    public string FullName { get; set; }
    public DateTime Birthdate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}