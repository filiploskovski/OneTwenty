using OneTwenty.Jobs.Interfaces;
using OneTwenty.Jobs.Jobs.RestApi.RefitClients;

namespace OneTwenty.Jobs.Jobs.RestApi;

public class RestApiJobService : IRestApiJobService
{
    private readonly IUsersApi _usersApi;

    public RestApiJobService(IUsersApi usersApi)
    {
        _usersApi = usersApi;
    }

    public async Task<bool> Execute()
    {
        var apiData = await _usersApi.GetUsers();
        var validatedData = DataValidationService.Validate(apiData);
        Console.WriteLine(apiData);
        throw new NotImplementedException();
    }
}

public interface IRestApiJobService : IJobService
{

}