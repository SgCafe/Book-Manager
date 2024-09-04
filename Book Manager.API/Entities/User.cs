namespace Book_Manager.API.Entities;

public class User : BaseEntity
{
    public User(string fullName, DateTime birthdate, string email, string phone) : base()
    {
        FullName = fullName;
        Birthdate = birthdate;
        Email = email;
        Phone = phone;
        Active = true;
    }

    public int Id { get; private set; }
    public string FullName { get; private set; }
    public DateTime Birthdate { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public bool Active { get; private set; }

    public List<Loan> Loans { get; private set; }
}