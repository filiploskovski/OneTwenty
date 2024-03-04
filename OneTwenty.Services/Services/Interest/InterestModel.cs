namespace OneTwenty.Services.Services.Interest;

public class InterestModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public InterestModel(){}

    public InterestModel(Infrastructure.Entities.Interest entity)
    {
        Id = entity.InterestId;
        Name = entity.Name;
    }
}