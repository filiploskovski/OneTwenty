using Microsoft.Extensions.Primitives;
using OneTwenty.Shared.Models;

namespace OneTwenty.Jobs;

public class DataValidationService
{
    // This is a simple data validation service that utilizes extensions from the Shared namespace.
    // The use of Extensions.Extensions is an anti-pattern in naming convention, but it's kept for the sake of time.
    // We can extend this with datetime and all the necessary validations that needs to be executed in memory
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