namespace Book_Manager.API.Models;

public class CreateBookInputModel
{
    public int IdBook { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int YearPublication { get; set; }
}