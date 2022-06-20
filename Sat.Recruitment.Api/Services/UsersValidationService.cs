using Sat.Recruitment.Api.Abstractions;
using System.Text;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Services
{
    public class UserValidationService : IModelValidation<User>
    {
        public string Validate(User source)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(source.Name))
                //Validate if Name is null
                errors.Append("The name is required");
            if (string.IsNullOrWhiteSpace(source.Email))
                //Validate if Email is null
                errors.Append(" The email is required");
            if (string.IsNullOrWhiteSpace(source.Address))
                //Validate if Address is null
                errors.Append(" The address is required");
            if (string.IsNullOrWhiteSpace(source.Phone))
                //Validate if Phone is null
                errors.Append(" The phone is required");

            return errors.ToString();
        }
    }
}
