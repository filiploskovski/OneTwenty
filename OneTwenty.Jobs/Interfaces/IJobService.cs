namespace OneTwenty.Jobs.Interfaces;

public interface IJobService
{
    Task<bool> Execute();
}