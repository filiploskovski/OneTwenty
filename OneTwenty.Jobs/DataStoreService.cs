using Microsoft.EntityFrameworkCore;
using OneTwenty.Infrastructure;
using OneTwenty.Infrastructure.Entities;
using OneTwenty.Shared.Models;

namespace OneTwenty.Jobs
{
    public class DataStoreService : IDataStoreService
    {
        private readonly DataContext _dataContext;

        public DataStoreService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // If we have more than 1000 records per job execution, we can extend this method to
        // save the users in batches
        public async Task Store(List<UserModel> users)
        {
            var usersToCreate = new List<User>();
            var usersToUpdate = new List<User>();

            // Get distinct interests from all new users
            var usersInterests = users.SelectMany(q => q.Interests).Distinct().ToList();

            // Create all the necessary interests that are not in the database so
            // when creating we don't need to 
            // make unnecessary requests to the db
            var interestsFromDb = await CreateUpdateInterests(usersInterests);

            // Fetch existing users from the database based on their names
            var existingUsersFromDatabase = await _dataContext.Users
                .Include(q => q.UserInterests)
                .ThenInclude(q => q.Interest)
                .Where(q => users.Select(q => q.Name).Contains(q.Name))
                .AsSplitQuery().ToListAsync();

            // Iterate through each user model to determine whether to create a new user or update an existing one
            foreach (var user in users)
            {
                var existingUser = existingUsersFromDatabase.FirstOrDefault(q => q.Name == user.Name);

                if (existingUser == null)
                {
                    // Create a new user model
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
                    // Update an existing user model
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    existingUser.UserInterests =
                        GenerateUserInterests(existingUser.UserInterests, user.Interests, interestsFromDb);

                    usersToUpdate.Add(existingUser);
                }
            }

            // Remove duplicates left commented on purpose so we can test the Advanced Data Handling feature
            //usersToCreate = usersToCreate.GroupBy(x => x.Name)
            //    .SelectMany(g => g.Count() > 1 ? new List<User> { g.Last() } : new List<User> { g.First() })
            //    .ToList();

            // Add new users and update existing ones to the database
            await _dataContext.Users.AddRangeAsync(usersToCreate);
            _dataContext.Users.UpdateRange(usersToUpdate);
            await _dataContext.SaveChangesAsync();
        }

        // Generate user interests based on the provided new user interests and existing interests from the database
        // It's possible to optimize this so it deletes only the necessary
        // items, but for the sake of time and not to overcomplicate things, it's made simple
        private List<UserInterest> GenerateUserInterests(List<UserInterest> currentUserInterests, List<string> newUserInterests, List<Interest> existingInterestsFromDb)
        {
            currentUserInterests.Clear();
            return existingInterestsFromDb.Where(q => newUserInterests.Contains(q.Name)).Select(a => new UserInterest()
            {
                InterestId = a.InterestId
            }).ToList();
        }

        // Create or update interests in the database based on the provided user interests
        private async Task<List<Interest>> CreateUpdateInterests(List<string> usersInterests)
        {
            // Get all items from db
            var interestsInDb = await _dataContext.Interests.Select(a => a.Name).ToListAsync();

            // Add only the necessary items
            var interestToAdd = usersInterests.Except(interestsInDb).Select(interest => new Interest()
            {
                Name = interest
            });

            _dataContext.Interests.AddRange(interestToAdd);
            await _dataContext.SaveChangesAsync();

            // Return the items that are only needed
            return await _dataContext.Interests.Where(a => usersInterests.Contains(a.Name)).ToListAsync();
        }
    }

    public interface IDataStoreService
    {
        Task Store(List<UserModel> users);
    }
}
