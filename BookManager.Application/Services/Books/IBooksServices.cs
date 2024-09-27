using Book_Manager.Application.Models;
using BookManager.Application.Models;

namespace BookManager.Application.Services.Books
{
    public interface IBooksServices
    {
        ResultViewModel<List<BooksViewModel>> GetAll(string search = "");
        ResultViewModel<BooksViewModel> GetById(int id);
        ResultViewModel<int> Post(CreateBookInputModel model);
        ResultViewModel Delete(int id);
    }
}