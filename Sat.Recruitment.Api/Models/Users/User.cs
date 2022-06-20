using System;

namespace Sat.Recruitment.Api.Models.Users
{
    public class User
    {
        //Model validation can also be achieved by Data annotations 
        private string _email;
        public string Name { get; set; }

        public string Email
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_email)) return _email;
                var aux = _email.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);
                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);
                return string.Join("@", aux[0], aux[1]);
            }
            set => _email = value;
        }

        public string Address { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
        public decimal Money { get; set; }
    }
}