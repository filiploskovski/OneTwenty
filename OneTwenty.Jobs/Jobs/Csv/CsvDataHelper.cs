using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using OneTwenty.Shared.Models;

namespace OneTwenty.Jobs.Jobs.Csv;

public sealed class UserModelMap : ClassMap<UserModel>
{
    public UserModelMap()
    {
        Map(m => m.Name).Name("name");
        Map(m => m.Email).Name("email");
        Map(m => m.Signup_date).Name("signup_date");
        Map(m => m.Interests).Name("interests").Convert(row =>
        {
            var interestsString = row.Row.GetField("interests");
            var interestsArray = interestsString.Split(',');
            return new List<string>(interestsArray);
        });
    }
}

public class CsvDataHelper
{
    public static List<UserModel> FromFile()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(baseDirectory, "CsvFiles", "file_data.csv");

        var users = new List<UserModel>();

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true, 
            TrimOptions = TrimOptions.Trim | TrimOptions.InsideQuotes,
        });
        csv.Context.RegisterClassMap<UserModelMap>();

        while (csv.Read())
        {
            UserModel user = csv.GetRecord<UserModel>();
            users.Add(user);
        }

        return users;
    }
}