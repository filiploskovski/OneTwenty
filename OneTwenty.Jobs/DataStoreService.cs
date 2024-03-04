using Microsoft.EntityFrameworkCore;
using OneTwenty.Infrastructure;
using OneTwenty.Infrastructure.Entities;
using OneTwenty.Shared.Models;

namespace OneTwenty.Jobs;

public class DataStoreService : IDataStoreService
{
    private readonly DataContext _dataContext;

    public DataStoreService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task Store(List<UserModel> users)
    {
        var usersToCreate = new List<User>();
        var usersToUpdate = new List<User>();

        var usersInterests = users.SelectMany(q => q.Interests).Distinct().ToList();
        var interestsFromDb = await CreateUpdateInterests(usersInterests);

        var exitingUsersFromDatabase = await _dataContext.Users
            .Include(q => q.UserInterests)
            .ThenInclude(q => q.Interest)
            .Where(q => users.Select(q => q.Name).Contains(q.Name))
            .AsSplitQuery().ToListAsync();

        foreach (var user in users)
        {
            var exitingUser = exitingUsersFromDatabase.FirstOrDefault(q => q.Name == user.Name);

            if (exitingUser == null)
            {
                var newUser = new User()
                {
                    Name = user.Name,
                    Email = user.Email,
                    SignupDate = user.Signup_date,
                    UserInterests = GenerateUserInterests(new List<UserInterest>(), user.Interests, interestsFromDb)
                };
                usersToCreate.Add(newUser);
            }
            else
            {
                exitingUser.Name = user.Name;
                exitingUser.Email = user.Email;
                exitingUser.UserInterests =
                    GenerateUserInterests(exitingUser.UserInterests, user.Interests, interestsFromDb);

                usersToUpdate.Add(exitingUser);
            }
        }

        //remove duplicates left commented on purpose
        //usersToCreate = usersToCreate.GroupBy(x => x.Name)
        //    .SelectMany(g => g.Count() > 1 ? new List<User> { g.Last() } : new List<User> { g.First() })
        //    .ToList();

        await _dataContext.Users.AddRangeAsync(usersToCreate);
        _dataContext.Users.UpdateRange(usersToUpdate);
        await _dataContext.SaveChangesAsync();
    }

    private List<UserInterest> GenerateUserInterests(List<UserInterest> currentUserInterests, List<string> newUserInterests, List<Interest> exitingInterestsFromDb)
    {
        currentUserInterests.Clear();
        return exitingInterestsFromDb.Where(q => newUserInterests.Contains(q.Name)).Select(a => new UserInterest()
        {
            InterestId = a.InterestId
        }).ToList();
    }

    private async Task<List<Interest>> CreateUpdateInterests(List<string> usersInterests)
    {
        var interestsInDb = await _dataContext.Interests.Select(a => a.Name).ToListAsync();
        var interestToAdd = usersInterests.Except(interestsInDb).Select(interest => new Interest()
        {
            Name = interest
        });

        _dataContext.Interests.AddRange(interestToAdd);
        await _dataContext.SaveChangesAsync();

        return await _dataContext.Interests.Where(a => usersInterests.Contains(a.Name)).ToListAsync();
    }
}

public interface IDataStoreService
{
    Task Store(List<UserModel> users);
}