using OneTwenty.Jobs.Interfaces;
using OneTwenty.Jobs.Jobs.RestApi.RefitClients;

namespace OneTwenty.Jobs.Jobs.RestApi;

public class RestApiJobService : IRestApiJobService
{
    private readonly IUsersApi _usersApi;
    private readonly IDataStoreService _dataStoreService;

    public RestApiJobService(IUsersApi usersApi, IDataStoreService dataStoreService)
    {
        _usersApi = usersApi;
        _dataStoreService = dataStoreService;
    }

    public async Task<bool> Execute()
    {
        var apiData = await _usersApi.GetUsers();
        var validatedData = DataValidationService.Validate(apiData);
        await _dataStoreService.Store(validatedData);
        return true;
    }
}

public interface IRestApiJobService : IJobService
{

}