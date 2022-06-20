using System;

namespace Sat.Recruitment.Api.Models.Users
{
    public class NormalUser : User
    {
        private decimal _money;

        public new decimal Money
        {
            get
            {
                if (_money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = _money * percentage;
                    _money += gif;
                }
                if (_money < 100)
                {
                    if (_money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = _money * percentage;
                        _money += gif;
                    }
                }
                return _money;
            }
            set => _money = value;
        }
    }
}