using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Abstractions
{
    public interface IUserFactory
    {
        User CreateUser(string name, string email, string address, string phone, UserType userType, string money);
    }
}