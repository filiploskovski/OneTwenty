using Microsoft.EntityFrameworkCore;
using OneTwenty.Infrastructure;

namespace OneTwenty.Services.Services.Interest;

// TODO: Validation
public class InterestService : IInterestService
{
    private readonly DataContext _dataContext;

    public InterestService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<InterestModel> Create(InterestModel model)
    {
        var entity = new Infrastructure.Entities.Interest
        {
            Name = model.Name
        };
        _dataContext.Interests.Add(entity);
        await _dataContext.SaveChangesAsync();
        return new InterestModel(entity);
    }

    public async Task<InterestModel> Update(InterestModel model)
    {
        var entity = await _dataContext.Interests.FirstOrDefaultAsync(q => q.InterestId == model.Id);

        if (entity == null)
            throw new NullReferenceException();

        entity.Name = model.Name;
        _dataContext.Interests.Update(entity);
        await _dataContext.SaveChangesAsync();
        return new InterestModel(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _dataContext.Interests.Include(q => q.UserInterests)
            .FirstOrDefaultAsync(q => q.InterestId == id);

        if (entity == null)
            throw new NullReferenceException();

        if (entity.UserInterests.Any())
            throw new Exception("Cannot delete!");

        _dataContext.Interests.Remove(entity);
        await _dataContext.SaveChangesAsync();
    }
}