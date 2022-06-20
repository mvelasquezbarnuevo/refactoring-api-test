using System.ComponentModel;
using Sat.Recruitment.Api.Abstractions;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Services
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(string name, string email, string address, string phone, UserType userType, string money)
        {
            decimal.TryParse(money, out decimal decMoney);
            switch (userType)
            {
                case UserType.Normal:
                    {
                        return new NormalUser()
                        {
                            Name = name,
                            Email = email,
                            Address = address,
                            Phone = phone,
                            UserType = UserType.Normal,
                            Money = decMoney
                        };
                    }
                case UserType.SuperUser:
                    {
                        return new SuperUser()
                        {
                            Name = name,
                            Email = email,
                            Address = address,
                            Phone = phone,
                            UserType = UserType.SuperUser,
                            Money = decMoney
                        };
                    }
                case UserType.Premium:
                    {
                        return new PremiumUser()
                        {
                            Name = name,
                            Email = email,
                            Address = address,
                            Phone = phone,
                            UserType = UserType.Premium,
                            Money = decMoney
                        };
                    }

                default:
                    throw new InvalidEnumArgumentException("Cannot create unknown user type.");
            }
        }
    }
}