namespace Book_Manager.API.Models;

public class UpdateLoanInputModel
{
    public int BookId { get; private set; }
    public decimal Cost { get; private set; }
}