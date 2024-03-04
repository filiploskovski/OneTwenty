namespace OneTwenty.Shared.Generic;

public interface IGenericInterface<T> where T : class
{
    Task<T> Create(T model);
    Task<T> Update(T model);
    Task Delete(int id);
}