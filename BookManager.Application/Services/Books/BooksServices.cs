using Book_Manager.API.Persistence;
using Book_Manager.Application.Models;
using BookManager.Application.Models;

namespace BookManager.Application.Services.Books
{
    public class BooksServices : IBooksServices
    {
        private readonly BookManagerDbContext _context;

        public BooksServices(BookManagerDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<BooksViewModel>> GetAll(string search = "")
        {
            var books = _context.Books.Where(b => !b.IsDeleted).ToList();

            if (books is null)
            {
                return ResultViewModel<List<BooksViewModel>>.Erro("Os livros não foram encontradom.");
            }

            var model = books.Select(BooksViewModel.FromEntity).ToList();

            return ResultViewModel<List<BooksViewModel>>.Success(model);
        }

        public ResultViewModel<BooksViewModel> GetById(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book is null)
            {
                return ResultViewModel<BooksViewModel>.Erro("O livro não foi encontrado.");
            }

            var model = BooksViewModel.FromEntity(book);

            return ResultViewModel<BooksViewModel>.Success(model);
        }

        public ResultViewModel<int> Post(CreateBookInputModel model)
        {
            var book = model.ToEntity();

            _context.Books.Add(book);
            _context.SaveChanges();

            return ResultViewModel<int>.Success(book.Id);
        }

        public ResultViewModel Delete(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book is null)
            {
                return ResultViewModel.Erro("Livro não encontrado");
            }
            book.SetAsDeleted();

            _context.Books.Update(book);
            _context.SaveChanges();

            return ResultViewModel.Success();
        }
    }
}
