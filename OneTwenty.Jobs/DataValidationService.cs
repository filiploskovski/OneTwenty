using Microsoft.Extensions.Primitives;
using OneTwenty.Shared.Models;

namespace OneTwenty.Jobs;

public class DataValidationService
{
    public static List<UserModel> Validate(List<UserModel> users)
    {
        var validated = new List<UserModel>();

        foreach (var user in users)
        {
            user.Signup_date = user.Signup_date.Date; // Remove time part

            if (!Shared.Extensions.Extensions.IsValidEmail(user.Email))
            {
                // Handle invalid email address (e.g., log, throw exception, etc.)
                continue; // Skip this user
            }

            validated.Add(user);
        }

        return validated;
    }
}