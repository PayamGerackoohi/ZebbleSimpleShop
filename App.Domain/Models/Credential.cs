using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Credential : IValidable
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool StayLoggedIn = false;

        public bool IsValid() => !Username.None() && !Password.None();
    }
}
