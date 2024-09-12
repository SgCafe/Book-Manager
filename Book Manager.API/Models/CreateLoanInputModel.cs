using Book_Manager.API.Entities;

namespace Book_Manager.API.Models;

public class CreateLoanInputModel
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public decimal Cost { get; set; }
    public Loan ToEntity()
        => new Loan(BookId, UserId, Cost);
}