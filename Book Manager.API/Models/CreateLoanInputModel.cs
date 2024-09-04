namespace Book_Manager.API.Models;

public class CreateLoanInputModel
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public DateTime LoanDate { get; set; }
}