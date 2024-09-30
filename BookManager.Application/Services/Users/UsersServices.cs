using Book_Manager.Application.Models;
using Book_Manager.Infrastructure.Persistence;
using BookManager.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManager.Application.Services.Users
{
    public class UsersServices : IUsersServices
    {
        private readonly BookManagerDbContext _context;

        public UsersServices(BookManagerDbContext context)
        {
            _context = context;
        }

        public ResultViewModel<List<UsersViewModel>> GetAll(string search = "")
        {
            var user = _context.Users
            .Include(u => u.Loans)
                .ThenInclude(b => b.Book)
            .Where(u => !u.IsDeleted)
            .ToList();

            if (user is null)
            {
                ResultViewModel<List<UsersViewModel>>.Erro("Não existe usuários cadastrados");
            }

            var model = user.Select(UsersViewModel.FromEntity).ToList();

            return ResultViewModel<List<UsersViewModel>>.Success(model);
        }

        public ResultViewModel<UsersViewModel> GetById(int id)
        {
            var user = _context.Users
           .Include(l => l.Loans)
               .ThenInclude(b => b.Book)
           .SingleOrDefault(u => u.Id == id);

            if (user is null)
            {
                return ResultViewModel<UsersViewModel>.Erro("Usuário não encontrado");
            }

            var model = UsersViewModel.FromEntity(user);
            return ResultViewModel<UsersViewModel>.Success(model);
        }

        public ResultViewModel<int> Post(CreateUserInputModel model)
        {
            var user = model.ToEntity();

            _context.Users.Add(user);
            _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
