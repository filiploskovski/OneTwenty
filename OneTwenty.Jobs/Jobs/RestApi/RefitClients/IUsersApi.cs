using OneTwenty.Shared.Models;
using Refit;

namespace OneTwenty.Jobs.Jobs.RestApi.RefitClients;

public interface IUsersApi
{
    [Get("/api/user")]
    Task<List<UserModel>> GetUsers();
}