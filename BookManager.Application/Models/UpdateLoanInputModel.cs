using Book_Manager.Core.Entities;

namespace Book_Manager.Application.Models;
public class UpdateLoanInputModel
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public decimal Cost { get; set; }

    public Loan ToEntity()
        => new(BookId, UserId, Cost);
}