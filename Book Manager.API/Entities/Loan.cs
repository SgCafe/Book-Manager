namespace Book_Manager.API.Entities;

public class Loan : BaseEntity
{
    public Loan(string name, string email, string phone)
    : base()
    {
        Name = name;
        Email = email;
        Phone = phone;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
}