using Microsoft.EntityFrameworkCore;
using OneTwenty.Infrastructure;
using OneTwenty.Shared.Models;

namespace OneTwenty.Services.Services;

public class UserService : IUserService
{
    private readonly DataContext _dataContext;

    public UserService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<UserModel>> GetUsersForLastMonth(string interest)
    {
        return await _dataContext.Users.Where(a => a.SignupDate >= DateTime.Now.AddMonths(-1)
                                             && a.UserInterests.Any(q => q.Interest.Name == interest))
            .OrderBy(a => a.SignupDate).Select(u => new UserModel()
            {
                Name = u.Name,
                Signup_date = u.SignupDate,
                Email = u.Email,
                Interests = u.UserInterests.Select(q => q.Interest.Name).ToList(),
            }).AsSplitQuery()
            .ToListAsync();
    }
}

public interface IUserService
{
    Task<List<UserModel>> GetUsersForLastMonth(string interest);
}