using System;

namespace Sat.Recruitment.Api.Models.Users
{
    public class SuperUser : User
    {
        private decimal _money;

        public new decimal Money {
            get
            {
                if (_money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = _money * percentage;
                    return _money + gif;
                }
                return _money;
            }
            set => _money = value;
        }
    }
}