using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using System;
using System.Diagnostics;
using System.Linq;
using Sat.Recruitment.Api.Abstractions;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IModelValidation<User> _modelValidation;
        private readonly IUserFactory _userFactory;
        private readonly IDataStore<User> _dataStore;

        public UsersController(IModelValidation<User> modelValidation,
            IUserFactory userFactory, IDataStore<User> dataStore)
        {
            _modelValidation = modelValidation ?? throw new ArgumentNullException(nameof(modelValidation));
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        [HttpPost]
        [Route("/create-user")]
        public IActionResult CreateUser(string name, string email, string address, string phone, string userType, string money)
        {

            //viewmodel model validation can be also done at this section (Model.IsValid - based on data annotations)
            Enum.TryParse(userType, out UserType type);
            var newUser = _userFactory.CreateUser(name, email, address, phone, type, money);

            string errors = _modelValidation.Validate(newUser);
            if (!string.IsNullOrEmpty(errors))
                return BadRequest(new Result
                {
                    IsSuccess = false,
                    Message = errors
                });

            try
            {
                var users = _dataStore.GetData().ToList();

                foreach (var user in users)
                {
                    if (user.Email == newUser.Email || user.Phone == newUser.Phone)
                    {
                        throw new Exception("User is duplicated");
                    }
                    else if (user.Name == newUser.Name)
                    {
                        if (user.Address == newUser.Address)
                        {
                            throw new Exception("User is duplicated");
                        }

                    }
                }

                Debug.WriteLine("User Created");
                return Ok(new Result()
                {
                    IsSuccess = true,
                    Message = "User Created"
                });
            }
            catch(Exception ex)
            {
                Debug.WriteLine("The user is duplicated");
                return Ok(new Result()
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }
    }
}
