using OneTwenty.Jobs.Interfaces;

namespace OneTwenty.Jobs.Jobs.Csv;

public class CsvJobService : ICsvJobService
{
    public async Task<bool> Execute()
    {
        throw new NotImplementedException();
    }
}

public interface ICsvJobService : IJobService
{

}