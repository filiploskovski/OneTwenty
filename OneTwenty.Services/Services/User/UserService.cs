using Microsoft.EntityFrameworkCore;
using OneTwenty.Infrastructure;
using OneTwenty.Shared.Models;

namespace OneTwenty.Services.Services.User;

// TODO: Validation
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

    public async Task<UserModel> Create(UserModel model)
    {
        var entity = new Infrastructure.Entities.User()
        {
            Email = model.Email,
            Name = model.Name,
            SignupDate = model.Signup_date
        };
        _dataContext.Users.Update(entity);
        await _dataContext.SaveChangesAsync();
        return new UserModel()
        {
            Id = model.Id,
            Email = model.Email,
            Name = model.Name,
            Signup_date = model.Signup_date
        };
    }

    public async Task<UserModel> Update(UserModel model)
    {
        var entity = await _dataContext.Users.FirstOrDefaultAsync(q => q.UserId == model.Id);

        if (entity == null)
            throw new NullReferenceException();

        entity.Name = model.Name;
        entity.Email = model.Email;
        _dataContext.Users.Update(entity);
        await _dataContext.SaveChangesAsync();

        return new UserModel()
        {
            Id = entity.UserId,
            Email = entity.Email,
            Name = entity.Name,
            Signup_date = entity.SignupDate
        };
    }

    public async Task Delete(int id)
    {
        var entity = await _dataContext.Users.FirstOrDefaultAsync(q => q.UserId == id);

        if (entity == null)
            throw new NullReferenceException();

        _dataContext.Users.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }
}