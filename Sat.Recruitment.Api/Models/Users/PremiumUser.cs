namespace Sat.Recruitment.Api.Models.Users
{
    public class PremiumUser : User
    {
        private decimal _money;

        public new decimal Money
        {
            get
            {
                if (_money > 100)
                {
                    var gif = _money * 2;
                    _money += gif;
                }
                return _money;
            }
            set => _money = value;
        }
    }
}