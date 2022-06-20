using System;
using System.ComponentModel;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Models.Users;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserFactoryTests
    {
        private readonly UserFactory _userFactory;

        public UserFactoryTests()
        {
            _userFactory = new UserFactory();
        }

        [Fact]
        public void Creates_Normal_User()
        {
            var user = _userFactory.CreateUser("name", "test@test.com", "addrss", "+85884545545", UserType.Normal, "0");
            Assert.NotNull(user);
            Assert.IsAssignableFrom<NormalUser>(user);
            Assert.Equal(UserType.Normal, user.UserType);
        }
        [Fact]
        public void Creates_Premium_User()
        {
            var user = _userFactory.CreateUser("name", "test@test.com", "addrss", "+85884545545", UserType.Premium, "0");
            Assert.NotNull(user);
            Assert.IsAssignableFrom<PremiumUser>(user);
            Assert.Equal(UserType.Premium, user.UserType);
        }

        [Fact]
        public void Creates_Super_User()
        {
            var user = _userFactory.CreateUser("name", "test@test.com", "addrss", "+85884545545", UserType.SuperUser, "0");
            Assert.NotNull(user);
            Assert.IsAssignableFrom<SuperUser>(user);
            Assert.Equal(UserType.SuperUser, user.UserType);
        }

        [Fact]
        public void ThrowsException_WhenUserTypeIs_Unknown()
        {
            Enum.TryParse("some value", out UserType type);
            Assert.Throws<InvalidEnumArgumentException>(() => _userFactory.CreateUser("name", "test@test.com", "address 123", "+85884545545", type, "0"));

        }
    }



}
