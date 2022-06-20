using System;
using System.Collections.Generic;
using System.IO;
using Sat.Recruitment.Api.Abstractions;
using Sat.Recruitment.Api.Models.Users;

namespace Sat.Recruitment.Api.Services
{
    public class FileDataStore : IDataStore<User>
    {
        private readonly IUserFactory _userFactory;
        private const string DirFileName = "/Files/Users.txt";

        public FileDataStore(IUserFactory userFactory)
        {
            _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
        }

        public IEnumerable<User> GetData()
        {
            var reader = ReadUsersFromFile();

            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLineAsync().Result;
                if (line != null)
                {
                    Enum.TryParse(line.Split(',')[4], out UserType userType);
                    var user = _userFactory.CreateUser(
                        line.Split(',')[0],
                        line.Split(',')[1],
                        line.Split(',')[3],
                        line.Split(',')[2],
                        userType,
                        line.Split(',')[5]);

                    yield return user;
                }
            }
            reader.Close();
        }


        private StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + DirFileName;

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}