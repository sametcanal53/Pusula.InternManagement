using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pusula.InternManagement.Interns
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            var hasher = new PasswordHasher<Intern>();
            return hasher.HashPassword(null, password);
        }
    }
}
