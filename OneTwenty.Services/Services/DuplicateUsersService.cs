using Microsoft.EntityFrameworkCore;
using OneTwenty.Infrastructure;
using OneTwenty.Infrastructure.Entities;

namespace OneTwenty.Services.Services;

public class DuplicateUsersService : IDuplicateUsersService
{
    private readonly DataContext _dataContext;

    public DuplicateUsersService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task MergeDuplicateByUserEmail(string email)
    {
        var duplicateUsers = await _dataContext.Users
            .Include(q => q.UserInterests)
            .ThenInclude(a => a.Interest)
            .Where(q => q.Email == email)
            .ToListAsync();

        var firstUser = duplicateUsers.First();

        var mergedUserInterests = duplicateUsers
            .SelectMany(user => user.UserInterests)
            .DistinctBy(q => q.InterestId)
            .Select(a => new UserInterest()
            {
                InterestId = a.InterestId,
                UserId = firstUser.UserId
            })
            .ToList();

        foreach (var duplicateUser in duplicateUsers.Skip(1))
        {
            _dataContext.UserInterests.RemoveRange(duplicateUser.UserInterests);
            _dataContext.Users.Remove(duplicateUser);
        }

        firstUser.UserInterests.Clear();
        firstUser.UserInterests.AddRange(mergedUserInterests);
        _dataContext.Users.Update(firstUser);
        await _dataContext.SaveChangesAsync();
    }
}

public interface IDuplicateUsersService
{
    Task MergeDuplicateByUserEmail(string email);
}