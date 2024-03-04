using OneTwenty.Shared.Generic;
using OneTwenty.Shared.Models;

namespace OneTwenty.Services.Services.User;

public interface IUserService : IGenericInterface<UserModel>
{
    Task<List<UserModel>> GetUsersForLastMonth(string interest);
}