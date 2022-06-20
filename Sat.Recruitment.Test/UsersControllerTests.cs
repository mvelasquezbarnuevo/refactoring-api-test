using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Abstractions;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Models.Users;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTests
    {
        private readonly UsersController _usersController;
        private readonly IModelValidation<User> _modelValidation;
        private readonly IUserFactory _userFactory;
        private readonly Mock<IDataStore<User>> _usersStoreMock;

        public UsersControllerTests()
        {
            _modelValidation = new UserValidationService();
            _userFactory = new UserFactory();
            _usersStoreMock = new Mock<IDataStore<User>>();

            _usersController = new UsersController(_modelValidation,
                _userFactory, _usersStoreMock.Object);
        }

        [Fact]
        public void Sut_CanBe_Instantiated()
        {
            Assert.NotNull(_usersController);
        }

        [Fact]
        public void FailedValidation_Returns_BadRequest()
        {
            var result =
                _usersController.CreateUser("", "", "", "", "Normal", "0")
                    as ObjectResult;

            Assert.IsType<BadRequestObjectResult>(result);
            Assert.False(((Result)result.Value).IsSuccess);
        }

        [Fact]
        public void Returns_User_Created()
        {
            _usersStoreMock.Setup(x => x.GetData())
                .Returns(new List<User>
                {
                    new NormalUser
                    {
                        Name = "Luis",
                        Email = "Luis@gmail.com",
                        Address = "Av. Juan G",
                        Phone = "+349 56676768215",
                        UserType = UserType.Normal
                    }
                });

            var result =
                _usersController.CreateUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124")
                    as ObjectResult;

            Assert.IsType<OkObjectResult>(result);
            Assert.True(((Result)result.Value).IsSuccess);
            Assert.Equal("User Created", ((Result)result.Value).Message);
        }

        [Fact]
        public void Returns_User_Duplicated()
        {

            _usersStoreMock.Setup(x => x.GetData())
                .Returns(new List<User>
                {
                    new NormalUser
                    {
                        Name = "Agustina",
                        Email = "Agustina@gmail.com",
                        Address = "Av. Juan G",
                        Phone = "+349 1122354215",
                        UserType = UserType.Normal
                    }
                });

            var result = _usersController.CreateUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215",
                    "Normal", "124")
                as ObjectResult;

            Assert.IsType<OkObjectResult>(result);
            Assert.False(((Result)result.Value).IsSuccess);
            Assert.Equal("User is duplicated", ((Result)result.Value).Message);

        }

        [Fact]
        public void ThrowsException_When_NullUserValidationService()
        {
            Assert.Throws<ArgumentNullException>(() => new UsersController(null,
                _userFactory, _usersStoreMock.Object));
        }

        [Fact]
        public void ThrowsException_When_NullUserFactory()
        {
            Assert.Throws<ArgumentNullException>(() => new UsersController(_modelValidation,
                null, _usersStoreMock.Object));
        }

        [Fact]
        public void ThrowsException_When_NullUserStore()
        {
            Assert.Throws<ArgumentNullException>(() => new UsersController(_modelValidation,
                _userFactory, null));

        }
    }
}
