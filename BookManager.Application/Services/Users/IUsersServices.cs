using Book_Manager.Application.Models;
using BookManager.Application.Models;

namespace BookManager.Application.Services.Users
{
    public interface IUsersServices
    {
        ResultViewModel<List<UsersViewModel>> GetAll(string search = "");
        ResultViewModel<UsersViewModel> GetById(int id);
        ResultViewModel<int> Post(CreateUserInputModel model);
    }
}
