using OneTwenty.Jobs.Interfaces;

namespace OneTwenty.Jobs.Jobs.Csv;

public class CsvJobService : ICsvJobService
{
    private readonly IDataStoreService _dataStoreService;

    public CsvJobService(IDataStoreService dataStoreService)
    {
        _dataStoreService = dataStoreService;
    }

    public async Task<bool> Execute()
    {
        var data = CsvDataHelper.FromFile();
        var validatedData = DataValidationService.Validate(data);
        await _dataStoreService.Store(validatedData);
        return true;
    }
}

public interface ICsvJobService : IJobService
{

}